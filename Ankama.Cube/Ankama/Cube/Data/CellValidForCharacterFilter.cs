// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CellValidForCharacterFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class CellValidForCharacterFilter : IEditableContent, ICoordFilter, ITargetFilter
  {
    public override string ToString() => this.GetType().Name;

    public static CellValidForCharacterFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CellValidForCharacterFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CellValidForCharacterFilter forCharacterFilter = new CellValidForCharacterFilter();
      forCharacterFilter.PopulateFromJson(jsonObject);
      return forCharacterFilter;
    }

    public static CellValidForCharacterFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CellValidForCharacterFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CellValidForCharacterFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      DynamicValueFightContext valueFightContext = (DynamicValueFightContext) (context as CastTargetContext);
      return valueFightContext == null ? ICoordFilterExtension.empty : CellValidForCharacterFilter.Filter(coords, valueFightContext.fightStatus);
    }

    private static IEnumerable<Coord> Filter(IEnumerable<Coord> coords, FightStatus fightStatus)
    {
      foreach (Coord coord in coords)
      {
        if (!fightStatus.HasEntityBlockingMovementAt((Vector2Int) coord))
          yield return coord;
      }
    }

    public static IEnumerable<Coord> EnumerateCells(FightStatus fightStatus) => CellValidForCharacterFilter.Filter(fightStatus.EnumerateCoords(), fightStatus);
  }
}
