// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AbstractEffectExecutionDefinition
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
  public abstract class AbstractEffectExecutionDefinition : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static AbstractEffectExecutionDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AbstractEffectExecutionDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class AbstractEffectExecutionDefinition");
        return (AbstractEffectExecutionDefinition) null;
      }
      string str = jtoken.Value<string>();
      AbstractEffectExecutionDefinition executionDefinition;
      switch (str)
      {
        case "ActivateFloorMechanismEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ActivateFloorMechanismEffectDefinition();
          break;
        case "AddSpellInGameEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new AddSpellInGameEffectDefinition();
          break;
        case "CaracChangedEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new CaracChangedEffectDefinition();
          break;
        case "ChangeEntitySkinEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ChangeEntitySkinEffectDefinition();
          break;
        case "ChargeEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ChargeEffectDefinition();
          break;
        case "DiscardSpellAndDrawEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new DiscardSpellAndDrawEffectDefinition();
          break;
        case "DiscardSpellEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new DiscardSpellEffectDefinition();
          break;
        case "DrawSpellEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new DrawSpellEffectDefinition();
          break;
        case "DuplicateSummoningEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new DuplicateSummoningEffectDefinition();
          break;
        case "ElementStateChangeEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ElementStateChangeEffectDefinition();
          break;
        case "ExplosionEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ExplosionEffectDefinition();
          break;
        case "FloatingCounterModificationEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new FloatingCounterModificationEffectDefinition();
          break;
        case "GrowEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new GrowEffectDefinition();
          break;
        case "HealEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new HealEffectDefinition();
          break;
        case "InvokeCreatureEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new InvokeCreatureEffectDefinition();
          break;
        case "LifeLeechEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new LifeLeechEffectDefinition();
          break;
        case "MagicalDamageEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new MagicalDamageEffectDefinition();
          break;
        case "MoveInLineEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new MoveInLineEffectDefinition();
          break;
        case "MultipleEffectExecutionDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new MultipleEffectExecutionDefinition();
          break;
        case "PhysicalDamageEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new PhysicalDamageEffectDefinition();
          break;
        case "PlayEntityAnimationEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new PlayEntityAnimationEffectDefinition();
          break;
        case "PlayEntityPassiveFxEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new PlayEntityPassiveFxEffectDefinition();
          break;
        case "PropertyChangeEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new PropertyChangeEffectDefinition();
          break;
        case "RegisterDamageProtectorEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new RegisterDamageProtectorEffectDefinition();
          break;
        case "RemoveEntityEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new RemoveEntityEffectDefinition();
          break;
        case "ResetActionEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ResetActionEffectDefinition();
          break;
        case "ResurrectCompanionsEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ResurrectCompanionsEffectDefinition();
          break;
        case "ReturnCompanionToHandEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ReturnCompanionToHandEffectDefinition();
          break;
        case "ReturnSpellToHandEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ReturnSpellToHandEffectDefinition();
          break;
        case "SetNonHealableLifeEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new SetNonHealableLifeEffectDefinition();
          break;
        case "SpellCostModifierEffect":
          executionDefinition = (AbstractEffectExecutionDefinition) new SpellCostModifierEffect();
          break;
        case "StealCaracEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new StealCaracEffectDefinition();
          break;
        case "StoppableCaracChangedEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new StoppableCaracChangedEffectDefinition();
          break;
        case "SwapPositionsEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new SwapPositionsEffectDefinition();
          break;
        case "TeleportEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new TeleportEffectDefinition();
          break;
        case "ThrowDiceEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ThrowDiceEffectDefinition();
          break;
        case "ThrowSpecificEventTrigger":
          executionDefinition = (AbstractEffectExecutionDefinition) new ThrowSpecificEventTrigger();
          break;
        case "ToggleElementaryStateEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new ToggleElementaryStateEffectDefinition();
          break;
        case "TransformationEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new TransformationEffectDefinition();
          break;
        case "TriggerFightActionEffectDefinition":
          executionDefinition = (AbstractEffectExecutionDefinition) new TriggerFightActionEffectDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (AbstractEffectExecutionDefinition) null;
      }
      executionDefinition.PopulateFromJson(jsonObject);
      return executionDefinition;
    }

    public static AbstractEffectExecutionDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AbstractEffectExecutionDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AbstractEffectExecutionDefinition.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }
  }
}
