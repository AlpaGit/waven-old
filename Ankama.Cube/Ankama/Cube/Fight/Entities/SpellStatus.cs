// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.SpellStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Utilities;
using JetBrains.Annotations;

namespace Ankama.Cube.Fight.Entities
{
  public class SpellStatus : ICastableStatus
  {
    public readonly PlayerStatus ownerPlayer;
    public readonly int instanceId;
    public Ankama.Cube.Data.SpellMovementZone location;

    public SpellDefinition definition { get; private set; }

    public int? baseCost { get; private set; }

    public int level { get; private set; }

    [CanBeNull]
    public static SpellStatus TryCreate(SpellInfo spellInfo, PlayerStatus ownerStatus)
    {
      int? spellDefinitionId = spellInfo.SpellDefinitionId;
      if (!spellDefinitionId.HasValue)
        return new SpellStatus(ownerStatus, spellInfo.SpellInstanceId);
      int key = spellDefinitionId.Value;
      SpellDefinition definition;
      if (!RuntimeData.spellDefinitions.TryGetValue(key, out definition))
      {
        Log.Error(string.Format("Could not find spell definition with id {0}.", (object) key), 37, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Entities\\Status\\SpellStatus.cs");
        return (SpellStatus) null;
      }
      int? spellLevel = spellInfo.SpellLevel;
      int level = spellLevel.HasValue ? spellLevel.Value : 0;
      return new SpellStatus(definition, level, ownerStatus, spellInfo.SpellInstanceId);
    }

    public SpellStatus(PlayerStatus ownerPlayer, int instanceId)
    {
      this.ownerPlayer = ownerPlayer;
      this.instanceId = instanceId;
      this.definition = (SpellDefinition) null;
      this.level = 0;
      this.baseCost = new int?();
    }

    public SpellStatus(
      [NotNull] SpellDefinition definition,
      int level,
      PlayerStatus ownerPlayer,
      int instanceId)
    {
      this.ownerPlayer = ownerPlayer;
      this.instanceId = instanceId;
      this.definition = definition;
      this.level = level;
      this.baseCost = definition.GetBaseCost(level);
    }

    public void Upgrade(SpellDefinition spellDefinition, int spellLevel)
    {
      this.definition = spellDefinition;
      this.level = spellLevel;
      this.baseCost = this.definition.GetBaseCost(this.level);
    }

    public ICastableDefinition GetDefinition() => (ICastableDefinition) this.definition;

    public CastTargetContext CreateCastTargetContext() => this.definition.castTarget.CreateCastTargetContext(FightStatus.local, this.ownerPlayer.id, DynamicValueHolderType.Spell, this.definition.id, this.level, this.instanceId);

    public override string ToString() => string.Format("{0}: {1}, ", (object) "instanceId", (object) this.instanceId) + "ownerPlayer: " + this.ownerPlayer.nickname + ", " + string.Format("{0}: {1}, ", (object) "definition", (object) this.definition.id) + string.Format("{0}: {1}, ", (object) "level", (object) this.level);

    public enum Location
    {
      None,
      Hand,
      Deck,
    }
  }
}
