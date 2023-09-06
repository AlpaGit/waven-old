// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.CompanionGivenEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class CompanionGivenEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int companionDefId { get; private set; }

    public int companionLevel { get; private set; }

    public int toFightId { get; private set; }

    public int toPlayerId { get; private set; }

    public CompanionGivenEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int companionDefId,
      int companionLevel,
      int toFightId,
      int toPlayerId)
      : base(FightEventData.Types.EventType.CompanionGiven, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.companionDefId = companionDefId;
      this.companionLevel = companionLevel;
      this.toFightId = toFightId;
      this.toPlayerId = toPlayerId;
    }

    public CompanionGivenEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.CompanionGiven, proto)
    {
      this.concernedEntity = proto.Int1;
      this.companionDefId = proto.Int2;
      this.companionLevel = proto.Int3;
      this.toFightId = proto.Int4;
      this.toPlayerId = proto.Int5;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus1;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus1))
      {
        ReserveCompanionStatus companionStatus;
        if (entityStatus1.TryGetCompanion(this.companionDefId, out companionStatus))
        {
          PlayerStatus entityStatus2;
          if (FightLogicExecutor.GetFightStatus(this.toFightId).TryGetEntity<PlayerStatus>(this.toPlayerId, out entityStatus2))
          {
            entityStatus2.AddAdditionalCompanion(companionStatus);
            AbstractPlayerUIRework view = entityStatus2.view;
            if (!((Object) null != (Object) view))
              return;
            view.AddAdditionalCompanionStatus(entityStatus1, this.companionDefId, companionStatus.level);
          }
          else
            Log.Error(FightEventErrors.PlayerNotFound(this.toPlayerId, this.toFightId), 30, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionGivenEvent.cs");
        }
        else
          Log.Error(FightEventErrors.ReserveCompanionNotFound(this.companionDefId, this.concernedEntity), 35, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionGivenEvent.cs");
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 40, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionGivenEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus1;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus1))
      {
        ReserveCompanionStatus companionStatus;
        if (entityStatus1.TryGetCompanion(this.companionDefId, out companionStatus))
        {
          PlayerStatus entityStatus2;
          if (FightLogicExecutor.GetFightStatus(this.toFightId).TryGetEntity<PlayerStatus>(this.toPlayerId, out entityStatus2))
          {
            AbstractPlayerUIRework view = entityStatus2.view;
            if ((Object) null != (Object) view)
              yield return (object) view.AddAdditionalCompanion(entityStatus1, this.companionDefId, companionStatus.level);
          }
          else
            Log.Error(FightEventErrors.PlayerNotFound(this.toPlayerId, this.toFightId), 62, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionGivenEvent.cs");
        }
        else
          Log.Error(FightEventErrors.ReserveCompanionNotFound(this.companionDefId, this.concernedEntity), 67, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionGivenEvent.cs");
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 72, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionGivenEvent.cs");
    }
  }
}
