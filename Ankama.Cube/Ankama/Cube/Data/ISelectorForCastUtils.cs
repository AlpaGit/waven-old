// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISelectorForCastUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ISelectorForCastUtils
  {
    public static ISelectorForCast FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ISelectorForCast) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ISelectorForCast");
        return (ISelectorForCast) null;
      }
      string str = jtoken.Value<string>();
      ISelectorForCast selectorForCast;
      switch (str)
      {
        case "AllEntitiesSelector":
          selectorForCast = (ISelectorForCast) new AllEntitiesSelector();
          break;
        case "CasterHeroSelector":
          selectorForCast = (ISelectorForCast) new CasterHeroSelector();
          break;
        case "CasterSelector":
          selectorForCast = (ISelectorForCast) new CasterSelector();
          break;
        case "ConditionalSelectorForCast":
          selectorForCast = (ISelectorForCast) new ConditionalSelectorForCast();
          break;
        case "FilteredCoordSelector":
          selectorForCast = (ISelectorForCast) new FilteredCoordSelector();
          break;
        case "FilteredEntitySelector":
          selectorForCast = (ISelectorForCast) new FilteredEntitySelector();
          break;
        case "OwnerOfEffectHolderSelector":
          selectorForCast = (ISelectorForCast) new OwnerOfEffectHolderSelector();
          break;
        case "OwnerOfSelector":
          selectorForCast = (ISelectorForCast) new OwnerOfSelector();
          break;
        case "UnionOfCoordsSelector":
          selectorForCast = (ISelectorForCast) new UnionOfCoordsSelector();
          break;
        case "UnionOfEntitiesSelector":
          selectorForCast = (ISelectorForCast) new UnionOfEntitiesSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ISelectorForCast) null;
      }
      selectorForCast.PopulateFromJson(jsonObject);
      return selectorForCast;
    }

    public static ISelectorForCast FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ISelectorForCast defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ISelectorForCastUtils.FromJsonToken(jproperty.Value);
    }
  }
}
