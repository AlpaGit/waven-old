// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpecificObjectMechanismFilter
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
  public sealed class SpecificObjectMechanismFilter : 
    IEditableContent,
    ISpecificEntityFilter,
    IEntityFilter,
    ITargetFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<Id<ObjectMechanismDefinition>> m_objectMechanisms;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<Id<ObjectMechanismDefinition>> objectMechanisms => (IReadOnlyList<Id<ObjectMechanismDefinition>>) this.m_objectMechanisms;

    public override string ToString()
    {
      switch (this.m_objectMechanisms.Count)
      {
        case 0:
          return "objectMechanism is <unset>";
        case 1:
          return string.Format("objectMechanism {0} {1}", (object) this.condition, (object) this.m_objectMechanisms[0]);
        default:
          return string.Format("objectMechanism {0} \n - ", (object) this.condition) + string.Join<Id<ObjectMechanismDefinition>>("\n - ", (IEnumerable<Id<ObjectMechanismDefinition>>) this.m_objectMechanisms);
      }
    }

    public static SpecificObjectMechanismFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpecificObjectMechanismFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpecificObjectMechanismFilter objectMechanismFilter = new SpecificObjectMechanismFilter();
      objectMechanismFilter.PopulateFromJson(jsonObject);
      return objectMechanismFilter;
    }

    public static SpecificObjectMechanismFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpecificObjectMechanismFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpecificObjectMechanismFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      JArray jarray = Serialization.JsonArray(jsonObject, "objectMechanisms");
      this.m_objectMechanisms = new List<Id<ObjectMechanismDefinition>>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_objectMechanisms.Add(Serialization.JsonTokenIdValue<ObjectMechanismDefinition>(token));
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
      if (!(entity is ObjectMechanismStatus objectMechanismStatus))
        return false;
      bool flag = this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      return this.Contains(objectMechanismStatus.definition.id) == flag;
    }

    private bool Contains(int id)
    {
      int count = this.m_objectMechanisms.Count;
      for (int index = 0; index < count; ++index)
      {
        if (id == this.m_objectMechanisms[index].value)
          return true;
      }
      return false;
    }
  }
}
