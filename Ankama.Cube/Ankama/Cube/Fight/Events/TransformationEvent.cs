// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TransformationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class TransformationEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int newEntityType { get; private set; }

    public int newEntityId { get; private set; }

    public int entityDefId { get; private set; }

    public int ownerId { get; private set; }

    public CellCoord refCoord { get; private set; }

    public int direction { get; private set; }

    public int level { get; private set; }

    public bool copyActionUsed { get; private set; }

    public TransformationEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int newEntityType,
      int newEntityId,
      int entityDefId,
      int ownerId,
      CellCoord refCoord,
      int direction,
      int level,
      bool copyActionUsed)
      : base(FightEventData.Types.EventType.Transformation, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.newEntityType = newEntityType;
      this.newEntityId = newEntityId;
      this.entityDefId = entityDefId;
      this.ownerId = ownerId;
      this.refCoord = refCoord;
      this.direction = direction;
      this.level = level;
      this.copyActionUsed = copyActionUsed;
    }

    public TransformationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.Transformation, proto)
    {
      this.concernedEntity = proto.Int1;
      this.newEntityType = proto.Int2;
      this.newEntityId = proto.Int3;
      this.entityDefId = proto.Int4;
      this.ownerId = proto.Int5;
      this.direction = proto.Int6;
      this.level = proto.Int7;
      this.refCoord = proto.CellCoord1;
      this.copyActionUsed = proto.Bool1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      fightStatus.RemoveEntity(this.concernedEntity);
      PlayerStatus entityStatus1;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus1))
      {
        EntityType newEntityType = (EntityType) this.newEntityType;
        EntityStatus entity;
        switch (newEntityType)
        {
          case EntityType.Global:
          case EntityType.Player:
          case EntityType.Team:
            Log.Error(string.Format("Transformation not handled for type {0}.", (object) newEntityType), 48, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TransformationEvent.cs");
            return;
          case EntityType.Hero:
            throw new Exception("[TransformationEvent] Heroes cannot be transformed.");
          case EntityType.Companion:
            entity = (EntityStatus) CompanionAddedEvent.CreateCompanionStatus(this.newEntityId, this.entityDefId, this.level, entityStatus1, this.refCoord);
            break;
          case EntityType.Summoning:
            entity = (EntityStatus) SummoningAddedEvent.CreateSummoningStatus(this.newEntityId, this.entityDefId, this.level, entityStatus1, this.refCoord);
            break;
          case EntityType.FloorMechanism:
            entity = (EntityStatus) FloorMechanismAddedEvent.CreateFloorMechanismStatus(this.newEntityId, this.entityDefId, this.level, entityStatus1, this.refCoord);
            break;
          case EntityType.ObjectMechanism:
            entity = (EntityStatus) ObjectMechanismAddedEvent.CreateObjectMechanismStatus(this.newEntityId, this.entityDefId, this.level, entityStatus1, this.refCoord);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        IEntityWithAction entityStatus2;
        if (this.copyActionUsed && fightStatus.TryGetEntity<IEntityWithAction>(this.concernedEntity, out entityStatus2) && entity is IEntityWithAction entityWithAction)
        {
          entityWithAction.actionUsed = entityStatus2.actionUsed;
          fightStatus.NotifyEntityPlayableStateChanged();
        }
        fightStatus.AddEntity(entity);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 72, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TransformationEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      Ankama.Cube.Data.Direction direction = Ankama.Cube.Data.Direction.SouthEast;
      IEntityWithBoardPresence entityStatus1;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus1))
      {
        IsoObject view = entityStatus1.view;
        if ((UnityEngine.Object) null != (UnityEngine.Object) view)
        {
          if (view is ICharacterObject characterObject)
            direction = characterObject.direction;
          view.DetachFromCell();
          view.Destroy();
        }
        else
          Log.Error(FightEventErrors.EntityHasNoView(entityStatus1), 97, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TransformationEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 102, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TransformationEvent.cs");
      IEntityWithBoardPresence entityStatus2;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.newEntityId, out entityStatus2))
      {
        PlayerStatus entityStatus3;
        if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus3))
        {
          EntityType newEntityType = (EntityType) this.newEntityType;
          switch (newEntityType)
          {
            case EntityType.Global:
            case EntityType.Player:
            case EntityType.Team:
              Log.Error(string.Format("Transformation not handled for type {0}.", (object) newEntityType), 134, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TransformationEvent.cs");
              break;
            case EntityType.Hero:
              throw new Exception("[TransformationEvent] Heroes cannot be transformed.");
            case EntityType.Companion:
              yield return (object) CompanionAddedEvent.CreateCompanionCharacterObject(fightStatus, (CompanionStatus) entityStatus2, entityStatus3, this.refCoord.X, this.refCoord.Y, direction);
              break;
            case EntityType.Summoning:
              yield return (object) SummoningAddedEvent.CreateSummoningCharacterObject(fightStatus, (SummoningStatus) entityStatus2, entityStatus3, this.refCoord.X, this.refCoord.Y, direction);
              break;
            case EntityType.FloorMechanism:
              yield return (object) FloorMechanismAddedEvent.CreateFloorMechanismObject(fightStatus, (FloorMechanismStatus) entityStatus2, entityStatus3, this.refCoord.X, this.refCoord.Y);
              break;
            case EntityType.ObjectMechanism:
              yield return (object) ObjectMechanismAddedEvent.CreateObjectMechanismObject(fightStatus, (ObjectMechanismStatus) entityStatus2, entityStatus3, this.refCoord.X, this.refCoord.Y);
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
        else
          Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 143, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TransformationEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.newEntityId), 148, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TransformationEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }
  }
}
