using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;


//------------------------------------
// This extension adds JSON serialization/deserialization support to C#/XAML for HTML5 (www.cshtml5.com)
//
// It requires v1.0 Beta 8.2 or newer.
//
// This extension is licensed under the open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2017 Userware / CSHTML5
//------------------------------------


namespace CSHTML5.Extensions.Json
{
    public static class JsonConvert
    {
        #region Public Methods

        public static string SerializeObject(object objectToSerialize, bool ignoreErrors = false)
        {
            var javaScriptObject = ConvertCSharpObjectToJavaScriptObject(objectToSerialize, ignoreErrors);
            string serializedObject = Interop.ExecuteJavaScript(@"JSON.stringify($0)", javaScriptObject).ToString();
            return serializedObject;
        }

        public static async Task<T> DeserializeObject<T>(string json, bool ignoreErrors = false)
            where T : class, new()
        {
            var javaScriptObject = Interop.ExecuteJavaScript("JSON.parse($0)", json);
            var cSharpNestedDictionariesAndLists = await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(javaScriptObject, ignoreErrors);
            T cSharpObject = (T)ConvertCSharpNestedDictionariesAndListsToCSharpObject(typeof(T), cSharpNestedDictionariesAndLists, ignoreErrors);
            return cSharpObject;
        }

        public static async Task<IJsonType> DeserializeObject(string json, bool ignoreErrors = false)
        {
            var javaScriptObject = Interop.ExecuteJavaScript("JSON.parse($0)", json);
            IJsonType cSharpNestedDictionariesAndLists = await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(javaScriptObject, ignoreErrors);
            return cSharpNestedDictionariesAndLists;
        }

        #endregion

        #region Private Methods
        static object ConvertCSharpObjectToJavaScriptObject(object cSharpObject, bool ignoreErrors)
        {
            if (cSharpObject is Enum || cSharpObject is Guid)
            {
                return cSharpObject.ToString();
            }
            else if (cSharpObject is DateTime)
            {
                //Uncomment when fully supported by CSHTML5:
                //return ((DateTime)cSharpObject).ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);

                var dateTimeUtc = ((DateTime)cSharpObject).ToUniversalTime();
                TimeSpan timeSince1970 = (dateTimeUtc - new DateTime(1970, 1, 1, 0, 0, 0));
                double millisecondsSince1970 = timeSince1970.TotalMilliseconds;
                var jsDate = Interop.ExecuteJavaScript("new Date($0)", millisecondsSince1970);
                string json = Convert.ToString(Interop.ExecuteJavaScript("$0.toJSON()", jsDate));
                return json;
            }
            else if (cSharpObject is string || (cSharpObject != null && cSharpObject.GetType().IsValueType))
            {
                return cSharpObject;
            }
            else if (cSharpObject is IEnumerable && !(cSharpObject is string))
            {
                //----------------
                // ARRAY
                //----------------

                // Create the JS array:
                var jsArray = Interop.ExecuteJavaScript("[]");

                // Traverse the enumerable:
                foreach (var cSharpItem in (IEnumerable)cSharpObject)
                {
                    var jsItem = ConvertCSharpObjectToJavaScriptObject(cSharpItem, ignoreErrors);
                    Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsItem);
                }

                return jsArray;
            }
            else if (cSharpObject != null)
            {
                //----------------
                // OBJECT
                //----------------

                var jsObject = Interop.ExecuteJavaScript(@"new Object()");

                // Traverse all properties:
                foreach (PropertyInfo property in cSharpObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(cSharpObject);

                    if (propertyValue != null)
                    {
                        var recursionResult = ConvertCSharpObjectToJavaScriptObject(propertyValue, ignoreErrors);
                        if (recursionResult != null)
                        {
                            Interop.ExecuteJavaScript(@"$0[$1] = $2;", jsObject, propertyName, recursionResult);
                        }
                    }
                }

                return jsObject;
            }
            else
                return Interop.ExecuteJavaScript("undefined");
        }

        static object ConvertCSharpNestedDictionariesAndListsToCSharpObject(Type resultType, object cSharpNestedDictionariesAndLists, bool ignoreErrors)
        {
            object result;

