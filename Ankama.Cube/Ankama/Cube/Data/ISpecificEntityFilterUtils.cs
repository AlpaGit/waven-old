// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISpecificEntityFilterUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ISpecificEntityFilterUtils
  {
    public static ISpecificEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ISpecificEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ISpecificEntityFilter");
        return (ISpecificEntityFilter) null;
      }
      string str = jtoken.Value<string>();
      ISpecificEntityFilter specificEntityFilter;
      switch (str)
      {
        case "SpecificCompanionFilter":
          specificEntityFilter = (ISpecificEntityFilter) new SpecificCompanionFilter();
          break;
        case "SpecificSummoningFilter":
          specificEntityFilter = (ISpecificEntityFilter) new SpecificSummoningFilter();
          break;
        case "SpecificObjectMechanismFilter":
          specificEntityFilter = (ISpecificEntityFilter) new SpecificObjectMechanismFilter();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ISpecificEntityFilter) null;
      }
      specificEntityFilter.PopulateFromJson(jsonObject);
      return specificEntityFilter;
    }

    public static ISpecificEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ISpecificEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ISpecificEntityFilterUtils.FromJsonToken(jproperty.Value);
    }
  }
}
