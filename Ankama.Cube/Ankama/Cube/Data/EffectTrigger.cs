// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EffectTrigger
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class EffectTrigger : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static EffectTrigger FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EffectTrigger) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class EffectTrigger");
        return (EffectTrigger) null;
      }
      string str = jtoken.Value<string>();
      EffectTrigger effectTrigger;
      switch (str)
      {
        case "AfterEntityRemoval":
          effectTrigger = (EffectTrigger) new EffectTrigger.AfterEntityRemoval();
          break;
        case "AfterFloorMechanismActivation":
          effectTrigger = (EffectTrigger) new EffectTrigger.AfterFloorMechanismActivation();
          break;
        case "AfterOtherSpellPlayed":
          effectTrigger = (EffectTrigger) new EffectTrigger.AfterOtherSpellPlayed();
          break;
        case "AfterOtherSpellPlayedAndExecuted":
          effectTrigger = (EffectTrigger) new EffectTrigger.AfterOtherSpellPlayedAndExecuted();
          break;
        case "AfterThisSpellPlayedAndExecuted":
          effectTrigger = (EffectTrigger) new EffectTrigger.AfterThisSpellPlayedAndExecuted();
          break;
        case "ConditionalTrigger":
          effectTrigger = (EffectTrigger) new EffectTrigger.ConditionalTrigger();
          break;
        case "EntityRemoval":
          effectTrigger = (EffectTrigger) new EffectTrigger.EntityRemoval();
          break;
        case "OnArmorChanged":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnArmorChanged();
          break;
        case "OnAssemblingTrigger":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnAssemblingTrigger();
          break;
        case "OnCaracTheft":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnCaracTheft();
          break;
        case "OnCharacterAdded":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnCharacterAdded();
          break;
        case "OnCompanionTransfered":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnCompanionTransfered();
          break;
        case "OnDiceThrown":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnDiceThrown();
          break;
        case "OnElementaryStateApplied":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnElementaryStateApplied();
          break;
        case "OnEntityAction":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityAction();
          break;
        case "OnEntityCharged":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityCharged();
          break;
        case "OnEntityCrossedOver":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityCrossedOver();
          break;
        case "OnEntityFinishMoveIntoArea":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityFinishMoveIntoArea();
          break;
        case "OnEntityFullMovementDoneTrigger":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityFullMovementDoneTrigger();
          break;
        case "OnEntityHealed":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityHealed();
          break;
        case "OnEntityHurt":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityHurt();
          break;
        case "OnEntityRemainingLifeChanged":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityRemainingLifeChanged();
          break;
        case "OnEntityStartMoveFromArea":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnEntityStartMoveFromArea();
          break;
        case "OnExplosion":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnExplosion();
          break;
        case "OnFloatingCounterOfEffectHolderTerminated":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnFloatingCounterOfEffectHolderTerminated();
          break;
        case "OnFloorMechanismActivation":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnFloorMechanismActivation();
          break;
        case "OnMechanismAdded":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnMechanismAdded();
          break;
        case "OnPropertyApplied":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnPropertyApplied();
          break;
        case "OnReservePointChanged":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnReservePointChanged();
          break;
        case "OnReserveUsed":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnReserveUsed();
          break;
        case "OnSpecificEventTrigger":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnSpecificEventTrigger();
          break;
        case "OnSpellDrawn":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnSpellDrawn();
          break;
        case "OnSquadChanged":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnSquadChanged();
          break;
        case "OnTeamsInitialized":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnTeamsInitialized();
          break;
        case "OnThisEffectStored":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnThisEffectStored();
          break;
        case "OnTurnEnded":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnTurnEnded();
          break;
        case "OnTurnStarted":
          effectTrigger = (EffectTrigger) new EffectTrigger.OnTurnStarted();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (EffectTrigger) null;
      }
      effectTrigger.PopulateFromJson(jsonObject);
      return effectTrigger;
    }

    public static EffectTrigger FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EffectTrigger defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    [Serializable]
    public sealed class AfterEntityRemoval : EffectTrigger
    {
      private IEntityFilter m_removedValidator;
      private IEntityFilter m_removerValidator;
      private bool? m_onlyIfRemovedByDeath;
      private bool? m_removedByThisSpell;

      public IEntityFilter removedValidator => this.m_removedValidator;

      public IEntityFilter removerValidator => this.m_removerValidator;

      public bool? onlyIfRemovedByDeath => this.m_onlyIfRemovedByDeath;

      public bool? removedByThisSpell => this.m_removedByThisSpell;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.AfterEntityRemoval FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.AfterEntityRemoval) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.AfterEntityRemoval afterEntityRemoval = new EffectTrigger.AfterEntityRemoval();
        afterEntityRemoval.PopulateFromJson(jsonObject);
        return afterEntityRemoval;
      }

      public static EffectTrigger.AfterEntityRemoval FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.AfterEntityRemoval defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.AfterEntityRemoval.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_removedValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "removedValidator");
        this.m_removerValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "removerValidator");
        this.m_onlyIfRemovedByDeath = Serialization.JsonTokenValue<bool?>(jsonObject, "onlyIfRemovedByDeath");
        this.m_removedByThisSpell = Serialization.JsonTokenValue<bool?>(jsonObject, "removedByThisSpell");
      }
    }

    [Serializable]
    public sealed class AfterFloorMechanismActivation : EffectTrigger
    {
      private IEntityFilter m_activatedValidator;
      private IEntityFilter m_activatorValidator;

      public IEntityFilter activatedValidator => this.m_activatedValidator;

      public IEntityFilter activatorValidator => this.m_activatorValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.AfterFloorMechanismActivation FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.AfterFloorMechanismActivation) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.AfterFloorMechanismActivation mechanismActivation = new EffectTrigger.AfterFloorMechanismActivation();
        mechanismActivation.PopulateFromJson(jsonObject);
        return mechanismActivation;
      }

      public static EffectTrigger.AfterFloorMechanismActivation FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.AfterFloorMechanismActivation defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.AfterFloorMechanismActivation.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_activatedValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "activatedValidator");
        this.m_activatorValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "activatorValidator");
      }
    }

    [Serializable]
    public sealed class AfterOtherSpellPlayed : EffectTrigger
    {
      private OwnerFilter m_playedBy;
      private List<SpellFilter> m_spellFilters;
      private ITargetFilter m_playedOn;

      public OwnerFilter playedBy => this.m_playedBy;

      public IReadOnlyList<SpellFilter> spellFilters => (IReadOnlyList<SpellFilter>) this.m_spellFilters;

      public ITargetFilter playedOn => this.m_playedOn;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.AfterOtherSpellPlayed FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.AfterOtherSpellPlayed) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.AfterOtherSpellPlayed otherSpellPlayed = new EffectTrigger.AfterOtherSpellPlayed();
        otherSpellPlayed.PopulateFromJson(jsonObject);
        return otherSpellPlayed;
      }

      public static EffectTrigger.AfterOtherSpellPlayed FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.AfterOtherSpellPlayed defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.AfterOtherSpellPlayed.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_playedBy = OwnerFilter.FromJsonProperty(jsonObject, "playedBy");
        JArray jarray = Serialization.JsonArray(jsonObject, "spellFilters");
        this.m_spellFilters = new List<SpellFilter>(jarray != null ? jarray.Count : 0);
        if (jarray != null)
        {
          foreach (JToken token in jarray)
            this.m_spellFilters.Add(SpellFilter.FromJsonToken(token));
        }
        this.m_playedOn = ITargetFilterUtils.FromJsonProperty(jsonObject, "playedOn");
      }
    }

    [Serializable]
    public sealed class AfterOtherSpellPlayedAndExecuted : EffectTrigger
    {
      private OwnerFilter m_playedBy;
      private List<SpellFilter> m_spellFilters;
      private ITargetFilter m_playedOn;

      public OwnerFilter playedBy => this.m_playedBy;

      public IReadOnlyList<SpellFilter> spellFilters => (IReadOnlyList<SpellFilter>) this.m_spellFilters;

      public ITargetFilter playedOn => this.m_playedOn;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.AfterOtherSpellPlayedAndExecuted FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.AfterOtherSpellPlayedAndExecuted) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.AfterOtherSpellPlayedAndExecuted playedAndExecuted = new EffectTrigger.AfterOtherSpellPlayedAndExecuted();
        playedAndExecuted.PopulateFromJson(jsonObject);
        return playedAndExecuted;
      }

      public static EffectTrigger.AfterOtherSpellPlayedAndExecuted FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.AfterOtherSpellPlayedAndExecuted defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.AfterOtherSpellPlayedAndExecuted.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_playedBy = OwnerFilter.FromJsonProperty(jsonObject, "playedBy");
        JArray jarray = Serialization.JsonArray(jsonObject, "spellFilters");
        this.m_spellFilters = new List<SpellFilter>(jarray != null ? jarray.Count : 0);
        if (jarray != null)
        {
          foreach (JToken token in jarray)
            this.m_spellFilters.Add(SpellFilter.FromJsonToken(token));
        }
        this.m_playedOn = ITargetFilterUtils.FromJsonProperty(jsonObject, "playedOn");
      }
    }

    [Serializable]
    public sealed class AfterThisSpellPlayedAndExecuted : EffectTrigger
    {
      private ITargetFilter m_playedOn;

      public ITargetFilter playedOn => this.m_playedOn;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.AfterThisSpellPlayedAndExecuted FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.AfterThisSpellPlayedAndExecuted) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.AfterThisSpellPlayedAndExecuted playedAndExecuted = new EffectTrigger.AfterThisSpellPlayedAndExecuted();
        playedAndExecuted.PopulateFromJson(jsonObject);
        return playedAndExecuted;
      }

      public static EffectTrigger.AfterThisSpellPlayedAndExecuted FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.AfterThisSpellPlayedAndExecuted defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.AfterThisSpellPlayedAndExecuted.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_playedOn = ITargetFilterUtils.FromJsonProperty(jsonObject, "playedOn");
      }
    }

    [Serializable]
    public sealed class ConditionalTrigger : EffectTrigger
    {
      private EffectCondition m_condition;
      private List<EffectTrigger> m_triggers;

      public EffectCondition condition => this.m_condition;

      public IReadOnlyList<EffectTrigger> triggers => (IReadOnlyList<EffectTrigger>) this.m_triggers;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.ConditionalTrigger FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.ConditionalTrigger) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.ConditionalTrigger conditionalTrigger = new EffectTrigger.ConditionalTrigger();
        conditionalTrigger.PopulateFromJson(jsonObject);
        return conditionalTrigger;
      }

      public static EffectTrigger.ConditionalTrigger FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.ConditionalTrigger defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.ConditionalTrigger.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_condition = EffectCondition.FromJsonProperty(jsonObject, "condition");
        JArray jarray = Serialization.JsonArray(jsonObject, "triggers");
        this.m_triggers = new List<EffectTrigger>(jarray != null ? jarray.Count : 0);
        if (jarray == null)
          return;
        foreach (JToken token in jarray)
          this.m_triggers.Add(EffectTrigger.FromJsonToken(token));
      }
    }

    [Serializable]
    public sealed class EntityRemoval : EffectTrigger
    {
      private IEntityFilter m_removedValidator;
      private IEntityFilter m_removerValidator;
      private bool? m_onlyIfRemovedByDeath;
      private bool? m_removedByThisSpell;

      public IEntityFilter removedValidator => this.m_removedValidator;

      public IEntityFilter removerValidator => this.m_removerValidator;

      public bool? onlyIfRemovedByDeath => this.m_onlyIfRemovedByDeath;

      public bool? removedByThisSpell => this.m_removedByThisSpell;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.EntityRemoval FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.EntityRemoval) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.EntityRemoval entityRemoval = new EffectTrigger.EntityRemoval();
        entityRemoval.PopulateFromJson(jsonObject);
        return entityRemoval;
      }

      public static EffectTrigger.EntityRemoval FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.EntityRemoval defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.EntityRemoval.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_removedValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "removedValidator");
        this.m_removerValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "removerValidator");
        this.m_onlyIfRemovedByDeath = Serialization.JsonTokenValue<bool?>(jsonObject, "onlyIfRemovedByDeath");
        this.m_removedByThisSpell = Serialization.JsonTokenValue<bool?>(jsonObject, "removedByThisSpell");
      }
    }

    [Serializable]
    public sealed class OnArmorChanged : EffectTrigger
    {
      private IEntityFilter m_targetValidator;
      private IEntityFilter m_sourceValidator;
      private ValueFilter m_modification;
      private ValueFilter m_newValue;

      public IEntityFilter targetValidator => this.m_targetValidator;

      public IEntityFilter sourceValidator => this.m_sourceValidator;

      public ValueFilter modification => this.m_modification;

      public ValueFilter newValue => this.m_newValue;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnArmorChanged FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnArmorChanged) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnArmorChanged onArmorChanged = new EffectTrigger.OnArmorChanged();
        onArmorChanged.PopulateFromJson(jsonObject);
        return onArmorChanged;
      }

      public static EffectTrigger.OnArmorChanged FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnArmorChanged defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnArmorChanged.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targetValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "targetValidator");
        this.m_sourceValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "sourceValidator");
        this.m_modification = ValueFilter.FromJsonProperty(jsonObject, "modification");
        this.m_newValue = ValueFilter.FromJsonProperty(jsonObject, "newValue");
      }
    }

    [Serializable]
    public sealed class OnAssemblingTrigger : EffectTrigger
    {
      private ITargetSelector m_triggeredBy;
      private ValueFilter m_assemblingSize;

      public ITargetSelector triggeredBy => this.m_triggeredBy;

      public ValueFilter assemblingSize => this.m_assemblingSize;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnAssemblingTrigger FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnAssemblingTrigger) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnAssemblingTrigger assemblingTrigger = new EffectTrigger.OnAssemblingTrigger();
        assemblingTrigger.PopulateFromJson(jsonObject);
        return assemblingTrigger;
      }

      public static EffectTrigger.OnAssemblingTrigger FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnAssemblingTrigger defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnAssemblingTrigger.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_triggeredBy = ITargetSelectorUtils.FromJsonProperty(jsonObject, "triggeredBy");
        this.m_assemblingSize = ValueFilter.FromJsonProperty(jsonObject, "assemblingSize");
      }
    }

    [Serializable]
    public sealed class OnCaracTheft : EffectTrigger
    {
      private OwnerFilter m_thief;
      private OwnerFilter m_robbed;
      private ListComparison m_comparison;
      private List<CaracId> m_carac;
      private ValueFilter m_quantity;

      public OwnerFilter thief => this.m_thief;

      public OwnerFilter robbed => this.m_robbed;

      public ListComparison comparison => this.m_comparison;

      public IReadOnlyList<CaracId> carac => (IReadOnlyList<CaracId>) this.m_carac;

      public ValueFilter quantity => this.m_quantity;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnCaracTheft FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnCaracTheft) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnCaracTheft onCaracTheft = new EffectTrigger.OnCaracTheft();
        onCaracTheft.PopulateFromJson(jsonObject);
        return onCaracTheft;
      }

      public static EffectTrigger.OnCaracTheft FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnCaracTheft defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnCaracTheft.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_thief = OwnerFilter.FromJsonProperty(jsonObject, "thief");
        this.m_robbed = OwnerFilter.FromJsonProperty(jsonObject, "robbed");
        this.m_comparison = (ListComparison) Serialization.JsonTokenValue<int>(jsonObject, "comparison", 2);
        this.m_carac = Serialization.JsonArrayAsList<CaracId>(jsonObject, "carac");
        this.m_quantity = ValueFilter.FromJsonProperty(jsonObject, "quantity");
      }
    }

    [Serializable]
    public sealed class OnCharacterAdded : EffectTrigger
    {
      private bool m_evenIfTransformation;
      private List<IEntityFilter> m_addedCharacterValidator;

      public bool evenIfTransformation => this.m_evenIfTransformation;

      public IReadOnlyList<IEntityFilter> addedCharacterValidator => (IReadOnlyList<IEntityFilter>) this.m_addedCharacterValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnCharacterAdded FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnCharacterAdded) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnCharacterAdded onCharacterAdded = new EffectTrigger.OnCharacterAdded();
        onCharacterAdded.PopulateFromJson(jsonObject);
        return onCharacterAdded;
      }

      public static EffectTrigger.OnCharacterAdded FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnCharacterAdded defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnCharacterAdded.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_evenIfTransformation = Serialization.JsonTokenValue<bool>(jsonObject, "evenIfTransformation");
        JArray jarray = Serialization.JsonArray(jsonObject, "addedCharacterValidator");
        this.m_addedCharacterValidator = new List<IEntityFilter>(jarray != null ? jarray.Count : 0);
        if (jarray == null)
          return;
        foreach (JToken token in jarray)
          this.m_addedCharacterValidator.Add(IEntityFilterUtils.FromJsonToken(token));
      }
    }

    [Serializable]
    public sealed class OnCompanionTransfered : EffectTrigger
    {
      private OwnerFilter m_player;

      public OwnerFilter player => this.m_player;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnCompanionTransfered FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnCompanionTransfered) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnCompanionTransfered companionTransfered = new EffectTrigger.OnCompanionTransfered();
        companionTransfered.PopulateFromJson(jsonObject);
        return companionTransfered;
      }

      public static EffectTrigger.OnCompanionTransfered FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnCompanionTransfered defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnCompanionTransfered.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_player = OwnerFilter.FromJsonProperty(jsonObject, "player");
      }
    }

    [Serializable]
    public sealed class OnDiceThrown : EffectTrigger
    {
      private ValueFilter m_valueCheck;
      private IEntityFilter m_throwerCheck;
      private bool? m_thrownByThisSpell;
      private bool? m_thrownByThisCharacterAction;

      public ValueFilter valueCheck => this.m_valueCheck;

      public IEntityFilter throwerCheck => this.m_throwerCheck;

      public bool? thrownByThisSpell => this.m_thrownByThisSpell;

      public bool? thrownByThisCharacterAction => this.m_thrownByThisCharacterAction;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnDiceThrown FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnDiceThrown) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnDiceThrown onDiceThrown = new EffectTrigger.OnDiceThrown();
        onDiceThrown.PopulateFromJson(jsonObject);
        return onDiceThrown;
      }

      public static EffectTrigger.OnDiceThrown FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnDiceThrown defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnDiceThrown.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_valueCheck = ValueFilter.FromJsonProperty(jsonObject, "valueCheck");
        this.m_throwerCheck = IEntityFilterUtils.FromJsonProperty(jsonObject, "throwerCheck");
        this.m_thrownByThisSpell = Serialization.JsonTokenValue<bool?>(jsonObject, "thrownByThisSpell");
        this.m_thrownByThisCharacterAction = Serialization.JsonTokenValue<bool?>(jsonObject, "thrownByThisCharacterAction");
      }
    }

    [Serializable]
    public sealed class OnElementaryStateApplied : EffectTrigger
    {
      private IEntityFilter m_targetValidator;
      private IEntityFilter m_casterValidator;
      private ListComparison m_comparison;
      private List<ElementaryStates> m_states;

      public IEntityFilter targetValidator => this.m_targetValidator;

      public IEntityFilter casterValidator => this.m_casterValidator;

      public ListComparison comparison => this.m_comparison;

      public IReadOnlyList<ElementaryStates> states => (IReadOnlyList<ElementaryStates>) this.m_states;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnElementaryStateApplied FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnElementaryStateApplied) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnElementaryStateApplied elementaryStateApplied = new EffectTrigger.OnElementaryStateApplied();
        elementaryStateApplied.PopulateFromJson(jsonObject);
        return elementaryStateApplied;
      }

      public static EffectTrigger.OnElementaryStateApplied FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnElementaryStateApplied defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnElementaryStateApplied.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targetValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "targetValidator");
        this.m_casterValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "casterValidator");
        this.m_comparison = (ListComparison) Serialization.JsonTokenValue<int>(jsonObject, "comparison", 2);
        this.m_states = Serialization.JsonArrayAsList<ElementaryStates>(jsonObject, "states");
      }
    }

    [Serializable]
    public sealed class OnEntityAction : EffectTrigger
    {
      private bool m_afterAction;
      private IEntityFilter m_actionSrc;
      private IEntityFilter m_actionTarget;
      private bool? m_hasMovedBeforeAction;
      private ActionType? m_actionType;

      public bool afterAction => this.m_afterAction;

      public IEntityFilter actionSrc => this.m_actionSrc;

      public IEntityFilter actionTarget => this.m_actionTarget;

      public bool? hasMovedBeforeAction => this.m_hasMovedBeforeAction;

      public ActionType? actionType => this.m_actionType;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityAction FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityAction) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityAction onEntityAction = new EffectTrigger.OnEntityAction();
        onEntityAction.PopulateFromJson(jsonObject);
        return onEntityAction;
      }

      public static EffectTrigger.OnEntityAction FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityAction defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityAction.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_afterAction = Serialization.JsonTokenValue<bool>(jsonObject, "afterAction", true);
        this.m_actionSrc = IEntityFilterUtils.FromJsonProperty(jsonObject, "actionSrc");
        this.m_actionTarget = IEntityFilterUtils.FromJsonProperty(jsonObject, "actionTarget");
        this.m_hasMovedBeforeAction = Serialization.JsonTokenValue<bool?>(jsonObject, "hasMovedBeforeAction");
        int? nullable = Serialization.JsonTokenValue<int?>(jsonObject, "actionType");
        this.m_actionType = nullable.HasValue ? new ActionType?((ActionType) nullable.GetValueOrDefault()) : new ActionType?();
      }
    }

    [Serializable]
    public sealed class OnEntityCharged : EffectTrigger
    {
      private IEntitySelector m_entityValidator;
      private ValueFilter m_chargeLengthValidator;

      public IEntitySelector entityValidator => this.m_entityValidator;

      public ValueFilter chargeLengthValidator => this.m_chargeLengthValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityCharged FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityCharged) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityCharged onEntityCharged = new EffectTrigger.OnEntityCharged();
        onEntityCharged.PopulateFromJson(jsonObject);
        return onEntityCharged;
      }

      public static EffectTrigger.OnEntityCharged FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityCharged defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityCharged.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_entityValidator = IEntitySelectorUtils.FromJsonProperty(jsonObject, "entityValidator");
        this.m_chargeLengthValidator = ValueFilter.FromJsonProperty(jsonObject, "chargeLengthValidator");
      }
    }

    [Serializable]
    public sealed class OnEntityCrossedOver : EffectTrigger
    {
      private IEntityFilter m_targetValidator;
      private IEntityFilter m_sourceValidator;

      public IEntityFilter targetValidator => this.m_targetValidator;

      public IEntityFilter sourceValidator => this.m_sourceValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityCrossedOver FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityCrossedOver) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityCrossedOver entityCrossedOver = new EffectTrigger.OnEntityCrossedOver();
        entityCrossedOver.PopulateFromJson(jsonObject);
        return entityCrossedOver;
      }

      public static EffectTrigger.OnEntityCrossedOver FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityCrossedOver defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityCrossedOver.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targetValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "targetValidator");
        this.m_sourceValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "sourceValidator");
      }
    }

    [Serializable]
    public sealed class OnEntityFinishMoveIntoArea : EffectTrigger
    {
      private IEntityFilter m_movedEntityValidator;
      private ITargetSelector m_area;
      private int m_maxDistanceToArea;

      public IEntityFilter movedEntityValidator => this.m_movedEntityValidator;

      public ITargetSelector area => this.m_area;

      public int maxDistanceToArea => this.m_maxDistanceToArea;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityFinishMoveIntoArea FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityFinishMoveIntoArea) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityFinishMoveIntoArea finishMoveIntoArea = new EffectTrigger.OnEntityFinishMoveIntoArea();
        finishMoveIntoArea.PopulateFromJson(jsonObject);
        return finishMoveIntoArea;
      }

      public static EffectTrigger.OnEntityFinishMoveIntoArea FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityFinishMoveIntoArea defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityFinishMoveIntoArea.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_movedEntityValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "movedEntityValidator");
        this.m_area = ITargetSelectorUtils.FromJsonProperty(jsonObject, "area");
        this.m_maxDistanceToArea = Serialization.JsonTokenValue<int>(jsonObject, "maxDistanceToArea");
      }
    }

    [Serializable]
    public sealed class OnEntityFullMovementDoneTrigger : EffectTrigger
    {
      private IEntityFilter m_entityValidator;
      private List<MovementType> m_movementTypes;
      private ValueFilter m_movementSize;
      private YesNoWhatever m_requiresSpellCast;

      public IEntityFilter entityValidator => this.m_entityValidator;

      public IReadOnlyList<MovementType> movementTypes => (IReadOnlyList<MovementType>) this.m_movementTypes;

      public ValueFilter movementSize => this.m_movementSize;

      public YesNoWhatever requiresSpellCast => this.m_requiresSpellCast;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityFullMovementDoneTrigger FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityFullMovementDoneTrigger) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityFullMovementDoneTrigger movementDoneTrigger = new EffectTrigger.OnEntityFullMovementDoneTrigger();
        movementDoneTrigger.PopulateFromJson(jsonObject);
        return movementDoneTrigger;
      }

      public static EffectTrigger.OnEntityFullMovementDoneTrigger FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityFullMovementDoneTrigger defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityFullMovementDoneTrigger.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_entityValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "entityValidator");
        this.m_movementTypes = Serialization.JsonArrayAsList<MovementType>(jsonObject, "movementTypes");
        this.m_movementSize = ValueFilter.FromJsonProperty(jsonObject, "movementSize");
        this.m_requiresSpellCast = (YesNoWhatever) Serialization.JsonTokenValue<int>(jsonObject, "requiresSpellCast");
      }
    }

    [Serializable]
    public sealed class OnEntityHealed : EffectTrigger
    {
      private IEntityFilter m_targetValidator;
      private IEntityFilter m_sourceValidator;

      public IEntityFilter targetValidator => this.m_targetValidator;

      public IEntityFilter sourceValidator => this.m_sourceValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityHealed FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityHealed) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityHealed onEntityHealed = new EffectTrigger.OnEntityHealed();
        onEntityHealed.PopulateFromJson(jsonObject);
        return onEntityHealed;
      }

      public static EffectTrigger.OnEntityHealed FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityHealed defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityHealed.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targetValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "targetValidator");
        this.m_sourceValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "sourceValidator");
      }
    }

    [Serializable]
    public sealed class OnEntityHurt : EffectTrigger
    {
      private IEntityFilter m_targetValidator;
      private IEntityFilter m_sourceValidator;

      public IEntityFilter targetValidator => this.m_targetValidator;

      public IEntityFilter sourceValidator => this.m_sourceValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityHurt FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityHurt) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityHurt onEntityHurt = new EffectTrigger.OnEntityHurt();
        onEntityHurt.PopulateFromJson(jsonObject);
        return onEntityHurt;
      }

      public static EffectTrigger.OnEntityHurt FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityHurt defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityHurt.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targetValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "targetValidator");
        this.m_sourceValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "sourceValidator");
      }
    }

    [Serializable]
    public sealed class OnEntityRemainingLifeChanged : EffectTrigger
    {
      private ISingleEntitySelector m_entity;
      private ValueFilter m_remainingLifePercent;

      public ISingleEntitySelector entity => this.m_entity;

      public ValueFilter remainingLifePercent => this.m_remainingLifePercent;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityRemainingLifeChanged FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityRemainingLifeChanged) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityRemainingLifeChanged remainingLifeChanged = new EffectTrigger.OnEntityRemainingLifeChanged();
        remainingLifeChanged.PopulateFromJson(jsonObject);
        return remainingLifeChanged;
      }

      public static EffectTrigger.OnEntityRemainingLifeChanged FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityRemainingLifeChanged defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityRemainingLifeChanged.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_entity = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "entity");
        this.m_remainingLifePercent = ValueFilter.FromJsonProperty(jsonObject, "remainingLifePercent");
      }
    }

    [Serializable]
    public sealed class OnEntityStartMoveFromArea : EffectTrigger
    {
      private IEntityFilter m_movedEntityValidator;
      private ITargetSelector m_area;
      private bool m_allowReenter;
      private int m_maxDistanceToArea;

      public IEntityFilter movedEntityValidator => this.m_movedEntityValidator;

      public ITargetSelector area => this.m_area;

      public bool allowReenter => this.m_allowReenter;

      public int maxDistanceToArea => this.m_maxDistanceToArea;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnEntityStartMoveFromArea FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnEntityStartMoveFromArea) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnEntityStartMoveFromArea startMoveFromArea = new EffectTrigger.OnEntityStartMoveFromArea();
        startMoveFromArea.PopulateFromJson(jsonObject);
        return startMoveFromArea;
      }

      public static EffectTrigger.OnEntityStartMoveFromArea FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnEntityStartMoveFromArea defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnEntityStartMoveFromArea.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_movedEntityValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "movedEntityValidator");
        this.m_area = ITargetSelectorUtils.FromJsonProperty(jsonObject, "area");
        this.m_allowReenter = Serialization.JsonTokenValue<bool>(jsonObject, "allowReenter", true);
        this.m_maxDistanceToArea = Serialization.JsonTokenValue<int>(jsonObject, "maxDistanceToArea");
      }
    }

    [Serializable]
    public sealed class OnExplosion : EffectTrigger
    {
      private IEntityFilter m_from;
      private bool m_beforeDamage;

      public IEntityFilter from => this.m_from;

      public bool beforeDamage => this.m_beforeDamage;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnExplosion FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnExplosion) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnExplosion onExplosion = new EffectTrigger.OnExplosion();
        onExplosion.PopulateFromJson(jsonObject);
        return onExplosion;
      }

      public static EffectTrigger.OnExplosion FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnExplosion defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnExplosion.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_from = IEntityFilterUtils.FromJsonProperty(jsonObject, "from");
        this.m_beforeDamage = Serialization.JsonTokenValue<bool>(jsonObject, "beforeDamage", true);
      }
    }

    [Serializable]
    public sealed class OnFloatingCounterOfEffectHolderTerminated : EffectTrigger
    {
      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnFloatingCounterOfEffectHolderTerminated FromJsonToken(
        JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnFloatingCounterOfEffectHolderTerminated) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnFloatingCounterOfEffectHolderTerminated holderTerminated = new EffectTrigger.OnFloatingCounterOfEffectHolderTerminated();
        holderTerminated.PopulateFromJson(jsonObject);
        return holderTerminated;
      }

      public static EffectTrigger.OnFloatingCounterOfEffectHolderTerminated FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnFloatingCounterOfEffectHolderTerminated defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnFloatingCounterOfEffectHolderTerminated.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class OnFloorMechanismActivation : EffectTrigger
    {
      private IEntityFilter m_activatedValidator;
      private IEntityFilter m_activatorValidator;

      public IEntityFilter activatedValidator => this.m_activatedValidator;

      public IEntityFilter activatorValidator => this.m_activatorValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnFloorMechanismActivation FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnFloorMechanismActivation) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnFloorMechanismActivation mechanismActivation = new EffectTrigger.OnFloorMechanismActivation();
        mechanismActivation.PopulateFromJson(jsonObject);
        return mechanismActivation;
      }

      public static EffectTrigger.OnFloorMechanismActivation FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnFloorMechanismActivation defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnFloorMechanismActivation.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_activatedValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "activatedValidator");
        this.m_activatorValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "activatorValidator");
      }
    }

    [Serializable]
    public sealed class OnMechanismAdded : EffectTrigger
    {
      private List<IEntityFilter> m_addedMechanismValidator;

      public IReadOnlyList<IEntityFilter> addedMechanismValidator => (IReadOnlyList<IEntityFilter>) this.m_addedMechanismValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnMechanismAdded FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnMechanismAdded) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnMechanismAdded onMechanismAdded = new EffectTrigger.OnMechanismAdded();
        onMechanismAdded.PopulateFromJson(jsonObject);
        return onMechanismAdded;
      }

      public static EffectTrigger.OnMechanismAdded FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnMechanismAdded defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnMechanismAdded.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        JArray jarray = Serialization.JsonArray(jsonObject, "addedMechanismValidator");
        this.m_addedMechanismValidator = new List<IEntityFilter>(jarray != null ? jarray.Count : 0);
        if (jarray == null)
          return;
        foreach (JToken token in jarray)
          this.m_addedMechanismValidator.Add(IEntityFilterUtils.FromJsonToken(token));
      }
    }

    [Serializable]
    public sealed class OnPropertyApplied : EffectTrigger
    {
      private IEntityFilter m_targetValidator;
      private IEntityFilter m_casterValidator;
      private ListComparison m_comparison;
      private List<PropertyId> m_properties;

      public IEntityFilter targetValidator => this.m_targetValidator;

      public IEntityFilter casterValidator => this.m_casterValidator;

      public ListComparison comparison => this.m_comparison;

      public IReadOnlyList<PropertyId> properties => (IReadOnlyList<PropertyId>) this.m_properties;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnPropertyApplied FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnPropertyApplied) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnPropertyApplied onPropertyApplied = new EffectTrigger.OnPropertyApplied();
        onPropertyApplied.PopulateFromJson(jsonObject);
        return onPropertyApplied;
      }

      public static EffectTrigger.OnPropertyApplied FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnPropertyApplied defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnPropertyApplied.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targetValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "targetValidator");
        this.m_casterValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "casterValidator");
        this.m_comparison = (ListComparison) Serialization.JsonTokenValue<int>(jsonObject, "comparison", 2);
        this.m_properties = Serialization.JsonArrayAsList<PropertyId>(jsonObject, "properties");
      }
    }

    [Serializable]
    public sealed class OnReservePointChanged : EffectTrigger
    {
      private bool m_exceptFromSpellGaugeModification;
      private OwnerFilter m_player;
      private ValueFilter m_modification;
      private ValueFilter m_newValue;

      public bool exceptFromSpellGaugeModification => this.m_exceptFromSpellGaugeModification;

      public OwnerFilter player => this.m_player;

      public ValueFilter modification => this.m_modification;

      public ValueFilter newValue => this.m_newValue;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnReservePointChanged FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnReservePointChanged) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnReservePointChanged reservePointChanged = new EffectTrigger.OnReservePointChanged();
        reservePointChanged.PopulateFromJson(jsonObject);
        return reservePointChanged;
      }

      public static EffectTrigger.OnReservePointChanged FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnReservePointChanged defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnReservePointChanged.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_exceptFromSpellGaugeModification = Serialization.JsonTokenValue<bool>(jsonObject, "exceptFromSpellGaugeModification");
        this.m_player = OwnerFilter.FromJsonProperty(jsonObject, "player");
        this.m_modification = ValueFilter.FromJsonProperty(jsonObject, "modification");
        this.m_newValue = ValueFilter.FromJsonProperty(jsonObject, "newValue");
      }
    }

    [Serializable]
    public sealed class OnReserveUsed : EffectTrigger
    {
      private OwnerFilter m_player;

      public OwnerFilter player => this.m_player;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnReserveUsed FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnReserveUsed) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnReserveUsed onReserveUsed = new EffectTrigger.OnReserveUsed();
        onReserveUsed.PopulateFromJson(jsonObject);
        return onReserveUsed;
      }

      public static EffectTrigger.OnReserveUsed FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnReserveUsed defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnReserveUsed.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_player = OwnerFilter.FromJsonProperty(jsonObject, "player");
      }
    }

    [Serializable]
    public sealed class OnSpecificEventTrigger : EffectTrigger
    {
      private SpecificEventTrigger m_trigger;
      private IEntityFilter m_effectHolderValidator;

      public SpecificEventTrigger trigger => this.m_trigger;

      public IEntityFilter effectHolderValidator => this.m_effectHolderValidator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnSpecificEventTrigger FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnSpecificEventTrigger) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnSpecificEventTrigger specificEventTrigger = new EffectTrigger.OnSpecificEventTrigger();
        specificEventTrigger.PopulateFromJson(jsonObject);
        return specificEventTrigger;
      }

      public static EffectTrigger.OnSpecificEventTrigger FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnSpecificEventTrigger defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnSpecificEventTrigger.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_trigger = (SpecificEventTrigger) Serialization.JsonTokenValue<int>(jsonObject, "trigger");
        this.m_effectHolderValidator = IEntityFilterUtils.FromJsonProperty(jsonObject, "effectHolderValidator");
      }
    }

    [Serializable]
    public sealed class OnSpellDrawn : EffectTrigger
    {
      private OwnerFilter m_drawnBy;
      private List<SpellFilter> m_spellFilters;

      public OwnerFilter drawnBy => this.m_drawnBy;

      public IReadOnlyList<SpellFilter> spellFilters => (IReadOnlyList<SpellFilter>) this.m_spellFilters;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnSpellDrawn FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnSpellDrawn) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnSpellDrawn onSpellDrawn = new EffectTrigger.OnSpellDrawn();
        onSpellDrawn.PopulateFromJson(jsonObject);
        return onSpellDrawn;
      }

      public static EffectTrigger.OnSpellDrawn FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnSpellDrawn defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnSpellDrawn.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_drawnBy = OwnerFilter.FromJsonProperty(jsonObject, "drawnBy");
        JArray jarray = Serialization.JsonArray(jsonObject, "spellFilters");
        this.m_spellFilters = new List<SpellFilter>(jarray != null ? jarray.Count : 0);
        if (jarray == null)
          return;
        foreach (JToken token in jarray)
          this.m_spellFilters.Add(SpellFilter.FromJsonToken(token));
      }
    }

    [Serializable]
    public sealed class OnSquadChanged : EffectTrigger
    {
      private List<SquadModification> m_modification;
      private IEntityFilter m_validator;

      public IReadOnlyList<SquadModification> modification => (IReadOnlyList<SquadModification>) this.m_modification;

      public IEntityFilter validator => this.m_validator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnSquadChanged FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnSquadChanged) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnSquadChanged onSquadChanged = new EffectTrigger.OnSquadChanged();
        onSquadChanged.PopulateFromJson(jsonObject);
        return onSquadChanged;
      }

      public static EffectTrigger.OnSquadChanged FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnSquadChanged defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnSquadChanged.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_modification = Serialization.JsonArrayAsList<SquadModification>(jsonObject, "modification");
        this.m_validator = IEntityFilterUtils.FromJsonProperty(jsonObject, "validator");
      }
    }

    [Serializable]
    public sealed class OnTeamsInitialized : EffectTrigger
    {
      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnTeamsInitialized FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnTeamsInitialized) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnTeamsInitialized teamsInitialized = new EffectTrigger.OnTeamsInitialized();
        teamsInitialized.PopulateFromJson(jsonObject);
        return teamsInitialized;
      }

      public static EffectTrigger.OnTeamsInitialized FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnTeamsInitialized defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnTeamsInitialized.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class OnThisEffectStored : EffectTrigger
    {
      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnThisEffectStored FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnThisEffectStored) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnThisEffectStored thisEffectStored = new EffectTrigger.OnThisEffectStored();
        thisEffectStored.PopulateFromJson(jsonObject);
        return thisEffectStored;
      }

      public static EffectTrigger.OnThisEffectStored FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnThisEffectStored defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnThisEffectStored.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class OnTurnEnded : EffectTrigger
    {
      private IEntityFilter m_validator;

      public IEntityFilter validator => this.m_validator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnTurnEnded FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnTurnEnded) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnTurnEnded onTurnEnded = new EffectTrigger.OnTurnEnded();
        onTurnEnded.PopulateFromJson(jsonObject);
        return onTurnEnded;
      }

      public static EffectTrigger.OnTurnEnded FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnTurnEnded defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnTurnEnded.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_validator = IEntityFilterUtils.FromJsonProperty(jsonObject, "validator");
      }
    }

    [Serializable]
    public sealed class OnTurnStarted : EffectTrigger
    {
      private IEntityFilter m_validator;

      public IEntityFilter validator => this.m_validator;

      public override string ToString() => this.GetType().Name;

      public static EffectTrigger.OnTurnStarted FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (EffectTrigger.OnTurnStarted) null;
        }
        JObject jsonObject = token.Value<JObject>();
        EffectTrigger.OnTurnStarted onTurnStarted = new EffectTrigger.OnTurnStarted();
        onTurnStarted.PopulateFromJson(jsonObject);
        return onTurnStarted;
      }

      public static EffectTrigger.OnTurnStarted FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        EffectTrigger.OnTurnStarted defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectTrigger.OnTurnStarted.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_validator = IEntityFilterUtils.FromJsonProperty(jsonObject, "validator");
      }
    }
  }
}
