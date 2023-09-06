// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UnionOfEntityFilter
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
  public sealed class UnionOfEntityFilter : 
    IEditableContent,
    IEntityFilter,
    ITargetFilter,
    IUnionTargetsFilter
  {
    private IEntityFilter m_firstFilter;
    private IEntityFilter m_secondFilter;

    public IEntityFilter firstFilter => this.m_firstFilter;

    public IEntityFilter secondFilter => this.m_secondFilter;

    public override string ToString() => string.Format("({0} OR {1})", (object) this.m_firstFilter, (object) this.m_secondFilter);

    public static UnionOfEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (UnionOfEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      UnionOfEntityFilter unionOfEntityFilter = new UnionOfEntityFilter();
      unionOfEntityFilter.PopulateFromJson(jsonObject);
      return unionOfEntityFilter;
    }

    public static UnionOfEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      UnionOfEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : UnionOfEntityFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_firstFilter = IEntityFilterUtils.FromJsonProperty(jsonObject, "firstFilter");
      this.m_secondFilter = IEntityFilterUtils.FromJsonProperty(jsonObject, "secondFilter");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      IEntity[] allEntities = entities.ToArray<IEntity>();
      IEntity[] first = this.m_firstFilter.Filter((IEnumerable<IEntity>) allEntities, context).ToArray<IEntity>();
      for (int i = 0; i < first.Length; ++i)
        yield return first[i];
      foreach (IEntity entity in this.m_secondFilter.Filter((IEnumerable<IEntity>) allEntities, context))
      {
        if (!((IEnumerable<IEntity>) first).Contains<IEntity>(entity))
          yield return entity;
      }
    }
  }
}
