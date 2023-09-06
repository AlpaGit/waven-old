// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.CompanionAddedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class CompanionAddedEvent : FightEvent, IRelatedToEntity, ICharacterAdded
  {
    public int concernedEntity { get; private set; }

    public int entityDefId { get; private set; }

    public int ownerId { get; private set; }

    public CellCoord refCoord { get; private set; }

    public int direction { get; private set; }

    public int level { get; private set; }

    public CompanionAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int entityDefId,
      int ownerId,
      CellCoord refCoord,
      int direction,
      int level)
      : base(FightEventData.Types.EventType.CompanionAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.entityDefId = entityDefId;
      this.ownerId = ownerId;
      this.refCoord = refCoord;
      this.direction = direction;
      this.level = level;
    }

    public CompanionAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.CompanionAdded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.entityDefId = proto.Int2;
      this.ownerId = proto.Int3;
      this.direction = proto.Int4;
      this.level = proto.Int5;
      this.refCoord = proto.CellCoord1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus))
      {
        CompanionStatus companionStatus = CompanionAddedEvent.CreateCompanionStatus(this.concernedEntity, this.entityDefId, this.level, entityStatus, this.refCoord);
        if (companionStatus != null)
          fightStatus.AddEntity((EntityStatus) companionStatus);
        if (entityStatus.isLocalPlayer)
          FightCastManager.CheckCompanionInvoked(this.entityDefId);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 32, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionAddedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      CompanionStatus companionStatus;
      if (fightStatus.TryGetEntity<CompanionStatus>(this.concernedEntity, out companionStatus))
      {
        PlayerStatus ownerStatus;
        if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out ownerStatus))
        {
          if (!ownerStatus.isLocalPlayer)
          {
            FightMap current = FightMap.current;
            if ((Object) null != (Object) current)
            {
              CellObject cellObject;
              if (current.TryGetCellObject(this.refCoord.X, this.refCoord.Y, out cellObject))
                yield return (object) FightUIRework.ShowPlayingCompanion(new ReserveCompanionStatus(ownerStatus, (CompanionDefinition) companionStatus.definition, this.level), cellObject);
              else
                Log.Error(FightEventErrors.InvalidPosition(this.refCoord), 59, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionAddedEvent.cs");
            }
          }
          yield return (object) CompanionAddedEvent.CreateCompanionCharacterObject(fightStatus, companionStatus, ownerStatus, this.refCoord.X, this.refCoord.Y, (Ankama.Cube.Data.Direction) this.direction);
        }
        else
          Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 70, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionAddedEvent.cs");
        ownerStatus = (PlayerStatus) null;
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<CompanionStatus>(this.concernedEntity), 75, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionAddedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public static CompanionStatus CreateCompanionStatus(
      int id,
      int definitionId,
      int level,
      PlayerStatus playerStatus,
      CellCoord coord)
    {
      CompanionDefinition definition;
      if (RuntimeData.companionDefinitions.TryGetValue(definitionId, out definition))
        return CompanionStatus.Create(id, definition, level, playerStatus, (Vector2Int) coord);
      Log.Error(FightEventErrors.EntityCreationFailed<CompanionStatus, CompanionDefinition>(id, definitionId), 90, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionAddedEvent.cs");
      return (CompanionStatus) null;
    }

    public static IEnumerator CreateCompanionCharacterObject(
      FightStatus fightStatus,
      CompanionStatus companionStatus,
      PlayerStatus ownerStatus,
      int x,
      int y,
      Ankama.Cube.Data.Direction direction)
    {
      CompanionDefinition definition = (CompanionDefinition) companionStatus.definition;
      if (!((Object) null == (Object) definition))
      {
        CompanionCharacterObject companionCharacterObject = FightObjectFactory.CreateCompanionCharacterObject(definition, x, y, direction);
        if (!((Object) null == (Object) companionCharacterObject))
        {
          companionStatus.view = (IsoObject) companionCharacterObject;
          yield return (object) companionCharacterObject.LoadAnimationDefinitions(definition.defaultSkin.value);
          companionCharacterObject.Initialize(fightStatus, ownerStatus, (CharacterStatus) companionStatus);
          yield return (object) companionCharacterObject.Spawn();
        }
      }
    }
  }
}
