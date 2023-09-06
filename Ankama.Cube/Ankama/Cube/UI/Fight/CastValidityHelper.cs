// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.CastValidityHelper
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.CostModifiers;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.DeckMaker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ankama.Cube.UI.Fight
{
  public static class CastValidityHelper
  {
    private static bool HasCompanionValidTargets(
      PlayerStatus casterStatus,
      ReserveCompanionStatus companionStatus)
    {
      CompanionDefinition definition = companionStatus.definition;
      if ((UnityEngine.Object) null == (UnityEngine.Object) definition)
        return false;
      OneCastTargetContext context = new OneCastTargetContext(FightStatus.local, casterStatus.id, DynamicValueHolderType.Companion, definition.id, companionStatus.level, 0);
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightMap.current)
        return false;
      ICoordSelector spawnLocation = definition.spawnLocation;
      return spawnLocation != null && spawnLocation.EnumerateCoords((DynamicValueContext) context).GetEnumerator().MoveNext();
    }

    private static bool HasSpellValidTargets(PlayerStatus casterStatus, SpellStatus spellStatus)
    {
      SpellDefinition definition = spellStatus.definition;
      if ((UnityEngine.Object) null == (UnityEngine.Object) definition)
        return false;
      ICastTargetDefinition castTarget = definition.castTarget;
      if (castTarget == null)
        return false;
      CastTargetContext castTargetContext = castTarget.CreateCastTargetContext(FightStatus.local, casterStatus.id, DynamicValueHolderType.Spell, definition.id, spellStatus.level, spellStatus.instanceId);
      return castTarget.EnumerateTargets(castTargetContext).GetEnumerator().MoveNext();
    }

    private static CastValidity ComputeCastValidity(ICastableStatus status)
    {
      PrecomputedData precomputedData = status.GetDefinition()?.precomputedData;
      FightStatus local = FightStatus.local;
      if (precomputedData != null && local != null)
      {
        PlayerStatus player = local.GetLocalPlayer();
        HeroStatus heroStatus = player.heroStatus;
        WeaponDefinition definition = (WeaponDefinition) heroStatus.definition;
        if (precomputedData.checkNumberOfSummonings && local.EnumerateEntities<SummoningStatus>((Predicate<SummoningStatus>) (s => s.ownerId == player.id)).Count<SummoningStatus>() >= definition.maxSummoningsOnBoard.GetValueWithLevel(heroStatus.level))
          return CastValidity.TOO_MANY_SUMMONING;
        if (precomputedData.checkNumberOfMechanisms && local.EnumerateEntities<MechanismStatus>((Predicate<MechanismStatus>) (s => s.ownerId == player.id)).Count<MechanismStatus>() >= definition.maxMechanismsOnBoard.GetValueWithLevel(heroStatus.level))
          return CastValidity.TOO_MANY_MECHANISM;
      }
      return CastValidity.SUCCESS;
    }

    public static CastValidity ComputeSpellCostCastValidity(
      PlayerStatus owner,
      SpellStatus spellStatus)
    {
      if (owner.HasProperty(PropertyId.PlaySpellForbidden))
        return CastValidity.NOT_ALLOW_TO_PLAY_SPELLS;
      CastTargetContext castTargetContext = spellStatus.CreateCastTargetContext();
      IReadOnlyList<Cost> costs = spellStatus.definition.costs;
      for (int index = 0; index < ((IReadOnlyCollection<Cost>) costs).Count; ++index)
      {
        CastValidity costCastValidity = costs[index].CheckValidity(owner, (DynamicValueContext) castTargetContext);
        if (costCastValidity != CastValidity.SUCCESS)
          return costCastValidity;
      }
      return CastValidity.SUCCESS;
    }

    public static CastValidity ComputeCompanionCostCastValidity(
      PlayerStatus owner,
      ReserveCompanionStatus companionStatus)
    {
      DynamicValueFightContext valueContext = (DynamicValueFightContext) companionStatus.CreateValueContext();
      if (companionStatus.state == CompanionReserveState.Idle && companionStatus.isGiven)
        return CastValidity.SUCCESS;
      IReadOnlyList<Cost> cost = companionStatus.definition.cost;
      int num = 0;
      for (int count = ((IReadOnlyCollection<Cost>) cost).Count; num < count; ++num)
      {
        CastValidity costCastValidity = cost[num].CheckValidity(owner, (DynamicValueContext) valueContext);
        if (costCastValidity != CastValidity.SUCCESS)
          return costCastValidity;
      }
      return CastValidity.SUCCESS;
    }

    public static CastValidity ComputeSpellCastValidity(
      PlayerStatus playerStatus,
      SpellStatus spellStatus)
    {
      CastValidity castValidity = CastValidityHelper.ComputeCastValidity((ICastableStatus) spellStatus);
      if (castValidity != CastValidity.SUCCESS)
        return castValidity;
      return !CastValidityHelper.HasSpellValidTargets(playerStatus, spellStatus) ? CastValidity.NO_TARGET_AVAILABLE : CastValidity.SUCCESS;
    }

    public static CastValidity ComputeCompanionCastValidity(
      PlayerStatus owner,
      ReserveCompanionStatus companionStatus)
    {
      CastValidity castValidity = CastValidityHelper.ComputeCastValidity((ICastableStatus) companionStatus);
      if (castValidity != CastValidity.SUCCESS)
        return castValidity;
      return !CastValidityHelper.HasCompanionValidTargets(owner, companionStatus) ? CastValidity.NO_TARGET_AVAILABLE : CastValidity.SUCCESS;
    }

    public static void RecomputeCompanionCastValidity(
      PlayerStatus owner,
      ReserveCompanionStatus status,
      ref CompanionStatusData data)
    {
      data.hasResources = CastValidityHelper.ComputeCompanionCostCastValidity(owner, status) == CastValidity.SUCCESS;
    }

    public static void RecomputeSpellCastValidity(
      PlayerStatus owner,
      SpellStatus spellStatus,
      ref SpellStatusData data)
    {
      data.hasEnoughAp = CastValidityHelper.ComputeSpellCostCastValidity(owner, spellStatus) == CastValidity.SUCCESS;
    }

    public static void RecomputeCompanionCost(
      ReserveCompanionStatus status,
      ref CompanionStatusData data)
    {
      data.cost = status.isGiven ? (IReadOnlyList<Cost>) null : status.definition.cost;
    }

    public static void RecomputeSpellCost(SpellStatus spellStatus, ref SpellStatusData data)
    {
      if (spellStatus.ownerPlayer == null)
        return;
      int? baseCost = spellStatus.baseCost;
      if (!baseCost.HasValue)
      {
        data.apCost = new int?();
        data.baseCost = new int?();
      }
      else
      {
        SpellDefinition definition = spellStatus.definition;
        CastTargetContext castTargetContext = spellStatus.CreateCastTargetContext();
        int cost = spellStatus.definition.GetCost((DynamicValueContext) castTargetContext) ?? 0;
        data.apCost = new int?(SpellCostModification.ApplyCostModification(spellStatus.ownerPlayer.spellCostModifiers, cost, definition, castTargetContext));
        data.baseCost = new int?(baseCost.Value);
      }
    }
  }
}
