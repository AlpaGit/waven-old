// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.MagicalDamageModifierChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Utilities;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class MagicalDamageModifierChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public MagicalDamageModifierChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int valueBefore,
      int valueAfter)
      : base(FightEventData.Types.EventType.MagicalDamageModifierChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
    }

    public MagicalDamageModifierChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.MagicalDamageModifierChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
      this.valueAfter = proto.Int3;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntity entityStatus;
      if (fightStatus.TryGetEntity<IEntity>(this.concernedEntity, out entityStatus))
        entityStatus.SetCarac(CaracId.MagicalDamageModifier, this.valueAfter);
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntity>(this.concernedEntity), 18, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\MagicalDamageModifierChangedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.DamageModifierChanged);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FightLogicExecutor.FireUpdateView(fightStatus.fightId, EventCategory.DamageModifierChanged);
      yield break;
    }
  }
}
