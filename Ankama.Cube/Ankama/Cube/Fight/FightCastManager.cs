// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightCastManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.States;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Fight
{
  public static class FightCastManager
  {
    private static FightCastManager.CurrentCastType s_currentCastType;
    private static FightCastState s_currentCastState;
    private static PlayerStatus s_playerCasting;
    private static SpellStatus s_spellBeingCast;
    private static ReserveCompanionStatus s_companionBeingInvoked;
    private static ICastTargetDefinition s_castTargetDefinition;
    private static CastTargetContext s_castTargetContext;

    public static FightCastManager.CurrentCastType currentCastType => FightCastManager.s_currentCastType;

    public static FightCastState currentCastState => FightCastManager.s_currentCastState;

    public static event FightCastManager.OnTargetChangeDelegate OnTargetChange;

    public static event FightCastManager.OnUserActionEndDelegate OnUserActionEnd;

    public static bool StartCastingSpell(PlayerStatus casterStatus, SpellStatus spellStatus)
    {
      if (FightCastManager.s_currentCastType != FightCastManager.CurrentCastType.None)
      {
        Log.Error(string.Format("Tried to start casting a spell while current cast type is {0}", (object) FightCastManager.s_currentCastType), 67, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
        return false;
      }
      SpellDefinition definition = spellStatus.definition;
      if ((UnityEngine.Object) null == (UnityEngine.Object) definition)
      {
        Log.Error("Tried to start casting a spell without a loaded definition.", 74, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
        return false;
      }
      ICastTargetDefinition castTarget = definition.castTarget;
      if (castTarget == null)
      {
        Log.Error("Tried to cast a spell that has no cast target definition.", 81, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
        return false;
      }
      CastTargetContext castTargetContext = castTarget.CreateCastTargetContext(FightStatus.local, casterStatus.id, DynamicValueHolderType.Spell, definition.id, spellStatus.level, spellStatus.instanceId);
      IReadOnlyList<Cost> costs = definition.costs;
      int count = ((IReadOnlyCollection<Cost>) costs).Count;
      for (int index = 0; index < count; ++index)
      {
        if (costs[index].CheckValidity(casterStatus, (DynamicValueContext) castTargetContext) != CastValidity.SUCCESS)
        {
          Log.Error("Tried to cast a spell but one cost requirement is not met.", 94, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
          return false;
        }
      }
      FightMap current = FightMap.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
      {
        FightMap fightMap1 = current;
        fightMap1.onTargetChanged = fightMap1.onTargetChanged + new Action<Target?, CellObject>(FightCastManager.OnSpellTargetChanged);
        FightMap fightMap2 = current;
        fightMap2.onTargetSelected = fightMap2.onTargetSelected + new Action<Target?>(FightCastManager.OnSpellTargetSelected);
        current.SetTargetingPhase(castTarget.EnumerateTargets(castTargetContext));
      }
      FightCastManager.s_currentCastType = FightCastManager.CurrentCastType.Spell;
      FightCastManager.s_currentCastState = FightCastState.Targeting;
      FightCastManager.s_playerCasting = casterStatus;
      FightCastManager.s_spellBeingCast = spellStatus;
      FightCastManager.s_castTargetDefinition = castTarget;
      FightCastManager.s_castTargetContext = castTargetContext;
      FightCastManager.ShowSpellCostsPreview();
      return true;
    }

    private static void OnSpellTargetChanged(Target? target, [CanBeNull] CellObject cellObject)
    {
      FightCastManager.OnTargetChangeDelegate onTargetChange = FightCastManager.OnTargetChange;
      if (onTargetChange == null)
        return;
      onTargetChange(target.HasValue, cellObject);
    }

    private static void OnSpellTargetSelected(Target? targetOpt)
    {
      if (targetOpt.HasValue)
      {
        Target target = targetOpt.Value;
        FightCastManager.s_castTargetContext.SelectTarget(target);
        int selectedTargetCount = FightCastManager.s_castTargetContext.selectedTargetCount;
        if (selectedTargetCount == 1)
        {
          FightCastManager.s_currentCastState = FightCastState.Selecting;
          FightCastManager.OnUserActionEndDelegate onUserActionEnd = FightCastManager.OnUserActionEnd;
          if (onUserActionEnd != null)
            onUserActionEnd(FightCastState.Selecting);
        }
        if (selectedTargetCount >= FightCastManager.s_castTargetContext.expectedTargetCount)
        {
          FightCastManager.s_castTargetContext.SendCommand();
          FightCastManager.s_currentCastState = FightCastState.Casting;
          FightCastManager.OnUserActionEndDelegate onUserActionEnd = FightCastManager.OnUserActionEnd;
          if (onUserActionEnd == null)
            return;
          onUserActionEnd(FightCastState.Casting);
        }
        else
          FightMap.current.SetTargetingPhase(FightCastManager.s_castTargetDefinition.EnumerateTargets(FightCastManager.s_castTargetContext));
      }
      else
        FightCastManager.StopCastingSpell(true);
    }

    public static void StopCastingSpell(bool cancelled)
    {
      if (FightCastManager.s_currentCastType != FightCastManager.CurrentCastType.Spell)
      {
        Log.Error(string.Format("Tried to stop casting a spell while current cast type is {0}", (object) FightCastManager.s_currentCastType), 166, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
      }
      else
      {
        if (cancelled)
        {
          FightCastManager.OnUserActionEndDelegate onUserActionEnd = FightCastManager.OnUserActionEnd;
          if (onUserActionEnd != null)
            onUserActionEnd(FightCastState.Cancelled);
        }
        else
        {
          FightCastManager.OnUserActionEndDelegate onUserActionEnd = FightCastManager.OnUserActionEnd;
          if (onUserActionEnd != null)
            onUserActionEnd(FightCastState.DoneCasting);
        }
        FightCastManager.HideSpellCostsPreview(cancelled);
        FightMap current = FightMap.current;
        if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        {
          FightMap fightMap1 = current;
          fightMap1.onTargetChanged = fightMap1.onTargetChanged - new Action<Target?, CellObject>(FightCastManager.OnSpellTargetChanged);
          FightMap fightMap2 = current;
          fightMap2.onTargetSelected = fightMap2.onTargetSelected - new Action<Target?>(FightCastManager.OnSpellTargetSelected);
          FightCastManager.RevertFightMapTargetingPhase(current);
        }
        FightCastManager.s_currentCastType = FightCastManager.CurrentCastType.None;
        FightCastManager.s_currentCastState = FightCastState.Idle;
        FightCastManager.s_playerCasting = (PlayerStatus) null;
        FightCastManager.s_spellBeingCast = (SpellStatus) null;
        FightCastManager.s_castTargetDefinition = (ICastTargetDefinition) null;
        FightCastManager.s_castTargetContext = (CastTargetContext) null;
      }
    }

    public static void CheckSpellPlayed(int spellInstanceId)
    {
      if (FightCastManager.s_currentCastType != FightCastManager.CurrentCastType.Spell || FightCastManager.s_spellBeingCast.instanceId != spellInstanceId)
        return;
      FightCastManager.StopCastingSpell(false);
    }

    public static bool StartInvokingCompanion(
      PlayerStatus casterStatus,
      ReserveCompanionStatus companionStatus)
    {
      if (FightCastManager.s_currentCastType != FightCastManager.CurrentCastType.None)
      {
        Log.Error(string.Format("Tried to start invoking a companion while current cast type is {0}", (object) FightCastManager.s_currentCastType), 220, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
        return false;
      }
      CompanionDefinition definition = companionStatus.definition;
      if ((UnityEngine.Object) null == (UnityEngine.Object) definition)
      {
        Log.Error("Tried to start invoking a companion without a loaded definition.", 227, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
        return false;
      }
      OneCastTargetContext castTargetContext = new OneCastTargetContext(FightStatus.local, casterStatus.id, DynamicValueHolderType.Companion, definition.id, companionStatus.level, 0);
      FightMap current = FightMap.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
      {
        ICoordSelector spawnLocation = definition.spawnLocation;
        if (spawnLocation == null)
        {
          Log.Error("Tried to start invoking a companion that has no spawn location.", 239, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
          return false;
        }
        FightMap fightMap1 = current;
        fightMap1.onTargetChanged = fightMap1.onTargetChanged + new Action<Target?, CellObject>(FightCastManager.OnCompanionInvocationLocationChanged);
        FightMap fightMap2 = current;
        fightMap2.onTargetSelected = fightMap2.onTargetSelected + new Action<Target?>(FightCastManager.OnCompanionInvocationLocationSelected);
        current.SetTargetingPhase(FightCastManager.EnumerateCompanionAvailableLocations(spawnLocation, (CastTargetContext) castTargetContext));
      }
      FightCastManager.s_currentCastType = FightCastManager.CurrentCastType.Companion;
      FightCastManager.s_currentCastState = FightCastState.Targeting;
      FightCastManager.s_playerCasting = casterStatus;
      FightCastManager.s_companionBeingInvoked = companionStatus;
      FightCastManager.s_castTargetContext = (CastTargetContext) castTargetContext;
      FightCastManager.ShowCompanionCostsPreview();
      return true;
    }

    private static void OnCompanionInvocationLocationChanged(Target? target, [CanBeNull] CellObject cellObject)
    {
      FightCastManager.OnTargetChangeDelegate onTargetChange = FightCastManager.OnTargetChange;
      if (onTargetChange == null)
        return;
      onTargetChange(target.HasValue, cellObject);
    }

    private static void OnCompanionInvocationLocationSelected(Target? targetOpt)
    {
      if (targetOpt.HasValue)
      {
        FightFrame frame = FightState.instance?.frame;
        bool flag = false;
        if (frame != null)
        {
          Target target = targetOpt.Value;
          frame.SendInvokeCompanion(FightCastManager.s_companionBeingInvoked.definition.id, target.coord);
          flag = true;
        }
        if (flag)
        {
          FightCastManager.s_currentCastState = FightCastState.Casting;
          FightCastManager.OnUserActionEndDelegate onUserActionEnd = FightCastManager.OnUserActionEnd;
          if (onUserActionEnd == null)
            return;
          onUserActionEnd(FightCastState.Casting);
        }
        else
          FightCastManager.StopInvokingCompanion(true);
      }
      else
        FightCastManager.StopInvokingCompanion(true);
    }

    public static void StopInvokingCompanion(bool cancelled)
    {
      if (FightCastManager.s_currentCastType != FightCastManager.CurrentCastType.Companion)
      {
        Log.Error(string.Format("Tried to stop casting a spell while current cast type is {0}", (object) FightCastManager.s_currentCastType), 298, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightCastManager.cs");
      }
      else
      {
        FightCastManager.OnUserActionEndDelegate onUserActionEnd = FightCastManager.OnUserActionEnd;
        if (onUserActionEnd != null)
          onUserActionEnd(cancelled ? FightCastState.Cancelled : FightCastState.DoneCasting);
        FightCastManager.HideCompanionCostsPreview(cancelled);
        FightMap current = FightMap.current;
        if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        {
          FightMap fightMap1 = current;
          fightMap1.onTargetChanged = fightMap1.onTargetChanged - new Action<Target?, CellObject>(FightCastManager.OnCompanionInvocationLocationChanged);
          FightMap fightMap2 = current;
          fightMap2.onTargetSelected = fightMap2.onTargetSelected - new Action<Target?>(FightCastManager.OnCompanionInvocationLocationSelected);
          FightCastManager.RevertFightMapTargetingPhase(current);
        }
        FightCastManager.s_currentCastType = FightCastManager.CurrentCastType.None;
        FightCastManager.s_currentCastState = FightCastState.Idle;
        FightCastManager.s_playerCasting = (PlayerStatus) null;
        FightCastManager.s_companionBeingInvoked = (ReserveCompanionStatus) null;
        FightCastManager.s_castTargetContext = (CastTargetContext) null;
      }
    }

    public static void CheckCompanionInvoked(int companionDefinitionId)
    {
      if (FightCastManager.s_currentCastType != FightCastManager.CurrentCastType.Companion || FightCastManager.s_companionBeingInvoked.definition.id != companionDefinitionId)
        return;
      FightCastManager.StopInvokingCompanion(false);
    }

    private static IEnumerable<Target> EnumerateCompanionAvailableLocations(
      ICoordSelector spawnLocation,
      CastTargetContext castTargetContext)
    {
      foreach (Coord enumerateCoord in spawnLocation.EnumerateCoords((DynamicValueContext) castTargetContext))
        yield return new Target(enumerateCoord);
    }

    private static void RevertFightMapTargetingPhase(FightMap fightMap)
    {
      FightStatus local = FightStatus.local;
      if (local != null && local.currentTurnPlayerId == FightCastManager.s_playerCasting.id)
      {
        if (!fightMap.IsInTargetingPhase())
          return;
        fightMap.SetMovementPhase();
      }
      else
        fightMap.EndCurrentPhase();
    }

    private static void ShowSpellCostsPreview()
    {
      CastTargetContext castTargetContext = FightCastManager.s_spellBeingCast.CreateCastTargetContext();
      FightCastManager.PreviewCosts(FightCastManager.s_spellBeingCast.definition.costs, (DynamicValueFightContext) castTargetContext);
    }

    private static void HideSpellCostsPreview(bool cancelled) => FightUIRework.instance.GetLocalPlayerUI(FightCastManager.s_playerCasting).HideAllPreviews(cancelled);

    private static void ShowCompanionCostsPreview()
    {
      ReserveCompanionValueContext valueContext = FightCastManager.s_companionBeingInvoked.CreateValueContext();
      FightCastManager.PreviewCosts(FightCastManager.s_companionBeingInvoked.definition.cost, (DynamicValueFightContext) valueContext);
    }

    private static void HideCompanionCostsPreview(bool cancelled) => FightUIRework.instance.GetLocalPlayerUI(FightCastManager.s_playerCasting).HideAllPreviews(cancelled);

    private static void PreviewCosts(IReadOnlyList<Cost> costs, DynamicValueFightContext context)
    {
      LocalPlayerUIRework localPlayerUi = FightUIRework.instance.GetLocalPlayerUI(FightCastManager.s_playerCasting);
      for (int index = 0; index < ((IReadOnlyCollection<Cost>) costs).Count; ++index)
      {
        switch (costs[index])
        {
          case ActionPointsCost actionPointsCost:
            int num1;
            if (actionPointsCost.value.GetValue((DynamicValueContext) context, out num1))
            {
              localPlayerUi.PreviewActionPoints(num1, ValueModifier.Add);
              break;
            }
            break;
          case DrainActionPointsCost _:
            localPlayerUi.PreviewActionPoints(0, ValueModifier.Set);
            break;
          case ElementPointsCost elementPointsCost:
            int num2;
            if (elementPointsCost.value.GetValue((DynamicValueContext) context, out num2))
            {
              FightCastManager.PreviewElementaryPoints(localPlayerUi, elementPointsCost.element, num2, ValueModifier.Add);
              break;
            }
            break;
          case ReservePointsCost reservePointsCost:
            int num3;
            if (reservePointsCost.value.GetValue((DynamicValueContext) context, out num3))
            {
              localPlayerUi.PreviewReservePoints(num3, ValueModifier.Add);
              break;
            }
            break;
          case DrainReservePointsCost _:
            localPlayerUi.PreviewReservePoints(0, ValueModifier.Set);
            break;
        }
      }
    }

    private static void PreviewElementaryPoints(
      LocalPlayerUIRework localPlayerUI,
      CaracId element,
      int value,
      ValueModifier modifier)
    {
      switch (element)
      {
        case CaracId.FirePoints:
          localPlayerUI.ShowPreviewFire(value, modifier);
          break;
        case CaracId.WaterPoints:
          localPlayerUI.ShowPreviewWater(value, modifier);
          break;
        case CaracId.EarthPoints:
          localPlayerUI.ShowPreviewEarth(value, modifier);
          break;
        case CaracId.AirPoints:
          localPlayerUI.ShowPreviewAir(value, modifier);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (element), (object) element, (string) null);
      }
    }

    public delegate void OnTargetChangeDelegate(bool hasTarget, CellObject cellObject);

    public delegate void OnUserActionEndDelegate(FightCastState state);

    public enum CurrentCastType
    {
      None,
      Spell,
      Companion,
    }
  }
}
