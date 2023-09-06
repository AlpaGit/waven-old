// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CentralSymmetryCoordSelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class CentralSymmetryCoordSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleCoordSelector,
    ICoordSelector,
    ISingleTargetSelector
  {
    private ISingleTargetSelector m_symmetryCenter;
    private ISingleTargetSelector m_refCoords;

    public ISingleTargetSelector symmetryCenter => this.m_symmetryCenter;

    public ISingleTargetSelector refCoords => this.m_refCoords;

    public override string ToString() => this.GetType().Name;

    public static CentralSymmetryCoordSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CentralSymmetryCoordSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CentralSymmetryCoordSelector symmetryCoordSelector = new CentralSymmetryCoordSelector();
      symmetryCoordSelector.PopulateFromJson(jsonObject);
      return symmetryCoordSelector;
    }

    public static CentralSymmetryCoordSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CentralSymmetryCoordSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CentralSymmetryCoordSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_symmetryCenter = ISingleTargetSelectorUtils.FromJsonProperty(jsonObject, "symmetryCenter");
      this.m_refCoords = ISingleTargetSelectorUtils.FromJsonProperty(jsonObject, "refCoords");
    }

    public IEnumerable<Coord> EnumerateCoords(DynamicValueContext context)
    {
      Coord? coord1 = this.TryGetCoord(this.m_refCoords, context);
      if (coord1.HasValue)
      {
        Coord? coord2 = this.TryGetCoord(this.m_symmetryCenter, context);
        if (coord2.HasValue)
        {
          Coord coord3 = coord1.Value;
          Coord coord4 = coord2.Value;
          yield return new Coord(2 * coord4.x - coord3.x, 2 * coord4.y - coord3.y);
        }
      }
    }

    public bool TryGetCoord(DynamicValueContext context, out Coord coord)
    {
      coord = new Coord();
      return false;
    }

    private Coord? TryGetCoord(ISingleTargetSelector selector, DynamicValueContext context)
    {
      switch (selector)
      {
        case ISingleEntitySelector singleEntitySelector:
          IEntityWithBoardPresence entity;
          return singleEntitySelector.TryGetEntity<IEntityWithBoardPresence>(context, out entity) ? new Coord?(new Coord(entity.area.refCoord)) : new Coord?();
        case ISingleCoordSelector singleCoordSelector:
          Coord coord;
          return singleCoordSelector.TryGetCoord(context, out coord) ? new Coord?(coord) : new Coord?();
        default:
          return new Coord?();
      }
    }
  }
}
