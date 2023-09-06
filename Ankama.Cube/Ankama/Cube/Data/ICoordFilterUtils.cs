// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ICoordFilterUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.EntityAddedOrRemoved, EventCategory.EntityMoved})]
  public class ICoordFilterUtils
  {
    public static ICoordFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ICoordFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ICoordFilter");
        return (ICoordFilter) null;
      }
      string str = jtoken.Value<string>();
      ICoordFilter coordFilter;
      switch (str)
      {
        case "AroundSquaredTargetFilter":
          coordFilter = (ICoordFilter) new AroundSquaredTargetFilter();
          break;
        case "AroundTargetFilter":
          coordFilter = (ICoordFilter) new AroundTargetFilter();
          break;
        case "CellValidForCharacterFilter":
          coordFilter = (ICoordFilter) new CellValidForCharacterFilter();
          break;
        case "CellValidForMechanismFilter":
          coordFilter = (ICoordFilter) new CellValidForMechanismFilter();
          break;
        case "FirstTargetsFilter":
          coordFilter = (ICoordFilter) new FirstTargetsFilter();
          break;
        case "HasEmptyPathInLineToTargetFilter":
          coordFilter = (ICoordFilter) new HasEmptyPathInLineToTargetFilter();
          break;
        case "InLineInOneDirectionTargetFilter":
          coordFilter = (ICoordFilter) new InLineInOneDirectionTargetFilter();
          break;
        case "InLineTargetFilter":
          coordFilter = (ICoordFilter) new InLineTargetFilter();
          break;
        case "NotCoordFilter":
          coordFilter = (ICoordFilter) new NotCoordFilter();
          break;
        case "RandomTargetsFilter":
          coordFilter = (ICoordFilter) new RandomTargetsFilter();
          break;
        case "UnionOfCoordFilter":
          coordFilter = (ICoordFilter) new UnionOfCoordFilter();
          break;
        case "UnionOfCoordOrEntityFilter":
          coordFilter = (ICoordFilter) new UnionOfCoordOrEntityFilter();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ICoordFilter) null;
      }
      coordFilter.PopulateFromJson(jsonObject);
      return coordFilter;
    }

    public static ICoordFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ICoordFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ICoordFilterUtils.FromJsonToken(jproperty.Value);
    }
  }
}
