// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.NotEntityFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

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
  public sealed class NotEntityFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private IEntityFilter m_filter;

    public IEntityFilter filter => this.m_filter;

    public override string ToString() => string.Format("not ({0})", (object) this.filter);

    public static NotEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (NotEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      NotEntityFilter notEntityFilter = new NotEntityFilter();
      notEntityFilter.PopulateFromJson(jsonObject);
      return notEntityFilter;
    }

    public static NotEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      NotEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : NotEntityFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_filter = IEntityFilterUtils.FromJsonProperty(jsonObject, "filter");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      List<IEntity> entityList = ListPool<IEntity>.Get();
      List<IEntity> filtered = ListPool<IEntity>.Get();
      entityList.AddRange(entities);
      filtered.AddRange(this.m_filter.Filter((IEnumerable<IEntity>) entityList, context));
      int coordCount = entityList.Count;
      int filteredCount = filtered.Count;
label_7:
      for (int i = 0; i < coordCount; ++i)
      {
        IEntity entity = entityList[i];
        for (int index = 0; index < filteredCount; ++index)
        {
          if (entity == filtered[index])
            goto label_7;
        }
        yield return entity;
      }
      ListPool<IEntity>.Release(entityList);
      ListPool<IEntity>.Release(filtered);
    }
  }
}
