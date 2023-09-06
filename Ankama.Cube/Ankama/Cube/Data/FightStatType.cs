// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FightStatType
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public enum FightStatType
  {
    Mvp,
    TotalDamageDealt,
    TurnDamageDealt,
    TotalDamageSustained,
    TurnDamageSustained,
    TotalKills,
    CompanionGiven,
    SpellCasts,
    APUsed,
    MPUsed,
    PlayTime,
    BudgetPointsWon,
    HeroKillsSuffered,
    BudgetPointsDiff,
  }
}
