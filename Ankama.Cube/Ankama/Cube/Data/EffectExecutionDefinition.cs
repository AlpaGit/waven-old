// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EffectExecutionDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class EffectExecutionDefinition : AbstractEffectExecutionDefinition
  {
    protected EffectCondition m_condition;
    protected ITargetSelector m_executionTargetSelector;
    protected bool m_groupExecutionOnTargets;

    public EffectCondition condition => this.m_condition;

    public ITargetSelector executionTargetSelector => this.m_executionTargetSelector;

    public bool groupExecutionOnTargets => this.m_groupExecutionOnTargets;

    public override string ToString() => this.GetType().Name;

    public static EffectExecutionDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EffectExecutionDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class EffectExecutionDefinition");
        return (EffectExecutionDefinition) null;
      }
      string str = jtoken.Value<string>();
      EffectExecutionDefinition executionDefinition;
      switch (str)
      {
        case "ActivateFloorMechanismEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ActivateFloorMechanismEffectDefinition();
          break;
        case "AddSpellInGameEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new AddSpellInGameEffectDefinition();
          break;
        case "CaracChangedEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new CaracChangedEffectDefinition();
          break;
        case "ChangeEntitySkinEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ChangeEntitySkinEffectDefinition();
          break;
        case "ChargeEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ChargeEffectDefinition();
          break;
        case "DiscardSpellAndDrawEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new DiscardSpellAndDrawEffectDefinition();
          break;
        case "DiscardSpellEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new DiscardSpellEffectDefinition();
          break;
        case "DrawSpellEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new DrawSpellEffectDefinition();
          break;
        case "DuplicateSummoningEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new DuplicateSummoningEffectDefinition();
          break;
        case "ElementStateChangeEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ElementStateChangeEffectDefinition();
          break;
        case "ExplosionEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ExplosionEffectDefinition();
          break;
        case "FloatingCounterModificationEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new FloatingCounterModificationEffectDefinition();
          break;
        case "GrowEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new GrowEffectDefinition();
          break;
        case "HealEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new HealEffectDefinition();
          break;
        case "InvokeCreatureEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new InvokeCreatureEffectDefinition();
          break;
        case "LifeLeechEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new LifeLeechEffectDefinition();
          break;
        case "MagicalDamageEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new MagicalDamageEffectDefinition();
          break;
        case "MoveInLineEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new MoveInLineEffectDefinition();
          break;
        case "PhysicalDamageEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new PhysicalDamageEffectDefinition();
          break;
        case "PlayEntityAnimationEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new PlayEntityAnimationEffectDefinition();
          break;
        case "PlayEntityPassiveFxEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new PlayEntityPassiveFxEffectDefinition();
          break;
        case "PropertyChangeEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new PropertyChangeEffectDefinition();
          break;
        case "RegisterDamageProtectorEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new RegisterDamageProtectorEffectDefinition();
          break;
        case "RemoveEntityEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new RemoveEntityEffectDefinition();
          break;
        case "ResetActionEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ResetActionEffectDefinition();
          break;
        case "ResurrectCompanionsEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ResurrectCompanionsEffectDefinition();
          break;
        case "ReturnCompanionToHandEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ReturnCompanionToHandEffectDefinition();
          break;
        case "ReturnSpellToHandEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ReturnSpellToHandEffectDefinition();
          break;
        case "SetNonHealableLifeEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new SetNonHealableLifeEffectDefinition();
          break;
        case "SpellCostModifierEffect":
          executionDefinition = (EffectExecutionDefinition) new SpellCostModifierEffect();
          break;
        case "StealCaracEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new StealCaracEffectDefinition();
          break;
        case "StoppableCaracChangedEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new StoppableCaracChangedEffectDefinition();
          break;
        case "SwapPositionsEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new SwapPositionsEffectDefinition();
          break;
        case "TeleportEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new TeleportEffectDefinition();
          break;
        case "ThrowDiceEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ThrowDiceEffectDefinition();
          break;
        case "ThrowSpecificEventTrigger":
          executionDefinition = (EffectExecutionDefinition) new ThrowSpecificEventTrigger();
          break;
        case "ToggleElementaryStateEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new ToggleElementaryStateEffectDefinition();
          break;
        case "TransformationEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new TransformationEffectDefinition();
          break;
        case "TriggerFightActionEffectDefinition":
          executionDefinition = (EffectExecutionDefinition) new TriggerFightActionEffectDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (EffectExecutionDefinition) null;
      }
      executionDefinition.PopulateFromJson(jsonObject);
      return executionDefinition;
    }

    public static EffectExecutionDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EffectExecutionDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectExecutionDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_condition = EffectCondition.FromJsonProperty(jsonObject, "condition");
      this.m_executionTargetSelector = ITargetSelectorUtils.FromJsonProperty(jsonObject, "executionTargetSelector");
      this.m_groupExecutionOnTargets = Serialization.JsonTokenValue<bool>(jsonObject, "groupExecutionOnTargets");
    }
  }
}
