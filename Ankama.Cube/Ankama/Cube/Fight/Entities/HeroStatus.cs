// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.HeroStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Fight.Entities
{
  public sealed class HeroStatus : CharacterStatus
  {
    public readonly Gender gender;

    public override EntityType type => EntityType.Hero;

    private HeroStatus(int id, int ownerId, int teamId, int teamIndex, int level, Gender gender)
      : base(id, ownerId, teamId, teamIndex, level)
    {
      this.gender = gender;
    }

    [NotNull]
    public static HeroStatus Create(
      int id,
      [NotNull] WeaponDefinition definition,
      int level,
      Gender gender,
      PlayerStatus owner,
      Vector2Int position)
    {
      int id1 = owner.id;
      int teamId = owner.teamId;
      int teamIndex = owner.teamIndex;
      Area area = definition.areaDefinition.ToArea(position);
      HeroStatus status = new HeroStatus(id, id1, teamId, teamIndex, level, gender);
      status.area = area;
      status.definition = (CharacterDefinition) definition;
      CharacterStatus.InitializeStatus((CharacterStatus) status, (CharacterDefinition) definition);
      return status;
    }
  }
}
