// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.KillEntityCommand
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
  public class KillEntityCommand : ContinuousFightAdminCommand
  {
    public KillEntityCommand(KeyCode keycode)
      : base("Kill", keycode)
    {
    }

    protected override void Start()
    {
      this.m_fightMap.SetTargetingPhase(this.EnumerateEntitiesAsTargets<IEntityWithLife>());
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected + new Action<Target?>(KillEntityCommand.OnUpdateTarget);
    }

    private static void OnUpdateTarget(Target? obj)
    {
      IEntity entity = obj?.entity;
      if (entity == null)
        return;
      AbstractFightAdminCommand.SendAdminCommand(new AdminRequestCmd()
      {
        Kill = new AdminRequestCmd.Types.KillAdminCmd()
        {
          TargetEntityId = entity.id
        }
      });
    }

    protected override void Stop()
    {
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected - new Action<Target?>(KillEntityCommand.OnUpdateTarget);
      this.EndTargetingPhase();
    }
  }
}
