// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.CompanionStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Fight.Entities
{
  public sealed class CompanionStatus : CharacterStatus
  {
    public override EntityType type => EntityType.Companion;

    private CompanionStatus(int id, int ownerId, int teamId, int teamIndex, int level)
      : base(id, ownerId, teamId, teamIndex, level)
    {
    }

    [NotNull]
    public static CompanionStatus Create(
      int id,
      [NotNull] CompanionDefinition definition,
      int level,
      PlayerStatus owner,
      Vector2Int position)
    {
      int id1 = owner.id;
      int teamId = owner.teamId;
      int teamIndex = owner.teamIndex;
      Area area = definition.areaDefinition.ToArea(position);
      CompanionStatus status = new CompanionStatus(id, id1, teamId, teamIndex, level);
      status.area = area;
      status.definition = (CharacterDefinition) definition;
      CharacterStatus.InitializeStatus((CharacterStatus) status, (CharacterDefinition) definition);
      return status;
    }
  }
}
