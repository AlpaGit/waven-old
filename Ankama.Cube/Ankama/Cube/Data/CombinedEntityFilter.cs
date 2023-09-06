// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CombinedEntityFilter
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
  public sealed class CombinedEntityFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private List<IEntityFilter> m_filters;

    public IReadOnlyList<IEntityFilter> filters => (IReadOnlyList<IEntityFilter>) this.m_filters;

    public override string ToString() => this.GetType().Name;

    public static CombinedEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CombinedEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CombinedEntityFilter combinedEntityFilter = new CombinedEntityFilter();
      combinedEntityFilter.PopulateFromJson(jsonObject);
      return combinedEntityFilter;
    }

    public static CombinedEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CombinedEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CombinedEntityFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      JArray jarray = Serialization.JsonArray(jsonObject, "filters");
      this.m_filters = new List<IEntityFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_filters.Add(IEntityFilterUtils.FromJsonToken(token));
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      int index = 0;
      for (int count = this.m_filters.Count; index < count; ++index)
        entities = this.m_filters[index].Filter(entities, context);
      return entities;
    }
  }
}
