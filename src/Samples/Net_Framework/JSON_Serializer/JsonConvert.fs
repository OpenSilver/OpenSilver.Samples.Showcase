namespace Newtonsoft.Json

open OpenSilver
open System
open System.Collections
open System.Collections.Generic
open System.Reflection
open System.Threading.Tasks


//------------------------------------
// This extension adds JSON serialization/deserialization
// support to C#/XAML for OpenSilver (https://opensilver.net)
//
// It is a CSHTML5-compatible version of Newtonsoft JsonConvert,
// optimized for running in the browser by taking advantage
// of the JavaScript built-in methods for serialization
// and deserialization.
//
// This project is licensed under The open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2018 Userware / OpenSilver
//------------------------------------

[<AllowNullLiteral>]
type IJsonType =
    abstract member Item : string -> IJsonType
    abstract member Item : int -> IJsonType
    abstract member Value : obj

type JsonArray() =
    inherit List<obj>()

    interface IJsonType with
        member this.Item (key : string) : IJsonType =
            null

        member this.Item (index : int) : IJsonType =
            let item = base.[index]
            if item :? JsonObject || item :? JsonArray || item :? JsonValue then
                item :?> IJsonType
            else
                JsonValue(item)

        member this.Value =
            this :> obj

and JsonObject() =
    inherit Dictionary<string, obj>()

    interface IJsonType with
        member this.Item (key : string) : IJsonType =
            let item = base.[key]
            if item :? JsonObject || item :? JsonArray || item :? JsonValue then
                item :?> IJsonType
            else
                JsonValue(item)

        member this.Item (index : int) : IJsonType =
            null // TODO: Implement logic for accessing an item by index

        member this.Value =
            this :> obj

and JsonValue(value: obj) =
    member val Value : obj = value with get

    interface IJsonType with
        member this.Item (key : string) : IJsonType =
            null // TODO: Implement logic for accessing an item by key

        member this.Item  (index : int) : IJsonType =
            null // TODO: Implement logic for accessing an item by index

        member this.Value =
            this.Value

type Required =
    | Default
    | AllowNull
    | Always
    | DisallowNull

type NullValueHandling =
    | Include
    | Ignore

type JsonProperty(name: string) =
    inherit Attribute()
    member val Name : string = name with get, set
    member val Required : Required = Required.Default with get, set
    member val NullValueHandling : NullValueHandling = NullValueHandling.Include with get, set

    static member AreNullableStructsEqual(struct1: obj, struct2: obj) =
        if not (isNull struct1) then
            struct1.Equals(struct2)
        else
            isNull struct2

