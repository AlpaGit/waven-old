// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ActionPointsChangedEvent
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
  public class ActionPointsChangedEvent : FightEvent, IRelatedToEntity
  {
    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.actionPoints != this.valueBefore)
          Log.Warning(string.Format("The previous action points value ({0}) for player with id {1} doesn't match the value in the event ({2}).", (object) entityStatus.actionPoints, (object) this.concernedEntity, (object) this.valueBefore), 17, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ActionPointsChangedEvent.cs");
        entityStatus.SetCarac(CaracId.ActionPoints, this.valueAfter);
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
          view.RefreshAvailableActions(false);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 30, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ActionPointsChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.ActionPointsChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
        {
          view.ChangeActionPoints(this.valueAfter);
          view.UpdateAvailableActions(false);
        }
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 50, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ActionPointsChangedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.ActionPointsChanged);
      yield break;
    }

    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public bool isCost { get; private set; }

    public ActionPointsChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter,
      bool isCost)
      : base(FightEventData.Types.EventType.ActionPointsChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
      this.isCost = isCost;
    }

    public ActionPointsChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.ActionPointsChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
      this.isCost = proto.Bool1;
    }
  }
}
