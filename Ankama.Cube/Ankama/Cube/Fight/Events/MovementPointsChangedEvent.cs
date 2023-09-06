// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.MovementPointsChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class MovementPointsChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public MovementPointsChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter)
      : base(FightEventData.Types.EventType.MovementPointsChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
    }

    public MovementPointsChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.MovementPointsChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithMovement entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithMovement>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.GetCarac(CaracId.MovementPoints) != this.valueBefore)
          Log.Warning(string.Format("The previous movement points value ({0}) for entity with id {1} doesn't match the value in the event ({2}).", (object) entityStatus.GetCarac(CaracId.MovementPoints), (object) this.concernedEntity, (object) this.valueBefore), 18, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MovementPointsChangedEvent.cs");
        entityStatus.SetCarac(CaracId.MovementPoints, this.valueAfter);
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithMovement>(this.concernedEntity), 25, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MovementPointsChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.MovementPointsChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      MovementPointsChangedEvent pointsChangedEvent = this;
      int fightId = fightStatus.fightId;
      IEntityWithMovement entityStatus;
      // ISSUE: explicit non-virtual call
      if (fightStatus.TryGetEntity<IEntityWithMovement>(__nonvirtual (pointsChangedEvent.concernedEntity), out entityStatus))
      {
        IsoObject isoObject = entityStatus.view;
        if ((Object) null != (Object) isoObject)
        {
          if (isoObject is IObjectWithMovement objectWithMovement)
            objectWithMovement.SetMovementPoints(pointsChangedEvent.valueAfter);
          else
            Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithMovement>((IEntityWithBoardPresence) entityStatus), 46, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MovementPointsChangedEvent.cs");
          if (pointsChangedEvent.valueAfter > pointsChangedEvent.valueBefore)
          {
            yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.MovementPointGain, fightId, pointsChangedEvent.parentEventId, isoObject, fightStatus.context);
            ValueChangedFeedback.Launch(pointsChangedEvent.valueAfter - pointsChangedEvent.valueBefore, ValueChangedFeedback.Type.Movement, isoObject.cellObject.transform);
          }
          else
          {
            yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.MovementPointLoss, fightId, pointsChangedEvent.parentEventId, isoObject, fightStatus.context);
            ValueChangedFeedback.Launch(pointsChangedEvent.valueAfter - pointsChangedEvent.valueBefore, ValueChangedFeedback.Type.Movement, isoObject.cellObject.transform);
          }
        }
        else
          Log.Error(FightEventErrors.EntityHasNoView((IEntityWithBoardPresence) entityStatus), 62, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MovementPointsChangedEvent.cs");
        isoObject = (IsoObject) null;
      }
      else
      {
        // ISSUE: explicit non-virtual call
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithMovement>(__nonvirtual (pointsChangedEvent.concernedEntity)), 67, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MovementPointsChangedEvent.cs");
      }
      FightLogicExecutor.FireUpdateView(fightId, EventCategory.MovementPointsChanged);
    }
  }
}
