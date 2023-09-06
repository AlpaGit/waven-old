// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PropertyFilter
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
  public sealed class PropertyFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private PropertyId m_property;

    public PropertyId property => this.m_property;

    public override string ToString() => this.GetType().Name;

    public static PropertyFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PropertyFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PropertyFilter propertyFilter = new PropertyFilter();
      propertyFilter.PopulateFromJson(jsonObject);
      return propertyFilter;
    }

    public static PropertyFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PropertyFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PropertyFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_property = (PropertyId) Serialization.JsonTokenValue<int>(jsonObject, "property");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      foreach (IEntity entity in entities)
      {
        if (entity.HasProperty(this.m_property))
          yield return entity;
      }
    }
  }
}
