// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.PlayerAddedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.UI.Fight;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class PlayerAddedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public string name { get; private set; }

    public int teamId { get; private set; }

    public bool isLocalPlayer { get; private set; }

    public int baseActionPoints { get; private set; }

    public int index { get; private set; }

    public int teamIndex { get; private set; }

    public PlayerAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      string name,
      int teamId,
      bool isLocalPlayer,
      int baseActionPoints,
      int index,
      int teamIndex)
      : base(FightEventData.Types.EventType.PlayerAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.name = name;
      this.teamId = teamId;
      this.isLocalPlayer = isLocalPlayer;
      this.baseActionPoints = baseActionPoints;
      this.index = index;
      this.teamIndex = teamIndex;
    }

    public PlayerAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.PlayerAdded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.teamId = proto.Int2;
      this.baseActionPoints = proto.Int3;
      this.index = proto.Int4;
      this.teamIndex = proto.Int5;
      this.name = proto.String1;
      this.isLocalPlayer = proto.Bool1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerType playerType = this.isLocalPlayer ? PlayerType.Player : (PlayerType) ((this.teamIndex == GameStatus.localPlayerTeamIndex ? 1 : 2) | (fightStatus == FightStatus.local ? 4 : 0));
      PlayerStatus playerStatus = new PlayerStatus(this.concernedEntity, fightStatus.fightId, this.index, this.teamId, this.teamIndex, this.name, playerType);
      fightStatus.AddEntity((EntityStatus) playerStatus);
      playerStatus.SetCarac(CaracId.ActionPoints, this.baseActionPoints);
      if (this.isLocalPlayer)
      {
        fightStatus.localPlayerId = this.concernedEntity;
        CameraHandler current = CameraHandler.current;
        if ((Object) null != (Object) current)
        {
          DirectionAngle mapRotation = GameStatus.GetMapRotation(playerStatus);
          current.ChangeRotation(mapRotation);
        }
      }
      FightUIRework instance = FightUIRework.instance;
      if (!((Object) null != (Object) instance))
        return;
      AbstractPlayerUIRework abstractPlayerUiRework = !this.isLocalPlayer ? (AbstractPlayerUIRework) instance.AddPlayer(playerStatus) : (AbstractPlayerUIRework) instance.GetLocalPlayerUI(playerStatus);
      playerStatus.view = abstractPlayerUiRework;
      abstractPlayerUiRework.SetPlayerStatus(playerStatus);
      abstractPlayerUiRework.SetPlayerName(playerStatus.nickname);
      abstractPlayerUiRework.SetRankIcon(0);
      abstractPlayerUiRework.SetActionPoints(this.baseActionPoints);
      abstractPlayerUiRework.SetReservePoints(0);
      abstractPlayerUiRework.SetElementaryPoints(0, 0, 0, 0);
    }
  }
}
