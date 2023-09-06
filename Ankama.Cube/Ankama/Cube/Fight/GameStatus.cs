// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.GameStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Fight
{
  public static class GameStatus
  {
    public static FightType fightType;
    public static FightDefinition fightDefinition;
    public static FightStatus[] fights;
    public static bool hasEnded;
    public static int localPlayerTeamIndex = -1;
    public static int allyTeamPoints;
    public static int opponentTeamPoints;

    public static bool isPvP
    {
      get
      {
        switch (GameStatus.fightType)
        {
          case FightType.None:
          case FightType.Versus:
          case FightType.TeamVersus:
            return true;
          case FightType.BossFight:
            return false;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    public static void Initialize(
      FightType type,
      FightDefinition definition,
      FightStatus[] fightStatusArray)
    {
      GameStatus.fightType = type;
      GameStatus.fightDefinition = definition;
      GameStatus.fights = fightStatusArray;
      GameStatus.hasEnded = false;
      GameStatus.localPlayerTeamIndex = -1;
      GameStatus.allyTeamPoints = 0;
      GameStatus.opponentTeamPoints = 0;
    }

    public static FightStatus GetFightStatus(int index) => GameStatus.fights[index];

    public static DirectionAngle GetMapRotation([NotNull] PlayerStatus localPlayerStatus)
    {
      switch (GameStatus.fightType)
      {
        case FightType.None:
          return DirectionAngle.None;
        case FightType.Versus:
          return localPlayerStatus.index != 0 ? DirectionAngle.Clockwise180 : DirectionAngle.None;
        case FightType.BossFight:
          switch (FightStatus.local.fightId)
          {
            case 0:
              return DirectionAngle.None;
            case 1:
              return DirectionAngle.Clockwise90;
            case 2:
              return DirectionAngle.Clockwise180;
            case 3:
              return DirectionAngle.CounterClockwise90;
            default:
              throw new ArgumentOutOfRangeException();
          }
        case FightType.TeamVersus:
          return localPlayerStatus.teamIndex != 0 ? DirectionAngle.Clockwise180 : DirectionAngle.None;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
