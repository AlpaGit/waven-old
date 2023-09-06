// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ElementPointsChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class ElementPointsChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public int element { get; private set; }

    public ElementPointsChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter,
      int element)
      : base(FightEventData.Types.EventType.ElementPointsChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
      this.element = element;
    }

    public ElementPointsChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.ElementPointsChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
      this.element = proto.Int4;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        CaracId element = (CaracId) this.element;
        if (entityStatus.GetCarac(element, 0) != this.valueBefore)
          Log.Warning(string.Format("The previous element points value ({0}) for player with id {1} doesn't match the value in the event ({2}).", (object) entityStatus.GetCarac(element, 0), (object) this.concernedEntity, (object) this.valueBefore), 20, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ElementPointsChangedEvent.cs");
        entityStatus.SetCarac(element, this.valueAfter);
        AbstractPlayerUIRework view = entityStatus.view;
        if ((UnityEngine.Object) null != (UnityEngine.Object) view)
          view.RefreshAvailableCompanions();
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 33, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ElementPointsChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.ElementPointsChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((UnityEngine.Object) null != (UnityEngine.Object) view)
        {
          switch (this.element)
          {
            case 11:
              view.ChangeFireElementaryPoints(this.valueAfter);
              break;
            case 12:
              view.ChangeWaterElementaryPoints(this.valueAfter);
              break;
            case 13:
              view.ChangeEarthElementaryPoints(this.valueAfter);
              break;
            case 14:
              view.ChangeAirElementaryPoints(this.valueAfter);
              break;
            default:
              throw new ArgumentException();
          }
          yield return (object) view.UpdateAvailableCompanions();
        }
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 70, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ElementPointsChangedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.ElementPointsChanged);
    }
  }
}
