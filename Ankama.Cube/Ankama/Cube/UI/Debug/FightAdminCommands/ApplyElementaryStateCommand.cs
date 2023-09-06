// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.ApplyElementaryStateCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Protocols.FightAdminProtocol;
using System;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public class ApplyElementaryStateCommand : ContinuousFightAdminCommand
  {
    private readonly DebugSelectorElementaryState m_elementaryStateSelector;

    public ApplyElementaryStateCommand(
      KeyCode keycode,
      DebugSelectorElementaryState elementaryStateSelector)
      : base("Set Elementary State <i>(⇧: remove)</i>", keycode)
    {
      this.m_elementaryStateSelector = elementaryStateSelector;
    }

    protected override void Start()
    {
      this.m_elementaryStateSelector.SetActive(true);
      this.SetTargetingPhase();
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected + new Action<Target?>(this.OnEntitySelected);
    }

    protected override void Stop()
    {
      this.m_elementaryStateSelector.SetActive(false);
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected - new Action<Target?>(this.OnEntitySelected);
      this.EndTargetingPhase();
    }

    private void OnEntitySelected(Target? obj)
    {
      IEntity entity = obj?.entity;
      if (entity == null)
        return;
      this.SetTargetingPhase();
      AbstractFightAdminCommand.SendAdminCommand(new AdminRequestCmd()
      {
        SetElementaryState = new AdminRequestCmd.Types.SetElementaryStateAdminCmd()
        {
          TargetEntityId = entity.id,
          ElementaryStateId = AbstractFightAdminCommand.IsShiftDown() ? 1 : (int) this.m_elementaryStateSelector.selected
        }
      });
    }

    private void SetTargetingPhase() => this.m_fightMap.SetTargetingPhase(this.EnumerateEntitiesAsTargets<IEntity>());
  }
}
