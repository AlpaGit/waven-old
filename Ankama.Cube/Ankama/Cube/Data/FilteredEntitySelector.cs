// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FilteredEntitySelector
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
  public sealed class FilteredEntitySelector : EntitySelectorForCast
  {
    private List<IEntityFilter> m_filters;

    public IReadOnlyList<IEntityFilter> filters => (IReadOnlyList<IEntityFilter>) this.m_filters;

    public override string ToString()
    {
      switch (this.m_filters.Count)
      {
        case 0:
          return "All entities";
        case 1:
          return string.Format("entity with ({0})", (object) this.m_filters[0]);
        default:
          return "Entities where:\n - " + string.Join<IEntityFilter>("\n - ", (IEnumerable<IEntityFilter>) this.filters);
      }
    }

    public static FilteredEntitySelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FilteredEntitySelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FilteredEntitySelector filteredEntitySelector = new FilteredEntitySelector();
      filteredEntitySelector.PopulateFromJson(jsonObject);
      return filteredEntitySelector;
    }

    public static FilteredEntitySelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FilteredEntitySelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FilteredEntitySelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "filters");
      this.m_filters = new List<IEntityFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_filters.Add(IEntityFilterUtils.FromJsonToken(token));
    }

    public static FilteredEntitySelector From(params IEntityFilter[] filters) => new FilteredEntitySelector()
    {
      m_filters = ((IEnumerable<IEntityFilter>) filters).ToList<IEntityFilter>()
    };

    public override IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      if (!(context is DynamicValueFightContext valueFightContext))
        return Enumerable.Empty<IEntity>();
      IEnumerable<IEntity> entities = valueFightContext.fightStatus.EnumerateEntities();
      int count = this.m_filters.Count;
      for (int index = 0; index < count; ++index)
        entities = this.m_filters[index].Filter(entities, context);
      return entities;
    }
  }
}
