// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.HasEmptyPathInLineToTargetFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class HasEmptyPathInLineToTargetFilter : 
    IEditableContent,
    ICoordOrEntityFilter,
    ICoordFilter,
    ITargetFilter,
    IEntityFilter
  {
    private ISingleTargetSelector m_startCoords;

    public ISingleTargetSelector startCoords => this.m_startCoords;

    public override string ToString() => this.GetType().Name;

    public static HasEmptyPathInLineToTargetFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (HasEmptyPathInLineToTargetFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      HasEmptyPathInLineToTargetFilter lineToTargetFilter = new HasEmptyPathInLineToTargetFilter();
      lineToTargetFilter.PopulateFromJson(jsonObject);
      return lineToTargetFilter;
    }

    public static HasEmptyPathInLineToTargetFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      HasEmptyPathInLineToTargetFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : HasEmptyPathInLineToTargetFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_startCoords = ISingleTargetSelectorUtils.FromJsonProperty(jsonObject, "startCoords");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      Coord srcCoord;
      if (context is DynamicValueFightContext valueFightContext && this.TryGetSrcEntityCoords(context, out srcCoord))
      {
        FightStatus fightStatus = valueFightContext.fightStatus;
        foreach (IEntity entity in entities)
        {
          if (entity is IEntityWithBoardPresence withBoardPresence && HasEmptyPathInLineToTargetFilter.IsPathEmptyBetween(fightStatus, srcCoord, new Coord(withBoardPresence.area.refCoord), context))
            yield return (IEntity) withBoardPresence;
        }
      }
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      Coord srcCoord;
      if (context is DynamicValueFightContext valueFightContext && this.TryGetSrcEntityCoords(context, out srcCoord))
      {
        FightStatus fightStatus = valueFightContext.fightStatus;
        foreach (Coord coord in coords)
        {
          if (HasEmptyPathInLineToTargetFilter.IsPathEmptyBetween(fightStatus, srcCoord, coord, context))
            yield return coord;
        }
      }
    }

    private bool TryGetSrcEntityCoords(DynamicValueContext context, out Coord coords)
    {
      if (this.m_startCoords is ISingleEntitySelector startCoords1)
      {
        IEntityWithBoardPresence entity;
        if (startCoords1.TryGetEntity<IEntityWithBoardPresence>(context, out entity))
        {
          coords = new Coord(entity.area.refCoord);
          return true;
        }
        coords = new Coord();
        return false;
      }
      if (this.m_startCoords is ISingleCoordSelector startCoords2)
        return startCoords2.TryGetCoord(context, out coords);
      coords = new Coord();
      return false;
    }

    private static bool IsPathEmptyBetween(
      FightStatus fightStatus,
      Coord src,
      Coord dest,
      DynamicValueContext context)
    {
      if (!src.IsAlignedWith(dest))
        return false;
      FightMapStatus mapStatus = fightStatus.mapStatus;
      foreach (Coord position in src.StraightPathUntil(dest))
      {
        switch (mapStatus.GetCellState(position.x, position.y))
        {
          case FightCellState.None:
            return false;
          case FightCellState.Movement:
            if (fightStatus.HasEntityBlockingMovementAt((Vector2Int) position))
              return false;
            continue;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      return true;
    }
  }
}
