// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.FloorMechanismStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Entities
{
  public class FloorMechanismStatus : 
    MechanismStatus,
    IEntityWithAssemblage,
    IEntityWithBoardPresence,
    IEntity
  {
    public override EntityType type => EntityType.FloorMechanism;

    public override bool blocksMovement => false;

    private FloorMechanismStatus(int id, int ownerId, int teamId, int teamIndex, int level)
      : base(id, ownerId, teamId, teamIndex, level)
    {
    }

    [NotNull]
    public static FloorMechanismStatus Create(
      int id,
      [NotNull] FloorMechanismDefinition definition,
      int level,
      PlayerStatus owner,
      Vector2Int position)
    {
      int id1 = owner.id;
      int teamId = owner.teamId;
      int teamIndex = owner.teamIndex;
      Area area = definition.areaDefinition.ToArea(position);
      FloorMechanismStatus floorMechanismStatus = new FloorMechanismStatus(id, id1, teamId, teamIndex, level);
      floorMechanismStatus.area = area;
      floorMechanismStatus.definition = (MechanismDefinition) definition;
      return floorMechanismStatus;
    }

    public IReadOnlyList<int> assemblingIds { get; set; }

    public int? activationValue => ((FloorMechanismDefinition) this.definition).activationValue?.GetValueWithLevel(this.level);
  }
}
