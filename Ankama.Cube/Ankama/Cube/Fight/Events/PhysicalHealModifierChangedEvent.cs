// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.PhysicalHealModifierChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class PhysicalHealModifierChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public PhysicalHealModifierChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter)
      : base(FightEventData.Types.EventType.PhysicalHealModifierChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
    }

    public PhysicalHealModifierChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.PhysicalHealModifierChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
        entityStatus.SetCarac(CaracId.PhysicalHealModifier, this.valueAfter);
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 19, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalHealModifierChangedEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
      {
        if (entityStatus.view is IObjectWithAction view)
          view.SetPhysicalHealBoost(this.valueAfter);
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IEntityWithBoardPresence>(entityStatus), 33, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalHealModifierChangedEvent.cs");
      }
      else
      {
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 38, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PhysicalHealModifierChangedEvent.cs");
        yield break;
      }
    }
  }
}
