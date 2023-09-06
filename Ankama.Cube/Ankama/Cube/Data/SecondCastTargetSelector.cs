// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SecondCastTargetSelector
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
  public sealed class SecondCastTargetSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleEntitySelector,
    IEntitySelector,
    ISingleTargetSelector,
    ISingleCoordSelector,
    ICoordSelector
  {
    public override string ToString() => this.GetType().Name;

    public static SecondCastTargetSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SecondCastTargetSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SecondCastTargetSelector castTargetSelector = new SecondCastTargetSelector();
      castTargetSelector.PopulateFromJson(jsonObject);
      return castTargetSelector;
    }

    public static SecondCastTargetSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SecondCastTargetSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SecondCastTargetSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      if (context is CastTargetContext castTargetContext)
      {
        Target target = castTargetContext.GetTarget(1);
        if (target.type == Target.Type.Entity && target.entity is T entity1)
        {
          entity = entity1;
          return true;
        }
      }
      entity = default (T);
      return false;
    }

    public bool TryGetCoord(DynamicValueContext context, out Coord coord)
    {
      if (context is CastTargetContext castTargetContext)
      {
        Target target = castTargetContext.GetTarget(1);
        if (target.type == Target.Type.Coord)
        {
          coord = target.coord;
          return true;
        }
      }
      coord = new Coord();
      return false;
    }

    public IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      IEntity entity;
      if (this.TryGetEntity<IEntity>(context, out entity))
        yield return entity;
    }

    public IEnumerable<Coord> EnumerateCoords(DynamicValueContext context)
    {
      Coord coord;
      if (this.TryGetCoord(context, out coord))
        yield return coord;
    }
  }
}
