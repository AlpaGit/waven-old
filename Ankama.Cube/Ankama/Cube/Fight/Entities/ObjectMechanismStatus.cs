// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.ObjectMechanismStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Fight.Entities
{
  public class ObjectMechanismStatus : 
    MechanismStatus,
    IEntityWithLife,
    IEntity,
    IEntityTargetableByAction,
    IEntityWithBoardPresence
  {
    public override EntityType type => EntityType.ObjectMechanism;

    public int baseLife => 0;

    public int life => this.GetCarac(CaracId.Life, 0);

    public bool hasArmor => this.GetCarac(CaracId.Armor, 0) > 0;

    public int armor => this.GetCarac(CaracId.Armor, 0);

    public int armoredLife => this.life + this.armor;

    public int resistance => this.GetCarac(CaracId.Resistance, 0);

    public int hitLimit => this.GetCarac(CaracId.HitLimit, 0);

    public bool wounded => this.life < this.baseLife;

    public override bool blocksMovement => true;

    public ObjectMechanismStatus(int id, int ownerId, int teamId, int teamIndex, int level)
      : base(id, ownerId, teamId, teamIndex, level)
    {
    }

    [NotNull]
    public static ObjectMechanismStatus Create(
      int id,
      [NotNull] ObjectMechanismDefinition definition,
      int level,
      PlayerStatus owner,
      Vector2Int position)
    {
      int id1 = owner.id;
      int teamId = owner.teamId;
      int teamIndex = owner.teamIndex;
      Area area = definition.areaDefinition.ToArea(position);
      int valueWithLevel = definition.baseMecaLife.GetValueWithLevel(level);
      ObjectMechanismStatus objectMechanismStatus = new ObjectMechanismStatus(id, id1, teamId, teamIndex, level);
      objectMechanismStatus.area = area;
      objectMechanismStatus.definition = (MechanismDefinition) definition;
      objectMechanismStatus.SetCarac(CaracId.Life, valueWithLevel);
      return objectMechanismStatus;
    }
  }
}
