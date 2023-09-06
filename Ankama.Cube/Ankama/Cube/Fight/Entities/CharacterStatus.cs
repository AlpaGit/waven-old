// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.CharacterStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Maps.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Entities
{
  public abstract class CharacterStatus : 
    EntityStatus,
    ICharacterEntity,
    IEntityWithOwner,
    IEntityWithTeam,
    IEntity,
    IEntityWithLevel,
    IEntityWithMovement,
    IEntityWithBoardPresence,
    IEntityWithAction,
    IEntityWithLife,
    IEntityWithFamilies,
    IEntityWithElementaryState,
    IDynamicValueSource,
    IEntityTargetableByAction
  {
    private ElementaryStates m_elemState;

    public CharacterDefinition definition { get; protected set; }

    public int teamId { get; }

    public int teamIndex { get; }

    public int ownerId { get; }

    public int baseLife => this.GetCarac(CaracId.LifeMax, this.definition.life.GetValueWithLevel(this.level));

    public int life => this.GetCarac(CaracId.Life, 0);

    public bool hasArmor => this.GetCarac(CaracId.Armor, 0) > 0;

    public int armor => this.GetCarac(CaracId.Armor, 0);

    public int armoredLife => this.life + this.armor;

    public int resistance => this.GetCarac(CaracId.Resistance, 0);

    public int hitLimit => this.GetCarac(CaracId.HitLimit, 0);

    public int physicalDamageBoost => this.GetCarac(CaracId.PhysicalDamageModifier, 0);

    public int physicalHealBoost => this.GetCarac(CaracId.PhysicalHealModifier, 0);

    public bool wounded => this.life < this.baseLife;

    public Area area { get; protected set; }

    public IsoObject view { get; set; }

    public bool blocksMovement => true;

    public int baseMovementPoints => this.definition.movementPoints.GetValueWithLevel(this.level);

    public int movementPoints => this.GetCarac(CaracId.MovementPoints, 0);

    public ActionType actionType => !((Object) this.definition != (Object) null) ? ActionType.None : this.definition.actionType;

    public int? actionValue => this.definition.actionValue?.GetValueWithLevel(this.level);

    public bool hasRange => this.definition.actionRange != null;

    public int rangeMin => this.GetCarac(CaracId.RangeMin, 0);

    public int rangeMax => this.GetCarac(CaracId.RangeMax, 0);

    public IReadOnlyList<Family> families => this.definition.families;

    public int level { get; }

    public IReadOnlyList<ILevelOnlyDependant> dynamicValues => this.definition.precomputedData.dynamicValueReferences;

    public void ChangeElementaryState(ElementaryStates elemState) => this.m_elemState = elemState;

    public bool HasElementaryState(ElementaryStates elemState) => this.m_elemState == elemState;

    public bool actionUsed { get; set; }

    public bool canMove => this.movementPoints > 0 && !this.HasAnyProperty(PropertiesUtility.propertiesWhichPreventVoluntaryMove);

    public bool canDoActionOnTarget => this.definition.actionType != ActionType.None && !this.HasAnyProperty(PropertiesUtility.propertiesWhichPreventAction);

    public IEntitySelector customActionTarget => this.definition.customActionTarget;

    protected CharacterStatus(int id, int ownerId, int teamId, int teamIndex, int level)
      : base(id)
    {
      this.ownerId = ownerId;
      this.teamId = teamId;
      this.teamIndex = teamIndex;
      this.level = level;
    }

    protected static void InitializeStatus(CharacterStatus status, CharacterDefinition definition)
    {
      int level = status.level;
      int valueWithLevel1 = definition.life.GetValueWithLevel(level);
      status.SetCarac(CaracId.Life, valueWithLevel1);
      int valueWithLevel2 = definition.movementPoints.GetValueWithLevel(level);
      status.SetCarac(CaracId.MovementPoints, valueWithLevel2);
      ActionRange actionRange = definition.actionRange;
      if (actionRange == null)
        return;
      int valueWithLevel3 = actionRange.min.GetValueWithLevel(level);
      int valueWithLevel4 = actionRange.max.GetValueWithLevel(level);
      status.SetCarac(CaracId.RangeMin, valueWithLevel3);
      status.SetCarac(CaracId.RangeMax, valueWithLevel4);
    }
  }
}