            if (cSharpNestedDictionariesAndLists == null)
            {
                return null;
            }
            else if (cSharpNestedDictionariesAndLists is JsonValue)
            {
                object value = ((JsonValue)cSharpNestedDictionariesAndLists).Value;
                if (value == null)
                {
                    return null;
                }
                else if (value is string)
                {
                    //--------
                    // Enum, Guid, DateTime, String...
                    //--------

                    // If the target type is an Enum, convert from string to the Enum:
                    if (resultType != null && resultType.IsEnum)
                    {
                        try
                        {
                            var enumValue = Enum.Parse(resultType, (string)value);
                            return enumValue;
                        }
                        catch
                        {
                            throw new InvalidOperationException(string.Format("'{0}' is not a valid value for the enum type '{1}'.", (string)value, resultType.ToString()));
                        }
                    }
                    // If the target type is a Guid, convert from string to the Guid:
                    else if (resultType == typeof(Guid) || resultType == typeof(Guid?))
                    {
                        try
                        {
                            var guid = Guid.Parse((string)value);
                            return guid;
                        }
                        catch
                        {
                            throw new InvalidOperationException(string.Format("'{0}' is not a valid value for a Guid.", (string)value));
                        }
                    }
                    // If the target type is a DateTime, convert from string to the DateTime:
                    else if (resultType == typeof(DateTime) || resultType == typeof(DateTime?))
                    {
                        try
                        {
                            //Uncomment when fully supported by CSHTML5:
                            //var dateTime = DateTime.Parse((string)value);
                            //return dateTime;

                            string dateString = (string)value;
                            var jsDate = Interop.ExecuteJavaScript("new Date($0)", dateString);

                            int year = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getFullYear()", jsDate));
                            int month = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getMonth()", jsDate)) + 1; // Note: month index in JS starts at "0".
                            int day = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getDate()", jsDate));
                            int hour = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getHours()", jsDate));
                            int minute = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getMinutes()", jsDate));
                            int second = Convert.ToInt32(Interop.ExecuteJavaScript("$0.getSeconds()", jsDate));

                            DateTime dateTime = new DateTime(year, month, day, hour, minute, second);

                            return dateTime;
                        }
                        catch
                        {
                            throw new InvalidOperationException(string.Format("'{0}' is not a valid value for a DateTime.", (string)value));
                        }
                    }
                    else if (resultType == typeof(string))
                    {
                        // If the target type is a string, no conversion is needed:
                        return value;
                    }
                    // If the target is another value type, it means that we are converting for example a string into a number or a bool (this is not supposed to happen if the JSON string is perfectly formatted, but it may happen if the JSON string contains numbers inside quotes, or booleans inside quotes):
                    else if (resultType.IsValueType)
                    {
                        // If the result type is Nullable, we need to get the underlying type in order to be able to properly call the "ChangeType" method below (useful for example when converting from "double" to nullable "int?"):
                        var nullableUnderlyingType = Nullable.GetUnderlyingType(resultType);
                        if (nullableUnderlyingType != null)
                        {
                            resultType = nullableUnderlyingType;
                        }

                        // We need to convert the value to the appropriate type because JSON does not contain type information:
                        var convertedValue = ChangeType(value, resultType);
                        return convertedValue;
                    }
                    else
                    {
                        if (!ignoreErrors)
                            throw new InvalidOperationException(string.Format("Cannot convert a string to '{0}'.", (resultType != null ? resultType.ToString() : "null")));
                        else
                            return null;
                    }
                }
                else if (value.GetType().IsValueType)
                {
                    //--------
                    // Other value types
                    //--------

                    // Verify that the target type is also a value type:
                    if (resultType != null && resultType.IsValueType)
                    {
                        // If the result type is Nullable, we need to get the underlying type in order to be able to properly call the "ChangeType" method below (useful for example when converting from "double" to nullable "int?"):
                        var nullableUnderlyingType = Nullable.GetUnderlyingType(resultType);
                        if (nullableUnderlyingType != null)
                        {
                            resultType = nullableUnderlyingType;
                        }

                        // We need to convert the value to the appropriate type because JSON does not contain type information. For example, it could be an "double" that we need to set as an "int".
                        var convertedValue = Convert.ChangeType(value, resultType);
                        return convertedValue;
                    }
                    else
                    {
                        if (!ignoreErrors)
                            throw new InvalidOperationException(string.Format("Cannot convert a value type to '{0}'.", (resultType != null ? resultType.ToString() : "null")));
                        else
                            return Activator.CreateInstance(resultType);
                    }
                }
                else
                {
                    if (!ignoreErrors)
                        throw new Exception("Unexpected type: " + value.GetType().ToString());
                    else
                        return null;
                }
            }
            else if (cSharpNestedDictionariesAndLists is JsonArray)
            {
                Type[] genericArguments;
                if (resultType.IsArray)
                {
                    //--------
                    // Array
                    //--------

                    // Get the type of the array items:
                    Type itemsType = resultType.GetElementType();

                    // Create a new ArrayList that we will then convert to an array:
                    var arrayList = new ArrayList();

                    // Add the items to the ArrayList:
                    foreach (var item in (IEnumerable)cSharpNestedDictionariesAndLists)
                    {
                        //********** RECURSION **********
                        var recursionResult = ConvertCSharpNestedDictionariesAndListsToCSharpObject(itemsType, item, ignoreErrors);
                        arrayList.Add(recursionResult);
                    }

                    // Convert the ArrayList to the expected array type:
                    result = arrayList.ToArray(itemsType);
                }
                else if (resultType.IsGenericType
                    && (genericArguments = resultType.GetGenericArguments()).Length > 0
                    && IsAssignableToGenericEnumerable(resultType, genericArguments[0]))
                {
                    //--------
                    // IEnumerable<T>
                    //--------

                    // Get the type of the IEnumerable items (this works for example with List<T>, ObservableCollection<T>, etc.):
                    Type itemsType = genericArguments[0];

                    // Create a temporary List<T> in order to add items to it. Later we will convert it to the final type if needed.
                    var list = typeof(JsonConvert)
                        .GetMethod("CreateNewInstanceOfGenericList", BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(itemsType)
                        .Invoke(null, new object[] { });

                    // Note: in the code above, we call the method "CreateNewInstanceOfGenericList" instead of
                    // calling "var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(itemsType))"
                    // because the latter does not appear to properly initialize the underlying JavaScript "_items"
                    // collection that is inside the List<T> implementation in JSIL as of July 14, 2017, thus
                    // resulting in an exception when calling the Add method (reference: CSHTML5 tickets #623 and
                    // #648).

                    // Get a reference to the "Add" method of the generic list that we just created:
                    var listAddMethod = list.GetType().GetMethod("Add");

                    // Add the items to the list:
                    foreach (var item in (IEnumerable)cSharpNestedDictionariesAndLists)
                    {
                        //********** RECURSION **********
                        var recursionResult = ConvertCSharpNestedDictionariesAndListsToCSharpObject(itemsType, item, ignoreErrors);
                        listAddMethod.Invoke(list, new object[] { recursionResult });
                    }

                    // If the result type is List<> or a compatible interface, we directly return the List<> that we created above, otherwise we convert it to the result type:
                    Type genericTypeDefinition = resultType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(List<>)
                        || genericTypeDefinition == typeof(IList<>)
                        || genericTypeDefinition == typeof(IEnumerable<>)
                        || genericTypeDefinition == typeof(ICollection<>)
                        || genericTypeDefinition == typeof(IReadOnlyCollection<>)
                        || genericTypeDefinition == typeof(IReadOnlyList<>))
                    {
                        // Return the List<T>:
                        result = list;
                    }
                    else
                    {
                        // Otherwise, attempt to create a new instance of the result type while passing the items as first argument of the constructor (this works for example with ObservableCollection<T> and other common collections):
                        try
                        {
                            result = Activator.CreateInstance(resultType, args: new object[] { list });
                        }
                        catch
                        {
                            if (!ignoreErrors)
                                throw new InvalidOperationException("Type not supported by JsonConvert: " + (resultType != null ? resultType.ToString() : "null"));
                            else
                                return null;
                        }
                    }
                }
                else
                {
                    if (!ignoreErrors)
                        throw new InvalidOperationException("Type not supported by JsonConvert: " + (resultType != null ? resultType.ToString() : "null"));
                    else
                        return null;
                }
            }
            else if (cSharpNestedDictionariesAndLists is JsonObject)
            {
                //--------
                // Object with properties
                //--------

                // Create a new instance of the resulting type:
                try
                {
                    result = Activator.CreateInstance(resultType);
                }
                catch (Exception ex)
                {
                    if (!ignoreErrors)
                        throw new InvalidOperationException(string.Format("Unable to create an instance of type '{0}'. A common cause is that the type does not have a default public constructor.", (resultType != null ? resultType.ToString() : "null")), ex);
                    else
                        return null;
                }

                // Make a dictionary of all the destination object properties, for fast lookup:
                var propertyNameToTargetProperty = new Dictionary<string, PropertyInfo>();
                var propertyNameToTargetProperty_Lowercase = new Dictionary<string, PropertyInfo>();
                foreach (PropertyInfo property in resultType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    propertyNameToTargetProperty.Add(property.Name, property);
                    propertyNameToTargetProperty_Lowercase[property.Name.ToLower()] = property; // This line adds or updates if the key already exists (in case of multiple properties that have the same Lowercase representation, we keep only the last one).
                }

                // Traverse each property:
                foreach (var keyValuePair in (JsonObject)cSharpNestedDictionariesAndLists)
                {
                    string propertyName = keyValuePair.Key;
                    object propertyValue = keyValuePair.Value;

                    PropertyInfo targetProperty = null;

                    // First, check case-sensitively:
                    if (propertyNameToTargetProperty.ContainsKey(propertyName))
                    {
                        targetProperty = propertyNameToTargetProperty[propertyName];
                    }
                    // Alternatively, check case-insensitively:
                    else if (propertyNameToTargetProperty_Lowercase.ContainsKey(propertyName.ToLower()))
                    {
                        targetProperty = propertyNameToTargetProperty_Lowercase[propertyName.ToLower()];
                    }

                    if (targetProperty != null)
                    {
                        //********** RECURSION **********
                        var recursionResult = ConvertCSharpNestedDictionariesAndListsToCSharpObject(targetProperty.PropertyType, propertyValue, ignoreErrors);
                        if (recursionResult != null)
                        {
                            targetProperty.SetValue(result, recursionResult);
                        }
                    }
                    else
                    {
                        if (!ignoreErrors)
                            throw new Exception(string.Format("Property '{0}' not found in type '{1}'", propertyName, (resultType != null ? resultType.ToString() : "null")));
                    }
                }
            }
            else
            {
                if (!ignoreErrors)
                    throw new Exception("Unexpected type: " + cSharpNestedDictionariesAndLists.GetType().ToString());
                else
                    return null;
            }

