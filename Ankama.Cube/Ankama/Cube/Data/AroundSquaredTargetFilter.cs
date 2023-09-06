// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AroundSquaredTargetFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class AroundSquaredTargetFilter : 
    IEditableContent,
    ICoordOrEntityFilter,
    ICoordFilter,
    ITargetFilter,
    IEntityFilter
  {
    private ITargetSelector m_targetsToCompare;
    private ValueFilter m_distance;

    public ITargetSelector targetsToCompare => this.m_targetsToCompare;

    public ValueFilter distance => this.m_distance;

    public override string ToString() => string.Format("squared with distance to {0} {1}", (object) this.m_targetsToCompare, (object) this.distance);

    public static AroundSquaredTargetFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AroundSquaredTargetFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      AroundSquaredTargetFilter squaredTargetFilter = new AroundSquaredTargetFilter();
      squaredTargetFilter.PopulateFromJson(jsonObject);
      return squaredTargetFilter;
    }

    public static AroundSquaredTargetFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AroundSquaredTargetFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AroundSquaredTargetFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_targetsToCompare = ITargetSelectorUtils.FromJsonProperty(jsonObject, "targetsToCompare");
      this.m_distance = ValueFilter.FromJsonProperty(jsonObject, "distance");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      List<Area> areas = ZoneAreaFilterUtils.TargetsToCompareAreaList(this.targetsToCompare, context).ToList<Area>();
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithBoardPresence withBoardPresence)
        {
          foreach (Area other in areas)
          {
            if (this.distance.Matches(withBoardPresence.area.MinSquaredDistanceWith(other), context))
            {
              yield return entity;
              break;
            }
          }
        }
      }
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      List<Area> areas = ZoneAreaFilterUtils.TargetsToCompareAreaList(this.targetsToCompare, context).ToList<Area>();
      foreach (Coord coord in coords)
      {
        Vector2Int other = new Vector2Int(coord.x, coord.y);
        foreach (Area area in areas)
        {
          if (this.distance.Matches(area.MinSquaredDistanceWith(other), context))
          {
            yield return coord;
            break;
          }
        }
      }
    }
  }
}
