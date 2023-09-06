// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.GameEndedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.States;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class GameEndedEvent : FightEvent
  {
    public FightResult result { get; private set; }

    [CanBeNull]
    public GameStatistics gameStats { get; private set; }

    public int fightDuration { get; private set; }

    public GameEndedEvent(
      int eventId,
      int? parentEventId,
      FightResult result,
      GameStatistics gameStats,
      int fightDuration)
      : base(FightEventData.Types.EventType.GameEnded, eventId, parentEventId)
    {
      this.result = result;
      this.gameStats = gameStats;
      this.fightDuration = fightDuration;
    }

    public GameEndedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.GameEnded, proto)
    {
      this.result = proto.FightResult1;
      this.gameStats = proto.GameStatistics1;
      this.fightDuration = proto.Int1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      GameStatus.hasEnded = true;
      FightUIRework instance = FightUIRework.instance;
      if (!((Object) null != (Object) instance))
        return;
      instance.SetResignButtonEnabled(false);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FightState instance = FightState.instance;
      if (instance != null)
      {
        instance.GotoFightEndState(this.result, this.gameStats, this.fightDuration);
      }
      else
      {
        Log.Error("Could not find fight state.", 30, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\GameEndedEvent.cs");
        yield break;
      }
    }
  }
}
