// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.PlayerUIRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Components;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public sealed class PlayerUIRework : AbstractPlayerUIRework
  {
    [SerializeField]
    private CardNumberCounterRework m_cardNumberCounterRework;
    [SerializeField]
    private ContainerDrawer m_container;
    [SerializeField]
    private Button m_switchContainerInfoButton;

    private void Awake()
    {
      if (!(bool) (Object) this.m_switchContainerInfoButton)
        return;
      this.m_switchContainerInfoButton.onClick.AddListener(new UnityAction(this.OnSwitchContainer));
    }

    private void OnSwitchContainer()
    {
      if (!(bool) (Object) this.m_container)
        return;
      this.m_container.Switch();
    }

    public override void SetUIInteractable(bool interactable)
    {
      if (!((Object) null != (Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.SetInteractable(true);
    }

    public override IEnumerator AddSpell(SpellInfo spellInfo, int index)
    {
      if ((Object) null != (Object) this.m_cardNumberCounterRework)
        yield return (object) this.m_cardNumberCounterRework.Increment();
    }

    public override IEnumerator RemoveSpell(int spellInstanceId, int index)
    {
      if ((Object) null != (Object) this.m_cardNumberCounterRework)
        yield return (object) this.m_cardNumberCounterRework.Decrement();
    }

    public override void AddSpellStatus(SpellInfo spellInfo, int index)
    {
    }

    public override void RemoveSpellStatus(int spellInstanceId, int index)
    {
    }

    public override void RefreshAvailableCompanions()
    {
    }

    public override IEnumerator UpdateAvailableCompanions()
    {
      yield break;
    }

    public override void AddCompanionStatus(int companionDefinitionId, int level, int index)
    {
    }

    public override void AddAdditionalCompanionStatus(
      PlayerStatus owner,
      int companionDefinitionId,
      int level)
    {
    }

    public override void ChangeCompanionStateStatus(
      int companionDefinitionId,
      CompanionReserveState state)
    {
    }

    public override void RemoveAdditionalCompanionStatus(int companionDefinitionId)
    {
    }

    public override IEnumerator AddCompanion(int companionDefinitionId, int level, int index)
    {
      yield break;
    }

    public override IEnumerator AddAdditionalCompanion(
      PlayerStatus owner,
      int companionDefinitionId,
      int level)
    {
      yield break;
    }

    public override IEnumerator ChangeCompanionState(
      int companionDefinitionId,
      CompanionReserveState state)
    {
      yield break;
    }

    public override IEnumerator RemoveAdditionalCompanion(int companionDefinitionId)
    {
      yield break;
    }
  }
}
