// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CellValidForMechanismFilter
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
  public sealed class CellValidForMechanismFilter : IEditableContent, ICoordFilter, ITargetFilter
  {
    public override string ToString() => this.GetType().Name;

    public static CellValidForMechanismFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CellValidForMechanismFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CellValidForMechanismFilter forMechanismFilter = new CellValidForMechanismFilter();
      forMechanismFilter.PopulateFromJson(jsonObject);
      return forMechanismFilter;
    }

    public static CellValidForMechanismFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CellValidForMechanismFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CellValidForMechanismFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      DynamicValueFightContext valueFightContext = (DynamicValueFightContext) (context as CastTargetContext);
      return valueFightContext == null ? ICoordFilterExtension.empty : CellValidForMechanismFilter.Filter(coords, valueFightContext.fightStatus);
    }

    private static IEnumerable<Coord> Filter(IEnumerable<Coord> coords, FightStatus fightStatus)
    {
      foreach (Coord coord in coords)
      {
        if (!fightStatus.TryGetEntityAt<IEntityWithBoardPresence>((Vector2Int) coord, out IEntityWithBoardPresence _))
          yield return coord;
      }
    }

    public static IEnumerable<Coord> EnumerateCells(FightStatus fightStatus) => CellValidForMechanismFilter.Filter(fightStatus.EnumerateCoords(), fightStatus);
  }
}