            return result;
        }

        static Task<IJsonType> ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(object javaScriptObject, bool ignoreErrors)
        {
            // Note: This method needs to be "async" because it is
            // recursive and, in the Simulator, it is not possible
            // to recursively call "ExecuteJavaScript" (ie. to make
            // nested synchronous calls to JS).

            IJsonType result = null;
            var taskCompletionSource = new TaskCompletionSource<IJsonType>();
            Action onCompleted = () =>
            {
                taskCompletionSource.SetResult(result);
            };

            bool isArray = Convert.ToBoolean(Interop.ExecuteJavaScript("Array.isArray($0)", javaScriptObject));
            bool isObject = Convert.ToBoolean(Interop.ExecuteJavaScript(@"(typeof($0) === ""object"")", javaScriptObject));
            bool isNumber = Convert.ToBoolean(Interop.ExecuteJavaScript(@"(typeof($0) === ""number"")", javaScriptObject));
            bool isBoolean = Convert.ToBoolean(Interop.ExecuteJavaScript(@"(typeof($0) === ""boolean"")", javaScriptObject));
            bool isNullOrUndefined = Convert.ToBoolean(Interop.ExecuteJavaScript(@"($0 === undefined || $0 === null)", javaScriptObject));

            if (isNullOrUndefined)
            {
                result = null;
                onCompleted();
            }
            else if (isArray)
            {
                int arrayItemsCount = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.length", javaScriptObject));
                result = new JsonArray();
                if (arrayItemsCount > 0)
                {
                    int currentIndex = 0;
                    Action<object> methodToAddObject = async (jsObj) =>
                    {
                        ((JsonArray)result).Add(await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(jsObj, ignoreErrors));
                        currentIndex++;
                        if (currentIndex == arrayItemsCount)
                            onCompleted();
                    };

                    // Note: the following JS call must be asynchronous because
                    // this algorithm is recursive and, in the Simulator, it is
                    // not possible to recursively call "ExecuteJavaScript"
                    // (ie. to make nested synchronous calls to JS).

                    Interop.ExecuteJavaScriptAsync(@"
                    var input = $0;
                    var methodToAddObject = $1;
                    for (var i = 0; i < input.length; i++) {
                        methodToAddObject(input[i]);
                    }",
                        javaScriptObject,
                        methodToAddObject);
                }
                else
                {
                    onCompleted();
                }
            }
            else if (isObject)
            {
                int keysCount = Convert.ToInt32(Interop.ExecuteJavaScript(@"Object.keys($0).length", javaScriptObject));
                result = new JsonObject();
                if (keysCount > 0)
                {
                    int currentIndexInKeys = 0;
                    Action<object, object> methodToAddObject = async (key, jsObj) =>
                    {
                        ((Dictionary<string, object>)result)[key.ToString()] = await ConvertJavaScriptObjectToCSharpNestedDictionariesAndLists(jsObj, ignoreErrors);
                        currentIndexInKeys++;
                        if (currentIndexInKeys == keysCount)
                            onCompleted();
                    };

                    // Note: the following JS call must be asynchronous because
                    // this algorithm is recursive and, in the Simulator, it is
                    // not possible to recursively call "ExecuteJavaScript"
                    // (ie. to make nested synchronous calls to JS).

                    Interop.ExecuteJavaScriptAsync(@"
                    var input = $0;
                    var methodToAddObject = $1;
                    for (var key in input) {
                        var str = key.toString();
                        //alert(""key: "" + str);
                        methodToAddObject(str, input[key]);
                    }",
                        javaScriptObject,
                        methodToAddObject);
                }
                else
                {
                    onCompleted();
                }
            }
            else if (isNumber)
            {
                result = new JsonValue(Convert.ToDouble(javaScriptObject));
                onCompleted();
            }
            else if (isBoolean)
            {
                result = new JsonValue(Convert.ToBoolean(javaScriptObject));
                onCompleted();
            }
            else
            {
                result = new JsonValue(javaScriptObject.ToString());
                onCompleted();
            }
            return taskCompletionSource.Task;
        }

