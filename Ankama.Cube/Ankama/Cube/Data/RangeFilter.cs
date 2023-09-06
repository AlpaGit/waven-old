// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.RangeFilter
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
  public sealed class RangeFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ValueFilter m_valueFilter;

    public ValueFilter valueFilter => this.m_valueFilter;

    public override string ToString() => this.m_valueFilter != null ? string.Format("entity with RangeMax {0}", (object) this.valueFilter) : "entity with No Range";

    public static RangeFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (RangeFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      RangeFilter rangeFilter = new RangeFilter();
      rangeFilter.PopulateFromJson(jsonObject);
      return rangeFilter;
    }

    public static RangeFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      RangeFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : RangeFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_valueFilter = ValueFilter.FromJsonProperty(jsonObject, "valueFilter");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      ValueFilter filter = this.m_valueFilter;
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithAction entityWithAction)
        {
          if (filter == null)
          {
            if (!entityWithAction.hasRange)
              yield return entity;
          }
          else if (entityWithAction.hasRange && filter.Matches(entityWithAction.rangeMax, context))
            yield return entity;
        }
      }
    }
  }
}
