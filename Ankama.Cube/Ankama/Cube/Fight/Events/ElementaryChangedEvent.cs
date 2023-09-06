// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ElementaryChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class ElementaryChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int elementaryState { get; private set; }

    public ElementaryChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int elementaryState)
      : base(FightEventData.Types.EventType.ElementaryChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.elementaryState = elementaryState;
    }

    public ElementaryChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.ElementaryChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.elementaryState = proto.Int2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithElementaryState entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithElementaryState>(this.concernedEntity, out entityStatus))
        entityStatus.ChangeElementaryState((ElementaryStates) this.elementaryState);
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithElementaryState>(this.concernedEntity), 19, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ElementaryChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.ElementaryStateChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.view is IObjectWithElementaryState view)
          view.SetElementaryState((ElementaryStates) this.elementaryState);
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithElementaryState>(entityStatus), 35, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ElementaryChangedEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 40, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ElementaryChangedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.ElementaryStateChanged);
      yield break;
    }
  }
}
