// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.HeroAddedEvent
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
  public class HeroAddedEvent : FightEvent, IRelatedToEntity, ICharacterAdded
  {
    public int concernedEntity { get; private set; }

    public int entityDefId { get; private set; }

    public int ownerId { get; private set; }

    public CellCoord refCoord { get; private set; }

    public int direction { get; private set; }

    public int level { get; private set; }

    public int gender { get; private set; }

    public int skinId { get; private set; }

    public HeroAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int entityDefId,
      int ownerId,
      CellCoord refCoord,
      int direction,
      int level,
      int gender,
      int skinId)
      : base(FightEventData.Types.EventType.HeroAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.entityDefId = entityDefId;
      this.ownerId = ownerId;
      this.refCoord = refCoord;
      this.direction = direction;
      this.level = level;
      this.gender = gender;
      this.skinId = skinId;
    }

    public HeroAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.HeroAdded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.entityDefId = proto.Int2;
      this.ownerId = proto.Int3;
      this.direction = proto.Int4;
      this.level = proto.Int5;
      this.gender = proto.Int6;
      this.skinId = proto.Int7;
      this.refCoord = proto.CellCoord1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out entityStatus))
      {
        Gender gender = (Gender) this.gender;
        WeaponDefinition definition1;
        if (RuntimeData.weaponDefinitions.TryGetValue(this.entityDefId, out definition1))
        {
          HeroStatus heroStatus = HeroStatus.Create(this.concernedEntity, definition1, this.level, gender, entityStatus, (Vector2Int) this.refCoord);
          fightStatus.AddEntity((EntityStatus) heroStatus);
          entityStatus.heroStatus = heroStatus;
          AbstractPlayerUIRework view = entityStatus.view;
          if ((Object) null != (Object) view)
          {
            view.SetHeroIllustration(definition1, gender);
            view.SetHeroStartLifePoints(heroStatus.baseLife, entityStatus.playerType);
            ReserveDefinition definition2;
            if (RuntimeData.reserveDefinitions.TryGetValue(definition1.god, out definition2))
              view.SetupReserve(heroStatus, definition2);
            else
              Log.Error(FightEventErrors.DefinitionNotFound<ReserveDefinition>((int) definition1.god), 45, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\HeroAddedEvent.cs");
          }
        }
        else
          Log.Error(FightEventErrors.EntityCreationFailed<HeroStatus, WeaponDefinition>(this.concernedEntity, this.entityDefId), 51, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\HeroAddedEvent.cs");
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 56, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\HeroAddedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      HeroStatus heroStatus;
      if (fightStatus.TryGetEntity<HeroStatus>(this.concernedEntity, out heroStatus))
      {
        PlayerStatus ownerStatus;
        if (fightStatus.TryGetEntity<PlayerStatus>(this.ownerId, out ownerStatus))
        {
          WeaponDefinition definition = (WeaponDefinition) heroStatus.definition;
          if ((Object) null != (Object) definition)
          {
            HeroCharacterObject heroCharacterObject = FightObjectFactory.CreateHeroCharacterObject(definition, this.refCoord.X, this.refCoord.Y, (Ankama.Cube.Data.Direction) this.direction);
            if ((Object) null != (Object) heroCharacterObject)
            {
              heroStatus.view = (IsoObject) heroCharacterObject;
              yield return (object) heroCharacterObject.LoadAnimationDefinitions(this.skinId, (Gender) this.gender);
              heroCharacterObject.Initialize(fightStatus, ownerStatus, (CharacterStatus) heroStatus);
              HeroAddedEvent.UpdateAudioContext(ownerStatus, heroStatus.baseLife);
              yield return (object) heroCharacterObject.Spawn();
            }
            heroCharacterObject = (HeroCharacterObject) null;
          }
        }
        else
          Log.Error(FightEventErrors.PlayerNotFound(this.ownerId), 98, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\HeroAddedEvent.cs");
        ownerStatus = (PlayerStatus) null;
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<HeroStatus>(this.concernedEntity), 103, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\HeroAddedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    private static void UpdateAudioContext(PlayerStatus ownerStatus, int life)
    {
      if (!ownerStatus.isLocalPlayer)
        return;
      FightMap current = FightMap.current;
      if (!((Object) null != (Object) current))
        return;
      current.SetLocalPlayerHeroLife(life, life);
    }
  }
}
