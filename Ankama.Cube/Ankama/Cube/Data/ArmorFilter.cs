// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ArmorFilter
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
  [RelatedToEvents(new EventCategory[] {EventCategory.LifeArmorChanged})]
  [Serializable]
  public sealed class ArmorFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ValueFilter m_valueFilter;

    public ValueFilter valueFilter => this.m_valueFilter;

    public override string ToString() => string.Format("entity with Armor {0}", (object) this.valueFilter);

    public static ArmorFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ArmorFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ArmorFilter armorFilter = new ArmorFilter();
      armorFilter.PopulateFromJson(jsonObject);
      return armorFilter;
    }

    public static ArmorFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ArmorFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ArmorFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_valueFilter = ValueFilter.FromJsonProperty(jsonObject, "valueFilter");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithLife entityWithLife && entityWithLife.hasArmor && this.m_valueFilter.Matches(entityWithLife.armor, context))
          yield return entity;
      }
    }
  }
}
