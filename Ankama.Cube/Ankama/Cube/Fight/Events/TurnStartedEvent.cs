// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TurnStartedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class TurnStartedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int turnIndex { get; private set; }

    public TurnStartedEvent(int eventId, int? parentEventId, int concernedEntity, int turnIndex)
      : base(FightEventData.Types.EventType.TurnStarted, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.turnIndex = turnIndex;
    }

    public TurnStartedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.TurnStarted, proto)
    {
      this.concernedEntity = proto.Int1;
      this.turnIndex = proto.Int2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      fightStatus.currentTurnPlayerId = this.concernedEntity;
      foreach (CharacterStatus enumerateEntity in fightStatus.EnumerateEntities<CharacterStatus>((Predicate<CharacterStatus>) (c => c.ownerId == this.concernedEntity)))
        enumerateEntity.actionUsed = false;
      fightStatus.NotifyEntityPlayableStateChanged();
      if (fightStatus != FightStatus.local || fightStatus.localPlayerId != this.concernedEntity)
        return;
      FightMap current = FightMap.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        current.SetMovementPhase();
      FightUIRework instance = FightUIRework.instance;
      if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
        instance.StartLocalPlayerTurn();
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) view))
          return;
        view.SetUIInteractable(true);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 55, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TurnStartedEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      TurnStartedEvent turnStartedEvent = this;
      // ISSUE: reference to a compiler-generated method
      foreach (CharacterStatus enumerateEntity in fightStatus.EnumerateEntities<CharacterStatus>(new Predicate<CharacterStatus>(turnStartedEvent.\u003CUpdateView\u003Eb__11_0)))
      {
        if (enumerateEntity.view is IObjectWithAction view)
          view.SetActionUsed(false, false);
      }
      if (fightStatus == FightStatus.local)
      {
        FightUIRework instance = FightUIRework.instance;
        if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
        {
          switch (GameStatus.fightType)
          {
            case FightType.Versus:
              PlayerStatus entityStatus;
              // ISSUE: explicit non-virtual call
              if (!fightStatus.TryGetEntity<PlayerStatus>(__nonvirtual (turnStartedEvent.concernedEntity), out entityStatus))
                break;
              if (entityStatus.isLocalPlayer)
              {
                yield return (object) instance.ShowTurnFeedback(TurnFeedbackUI.Type.Player, 61373);
                break;
              }
              yield return (object) instance.ShowTurnFeedback(TurnFeedbackUI.Type.Opponent, 30091);
              break;
            case FightType.BossFight:
              break;
            case FightType.TeamVersus:
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
      }
    }
  }
}
