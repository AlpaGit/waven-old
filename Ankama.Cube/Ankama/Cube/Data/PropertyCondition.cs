// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PropertyCondition
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
  public sealed class PropertyCondition : EffectCondition
  {
    private ISingleEntitySelector m_selector;
    private ListComparison m_comparison;
    private List<PropertyId> m_properties;

    public ISingleEntitySelector selector => this.m_selector;

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
      return string.Format("{0} {1} {2}", (object) this.m_selector, (object) this.m_comparison, (object) str);
    }

    public static PropertyCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PropertyCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PropertyCondition propertyCondition = new PropertyCondition();
      propertyCondition.PopulateFromJson(jsonObject);
      return propertyCondition;
    }

    public static PropertyCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PropertyCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PropertyCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_selector = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "selector");
      this.m_comparison = (ListComparison) Serialization.JsonTokenValue<int>(jsonObject, "comparison", 1);
      this.m_properties = Serialization.JsonArrayAsList<PropertyId>(jsonObject, "properties");
    }

    public override bool IsValid(DynamicValueContext context)
    {
      EntityStatus entity;
      return this.selector.TryGetEntity<EntityStatus>(context, out entity) && PropertiesFilter.ValidateCondition((IEntity) entity, this.m_comparison, this.m_properties);
    }
  }
}
