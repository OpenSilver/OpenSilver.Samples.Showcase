Imports CSHTML5
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading.Tasks


'------------------------------------
' This extension adds JSON serialization/deserialization
' support to C#/XAML for OpenSilver (https://opensilver.net)
'
' It is a CSHTML5-compatible version of Newtonsoft JsonConvert,
' optimized for running in the browser by taking advantage
' of the JavaScript built-in methods for serialization
' and deserialization.
'
' This project is licensed under The open-source MIT license:
' https://opensource.org/licenses/MIT
'
' Copyright 2018 Userware / OpenSilver
'------------------------------------


Namespace Global.Newtonsoft.Json
    Public Module JsonConvert
#Region "Public Methods"

        Public Function SerializeObject(ByVal objectToSerialize As Object, ByVal Optional ignoreErrors As Boolean = False) As String
            Dim javaScriptObject = ConvertCSharpObjectToJavaScriptObject(objectToSerialize, ignoreErrors)
            Dim serializedObject As String = Interop.ExecuteJavaScript("JSON.stringify($0)", javaScriptObject).ToString()
            Return serializedObject
        End Function

        'where T : class, new()
        Public Async Function DeserializeObject(Of T)(ByVal json As String, ByVal Optional ignoreErrors As Boolean = False) As Task(Of T)
            Dim javaScriptObject = Interop.ExecuteJavaScript("JSON.parse($0)", json)
            Dim cSharpNestedDictionariesAndLists = Await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(javaScriptObject, ignoreErrors)
            Dim cSharpObject As T = ConvertCSharpNestedDictionariesAndListsToCSharpObject(GetType(T), cSharpNestedDictionariesAndLists, ignoreErrors)
            Return cSharpObject
        End Function

        Public Async Function DeserializeObject(ByVal json As String, ByVal Optional ignoreErrors As Boolean = False) As Task(Of IJsonType)
            Dim javaScriptObject = Interop.ExecuteJavaScript("JSON.parse($0)", json)
            Dim cSharpNestedDictionariesAndLists = Await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(javaScriptObject, ignoreErrors)
            Return cSharpNestedDictionariesAndLists
        End Function

#End Region

#Region "Private Methods"
        Private Function ConvertCSharpObjectToJavaScriptObject(ByVal cSharpObject As Object, ByVal ignoreErrors As Boolean) As Object
            If TypeOf cSharpObject Is [Enum] OrElse TypeOf cSharpObject Is Guid OrElse TypeOf cSharpObject Is Long Then
                Return cSharpObject.ToString()
            ElseIf TypeOf cSharpObject Is Date Then
                'Uncomment when fully supported by CSHTML5:
                'return ((DateTime)cSharpObject).ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);

                Dim dateTimeUtc = CDate(cSharpObject).ToUniversalTime()
                Dim timeSince1970 As TimeSpan = (dateTimeUtc - New DateTime(1970, 1, 1, 0, 0, 0))
                Dim millisecondsSince1970 = timeSince1970.TotalMilliseconds
                Dim jsDate = Interop.ExecuteJavaScript("new Date($0)", millisecondsSince1970)
                Dim json = Convert.ToString(Interop.ExecuteJavaScript("$0.toJSON()", jsDate))
                Return json
#If Not BRIDGE Then
            ElseIf TypeOf cSharpObject Is String OrElse (cSharpObject IsNot Nothing AndAlso cSharpObject.GetType().IsValueType) Then
#Else
            ElseIf TypeOf cSharpObject Is String Then
#End If
                Return cSharpObject
#If BRIDGE Then
            ElseIf cSharpObject IsNot Nothing AndAlso cSharpObject.GetType().IsValueType Then
                Return Interop.ExecuteJavaScript("$0", Unbox(cSharpObject)) 'todo: merge this and the "if (cShrapObject is string)" above.
