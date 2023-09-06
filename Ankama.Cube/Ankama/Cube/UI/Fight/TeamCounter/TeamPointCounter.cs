// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.TeamCounter.TeamPointCounter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.TeamCounter
{
  public class TeamPointCounter : MonoBehaviour
  {
    [SerializeField]
    private UISpriteTextRenderer m_team0Score;
    [SerializeField]
    private UISpriteTextRenderer m_team1Score;
    [Header("Feedback")]
    [SerializeField]
    private TeamCounterFeedback m_team0Feedback;
    [SerializeField]
    private TeamCounterFeedback m_team1Feedback;
    [SerializeField]
    private UISpriteTextRenderer m_team0PointFeedback;
    [SerializeField]
    private UISpriteTextRenderer m_team1PointFeedback;
    [SerializeField]
    private TeamCounterFeedback m_team0CrownFeedback;
    [SerializeField]
    private TeamCounterFeedback m_team1CrownFeedback;
    private bool m_hasFirstVictory;

    public bool HasFinishedAFight() => this.m_hasFirstVictory;

    public void OnFirstVictory() => this.m_hasFirstVictory = true;

    public void OnScoreChange(FightScore score)
    {
      string str1 = score.myTeamScore.scoreAfter.ToString();
      string str2 = score.opponentTeamScore.scoreAfter.ToString();
      this.m_team0Score.text = str1;
      this.m_team1Score.text = str2;
      this.m_team0PointFeedback.text = str1;
      this.m_team1PointFeedback.text = str2;
      if (score.myTeamScore.changed)
      {
        this.m_team0CrownFeedback.PlayFeedback();
        this.m_team0Feedback.PlayFeedback();
      }
      if (!score.myTeamScore.changed)
        return;
      this.m_team1CrownFeedback.PlayFeedback();
      this.m_team1Feedback.PlayFeedback();
    }

    public void InitialiseScore(int score0, int score1)
    {
      this.m_team0Score.text = score0.ToString();
      this.m_team1Score.text = score1.ToString();
      this.m_team0CrownFeedback.SetOff();
      this.m_team1CrownFeedback.SetOff();
      this.m_team0Feedback.SetOff();
      this.m_team1Feedback.SetOff();
    }
  }
}
