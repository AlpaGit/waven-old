// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntityTypeFilter
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
  public sealed class EntityTypeFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<EntityType> m_entityTypes;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<EntityType> entityTypes => (IReadOnlyList<EntityType>) this.m_entityTypes;

    public override string ToString()
    {
      switch (this.m_entityTypes.Count)
      {
        case 0:
          return "entity type <unset>";
        case 1:
          return string.Format("entity type {0} {1}", (object) this.condition, (object) this.m_entityTypes[0]);
        default:
          return string.Format("entity type {0} \n - ", (object) this.condition) + string.Join<EntityType>("\n - ", (IEnumerable<EntityType>) this.m_entityTypes);
      }
    }

    public static EntityTypeFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EntityTypeFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EntityTypeFilter entityTypeFilter = new EntityTypeFilter();
      entityTypeFilter.PopulateFromJson(jsonObject);
      return entityTypeFilter;
    }

    public static EntityTypeFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EntityTypeFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EntityTypeFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      this.m_entityTypes = Serialization.JsonArrayAsList<EntityType>(jsonObject, "entityTypes");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      bool shouldContains = this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      foreach (IEntity entity in entities)
      {
        if (this.ContainsEntityType(entity.type) == shouldContains)
          yield return entity;
      }
    }

    private bool ContainsEntityType(EntityType entityType)
    {
      int count = this.m_entityTypes.Count;
      for (int index = 0; index < count; ++index)
      {
        if (entityType == this.m_entityTypes[index])
          return true;
      }
      return false;
    }
  }
}
