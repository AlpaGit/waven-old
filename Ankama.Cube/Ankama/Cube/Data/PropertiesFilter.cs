// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PropertiesFilter
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
  [RelatedToEvents(new EventCategory[] {EventCategory.PropertyChanged})]
  [Serializable]
  public sealed class PropertiesFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ListComparison m_comparison;
    private List<PropertyId> m_properties;

    public ListComparison comparison => this.m_comparison;

    public IReadOnlyList<PropertyId> properties => (IReadOnlyList<PropertyId>) this.m_properties;

    public override string ToString()
    {
      string str;
      switch (this.m_properties.Count)
      {
        case 0:
          str = "<unset>";
          break;
        case 1:
          str = string.Format("{0}", (object) this.m_properties[0]);
          break;
        default:
          str = "\n - " + string.Join<PropertyId>("\n - ", (IEnumerable<PropertyId>) this.m_properties);
          break;
      }
      return string.Format("{0} {1}", (object) this.m_comparison, (object) str);
    }

    public static PropertiesFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PropertiesFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PropertiesFilter propertiesFilter = new PropertiesFilter();
      propertiesFilter.PopulateFromJson(jsonObject);
      return propertiesFilter;
    }

    public static PropertiesFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PropertiesFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PropertiesFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_comparison = (ListComparison) Serialization.JsonTokenValue<int>(jsonObject, "comparison", 1);
      this.m_properties = Serialization.JsonArrayAsList<PropertyId>(jsonObject, "properties");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      ListComparison comp = this.m_comparison;
      List<PropertyId> expectedProperties = this.m_properties;
      foreach (IEntity entity in entities)
      {
        if (PropertiesFilter.ValidateCondition(entity, comp, expectedProperties))
          yield return entity;
      }
    }

    public static bool ValidateCondition(
      IEntity entity,
      ListComparison comparison,
      List<PropertyId> properties)
    {
      return ListComparisonUtility.ValidateCondition<PropertyId>(entity.properties, comparison, (IReadOnlyList<PropertyId>) properties);
    }
  }
}
