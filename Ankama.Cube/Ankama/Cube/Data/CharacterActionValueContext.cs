// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CharacterActionValueContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;

namespace Ankama.Cube.Data
{
  public sealed class CharacterActionValueContext : DynamicValueFightContext
  {
    public readonly ICharacterEntity relatedCharacterStatus;

    public CharacterActionValueContext(
      [NotNull] FightStatus fightStatus,
      [NotNull] ICharacterEntity relatedCharacterStatus)
      : base(fightStatus, relatedCharacterStatus.ownerId, DynamicValueHolderType.CharacterAction, relatedCharacterStatus.level)
    {
      this.relatedCharacterStatus = relatedCharacterStatus;
    }

    public int? relatedCharacterActionValue => this.relatedCharacterStatus.actionValue;
  }
}
