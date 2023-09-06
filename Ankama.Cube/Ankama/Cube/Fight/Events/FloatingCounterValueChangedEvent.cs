// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FloatingCounterValueChangedEvent
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
  public class FloatingCounterValueChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public int floatingCounterType { get; private set; }

    public int? sightProperty { get; private set; }

    public int? counterReplaced { get; private set; }

    public FloatingCounterValueChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter,
      int floatingCounterType,
      int? sightProperty,
      int? counterReplaced)
      : base(FightEventData.Types.EventType.FloatingCounterValueChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
      this.floatingCounterType = floatingCounterType;
      this.sightProperty = sightProperty;
      this.counterReplaced = counterReplaced;
    }

    public FloatingCounterValueChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.FloatingCounterValueChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
      this.floatingCounterType = proto.Int4;
      this.sightProperty = proto.OptInt1;
      this.counterReplaced = proto.OptInt2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntity entityStatus;
      if (fightStatus.TryGetEntity<IEntity>(this.concernedEntity, out entityStatus))
      {
        if (this.counterReplaced.HasValue)
          entityStatus.SetCarac((CaracId) this.counterReplaced.Value, 0);
        CaracId floatingCounterType = (CaracId) this.floatingCounterType;
        entityStatus.SetCarac(floatingCounterType, this.valueAfter);
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntity>(this.concernedEntity), 27, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloatingCounterValueChangedEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FloatingCounterValueChangedEvent valueChangedEvent = this;
      IEntityWithBoardPresence entityStatus;
      // ISSUE: explicit non-virtual call
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(__nonvirtual (valueChangedEvent.concernedEntity), out entityStatus))
      {
        if (entityStatus.view is IObjectWithCounterEffects isoObject)
        {
          if (valueChangedEvent.counterReplaced.HasValue)
            yield return (object) isoObject.RemoveFloatingCounterEffect();
          CaracId floatingCounterType = (CaracId) valueChangedEvent.floatingCounterType;
          int counterId = (int) floatingCounterType;
          int? nullable = valueChangedEvent.sightProperty;
          PropertyId? propertyId = nullable.HasValue ? new PropertyId?((PropertyId) nullable.GetValueOrDefault()) : new PropertyId?();
          FloatingCounterEffect effect;
          ref FloatingCounterEffect local = ref effect;
          if (FightSpellEffectFactory.TryGetFloatingCounterEffect((CaracId) counterId, propertyId, out local))
          {
            if ((Object) null != (Object) effect)
            {
              if (valueChangedEvent.valueBefore != 0)
              {
                nullable = valueChangedEvent.counterReplaced;
                if (!nullable.HasValue)
                {
                  FloatingCounterFeedback floatingCounterFeedback = isoObject.GetCurrentFloatingCounterFeedback();
                  floatingCounterFeedback.ChangeVisual(effect);
                  yield return (object) floatingCounterFeedback.SetCount(valueChangedEvent.valueAfter);
                  goto label_10;
                }
              }
              yield return (object) isoObject.InitializeFloatingCounterEffect(effect, valueChangedEvent.valueAfter);
label_10:
              if (valueChangedEvent.parentEventId.HasValue)
                FightSpellEffectFactory.SetupSpellEffectOverrides((ISpellEffectOverrideProvider) effect, fightStatus.fightId, valueChangedEvent.parentEventId.Value);
            }
            else
              Log.Error(string.Format("No prefab defined for {0} {1}.", (object) "FloatingCounterEffect", (object) floatingCounterType), 68, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloatingCounterValueChangedEvent.cs");
          }
          effect = (FloatingCounterEffect) null;
        }
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithCounterEffects>(entityStatus), 74, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloatingCounterValueChangedEvent.cs");
        isoObject = (IObjectWithCounterEffects) null;
      }
      else
      {
        // ISSUE: explicit non-virtual call
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(__nonvirtual (valueChangedEvent.concernedEntity)), 79, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\FloatingCounterValueChangedEvent.cs");
      }
    }
  }
}
