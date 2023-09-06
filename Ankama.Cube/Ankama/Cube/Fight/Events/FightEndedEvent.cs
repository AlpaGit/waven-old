// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FightEndedEvent
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class FightEndedEvent : FightEvent
  {
    private readonly List<IEntityWithBoardPresence> m_removedEntities = new List<IEntityWithBoardPresence>();

    public int? winningTeamId { get; private set; }

    public IReadOnlyList<int> winningPlayers { get; private set; }

    public FightEndedEvent(
      int eventId,
      int? parentEventId,
      int? winningTeamId,
      IReadOnlyList<int> winningPlayers)
      : base(FightEventData.Types.EventType.FightEnded, eventId, parentEventId)
    {
      this.winningTeamId = winningTeamId;
      this.winningPlayers = winningPlayers;
    }

    public FightEndedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.FightEnded, proto)
    {
      this.winningTeamId = proto.OptInt1;
      this.winningPlayers = (IReadOnlyList<int>) proto.IntList1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      fightStatus.endReason = this.GetEndReason(fightStatus);
      switch (GameStatus.fightType)
      {
        case FightType.BossFight:
          using (IEnumerator<IEntityWithBoardPresence> enumerator = fightStatus.EnumerateEntities<IEntityWithBoardPresence>().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              IEntityWithBoardPresence current = enumerator.Current;
              this.m_removedEntities.Add(current);
              fightStatus.RemoveEntity(current.id);
            }
            break;
          }
        case FightType.TeamVersus:
          using (IEnumerator<IEntityWithBoardPresence> enumerator = fightStatus.EnumerateEntities<IEntityWithBoardPresence>().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              IEntityWithBoardPresence current = enumerator.Current;
              if (current is IEntityWithOwner entityWithOwner)
              {
                int teamId = entityWithOwner.teamId;
                int? winningTeamId = this.winningTeamId;
                int valueOrDefault = winningTeamId.GetValueOrDefault();
                if (teamId == valueOrDefault & winningTeamId.HasValue)
                  continue;
              }
              this.m_removedEntities.Add(current);
              fightStatus.RemoveEntity(current.id);
            }
            break;
          }
        default:
          throw new ArgumentOutOfRangeException();
      }
      if (fightStatus != FightStatus.local)
        return;
      FightMap current1 = FightMap.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current1)
        current1.Stop();
      FightUIRework instance = FightUIRework.instance;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) instance))
        return;
      instance.SetResignButtonEnabled(false);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      MonoBehaviour monoBehaviour = Main.monoBehaviour;
      List<IEntityWithBoardPresence> removedEntities = this.m_removedEntities;
      int count = removedEntities.Count;
      for (int index = 0; index < count; ++index)
      {
        IEntityWithBoardPresence entity = removedEntities[index];
        monoBehaviour.StartCoroutineImmediateSafe(FightEndedEvent.RemoveEntityFromBoard(entity));
      }
      if (GameStatus.fightType == FightType.BossFight && fightStatus.endReason == FightStatusEndReason.Lose)
      {
        FightMap current = FightMap.current;
        if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        {
          PlayerStatus entityStatus;
          if (fightStatus.TryGetEntity<PlayerStatus>((Predicate<PlayerStatus>) (p => p.teamIndex == GameStatus.localPlayerTeamIndex), out entityStatus))
          {
            HeroStatus heroStatus = entityStatus.heroStatus;
            if (heroStatus != null)
            {
              Vector2Int refCoord = heroStatus.area.refCoord;
              current.AddHeroLostFeedback(refCoord);
            }
          }
          monoBehaviour.StartCoroutineImmediateSafe(current.ClearMonsterSpawnCells(fightStatus.fightId));
        }
      }
      if (fightStatus == FightStatus.local && !GameStatus.hasEnded)
        yield return (object) FightEndedEvent.DisplayFightResultFeedback(fightStatus);
    }

    private FightStatusEndReason GetEndReason(FightStatus fightStatus)
    {
      int count = ((IReadOnlyCollection<int>) this.winningPlayers).Count;
      if (count == 0)
        return FightStatusEndReason.Draw;
      int localPlayerId = fightStatus.localPlayerId;
      for (int index = 0; index < count; ++index)
      {
        if (this.winningPlayers[index] == localPlayerId)
          return FightStatusEndReason.Win;
      }
      return FightStatusEndReason.Lose;
    }

    private static IEnumerator DisplayFightResultFeedback(FightStatus fightStatus)
    {
      switch (GameStatus.fightType)
      {
        case FightType.None:
          break;
        case FightType.Versus:
          break;
        case FightType.BossFight:
          if (fightStatus.endReason != FightStatusEndReason.Lose)
            break;
          goto case FightType.TeamVersus;
        case FightType.TeamVersus:
          FightState instance = FightState.instance;
          if (instance != null)
          {
            yield return (object) instance.ShowFightEndFeedback(fightStatus.endReason);
            break;
          }
          Log.Error("Could not find fight state.", 189, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FightEndedEvent.cs");
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static IEnumerator RemoveEntityFromBoard(IEntityWithBoardPresence entity)
    {
      IsoObject view = entity.view;
      if (view is ICharacterObject characterObject)
        yield return (object) characterObject.Die();
      view.DetachFromCell();
      view.Destroy();
    }
  }
}
