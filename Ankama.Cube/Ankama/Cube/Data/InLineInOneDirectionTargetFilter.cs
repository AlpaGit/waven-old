// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.InLineInOneDirectionTargetFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
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
  public sealed class InLineInOneDirectionTargetFilter : 
    IEditableContent,
    ICoordOrEntityFilter,
    ICoordFilter,
    ITargetFilter,
    IEntityFilter
  {
    private ISingleTargetSelector m_refDirectionTargetA;
    private ISingleTargetSelector m_refDirectionTargetB;
    private ITargetSelector m_applyStartTargets;
    private ValueFilter m_distance;

    public ISingleTargetSelector refDirectionTargetA => this.m_refDirectionTargetA;

    public ISingleTargetSelector refDirectionTargetB => this.m_refDirectionTargetB;

    public ITargetSelector applyStartTargets => this.m_applyStartTargets;

    public ValueFilter distance => this.m_distance;

    public override string ToString() => "Vector from A to B.";

    public static InLineInOneDirectionTargetFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (InLineInOneDirectionTargetFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      InLineInOneDirectionTargetFilter directionTargetFilter = new InLineInOneDirectionTargetFilter();
      directionTargetFilter.PopulateFromJson(jsonObject);
      return directionTargetFilter;
    }

    public static InLineInOneDirectionTargetFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      InLineInOneDirectionTargetFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : InLineInOneDirectionTargetFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_refDirectionTargetA = ISingleTargetSelectorUtils.FromJsonProperty(jsonObject, "refDirectionTargetA");
      this.m_refDirectionTargetB = ISingleTargetSelectorUtils.FromJsonProperty(jsonObject, "refDirectionTargetB");
      this.m_applyStartTargets = ITargetSelectorUtils.FromJsonProperty(jsonObject, "applyStartTargets");
      this.m_distance = ValueFilter.FromJsonProperty(jsonObject, "distance");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      Area area1;
      Area area2;
      if (ZoneAreaFilterUtils.SingleTargetToCompareArea(this.refDirectionTargetA, context, out area1) && ZoneAreaFilterUtils.SingleTargetToCompareArea(this.refDirectionTargetB, context, out area2))
      {
        Direction? dirOpt = area1.refCoord.GetStrictDirection4To(area2.refCoord);
        if (dirOpt.HasValue)
        {
          List<Area> applyAreas = ListPool<Area>.Get();
          applyAreas.AddRange(ZoneAreaFilterUtils.TargetsToCompareAreaList(this.applyStartTargets, context));
          int applyAreaCount = applyAreas.Count;
          foreach (IEntity entity in entities)
          {
            if (entity is IEntityWithBoardPresence withBoardPresence)
            {
              for (int index = 0; index < applyAreaCount; ++index)
              {
                Area other = applyAreas[index];
                if (this.distance.Matches(withBoardPresence.area.MinDistanceWith(other), context) && withBoardPresence.area.IsAlignedWith(other))
                {
                  Direction? strictDirection4To = other.GetStrictDirection4To(withBoardPresence.area);
                  Direction? nullable = dirOpt;
                  if (strictDirection4To.GetValueOrDefault() == nullable.GetValueOrDefault() & strictDirection4To.HasValue == nullable.HasValue)
                  {
                    yield return entity;
                    break;
                  }
                }
              }
            }
          }
          ListPool<Area>.Release(applyAreas);
          dirOpt = new Direction?();
          applyAreas = (List<Area>) null;
        }
      }
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      Area area1;
      Area area2;
      if (ZoneAreaFilterUtils.SingleTargetToCompareArea(this.refDirectionTargetA, context, out area1) && ZoneAreaFilterUtils.SingleTargetToCompareArea(this.refDirectionTargetB, context, out area2))
      {
        Direction? dirOpt = area1.refCoord.GetStrictDirection4To(area2.refCoord);
        if (dirOpt.HasValue)
        {
          List<Area> applyAreas = ListPool<Area>.Get();
          applyAreas.AddRange(ZoneAreaFilterUtils.TargetsToCompareAreaList(this.applyStartTargets, context));
          int applyAreaCount = applyAreas.Count;
          foreach (Coord coord in coords)
          {
            Vector2Int vector2Int = new Vector2Int(coord.x, coord.y);
            for (int index = 0; index < applyAreaCount; ++index)
            {
              Area area3 = applyAreas[index];
              if (this.distance.Matches(area3.MinDistanceWith(vector2Int), context) && area3.IsAlignedWith(vector2Int))
              {
                Direction? strictDirection4To = area3.GetStrictDirection4To(vector2Int);
                Direction? nullable = dirOpt;
                if (strictDirection4To.GetValueOrDefault() == nullable.GetValueOrDefault() & strictDirection4To.HasValue == nullable.HasValue)
                {
                  yield return coord;
                  break;
                }
              }
            }
          }
          ListPool<Area>.Release(applyAreas);
          dirOpt = new Direction?();
          applyAreas = (List<Area>) null;
        }
      }
    }
  }
}
