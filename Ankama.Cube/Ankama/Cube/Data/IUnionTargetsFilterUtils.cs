// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IUnionTargetsFilterUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IUnionTargetsFilterUtils
  {
    public static IUnionTargetsFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IUnionTargetsFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IUnionTargetsFilter");
        return (IUnionTargetsFilter) null;
      }
      string str = jtoken.Value<string>();
      IUnionTargetsFilter unionTargetsFilter;
      switch (str)
      {
        case "UnionOfEntityFilter":
          unionTargetsFilter = (IUnionTargetsFilter) new UnionOfEntityFilter();
          break;
        case "UnionOfCoordFilter":
          unionTargetsFilter = (IUnionTargetsFilter) new UnionOfCoordFilter();
          break;
        case "UnionOfCoordOrEntityFilter":
          unionTargetsFilter = (IUnionTargetsFilter) new UnionOfCoordOrEntityFilter();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IUnionTargetsFilter) null;
      }
      unionTargetsFilter.PopulateFromJson(jsonObject);
      return unionTargetsFilter;
    }

    public static IUnionTargetsFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IUnionTargetsFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IUnionTargetsFilterUtils.FromJsonToken(jproperty.Value);
    }
  }
}
