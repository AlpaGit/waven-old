// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ICastTargetDefinitionUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ICastTargetDefinitionUtils
  {
    public static ICastTargetDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ICastTargetDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ICastTargetDefinition");
        return (ICastTargetDefinition) null;
      }
      string str = jtoken.Value<string>();
      ICastTargetDefinition targetDefinition;
      switch (str)
      {
        case "OneCastTargetDefinition":
          targetDefinition = (ICastTargetDefinition) new OneCastTargetDefinition();
          break;
        case "TwoCastTargetDefinition":
          targetDefinition = (ICastTargetDefinition) new TwoCastTargetDefinition();
          break;
        case "MultipleCastTargetDefinition":
          targetDefinition = (ICastTargetDefinition) new MultipleCastTargetDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ICastTargetDefinition) null;
      }
      targetDefinition.PopulateFromJson(jsonObject);
      return targetDefinition;
    }

    public static ICastTargetDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ICastTargetDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ICastTargetDefinitionUtils.FromJsonToken(jproperty.Value);
    }
  }
}
