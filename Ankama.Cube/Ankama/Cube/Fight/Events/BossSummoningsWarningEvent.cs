// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.BossSummoningsWarningEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class BossSummoningsWarningEvent : FightEvent
  {
    public bool addWarning { get; private set; }

    public IReadOnlyList<CellCoord> positions { get; private set; }

    public IReadOnlyList<int> directions { get; private set; }

    public BossSummoningsWarningEvent(
      int eventId,
      int? parentEventId,
      bool addWarning,
      IReadOnlyList<CellCoord> positions,
      IReadOnlyList<int> directions)
      : base(FightEventData.Types.EventType.BossSummoningsWarning, eventId, parentEventId)
    {
      this.addWarning = addWarning;
      this.positions = positions;
      this.directions = directions;
    }

    public BossSummoningsWarningEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.BossSummoningsWarning, proto)
    {
      this.addWarning = proto.Bool1;
      this.positions = (IReadOnlyList<CellCoord>) proto.CellCoordList1;
      this.directions = (IReadOnlyList<int>) proto.IntList1;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      int positionCount = ((IReadOnlyCollection<CellCoord>) this.positions).Count;
      FightMap current = FightMap.current;
      if ((Object) null != (Object) current)
      {
        IEnumerator[] enumeratorArray = new IEnumerator[positionCount];
        if (this.addWarning)
        {
          for (int index = 0; index < positionCount; ++index)
          {
            CellCoord position = this.positions[index];
            Ankama.Cube.Data.Direction direction = (Ankama.Cube.Data.Direction) this.directions[index];
            enumeratorArray[index] = current.AddMonsterSpawnCell(position.X, position.Y, direction);
          }
        }
        else
        {
          for (int index = 0; index < positionCount; ++index)
          {
            CellCoord position = this.positions[index];
            enumeratorArray[index] = current.RemoveMonsterSpawnCell(position.X, position.Y);
          }
        }
        yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(enumeratorArray);
      }
      for (int index = 0; index < positionCount; ++index)
      {
        ICharacterEntity character;
        if (fightStatus.TryGetEntityAt<ICharacterEntity>((Vector2Int) this.positions[index], out character) && character.view is ICharacterObject view)
          view.CheckParentCellIndicator();
      }
    }
  }
}