type JsonConvert() =
    static member SerializeObject(objectToSerialize: obj, ignoreErrors: bool) : string=
        let javaScriptObject = JsonConvert.ConvertCSharpObjectToJavaScriptObject(objectToSerialize, ignoreErrors)
        let serializedObject = Interop.ExecuteJavaScript("JSON.stringify($0)", javaScriptObject)
        serializedObject.ToString()

    static member DeserializeObject<'T>(json: string, ignoreErrors: bool) : Task<'T> = 
        async {
            let javaScriptObject = Interop.ExecuteJavaScript("JSON.parse($0)", json)
            let cSharpNestedDictionariesAndLists = JsonConvert.ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(javaScriptObject, ignoreErrors) |> Async.AwaitTask |> Async.RunSynchronously
            let cSharpObject = JsonConvert.ConvertCSharpNestedDictionariesAndListsToCSharpObject(typeof<'T>, cSharpNestedDictionariesAndLists, ignoreErrors)
            return unbox<'T> cSharpObject
        } |> Async.StartAsTask

    static member DeserializeObject(json: string, ignoreErrors) =
        async {
            let javaScriptObject = Interop.ExecuteJavaScript("JSON.parse($0)", json)
            let cSharpNestedDictionariesAndLists = JsonConvert.ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(javaScriptObject, ignoreErrors) |> Async.AwaitTask |> Async.RunSynchronously
            return cSharpNestedDictionariesAndLists
        }

    static member ConvertCSharpObjectToJavaScriptObject(cSharpObject: obj, ignoreErrors: bool) : obj=
        if cSharpObject :? Enum || cSharpObject :? Guid || cSharpObject :? int64 then
            cSharpObject.ToString()
        elif cSharpObject :? DateTime then
            let dateTimeUtc = ((cSharpObject :?> DateTime).ToUniversalTime())
            let timeSince1970 = dateTimeUtc - DateTime(1970, 1, 1, 0, 0, 0)
            let millisecondsSince1970 = timeSince1970.TotalMilliseconds
            let jsDate = Interop.ExecuteJavaScript("new Date($0)", millisecondsSince1970)
            Convert.ToString(Interop.ExecuteJavaScript("$0.toJSON()", jsDate))
        elif cSharpObject :? string then
            cSharpObject
        elif cSharpObject <> null && cSharpObject.GetType().IsValueType then
            Interop.ExecuteJavaScript("$0", InteropHelper.Unbox(cSharpObject)) // todo: merge this and the "if (cSharpObject :? string)" above.
        elif cSharpObject :? IEnumerable && not (cSharpObject :? string) then
            // ARRAY
            let jsArray = Interop.ExecuteJavaScript("[]")
            for cSharpItem in (cSharpObject :?> IEnumerable) do
                let jsItem = JsonConvert.ConvertCSharpObjectToJavaScriptObject(cSharpItem, ignoreErrors)
                Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsItem) |> ignore
            jsArray
        elif cSharpObject <> null then
            // OBJECT
            let jsObject = Interop.ExecuteJavaScript(@"new Object()")
            for property in cSharpObject.GetType().GetProperties(BindingFlags.Instance ||| BindingFlags.Public) do
                let propertyName = property.Name
                let propertyValue = property.GetValue(cSharpObject)
                if propertyValue <> null then
                    let recursionResult = JsonConvert.ConvertCSharpObjectToJavaScriptObject(propertyValue, ignoreErrors)
                    if recursionResult <> null then
                        Interop.ExecuteJavaScript(@"$0[$1] = $2;", jsObject, propertyName, recursionResult) |> ignore
            jsObject
        else
            Interop.ExecuteJavaScript("undefined")

    static member ConvertCSharpNestedDictionariesAndListsToCSharpObject(resultType: Type, cSharpNestedDictionariesAndLists : obj, ignoreErrors : bool) : obj option =
        let mutable result : obj option = None

        if cSharpNestedDictionariesAndLists = None then
            ()
        else if cSharpNestedDictionariesAndLists :? JsonValue then
            let value = (cSharpNestedDictionariesAndLists :?> JsonValue).Value
            if value = null then
                ()
            elif value :? string then
                // Enum, Guid, DateTime, String...
                if resultType <> null && resultType.IsEnum then
                    try
                        let enumValue = Enum.Parse(resultType, value :?> string)
                        result <- Some enumValue
                    with
                    | _ ->
                        raise (InvalidOperationException(sprintf "'%s' is not a valid value for the enum type '%s'." (value :?> string) resultType.FullName))
                elif resultType = typeof<Guid> || resultType = typeof<option<Guid>> then
                    try
                        let guid = Guid.Parse(value :?> string)
                        result <- Some guid
                    with
                    | _ ->
                        raise (InvalidOperationException(sprintf "'%s' is not a valid value for a Guid." (value :?> string)))
                elif resultType = typeof<DateTime> || resultType = typeof<option<DateTime>> then
                    try
                        // Uncomment when fully supported by CSHTML5:
                        // let dateTime = DateTime.Parse(value :?> string)
                        // dateTime

                        let dateString = value :?> string
                        let jsDate = Interop.ExecuteJavaScript("new Date($0)", dateString)

                        let year = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getFullYear()", jsDate))
                        let month = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getMonth()", jsDate)) + 1
                        let day = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getDate()", jsDate))
                        let hour = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getHours()", jsDate))
                        let minute = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getMinutes()", jsDate))
                        let second = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getSeconds()", jsDate))

                        let dateTime = DateTime(year, month, day, hour, minute, second)
                        result <- Some dateTime
                    with
                    | _ ->
                        raise (InvalidOperationException(sprintf "'%s' is not a valid value for a DateTime." (value :?> string)))
                elif resultType = typeof<string> then
                    // If the target type is a string, no conversion is needed:
                    result <- Some value
                elif resultType.IsValueType then
                    // If the target is another value type, it means that we are converting, for example, a string into a number or a bool
                    // (this is not supposed to happen if the JSON string is perfectly formatted, but it may happen if the JSON string
                    // contains numbers inside quotes, or booleans inside quotes):
                    // If the result type is Nullable, we need to get the underlying type in order to be able to properly call the "ChangeType"
                    // method below (useful, for example, when converting from "double" to nullable "int?"):
                    let nullableUnderlyingType = Nullable.GetUnderlyingType(resultType)
                    if nullableUnderlyingType <> null then
                        let convertedValue = Convert.ChangeType(value, nullableUnderlyingType)
                        result <- Some convertedValue
                    else
                        // We need to convert the value to the appropriate type because JSON does not contain type information:
                        let convertedValue = Convert.ChangeType(value, resultType)
                        result <- Some convertedValue
                else
                    if not ignoreErrors then
                        raise (InvalidOperationException(sprintf "Cannot convert a string to '%s'." (if resultType <> null then resultType.ToString() else "null")))
            elif value.GetType().IsValueType then
                // Other value types
                // Verify that the target type is also a value type:
                if resultType <> null && resultType.IsValueType then
                    // If the result type is Nullable, we need to get the underlying type in order to be able to properly call the "ChangeType"
                    // method below (useful, for example, when converting from "double" to nullable "int?"):
                    let nullableUnderlyingType = Nullable.GetUnderlyingType(resultType)
                    let mutable _resultType = resultType
                    if nullableUnderlyingType <> null then
                        _resultType <- nullableUnderlyingType

                    // We need to convert the value to the appropriate type because JSON does not contain type information.
                    // For example, it could be a "double" that we need to set as an "int".
                    let mutable convertedValue = Convert.ChangeType(value, _resultType)
                    if _resultType = typeof<int64> then
                        convertedValue <- Convert.ToInt64(value)
                    result <- Some convertedValue
                else
                    if not ignoreErrors then
                        raise (InvalidOperationException(sprintf "Cannot convert a value type to '%s'." (if resultType <> null then resultType.ToString() else "null")))
                    else
                        result <- Some (Activator.CreateInstance resultType)
            else
                if not ignoreErrors then
                    raise (Exception("Unexpected type: " + value.GetType().ToString()))
        elif cSharpNestedDictionariesAndLists :? JsonArray then
            let mutable genericArguments : Type array = [||]
            if resultType.IsArray then
                // Array
                // Get the type of the array items:
                let itemsType = resultType.GetElementType()

                // Create a new list that we will then convert to an array:
                let list = new List<obj>()

                // Add the items to the ArrayList:
                for item in (cSharpNestedDictionariesAndLists :?> IEnumerable) do
                    // ********** RECURSION **********
                    let recursionResult = JsonConvert.ConvertCSharpNestedDictionariesAndListsToCSharpObject(itemsType, item, ignoreErrors)
                    list.Add(recursionResult)

                // Convert the list to the expected array type:
                let array = Array.CreateInstance(itemsType, list.Count)
                let mutable i = 0
                for element in list do
                    array.SetValue(element, i)
                    i <- i + 1
                result <- Some array
            elif resultType.IsGenericType then
                genericArguments <- resultType.GetGenericArguments()

                if (genericArguments.Length > 0  && JsonConvert.IsAssignableToGenericEnumerable(resultType, genericArguments[0])) then
                    // IEnumerable<T>
                    // Get the type of the IEnumerable items (this works, for example, with List<T>, ObservableCollection<T>, etc.):
                    let itemsType = genericArguments[0]

                    // Create a temporary List<T> to add items to it. Later, we will convert it to the final type if needed.
                    let typeOfList = typedefof<List<'T>>.MakeGenericType [| itemsType |]
                    let list = Activator.CreateInstance(typeOfList)

                    // Get a reference to the "Add" method of the generic list that we just created:
                    let listAddMethod = list.GetType().GetMethod("Add")

                    // Add the items to the list:
                    for item in (cSharpNestedDictionariesAndLists :?> IEnumerable) do
                        // ********** RECURSION **********
                        let recursionResult = JsonConvert.ConvertCSharpNestedDictionariesAndListsToCSharpObject(itemsType, item, ignoreErrors)
                        listAddMethod.Invoke(list, [| box recursionResult |] ) |> ignore

                    // If the result type is List<> or a compatible interface, we directly return the List<> that we created above,
                    // otherwise, we convert it to the result type:
                    let genericTypeDefinition = resultType.GetGenericTypeDefinition()
                    if genericTypeDefinition = typedefof<List<obj>> ||
                       genericTypeDefinition = typedefof<IList<obj>> ||
                       genericTypeDefinition = typedefof<IEnumerable<obj>> ||
                       genericTypeDefinition = typedefof<ICollection<obj>> ||
                       genericTypeDefinition = typedefof<IReadOnlyCollection<obj>> ||
                       genericTypeDefinition = typedefof<IReadOnlyList<obj>> then
                        // Return the List<T>:
                        result <- Some list
                    else
                        // Otherwise, attempt to create a new instance of the result type while passing the items as the first argument
                        // of the constructor (this works, for example, with ObservableCollection<T> and other common collections):
                        try
                            result <- Some (Activator.CreateInstance (resultType, [| list |]))
                        with
                        | _ ->
                            if not ignoreErrors then
                                raise (InvalidOperationException("Type not supported by JsonConvert: " + (if resultType <> null then resultType.ToString() else "null")))
                            else
                                ()
                else
                    if not ignoreErrors then
                        raise (InvalidOperationException("Type not supported by JsonConvert: " + (if resultType <> null then resultType.ToString() else "null")))
                    else
                        ()
            else
                if not ignoreErrors then
                    raise (InvalidOperationException("Type not supported by JsonConvert: " + (if resultType <> null then resultType.ToString() else "null")))
                else
                    ()
        elif cSharpNestedDictionariesAndLists :? JsonObject then
            // Object with properties
            // Create a new instance of the resulting type:
            try
                result <- Some (Activator.CreateInstance resultType)
            with
            | ex ->
                if not ignoreErrors then
                    raise (InvalidOperationException(sprintf "Unable to create an instance of type '%s'. A common cause is that the type does not have a default public constructor." (if resultType <> null then resultType.ToString() else "null"), ex))
                else
                    ()

            // Make a dictionary of all the destination object properties, for fast lookup:
            let propertyNameToTargetProperty = new Dictionary<string, PropertyInfo>()
            let propertyNameToTargetProperty_Lowercase = new Dictionary<string, PropertyInfo>()
            for property in resultType.GetProperties(BindingFlags.Instance ||| BindingFlags.Public) do
                propertyNameToTargetProperty.[property.Name] <- property
                propertyNameToTargetProperty_Lowercase.[property.Name.ToLower()] <- property // This line adds or updates if the key already exists (in case of multiple properties that have the same Lowercase representation, we keep only the last one).

            // Traverse each property:
            for keyValuePair in (cSharpNestedDictionariesAndLists :?> JsonObject) do
                let propertyName = keyValuePair.Key;
                let propertyValue = keyValuePair.Value;
                let mutable targetProperty = null

                // First, check case-sensitively:
                if propertyNameToTargetProperty.ContainsKey(propertyName) then
                    targetProperty <- propertyNameToTargetProperty.[propertyName]

                // Alternatively, check case-insensitively:
                elif propertyNameToTargetProperty_Lowercase.ContainsKey(propertyName.ToLower()) then
                    targetProperty <- propertyNameToTargetProperty_Lowercase.[propertyName.ToLower()]

                if targetProperty <> null then
                    // ********** RECURSION **********
                    let recursionResult = JsonConvert.ConvertCSharpNestedDictionariesAndListsToCSharpObject(targetProperty.PropertyType, propertyValue, ignoreErrors)
                    if recursionResult <> None then
                        targetProperty.SetValue(result, recursionResult)
                else
                    if not ignoreErrors then
                        raise (Exception(sprintf "Property '%s' not found in type '%s'" propertyName (if resultType <> null then resultType.ToString() else "null")))
        else
            if not ignoreErrors then
                raise (Exception("Unexpected type: " + cSharpNestedDictionariesAndLists.GetType().ToString()))
            else
                ()

        result

    static member ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(javaScriptObject: obj, ignoreErrors: bool) =
        let mutable result : IJsonType option = None
        let taskCompletionSource = new TaskCompletionSource<IJsonType option>()

        let onCompleted () =
            taskCompletionSource.SetResult(result)

        let isArray = Convert.ToBoolean(Interop.ExecuteJavaScript("Array.isArray($0)", javaScriptObject))
        let isObject = Convert.ToBoolean(Interop.ExecuteJavaScript(@"(typeof($0) === ""object"")", javaScriptObject))
        let isNumber = Convert.ToBoolean(Interop.ExecuteJavaScript(@"(typeof($0) === ""number"")", javaScriptObject))
        let isBoolean = Convert.ToBoolean(Interop.ExecuteJavaScript(@"(typeof($0) === ""boolean"")", javaScriptObject))
        let isNullOrUndefined = Convert.ToBoolean(Interop.ExecuteJavaScript(@"($0 === undefined || $0 === null)", javaScriptObject))

        if isNullOrUndefined then
            result <- None
            onCompleted()
        elif isArray then
            let arrayItemsCount = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.length", javaScriptObject))
            let array = JsonArray()
            result <- Some (array :> IJsonType)
            if arrayItemsCount > 0 then
                let mutable currentIndex = 0
                let methodToAddObject = fun (jsObj: obj) ->
                    task {
                        JsonConvert.ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(jsObj, ignoreErrors)
                        |> Async.AwaitTask
                        |> Async.RunSynchronously
                        |> fun resultObj ->
                            array.Add(resultObj)
                            currentIndex <- currentIndex + 1
                            if currentIndex = arrayItemsCount then
                                onCompleted()
                    }

                // Note: the following JS call must be asynchronous because
                // this algorithm is recursive and, in the Simulator, it is
                // not possible to recursively call "ExecuteJavaScript"
                // (ie. to make nested synchronous calls to JS).

                let methodToUpdateDelegate : Delegate = upcast Func<obj, Task<unit>>(fun jsObj -> methodToAddObject(jsObj))

                Interop.ExecuteJavaScriptAsync(@"
                var input = $0;
                var methodToAddObject = $1;
                for (var i = 0; i < input.length; i++) {
                    methodToAddObject(input[i]);
                }",
                    javaScriptObject,
                    methodToUpdateDelegate) |> ignore
            else
                onCompleted()
        elif isObject then
            let keysCount = Convert.ToInt32(Interop.ExecuteJavaScript(@"Object.keys($0).length", javaScriptObject))
            let jObject = JsonObject()
            result <- Some (jObject :> IJsonType)
            if keysCount > 0 then
                let mutable currentIndex = 0
                let methodToAddObject = fun (key: obj, jsObj: obj) ->
                    task {
                        JsonConvert.ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(jsObj, ignoreErrors)
                        |> Async.AwaitTask
                        |> Async.RunSynchronously
                        |> fun resultObj ->
                            jObject.Add(key.ToString(), resultObj)
                            currentIndex <- currentIndex + 1
                            if currentIndex = keysCount then
                                onCompleted()
                    }

                // Note: the following JS call must be asynchronous because
                // this algorithm is recursive and, in the Simulator, it is
                // not possible to recursively call "ExecuteJavaScript"
                // (ie. to make nested synchronous calls to JS).

                let methodToUpdateDelegate : Delegate = upcast Func<obj, obj, Task<unit>>(fun key jsObj -> methodToAddObject(key, jsObj))

                Interop.ExecuteJavaScriptAsync(@"
                var input = $0;
                var methodToAddObject = $1;
                for (var key in input) {
                    var str = key.toString();
                    methodToAddObject(str, input[key]);
                }",
                    javaScriptObject,
                    methodToUpdateDelegate) |> ignore
            else
                onCompleted()
        elif isNumber then
            result <- Some (JsonValue(Convert.ToDouble(javaScriptObject)) :> IJsonType)
            onCompleted()
        elif isBoolean then
            result <- Some (JsonValue(Convert.ToBoolean(javaScriptObject)) :> IJsonType)
            onCompleted()
        else
            result <- Some (JsonValue(javaScriptObject.ToString()) :> IJsonType)
            onCompleted()

        taskCompletionSource.Task

    static member IsAssignableToGenericEnumerable(genericType: Type, itemsType: Type) : bool =
        // Note: the code above was commented and replaced by the code below because in some
        // cases the call to "IsAssignableFrom" led to an exception due to an issue in the
        // JS implementation in the JSIL libraries (reference: CSHTML5 tickets #623 and #648).

        let ienumerableInterface = JsonConvert.GetInterface(genericType, (typeof<IEnumerable<_>>.Name))
        ienumerableInterface <> None

    static member GetInterface(_type: Type, name: string) : Type option =
        // Note: this method is here because "Type.GetInterface(name)" is not yet
        // available in the JSIL libraries as of July 14, 2017.
        // When available, this method can be replaced with "Type.GetInterface(name)".

        let interfaces = _type.GetInterfaces()
        let matchingInterface =
            interfaces
            |> Seq.tryFind (fun intf -> intf.Name = name)

        matchingInterface

    static member CreateNewInstanceOfGenericList<'T> () =
        new List<'T>()

    static member ChangeType(value: obj, conversionType: Type) : obj =
        // Note: this method is here to work around an issue in the "Convert.ChangeType"
        // implementation in the JSIL libraries as of July 14, 2017, which does not
        // appear to give the appropriate result when converting from a string to
        // an integer or boolean. It can be replaced with "Convert.ChangeType" when
        // fixed.

        if conversionType = typeof<bool> then
            Convert.ToBoolean(value)
        elif conversionType = typeof<int16> then
            Convert.ToInt16(value)
        elif conversionType = typeof<int32> then
            Convert.ToInt32(value)
        elif conversionType = typeof<int64> then
            Convert.ToInt64(value)
        elif conversionType = typeof<byte> then
            Convert.ToByte(value)
        elif conversionType = typeof<double> then
            Convert.ToDouble(value)
        elif conversionType = typeof<float> then
            Convert.ToSingle(value)
        elif conversionType = typeof<char> then
            Convert.ToChar(value)
        else
            Convert.ChangeType(value, conversionType)
