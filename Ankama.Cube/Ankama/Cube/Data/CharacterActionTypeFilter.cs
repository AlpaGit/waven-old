// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CharacterActionTypeFilter
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
  public sealed class CharacterActionTypeFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<ActionType> m_actionTypes;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<ActionType> actionTypes => (IReadOnlyList<ActionType>) this.m_actionTypes;

    public override string ToString()
    {
      switch (this.m_actionTypes.Count)
      {
        case 0:
          return "actionType <unset>";
        case 1:
          return string.Format("actionType {0} {1}", (object) this.condition, (object) this.m_actionTypes[0]);
        default:
          return string.Format("actionType {0} \n - ", (object) this.condition) + string.Join<ActionType>("\n - ", (IEnumerable<ActionType>) this.m_actionTypes);
      }
    }

    public static CharacterActionTypeFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CharacterActionTypeFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CharacterActionTypeFilter actionTypeFilter = new CharacterActionTypeFilter();
      actionTypeFilter.PopulateFromJson(jsonObject);
      return actionTypeFilter;
    }

    public static CharacterActionTypeFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CharacterActionTypeFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CharacterActionTypeFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      this.m_actionTypes = Serialization.JsonArrayAsList<ActionType>(jsonObject, "actionTypes");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      bool shouldContains = this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithAction entityWithAction && this.ContainsEntityType(entityWithAction.actionType) == shouldContains)
          yield return entity;
      }
    }

    private bool ContainsEntityType(ActionType actionType)
    {
      int count = this.m_actionTypes.Count;
      for (int index = 0; index < count; ++index)
      {
        if (actionType == this.m_actionTypes[index])
          return true;
      }
      return false;
    }
  }
}
