// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ILevelOnlyDependantUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ILevelOnlyDependantUtils
  {
    public static ILevelOnlyDependant FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ILevelOnlyDependant) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ILevelOnlyDependant");
        return (ILevelOnlyDependant) null;
      }
      string str = jtoken.Value<string>();
      ILevelOnlyDependant levelOnlyDependant;
      switch (str)
      {
        case "LinearLevelBasedDynamicValue":
          levelOnlyDependant = (ILevelOnlyDependant) new LinearLevelBasedDynamicValue();
          break;
        case "ConstIntLevelBasedDynamicValue":
          levelOnlyDependant = (ILevelOnlyDependant) new ConstIntLevelBasedDynamicValue();
          break;
        case "ConstIntegerValue":
          levelOnlyDependant = (ILevelOnlyDependant) new ConstIntegerValue();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ILevelOnlyDependant) null;
      }
      levelOnlyDependant.PopulateFromJson(jsonObject);
      return levelOnlyDependant;
    }

    public static ILevelOnlyDependant FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ILevelOnlyDependant defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ILevelOnlyDependantUtils.FromJsonToken(jproperty.Value);
    }
  }
}
