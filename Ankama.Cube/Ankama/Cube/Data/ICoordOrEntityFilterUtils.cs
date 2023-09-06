// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ICoordOrEntityFilterUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ICoordOrEntityFilterUtils
  {
    public static ICoordOrEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ICoordOrEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ICoordOrEntityFilter");
        return (ICoordOrEntityFilter) null;
      }
      string str = jtoken.Value<string>();
      ICoordOrEntityFilter coordOrEntityFilter;
      switch (str)
      {
        case "AroundSquaredTargetFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new AroundSquaredTargetFilter();
          break;
        case "AroundTargetFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new AroundTargetFilter();
          break;
        case "FirstTargetsFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new FirstTargetsFilter();
          break;
        case "HasEmptyPathInLineToTargetFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new HasEmptyPathInLineToTargetFilter();
          break;
        case "InLineInOneDirectionTargetFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new InLineInOneDirectionTargetFilter();
          break;
        case "InLineTargetFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new InLineTargetFilter();
          break;
        case "RandomTargetsFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new RandomTargetsFilter();
          break;
        case "UnionOfCoordOrEntityFilter":
          coordOrEntityFilter = (ICoordOrEntityFilter) new UnionOfCoordOrEntityFilter();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ICoordOrEntityFilter) null;
      }
      coordOrEntityFilter.PopulateFromJson(jsonObject);
      return coordOrEntityFilter;
    }

    public static ICoordOrEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ICoordOrEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ICoordOrEntityFilterUtils.FromJsonToken(jproperty.Value);
    }
  }
}
