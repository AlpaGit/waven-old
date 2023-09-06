// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.PhysicalDamageModifierChangedEvent
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
  public class PhysicalDamageModifierChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public PhysicalDamageModifierChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter)
      : base(FightEventData.Types.EventType.PhysicalDamageModifierChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
    }

    public PhysicalDamageModifierChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.PhysicalDamageModifierChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
        entityStatus.SetCarac(CaracId.PhysicalDamageModifier, this.valueAfter);
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 20, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalDamageModifierChangedEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PhysicalDamageModifierChangedEvent modifierChangedEvent = this;
      IEntityWithBoardPresence entityStatus;
      // ISSUE: explicit non-virtual call
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(__nonvirtual (modifierChangedEvent.concernedEntity), out entityStatus))
      {
        IsoObject view = entityStatus.view;
        if ((Object) null != (Object) view)
        {
          if (view is IObjectWithAction objectWithAction)
            objectWithAction.SetPhysicalDamageBoost(modifierChangedEvent.valueAfter);
          else
            Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAction>(entityStatus), 37, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalDamageModifierChangedEvent.cs");
          CellObject cellObject = view.cellObject;
          if ((Object) null != (Object) cellObject)
          {
            if (modifierChangedEvent.valueAfter > modifierChangedEvent.valueBefore)
            {
              yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.PhysicalDamageGain, fightStatus.fightId, modifierChangedEvent.parentEventId, view, fightStatus.context);
              ValueChangedFeedback.Launch(modifierChangedEvent.valueAfter - modifierChangedEvent.valueBefore, ValueChangedFeedback.Type.Action, cellObject.transform);
            }
            else
            {
              yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.PhysicalDamageLoss, fightStatus.fightId, modifierChangedEvent.parentEventId, view, fightStatus.context);
              ValueChangedFeedback.Launch(modifierChangedEvent.valueAfter - modifierChangedEvent.valueBefore, ValueChangedFeedback.Type.Action, cellObject.transform);
            }
          }
          else
            Log.Warning("Tried to apply a damage modifier on target named " + view.name + " (" + view.GetType().Name + ") but the target is no longer on the board.", 56, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalDamageModifierChangedEvent.cs");
          cellObject = (CellObject) null;
        }
        else
          Log.Error(FightEventErrors.EntityHasNoView(entityStatus), 61, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalDamageModifierChangedEvent.cs");
      }
      else
      {
        // ISSUE: explicit non-virtual call
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(__nonvirtual (modifierChangedEvent.concernedEntity)), 66, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalDamageModifierChangedEvent.cs");
      }
    }
  }
}
