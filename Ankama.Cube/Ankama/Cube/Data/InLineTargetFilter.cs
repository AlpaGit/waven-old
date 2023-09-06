// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.InLineTargetFilter
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
  public sealed class InLineTargetFilter : 
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

    public override string ToString() => string.Format("distance in line to {0} {1}", (object) this.m_targetsToCompare, (object) this.distance);

    public static InLineTargetFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (InLineTargetFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      InLineTargetFilter lineTargetFilter = new InLineTargetFilter();
      lineTargetFilter.PopulateFromJson(jsonObject);
      return lineTargetFilter;
    }

    public static InLineTargetFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      InLineTargetFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : InLineTargetFilter.FromJsonToken(jproperty.Value);
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
            Area other = areas[index];
            if (this.distance.Matches(withBoardPresence.area.MinDistanceWith(other), context) && withBoardPresence.area.IsAlignedWith(other))
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
          Area area = areas[index];
          if (this.distance.Matches(area.MinDistanceWith(other), context) && area.IsAlignedWith(other))
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
