// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TeamTurnStartedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps;
using Ankama.Cube.UI.Fight;
using System;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class TeamTurnStartedEvent : FightEvent
  {
    public int turnIndex { get; private set; }

    public int teamIndex { get; private set; }

    public int turnDuration { get; private set; }

    public TeamTurnStartedEvent(
      int eventId,
      int? parentEventId,
      int turnIndex,
      int teamIndex,
      int turnDuration)
      : base(FightEventData.Types.EventType.TeamTurnStarted, eventId, parentEventId)
    {
      this.turnIndex = turnIndex;
      this.teamIndex = teamIndex;
      this.turnDuration = turnDuration;
    }

    public TeamTurnStartedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.TeamTurnStarted, proto)
    {
      this.turnIndex = proto.Int1;
      this.teamIndex = proto.Int2;
      this.turnDuration = proto.Int3;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      fightStatus.turnIndex = this.turnIndex;
      if (fightStatus != FightStatus.local)
        return;
      FightUIRework instance = FightUIRework.instance;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) instance))
        return;
      bool isLocalPlayerTeam = GameStatus.localPlayerTeamIndex == this.teamIndex;
      instance.StartTurn(this.turnIndex, this.turnDuration, isLocalPlayerTeam);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      if (fightStatus == FightStatus.local)
      {
        FightUIRework instance = FightUIRework.instance;
        if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
        {
          switch (GameStatus.fightType)
          {
            case FightType.Versus:
              break;
            case FightType.BossFight:
              if (GameStatus.localPlayerTeamIndex == this.teamIndex)
              {
                TurnFeedbackUI.Type type = fightStatus.isEnded ? TurnFeedbackUI.Type.PlayerTeam : TurnFeedbackUI.Type.Player;
                yield return (object) instance.ShowTurnFeedback(type, 61373);
                break;
              }
              yield return (object) instance.ShowTurnFeedback(TurnFeedbackUI.Type.Boss, 30091);
              break;
            case FightType.TeamVersus:
              if (GameStatus.localPlayerTeamIndex == this.teamIndex)
              {
                TurnFeedbackUI.Type type = fightStatus.isEnded ? TurnFeedbackUI.Type.PlayerTeam : TurnFeedbackUI.Type.Player;
                yield return (object) instance.ShowTurnFeedback(type, 61373);
                break;
              }
              yield return (object) instance.ShowTurnFeedback(TurnFeedbackUI.Type.OpponentTeam, 30091);
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
        FightMap current = FightMap.current;
        if ((UnityEngine.Object) null != (UnityEngine.Object) current)
          current.SetTurnIndex(this.turnIndex);
      }
    }

    public override bool CanBeGroupedWith(FightEvent other) => false;

    public override bool SynchronizeExecution() => true;
  }
}
