// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.AbstractFightAdminCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Protocols.FightAdminProtocol;
using Ankama.Cube.States;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public abstract class AbstractFightAdminCommand
  {
    public readonly KeyCode key;
    public readonly string name;
    protected readonly FightStatus m_fightStatus;
    protected readonly FightMap m_fightMap;

    protected AbstractFightAdminCommand(string name, KeyCode key)
    {
      this.key = key;
      this.name = name;
      this.m_fightStatus = FightStatus.local;
      this.m_fightMap = FightMap.current;
    }

    protected static void SendAdminCommand(AdminRequestCmd cmd)
    {
      UIManager instance = UIManager.instance;
      if ((UnityEngine.Object) null != (UnityEngine.Object) instance && instance.userInteractionLocked)
        return;
      FightState.instance?.frame?.SendFightAdminCommand((IMessage) cmd);
    }

    protected static bool IsAltDown() => Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);

    protected static bool IsShiftDown() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

    protected PlayerStatus GetPlayerOrOpponent(Event lastEvent = null)
    {
      if ((lastEvent != null ? (lastEvent.alt ? 1 : 0) : (AbstractFightAdminCommand.IsAltDown() ? 1 : 0)) == 0)
        return this.m_fightStatus.GetLocalPlayer();
      int localPlayerId = this.m_fightStatus.localPlayerId;
      foreach (PlayerStatus enumeratePlayer in this.m_fightStatus.EnumeratePlayers())
      {
        if (enumeratePlayer.id != localPlayerId)
          return enumeratePlayer;
      }
      return (PlayerStatus) null;
    }

    protected IEnumerable<Target> EnumerateEntitiesAsTargets<T>() where T : class, IEntity => this.m_fightStatus.EnumerateEntities<T>().Select<T, Target>((Func<T, Target>) (r => new Target((IEntity) r)));

    protected IEnumerable<Target> EnumerateValidCellsFor(IEntityWithBoardPresence entity) => (entity is MechanismStatus ? CellValidForMechanismFilter.EnumerateCells(this.m_fightStatus) : CellValidForCharacterFilter.EnumerateCells(this.m_fightStatus)).Select<Coord, Target>((Func<Coord, Target>) (r => new Target(r)));

    public abstract bool Handle();

    public abstract bool IsRunning();

    protected void EndTargetingPhase()
    {
      FightStatus local = FightStatus.local;
      if (local.currentTurnPlayerId == local.localPlayerId)
      {
        if (!this.m_fightMap.IsInTargetingPhase())
          return;
        this.m_fightMap.SetMovementPhase();
      }
      else
        this.m_fightMap.SetNoInteractionPhase();
    }
  }
}
