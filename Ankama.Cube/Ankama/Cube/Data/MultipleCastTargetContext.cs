// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MultipleCastTargetContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;

namespace Ankama.Cube.Data
{
  public sealed class MultipleCastTargetContext : CastTargetContext
  {
    public MultipleCastTargetContext(
      FightStatus fightStatus,
      int playerId,
      DynamicValueHolderType type,
      int spellDefinitionId,
      int level,
      int instanceId,
      int expectedTargetCount)
      : base(fightStatus, playerId, type, spellDefinitionId, level, instanceId, expectedTargetCount)
    {
    }
  }
}
