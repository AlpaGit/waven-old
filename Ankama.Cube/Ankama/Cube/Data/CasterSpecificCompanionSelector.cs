// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CasterSpecificCompanionSelector
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
  public sealed class CasterSpecificCompanionSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleEntitySelector,
    IEntitySelector,
    ISingleTargetSelector
  {
    private Id<CompanionDefinition> m_companion;

    public Id<CompanionDefinition> companion => this.m_companion;

    public override string ToString()
    {
      if (this.m_companion == (Id<CompanionDefinition>) null)
        return "<not defined>";
      return ObjectReference.GetCompanion(this.m_companion.value)?.idAndName;
    }

    public static CasterSpecificCompanionSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CasterSpecificCompanionSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CasterSpecificCompanionSelector companionSelector = new CasterSpecificCompanionSelector();
      companionSelector.PopulateFromJson(jsonObject);
      return companionSelector;
    }

    public static CasterSpecificCompanionSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CasterSpecificCompanionSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CasterSpecificCompanionSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_companion = Serialization.JsonTokenIdValue<CompanionDefinition>(jsonObject, "companion");

    public IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      IEntity entity;
      if (this.TryGetEntity<IEntity>(context, out entity))
        yield return entity;
    }

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      if (context is DynamicValueFightContext valueFightContext)
      {
        foreach (T enumerateEntity in valueFightContext.fightStatus.EnumerateEntities<T>())
        {
          if (enumerateEntity is CompanionStatus companionStatus && companionStatus.ownerId == valueFightContext.playerId && companionStatus.definition.id == this.m_companion.value)
          {
            entity = enumerateEntity;
            return true;
          }
        }
      }
      entity = default (T);
      return false;
    }
  }
}
