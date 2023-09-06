// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.MaxLifeChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class MaxLifeChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int maxLifeBefore { get; private set; }

    public int maxLifeAfter { get; private set; }

    public MaxLifeChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int maxLifeBefore,
      int maxLifeAfter)
      : base(FightEventData.Types.EventType.MaxLifeChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.maxLifeBefore = maxLifeBefore;
      this.maxLifeAfter = maxLifeAfter;
    }

    public MaxLifeChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.MaxLifeChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.maxLifeBefore = proto.Int2;
      this.maxLifeAfter = proto.Int3;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithLife entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithLife>(this.concernedEntity, out entityStatus))
        entityStatus.SetCarac(CaracId.LifeMax, this.maxLifeAfter);
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithLife>(this.concernedEntity), 21, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MaxLifeChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.LifeArmorChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus1;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus1))
      {
        if (entityStatus1.view is IObjectWithArmoredLife view1)
          view1.SetBaseLife(this.maxLifeAfter);
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithArmoredLife>(entityStatus1), 37, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MaxLifeChangedEvent.cs");
        if (entityStatus1.type == EntityType.Hero)
        {
          HeroStatus heroStatus = (HeroStatus) entityStatus1;
          if (heroStatus.ownerId == fightStatus.localPlayerId)
          {
            FightMap current = FightMap.current;
            if ((Object) null != (Object) current)
              current.SetLocalPlayerHeroLife(heroStatus.life, this.maxLifeAfter);
          }
          PlayerStatus entityStatus2;
          if (fightStatus.TryGetEntity<PlayerStatus>(heroStatus.ownerId, out entityStatus2))
          {
            AbstractPlayerUIRework view2 = entityStatus2.view;
            if ((Object) null != (Object) view2)
              view2.ChangeHeroBaseLifePoints(this.maxLifeAfter);
          }
          else
            Log.Error(FightEventErrors.PlayerNotFound(heroStatus.ownerId), 62, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MaxLifeChangedEvent.cs");
        }
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 68, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MaxLifeChangedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.LifeArmorChanged);
      yield break;
    }
  }
}
