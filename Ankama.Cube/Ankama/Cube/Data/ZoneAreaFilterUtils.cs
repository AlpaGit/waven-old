// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ZoneAreaFilterUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public static class ZoneAreaFilterUtils
  {
    public static bool SingleTargetToCompareArea(
      ISingleTargetSelector targetToCompare,
      DynamicValueContext context,
      out Area area)
    {
      IEntityWithBoardPresence entity;
      Coord coord;
      switch (targetToCompare)
      {
        case ISingleEntitySelector singleEntitySelector when singleEntitySelector.TryGetEntity<IEntityWithBoardPresence>(context, out entity):
          area = entity.area;
          return true;
        case ISingleCoordSelector singleCoordSelector when singleCoordSelector.TryGetCoord(context, out coord):
          area = (Area) new PointArea((Vector2Int) coord);
          return true;
        default:
          area = (Area) null;
          return false;
      }
    }

    public static IEnumerable<Area> TargetsToCompareAreaList(
      ITargetSelector targetToCompare,
      DynamicValueContext context)
    {
      if (targetToCompare is IEntitySelector entitySelector)
      {
        foreach (IEntity enumerateEntity in entitySelector.EnumerateEntities(context))
        {
          if (enumerateEntity is IEntityWithBoardPresence withBoardPresence)
            yield return withBoardPresence.area;
        }
      }
      if (targetToCompare is ICoordSelector coordSelector)
      {
        foreach (Coord enumerateCoord in coordSelector.EnumerateCoords(context))
          yield return (Area) new PointArea((Vector2Int) enumerateCoord);
      }
    }
  }
}
