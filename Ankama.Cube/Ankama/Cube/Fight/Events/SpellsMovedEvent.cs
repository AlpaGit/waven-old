// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.SpellsMovedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.Fight.Events
{
  public class SpellsMovedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public IReadOnlyList<SpellMovement> moves { get; private set; }

    public SpellsMovedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      IReadOnlyList<SpellMovement> moves)
      : base(FightEventData.Types.EventType.SpellsMoved, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.moves = moves;
    }

    public SpellsMovedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.SpellsMoved, proto)
    {
      this.concernedEntity = proto.Int1;
      this.moves = (IReadOnlyList<SpellMovement>) proto.SpellMovementList1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        bool flag = fightStatus == FightStatus.local;
        int count = ((IReadOnlyCollection<SpellMovement>) this.moves).Count;
        for (int index = 0; index < count; ++index)
        {
          SpellMovement move = this.moves[index];
          if (flag)
            FightCastManager.CheckSpellPlayed(move.Spell.SpellInstanceId);
          switch (move.To)
          {
            case Ankama.Cube.Protocols.FightCommonProtocol.SpellMovementZone.Nowhere:
              int spellInstanceId1 = move.Spell.SpellInstanceId;
              entityStatus.RemoveSpell(spellInstanceId1);
              if (move.From == Ankama.Cube.Protocols.FightCommonProtocol.SpellMovementZone.Hand)
              {
                AbstractPlayerUIRework view = entityStatus.view;
                if ((UnityEngine.Object) null != (UnityEngine.Object) view)
                {
                  view.RemoveSpellStatus(spellInstanceId1, index);
                  break;
                }
                break;
              }
              break;
            case Ankama.Cube.Protocols.FightCommonProtocol.SpellMovementZone.Hand:
              SpellInfo spell = move.Spell;
              SpellStatus spellStatus = SpellStatus.TryCreate(spell, entityStatus);
              if (spellStatus != null)
              {
                entityStatus.AddSpell(spellStatus);
                AbstractPlayerUIRework view = entityStatus.view;
                if ((UnityEngine.Object) null != (UnityEngine.Object) view)
                {
                  view.AddSpellStatus(spell, index);
                  break;
                }
                break;
              }
              break;
            case Ankama.Cube.Protocols.FightCommonProtocol.SpellMovementZone.Deck:
              if (move.From == Ankama.Cube.Protocols.FightCommonProtocol.SpellMovementZone.Hand)
              {
                int spellInstanceId2 = move.Spell.SpellInstanceId;
                entityStatus.DiscardSpell(spellInstanceId2);
                AbstractPlayerUIRework view = entityStatus.view;
                if ((UnityEngine.Object) null != (UnityEngine.Object) view)
                {
                  view.RemoveSpellStatus(spellInstanceId2, index);
                  break;
                }
                break;
              }
              break;
            default:
              throw new ArgumentOutOfRangeException(string.Format("Spell moved to unknown zone: {0}", (object) move.To));
          }
        }
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 90, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SpellsMovedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.SpellsMoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((UnityEngine.Object) null != (UnityEngine.Object) view)
        {
          int count = ((IReadOnlyCollection<SpellMovement>) this.moves).Count;
          IEnumerator[] enumeratorArray = new IEnumerator[count];
          for (int index = 0; index < count; ++index)
          {
            SpellMovement move = this.moves[index];
            if (move.From == Ankama.Cube.Protocols.FightCommonProtocol.SpellMovementZone.Hand)
            {
              int spellInstanceId = move.Spell.SpellInstanceId;
              enumeratorArray[index] = view.RemoveSpell(spellInstanceId, index);
            }
            else if (move.To == Ankama.Cube.Protocols.FightCommonProtocol.SpellMovementZone.Hand)
              enumeratorArray[index] = view.AddSpell(move.Spell, index);
          }
          yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(enumeratorArray);
        }
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), (int) sbyte.MaxValue, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SpellsMovedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.SpellsMoved);
    }
  }
}