#End If
            ElseIf TypeOf cSharpObject Is IEnumerable AndAlso Not (TypeOf cSharpObject Is String) Then
                '----------------
                ' ARRAY
                '----------------

                ' Create the JS array:
                Dim jsArray = Interop.ExecuteJavaScript("[]")

                ' Traverse the enumerable:
                For Each cSharpItem In CType(cSharpObject, IEnumerable)
                    Dim jsItem = ConvertCSharpObjectToJavaScriptObject(cSharpItem, ignoreErrors)
                    Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsItem)
                Next

                Return jsArray
            ElseIf cSharpObject IsNot Nothing Then
                '----------------
                ' OBJECT
                '----------------

                Dim jsObject = Interop.ExecuteJavaScript("new Object()")

                ' Traverse all properties:
                For Each [property] In cSharpObject.GetType().GetProperties(BindingFlags.Instance Or BindingFlags.Public)
                    Dim propertyName = [property].Name
                    Dim propertyValue = [property].GetValue(cSharpObject)

                    If propertyValue IsNot Nothing Then
                        Dim recursionResult = ConvertCSharpObjectToJavaScriptObject(propertyValue, ignoreErrors)
                        If recursionResult IsNot Nothing Then
                            Interop.ExecuteJavaScript("$0[$1] = $2;", jsObject, propertyName, recursionResult)
                        End If
                    End If
                Next

                Return jsObject
            Else
                Return Interop.ExecuteJavaScript("undefined")
            End If
        End Function

        Private Function ConvertCSharpNestedDictionariesAndListsToCSharpObject(ByVal resultType As Type, ByVal cSharpNestedDictionariesAndLists As Object, ByVal ignoreErrors As Boolean) As Object
            Dim result As Object

            If cSharpNestedDictionariesAndLists Is Nothing Then
                Return Nothing
            ElseIf TypeOf cSharpNestedDictionariesAndLists Is JsonValue Then
                Dim value = CType(cSharpNestedDictionariesAndLists, JsonValue).Value
                If value Is Nothing Then
                    Return Nothing
                ElseIf TypeOf value Is String Then
                    '--------
                    ' Enum, Guid, DateTime, String...
                    '--------

                    ' If the target type is an Enum, convert from string to the Enum:
                    If resultType IsNot Nothing AndAlso resultType.IsEnum Then
                        Try
                            Dim enumValue = [Enum].Parse(resultType, CStr(value))
                            Return enumValue
                        Catch
                            Throw New InvalidOperationException(String.Format("'{0}' is not a valid value for the enum type '{1}'.", CStr(value), resultType.ToString()))
                        End Try
                        ' If the target type is a Guid, convert from string to the Guid:
                    ElseIf resultType Is GetType(Guid) OrElse resultType Is GetType(Guid?) Then
                        Try
                            Dim guid = System.Guid.Parse(CStr(value))
                            Return guid
                        Catch
                            Throw New InvalidOperationException(String.Format("'{0}' is not a valid value for a Guid.", CStr(value)))
                        End Try
                        ' If the target type is a DateTime, convert from string to the DateTime:
                    ElseIf resultType Is GetType(Date) OrElse resultType Is GetType(Date?) Then
                        Try
                            'Uncomment when fully supported by CSHTML5:
                            'var dateTime = DateTime.Parse((string)value);
                            'return dateTime;

                            Dim dateString = CStr(value)
                            Dim jsDate = Interop.ExecuteJavaScript("new Date($0)", dateString)

                            Dim year = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getFullYear()", jsDate))
                            Dim month = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getMonth()", jsDate)) + 1 ' Note: month index in JS starts at "0".
                            Dim day = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getDate()", jsDate))
                            Dim hour = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getHours()", jsDate))
                            Dim minute = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getMinutes()", jsDate))
                            Dim second = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getSeconds()", jsDate))

                            Dim dateTime As Date = New DateTime(year, month, day, hour, minute, second)

                            Return dateTime
                        Catch
                            Throw New InvalidOperationException(String.Format("'{0}' is not a valid value for a DateTime.", CStr(value)))
                        End Try
                    ElseIf resultType Is GetType(String) Then
                        ' If the target type is a string, no conversion is needed:
                        Return value
                        ' If the target is another value type, it means that we are converting for example a string into a number or a bool (this is not supposed to happen if the JSON string is perfectly formatted, but it may happen if the JSON string contains numbers inside quotes, or booleans inside quotes):
                    ElseIf resultType.IsValueType Then
                        ' If the result type is Nullable, we need to get the underlying type in order to be able to properly call the "ChangeType" method below (useful for example when converting from "double" to nullable "int?"):
                        Dim nullableUnderlyingType = Nullable.GetUnderlyingType(resultType)
                        If nullableUnderlyingType IsNot Nothing Then
                            resultType = nullableUnderlyingType
                        End If

                        ' We need to convert the value to the appropriate type because JSON does not contain type information:
                        Dim convertedValue = ChangeType(value, resultType)
                        Return convertedValue
                    Else
                        If Not ignoreErrors Then
                            Throw New InvalidOperationException(String.Format("Cannot convert a string to '{0}'.", (If(resultType IsNot Nothing, resultType.ToString(), "null"))))
                        Else
                            Return Nothing
                        End If
                    End If
                ElseIf value.GetType().IsValueType Then
                    '--------
                    ' Other value types
                    '--------

                    ' Verify that the target type is also a value type:
                    If resultType IsNot Nothing AndAlso resultType.IsValueType Then
                        ' If the result type is Nullable, we need to get the underlying type in order to be able to properly call the "ChangeType" method below (useful for example when converting from "double" to nullable "int?"):
                        Dim nullableUnderlyingType = Nullable.GetUnderlyingType(resultType)
                        If nullableUnderlyingType IsNot Nothing Then
                            resultType = nullableUnderlyingType
                        End If

                        ' We need to convert the value to the appropriate type because JSON does not contain type information. For example, it could be an "double" that we need to set as an "int".
                        Dim convertedValue = Convert.ChangeType(value, resultType)
                        If resultType Is GetType(Long) Then convertedValue = Convert.ToInt64(value)
                        Return convertedValue
                    Else
                        If Not ignoreErrors Then
                            Throw New InvalidOperationException(String.Format("Cannot convert a value type to '{0}'.", (If(resultType IsNot Nothing, resultType.ToString(), "null"))))
                        Else
                            Return Activator.CreateInstance(resultType)
                        End If
                    End If
                Else
                    If Not ignoreErrors Then
                        Throw New Exception("Unexpected type: " & value.GetType().ToString())
                    Else
                        Return Nothing
                    End If
                End If
            ElseIf TypeOf cSharpNestedDictionariesAndLists Is JsonArray Then
                Dim genericArguments As Type()
                If resultType.IsArray Then
                    '--------
                    ' Array
                    '--------

                    ' Get the type of the array items:
                    Dim itemsType As Type = resultType.GetElementType()

                    ' Create a new list that we will then convert to an array:
