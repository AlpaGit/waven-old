// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityActionedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class EntityActionedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public bool executedByPlayer { get; private set; }

    public EntityActionedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      bool executedByPlayer)
      : base(FightEventData.Types.EventType.EntityActioned, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.executedByPlayer = executedByPlayer;
    }

    public EntityActionedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityActioned, proto)
    {
      this.concernedEntity = proto.Int1;
      this.executedByPlayer = proto.Bool1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      if (!this.executedByPlayer)
        return;
      ICharacterEntity entityStatus;
      if (fightStatus.TryGetEntity<ICharacterEntity>(this.concernedEntity, out entityStatus))
      {
        entityStatus.actionUsed = true;
        fightStatus.NotifyEntityPlayableStateChanged();
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<CharacterStatus>(this.concernedEntity), 22, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityActionedEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      if (this.executedByPlayer)
      {
        IEntityWithBoardPresence entityStatus;
        if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
        {
          if (entityStatus.view is IObjectWithAction view)
            view.SetActionUsed(true, false);
          else
            Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAction>(entityStatus), 39, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityActionedEvent.cs");
        }
        else
        {
          Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 44, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityActionedEvent.cs");
          yield break;
        }
      }
    }
  }
}
