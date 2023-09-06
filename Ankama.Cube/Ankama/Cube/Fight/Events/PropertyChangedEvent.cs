// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.PropertyChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class PropertyChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int propertyId { get; private set; }

    public bool active { get; private set; }

    public int? propertyReplaced { get; private set; }

    public PropertyChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int propertyId,
      bool active,
      int? propertyReplaced)
      : base(FightEventData.Types.EventType.PropertyChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.propertyId = propertyId;
      this.active = active;
      this.propertyReplaced = propertyReplaced;
    }

    public PropertyChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.PropertyChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.propertyId = proto.Int2;
      this.active = proto.Bool1;
      this.propertyReplaced = proto.OptInt1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PropertyId propertyId = (PropertyId) this.propertyId;
      EntityStatus entityStatus;
      if (fightStatus.TryGetEntity(this.concernedEntity, out entityStatus))
      {
        int? propertyReplaced = this.propertyReplaced;
        if (propertyReplaced.HasValue)
        {
          propertyReplaced = this.propertyReplaced;
          PropertyId property = (PropertyId) propertyReplaced.Value;
          if (property != propertyId)
          {
            entityStatus.RemoveProperty(property);
            this.PropertyStatusChanged(fightStatus, property);
            entityStatus.AddProperty(propertyId);
            this.PropertyStatusChanged(fightStatus, propertyId);
          }
        }
        else
        {
          if (this.active)
            entityStatus.AddProperty(propertyId);
          else
            entityStatus.RemoveProperty(propertyId);
          this.PropertyStatusChanged(fightStatus, propertyId);
        }
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<EntityStatus>(this.concernedEntity), 49, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PropertyChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.PropertyChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PropertyId property = (PropertyId) this.propertyId;
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.view is ICharacterObject characterObject)
        {
          if (this.propertyReplaced.HasValue)
          {
            PropertyId removedProperty = (PropertyId) this.propertyReplaced.Value;
            if (removedProperty != property)
            {
              if (PropertiesUtility.IsSightProperty(property))
              {
                yield return (object) PropertyChangedEvent.TryChangeSightVisual(characterObject, new PropertyId?(property));
              }
              else
              {
                yield return (object) PropertyChangedEvent.RemovePropertyFromView(characterObject, removedProperty);
                yield return (object) this.PropertyViewChanged(fightStatus, removedProperty);
                yield return (object) PropertyChangedEvent.AddPropertyToView(characterObject, property);
                yield return (object) this.PropertyViewChanged(fightStatus, property);
              }
            }
          }
          else
          {
            if (this.active)
            {
              if (PropertiesUtility.IsSightProperty(property))
                yield return (object) PropertyChangedEvent.TryChangeSightVisual(characterObject, new PropertyId?(property));
              else
                yield return (object) PropertyChangedEvent.AddPropertyToView(characterObject, property);
            }
            else if (PropertiesUtility.IsSightProperty(property))
              yield return (object) PropertyChangedEvent.TryChangeSightVisual(characterObject, new PropertyId?());
            else
              yield return (object) PropertyChangedEvent.RemovePropertyFromView(characterObject, property);
            yield return (object) this.PropertyViewChanged(fightStatus, property);
          }
        }
        characterObject = (ICharacterObject) null;
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 113, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PropertyChangedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.PropertyChanged);
    }

    private static IEnumerator AddPropertyToView(
      ICharacterObject characterObject,
      PropertyId property)
    {
      AttachableEffect attachableEffect;
      if (FightSpellEffectFactory.isReady && FightSpellEffectFactory.TryGetPropertyEffect(property, out attachableEffect) && (Object) null != (Object) attachableEffect)
        yield return (object) characterObject.AddPropertyEffect(attachableEffect, property);
    }

    private static IEnumerator RemovePropertyFromView(
      ICharacterObject characterObject,
      PropertyId property)
    {
      AttachableEffect attachableEffect;
      if (FightSpellEffectFactory.isReady && FightSpellEffectFactory.TryGetPropertyEffect(property, out attachableEffect))
        yield return (object) characterObject.RemovePropertyEffect(attachableEffect, property);
    }

    private void PropertyStatusChanged(FightStatus fightStatus, PropertyId property)
    {
      PlayerStatus entityStatus;
      if (property == PropertyId.PlaySpellForbidden && fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
          view.RefreshAvailableActions(false);
      }
      if (!PropertiesUtility.PreventsAction(property))
        return;
      fightStatus.NotifyEntityPlayableStateChanged();
    }

    private IEnumerator PropertyViewChanged(FightStatus fightStatus, PropertyId property)
    {
      PlayerStatus entityStatus;
      if (property == PropertyId.PlaySpellForbidden && fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
        {
          view.UpdateAvailableActions(false);
          yield break;
        }
      }
    }

    private static IEnumerator TryChangeSightVisual(
      ICharacterObject characterObject,
      PropertyId? property)
    {
      if (characterObject is IObjectWithCounterEffects withCounterEffects)
      {
        FloatingCounterEffect floatingEffectCounter;
        if (FightSpellEffectFactory.isReady && FightSpellEffectFactory.TryGetFloatingCounterEffect(CaracId.FloatingCounterSight, property, out floatingEffectCounter))
          yield return (object) withCounterEffects.ChangeFloatingCounterEffect(floatingEffectCounter);
      }
      else
        Log.Warning(string.Format("Try to set a {0} on {1}, which is not a {2}", (object) "SightPropertyId", (object) characterObject, (object) "IObjectWithCounterEffects"), 211, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PropertyChangedEvent.cs");
    }
  }
}
