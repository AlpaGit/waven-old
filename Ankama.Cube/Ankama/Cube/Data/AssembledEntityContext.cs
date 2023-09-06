// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AssembledEntityContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;

namespace Ankama.Cube.Data
{
  public sealed class AssembledEntityContext : DynamicValueFightContext
  {
    public readonly IEntityWithAssemblage assembling;

    public AssembledEntityContext(
      [NotNull] FightStatus fightStatus,
      int playerId,
      IEntityWithAssemblage assembling,
      int level)
      : base(fightStatus, playerId, DynamicValueHolderType.AssembledEntity, level)
    {
      this.assembling = assembling;
    }
  }
}