#If BRIDGE Then
                    Dim list = New List(Of Object)()
#Else
                    Dim list = New ArrayList()
#End If

                    ' Add the items to the ArrayList:
                    For Each item In CType(cSharpNestedDictionariesAndLists, IEnumerable)
                        '********** RECURSION **********
                        Dim recursionResult = ConvertCSharpNestedDictionariesAndListsToCSharpObject(itemsType, item, ignoreErrors)
                        list.Add(recursionResult)
                    Next

                    ' Convert the list to the expected array type:
#If BRIDGE Then
                    Dim array = Array.CreateInstance(itemsType, list.Count)
                    Dim i = 0
                    For Each element In list
                        array.SetValue(element, i)
                        Threading.Interlocked.Increment(i)
                    Next
                    result = array
#Else
                    result = list.ToArray(itemsType)
#End If

                ElseIf resultType.IsGenericType AndAlso (CSharpImpl.__Assign(genericArguments, resultType.GetGenericArguments())).Length > 0 AndAlso IsAssignableToGenericEnumerable(resultType, genericArguments(0)) Then
                    '--------
                    ' IEnumerable<T>
                    '--------

                    ' Get the type of the IEnumerable items (this works for example with List<T>, ObservableCollection<T>, etc.):
                    Dim itemsType = genericArguments(0)

                    ' Create a temporary List<T> in order to add items to it. Later we will convert it to the final type if needed.
