// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.BossLifeModificationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Cube.UI.Fight.Info;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class BossLifeModificationEvent : FightEvent
  {
    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public int sourceFightId { get; private set; }

    public int sourcePlayerId { get; private set; }

    public BossLifeModificationEvent(
      int eventId,
      int? parentEventId,
      int valueBefore,
      int valueAfter,
      int sourceFightId,
      int sourcePlayerId)
      : base(FightEventData.Types.EventType.BossLifeModification, eventId, parentEventId)
    {
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
      this.sourceFightId = sourceFightId;
      this.sourcePlayerId = sourcePlayerId;
    }

    public BossLifeModificationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.BossLifeModification, proto)
    {
      this.valueBefore = proto.Int1;
      this.valueAfter = proto.Int2;
      this.sourceFightId = proto.Int3;
      this.sourcePlayerId = proto.Int4;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (GameStatus.GetFightStatus(this.sourceFightId).TryGetEntity<PlayerStatus>(this.sourcePlayerId, out entityStatus))
      {
        FightUIRework instance = FightUIRework.instance;
        if ((Object) null != (Object) instance)
        {
          FightInfoMessage message = FightInfoMessage.BossPointEarn(MessageInfoRibbonGroup.MyID, this.valueBefore - this.valueAfter);
          instance.DrawScore(message, entityStatus.nickname);
        }
      }
      else
      {
        Log.Error(FightEventErrors.PlayerNotFound(this.sourcePlayerId, this.sourceFightId), 29, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\BossLifeModificationEvent.cs");
        yield break;
      }
    }
  }
}
