﻿#region License
//   Copyright 2010 John Sheehan
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

using RestSharp.Extensions;
using System.Globalization;
using RestSharp;
using RestSharp.Deserializers;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ECollegeAPI.Util
{
    public class CustomJsonDeserializer : IDeserializer
    {
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public bool UseISOUniversalTime { get; set; }

        protected string PrettyPrint(string json)
        {
            object jsonObject = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
        
        public T Deserialize<T>(RestResponse response) where T : new()
        {
            return Deserialize<T>(response.Content);
        }

        public T Deserialize<T>(string responseContent) where T : new()
        {
            var target = new T();

            try
            {
                if (target is IList)
                {
                    var objType = target.GetType();

                    if (RootElement.HasValue())
                    {
                        var root = FindRoot(responseContent);
                        target = (T)BuildList(objType, root.Children());
                    }
                    else
                    {
                        JArray json = JArray.Parse(responseContent);
                        target = (T)BuildList(objType, json.Root.Children());
                    }
                }
                else
                {
                    var root = FindRoot(responseContent);

                    Map(target, root);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                Debugger.Break();
            }

            return target;
        }

        private JToken FindRoot(string content)
        {
            JObject json = JObject.Parse(content);
            JToken root = json.Root;

            if (RootElement.HasValue())
                root = json.SelectToken(RootElement);

            return root;
        }

        private void Map(object x, JToken json)
        {
            var objType = x.GetType();
            var props = objType.GetProperties().Where(p => p.CanWrite).ToList();

            foreach (var prop in props)
            {
                var type = prop.PropertyType;

                var name = prop.Name;
                var value = json[name];
                var actualName = name;

                if (value == null)
                {
                    // try camel cased name
                    actualName = name.ToCamelCase(CultureInfo.InvariantCulture);
                    value = json[actualName];
                }

                if (value == null)
                {
                    // try lower cased name
                    actualName = name.ToLower();
                    value = json[actualName];
                }

                if (value == null)
                {
                    // try name with underscores
                    actualName = name.AddUnderscores();
                    value = json[actualName];
                }

                if (value == null)
                {
                    // try name with underscores with lower case
                    actualName = name.AddUnderscores().ToLower();
                    value = json[actualName];
                }

                if (value == null)
                {
                    // try name with dashes
                    actualName = name.AddDashes();
                    value = json[actualName];
                }

                if (value == null)
                {
                    // try name with dashes with lower case
                    actualName = name.AddDashes().ToLower();
                    value = json[actualName];
                }

                if (value == null || value.Type == JTokenType.Null)
                {
                    continue;
                }

                // check for nullable and extract underlying type
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = type.GetGenericArguments()[0];
                }

                if (type.IsPrimitive)
                {
                    // no primitives can contain quotes so we can safely remove them
                    // allows converting a json value like {"index": "1"} to an int
                    var tmpVal = value.ToString().Replace("\"", string.Empty);
                    prop.SetValue(x, tmpVal.ChangeType(type), null);
                }
                else if (type.IsEnum)
                {
                    string raw = value.AsString();
                    var converted = Enum.Parse(type, raw, false);
                    prop.SetValue(x, converted, null);
                }
                else if (type == typeof(Uri))
                {
                    string raw = value.AsString();
                    var uri = new Uri(raw, UriKind.RelativeOrAbsolute);
                    prop.SetValue(x, uri, null);
                }
                else if (type == typeof(string))
                { 
                    //string raw = value is string ? value.AsString() : value.ToString();
                    string raw;
                    if (value.Type == JTokenType.String)
                    {
                        raw = value.AsString();
                    }
                    else
                    {
                        raw = value.ToString().Replace("\"", string.Empty);
                    }
                    prop.SetValue(x, raw, null);
                }
                else if (type == typeof(DateTime))
                {
                    DateTime dt;

                    var clean = value.ToString().RemoveSurroundingQuotes();

                    if (UseISOUniversalTime && clean.EndsWith("Z"))//parses "2010-10-12T18:03:18Z"
                    {
                        dt = DateTime.ParseExact(clean, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture,DateTimeStyles.AssumeUniversal);
                    } else if (DateFormat.HasValue())
                    {
                        dt = DateTime.ParseExact(clean, DateFormat, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        // try parsing instead
                        dt = value.ToString().ParseJsonDate(CultureInfo.InvariantCulture);
                    }

                    prop.SetValue(x, dt, null);
                }
                else if (type == typeof(Decimal))
                {
                    var dec = Decimal.Parse(value.ToString());
                    prop.SetValue(x, dec, null);
                }
                else if (type == typeof(Guid))
                {
                    string raw = value.ToString();
                    raw = raw.Substring(1, raw.Length - 2);
                    var guid = string.IsNullOrEmpty(raw) ? Guid.Empty : new Guid(raw);
                    prop.SetValue(x, guid, null);
                }
                else if (type.IsGenericType)
                {
                    var genericTypeDef = type.GetGenericTypeDefinition();
                    if (genericTypeDef == typeof(List<>))
                    {
                        var list = BuildList(type, value.Children());
                        prop.SetValue(x, list, null);
                    }
                    else if (genericTypeDef == typeof(Dictionary<,>))
                    {
                        var keyType = type.GetGenericArguments()[0];

                        // only supports Dict<string, T>()
                        if (keyType == typeof(string))
                        {
                            var dict = BuildDictionary(type, value.Children());
                            prop.SetValue(x, dict, null);
                        }
                    }
                }
                else
                {
                    // nested property classes
                    var item = CreateAndMap(type, json[actualName]);
                    prop.SetValue(x, item, null);
                }
            }
        }

        private object CreateAndMap(Type type, JToken element)
        {
            object instance = null;

            try
            {

                if (type.IsGenericType)
                {
                    var genericTypeDef = type.GetGenericTypeDefinition();
                    if (genericTypeDef == typeof(Dictionary<,>))
                    {
                        instance = BuildDictionary(type, element.Children());
                    }
                    else if (genericTypeDef == typeof(List<>))
                    {
                        instance = BuildList(type, element.Children());
                    }
                }
                else
                {
                    instance = Activator.CreateInstance(type);
                    Map(instance, element);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                Debugger.Break();
            }

            return instance;
        }

        private IDictionary BuildDictionary(Type type, JEnumerable<JToken> elements)
        {
            var dict = (IDictionary)Activator.CreateInstance(type);
            var valueType = type.GetGenericArguments()[1];
            foreach (JProperty child in elements)
            {
                var key = child.Name;
                var item = CreateAndMap(valueType, child.Value);
                dict.Add(key, item);
            }

            return dict;
        }

        private IList BuildList(Type type, JEnumerable<JToken> elements)
        {
            var list = (IList)Activator.CreateInstance(type);
            var itemType = type.GetGenericArguments()[0];

            foreach (var element in elements)
            {
                if (itemType.IsPrimitive)
                {
                    var value = element as JValue;
                    if (value != null)
                    {
                        list.Add(value.Value.ChangeType(itemType));
                    }
                }
                else if (itemType == typeof(string))
                {
                    list.Add(element.AsString());
                }
                else
                {
                    var item = CreateAndMap(itemType, element);
                    list.Add(item);
                }
            }
            return list;
        }
    }
}