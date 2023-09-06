// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UnionOfCoordOrEntityFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class UnionOfCoordOrEntityFilter : 
    IEditableContent,
    ICoordOrEntityFilter,
    ICoordFilter,
    ITargetFilter,
    IEntityFilter,
    IUnionTargetsFilter
  {
    private ICoordOrEntityFilter m_firstFilter;
    private ICoordOrEntityFilter m_secondFilter;

    public ICoordOrEntityFilter firstFilter => this.m_firstFilter;

    public ICoordOrEntityFilter secondFilter => this.m_secondFilter;

    public override string ToString() => string.Format("({0} OR {1})", (object) this.m_firstFilter, (object) this.m_secondFilter);

    public static UnionOfCoordOrEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (UnionOfCoordOrEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      UnionOfCoordOrEntityFilter coordOrEntityFilter = new UnionOfCoordOrEntityFilter();
      coordOrEntityFilter.PopulateFromJson(jsonObject);
      return coordOrEntityFilter;
    }

    public static UnionOfCoordOrEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      UnionOfCoordOrEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : UnionOfCoordOrEntityFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_firstFilter = ICoordOrEntityFilterUtils.FromJsonProperty(jsonObject, "firstFilter");
      this.m_secondFilter = ICoordOrEntityFilterUtils.FromJsonProperty(jsonObject, "secondFilter");
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      Coord[] allCoords = coords.ToArray<Coord>();
      Coord[] first = this.m_firstFilter.Filter((IEnumerable<Coord>) allCoords, context).ToArray<Coord>();
      for (int i = 0; i < first.Length; ++i)
        yield return first[i];
      foreach (Coord coord in this.m_secondFilter.Filter((IEnumerable<Coord>) allCoords, context))
      {
        if (!((IEnumerable<Coord>) first).Contains<Coord>(coord))
          yield return coord;
      }
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      IEntity[] allEntities = entities.ToArray<IEntity>();
      IEntity[] first = this.m_firstFilter.Filter((IEnumerable<IEntity>) allEntities, context).ToArray<IEntity>();
      IEntity[] entityArray = first;
      for (int index = 0; index < entityArray.Length; ++index)
        yield return entityArray[index];
      entityArray = (IEntity[]) null;
      foreach (IEntity entity in this.m_secondFilter.Filter((IEnumerable<IEntity>) allEntities, context))
      {
        if (!((IEnumerable<IEntity>) first).Contains<IEntity>(entity))
          yield return entity;
      }
    }
  }
}
