// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISingleTargetWithFiltersSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ISingleTargetWithFiltersSelectorUtils
  {
    public static ISingleTargetWithFiltersSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ISingleTargetWithFiltersSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ISingleTargetWithFiltersSelector");
        return (ISingleTargetWithFiltersSelector) null;
      }
      string str = jtoken.Value<string>();
      ISingleTargetWithFiltersSelector withFiltersSelector;
      switch (str)
      {
        case "SingleCoordWithConditionSelector":
          withFiltersSelector = (ISingleTargetWithFiltersSelector) new SingleCoordWithConditionSelector();
          break;
        case "SingleEntityWithConditionSelector":
          withFiltersSelector = (ISingleTargetWithFiltersSelector) new SingleEntityWithConditionSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ISingleTargetWithFiltersSelector) null;
      }
      withFiltersSelector.PopulateFromJson(jsonObject);
      return withFiltersSelector;
    }

    public static ISingleTargetWithFiltersSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ISingleTargetWithFiltersSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ISingleTargetWithFiltersSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
