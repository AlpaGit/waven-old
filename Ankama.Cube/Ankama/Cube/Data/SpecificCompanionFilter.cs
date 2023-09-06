// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpecificCompanionFilter
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
  public sealed class SpecificCompanionFilter : 
    IEditableContent,
    ISpecificEntityFilter,
    IEntityFilter,
    ITargetFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<Id<CompanionDefinition>> m_companions;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<Id<CompanionDefinition>> companions => (IReadOnlyList<Id<CompanionDefinition>>) this.m_companions;

    public override string ToString()
    {
      switch (this.m_companions.Count)
      {
        case 0:
          return "companion is <unset>";
        case 1:
          return string.Format("companion {0} {1}", (object) this.condition, (object) this.m_companions[0]);
        default:
          return string.Format("companion {0} \n - ", (object) this.condition) + string.Join<Id<CompanionDefinition>>("\n - ", (IEnumerable<Id<CompanionDefinition>>) this.m_companions);
      }
    }

    public static SpecificCompanionFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpecificCompanionFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpecificCompanionFilter specificCompanionFilter = new SpecificCompanionFilter();
      specificCompanionFilter.PopulateFromJson(jsonObject);
      return specificCompanionFilter;
    }

    public static SpecificCompanionFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpecificCompanionFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpecificCompanionFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      JArray jarray = Serialization.JsonArray(jsonObject, "companions");
      this.m_companions = new List<Id<CompanionDefinition>>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_companions.Add(Serialization.JsonTokenIdValue<CompanionDefinition>(token));
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      foreach (IEntity entity in entities)
      {
        if (this.ValidFor(entity))
          yield return entity;
      }
    }

    public bool ValidFor(IEntity entity)
    {
      if (!(entity is CompanionStatus companionStatus))
        return false;
      bool flag = this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      return this.Contains(companionStatus.definition.id) == flag;
    }

    private bool Contains(int id)
    {
      int count = this.m_companions.Count;
      for (int index = 0; index < count; ++index)
      {
        if (id == this.m_companions[index].value)
          return true;
      }
      return false;
    }
  }
}
