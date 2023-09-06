// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TeamsScoreModificationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class TeamsScoreModificationEvent : FightEvent
  {
    public int firstTeamScoreBefore { get; private set; }

    public int firstTeamScoreAfter { get; private set; }

    public int secondTeamScoreBefore { get; private set; }

    public int secondTeamScoreAfter { get; private set; }

    public TeamsScoreModificationReason reason { get; private set; }

    public int relatedFightId { get; private set; }

    public IReadOnlyList<int> relatedPlayersId { get; private set; }

    public TeamsScoreModificationEvent(
      int eventId,
      int? parentEventId,
      int firstTeamScoreBefore,
      int firstTeamScoreAfter,
      int secondTeamScoreBefore,
      int secondTeamScoreAfter,
      TeamsScoreModificationReason reason,
      int relatedFightId,
      IReadOnlyList<int> relatedPlayersId)
      : base(FightEventData.Types.EventType.TeamsScoreModification, eventId, parentEventId)
    {
      this.firstTeamScoreBefore = firstTeamScoreBefore;
      this.firstTeamScoreAfter = firstTeamScoreAfter;
      this.secondTeamScoreBefore = secondTeamScoreBefore;
      this.secondTeamScoreAfter = secondTeamScoreAfter;
      this.reason = reason;
      this.relatedFightId = relatedFightId;
      this.relatedPlayersId = relatedPlayersId;
    }

    public TeamsScoreModificationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.TeamsScoreModification, proto)
    {
      this.firstTeamScoreBefore = proto.Int1;
      this.firstTeamScoreAfter = proto.Int2;
      this.secondTeamScoreBefore = proto.Int3;
      this.secondTeamScoreAfter = proto.Int4;
      this.relatedFightId = proto.Int5;
      this.reason = proto.TeamsScoreModificationReason1;
      this.relatedPlayersId = (IReadOnlyList<int>) proto.IntList1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      FightScore score = this.GetScore();
      GameStatus.allyTeamPoints = score.myTeamScore.scoreAfter;
      GameStatus.opponentTeamPoints = score.opponentTeamScore.scoreAfter;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      string playerName = this.GetPlayerName(GameStatus.GetFightStatus(this.relatedFightId));
      FightUIRework instance = FightUIRework.instance;
      if ((Object) null != (Object) instance)
      {
        FightScore score = this.GetScore();
        instance.SetScore(score, playerName, this.reason);
        yield break;
      }
    }

    private string GetPlayerName(FightStatus concernedFight)
    {
      IReadOnlyList<int> relatedPlayersId = this.relatedPlayersId;
      string playerName;
      switch (((IReadOnlyCollection<int>) relatedPlayersId).Count)
      {
        case 0:
          Log.Warning("No player was specified as the source of a team score modification.", 46, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TeamsScoreModificationEvent.cs");
          playerName = string.Empty;
          break;
        case 1:
          PlayerStatus entityStatus1;
          if (concernedFight.TryGetEntity<PlayerStatus>(relatedPlayersId[0], out entityStatus1))
          {
            playerName = entityStatus1.nickname;
            break;
          }
          Log.Error(string.Format("Could not find player with id {0}.", (object) relatedPlayersId[0]), 59, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TeamsScoreModificationEvent.cs");
          playerName = string.Empty;
          break;
        default:
          PlayerStatus entityStatus2;
          playerName = !concernedFight.TryGetEntity<PlayerStatus>(relatedPlayersId[0], out entityStatus2) ? string.Empty : "Team #" + (object) entityStatus2.teamIndex;
          break;
      }
      return playerName;
    }

    private FightScore GetScore()
    {
      FightScore.Score score1 = new FightScore.Score(this.firstTeamScoreBefore, this.firstTeamScoreAfter);
      FightScore.Score score2 = new FightScore.Score(this.secondTeamScoreBefore, this.secondTeamScoreAfter);
      return GameStatus.localPlayerTeamIndex != 0 ? new FightScore(score2, score1) : new FightScore(score1, score2);
    }
  }
}
