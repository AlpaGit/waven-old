// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TurnEndedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class TurnEndedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int turnIndex { get; private set; }

    public TurnEndedEvent(int eventId, int? parentEventId, int concernedEntity, int turnIndex)
      : base(FightEventData.Types.EventType.TurnEnded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.turnIndex = turnIndex;
    }

    public TurnEndedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.TurnEnded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.turnIndex = proto.Int2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      fightStatus.currentTurnPlayerId = 0;
      if (fightStatus != FightStatus.local || fightStatus.localPlayerId != this.concernedEntity)
        return;
      switch (FightCastManager.currentCastType)
      {
        case FightCastManager.CurrentCastType.None:
          FightMap current = FightMap.current;
          if ((UnityEngine.Object) null != (UnityEngine.Object) current)
            current.SetNoInteractionPhase();
          FightUIRework instance = FightUIRework.instance;
          if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
            instance.EndLocalPlayerTurn();
          PlayerStatus entityStatus;
          if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
          {
            AbstractPlayerUIRework view = entityStatus.view;
            if (!((UnityEngine.Object) null != (UnityEngine.Object) view))
              break;
            view.SetUIInteractable(false);
            break;
          }
          Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 60, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TurnEndedEvent.cs");
          break;
        case FightCastManager.CurrentCastType.Spell:
          FightCastManager.StopCastingSpell(true);
          goto case FightCastManager.CurrentCastType.None;
        case FightCastManager.CurrentCastType.Companion:
          FightCastManager.StopInvokingCompanion(true);
          goto case FightCastManager.CurrentCastType.None;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      TurnEndedEvent turnEndedEvent = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated method
      foreach (CharacterStatus enumerateEntity in fightStatus.EnumerateEntities<CharacterStatus>(new Predicate<CharacterStatus>(turnEndedEvent.\u003CUpdateView\u003Eb__11_0)))
      {
        if (enumerateEntity.view is IObjectWithAction view)
          view.SetActionUsed(true, true);
      }
      return false;
    }
  }
}
