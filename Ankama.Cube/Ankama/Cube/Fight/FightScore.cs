// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightScore
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight
{
  public struct FightScore
  {
    public readonly FightScore.Score myTeamScore;
    public readonly FightScore.Score opponentTeamScore;

    public FightScore(FightScore.Score myTeamScore, FightScore.Score opponentTeamScore)
    {
      this.myTeamScore = myTeamScore;
      this.opponentTeamScore = opponentTeamScore;
    }

    public struct Score
    {
      public readonly int scoreBefore;
      public readonly int scoreAfter;

      public Score(int scoreBefore, int scoreAfter)
      {
        this.scoreBefore = scoreBefore;
        this.scoreAfter = scoreAfter;
      }

      public int delta => this.scoreAfter - this.scoreBefore;

      public bool changed => this.scoreAfter != this.scoreBefore;
    }
  }
}
