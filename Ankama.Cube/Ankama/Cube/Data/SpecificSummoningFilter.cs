// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpecificSummoningFilter
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
  public sealed class SpecificSummoningFilter : 
    IEditableContent,
    ISpecificEntityFilter,
    IEntityFilter,
    ITargetFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<Id<SummoningDefinition>> m_summonings;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<Id<SummoningDefinition>> summonings => (IReadOnlyList<Id<SummoningDefinition>>) this.m_summonings;

    public override string ToString()
    {
      switch (this.m_summonings.Count)
      {
        case 0:
          return "summoning is <unset>";
        case 1:
          return string.Format("summoning {0} {1}", (object) this.condition, (object) this.m_summonings[0]);
        default:
          return string.Format("summoning {0} \n - ", (object) this.condition) + string.Join<Id<SummoningDefinition>>("\n - ", (IEnumerable<Id<SummoningDefinition>>) this.m_summonings);
      }
    }

    public static SpecificSummoningFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpecificSummoningFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpecificSummoningFilter specificSummoningFilter = new SpecificSummoningFilter();
      specificSummoningFilter.PopulateFromJson(jsonObject);
      return specificSummoningFilter;
    }

    public static SpecificSummoningFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpecificSummoningFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpecificSummoningFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      JArray jarray = Serialization.JsonArray(jsonObject, "summonings");
      this.m_summonings = new List<Id<SummoningDefinition>>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_summonings.Add(Serialization.JsonTokenIdValue<SummoningDefinition>(token));
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
      if (!(entity is SummoningStatus summoningStatus))
        return false;
      bool flag = this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      return this.Contains(summoningStatus.definition.id) == flag;
    }

    private bool Contains(int id)
    {
      int count = this.m_summonings.Count;
      for (int index = 0; index < count; ++index)
      {
        if (id == this.m_summonings[index].value)
          return true;
      }
      return false;
    }
  }
}
