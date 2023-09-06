// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.SpellCostModifierRemovedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class SpellCostModifierRemovedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int stoppedModifierId { get; private set; }

    public SpellCostModifierRemovedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int stoppedModifierId)
      : base(FightEventData.Types.EventType.SpellCostModifierRemoved, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.stoppedModifierId = stoppedModifierId;
    }

    public SpellCostModifierRemovedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.SpellCostModifierRemoved, proto)
    {
      this.concernedEntity = proto.Int1;
      this.stoppedModifierId = proto.Int2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        entityStatus.RemoveSpellCostModifier(this.stoppedModifierId);
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
          view.RefreshAvailableActions(true);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 25, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SpellCostModifierRemovedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.SpellCostModification);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework playerUI = entityStatus.view;
        if ((Object) null != (Object) playerUI)
        {
          yield return (object) playerUI.RemoveSpellCostModifier(this.stoppedModifierId);
          playerUI.UpdateAvailableActions(true);
        }
        playerUI = (AbstractPlayerUIRework) null;
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 45, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SpellCostModifierRemovedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.SpellCostModification);
    }
  }
}
