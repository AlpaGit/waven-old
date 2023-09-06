// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.SummoningAddedEvent
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
  public class SummoningAddedEvent : FightEvent, IRelatedToEntity, ICharacterAdded
  {
    public int concernedEntity { get; private set; }

    public int entityDefId { get; private set; }

    public int ownerId { get; private set; }

    public CellCoord refCoord { get; private set; }

    public int direction { get; private set; }

    public int level { get; private set; }

    public SummoningAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int entityDefId,
      int ownerId,
      CellCoord refCoord,
      int direction,
      int level)
      : base(FightEventData.Types.EventType.SummoningAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.entityDefId = entityDefId;
      this.ownerId = ownerId;
      this.refCoord = refCoord;
      this.direction = direction;
      this.level = level;
    }

    public SummoningAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.SummoningAdded, proto)
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
      if (!fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus))
      {
        Log.Error(string.Format("Could not find a {0} entity with id {1}.", (object) "PlayerStatus", (object) this.ownerId), 17, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SummoningAddedEvent.cs");
      }
      else
      {
        SummoningStatus summoningStatus = SummoningAddedEvent.CreateSummoningStatus(this.concernedEntity, this.entityDefId, this.level, entityStatus, this.refCoord);
        if (summoningStatus == null)
          return;
        fightStatus.AddEntity((EntityStatus) summoningStatus);
        FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
      }
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      SummoningStatus entityStatus1;
      if (fightStatus.TryGetEntity<SummoningStatus>(this.concernedEntity, out entityStatus1))
      {
        PlayerStatus entityStatus2;
        if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus2))
          yield return (object) SummoningAddedEvent.CreateSummoningCharacterObject(fightStatus, entityStatus1, entityStatus2, this.refCoord.X, this.refCoord.Y, (Ankama.Cube.Data.Direction) this.direction);
        else
          Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 44, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SummoningAddedEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<SummoningStatus>(this.concernedEntity), 49, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SummoningAddedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public static SummoningStatus CreateSummoningStatus(
      int id,
      int definitionId,
      int level,
      PlayerStatus playerStatus,
      CellCoord coord)
    {
      SummoningDefinition definition;
      if (RuntimeData.summoningDefinitions.TryGetValue(definitionId, out definition))
        return SummoningStatus.Create(id, definition, level, playerStatus, (Vector2Int) coord);
      Log.Error(FightEventErrors.DefinitionNotFound<SummoningDefinition>(definitionId), 64, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SummoningAddedEvent.cs");
      return (SummoningStatus) null;
    }

    public static IEnumerator CreateSummoningCharacterObject(
      FightStatus fightStatus,
      SummoningStatus summoningStatus,
      PlayerStatus ownerStatus,
      int x,
      int y,
      Ankama.Cube.Data.Direction direction)
    {
      SummoningDefinition definition = (SummoningDefinition) summoningStatus.definition;
      if (!((Object) null == (Object) definition))
      {
        SummoningCharacterObject summoningCharacterObject = FightObjectFactory.CreateSummoningCharacterObject(definition, x, y, direction);
        if (!((Object) null == (Object) summoningCharacterObject))
        {
          summoningStatus.view = (IsoObject) summoningCharacterObject;
          yield return (object) summoningCharacterObject.LoadAnimationDefinitions(definition.defaultSkin.value);
          summoningCharacterObject.Initialize(fightStatus, ownerStatus, (CharacterStatus) summoningStatus);
          yield return (object) summoningCharacterObject.Spawn();
        }
      }
    }
  }
}
