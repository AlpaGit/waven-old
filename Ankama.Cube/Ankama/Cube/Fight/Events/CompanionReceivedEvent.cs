// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.CompanionReceivedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Ankama.Cube.UI.Fight.Info;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class CompanionReceivedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int companionDefId { get; private set; }

    public int companionLevel { get; private set; }

    public int fightId { get; private set; }

    public int fromFightId { get; private set; }

    public int fromPlayerId { get; private set; }

    public CompanionReceivedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int companionDefId,
      int companionLevel,
      int fightId,
      int fromFightId,
      int fromPlayerId)
      : base(FightEventData.Types.EventType.CompanionReceived, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.companionDefId = companionDefId;
      this.companionLevel = companionLevel;
      this.fightId = fightId;
      this.fromFightId = fromFightId;
      this.fromPlayerId = fromPlayerId;
    }

    public CompanionReceivedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.CompanionReceived, proto)
    {
      this.concernedEntity = proto.Int1;
      this.companionDefId = proto.Int2;
      this.companionLevel = proto.Int3;
      this.fightId = proto.Int4;
      this.fromFightId = proto.Int5;
      this.fromPlayerId = proto.Int6;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      if (fightStatus == FightStatus.local)
      {
        FightUIRework instance = FightUIRework.instance;
        if ((Object) null != (Object) instance)
        {
          PlayerStatus entityStatus;
          if (GameStatus.GetFightStatus(this.fromFightId).TryGetEntity<PlayerStatus>(this.fromPlayerId, out entityStatus))
          {
            CompanionDefinition companionDefinition;
            if (RuntimeData.companionDefinitions.TryGetValue(this.companionDefId, out companionDefinition))
            {
              if (this.concernedEntity == fightStatus.GetLocalPlayer().id)
              {
                FightInfoMessage message = FightInfoMessage.ReceivedCompanion(MessageInfoRibbonGroup.MyID);
                instance.DrawInfoMessage(message, entityStatus.nickname, RuntimeData.FormattedText(companionDefinition.i18nNameId, (IValueProvider) null));
              }
            }
            else
              Log.Error(FightEventErrors.DefinitionNotFound<CompanionDefinition>(this.companionDefId), 37, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionReceivedEvent.cs");
          }
          else
          {
            Log.Error(FightEventErrors.PlayerNotFound(this.fromPlayerId, this.fromFightId), 42, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\CompanionReceivedEvent.cs");
            yield break;
          }
        }
      }
    }
  }
}
