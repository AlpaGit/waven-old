// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Fight;
using Ankama.Utilities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SpellDefinition : 
    EditableData,
    ICastableDefinition,
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData,
    ISpellEffectOverrideProvider
  {
    private List<EventCategory> m_eventsInvalidatingCost;
    private List<EventCategory> m_eventsInvalidatingCasting;
    private PrecomputedData m_precomputedData;
    private SpellType m_spellType;
    private God m_god;
    private Element m_element;
    private List<SpellTag> m_tags;
    [LocalizedString("SPELL_{id}_NAME", "Spells", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("SPELL_{id}_DESCRIPTION", "Spells", 3)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private List<GaugeValue> m_gaugeToModifyOnSpellPlay;
    private List<Cost> m_costs;
    private ICastTargetDefinition m_castTarget;
    private List<SpellEffectInstantiationData> m_spellEffectData;
    [SerializeField]
    private AssetReference m_illustrationReference;
    [SerializeField]
    private SpellEffectReferenceDictionary m_spellEffectOverrideReferences = new SpellEffectReferenceDictionary();
    [NonSerialized]
    private SpellDefinition.ResourceLoadingState m_resourceLoadingState;
    [NonSerialized]
    private SpellEffect[] m_spellEffects;
    [NonSerialized]
    private Dictionary<SpellEffectKey, SpellEffect> m_spellEffectOverrides;
    [SerializeField]
    private ElementaryStates m_elementaryStates = ElementaryStates.None;

    public IReadOnlyList<EventCategory> eventsInvalidatingCost => (IReadOnlyList<EventCategory>) this.m_eventsInvalidatingCost;

    public IReadOnlyList<EventCategory> eventsInvalidatingCasting => (IReadOnlyList<EventCategory>) this.m_eventsInvalidatingCasting;

    public PrecomputedData precomputedData => this.m_precomputedData;

    public SpellType spellType => this.m_spellType;

    public God god => this.m_god;

    public Element element => this.m_element;

    public IReadOnlyList<SpellTag> tags => (IReadOnlyList<SpellTag>) this.m_tags;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public IReadOnlyList<GaugeValue> gaugeToModifyOnSpellPlay => (IReadOnlyList<GaugeValue>) this.m_gaugeToModifyOnSpellPlay;

    public IReadOnlyList<Cost> costs => (IReadOnlyList<Cost>) this.m_costs;

    public ICastTargetDefinition castTarget => this.m_castTarget;

    public IReadOnlyList<SpellEffectInstantiationData> spellEffectData => (IReadOnlyList<SpellEffectInstantiationData>) this.m_spellEffectData;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_eventsInvalidatingCost = Serialization.JsonArrayAsList<EventCategory>(jsonObject, "eventsInvalidatingCost");
      this.m_eventsInvalidatingCasting = Serialization.JsonArrayAsList<EventCategory>(jsonObject, "eventsInvalidatingCasting");
      this.m_precomputedData = PrecomputedData.FromJsonProperty(jsonObject, "precomputedData");
      this.m_spellType = (SpellType) Serialization.JsonTokenValue<int>(jsonObject, "spellType", 1);
      this.m_god = (God) Serialization.JsonTokenValue<int>(jsonObject, "god");
      this.m_element = (Element) Serialization.JsonTokenValue<int>(jsonObject, "element");
      this.m_tags = Serialization.JsonArrayAsList<SpellTag>(jsonObject, "tags");
      JArray jarray1 = Serialization.JsonArray(jsonObject, "gaugeToModifyOnSpellPlay");
      this.m_gaugeToModifyOnSpellPlay = new List<GaugeValue>(jarray1 != null ? jarray1.Count : 0);
      if (jarray1 != null)
      {
        foreach (JToken token in jarray1)
          this.m_gaugeToModifyOnSpellPlay.Add(GaugeValue.FromJsonToken(token));
      }
      JArray jarray2 = Serialization.JsonArray(jsonObject, "costs");
      this.m_costs = new List<Cost>(jarray2 != null ? jarray2.Count : 0);
      if (jarray2 != null)
      {
        foreach (JToken token in jarray2)
          this.m_costs.Add(Cost.FromJsonToken(token));
      }
      this.m_castTarget = ICastTargetDefinitionUtils.FromJsonProperty(jsonObject, "castTarget");
      JArray jarray3 = Serialization.JsonArray(jsonObject, "spellEffectData");
      this.m_spellEffectData = new List<SpellEffectInstantiationData>(jarray3 != null ? jarray3.Count : 0);
      if (jarray3 == null)
        return;
      foreach (JToken token in jarray3)
        this.m_spellEffectData.Add(SpellEffectInstantiationData.FromJsonToken(token));
    }

    public AssetReference illustrationReference => this.m_illustrationReference;

    public string illustrationBundleName => "core/ui/spells";

    public ElementaryStates elementaryStates => this.m_elementaryStates;

    public SpellEffect GetSpellEffect(int index) => index >= this.m_spellEffects.Length ? (SpellEffect) null : this.m_spellEffects[index];

    public bool TryGetSpellEffectOverride(SpellEffectKey key, out SpellEffect spellEffect)
    {
      if (this.m_spellEffectOverrides != null)
        return this.m_spellEffectOverrides.TryGetValue(key, out spellEffect);
      spellEffect = (SpellEffect) null;
      return false;
    }

    public IEnumerator LoadResources()
    {
      SpellDefinition spellDefinition = this;
      switch (spellDefinition.m_resourceLoadingState)
      {
        case SpellDefinition.ResourceLoadingState.None:
          if (!FightSpellEffectFactory.isReady)
            break;
          spellDefinition.m_resourceLoadingState = SpellDefinition.ResourceLoadingState.Loading;
          int spellEffectReferenceCount = spellDefinition.m_spellEffectData.Count;
          int length = spellEffectReferenceCount + spellDefinition.m_spellEffectOverrideReferences.Count;
          AssetLoadRequest<SpellEffect>[] loadRequests = new AssetLoadRequest<SpellEffect>[length];
          IEnumerator[] spellEffectLoadRequests = new IEnumerator[length];
          int index1;
          for (index1 = 0; index1 < spellEffectReferenceCount; ++index1)
          {
            string spellEffect = spellDefinition.m_spellEffectData[index1].spellEffect;
            if (spellEffect.Length > 0)
            {
              loadRequests[index1] = AssetManager.LoadAssetAsync<SpellEffect>(spellEffect, "core/spells/effects");
            }
            else
            {
              Log.Warning(string.Format("Spell named '{0}' has an unassigned spell effect at index {1}.", (object) spellDefinition.displayName, (object) index1), 118, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Spells\\SpellDefinition.cs");
              loadRequests[index1] = (AssetLoadRequest<SpellEffect>) null;
            }
          }
          foreach (AssetReference assetReference in spellDefinition.m_spellEffectOverrideReferences.Values)
          {
            loadRequests[index1] = !assetReference.hasValue ? (AssetLoadRequest<SpellEffect>) null : assetReference.LoadFromAssetBundleAsync<SpellEffect>("core/spells/effects");
            ++index1;
          }
          yield return (object) EnumeratorUtility.ParallelRecursiveSafeExecution((IEnumerator[]) loadRequests);
          SpellEffect[] spellEffects = new SpellEffect[spellEffectReferenceCount];
          int index2;
          for (index2 = 0; index2 < spellEffectReferenceCount; ++index2)
          {
            AssetLoadRequest<SpellEffect> assetLoadRequest = loadRequests[index2];
            if (assetLoadRequest == null)
              spellEffects[index2] = (SpellEffect) null;
            else if ((int) assetLoadRequest.error == 0)
            {
              SpellEffect asset = assetLoadRequest.asset;
              spellEffects[index2] = asset;
              spellEffectLoadRequests[index2] = asset.Load();
            }
            else
            {
              spellEffects[index2] = (SpellEffect) null;
              Log.Error(string.Format("Could not load spell effect for spell {0}: {1}", (object) spellDefinition.name, (object) assetLoadRequest.error), 159, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Spells\\SpellDefinition.cs");
            }
          }
          Dictionary<SpellEffectKey, SpellEffect> spellEffectOverrides = new Dictionary<SpellEffectKey, SpellEffect>(spellDefinition.m_spellEffectOverrideReferences.Count, (IEqualityComparer<SpellEffectKey>) SpellEffectKeyComparer.instance);
          foreach (SpellEffectKey key in spellDefinition.m_spellEffectOverrideReferences.Keys)
          {
            AssetLoadRequest<SpellEffect> assetLoadRequest = loadRequests[index2];
            if (assetLoadRequest == null)
              spellEffectOverrides.Add(key, (SpellEffect) null);
            else if ((int) assetLoadRequest.error == 0)
            {
              SpellEffect asset = assetLoadRequest.asset;
              spellEffectOverrides.Add(key, asset);
              spellEffectLoadRequests[index2] = asset.Load();
            }
            else
            {
              spellEffectOverrides.Add(key, (SpellEffect) null);
              Log.Error(string.Format("Could not load spell effect override for key {0} for spell {1}: {2}", (object) key, (object) spellDefinition.name, (object) assetLoadRequest.error), 183, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Spells\\SpellDefinition.cs");
            }
            ++index2;
          }
          yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(spellEffectLoadRequests);
          spellDefinition.m_spellEffects = spellEffects;
          spellDefinition.m_spellEffectOverrides = spellEffectOverrides;
          spellDefinition.m_resourceLoadingState = SpellDefinition.ResourceLoadingState.Loaded;
          FightSpellEffectFactory.NotifySpellDefinitionLoaded(spellDefinition);
          break;
        case SpellDefinition.ResourceLoadingState.Loading:
          do
          {
            yield return (object) null;
          }
          while (spellDefinition.m_resourceLoadingState == SpellDefinition.ResourceLoadingState.Loading);
          break;
        case SpellDefinition.ResourceLoadingState.Loaded:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void UnloadResources()
    {
      if (this.m_resourceLoadingState == SpellDefinition.ResourceLoadingState.None)
        return;
      if (this.m_spellEffects != null)
      {
        int length = this.m_spellEffects.Length;
        for (int index = 0; index < length; ++index)
        {
          SpellEffect spellEffect = this.m_spellEffects[index];
          if ((UnityEngine.Object) null != (UnityEngine.Object) spellEffect)
            spellEffect.Unload();
        }
        this.m_spellEffects = (SpellEffect[]) null;
      }
      if (this.m_spellEffectOverrides != null)
      {
        foreach (SpellEffect spellEffect in this.m_spellEffectOverrides.Values)
        {
          if ((UnityEngine.Object) null != (UnityEngine.Object) spellEffect)
            spellEffect.Unload();
        }
        this.m_spellEffectOverrides.Clear();
        this.m_spellEffectOverrides = (Dictionary<SpellEffectKey, SpellEffect>) null;
      }
      this.m_resourceLoadingState = SpellDefinition.ResourceLoadingState.None;
    }

    public int? GetBaseCost(int level) => this.GetCost((DynamicValueContext) new SpellDefinitionContext(this, level));

    public int? GetCost(DynamicValueContext context)
    {
      int? cost1 = new int?();
      foreach (Cost cost2 in this.m_costs)
      {
        if (cost2 is ActionPointsCost actionPointsCost)
        {
          if (!cost1.HasValue)
            cost1 = new int?(0);
          int num1;
          actionPointsCost.value.GetValue(context, out num1);
          int? nullable = cost1;
          int num2 = num1;
          cost1 = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + num2) : new int?();
        }
      }
      return cost1;
    }

    private enum ResourceLoadingState
    {
      None,
      Loading,
      Loaded,
    }
  }
}
