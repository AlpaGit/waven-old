// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CompanionDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Utility;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class CompanionDefinition : 
    CharacterDefinition,
    ICastableDefinition,
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData
  {
    private List<EventCategory> m_eventsInvalidatingCost;
    private List<EventCategory> m_eventsInvalidatingCasting;
    [LocalizedString("COMPANION_{id}_NAME", "Companions", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("COMPANION_{id}_DESCRIPTION", "Companions", 3)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private ICoordSelector m_spawnLocation;
    private List<Cost> m_cost;
    private List<SpellOnSpawnWithDestination> m_spells;
    private bool m_autoResurrect;
    [SerializeField]
    private AssetReference m_illustrationReference;

    public IReadOnlyList<EventCategory> eventsInvalidatingCost => (IReadOnlyList<EventCategory>) this.m_eventsInvalidatingCost;

    public IReadOnlyList<EventCategory> eventsInvalidatingCasting => (IReadOnlyList<EventCategory>) this.m_eventsInvalidatingCasting;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public ICoordSelector spawnLocation => this.m_spawnLocation;

    public IReadOnlyList<Cost> cost => (IReadOnlyList<Cost>) this.m_cost;

    public IReadOnlyList<SpellOnSpawnWithDestination> spells => (IReadOnlyList<SpellOnSpawnWithDestination>) this.m_spells;

    public bool autoResurrect => this.m_autoResurrect;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_eventsInvalidatingCost = Serialization.JsonArrayAsList<EventCategory>(jsonObject, "eventsInvalidatingCost");
      this.m_eventsInvalidatingCasting = Serialization.JsonArrayAsList<EventCategory>(jsonObject, "eventsInvalidatingCasting");
      this.m_spawnLocation = ICoordSelectorUtils.FromJsonProperty(jsonObject, "spawnLocation");
      JArray jarray1 = Serialization.JsonArray(jsonObject, "cost");
      this.m_cost = new List<Cost>(jarray1 != null ? jarray1.Count : 0);
      if (jarray1 != null)
      {
        foreach (JToken token in jarray1)
          this.m_cost.Add(Cost.FromJsonToken(token));
      }
      JArray jarray2 = Serialization.JsonArray(jsonObject, "spells");
      this.m_spells = new List<SpellOnSpawnWithDestination>(jarray2 != null ? jarray2.Count : 0);
      if (jarray2 != null)
      {
        foreach (JToken token in jarray2)
          this.m_spells.Add(SpellOnSpawnWithDestination.FromJsonToken(token));
      }
      this.m_autoResurrect = Serialization.JsonTokenValue<bool>(jsonObject, "autoResurrect");
    }

    public AssetReference illustrationReference => this.m_illustrationReference;

    public string illustrationBundleName => AssetBundlesUtility.GetUICharacterResourcesBundleName(this);
  }
}
