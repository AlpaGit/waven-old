// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISingleCaracIdSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ISingleCaracIdSelectorUtils
  {
    public static ISingleCaracIdSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ISingleCaracIdSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ISingleCaracIdSelector");
        return (ISingleCaracIdSelector) null;
      }
      string str = jtoken.Value<string>();
      if (str == "CaracIdDirectSelector")
      {
        ISingleCaracIdSelector singleCaracIdSelector = (ISingleCaracIdSelector) new CaracIdDirectSelector();
        singleCaracIdSelector.PopulateFromJson(jsonObject);
        return singleCaracIdSelector;
      }
      Debug.LogWarning((object) ("Unknown type: " + str));
      return (ISingleCaracIdSelector) null;
    }

    public static ISingleCaracIdSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ISingleCaracIdSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ISingleCaracIdSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
