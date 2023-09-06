// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CastTargetContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.States;

namespace Ankama.Cube.Data
{
  public abstract class CastTargetContext : DynamicValueFightContext
  {
    public readonly int spellDefinitionId;
    public readonly int expectedTargetCount;
    private readonly Target[] m_targets;
    public readonly int instanceId;

    public int selectedTargetCount { get; private set; }

    protected CastTargetContext(
      FightStatus fightStatus,
      int playerId,
      DynamicValueHolderType type,
      int spellDefinitionId,
      int level,
      int instanceId,
      int expectedTargetCount)
      : base(fightStatus, playerId, type, level)
    {
      this.spellDefinitionId = spellDefinitionId;
      this.expectedTargetCount = expectedTargetCount;
      this.instanceId = instanceId;
      this.selectedTargetCount = 0;
      this.m_targets = new Target[expectedTargetCount];
    }

    public Target GetTarget(int index) => this.m_targets[index];

    public void SelectTarget(Target target)
    {
      this.m_targets[this.selectedTargetCount] = target;
      ++this.selectedTargetCount;
    }

    public void SendCommand() => FightState.instance?.frame.SendSpell(this.instanceId, this.m_targets);
  }
}
