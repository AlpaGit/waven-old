// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.PlayerStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.CostModifiers;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.Fight.Entities
{
  public class PlayerStatus : 
    EntityStatus,
    IEntityWithActionPoints,
    IEntity,
    IEntityWithOwner,
    IEntityWithTeam
  {
    public readonly int fightId;
    public readonly int index;
    public readonly PlayerType playerType;
    public readonly string nickname;
    private CastTargetContext m_currentCastTargetContext;
    private ICastTargetDefinition m_currentCastTargetDef;
    private readonly List<SpellCostModification> m_spellCostModifiers = new List<SpellCostModification>();
    private readonly Dictionary<int, SpellStatus> m_spells = new Dictionary<int, SpellStatus>(9);
    private readonly List<int> m_dirtySpells = new List<int>(7);
    private readonly List<ReserveCompanionStatus> m_availableCompanions = new List<ReserveCompanionStatus>(4);
    private readonly List<ReserveCompanionStatus> m_additionalCompanions = new List<ReserveCompanionStatus>(4);

    public int teamId { get; }

    public int teamIndex { get; }

    public int ownerId { get; }

    public int actionPoints => this.GetCarac(CaracId.ActionPoints, 0);

    public int reservePoints => this.GetCarac(CaracId.ReservePoints, 0);

    public HeroStatus heroStatus { get; set; }

    public AbstractPlayerUIRework view { get; set; }

    public override EntityType type => EntityType.Player;

    public bool isLocalPlayer => this.playerType == PlayerType.Player;

    public List<SpellCostModification> spellCostModifiers => this.m_spellCostModifiers;

    public PlayerStatus(
      int id,
      int fightId,
      int index,
      int teamId,
      int teamIndex,
      string nickname,
      PlayerType playerType)
      : base(id)
    {
      this.ownerId = id;
      this.fightId = fightId;
      this.index = index;
      this.teamId = teamId;
      this.teamIndex = teamIndex;
      this.nickname = nickname;
      this.playerType = playerType;
    }

    public int GetSpellInHandCount()
    {
      int spellInHandCount = 0;
      foreach (SpellStatus spellStatus in this.m_spells.Values)
      {
        if (spellStatus.location == Ankama.Cube.Data.SpellMovementZone.Hand)
          ++spellInHandCount;
      }
      return spellInHandCount;
    }

    public IEnumerable<SpellStatus> EnumerateSpellStatus()
    {
      foreach (SpellStatus spellStatus in this.m_spells.Values)
      {
        if (spellStatus.location == Ankama.Cube.Data.SpellMovementZone.Hand)
          yield return spellStatus;
      }
    }

    public void AddSpell(SpellStatus spellStatus)
    {
      this.m_spells[spellStatus.instanceId] = spellStatus;
      spellStatus.location = Ankama.Cube.Data.SpellMovementZone.Hand;
    }

    public bool TryGetSpell(int spellInstanceId, out SpellStatus spellStatus) => this.m_spells.TryGetValue(spellInstanceId, out spellStatus);

    public void DiscardSpell(int spellInstanceId)
    {
      SpellStatus spellStatus;
      if (!this.m_spells.TryGetValue(spellInstanceId, out spellStatus))
        return;
      spellStatus.location = Ankama.Cube.Data.SpellMovementZone.Deck;
    }

    public void RemoveSpell(int spellInstanceId)
    {
      SpellStatus spellStatus;
      if (this.m_spells.TryGetValue(spellInstanceId, out spellStatus))
      {
        spellStatus.location = Ankama.Cube.Data.SpellMovementZone.Nowhere;
        this.m_dirtySpells.Add(spellInstanceId);
        FightLogicExecutor.NotifySpellRemovedForPlayer(this.fightId, this);
      }
      else
        Log.Error(string.Format("Tried to remove spell {0} but it could not be found for player with id {1}.", (object) spellInstanceId, (object) this.id), 153, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Entities\\Status\\PlayerStatus.cs");
    }

    public IEnumerator CleanupDirtySpells(int counter)
    {
      for (int index = 0; index < counter; ++index)
        this.m_spells.Remove(this.m_dirtySpells[index]);
      this.m_dirtySpells.RemoveRange(0, counter);
      yield break;
    }

    public bool reachedMaxNumberOfAdditionalCompanions => this.m_additionalCompanions.Count >= 4;

    public IEnumerable<ReserveCompanionStatus> GetAvailableCompanionStatusEnumerator() => (IEnumerable<ReserveCompanionStatus>) this.m_availableCompanions;

    public IEnumerable<ReserveCompanionStatus> GetAdditionalCompanionStatusEnumerator() => (IEnumerable<ReserveCompanionStatus>) this.m_additionalCompanions;

    public void SetAvailableCompanions(IReadOnlyList<int> definitionIds, IReadOnlyList<int> levels)
    {
      int count = ((IReadOnlyCollection<int>) definitionIds).Count;
      for (int index = 0; index < count; ++index)
      {
        int definitionId = definitionIds[index];
        int level = levels[index];
        CompanionDefinition definition;
        if (!RuntimeData.companionDefinitions.TryGetValue(definitionId, out definition))
          Log.Error(string.Format("Could not find {0} with id {1}.", (object) "CompanionDefinition", (object) definitionId), 216, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Entities\\Status\\PlayerStatus.cs");
        else
          this.m_availableCompanions.Add(new ReserveCompanionStatus(this, definition, level));
      }
    }

    public bool HasCompanion(int definitionId)
    {
      List<ReserveCompanionStatus> availableCompanions = this.m_availableCompanions;
      int count1 = availableCompanions.Count;
      for (int index = 0; index < count1; ++index)
      {
        if (availableCompanions[index].definition.id == definitionId)
          return true;
      }
      List<ReserveCompanionStatus> additionalCompanions = this.m_additionalCompanions;
      int count2 = additionalCompanions.Count;
      for (int index = 0; index < count2; ++index)
      {
        if (additionalCompanions[index].definition.id == definitionId)
          return true;
      }
      return false;
    }

    public bool TryGetCompanion(int definitionId, out ReserveCompanionStatus companionStatus)
    {
      List<ReserveCompanionStatus> availableCompanions = this.m_availableCompanions;
      int count1 = availableCompanions.Count;
      for (int index = 0; index < count1; ++index)
      {
        ReserveCompanionStatus reserveCompanionStatus = availableCompanions[index];
        if (reserveCompanionStatus.definition.id == definitionId)
        {
          companionStatus = reserveCompanionStatus;
          return true;
        }
      }
      List<ReserveCompanionStatus> additionalCompanions = this.m_additionalCompanions;
      int count2 = additionalCompanions.Count;
      for (int index = 0; index < count2; ++index)
      {
        ReserveCompanionStatus reserveCompanionStatus = additionalCompanions[index];
        if (reserveCompanionStatus.definition.id == definitionId)
        {
          companionStatus = reserveCompanionStatus;
          return true;
        }
      }
      companionStatus = (ReserveCompanionStatus) null;
      return false;
    }

    public void AddAdditionalCompanion([NotNull] ReserveCompanionStatus companion)
    {
      companion.SetCurrentPlayer(this);
      this.m_additionalCompanions.Add(companion);
    }

    public void SetAdditionalCompanionState(int companionDefinitionId, CompanionReserveState state)
    {
      int count = this.m_additionalCompanions.Count;
      for (int index = 0; index < count; ++index)
      {
        ReserveCompanionStatus additionalCompanion = this.m_additionalCompanions[index];
        if (additionalCompanion.definition.id == companionDefinitionId)
        {
          additionalCompanion.SetState(state);
          return;
        }
      }
      Log.Error(string.Format("Could not change state of an additional companion from player with id {0} because it was not in its secondary reserve.", (object) this.id), 304, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Entities\\Status\\PlayerStatus.cs");
    }

    public void RemoveAdditionalCompanion(int companionDefinitionId)
    {
      int count = this.m_additionalCompanions.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.m_additionalCompanions[index].definition.id == companionDefinitionId)
        {
          this.m_additionalCompanions.RemoveAt(index);
          return;
        }
      }
      Log.Error(string.Format("Tried to remove a companion from player with id {0} but it was not in its secondary reserve.", (object) this.id), 320, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Entities\\Status\\PlayerStatus.cs");
    }

    public void AddSpellCostModifier(SpellCostModification spellCostModifier) => this.m_spellCostModifiers.Add(spellCostModifier);

    public void RemoveSpellCostModifier(int spellCostModifierId)
    {
      for (int index = 0; index < this.m_spellCostModifiers.Count; ++index)
      {
        if (this.m_spellCostModifiers[index].id == spellCostModifierId)
        {
          this.m_spellCostModifiers.RemoveAt(index);
          break;
        }
      }
    }
  }
}
