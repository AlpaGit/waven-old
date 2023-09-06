// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IEffectListUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IEffectListUtils
  {
    public static IEffectList FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IEffectList) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IEffectList");
        return (IEffectList) null;
      }
      string str = jtoken.Value<string>();
      IEffectList effectList;
      switch (str)
      {
        case "WeaponDefinition":
          effectList = (IEffectList) new WeaponDefinition();
          break;
        case "SummoningDefinition":
          effectList = (IEffectList) new SummoningDefinition();
          break;
        case "CompanionDefinition":
          effectList = (IEffectList) new CompanionDefinition();
          break;
        case "FloorMechanismDefinition":
          effectList = (IEffectList) new FloorMechanismDefinition();
          break;
        case "ObjectMechanismDefinition":
          effectList = (IEffectList) new ObjectMechanismDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IEffectList) null;
      }
      effectList.PopulateFromJson(jsonObject);
      return effectList;
    }

    public static IEffectList FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IEffectList defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IEffectListUtils.FromJsonToken(jproperty.Value);
    }
  }
}
