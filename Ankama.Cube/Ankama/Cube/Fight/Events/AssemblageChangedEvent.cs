// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.AssemblageChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class AssemblageChangedEvent : FightEvent, IRelatedToEntity
  {
    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithAssemblage entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithAssemblage>(this.concernedEntity, out entityStatus))
        entityStatus.assemblingIds = this.allEntities;
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithAssemblage>(this.concernedEntity), 20, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\AssemblageChangedEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      IEntityWithAssemblage entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithAssemblage>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.view is IObjectWithAssemblage view)
        {
          IEnumerable<Vector2Int> otherObjectInAssemblagePositions = AssemblageChangedEvent.EnumerateOtherObjectsInAssemblagePositions(fightStatus, this.concernedEntity, this.allEntities);
          view.RefreshAssemblage(otherObjectInAssemblagePositions);
        }
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAssemblage>((IEntityWithBoardPresence) entityStatus), 35, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\AssemblageChangedEvent.cs");
      }
      else
      {
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithAssemblage>(this.concernedEntity), 40, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\AssemblageChangedEvent.cs");
        yield break;
      }
    }

    private static IEnumerable<Vector2Int> EnumerateOtherObjectsInAssemblagePositions(
      FightStatus fightStatus,
      int concernedEntity,
      IReadOnlyList<int> entitiesInAssemblage)
    {
      int entityCountInAssemblage = ((IReadOnlyCollection<int>) entitiesInAssemblage).Count;
      for (int i = 0; i < entityCountInAssemblage; ++i)
      {
        int num = entitiesInAssemblage[i];
        if (num != concernedEntity)
        {
          IEntityWithAssemblage entityStatus;
          if (fightStatus.TryGetEntity<IEntityWithAssemblage>(num, out entityStatus))
          {
            if (entityStatus.view is IObjectWithAssemblage view)
              yield return view.cellObject.coords;
            else
              Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAssemblage>((IEntityWithBoardPresence) entityStatus), 65, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\AssemblageChangedEvent.cs");
          }
          else
            Log.Error(FightEventErrors.EntityNotFound<IEntityWithAssemblage>(num), 70, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\AssemblageChangedEvent.cs");
        }
      }
    }

    public int concernedEntity { get; private set; }

    public IReadOnlyList<int> allEntities { get; private set; }

    public AssemblageChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      IReadOnlyList<int> allEntities)
      : base(FightEventData.Types.EventType.AssemblageChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.allEntities = allEntities;
    }

    public AssemblageChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.AssemblageChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.allEntities = (IReadOnlyList<int>) proto.IntList1;
    }
  }
}
