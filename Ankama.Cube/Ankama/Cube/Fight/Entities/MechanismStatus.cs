// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.MechanismStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Maps.Objects;
using System.Collections.Generic;

namespace Ankama.Cube.Fight.Entities
{
  public abstract class MechanismStatus : 
    EntityStatus,
    IEntityWithOwner,
    IEntityWithTeam,
    IEntity,
    IEntityWithBoardPresence,
    IEntityWithFamilies,
    IEntityWithLevel,
    IDynamicValueSource
  {
    public MechanismDefinition definition { get; protected set; }

    public int teamId { get; }

    public int teamIndex { get; }

    public int ownerId { get; }

    public IsoObject view { get; set; }

    public virtual Area area { get; protected set; }

    public abstract bool blocksMovement { get; }

    public IReadOnlyList<Family> families => this.definition.families;

    public int level { get; }

    public IReadOnlyList<ILevelOnlyDependant> dynamicValues => this.definition.precomputedData.dynamicValueReferences;

    protected MechanismStatus(int id, int ownerId, int teamId, int teamIndex, int level)
      : base(id)
    {
      this.ownerId = ownerId;
      this.teamId = teamId;
      this.teamIndex = teamIndex;
      this.level = level;
    }
  }
}
