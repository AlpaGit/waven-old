// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.CompanionReserveStateChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class CompanionReserveStateChangedEvent : FightEvent, IRelatedToEntity
  {
    private bool m_wasGiven;

    public int concernedEntity { get; private set; }

    public int companionDefId { get; private set; }

    public CompanionReserveState state { get; private set; }

    public bool hasPreviousState { get; private set; }

    public CompanionReserveState previousState { get; private set; }

    public CompanionReserveStateChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int companionDefId,
      CompanionReserveState state,
      bool hasPreviousState,
      CompanionReserveState previousState)
      : base(FightEventData.Types.EventType.CompanionReserveStateChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.companionDefId = companionDefId;
      this.state = state;
      this.hasPreviousState = hasPreviousState;
      this.previousState = previousState;
    }

    public CompanionReserveStateChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.CompanionReserveStateChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.companionDefId = proto.Int2;
      this.state = proto.CompanionReserveState1;
      this.previousState = proto.CompanionReserveState2;
      this.hasPreviousState = proto.Bool1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        ReserveCompanionStatus companionStatus;
        if (!entityStatus.TryGetCompanion(this.companionDefId, out companionStatus))
          Log.Error(FightEventErrors.ReserveCompanionNotFound(this.companionDefId, this.concernedEntity), 19, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionReserveStateChangedEvent.cs");
        this.m_wasGiven = companionStatus.isGiven;
        if (this.m_wasGiven && this.state == CompanionReserveState.Dead)
          entityStatus.RemoveAdditionalCompanion(this.companionDefId);
        else
          companionStatus.SetState(this.state);
        AbstractPlayerUIRework view = entityStatus.view;
        if (!((Object) null != (Object) view))
          return;
        if (this.m_wasGiven && this.state == CompanionReserveState.Dead)
          view.RemoveAdditionalCompanionStatus(this.companionDefId);
        else
          view.ChangeCompanionStateStatus(this.companionDefId, this.state);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 48, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionReserveStateChangedEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
        {
          if (this.m_wasGiven && this.state == CompanionReserveState.Dead)
            yield return (object) view.RemoveAdditionalCompanion(this.companionDefId);
          else
            yield return (object) view.ChangeCompanionState(this.companionDefId, this.state);
        }
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 71, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionReserveStateChangedEvent.cs");
    }
  }
}
