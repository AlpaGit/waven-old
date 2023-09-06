// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TeamAddedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Events
{
  public class TeamAddedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int teamIndex { get; private set; }

    public bool isLocalTeam { get; private set; }

    public TeamAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int teamIndex,
      bool isLocalTeam)
      : base(FightEventData.Types.EventType.TeamAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.teamIndex = teamIndex;
      this.isLocalTeam = isLocalTeam;
    }

    public TeamAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.TeamAdded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.teamIndex = proto.Int2;
      this.isLocalTeam = proto.Bool1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      if (!this.isLocalTeam)
        return;
      GameStatus.localPlayerTeamIndex = this.teamIndex;
    }
  }
}
