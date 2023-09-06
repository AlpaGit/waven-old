// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.AttackEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class AttackEvent : FightEvent, IRelatedToEntity
  {
    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      CharacterStatus entityStatus;
      if (fightStatus.TryGetEntity<CharacterStatus>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.view is IObjectWithAction view)
        {
          Vector2Int attackerCoord = (Vector2Int) this.attackerCoord;
          Vector2Int targetCoord = (Vector2Int) this.targetCoord;
          yield return (object) view.DoAction(attackerCoord, targetCoord);
        }
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAction>((IEntityWithBoardPresence) entityStatus), 27, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\AttackEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<CharacterStatus>(this.concernedEntity), 32, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\AttackEvent.cs");
    }

    public int concernedEntity { get; private set; }

    public int target { get; private set; }

    public CellCoord attackerCoord { get; private set; }

    public CellCoord targetCoord { get; private set; }

    public AttackEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int target,
      CellCoord attackerCoord,
      CellCoord targetCoord)
      : base(FightEventData.Types.EventType.Attack, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.target = target;
      this.attackerCoord = attackerCoord;
      this.targetCoord = targetCoord;
    }

    public AttackEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.Attack, proto)
    {
      this.concernedEntity = proto.Int1;
      this.target = proto.Int2;
      this.attackerCoord = proto.CellCoord1;
      this.targetCoord = proto.CellCoord2;
    }
  }
}
