// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AroundTargetFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Utilities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class AroundTargetFilter : 
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

    public override string ToString() => string.Format("distance to {0} {1}", (object) this.m_targetsToCompare, (object) this.distance);

    public static AroundTargetFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AroundTargetFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      AroundTargetFilter aroundTargetFilter = new AroundTargetFilter();
      aroundTargetFilter.PopulateFromJson(jsonObject);
      return aroundTargetFilter;
    }

    public static AroundTargetFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AroundTargetFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AroundTargetFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_targetsToCompare = ITargetSelectorUtils.FromJsonProperty(jsonObject, "targetsToCompare");
      this.m_distance = ValueFilter.FromJsonProperty(jsonObject, "distance");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      List<Area> areas = ListPool<Area>.Get();
      areas.AddRange(ZoneAreaFilterUtils.TargetsToCompareAreaList(this.targetsToCompare, context));
      int areaCount = areas.Count;
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithBoardPresence withBoardPresence)
        {
          for (int index = 0; index < areaCount; ++index)
          {
            if (this.distance.Matches(withBoardPresence.area.MinDistanceWith(areas[index]), context))
            {
              yield return entity;
              break;
            }
          }
        }
      }
      ListPool<Area>.Release(areas);
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      List<Area> areas = ListPool<Area>.Get();
      areas.AddRange(ZoneAreaFilterUtils.TargetsToCompareAreaList(this.targetsToCompare, context));
      int areaCount = areas.Count;
      foreach (Coord coord in coords)
      {
        Vector2Int other = new Vector2Int(coord.x, coord.y);
        for (int index = 0; index < areaCount; ++index)
        {
          if (this.distance.Matches(areas[index].MinDistanceWith(other), context))
          {
            yield return coord;
            break;
          }
        }
      }
      ListPool<Area>.Release(areas);
    }
  }
}