        static bool IsAssignableToGenericEnumerable(Type genericType, Type itemsType)
        {
            /*
            var enumerableType = typeof(IEnumerable<>);
            var constructedEnumerableType = enumerableType.MakeGenericType(itemsType);
            return constructedEnumerableType.IsAssignableFrom(genericType);
             */

            // Note: the code above was commented and replaced by the code below because in some
            // cases the call to "IsAssignableFrom" let to an exception due to an issue in the
            // JS implementation in the JSIL libraries (reference: CSHTML5 tickets #623 and #648).

            var ienumerableInterface = GetInterface(genericType, typeof(IEnumerable<>).Name);
            return (ienumerableInterface != null);
        }

        static Type GetInterface(Type type, string name)
        {
            // Note: this method is here because "Type.GetInterface(name)" is not yet
            // available in the JSIL libraries as of July 14, 2017.
            // When available, this method can be replaced with "Type.GetInterface(name)".

            foreach (Type theInterface in type.GetInterfaces())
            {
                if (theInterface.Name == name)
                    return theInterface;
            }
            return null;
        }

        static IList CreateNewInstanceOfGenericList<T>()
        {
            return new List<T>();
        }

        static object ChangeType(object value, Type conversionType)
        {
            // Note: this method is here to work around an issue in the "Convert.ChangeType"
            // implementation in the JSIL libraries as of July 14, 2017, which does not
            // appear to give the appropriate result when converting from a string to
            // an integer or boolean. It can be replaced with "Convert.ChangeType" when
            // fixed.

