// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Tools.ToolFightCharacterStatus
// Assembly: Ankama.Cube.Tools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E5F9BDA-0991-43A6-B1CC-DE1630412C37
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Ankama.Cube.Tools.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;

namespace Ankama.Cube.Tools
{
  public sealed class ToolFightCharacterStatus : 
    ToolCharacterStatus,
    ICharacterEntity,
    IEntityWithOwner,
    IEntityWithTeam,
    IEntity,
    IEntityWithLevel,
    IEntityWithMovement,
    IEntityWithBoardPresence,
    IEntityWithAction,
    IEntityTargetableByAction
  {
    public ToolFightCharacterStatus(
      int id,
      EntityType entityType,
      ActionType actionType,
      int movementPoints,
      int rangeMin,
      int rangeMax,
      int life)
      : base(id)
    {
      this.type = entityType;
      this.SetMovementPoints(movementPoints);
      this.SetActionType(actionType);
      this.SetRange(rangeMin, rangeMax);
      this.SetLife(life);
    }

    public override EntityType type { get; }

    public int? actionValue { get; } = new int?(0);

    public ActionType actionType { get; private set; }

    public bool hasRange
    {
      get
      {
        int carac1 = this.GetCarac(CaracId.RangeMin, 0);
        int carac2 = this.GetCarac(CaracId.RangeMax, 0);
        return carac1 > 1 || carac2 > carac1;
      }
    }

    public int rangeMin { get; private set; }

    public int rangeMax { get; private set; }

    public int physicalDamageBoost { get; }

    public int physicalHealBoost { get; }

    public bool actionUsed { get; set; }

    public bool canDoActionOnTarget { get; } = true;

    public IEntitySelector customActionTarget { get; }

    public void SetMovementPoints(int value)
    {
      this.SetCarac(CaracId.MovementPoints, value);
      this.baseMovementPoints = value;
      this.movementPoints = value;
    }

    public void SetActionType(ActionType value) => this.actionType = value;

    public void SetRange(int min, int max)
    {
      this.SetCarac(CaracId.RangeMin, min);
      this.SetCarac(CaracId.RangeMax, max);
      this.rangeMin = min;
      this.rangeMax = max;
    }

    public void SetLife(int value) => this.SetCarac(CaracId.Life, value);

    public int teamId { get; }

    public int teamIndex { get; }

    public int ownerId { get; }

    public int baseMovementPoints { get; private set; }

    public int movementPoints { get; private set; }

    public bool canMove { get; } = true;

    public override bool blocksMovement { get; } = true;
  }
}
