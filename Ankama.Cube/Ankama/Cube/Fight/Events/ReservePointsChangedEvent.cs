// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ReservePointsChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class ReservePointsChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public ReservePointsChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter)
      : base(FightEventData.Types.EventType.ReservePointsChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
    }

    public ReservePointsChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.ReservePointsChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.reservePoints != this.valueBefore)
          Log.Warning(string.Format("The previous reserve points value ({0}) for player with id {1} doesn't match the value in the event ({2}).", (object) entityStatus.reservePoints, (object) this.concernedEntity, (object) this.valueBefore), 17, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ReservePointsChangedEvent.cs");
        entityStatus.SetCarac(CaracId.ReservePoints, this.valueAfter);
        if (!((Object) null != (Object) entityStatus.view))
          ;
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 31, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ReservePointsChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.ReserveChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
          view.ChangeReservePoints(this.valueAfter);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 49, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ReservePointsChangedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.ReserveChanged);
      yield break;
    }
  }
}
