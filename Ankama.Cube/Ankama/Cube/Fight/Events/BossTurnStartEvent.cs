// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.BossTurnStartEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Fight;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class BossTurnStartEvent : FightEvent
  {
    public BossTurnStartEvent(int eventId, int? parentEventId)
      : base(FightEventData.Types.EventType.BossTurnStart, eventId, parentEventId)
    {
    }

    public BossTurnStartEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.BossTurnStart, proto)
    {
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      if (fightStatus == FightStatus.local)
      {
        FightUIRework instance = FightUIRework.instance;
        if ((Object) null != (Object) instance)
        {
          int i18nNameId = GameStatus.fightDefinition.i18nNameId;
          yield return (object) instance.ShowTurnFeedback(TurnFeedbackUI.Type.Boss, i18nNameId);
        }
      }
    }

    public override bool CanBeGroupedWith(FightEvent other) => false;

    public override bool SynchronizeExecution() => true;
  }
}
