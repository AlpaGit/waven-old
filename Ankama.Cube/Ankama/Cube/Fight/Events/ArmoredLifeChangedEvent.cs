// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ArmoredLifeChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.UI.Fight;
using Ankama.Cube.UI.Fight.Info;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class ArmoredLifeChangedEvent : FightEvent, IRelatedToEntity
  {
    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithLife entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithLife>(this.concernedEntity, out entityStatus))
      {
        int? nullable;
        if (this.armorAfter.HasValue)
        {
          IEntityWithLife entityWithLife = entityStatus;
          nullable = this.armorAfter;
          int num = nullable.Value;
          entityWithLife.SetCarac(CaracId.Armor, num);
        }
        nullable = this.lifeAfter;
        if (nullable.HasValue)
        {
          IEntityWithLife entityWithLife = entityStatus;
          nullable = this.lifeAfter;
          int num = nullable.Value;
          entityWithLife.SetCarac(CaracId.Life, num);
        }
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithLife>(this.concernedEntity), 41, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ArmoredLifeChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.LifeArmorChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      ArmoredLifeChangedEvent lifeChangedEvent = this;
      int fightId = fightStatus.fightId;
      IEntityWithBoardPresence entityStatus1;
      // ISSUE: explicit non-virtual call
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(__nonvirtual (lifeChangedEvent.concernedEntity), out entityStatus1))
      {
        int change = 0;
        int num1 = 0;
        int num2 = 0;
        ArmoredLifeChangedEvent.LifeModificationType modificationType = ArmoredLifeChangedEvent.LifeModificationType.Undefined;
        int? nullable = lifeChangedEvent.armorAfter;
        if (nullable.HasValue)
        {
          nullable = lifeChangedEvent.armorAfter;
          num1 = nullable.Value;
          nullable = lifeChangedEvent.armorBefore;
          if (nullable.HasValue)
          {
            nullable = lifeChangedEvent.armorBefore;
            int num3 = nullable.Value;
            change += num1 - num3;
            if (num3 > num1)
              modificationType = ArmoredLifeChangedEvent.LifeModificationType.Damage;
            else if (num3 < num1)
              modificationType = ArmoredLifeChangedEvent.LifeModificationType.ArmorGain;
          }
        }
        nullable = lifeChangedEvent.lifeAfter;
        if (nullable.HasValue)
        {
          nullable = lifeChangedEvent.lifeAfter;
          num2 = nullable.Value;
          nullable = lifeChangedEvent.lifeBefore;
          if (nullable.HasValue)
          {
            nullable = lifeChangedEvent.lifeBefore;
            int num4 = nullable.Value;
            change += num2 - num4;
            if (num4 > num2)
              modificationType = num2 > 0 ? ArmoredLifeChangedEvent.LifeModificationType.Damage : ArmoredLifeChangedEvent.LifeModificationType.Death;
            else if (num4 < num2)
              modificationType = entityStatus1.type != EntityType.ObjectMechanism ? ArmoredLifeChangedEvent.LifeModificationType.Heal : ArmoredLifeChangedEvent.LifeModificationType.ArmorGain;
          }
        }
        nullable = lifeChangedEvent.lifeAfter;
        if (nullable.HasValue && entityStatus1.type == EntityType.Hero)
        {
          HeroStatus heroStatus = (HeroStatus) entityStatus1;
          if (heroStatus.ownerId == fightStatus.localPlayerId)
          {
            FightMap current = FightMap.current;
            if ((UnityEngine.Object) null != (UnityEngine.Object) current)
              current.SetLocalPlayerHeroLife(num2, heroStatus.baseLife);
          }
          PlayerStatus entityStatus2;
          if (fightStatus.TryGetEntity<PlayerStatus>(heroStatus.ownerId, out entityStatus2))
          {
            AbstractPlayerUIRework view = entityStatus2.view;
            if ((UnityEngine.Object) null != (UnityEngine.Object) view)
              view.ChangeHeroLifePoints(num2);
            lifeChangedEvent.TryDrawLowLifeMessage(num2, entityStatus2);
          }
        }
        IsoObject isoObject = entityStatus1.view;
        if ((UnityEngine.Object) null != (UnityEngine.Object) isoObject)
        {
          if (isoObject is IObjectWithArmoredLife objectWithArmoredLife)
          {
            nullable = lifeChangedEvent.lifeAfter;
            int life = nullable.HasValue ? num2 : objectWithArmoredLife.life;
            nullable = lifeChangedEvent.armorAfter;
            int armor = nullable.HasValue ? num1 : objectWithArmoredLife.armor;
            objectWithArmoredLife.SetArmoredLife(life, armor);
          }
          else
          {
            Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithArmoredLife>(entityStatus1), 151, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ArmoredLifeChangedEvent.cs");
            objectWithArmoredLife = (IObjectWithArmoredLife) null;
          }
          switch (modificationType)
          {
            case ArmoredLifeChangedEvent.LifeModificationType.Undefined:
              objectWithArmoredLife = (IObjectWithArmoredLife) null;
              break;
            case ArmoredLifeChangedEvent.LifeModificationType.ArmorGain:
              yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.ArmorGain, fightId, lifeChangedEvent.parentEventId, isoObject, fightStatus.context);
              ValueChangedFeedback.Launch(change, ValueChangedFeedback.Type.Heal, isoObject.cellObject.transform);
              goto case ArmoredLifeChangedEvent.LifeModificationType.Undefined;
            case ArmoredLifeChangedEvent.LifeModificationType.Damage:
              yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.Damage, fightId, lifeChangedEvent.parentEventId, isoObject, fightStatus.context);
              ValueChangedFeedback.Launch(change, ValueChangedFeedback.Type.Damage, isoObject.cellObject.transform);
              if (objectWithArmoredLife != null)
              {
                yield return (object) objectWithArmoredLife.Hit(isoObject.area.refCoord);
                goto case ArmoredLifeChangedEvent.LifeModificationType.Undefined;
              }
              else
                goto case ArmoredLifeChangedEvent.LifeModificationType.Undefined;
            case ArmoredLifeChangedEvent.LifeModificationType.Heal:
              yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.Heal, fightId, lifeChangedEvent.parentEventId, isoObject, fightStatus.context);
              ValueChangedFeedback.Launch(change, ValueChangedFeedback.Type.Heal, isoObject.cellObject.transform);
              goto case ArmoredLifeChangedEvent.LifeModificationType.Undefined;
            case ArmoredLifeChangedEvent.LifeModificationType.Death:
              yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.Damage, fightId, lifeChangedEvent.parentEventId, isoObject, fightStatus.context);
              ValueChangedFeedback.Launch(change, ValueChangedFeedback.Type.Damage, isoObject.cellObject.transform);
              if (objectWithArmoredLife != null)
              {
                objectWithArmoredLife.LethalHit(isoObject.area.refCoord);
                goto case ArmoredLifeChangedEvent.LifeModificationType.Undefined;
              }
              else
                goto case ArmoredLifeChangedEvent.LifeModificationType.Undefined;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
        else
          Log.Error(FightEventErrors.EntityHasNoView(entityStatus1), 197, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ArmoredLifeChangedEvent.cs");
        isoObject = (IsoObject) null;
      }
      else
      {
        // ISSUE: explicit non-virtual call
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(__nonvirtual (lifeChangedEvent.concernedEntity)), 202, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ArmoredLifeChangedEvent.cs");
      }
      FightLogicExecutor.FireUpdateView(fightId, EventCategory.LifeArmorChanged);
    }

    private void TryDrawLowLifeMessage(int lifeAfterValue, PlayerStatus ownerStatus)
    {
      switch (GameStatus.fightType)
      {
        case FightType.BossFight:
        case FightType.TeamVersus:
          if (lifeAfterValue <= 0)
            break;
          int num = (int) ((double) ownerStatus.heroStatus.baseLife * 0.34999999403953552);
          if (lifeAfterValue > num || this.lifeBefore.HasValue && this.lifeBefore.Value <= num)
            break;
          MessageInfoRibbonGroup messageGroup = GameStatus.localPlayerTeamIndex == ownerStatus.teamIndex ? MessageInfoRibbonGroup.MyID : MessageInfoRibbonGroup.OtherID;
          FightUIRework instance = FightUIRework.instance;
          if (!((UnityEngine.Object) null != (UnityEngine.Object) instance))
            break;
          FightInfoMessage message = FightInfoMessage.HeroLowLife(messageGroup);
          instance.DrawInfoMessage(message, ownerStatus.nickname);
          break;
      }
    }

    public int concernedEntity { get; private set; }

    public int? lifeBefore { get; private set; }

    public int? lifeAfter { get; private set; }

    public int? armorBefore { get; private set; }

    public int? armorAfter { get; private set; }

    public ArmoredLifeChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int? lifeBefore,
      int? lifeAfter,
      int? armorBefore,
      int? armorAfter)
      : base(FightEventData.Types.EventType.ArmoredLifeChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.lifeBefore = lifeBefore;
      this.lifeAfter = lifeAfter;
      this.armorBefore = armorBefore;
      this.armorAfter = armorAfter;
    }

    public ArmoredLifeChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.ArmoredLifeChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.lifeBefore = proto.OptInt1;
      this.lifeAfter = proto.OptInt2;
      this.armorBefore = proto.OptInt3;
      this.armorAfter = proto.OptInt4;
    }

    private enum LifeModificationType
    {
      Undefined,
      ArmorGain,
      Damage,
      Heal,
      Death,
    }
  }
}
