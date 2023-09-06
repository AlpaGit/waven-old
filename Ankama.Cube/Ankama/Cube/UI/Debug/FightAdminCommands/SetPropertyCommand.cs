// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.SetPropertyCommand
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
  public class SetPropertyCommand : ContinuousFightAdminCommand
  {
    private readonly DebugSelectorProperty m_propertySelector;

    public SetPropertyCommand(KeyCode keycode, DebugSelectorProperty propertySelector)
      : base("Set Property <i>(⇧: remove)</i>", keycode)
    {
      this.m_propertySelector = propertySelector;
    }

    protected override void Start()
    {
      this.m_propertySelector.SetActive(true);
      this.SetTargetingPhase();
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected + new Action<Target?>(this.OnEntitySelected);
    }

    protected override void Stop()
    {
      this.m_propertySelector.SetActive(false);
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected - new Action<Target?>(this.OnEntitySelected);
      this.EndTargetingPhase();
    }

    private void OnEntitySelected(Target? obj)
    {
      IEntity entity = obj?.entity;
      if (entity == null)
        return;
      bool flag = !AbstractFightAdminCommand.IsShiftDown();
      AbstractFightAdminCommand.SendAdminCommand(new AdminRequestCmd()
      {
        SetProperty = new AdminRequestCmd.Types.SetPropertyCmd()
        {
          TargetEntityId = entity.id,
          PropertyId = (int) this.m_propertySelector.selected,
          Active = flag
        }
      });
      this.SetTargetingPhase();
    }

    private void SetTargetingPhase() => this.m_fightMap.SetTargetingPhase(this.EnumerateEntitiesAsTargets<IEntity>());
  }
}
