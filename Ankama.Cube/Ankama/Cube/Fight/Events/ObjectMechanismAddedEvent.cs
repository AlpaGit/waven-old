// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ObjectMechanismAddedEvent
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
  public class ObjectMechanismAddedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int entityDefId { get; private set; }

    public int ownerId { get; private set; }

    public CellCoord refCoord { get; private set; }

    public int level { get; private set; }

    public ObjectMechanismAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int entityDefId,
      int ownerId,
      CellCoord refCoord,
      int level)
      : base(FightEventData.Types.EventType.ObjectMechanismAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.entityDefId = entityDefId;
      this.ownerId = ownerId;
      this.refCoord = refCoord;
      this.level = level;
    }

    public ObjectMechanismAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.ObjectMechanismAdded, proto)
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
        ObjectMechanismStatus objectMechanismStatus = ObjectMechanismAddedEvent.CreateObjectMechanismStatus(this.concernedEntity, this.entityDefId, this.level, entityStatus, this.refCoord);
        if (objectMechanismStatus != null)
          fightStatus.AddEntity((EntityStatus) objectMechanismStatus);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 25, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ObjectMechanismAddedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      ObjectMechanismStatus entityStatus1;
      if (fightStatus.TryGetEntity<ObjectMechanismStatus>(this.concernedEntity, out entityStatus1))
      {
        PlayerStatus entityStatus2;
        if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus2))
          yield return (object) ObjectMechanismAddedEvent.CreateObjectMechanismObject(fightStatus, entityStatus1, entityStatus2, this.refCoord.X, this.refCoord.Y);
        else
          Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 44, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ObjectMechanismAddedEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<ObjectMechanismStatus>(this.concernedEntity), 49, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ObjectMechanismAddedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public static ObjectMechanismStatus CreateObjectMechanismStatus(
      int id,
      int definitionId,
      int level,
      PlayerStatus playerStatus,
      CellCoord coord)
    {
      ObjectMechanismDefinition definition;
      if (RuntimeData.objectMechanismDefinitions.TryGetValue(definitionId, out definition))
        return ObjectMechanismStatus.Create(id, definition, level, playerStatus, (Vector2Int) coord);
      Log.Error(FightEventErrors.DefinitionNotFound<ObjectMechanismDefinition>(definitionId), 64, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ObjectMechanismAddedEvent.cs");
      return (ObjectMechanismStatus) null;
    }

    public static IEnumerator CreateObjectMechanismObject(
      FightStatus fightStatus,
      ObjectMechanismStatus objectMechanismStatus,
      PlayerStatus ownerStatus,
      int x,
      int y)
    {
      ObjectMechanismDefinition definition = (ObjectMechanismDefinition) objectMechanismStatus.definition;
      if (!((Object) null == (Object) definition))
      {
        ObjectMechanismObject objectMechanismObject = FightObjectFactory.CreateObjectMechanismObject(definition, x, y);
        if (!((Object) null == (Object) objectMechanismObject))
        {
          objectMechanismStatus.view = (IsoObject) objectMechanismObject;
          objectMechanismObject.alliedWithLocalPlayer = GameStatus.localPlayerTeamIndex == ownerStatus.teamIndex;
          yield return (object) objectMechanismObject.LoadAnimationDefinitions(definition.defaultSkin.value);
          objectMechanismObject.Initialize(fightStatus, ownerStatus, objectMechanismStatus);
          yield return (object) objectMechanismObject.Spawn();
        }
      }
    }
  }
}
