// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StatBoard
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Components;
using DG.Tweening;
using Google.Protobuf.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class StatBoard : MonoBehaviour
  {
    [SerializeField]
    private StatPlayerLine m_playerLine;
    [SerializeField]
    private List<StatValueLine> m_statLines;
    [SerializeField]
    private Image m_versusImage;
    [SerializeField]
    private GameObject m_versusBloom;
    [SerializeField]
    private CanvasGroup m_teamScoreLine;
    [SerializeField]
    private RawTextField m_allyTeamScore;
    [SerializeField]
    private RawTextField m_opponentTeamScore;
    [SerializeField]
    private StatData m_statData;
    private Sequence m_openTweenSequence;
    private List<StatBoard.PlayerStatData> m_allies = new List<StatBoard.PlayerStatData>();
    private List<StatBoard.PlayerStatData> m_opponents = new List<StatBoard.PlayerStatData>();
    private List<FightStatType> m_availableStats = new List<FightStatType>();

    public IEnumerator Init(GameStatistics gameStatistics)
    {
      this.m_allies.Clear();
      this.m_opponents.Clear();
      this.m_availableStats.Clear();
      RepeatedField<GameStatistics.Types.PlayerStats> playerStats1 = gameStatistics.PlayerStats;
      for (int index1 = 0; index1 < playerStats1.Count; ++index1)
      {
        GameStatistics.Types.PlayerStats playerStats2 = playerStats1[index1];
        for (int index2 = 0; index2 < playerStats2.Titles.Count; ++index2)
        {
          FightStatType title = (FightStatType) playerStats2.Titles[index2];
          if (!this.m_availableStats.Contains(title))
            this.m_availableStats.Add(title);
        }
        PlayerStatus entityStatus;
        if (GameStatus.GetFightStatus(playerStats2.FightId).TryGetEntity<PlayerStatus>(playerStats2.PlayerId, out entityStatus) && entityStatus.heroStatus != null)
        {
          StatBoard.PlayerStatData playerStatData = new StatBoard.PlayerStatData()
          {
            name = entityStatus.nickname,
            weaponDefinition = (WeaponDefinition) entityStatus.heroStatus.definition,
            playerStats = playerStats2
          };
          if (GameStatus.localPlayerTeamIndex == entityStatus.teamIndex)
          {
            playerStatData.ally = true;
            this.m_allies.Add(playerStatData);
          }
          else
          {
            playerStatData.ally = false;
            this.m_opponents.Add(playerStatData);
          }
        }
      }
      StatBoard.PlayerStatDataComparer statDataComparer = new StatBoard.PlayerStatDataComparer();
      this.m_allies.Sort((IComparer<StatBoard.PlayerStatData>) statDataComparer);
      this.m_opponents.Sort((IComparer<StatBoard.PlayerStatData>) statDataComparer);
      if (GameStatus.fightType == FightType.TeamVersus)
      {
        this.m_teamScoreLine.gameObject.SetActive(true);
        this.m_allyTeamScore.SetText(GameStatus.allyTeamPoints.ToString());
        this.m_opponentTeamScore.SetText(GameStatus.opponentTeamPoints.ToString());
      }
      else
        this.m_teamScoreLine.gameObject.SetActive(false);
      bool displayOpponent = this.m_opponents.Count > 0 || GameStatus.fightType != FightType.BossFight;
      this.m_versusImage.gameObject.SetActive(displayOpponent);
      this.m_versusBloom.gameObject.SetActive(displayOpponent);
      yield return (object) this.m_playerLine.Init(this.m_allies, this.m_opponents, displayOpponent);
      this.m_statLines[0].Init(this.m_allies, this.m_opponents, FightStatType.TotalDamageDealt, displayOpponent);
      this.m_statLines[1].Init(this.m_allies, this.m_opponents, FightStatType.TotalDamageSustained, displayOpponent);
      this.m_statLines[2].Init(this.m_allies, this.m_opponents, FightStatType.PlayTime, displayOpponent);
      this.m_statLines[3].Init(this.m_allies, this.m_opponents, FightStatType.TotalKills, displayOpponent);
      this.InitOptionnalLine(this.m_statLines[4], FightStatType.CompanionGiven, displayOpponent);
      this.InitOptionnalLine(this.m_statLines[5], FightStatType.BudgetPointsDiff, displayOpponent);
      this.InitOptionnalLine(this.m_statLines[6], FightStatType.BudgetPointsWon, displayOpponent);
    }

    private void InitOptionnalLine(StatValueLine line, FightStatType type, bool displayOpponent)
    {
      if (this.m_availableStats.Contains(type))
      {
        line.Init(this.m_allies, this.m_opponents, type, displayOpponent);
        line.gameObject.SetActive(true);
      }
      else
        line.gameObject.SetActive(false);
    }

    public Sequence Open()
    {
      if (this.m_openTweenSequence != null && this.m_openTweenSequence.IsActive())
        this.m_openTweenSequence.Kill();
      this.m_openTweenSequence = DOTween.Sequence();
      int num1 = this.m_statLines.Count + 2;
      float duration = Mathf.Min(this.m_statData.openBoardDuration, this.m_statData.openBoardLineTweenDuration);
      float num2 = (this.m_statData.openBoardDuration - duration) / (float) num1;
      float openBoardDelay = this.m_statData.openBoardDelay;
      this.m_playerLine.canvasGroup.alpha = 0.0f;
      this.m_openTweenSequence.Insert(openBoardDelay, (Tween) this.m_playerLine.canvasGroup.DOFade(1f, duration).SetEase<Tweener>(this.m_statData.openBoardLineTweenEase));
      float atPosition = openBoardDelay + num2;
      for (int index = 0; index < this.m_statLines.Count; ++index)
      {
        StatValueLine statLine = this.m_statLines[index];
        statLine.canvasGroup.alpha = 0.0f;
        this.m_openTweenSequence.Insert(atPosition, (Tween) statLine.canvasGroup.DOFade(1f, duration).SetEase<Tweener>(this.m_statData.openBoardLineTweenEase));
        atPosition += num2;
      }
      this.m_teamScoreLine.alpha = 0.0f;
      this.m_openTweenSequence.Insert(atPosition, (Tween) this.m_teamScoreLine.DOFade(1f, duration).SetEase<Tweener>(this.m_statData.openBoardLineTweenEase));
      return this.m_openTweenSequence;
    }

    public struct PlayerStatData
    {
      public bool ally;
      public string name;
      public WeaponDefinition weaponDefinition;
      public GameStatistics.Types.PlayerStats playerStats;
    }

    public class PlayerStatDataComparer : IComparer<StatBoard.PlayerStatData>
    {
      public int Compare(StatBoard.PlayerStatData x, StatBoard.PlayerStatData y) => x.playerStats.FightId.CompareTo(y.playerStats.FightId);
    }
  }
}
