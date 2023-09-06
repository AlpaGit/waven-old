// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TurnSynchronizationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.States;
using Ankama.Utilities;
using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class TurnSynchronizationEvent : FightEvent
  {
    public int tmp { get; private set; }

    public TurnSynchronizationEvent(int eventId, int? parentEventId, int tmp)
      : base(FightEventData.Types.EventType.TurnSynchronization, eventId, parentEventId)
    {
      this.tmp = tmp;
    }

    public TurnSynchronizationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.TurnSynchronization, proto)
    {
      this.tmp = proto.Int1;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FightState instance = FightState.instance;
      if (instance == null)
      {
        Log.Error("Could not find fight state.", 17, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TurnSynchronizationEvent.cs");
      }
      else
      {
        FightFrame frame = instance.frame;
        if (frame == null)
        {
          Log.Error("Could not retrieve fight frame in fight state.", 24, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\TurnSynchronizationEvent.cs");
        }
        else
        {
          frame.SendPlayerReady();
          yield break;
        }
      }
    }
  }
}
