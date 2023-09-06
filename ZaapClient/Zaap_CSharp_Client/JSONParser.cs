// Decompiled with JetBrains decompiler
// Type: Zaap_CSharp_Client.JSONParser
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Zaap_CSharp_Client
{
  public static class JSONParser
  {
    private static Stack<List<string>> splitArrayPool = new Stack<List<string>>();
    private static StringBuilder stringBuilder = new StringBuilder();
    private static readonly Dictionary<Type, Dictionary<string, FieldInfo>> fieldInfoCache = new Dictionary<Type, Dictionary<string, FieldInfo>>();
    private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> propertyInfoCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

    public static T FromJson<T>(this string json)
    {
      JSONParser.stringBuilder.Length = 0;
      for (int index = 0; index < json.Length; ++index)
      {
        char c = json[index];
        if (c == '"')
          index = JSONParser.AppendUntilStringEnd(true, index, json);
        else if (!char.IsWhiteSpace(c))
          JSONParser.stringBuilder.Append(c);
      }
      return (T) JSONParser.ParseValue(typeof (T), JSONParser.stringBuilder.ToString());
    }

    private static int AppendUntilStringEnd(bool appendEscapeCharacter, int startIdx, string json)
    {
      JSONParser.stringBuilder.Append(json[startIdx]);
      for (int index = startIdx + 1; index < json.Length; ++index)
      {
        if (json[index] == '\\')
        {
          if (appendEscapeCharacter)
            JSONParser.stringBuilder.Append(json[index]);
          JSONParser.stringBuilder.Append(json[index + 1]);
          ++index;
        }
        else
        {
          if (json[index] == '"')
          {
            JSONParser.stringBuilder.Append(json[index]);
            return index;
          }
          JSONParser.stringBuilder.Append(json[index]);
        }
      }
      return json.Length - 1;
    }

    private static List<string> Split(string json)
    {
      List<string> stringList = JSONParser.splitArrayPool.Count <= 0 ? new List<string>() : JSONParser.splitArrayPool.Pop();
      stringList.Clear();
      int num = 0;
      JSONParser.stringBuilder.Length = 0;
      for (int index = 1; index < json.Length - 1; ++index)
      {
        char ch = json[index];
        switch (ch)
        {
          case '[':
label_5:
            ++num;
            break;
          case ']':
label_6:
            --num;
            break;
          default:
            switch (ch)
            {
              case '{':
                goto label_5;
              case '}':
                goto label_6;
              default:
                if (ch != '"')
                {
                  if ((ch == ',' || ch == ':') && num == 0)
                  {
                    stringList.Add(JSONParser.stringBuilder.ToString());
                    JSONParser.stringBuilder.Length = 0;
                    continue;
                  }
                  break;
                }
                index = JSONParser.AppendUntilStringEnd(true, index, json);
                continue;
            }
            break;
        }
        JSONParser.stringBuilder.Append(json[index]);
      }
      stringList.Add(JSONParser.stringBuilder.ToString());
      return stringList;
    }

    internal static object ParseValue(Type type, string json)
    {
      if (type == typeof (string))
        return json.Length <= 2 ? (object) string.Empty : (object) json.Substring(1, json.Length - 2).Replace("\\\\", "\"\"").Replace("\\", string.Empty).Replace("\"\"", "\\");
      if (type == typeof (int))
      {
        int result;
        int.TryParse(json, out result);
        return (object) result;
      }
      if (type == typeof (float))
      {
        float result;
        float.TryParse(json, out result);
        return (object) result;
      }
      if (type == typeof (double))
      {
        double result;
        double.TryParse(json, out result);
        return (object) result;
      }
      if (type == typeof (bool))
        return (object) (json.ToLower() == "true");
      if (json == "null")
        return (object) null;
      if (type.IsArray)
      {
        Type elementType = type.GetElementType();
        if (json[0] != '[' || json[json.Length - 1] != ']')
          return (object) null;
        List<string> stringList = JSONParser.Split(json);
        Array instance = Array.CreateInstance(elementType, stringList.Count);
        for (int index = 0; index < stringList.Count; ++index)
          instance.SetValue(JSONParser.ParseValue(elementType, stringList[index]), index);
        JSONParser.splitArrayPool.Push(stringList);
        return (object) instance;
      }
      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (List<>))
      {
        Type genericArgument = type.GetGenericArguments()[0];
        if (json[0] != '[' || json[json.Length - 1] != ']')
          return (object) null;
        List<string> stringList = JSONParser.Split(json);
        IList list = (IList) type.GetConstructor(new Type[1]
        {
          typeof (int)
        }).Invoke(new object[1]{ (object) stringList.Count });
        for (int index = 0; index < stringList.Count; ++index)
          list.Add(JSONParser.ParseValue(genericArgument, stringList[index]));
        JSONParser.splitArrayPool.Push(stringList);
        return (object) list;
      }
      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Dictionary<,>))
      {
        Type[] genericArguments = type.GetGenericArguments();
        Type type1 = genericArguments[0];
        Type type2 = genericArguments[1];
        if (type1 != typeof (string))
          return (object) null;
        if (json[0] != '{' || json[json.Length - 1] != '}')
          return (object) null;
        List<string> stringList = JSONParser.Split(json);
        if (stringList.Count % 2 != 0)
          return (object) null;
        IDictionary dictionary = (IDictionary) type.GetConstructor(new Type[1]
        {
          typeof (int)
        }).Invoke(new object[1]
        {
          (object) (stringList.Count / 2)
        });
        for (int index = 0; index < stringList.Count; index += 2)
        {
          if (stringList[index].Length > 2)
          {
            string key = stringList[index].Substring(1, stringList[index].Length - 2);
            object obj = JSONParser.ParseValue(type2, stringList[index + 1]);
            dictionary.Add((object) key, obj);
          }
        }
        return (object) dictionary;
      }
      if (type == typeof (object))
        return JSONParser.ParseAnonymousValue(json);
      return json[0] == '{' && json[json.Length - 1] == '}' ? JSONParser.ParseObject(type, json) : (object) null;
    }

    private static object ParseAnonymousValue(string json)
    {
      if (json.Length == 0)
        return (object) null;
      if (json[0] == '{' && json[json.Length - 1] == '}')
      {
        List<string> stringList = JSONParser.Split(json);
        if (stringList.Count % 2 != 0)
          return (object) null;
        Dictionary<string, object> anonymousValue = new Dictionary<string, object>(stringList.Count / 2);
        for (int index = 0; index < stringList.Count; index += 2)
          anonymousValue.Add(stringList[index].Substring(1, stringList[index].Length - 2), JSONParser.ParseAnonymousValue(stringList[index + 1]));
        return (object) anonymousValue;
      }
      if (json[0] == '[' && json[json.Length - 1] == ']')
      {
        List<string> stringList = JSONParser.Split(json);
        List<object> anonymousValue = new List<object>(stringList.Count);
        for (int index = 0; index < stringList.Count; ++index)
          anonymousValue.Add(JSONParser.ParseAnonymousValue(stringList[index]));
        return (object) anonymousValue;
      }
      if (json[0] == '"' && json[json.Length - 1] == '"')
        return (object) json.Substring(1, json.Length - 2).Replace("\\", string.Empty);
      if (char.IsDigit(json[0]) || json[0] == '-')
      {
        if (json.Contains("."))
        {
          double result;
          double.TryParse(json, out result);
          return (object) result;
        }
        int result1;
        int.TryParse(json, out result1);
        return (object) result1;
      }
      switch (json)
      {
        case "true":
          return (object) true;
        case "false":
          return (object) false;
        default:
          return (object) null;
      }
    }

    private static object ParseObject(Type type, string json)
    {
      object uninitializedObject = FormatterServices.GetUninitializedObject(type);
      List<string> stringList = JSONParser.Split(json);
      if (stringList.Count % 2 != 0)
        return uninitializedObject;
      Dictionary<string, FieldInfo> dictionary1;
      if (!JSONParser.fieldInfoCache.TryGetValue(type, out dictionary1))
      {
        dictionary1 = ((IEnumerable<FieldInfo>) type.GetFields()).Where<FieldInfo>((Func<FieldInfo, bool>) (field => field.IsPublic)).ToDictionary<FieldInfo, string>((Func<FieldInfo, string>) (field => field.Name));
        JSONParser.fieldInfoCache.Add(type, dictionary1);
      }
      Dictionary<string, PropertyInfo> dictionary2;
      if (!JSONParser.propertyInfoCache.TryGetValue(type, out dictionary2))
      {
        dictionary2 = ((IEnumerable<PropertyInfo>) type.GetProperties()).ToDictionary<PropertyInfo, string>((Func<PropertyInfo, string>) (p => p.Name));
        JSONParser.propertyInfoCache.Add(type, dictionary2);
      }
      for (int index = 0; index < stringList.Count; index += 2)
      {
        if (stringList[index].Length > 2)
        {
          string key = stringList[index].Substring(1, stringList[index].Length - 2);
          string json1 = stringList[index + 1];
          FieldInfo fieldInfo;
          if (dictionary1.TryGetValue(key, out fieldInfo))
          {
            fieldInfo.SetValue(uninitializedObject, JSONParser.ParseValue(fieldInfo.FieldType, json1));
          }
          else
          {
            PropertyInfo propertyInfo;
            if (dictionary2.TryGetValue(key, out propertyInfo))
              propertyInfo.SetValue(uninitializedObject, JSONParser.ParseValue(propertyInfo.PropertyType, json1), (object[]) null);
          }
        }
      }
      return uninitializedObject;
    }
  }
}
