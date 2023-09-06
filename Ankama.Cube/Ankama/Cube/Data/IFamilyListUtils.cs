// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IFamilyListUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IFamilyListUtils
  {
    public static IFamilyList FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IFamilyList) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IFamilyList");
        return (IFamilyList) null;
      }
      string str = jtoken.Value<string>();
      IFamilyList familyList;
      switch (str)
      {
        case "WeaponDefinition":
          familyList = (IFamilyList) new WeaponDefinition();
          break;
        case "SummoningDefinition":
          familyList = (IFamilyList) new SummoningDefinition();
          break;
        case "CompanionDefinition":
          familyList = (IFamilyList) new CompanionDefinition();
          break;
        case "FloorMechanismDefinition":
          familyList = (IFamilyList) new FloorMechanismDefinition();
          break;
        case "ObjectMechanismDefinition":
          familyList = (IFamilyList) new ObjectMechanismDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IFamilyList) null;
      }
      familyList.PopulateFromJson(jsonObject);
      return familyList;
    }

    public static IFamilyList FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IFamilyList defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IFamilyListUtils.FromJsonToken(jproperty.Value);
    }
  }
}
