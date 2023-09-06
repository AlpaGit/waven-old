// Decompiled with JetBrains decompiler
// Type: DataEditor.Serialization
// Assembly: DataEditorRuntime, Version=1.0.6990.32389, Culture=neutral, PublicKeyToken=null
// MVID: 45C45C6B-0733-4518-B038-C58DEC652313
// Assembly location: E:\WAVEN\Waven_Data\Managed\DataEditorRuntime.dll

using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DataEditor
{
  public static class Serialization
  {
    [PublicAPI]
    public static T JsonTokenValue<T>(JObject jsonObject, string propertyName, T defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : jproperty.Value.Value<T>();
    }

    [PublicAPI]
    public static Vector2Int JsonTokenUnityValue(
      JObject jsonObject,
      string propertyName,
      Vector2Int defaultValue)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      if (jproperty == null || jproperty.Value.Type == JTokenType.Null)
        return defaultValue;
      JObject jobject = jproperty.Value as JObject;
      return new Vector2Int(jobject.Value<int>((object) "x"), jobject.Value<int>((object) "y"));
    }

    [PublicAPI]
    public static Color JsonTokenUnityValue(
      JObject jsonObject,
      string propertyName,
      Color defaultValue)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      if (jproperty == null || jproperty.Value.Type == JTokenType.Null)
        return defaultValue;
      JObject jobject = jproperty.Value as JObject;
      return new Color(jobject.Value<float>((object) "r"), jobject.Value<float>((object) "g"), jobject.Value<float>((object) "b"), jobject.Value<float>((object) "a"));
    }

    [PublicAPI]
    public static Id<T> JsonTokenIdValue<T>(JObject jsonObject, string propertyName) where T : EditableData
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null ? (Id<T>) null : Serialization.JsonTokenIdValue<T>(jproperty.Value);
    }

    [PublicAPI]
    public static Id<T> JsonTokenIdValue<T>(JToken token) where T : EditableData => token.Type == JTokenType.Null ? (Id<T>) null : new Id<T>(token.Value<int>());

    [PublicAPI]
    public static JArray JsonArray(JObject jsonObject, string propertyName)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      if (jproperty == null || jproperty.Value.Type == JTokenType.Null)
        return (JArray) null;
      if (jproperty.Value.Type == JTokenType.Array)
        return jproperty.Value.Value<JArray>();
      Debug.LogWarning((object) ("Malformed json: expected Array, but " + (object) jproperty.Value.Type + "found for property " + propertyName));
      return (JArray) null;
    }

    [PublicAPI]
    public static List<T> JsonArrayAsList<T>(JObject jsonObject, string propertyName) => Serialization.JsonArrayAsList<T>(jsonObject, propertyName, new List<T>());

    [PublicAPI]
    public static List<T> JsonArrayAsList<T>(
      JObject jsonObject,
      string propertyName,
      List<T> defaultValue)
    {
      JArray jarray = Serialization.JsonArray(jsonObject, propertyName);
      return jarray == null ? defaultValue : jarray.ToObject<List<T>>();
    }
  }
}
