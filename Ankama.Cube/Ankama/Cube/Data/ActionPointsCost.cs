// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ActionPointsCost
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.CostModifiers;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.ActionPointsChanged})]
  [Serializable]
  public sealed class ActionPointsCost : Cost
  {
    private DynamicValue m_value;

    public DynamicValue value => this.m_value;

    public override string ToString() => string.Format("{0} AP", (object) this.value);

    public static ActionPointsCost FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ActionPointsCost) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ActionPointsCost actionPointsCost = new ActionPointsCost();
      actionPointsCost.PopulateFromJson(jsonObject);
      return actionPointsCost;
    }

    public static ActionPointsCost FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ActionPointsCost defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ActionPointsCost.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
    }

    protected override CastValidity InternalCheckValidity(
      PlayerStatus status,
      DynamicValueContext context)
    {
      int baseCost;
      this.value.GetValue(context, out baseCost);
      switch (context.type)
      {
        case DynamicValueHolderType.Spell:
          return ActionPointsCost.CheckForSpell(status, context, baseCost);
        case DynamicValueHolderType.Companion:
          return ActionPointsCost.CheckForCompanion(status, baseCost);
        case DynamicValueHolderType.CharacterAction:
          return CastValidity.SUCCESS;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static CastValidity CheckForSpell(
      PlayerStatus playerStatus,
      DynamicValueContext context,
      int baseCost)
    {
      if (!(context is CastTargetContext context1))
        return CastValidity.DATA_ERROR;
      SpellDefinition spellDefinition;
      if (!RuntimeData.spellDefinitions.TryGetValue(context1.spellDefinitionId, out spellDefinition))
      {
        Log.Error(string.Format("Could not find spell definition with id {0}.", (object) context1.spellDefinitionId), 44, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Costs\\ActionPointsCost.cs");
        return CastValidity.DATA_ERROR;
      }
      int num = SpellCostModification.ApplyCostModification(playerStatus.spellCostModifiers, baseCost, spellDefinition, context1);
      return playerStatus.actionPoints < num ? CastValidity.NOT_ENOUGH_ACTION_POINTS : CastValidity.SUCCESS;
    }

    private static CastValidity CheckForCompanion(PlayerStatus status, int baseCost) => status.GetCarac(CaracId.ActionPoints, 0) < baseCost ? CastValidity.NOT_ENOUGH_ACTION_POINTS : CastValidity.SUCCESS;

    public override void UpdateModifications(
      ref GaugesModification modifications,
      PlayerStatus player,
      DynamicValueContext context)
    {
      int cost;
      this.value.GetValue(context, out cost);
      if (context is CastTargetContext context1)
      {
        SpellDefinition spellDefinition;
        if (!RuntimeData.spellDefinitions.TryGetValue(context1.spellDefinitionId, out spellDefinition))
        {
          Log.Error(string.Format("Could not find spell definition with id {0}.", (object) context1.spellDefinitionId), 69, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Costs\\ActionPointsCost.cs");
          return;
        }
        cost = SpellCostModification.ApplyCostModification(player.spellCostModifiers, cost, spellDefinition, context1);
      }
      modifications.Increment(CaracId.ActionPoints, -cost);
    }
  }
}