            if (conversionType == typeof(Boolean))
                return Convert.ToBoolean(value);
            else if (conversionType == typeof(Int16))
                return Convert.ToInt16(value);
            else if (conversionType == typeof(Int32))
                return Convert.ToInt32(value);
            else if (conversionType == typeof(Int64))
                return Convert.ToInt64(value);
            else if (conversionType == typeof(Byte))
                return Convert.ToByte(value);
            else if (conversionType == typeof(Double))
                return Convert.ToDouble(value);
            else if (conversionType == typeof(Single))
                return Convert.ToSingle(value);
            else if (conversionType == typeof(Char))
                return Convert.ToChar(value);
            else
                return Convert.ChangeType(value, conversionType);
        }
        #endregion
    }

    public interface IJsonType
    {
        IJsonType this[string key] { get; }
        IJsonType this[int index] { get; }
        object Value { get; }
    }


    public class JsonArray : List<object>, IJsonType
    {
        public IJsonType this[string key]
        {
            get
            {
                return null;
            }
        }

        public new IJsonType this[int index]
        {
            get
            {
                var obj = base[index];
                if (obj is JsonObject || obj is JsonArray || obj is JsonValue)
                    return obj as IJsonType;
                else
                    return new JsonValue(obj);
            }
        }

        public object Value
        {
            get
            {
                return this;
            }
        }

        public int Count
        {
            get
            {
                return base.Count;
            }
        }
    }

    public class JsonObject : Dictionary<string, object>, IJsonType
    {
        public new IJsonType this[string key]
        {
            get
            {
                var obj = base[key];
                if (obj is JsonObject || obj is JsonArray || obj is JsonValue)
                    return obj as IJsonType;
                else
                    return new JsonValue(obj);
            }
        }

        public IJsonType this[int index]
        {
            get
            {
                return null;
            }
        }

        public object Value
        {
            get
            {
                return this;
            }
        }
    }

    public class JsonValue : IJsonType
    {
        object _value;

        public JsonValue(object value)
        {
            _value = value;
        }

        public object Value
        {
            get
            {
                return _value;
            }
        }

        public IJsonType this[string key]
        {
            get
            {
                return null;
            }
        }

        public IJsonType this[int index]
        {
            get
            {
                return null;
            }
        }
    }
}