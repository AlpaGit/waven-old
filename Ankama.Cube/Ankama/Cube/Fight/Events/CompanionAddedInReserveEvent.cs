// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.CompanionAddedInReserveEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class CompanionAddedInReserveEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public IReadOnlyList<int> companionDefId { get; private set; }

    public IReadOnlyList<int> levels { get; private set; }

    public CompanionAddedInReserveEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      IReadOnlyList<int> companionDefId,
      IReadOnlyList<int> levels)
      : base(FightEventData.Types.EventType.CompanionAddedInReserve, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.companionDefId = companionDefId;
      this.levels = levels;
    }

    public CompanionAddedInReserveEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.CompanionAddedInReserve, proto)
    {
      this.concernedEntity = proto.Int1;
      this.companionDefId = (IReadOnlyList<int>) proto.IntList1;
      this.levels = (IReadOnlyList<int>) proto.IntList2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        entityStatus.SetAvailableCompanions(this.companionDefId, this.levels);
        AbstractPlayerUIRework view = entityStatus.view;
        if (!((Object) null != (Object) view))
          return;
        int count = ((IReadOnlyCollection<int>) this.companionDefId).Count;
        for (int index = 0; index < count; ++index)
          view.AddCompanionStatus(this.companionDefId[index], this.levels[index], index);
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 32, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionAddedInReserveEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        AbstractPlayerUIRework view = entityStatus.view;
        if ((Object) null != (Object) view)
        {
          int count = ((IReadOnlyCollection<int>) this.companionDefId).Count;
          if (count > 0)
          {
            IEnumerator[] enumeratorArray = new IEnumerator[count];
            for (int index = 0; index < count; ++index)
              enumeratorArray[index] = view.AddCompanion(this.companionDefId[index], this.levels[index], index);
            yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(enumeratorArray);
          }
        }
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 59, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionAddedInReserveEvent.cs");
    }
  }
}
