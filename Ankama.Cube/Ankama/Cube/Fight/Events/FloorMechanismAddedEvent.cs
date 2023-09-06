// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FloorMechanismAddedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class FloorMechanismAddedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int entityDefId { get; private set; }

    public int ownerId { get; private set; }

    public CellCoord refCoord { get; private set; }

    public int level { get; private set; }

    public FloorMechanismAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int entityDefId,
      int ownerId,
      CellCoord refCoord,
      int level)
      : base(FightEventData.Types.EventType.FloorMechanismAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.entityDefId = entityDefId;
      this.ownerId = ownerId;
      this.refCoord = refCoord;
      this.level = level;
    }

    public FloorMechanismAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.FloorMechanismAdded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.entityDefId = proto.Int2;
      this.ownerId = proto.Int3;
      this.level = proto.Int4;
      this.refCoord = proto.CellCoord1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus))
      {
        FloorMechanismStatus floorMechanismStatus = FloorMechanismAddedEvent.CreateFloorMechanismStatus(this.concernedEntity, this.entityDefId, this.level, entityStatus, this.refCoord);
        if (floorMechanismStatus != null)
          fightStatus.AddEntity((EntityStatus) floorMechanismStatus);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 25, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismAddedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FloorMechanismStatus entityStatus1;
      if (fightStatus.TryGetEntity<FloorMechanismStatus>(this.concernedEntity, out entityStatus1))
      {
        PlayerStatus entityStatus2;
        if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus2))
          yield return (object) FloorMechanismAddedEvent.CreateFloorMechanismObject(fightStatus, entityStatus1, entityStatus2, this.refCoord.X, this.refCoord.Y);
        else
          Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 43, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismAddedEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<FloorMechanismStatus>(this.concernedEntity), 48, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismAddedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public static FloorMechanismStatus CreateFloorMechanismStatus(
      int id,
      int definitionId,
      int level,
      PlayerStatus playerStatus,
      CellCoord coord)
    {
      FloorMechanismDefinition definition;
      if (RuntimeData.floorMechanismDefinitions.TryGetValue(definitionId, out definition))
        return FloorMechanismStatus.Create(id, definition, level, playerStatus, (Vector2Int) coord);
      Log.Error(FightEventErrors.EntityCreationFailed<FloorMechanismStatus, FloorMechanismDefinition>(id, definitionId), 63, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismAddedEvent.cs");
      return (FloorMechanismStatus) null;
    }

    public static IEnumerator CreateFloorMechanismObject(
      FightStatus fightStatus,
      FloorMechanismStatus floorMechanismStatus,
      PlayerStatus ownerStatus,
      int x,
      int y)
    {
      FloorMechanismDefinition definition = (FloorMechanismDefinition) floorMechanismStatus.definition;
      if (!((Object) null == (Object) definition))
      {
        FloorMechanismObject floorMechanismObject = FightObjectFactory.CreateFloorMechanismObject(definition, x, y);
        if (!((Object) null == (Object) floorMechanismObject))
        {
          floorMechanismStatus.view = (IsoObject) floorMechanismObject;
          floorMechanismObject.alliedWithLocalPlayer = GameStatus.localPlayerTeamIndex == floorMechanismStatus.teamIndex;
          yield return (object) floorMechanismObject.LoadAnimationDefinitions(definition.defaultSkin.value);
          floorMechanismObject.Initialize(fightStatus, ownerStatus, floorMechanismStatus);
          yield return (object) floorMechanismObject.Spawn();
        }
      }
    }
  }
}
