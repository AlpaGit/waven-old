// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntityPositionSelector
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
  public sealed class EntityPositionSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleCoordSelector,
    ICoordSelector,
    ISingleTargetSelector
  {
    private ISingleEntitySelector m_entity;

    public ISingleEntitySelector entity => this.m_entity;

    public override string ToString() => this.m_entity.ToString();

    public static EntityPositionSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EntityPositionSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EntityPositionSelector positionSelector = new EntityPositionSelector();
      positionSelector.PopulateFromJson(jsonObject);
      return positionSelector;
    }

    public static EntityPositionSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EntityPositionSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EntityPositionSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_entity = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "entity");

    public IEnumerable<Coord> EnumerateCoords(DynamicValueContext context)
    {
      Coord coord;
      if (this.TryGetCoord(context, out coord))
        yield return coord;
    }

    public bool TryGetCoord(DynamicValueContext context, out Coord coord)
    {
      IEntityWithBoardPresence entity;
      if (this.m_entity.TryGetEntity<IEntityWithBoardPresence>(context, out entity))
      {
        coord = new Coord(entity.area.refCoord);
        return true;
      }
      coord = new Coord();
      return false;
    }
  }
}
