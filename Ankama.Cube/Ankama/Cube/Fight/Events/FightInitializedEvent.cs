// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FightInitializedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class FightInitializedEvent : FightEvent
  {
    public FightInitializedEvent(int eventId, int? parentEventId)
      : base(FightEventData.Types.EventType.FightInitialized, eventId, parentEventId)
    {
    }

    public FightInitializedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.FightInitialized, proto)
    {
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      if (fightStatus != FightStatus.local)
        return;
      FightLogicExecutor.fightInitialized = true;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      if (fightStatus == FightStatus.local)
      {
        CameraHandler current1 = CameraHandler.current;
        if ((Object) null != (Object) current1)
        {
          FightMap current2 = FightMap.current;
          if ((Object) null != (Object) current2)
            yield return (object) current1.FocusOnMapRegion((IMapDefinition) current2.definition, fightStatus.fightId, current1.cinematicControlParameters);
        }
      }
    }
  }
}
