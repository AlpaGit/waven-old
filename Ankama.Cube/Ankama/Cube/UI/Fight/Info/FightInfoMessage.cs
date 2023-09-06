// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Info.FightInfoMessage
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Protocols.FightCommonProtocol;
using System;

namespace Ankama.Cube.UI.Fight.Info
{
  public struct FightInfoMessage
  {
    public readonly int id;
    public readonly MessageInfoRibbonGroup ribbonGroup;
    public readonly MessageInfoIconType iconType;
    public readonly int countValue;

    public FightInfoMessage(
      int id,
      MessageInfoRibbonGroup ribbonGroup,
      MessageInfoIconType iconType,
      int countValue = 0)
    {
      this.id = id;
      this.ribbonGroup = ribbonGroup;
      this.iconType = iconType;
      this.countValue = countValue;
    }

    public static FightInfoMessage HeroLowLife(MessageInfoRibbonGroup messageGroup) => new FightInfoMessage(78839, messageGroup, MessageInfoIconType.HeroLowLife);

    public static FightInfoMessage ReceivedCompanion(MessageInfoRibbonGroup messageGroup) => new FightInfoMessage(16244, messageGroup, MessageInfoIconType.CompanionReceived);

    public static FightInfoMessage HeroDeath(MessageInfoRibbonGroup messageGroup) => new FightInfoMessage(12120, messageGroup, MessageInfoIconType.HeroDeath);

    public static FightInfoMessage BossPointEarn(MessageInfoRibbonGroup messageGroup, int value) => new FightInfoMessage(45919, messageGroup, MessageInfoIconType.CompanionKilled, value);

    public static FightInfoMessage Score(FightScore score, TeamsScoreModificationReason reason)
    {
      int countValue = 0;
      MessageInfoRibbonGroup ribbonGroup = MessageInfoRibbonGroup.DefaultID;
      if (score.myTeamScore.changed)
      {
        countValue = score.myTeamScore.delta;
        ribbonGroup = MessageInfoRibbonGroup.MyID;
      }
      else
      {
        FightScore.Score opponentTeamScore = score.opponentTeamScore;
        if (opponentTeamScore.changed)
        {
          opponentTeamScore = score.opponentTeamScore;
          countValue = opponentTeamScore.delta;
          ribbonGroup = MessageInfoRibbonGroup.OtherID;
        }
      }
      switch (reason)
      {
        case TeamsScoreModificationReason.FirstVictory:
          return new FightInfoMessage(30898, ribbonGroup, MessageInfoIconType.FirstWin, countValue);
        case TeamsScoreModificationReason.HeroDeath:
          return new FightInfoMessage(583, ribbonGroup, MessageInfoIconType.Win, countValue);
        case TeamsScoreModificationReason.CompanionDeath:
          return new FightInfoMessage(72725, ribbonGroup, MessageInfoIconType.CompanionKilled, countValue);
        default:
          throw new ArgumentOutOfRangeException(nameof (reason), (object) reason, "Unhandled MessageInfoIconType");
      }
    }
  }
}
