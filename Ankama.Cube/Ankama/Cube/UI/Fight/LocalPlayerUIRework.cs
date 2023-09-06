// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.LocalPlayerUIRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.CostModifiers;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.States;
using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public sealed class LocalPlayerUIRework : AbstractPlayerUIRework
  {
    [SerializeField]
    private SpellBarRework m_spellBar;
    [SerializeField]
    private CompanionBarRework m_companionBar;

    private void Awake()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.OnReserveActivation += new Action(LocalPlayerUIRework.OnReserveActivation);
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.OnReserveActivation -= new Action(LocalPlayerUIRework.OnReserveActivation);
    }

    public override void SetPlayerStatus(PlayerStatus playerStatus)
    {
      base.SetPlayerStatus(playerStatus);
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_spellBar)
        this.m_spellBar.SetPlayerStatus(playerStatus);
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_companionBar))
        return;
      this.m_companionBar.SetPlayerStatus(playerStatus);
    }

    public override void RefreshAvailableActions(bool recomputeSpellCosts)
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_spellBar))
        return;
      this.m_spellBar.RefreshUsability(this.m_playerStatus, recomputeSpellCosts);
    }

    public override void UpdateAvailableActions(bool recomputeSpellCosts)
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_spellBar))
        return;
      this.m_spellBar.UpdateUsability(recomputeSpellCosts);
    }

    public override void SetUIInteractable(bool interactable)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_spellBar)
        this.m_spellBar.SetInteractable(interactable);
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_companionBar)
        this.m_companionBar.SetInteractable(interactable);
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.SetInteractable(interactable);
    }

    public override void AddSpellStatus(SpellInfo spellInfo, int index)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_spellBar)
        return;
      SpellStatus spellStatus = SpellStatus.TryCreate(spellInfo, this.m_playerStatus);
      if (spellStatus == null)
        return;
      this.m_spellBar.AddSpellStatus(spellStatus);
    }

    public override void RemoveSpellStatus(int spellInstanceId, int index)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_spellBar)
        return;
      this.m_spellBar.RemoveSpellStatus(spellInstanceId);
    }

    public override IEnumerator AddSpell(SpellInfo spellInfo, int index)
    {
      LocalPlayerUIRework localPlayerUiRework = this;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) localPlayerUiRework.m_spellBar))
      {
        float seconds = (float) index * 0.1f;
        if ((double) seconds > 0.0)
          yield return (object) new WaitForTime(seconds);
        SpellStatus spellStatus = SpellStatus.TryCreate(spellInfo, localPlayerUiRework.m_playerStatus);
        if (spellStatus != null)
          yield return (object) localPlayerUiRework.m_spellBar.AddSpell(spellStatus);
      }
    }

    public override IEnumerator RemoveSpell(int spellInstanceId, int index)
    {
      if (!((UnityEngine.Object) null == (UnityEngine.Object) this.m_spellBar))
      {
        float seconds = (float) index * 0.1f;
        if ((double) seconds > 0.0)
          yield return (object) new WaitForTime(seconds);
        yield return (object) this.m_spellBar.RemoveSpell(spellInstanceId);
      }
    }

    public override IEnumerator AddSpellCostModifier(SpellCostModification spellCostModifier)
    {
      yield return (object) base.AddSpellCostModifier(spellCostModifier);
    }

    public override IEnumerator RemoveSpellCostModifier(int spellCostModifierId)
    {
      yield return (object) base.RemoveSpellCostModifier(spellCostModifierId);
    }

    public override void RefreshAvailableCompanions()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_companionBar))
        return;
      this.m_companionBar.RefreshUsability(this.m_playerStatus);
    }

    public override void AddCompanionStatus(int companionDefinitionId, int level, int index)
    {
      CompanionDefinition definition;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBar || !RuntimeData.companionDefinitions.TryGetValue(companionDefinitionId, out definition))
        return;
      this.m_companionBar.AddCompanionStatus(this.m_playerStatus, new ReserveCompanionStatus(this.m_playerStatus, definition, level));
    }

    public override void AddAdditionalCompanionStatus(
      PlayerStatus owner,
      int companionDefinitionId,
      int level)
    {
      CompanionDefinition definition;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBar || !RuntimeData.companionDefinitions.TryGetValue(companionDefinitionId, out definition))
        return;
      this.m_companionBar.AddCompanionStatus(this.m_playerStatus, new ReserveCompanionStatus(owner, definition, level));
    }

    public override void ChangeCompanionStateStatus(
      int companionDefinitionId,
      CompanionReserveState state)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBar)
        return;
      this.m_companionBar.ChangeCompanionStateStatus(this.m_playerStatus, companionDefinitionId, state);
    }

    public override void RemoveAdditionalCompanionStatus(int companionDefinitionId)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBar)
        return;
      this.m_companionBar.RemoveCompanionStatus(companionDefinitionId);
    }

    public override IEnumerator UpdateAvailableCompanions()
    {
      if (!((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBar))
        yield return (object) this.m_companionBar.UpdateAvailableCompanions();
    }

    public override IEnumerator AddCompanion(int companionDefinitionId, int level, int index)
    {
      LocalPlayerUIRework localPlayerUiRework = this;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) localPlayerUiRework.m_companionBar))
      {
        float seconds = (float) index * 0.1f;
        if ((double) seconds > 0.0)
          yield return (object) new WaitForTime(seconds);
        CompanionDefinition definition;
        if (RuntimeData.companionDefinitions.TryGetValue(companionDefinitionId, out definition))
        {
          ReserveCompanionStatus companion = new ReserveCompanionStatus(localPlayerUiRework.m_playerStatus, definition, level);
          yield return (object) localPlayerUiRework.m_companionBar.AddCompanion(companion);
        }
      }
    }

    public override IEnumerator AddAdditionalCompanion(
      PlayerStatus owner,
      int companionDefinitionId,
      int level)
    {
      CompanionDefinition definition;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBar) && RuntimeData.companionDefinitions.TryGetValue(companionDefinitionId, out definition))
        yield return (object) this.m_companionBar.AddCompanion(new ReserveCompanionStatus(owner, definition, level));
    }

    public override IEnumerator ChangeCompanionState(
      int companionDefinitionId,
      CompanionReserveState state)
    {
      LocalPlayerUIRework localPlayerUiRework = this;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) localPlayerUiRework.m_companionBar))
        yield return (object) localPlayerUiRework.m_companionBar.ChangeCompanionState(localPlayerUiRework.m_playerStatus, companionDefinitionId);
    }

    public override IEnumerator RemoveAdditionalCompanion(int companionDefinitionId)
    {
      if (!((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBar))
        yield return (object) this.m_companionBar.RemoveCompanion(companionDefinitionId);
    }

    private static void OnReserveActivation() => FightState.instance?.frame?.SendUseReserve();
  }
}
