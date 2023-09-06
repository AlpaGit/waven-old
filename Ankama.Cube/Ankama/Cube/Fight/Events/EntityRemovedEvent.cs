// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityRemovedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.UI.Fight;
using Ankama.Cube.UI.Fight.Info;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class EntityRemovedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int reason { get; private set; }

    public EntityRemovedEvent(int eventId, int? parentEventId, int concernedEntity, int reason)
      : base(FightEventData.Types.EventType.EntityRemoved, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.reason = reason;
    }

    public EntityRemovedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityRemoved, proto)
    {
      this.concernedEntity = proto.Int1;
      this.reason = proto.Int2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      fightStatus.RemoveEntity(this.concernedEntity);
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
      {
        IsoObject view = entityStatus.view;
        if ((UnityEngine.Object) null != (UnityEngine.Object) view)
        {
          switch (this.reason)
          {
            case 1:
              view.DetachFromCell();
              view.Destroy();
              break;
            case 2:
            case 4:
            case 6:
            case 8:
            case 9:
              if (view is ICharacterObject characterObject1)
                yield return (object) characterObject1.Die();
              if (entityStatus is HeroStatus heroStatus)
              {
                PlayerStatus entityStatus1;
                if (fightStatus.TryGetEntity<PlayerStatus>(heroStatus.ownerId, out entityStatus1))
                {
                  AbstractPlayerUIRework view1 = entityStatus1.view;
                  if ((UnityEngine.Object) null != (UnityEngine.Object) view1)
                    view1.ChangeHeroLifePoints(0);
                  if (GameStatus.fightType == FightType.BossFight)
                  {
                    FightUIRework instance = FightUIRework.instance;
                    if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
                    {
                      FightInfoMessage message = FightInfoMessage.HeroDeath(MessageInfoRibbonGroup.MyID);
                      instance.DrawInfoMessage(message, entityStatus1.nickname);
                      goto case 1;
                    }
                    else
                      goto case 1;
                  }
                  else
                    goto case 1;
                }
                else
                {
                  Log.Error(FightEventErrors.PlayerNotFound(heroStatus.ownerId), 68, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityRemovedEvent.cs");
                  goto case 1;
                }
              }
              else
                goto case 1;
            case 3:
              throw new ArgumentException("Transformations should not trigger an EntityRemovedEvent.");
            case 5:
              if (view is ICharacterObject characterObject2)
              {
                yield return (object) characterObject2.Die();
                goto case 1;
              }
              else
              {
                Log.Error(FightEventErrors.EntityHasIncompatibleView<ICharacterObject>(entityStatus), 95, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityRemovedEvent.cs");
                goto case 1;
              }
            case 7:
              if (view is IObjectWithActivation objectWithActivation)
              {
                yield return (object) objectWithActivation.WaitForActivationEnd();
                goto case 1;
              }
              else
              {
                Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithActivation>(entityStatus), 82, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityRemovedEvent.cs");
                goto case 1;
              }
            default:
              throw new ArgumentOutOfRangeException(string.Format("EntityRemovedReason not handled: {0}", (object) this.reason));
          }
        }
        else
          Log.Error(FightEventErrors.EntityHasNoView(entityStatus), 112, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityRemovedEvent.cs");
        view = (IsoObject) null;
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 117, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityRemovedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.EntityAddedOrRemoved);
    }
  }
}
