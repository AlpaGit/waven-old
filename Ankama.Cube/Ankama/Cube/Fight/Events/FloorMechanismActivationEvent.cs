// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FloorMechanismActivationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.Fight.Events
{
  public class FloorMechanismActivationEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int activator { get; private set; }

    public IReadOnlyList<int> entitiesInAssemblage { get; private set; }

    public FloorMechanismActivationEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int activator,
      IReadOnlyList<int> entitiesInAssemblage)
      : base(FightEventData.Types.EventType.FloorMechanismActivation, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.activator = activator;
      this.entitiesInAssemblage = entitiesInAssemblage;
    }

    public FloorMechanismActivationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.FloorMechanismActivation, proto)
    {
      this.concernedEntity = proto.Int1;
      this.activator = proto.Int2;
      this.entitiesInAssemblage = (IReadOnlyList<int>) proto.IntList1;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FloorMechanismStatus entityStatus1;
      if (fightStatus.TryGetEntity<FloorMechanismStatus>(this.concernedEntity, out entityStatus1))
      {
        IEntityWithTeam entityStatus2;
        if (fightStatus.TryGetEntity<IEntityWithTeam>(this.activator, out entityStatus2))
        {
          if (entityStatus2.teamId == entityStatus1.teamId)
          {
            if (entityStatus1.view is IObjectWithActivation view1)
              yield return (object) view1.ActivatedByAlly();
            else
              Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithActivation>((IEntityWithBoardPresence) entityStatus1), 26, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismActivationEvent.cs");
          }
          else
          {
            IReadOnlyList<int> entitiesInAssemblage = this.entitiesInAssemblage;
            int count = ((IReadOnlyCollection<int>) entitiesInAssemblage).Count;
            if (count == 1)
            {
              if (entityStatus1.view is IObjectWithActivation view2)
                yield return (object) view2.ActivatedByOpponent();
              else
                Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithActivation>((IEntityWithBoardPresence) entityStatus1), 41, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismActivationEvent.cs");
            }
            else
            {
              IEnumerator[] enumeratorArray = new IEnumerator[count];
              for (int index = 0; index < count; ++index)
              {
                FloorMechanismStatus entityStatus3;
                if (fightStatus.TryGetEntity<FloorMechanismStatus>(entitiesInAssemblage[index], out entityStatus3))
                {
                  if (entityStatus3.view is IObjectWithActivation view3)
                    enumeratorArray[index] = view3.ActivatedByOpponent();
                  else
                    Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithActivation>((IEntityWithBoardPresence) entityStatus3), 57, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismActivationEvent.cs");
                }
                else
                  Log.Error(FightEventErrors.EntityNotFound<FloorMechanismStatus>(entitiesInAssemblage[index]), 62, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismActivationEvent.cs");
              }
              yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(enumeratorArray);
            }
          }
        }
        else
          Log.Error(FightEventErrors.EntityNotFound<IEntityWithTeam>(this.activator), 72, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismActivationEvent.cs");
      }
      else
        Log.Error(string.Format("Could not find entity with id {0}.", (object) this.concernedEntity), 77, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloorMechanismActivationEvent.cs");
    }
  }
}
