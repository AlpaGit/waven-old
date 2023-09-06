// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.ReserveCompanionStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;

namespace Ankama.Cube.UI.Fight
{
  public class ReserveCompanionStatus : ICastableStatus
  {
    public readonly CompanionDefinition definition;
    public readonly PlayerStatus ownerPlayer;
    private PlayerStatus m_currentPlayer;
    private CompanionReserveState m_state;

    public PlayerStatus currentPlayer => this.m_currentPlayer;

    public CompanionReserveState state => this.m_state;

    public int level { get; }

    public bool isGiven => this.ownerPlayer.id != this.m_currentPlayer.id || this.ownerPlayer.fightId != this.m_currentPlayer.fightId;

    public ReserveCompanionStatus(
      PlayerStatus ownerPlayer,
      CompanionDefinition definition,
      int level)
    {
      this.ownerPlayer = ownerPlayer;
      this.definition = definition;
      this.level = level;
      this.m_currentPlayer = ownerPlayer;
    }

    public void SetCurrentPlayer(PlayerStatus value) => this.m_currentPlayer = value;

    public void SetState(CompanionReserveState value) => this.m_state = value;

    public ReserveCompanionValueContext CreateValueContext() => new ReserveCompanionValueContext(GameStatus.GetFightStatus(this.m_currentPlayer.fightId), this.m_currentPlayer.id, this.level);

    public ICastableDefinition GetDefinition() => (ICastableDefinition) this.definition;
  }
}
