// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.SpellCostModifierAddedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.CostModifiers;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class SpellCostModifierAddedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int modifierId { get; private set; }

    public int modificationValue { get; private set; }

    public string spellFiltersJson { get; private set; }

    public SpellCostModifierAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int modifierId,
      int modificationValue,
      string spellFiltersJson)
      : base(FightEventData.Types.EventType.SpellCostModifierAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.modifierId = modifierId;
      this.modificationValue = modificationValue;
      this.spellFiltersJson = spellFiltersJson;
    }

    public SpellCostModifierAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.SpellCostModifierAdded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.modifierId = proto.Int2;
      this.modificationValue = proto.Int3;
      this.spellFiltersJson = proto.String1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        SpellCostModification spellCostModifier = new SpellCostModification(this.modifierId, this.modificationValue, this.spellFiltersJson);
        entityStatus.AddSpellCostModifier(spellCostModifier);
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
          view.RefreshAvailableActions(true);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 27, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SpellCostModifierAddedEvent.cs");
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
          yield return (object) playerUI.AddSpellCostModifier(new SpellCostModification(this.modifierId, this.modificationValue, this.spellFiltersJson));
          playerUI.UpdateAvailableActions(true);
        }
        playerUI = (AbstractPlayerUIRework) null;
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 48, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\SpellCostModifierAddedEvent.cs");
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.SpellCostModification);
    }
  }
}
