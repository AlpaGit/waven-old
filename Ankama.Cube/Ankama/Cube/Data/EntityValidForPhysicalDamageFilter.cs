// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntityValidForPhysicalDamageFilter
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
  public sealed class EntityValidForPhysicalDamageFilter : 
    IEditableContent,
    IEntityFilter,
    ITargetFilter
  {
    public override string ToString() => this.GetType().Name;

    public static EntityValidForPhysicalDamageFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EntityValidForPhysicalDamageFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EntityValidForPhysicalDamageFilter physicalDamageFilter = new EntityValidForPhysicalDamageFilter();
      physicalDamageFilter.PopulateFromJson(jsonObject);
      return physicalDamageFilter;
    }

    public static EntityValidForPhysicalDamageFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EntityValidForPhysicalDamageFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EntityValidForPhysicalDamageFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      foreach (IEntity entity in entities)
      {
        if (entity is CharacterStatus && !entity.HasAnyProperty(PropertiesUtility.propertiesWhichPreventPhysicalDamage))
          yield return entity;
      }
    }
  }
}
