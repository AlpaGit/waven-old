// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityActionResetEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class EntityActionResetEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public EntityActionResetEvent(int eventId, int? parentEventId, int concernedEntity)
      : base(FightEventData.Types.EventType.EntityActionReset, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
    }

    public EntityActionResetEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityActionReset, proto)
    {
      this.concernedEntity = proto.Int1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      CharacterStatus entityStatus;
      if (fightStatus.TryGetEntity<CharacterStatus>(this.concernedEntity, out entityStatus))
      {
        entityStatus.actionUsed = false;
        fightStatus.NotifyEntityPlayableStateChanged();
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<CharacterStatus>(this.concernedEntity), 20, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityActionResetEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.view is IObjectWithAction view)
          view.SetActionUsed(false, false);
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAction>(entityStatus), 34, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityActionResetEvent.cs");
      }
      else
      {
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 39, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityActionResetEvent.cs");
        yield break;
      }
    }
  }
}