#If BRIDGE Then
                    Dim typeOfList = GetType(List(Of))
                    Dim list = Activator.CreateInstance(typeOfList.MakeGenericType(New Type() {itemsType}))
                    'var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { itemsType })); //Note: For some reason, this doesn't work in a single line in the Simulator for the Bridge Version (V2) so we have to do as above.
#Else
                    Dim list = GetType(JsonConvert).GetMethod("CreateNewInstanceOfGenericList", BindingFlags.NonPublic Or BindingFlags.Static).MakeGenericMethod(itemsType).Invoke(Nothing, New Object() {})
#End If

                    ' Note: in the code above, we call the method "CreateNewInstanceOfGenericList" instead of
                    ' calling "var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(itemsType))"
                    ' because the latter does not appear to properly initialize the underlying JavaScript "_items"
                    ' collection that is inside the List<T> implementation in JSIL as of July 14, 2017, thus
                    ' resulting in an exception when calling the Add method (reference: CSHTML5 tickets #623 and
                    ' #648).

                    ' Get a reference to the "Add" method of the generic list that we just created:
                    Dim listAddMethod = list.GetType().GetMethod("Add")

                    ' Add the items to the list:
                    For Each item In CType(cSharpNestedDictionariesAndLists, IEnumerable)
                        '********** RECURSION **********
                        Dim recursionResult = ConvertCSharpNestedDictionariesAndListsToCSharpObject(itemsType, item, ignoreErrors)
                        listAddMethod.Invoke(list, New Object() {recursionResult})
                    Next

                    ' If the result type is List<> or a compatible interface, we directly return the List<> that we created above, otherwise we convert it to the result type:
                    Dim genericTypeDefinition As Type = resultType.GetGenericTypeDefinition()
                    If genericTypeDefinition Is GetType(List(Of)) OrElse genericTypeDefinition Is GetType(IList(Of)) OrElse genericTypeDefinition Is GetType(IEnumerable(Of)) OrElse genericTypeDefinition Is GetType(ICollection(Of)) OrElse genericTypeDefinition Is GetType(IReadOnlyCollection(Of)) OrElse genericTypeDefinition Is GetType(IReadOnlyList(Of)) Then
                        ' Return the List<T>:
                        result = list
                    Else
                        ' Otherwise, attempt to create a new instance of the result type while passing the items as first argument of the constructor (this works for example with ObservableCollection<T> and other common collections):
                        Try
                            result = Activator.CreateInstance(resultType, New Object() {list})
                        Catch
                            If Not ignoreErrors Then
                                Throw New InvalidOperationException("Type not supported by JsonConvert: " & (If(resultType IsNot Nothing, resultType.ToString(), "null")))
                            Else
                                Return Nothing
                            End If
                        End Try
                    End If
                Else
                    If Not ignoreErrors Then
                        Throw New InvalidOperationException("Type not supported by JsonConvert: " & (If(resultType IsNot Nothing, resultType.ToString(), "null")))
                    Else
                        Return Nothing
                    End If
                End If
            ElseIf TypeOf cSharpNestedDictionariesAndLists Is JsonObject Then
                '--------
                ' Object with properties
                '--------

                ' Create a new instance of the resulting type:
                Try
                    result = Activator.CreateInstance(resultType)
                Catch ex As Exception
                    If Not ignoreErrors Then
                        Throw New InvalidOperationException(String.Format("Unable to create an instance of type '{0}'. A common cause is that the type does not have a default public constructor.", (If(resultType IsNot Nothing, resultType.ToString(), "null"))), ex)
                    Else
                        Return Nothing
                    End If
                End Try

                ' Make a dictionary of all the destination object properties, for fast lookup:
                Dim propertyNameToTargetProperty = New Dictionary(Of String, PropertyInfo)()
                Dim propertyNameToTargetProperty_Lowercase = New Dictionary(Of String, PropertyInfo)()
                For Each [property] In resultType.GetProperties(BindingFlags.Instance Or BindingFlags.Public)
                    propertyNameToTargetProperty.Add([property].Name, [property])
                    propertyNameToTargetProperty_Lowercase([property].Name.ToLower()) = [property] ' This line adds or updates if the key already exists (in case of multiple properties that have the same Lowercase representation, we keep only the last one).
                Next

                ' Traverse each property:
                For Each keyValuePair In CType(cSharpNestedDictionariesAndLists, JsonObject)
                    Dim propertyName = keyValuePair.Key
                    Dim propertyValue = keyValuePair.Value

                    Dim targetProperty As PropertyInfo = Nothing

                    ' First, check case-sensitively:
                    If propertyNameToTargetProperty.ContainsKey(propertyName) Then
                        targetProperty = propertyNameToTargetProperty(propertyName)
                        ' Alternatively, check case-insensitively:
                    ElseIf propertyNameToTargetProperty_Lowercase.ContainsKey(propertyName.ToLower()) Then
                        targetProperty = propertyNameToTargetProperty_Lowercase(propertyName.ToLower())
                    End If

                    If targetProperty IsNot Nothing Then
                        '********** RECURSION **********
                        Dim recursionResult = ConvertCSharpNestedDictionariesAndListsToCSharpObject(targetProperty.PropertyType, propertyValue, ignoreErrors)
                        If recursionResult IsNot Nothing Then
                            targetProperty.SetValue(result, recursionResult)
                        End If
                    Else
                        If Not ignoreErrors Then Throw New Exception(String.Format("Property '{0}' not found in type '{1}'", propertyName, (If(resultType IsNot Nothing, resultType.ToString(), "null"))))
                    End If
                Next
            Else
                If Not ignoreErrors Then
                    Throw New Exception("Unexpected type: " & cSharpNestedDictionariesAndLists.GetType().ToString())
                Else
                    Return Nothing
                End If
            End If

            Return result
        End Function

        Private Function ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(ByVal javaScriptObject As Object, ByVal ignoreErrors As Boolean) As Task(Of IJsonType)
            ' Note: This method needs to be "async" because it is
            ' recursive and, in the Simulator, it is not possible
            ' to recursively call "ExecuteJavaScript" (ie. to make
            ' nested synchronous calls to JS).

            Dim result As IJsonType = Nothing
            Dim taskCompletionSource = New TaskCompletionSource(Of IJsonType)()
            Dim onCompleted As Action = Sub() taskCompletionSource.SetResult(result)

            Dim isArray = Convert.ToBoolean(Interop.ExecuteJavaScript("Array.isArray($0)", javaScriptObject))
            Dim isObject = Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof($0) === ""object"")", javaScriptObject))
            Dim isNumber = Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof($0) === ""number"")", javaScriptObject))
            Dim isBoolean = Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof($0) === ""boolean"")", javaScriptObject))
            Dim isNullOrUndefined = Convert.ToBoolean(Interop.ExecuteJavaScript("($0 === undefined || $0 === null)", javaScriptObject))

            If isNullOrUndefined Then
                result = Nothing
                onCompleted()
            ElseIf isArray Then
                Dim arrayItemsCount = Convert.ToInt32(Interop.ExecuteJavaScript("$0.length", javaScriptObject))
                result = New JsonArray()
                If arrayItemsCount > 0 Then
                    Dim currentIndex = 0
                    Dim methodToAddObject As Action(Of Object) = Async Sub(jsObj)
                                                                     CType(result, JsonArray).Add(Await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(jsObj, ignoreErrors))
                                                                     currentIndex += 1
                                                                     If currentIndex = arrayItemsCount Then onCompleted()
                                                                 End Sub

                    ' Note: the following JS call must be asynchronous because
                    ' this algorithm is recursive and, in the Simulator, it is
                    ' not possible to recursively call "ExecuteJavaScript"
                    ' (ie. to make nested synchronous calls to JS).

                    Interop.ExecuteJavaScriptAsync("
                    var input = $0;
                    var methodToAddObject = $1;
                    for (var i = 0; i < input.length; i++) {
                        methodToAddObject(input[i]);
                    }", javaScriptObject, methodToAddObject)
                Else
                    onCompleted()
                End If
            ElseIf isObject Then
                Dim keysCount = Convert.ToInt32(Interop.ExecuteJavaScript("Object.keys($0).length", javaScriptObject))
                result = New JsonObject()
                If keysCount > 0 Then
                    Dim currentIndexInKeys = 0
                    Dim methodToAddObject As Action(Of Object, Object) = Async Sub(key, jsObj)
                                                                             CType(result, Dictionary(Of String, Object))(key.ToString()) = Await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(jsObj, ignoreErrors)
                                                                             currentIndexInKeys += 1
                                                                             If currentIndexInKeys = keysCount Then onCompleted()
                                                                         End Sub

                    ' Note: the following JS call must be asynchronous because
                    ' this algorithm is recursive and, in the Simulator, it is
                    ' not possible to recursively call "ExecuteJavaScript"
                    ' (ie. to make nested synchronous calls to JS).

                    Interop.ExecuteJavaScriptAsync("
                    var input = $0;
                    var methodToAddObject = $1;
                    for (var key in input) {
                        var str = key.toString();
                        //alert(""key: "" + str);
                        methodToAddObject(str, input[key]);
                    }", javaScriptObject, methodToAddObject)
                Else
                    onCompleted()
                End If
            ElseIf isNumber Then
                result = New JsonValue(Convert.ToDouble(javaScriptObject))
                onCompleted()
            ElseIf isBoolean Then
                result = New JsonValue(Convert.ToBoolean(javaScriptObject))
                onCompleted()
            Else
                result = New JsonValue(javaScriptObject.ToString())
                onCompleted()
            End If
            Return taskCompletionSource.Task
        End Function

        Private Function IsAssignableToGenericEnumerable(ByVal genericType As Type, ByVal itemsType As Type) As Boolean
            ' 
            ' var enumerableType = typeof(IEnumerable<>);
            ' var constructedEnumerableType = enumerableType.MakeGenericType(itemsType);
            ' return constructedEnumerableType.IsAssignableFrom(genericType);
            ' 

            ' Note: the code above was commented and replaced by the code below because in some
            ' cases the call to "IsAssignableFrom" let to an exception due to an issue in the
            ' JS implementation in the JSIL libraries (reference: CSHTML5 tickets #623 and #648).

            Dim ienumerableInterface = GetInterface(genericType, GetType(IEnumerable(Of)).Name)
            Return ienumerableInterface IsNot Nothing
        End Function

        Private Function GetInterface(ByVal type As Type, ByVal name As String) As Type
            ' Note: this method is here because "Type.GetInterface(name)" is not yet
            ' available in the JSIL libraries as of July 14, 2017.
            ' When available, this method can be replaced with "Type.GetInterface(name)".

            For Each theInterface In type.GetInterfaces()
                If Equals(theInterface.Name, name) Then Return theInterface
            Next
            Return Nothing
        End Function

        Private Function CreateNewInstanceOfGenericList(Of T)() As IList
            Return New List(Of T)()
        End Function

        Private Function ChangeType(ByVal value As Object, ByVal conversionType As Type) As Object
            ' Note: this method is here to work around an issue in the "Convert.ChangeType"
            ' implementation in the JSIL libraries as of July 14, 2017, which does not
            ' appear to give the appropriate result when converting from a string to
            ' an integer or boolean. It can be replaced with "Convert.ChangeType" when
            ' fixed.

            If conversionType Is GetType(Boolean) Then
                Return Convert.ToBoolean(value)
            ElseIf conversionType Is GetType(Short) Then
                Return Convert.ToInt16(value)
            ElseIf conversionType Is GetType(Integer) Then
                Return Convert.ToInt32(value)
            ElseIf conversionType Is GetType(Long) Then
                Return Convert.ToInt64(value)
            ElseIf conversionType Is GetType(Byte) Then
                Return Convert.ToByte(value)
            ElseIf conversionType Is GetType(Double) Then
                Return Convert.ToDouble(value)
            ElseIf conversionType Is GetType(Single) Then
                Return Convert.ToSingle(value)
            ElseIf conversionType Is GetType(Char) Then
                Return Convert.ToChar(value)
            Else
                Return Convert.ChangeType(value, conversionType)
            End If
        End Function

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
#End Region
    End Module

    Public Interface IJsonType
        ReadOnly Property Item(ByVal key As String) As IJsonType
        ReadOnly Property Item(ByVal index As Integer) As IJsonType
        ReadOnly Property Value As Object
    End Interface


    Public Class JsonArray
        Inherits List(Of Object)
        Implements IJsonType
        Public ReadOnly Property Item(ByVal key As String) As IJsonType Implements IJsonType.Item
            Get
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property Item(ByVal index As Integer) As IJsonType Implements IJsonType.Item
            Get
                Dim obj = MyBase.Item(index)
                If TypeOf obj Is JsonObject OrElse TypeOf obj Is JsonArray OrElse TypeOf obj Is JsonValue Then
                    Return TryCast(obj, IJsonType)
                Else
                    Return New JsonValue(obj)
                End If
            End Get
        End Property

        Public ReadOnly Property Value As Object Implements IJsonType.Value
            Get
                Return Me
            End Get
        End Property

        Public ReadOnly Property Count As Integer
            Get
                Return MyBase.Count
            End Get
        End Property
    End Class

    Public Class JsonObject
        Inherits Dictionary(Of String, Object)
        Implements IJsonType
        Private ReadOnly Property Item(ByVal key As String) As IJsonType Implements IJsonType.Item
            Get
                Dim obj = MyBase.Item(key)
                If TypeOf obj Is JsonObject OrElse TypeOf obj Is JsonArray OrElse TypeOf obj Is JsonValue Then
                    Return TryCast(obj, IJsonType)
                Else
                    Return New JsonValue(obj)
                End If
            End Get
        End Property

        Public ReadOnly Property Item(ByVal index As Integer) As IJsonType Implements IJsonType.Item
            Get
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property Value As Object Implements IJsonType.Value
            Get
                Return Me
            End Get
        End Property
    End Class

    Public Class JsonValue
        Implements IJsonType
        Private _value As Object

        Public Sub New(ByVal value As Object)
            _value = value
        End Sub

        Public ReadOnly Property Value As Object Implements IJsonType.Value
            Get
                Return _value
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal key As String) As IJsonType Implements IJsonType.Item
            Get
                Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As IJsonType Implements IJsonType.Item
            Get
                Return Nothing
            End Get
        End Property
    End Class

    Public Enum Required
        [Default]
        AllowNull
        Always
        DisallowNull
    End Enum

    Public Enum NullValueHandling
        Include
        Ignore
    End Enum

    ''' <summary>
    ''' Maps a JSON property to a .NET member or constructor parameter.
    ''' </summary>
    Public Class JsonProperty
        Inherits Attribute
        Public Sub New(ByVal name As String)
        End Sub

        Public Property Required As Required
        Public Property NullValueHandling As NullValueHandling

        Public Shared Function AreNullableStructsEqual(ByVal struct1 As Object, ByVal struct2 As Object) As Boolean
            If struct1 IsNot Nothing Then
                Return struct1.Equals(struct2)
            Else
                Return struct2 Is Nothing
            End If
        End Function
    End Class
End Namespace
