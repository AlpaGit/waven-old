// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Fight.Events;
using Ankama.Cube.Network;
using Ankama.Cube.Protocols.FightProtocol;
using Ankama.Cube.UI;
using Google.Protobuf;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight
{
  public sealed class FightFrame : CubeMessageFrame
  {
    private readonly List<FightEvent> m_fightEventBuffer = new List<FightEvent>();
    public Action<int> onOtherPlayerLeftFight;
    public Action<FightSnapshot> onFightSnapshot;

    public FightFrame()
    {
      this.WhenReceiveEnqueue<FightEventsEvent>(new Action<FightEventsEvent>(this.OnFightEventsEvent));
      this.WhenReceiveEnqueue<CommandHandledEvent>(new Action<CommandHandledEvent>(FightFrame.OnCommandHandledEvent));
      this.WhenReceiveEnqueue<PlayerLeftFightEvent>(new Action<PlayerLeftFightEvent>(this.OnPlayerLeftFightEvent));
      this.WhenReceiveEnqueue<FightSnapshotEvent>(new Action<FightSnapshotEvent>(this.OnFightSnapshotEvent));
    }

    private void OnPlayerLeftFightEvent(PlayerLeftFightEvent obj)
    {
      Action<int> otherPlayerLeftFight = this.onOtherPlayerLeftFight;
      if (otherPlayerLeftFight == null)
        return;
      otherPlayerLeftFight(obj.PlayerId);
    }

    private void OnFightSnapshotEvent(FightSnapshotEvent snapshotEvent)
    {
      if (this.onFightSnapshot == null)
        return;
      RepeatedField<FightSnapshot> fightsSnapshots = snapshotEvent.FightsSnapshots;
      int count = fightsSnapshots.Count;
      for (int index = 0; index < count; ++index)
        this.onFightSnapshot(fightsSnapshots[index]);
    }

    private void OnFightEventsEvent(FightEventsEvent obj)
    {
      this.m_fightEventBuffer.Clear();
      RepeatedField<FightEventData> events = obj.Events;
      int count = events.Count;
      for (int index = 0; index < count; ++index)
        this.m_fightEventBuffer.Add(FightEventFactory.FromProto(events[index]));
      FightLogicExecutor.ProcessFightEvents(obj.FightId, this.m_fightEventBuffer);
    }

    private static void OnCommandHandledEvent(CommandHandledEvent obj)
    {
      UIManager instance = UIManager.instance;
      if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
        instance.ReleaseUserInteractionLock();
      switch (FightCastManager.currentCastType)
      {
        case FightCastManager.CurrentCastType.None:
          break;
        case FightCastManager.CurrentCastType.Spell:
          FightCastManager.StopCastingSpell(true);
          break;
        case FightCastManager.CurrentCastType.Companion:
          FightCastManager.StopInvokingCompanion(true);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void SendFightAdminCommand(IMessage cmd) => this.SendCommandAndLockUserInteraction(cmd);

    public void SendEntityMovement(int entityId, Vector2Int[] path)
    {
      MoveEntityCmd cmd = new MoveEntityCmd()
      {
        EntityId = entityId
      };
      int length = path.Length;
      for (int index = 1; index < length; ++index)
      {
        Vector2Int vector2Int = path[index];
        cmd.Path.Add(vector2Int.ToCellCoord());
      }
      this.SendCommandAndLockUserInteraction((IMessage) cmd);
    }

    public void SendEntityAttack(int attackerId, Vector2Int[] path, int defenderId)
    {
      MoveEntityCmd cmd = new MoveEntityCmd()
      {
        EntityId = attackerId,
        EntityToAttackId = new int?(defenderId)
      };
      int length = path.Length;
      for (int index = 1; index < length; ++index)
      {
        Vector2Int vector2Int = path[index];
        cmd.Path.Add(vector2Int.ToCellCoord());
      }
      this.SendCommandAndLockUserInteraction((IMessage) cmd);
    }

    public void SendSpell(int spellInstanceId, Target[] targets)
    {
      PlaySpellCmd cmd = new PlaySpellCmd()
      {
        SpellId = spellInstanceId
      };
      int length = targets.Length;
      for (int index = 0; index < length; ++index)
      {
        Target target = targets[index];
        cmd.CastTargets.Add(target.ToCastTarget());
      }
      this.SendCommandAndLockUserInteraction((IMessage) cmd);
    }

    public void SendInvokeCompanion(int companionDefId, Coord coord) => this.SendCommandAndLockUserInteraction((IMessage) new InvokeCompanionCmd()
    {
      CompanionDefId = companionDefId,
      Coords = coord.ToCellCoord()
    });

    public void SendGiveCompanion(int fightId, int playerId, int companionDefinitionId) => this.SendCommandAndLockUserInteraction((IMessage) new GiveCompanionCmd()
    {
      CompanionDefId = companionDefinitionId,
      TargetFightId = fightId,
      TargetPlayerId = playerId
    });

    public void SendUseReserve() => this.SendCommandAndLockUserInteraction((IMessage) new UseReserveCmd());

    public void SendResign() => this.SendCommandAndLockUserInteraction((IMessage) new ResignCmd());

    public void SendTurnEnd(int turnIndex) => this.SendCommandAndLockUserInteraction((IMessage) new EndOfTurnCmd()
    {
      TurnIndex = turnIndex
    });

    public void SendPlayerReady() => this.m_connection.Write((IMessage) new PlayerReadyCmd());

    public void SendLeave() => this.m_connection.Write((IMessage) new LeaveCmd());

    public void SendFightSnapshotRequest() => this.m_connection.Write((IMessage) new GetFightSnapshotCmd());

    private void SendCommandAndLockUserInteraction(IMessage cmd)
    {
      UIManager instance = UIManager.instance;
      if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
        instance.LockUserInteraction();
      this.m_connection.Write(cmd);
    }

    public override void Dispose()
    {
      this.onFightSnapshot = (Action<FightSnapshot>) null;
      this.onOtherPlayerLeftFight = (Action<int>) null;
      base.Dispose();
    }
  }
}
