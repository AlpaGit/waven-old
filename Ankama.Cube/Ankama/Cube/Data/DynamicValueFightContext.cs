// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DynamicValueFightContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using JetBrains.Annotations;

namespace Ankama.Cube.Data
{
  public abstract class DynamicValueFightContext : DynamicValueContext
  {
    public readonly FightStatus fightStatus;
    public readonly int playerId;

    protected DynamicValueFightContext(
      [NotNull] FightStatus fightStatus,
      int playerId,
      DynamicValueHolderType type,
      int level)
      : base(type, level)
    {
      this.fightStatus = fightStatus;
      this.playerId = playerId;
    }
  }
}
