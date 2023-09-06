// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ICaracIdSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ICaracIdSelectorUtils
  {
    public static ICaracIdSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ICaracIdSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ICaracIdSelector");
        return (ICaracIdSelector) null;
      }
      string str = jtoken.Value<string>();
      ICaracIdSelector caracIdSelector;
      switch (str)
      {
        case "ElementCaracIdMultipleSelector":
          caracIdSelector = (ICaracIdSelector) new ElementCaracIdMultipleSelector();
          break;
        case "ElementCaracIdSuperlativeSelector":
          caracIdSelector = (ICaracIdSelector) new ElementCaracIdSuperlativeSelector();
          break;
        case "CaracIdDirectSelector":
          caracIdSelector = (ICaracIdSelector) new CaracIdDirectSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ICaracIdSelector) null;
      }
      caracIdSelector.PopulateFromJson(jsonObject);
      return caracIdSelector;
    }

    public static ICaracIdSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ICaracIdSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ICaracIdSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
