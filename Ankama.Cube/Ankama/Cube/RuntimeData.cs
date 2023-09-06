// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.RuntimeData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Data;
using Ankama.Cube.Data.UI;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Utilities;
using DataEditor;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Ankama.Cube
{
  public static class RuntimeData
  {
    public static readonly Dictionary<int, FightDefinition> fightDefinitions = new Dictionary<int, FightDefinition>();
    public static readonly Dictionary<int, WeaponDefinition> weaponDefinitions = new Dictionary<int, WeaponDefinition>();
    public static readonly Dictionary<int, CompanionDefinition> companionDefinitions = new Dictionary<int, CompanionDefinition>();
    public static readonly Dictionary<int, SummoningDefinition> summoningDefinitions = new Dictionary<int, SummoningDefinition>();
    public static readonly Dictionary<int, FloorMechanismDefinition> floorMechanismDefinitions = new Dictionary<int, FloorMechanismDefinition>();
    public static readonly Dictionary<int, ObjectMechanismDefinition> objectMechanismDefinitions = new Dictionary<int, ObjectMechanismDefinition>();
    public static readonly Dictionary<int, CharacterSkinDefinition> characterSkinDefinitions = new Dictionary<int, CharacterSkinDefinition>();
    public static readonly Dictionary<int, SpellDefinition> spellDefinitions = new Dictionary<int, SpellDefinition>();
    public static readonly Dictionary<int, SquadDefinition> squadDefinitions = new Dictionary<int, SquadDefinition>();
    public static readonly Dictionary<God, ReserveDefinition> reserveDefinitions = new Dictionary<God, ReserveDefinition>((IEqualityComparer<God>) GodComparer.instance);
    public static readonly Dictionary<God, GodDefinition> godDefinitions = new Dictionary<God, GodDefinition>((IEqualityComparer<God>) GodComparer.instance);
    public static DataAvailabilityDefinition availabilityDefinition;
    public static ConstantsDefinition constantsDefinition;
    private static bool s_setupCultureCodeVariant;
    private static bool s_loadedTextCollectionsBundle;
    private static LocalizedTextData s_localizedTextData;
    private static readonly Dictionary<string, int> s_textCollectionsRefCount = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<int, string> s_textCollectionsData = new Dictionary<int, string>();
    private static readonly Dictionary<string, FontCollection> s_fontCollections = new Dictionary<string, FontCollection>((IEqualityComparer<string>) StringComparer.Ordinal);
    private static readonly Dictionary<string, FontCollection> s_fontCollectionsByName = new Dictionary<string, FontCollection>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static bool s_loadedDataBundleName;
    private static readonly List<RuntimeData.DeserializationTask> s_deserializationTasks = new List<RuntimeData.DeserializationTask>(16);
    public static readonly TextFormatter textFormatter = new TextFormatter(new IParserRule[4]
    {
      (IParserRule) new LineBreakParserRule(),
      (IParserRule) new ParserRulesGroupStartWith("\\", new IParserRule[3]
      {
        (IParserRule) new EscapedBraceParserRule(),
        (IParserRule) new VertivalTabParserRule(),
        (IParserRule) new NonBreakableSpaceParserRule()
      }),
      (IParserRule) new ConditionalParserRule(),
      (IParserRule) new BoundedParserRule('{', '}', new IParserRule[6]
      {
        (IParserRule) new KeywordParserRule(),
        (IParserRule) new WordSubstitutionParserRule(),
        (IParserRule) new ParserRulesGroupByFirstLetter(new IParserRuleWithPrefix[19]
        {
          (IParserRuleWithPrefix) new RangeParserRule(),
          (IParserRuleWithPrefix) new DynamicValueParserRule(),
          (IParserRuleWithPrefix) new ObjectReferenceParserRule("spell", ObjectReference.Type.Spell),
          (IParserRuleWithPrefix) new ObjectReferenceParserRule("companion", ObjectReference.Type.Companion),
          (IParserRuleWithPrefix) new ObjectReferenceParserRule("summoning", ObjectReference.Type.Summoning),
          (IParserRuleWithPrefix) new ObjectReferenceParserRule("floorMechanism", ObjectReference.Type.FloorMechanism),
          (IParserRuleWithPrefix) new ObjectReferenceParserRule("objectMechanism", ObjectReference.Type.ObjectMechanism),
          (IParserRuleWithPrefix) new ObjectReferenceParserRule("weapon", ObjectReference.Type.Weapon),
          (IParserRuleWithPrefix) new EffectValueParserRule("damage")
          {
            getModificationValue = (Func<IFightValueProvider, int>) (valueProvider => valueProvider.GetDamageModifierValue())
          },
          (IParserRuleWithPrefix) new EffectValueParserRule("explosion"),
          (IParserRuleWithPrefix) new EffectValueParserRule("herd"),
          (IParserRuleWithPrefix) new EffectValueParserRule("clan"),
          (IParserRuleWithPrefix) new EffectValueParserRule("heal")
          {
            getModificationValue = (Func<IFightValueProvider, int>) (valueProvider => valueProvider.GetHealModifierValue())
          },
          (IParserRuleWithPrefix) new EffectValueParserRule("blindage"),
          (IParserRuleWithPrefix) new EffectValueParserRule("cell"),
          (IParserRuleWithPrefix) new EffectValueParserRule("armor"),
          (IParserRuleWithPrefix) new EffectValueParserRule("maxLifeGain"),
          (IParserRuleWithPrefix) new EffectValueParserRule("frozen"),
          (IParserRuleWithPrefix) new ParserRulesGroupStartWith("add", new IParserRule[5]
          {
            (IParserRule) new EffectValueParserRule("addEarth"),
            (IParserRule) new EffectValueParserRule("addFire"),
            (IParserRule) new EffectValueParserRule("addAir"),
            (IParserRule) new EffectValueParserRule("addWater"),
            (IParserRule) new EffectValueParserRule("addReserve")
          })
        }),
        (IParserRule) new ValueParserRule(),
        (IParserRule) new PluralParserRule(),
        (IParserRule) new SelectParserRule()
      })
    });
    private const int DeserializationBatchSize = 128;

    public static event RuntimeData.CultureCodeChangedEventHandler CultureCodeChanged;

    public static CultureCode currentCultureCode { get; private set; } = CultureCode.Default;

    public static FontLanguage currentFontLanguage { get; private set; } = FontLanguage.Latin;

    public static bool isReady { get; private set; }

    public static AssetManagerError error { get; private set; } = (AssetManagerError) 0;

    public static KeywordContext currentKeywordContext { get; set; } = KeywordContext.FightSolo;

    public static bool InitializeLanguage(CultureCode cultureCode)
    {
      int fontLanguage = (int) cultureCode.GetFontLanguage();
      RuntimeData.currentCultureCode = cultureCode;
      RuntimeData.currentFontLanguage = (FontLanguage) fontLanguage;
      RuntimeData.textFormatter.pluralRules = cultureCode.GetPluralRules();
      RuntimeData.s_localizedTextData = Resources.Load<LocalizedTextData>("Localization/LocalizedTextData");
      if ((UnityEngine.Object) null == (UnityEngine.Object) RuntimeData.s_localizedTextData)
      {
        Log.Error("Failed to load localized text data from resources at path 'Localization/LocalizedTextData'.", 161, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      RuntimeData.s_localizedTextData.Initialize();
      string path = string.Format("Localization/{0}/Boot", (object) RuntimeData.currentCultureCode);
      TextCollection textCollection = Resources.Load<TextCollection>(path);
      if ((UnityEngine.Object) null == (UnityEngine.Object) textCollection)
      {
        Log.Error("Failed to load boot text collection from resources at path '" + path + "'.", 173, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      RuntimeData.LoadTextCollectionData(textCollection);
      return true;
    }

    public static bool InitializeFonts()
    {
      FontCollection[] fontCollectionArray = Resources.LoadAll<FontCollection>("GameData/UI/Fonts");
      if (fontCollectionArray.Length == 0)
      {
        Log.Error("Could not find font collections.", 191, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      foreach (FontCollection fontCollection in fontCollectionArray)
      {
        string identifier = fontCollection.identifier;
        string name = fontCollection.name;
        if (string.IsNullOrEmpty(identifier))
        {
          Log.Error("Font collection named '" + name + "' doesn't have an identifier.", 202, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          return false;
        }
        if (!RuntimeData.s_fontCollections.ContainsKey(identifier))
        {
          RuntimeData.s_fontCollections.Add(identifier, fontCollection);
          if (!RuntimeData.s_fontCollectionsByName.ContainsKey(name))
            RuntimeData.s_fontCollectionsByName.Add(name, fontCollection);
          else
            Log.Warning("Multiple font collections share the same name '" + name + "', subsequent assets will be ignored.", 222, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        }
        else
        {
          Log.Error("Multiple font collections share the same identifier '" + identifier + "'.", 212, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          return false;
        }
      }
      FontCollection fontCollection1;
      if (!RuntimeData.s_fontCollectionsByName.TryGetValue("Default", out fontCollection1))
      {
        Log.Error("Could not load default font collection", 230, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      fontCollection1.Load();
      return true;
    }

    public static IEnumerator Load()
    {
      if (RuntimeData.isReady)
      {
        Log.Warning("Load called while runtime data is already ready.", 248, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      }
      else
      {
        Log.Info("Loading application text collection...", 252, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        if (!AssetManager.AddActiveVariant(RuntimeData.currentCultureCode.ToString().ToLowerInvariant()))
        {
          RuntimeData.error = new AssetManagerError(10, string.Format("Could not setup variant for culture code {0}.", (object) RuntimeData.currentCultureCode));
          Log.Error(string.Format("Error while loading text collections bundle: {0}", (object) RuntimeData.error), 261, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        }
        else
        {
          RuntimeData.s_setupCultureCodeVariant = true;
          AssetBundleLoadRequest textCollectionsBundleLoadRequest = AssetManager.LoadAssetBundle("core/localization");
          while (!textCollectionsBundleLoadRequest.isDone)
            yield return (object) null;
          if ((int) textCollectionsBundleLoadRequest.error != 0)
          {
            RuntimeData.error = textCollectionsBundleLoadRequest.error;
            Log.Error(string.Format("Error while loading text collections bundle: {0}", (object) RuntimeData.error), 277, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          }
          else
          {
            RuntimeData.s_loadedTextCollectionsBundle = true;
            yield return (object) RuntimeData.LoadTextCollectionAsync("Application");
            Log.Info("Loading data assets...", 285, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
            AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle("core/data");
            while (!bundleLoadRequest.isDone)
              yield return (object) null;
            if ((int) bundleLoadRequest.error != 0)
            {
              Log.Error(string.Format("Error while loading data bundle: {0}", (object) RuntimeData.error), 297, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
              RuntimeData.error = bundleLoadRequest.error;
            }
            else
            {
              RuntimeData.s_loadedDataBundleName = true;
              DataAvailability minValidAvailability = DataAvailability.Wip;
              yield return (object) RuntimeData.LoadDataDefinition<DataAvailabilityDefinition>((Action<DataAvailabilityDefinition>) (asset => RuntimeData.availabilityDefinition = asset));
              if ((int) RuntimeData.error != 0)
              {
                Log.Error(string.Format("Error while loading {0}: {1}", (object) "DataAvailabilityDefinition", (object) RuntimeData.error), 311, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
              }
              else
              {
                yield return (object) RuntimeData.LoadDataDefinitions<FightDefinition>(RuntimeData.fightDefinitions);
                if ((int) RuntimeData.error != 0)
                {
                  Log.Error(string.Format("Error while loading {0}: {1}", (object) "fightDefinitions", (object) RuntimeData.error), 321, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                }
                else
                {
                  yield return (object) RuntimeData.LoadDataDefinitions<SummoningDefinition>(RuntimeData.summoningDefinitions);
                  if ((int) RuntimeData.error != 0)
                  {
                    Log.Error(string.Format("Error while loading {0}: {1}", (object) "summoningDefinitions", (object) RuntimeData.error), 331, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                  }
                  else
                  {
                    HashSet<int> validCompanionIds = new HashSet<int>();
                    foreach (CompanionAvailability companion in (IEnumerable<CompanionAvailability>) RuntimeData.availabilityDefinition.companions)
                    {
                      if (companion.availability >= minValidAvailability)
                        validCompanionIds.Add(companion.companion.value);
                    }
                    yield return (object) RuntimeData.LoadDataDefinitions<CompanionDefinition>(RuntimeData.companionDefinitions, new Predicate<int>(CompanionsPredicate));
                    if ((int) RuntimeData.error != 0)
                    {
                      Log.Error(string.Format("Error while loading {0}: {1}", (object) "companionDefinitions", (object) RuntimeData.error), 353, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                    }
                    else
                    {
                      yield return (object) RuntimeData.LoadDataDefinitions<WeaponDefinition>(RuntimeData.weaponDefinitions);
                      if ((int) RuntimeData.error != 0)
                      {
                        Log.Error(string.Format("Error while loading {0}: {1}", (object) "weaponDefinitions", (object) RuntimeData.error), 363, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                      }
                      else
                      {
                        yield return (object) RuntimeData.LoadDataDefinitions<FloorMechanismDefinition>(RuntimeData.floorMechanismDefinitions);
                        if ((int) RuntimeData.error != 0)
                        {
                          Log.Error(string.Format("Error while loading {0}: {1}", (object) "floorMechanismDefinitions", (object) RuntimeData.error), 373, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                        }
                        else
                        {
                          yield return (object) RuntimeData.LoadDataDefinitions<ObjectMechanismDefinition>(RuntimeData.objectMechanismDefinitions);
                          if ((int) RuntimeData.error != 0)
                          {
                            Log.Error(string.Format("Error while loading {0}: {1}", (object) "objectMechanismDefinitions", (object) RuntimeData.error), 383, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                          }
                          else
                          {
                            yield return (object) RuntimeData.LoadDataDefinitions<CharacterSkinDefinition>(RuntimeData.characterSkinDefinitions);
                            if ((int) RuntimeData.error != 0)
                            {
                              Log.Error(string.Format("Error while loading {0}: {1}", (object) "characterSkinDefinitions", (object) RuntimeData.error), 393, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                            }
                            else
                            {
                              Dictionary<int, ReserveDefinition> reserveDefinitionsById = new Dictionary<int, ReserveDefinition>();
                              yield return (object) RuntimeData.LoadDataDefinitions<ReserveDefinition>(reserveDefinitionsById);
                              if ((int) RuntimeData.error != 0)
                              {
                                Log.Error(string.Format("Error while loading {0}: {1}", (object) "reserveDefinitions", (object) RuntimeData.error), 404, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                              }
                              else
                              {
                                Dictionary<int, GodDefinition> godDefinitionsById = new Dictionary<int, GodDefinition>();
                                yield return (object) RuntimeData.LoadDataDefinitions<GodDefinition>(godDefinitionsById);
                                if ((int) RuntimeData.error != 0)
                                {
                                  Log.Error(string.Format("Error while loading {0}: {1}", (object) "godDefinitions", (object) RuntimeData.error), 415, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                }
                                else
                                {
                                  yield return (object) RuntimeData.LoadDataDefinitions<SpellDefinition>(RuntimeData.spellDefinitions);
                                  if ((int) RuntimeData.error != 0)
                                  {
                                    Log.Error(string.Format("Error while loading {0}: {1}", (object) "spellDefinitions", (object) RuntimeData.error), 425, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                  }
                                  else
                                  {
                                    yield return (object) RuntimeData.LoadDataDefinitions<SquadDefinition>(RuntimeData.squadDefinitions);
                                    if ((int) RuntimeData.error != 0)
                                    {
                                      Log.Error(string.Format("Error while loading {0}: {1}", (object) "squadDefinitions", (object) RuntimeData.error), 435, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                    }
                                    else
                                    {
                                      yield return (object) RuntimeData.LoadDataDefinition<ConstantsDefinition>((Action<ConstantsDefinition>) (asset => RuntimeData.constantsDefinition = asset));
                                      if ((int) RuntimeData.error != 0)
                                      {
                                        Log.Error(string.Format("Error while loading {0}: {1}", (object) "ConstantsDefinition", (object) RuntimeData.error), 445, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                      }
                                      else
                                      {
                                        Log.Info("Loading text collections...", 451, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        yield return (object) EnumeratorUtility.ParallelSafeExecution(RuntimeData.LoadTextCollectionAsync("Effects"), RuntimeData.LoadTextCollectionAsync("Companions"), RuntimeData.LoadTextCollectionAsync("Mechanisms"), RuntimeData.LoadTextCollectionAsync("Spells"), RuntimeData.LoadTextCollectionAsync("Gods"), RuntimeData.LoadTextCollectionAsync("Summonings"), RuntimeData.LoadTextCollectionAsync("Weapons"), RuntimeData.LoadTextCollectionAsync("UI"));
                                        Log.Info("Waiting for deserialization tasks completion...", 465, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        List<RuntimeData.DeserializationTask> deserializationTasks = RuntimeData.s_deserializationTasks;
                                        int deserializationTaskCount = deserializationTasks.Count;
                                        while (true)
                                        {
                                          bool flag = false;
                                          for (int index = 0; index < deserializationTaskCount; ++index)
                                          {
                                            RuntimeData.DeserializationTask deserializationTask = deserializationTasks[index];
                                            if (deserializationTask.isFaultedOrCancelled)
                                            {
                                              RuntimeData.error = new AssetManagerError(10, deserializationTask.errorMessage);
                                              Log.Error((object) RuntimeData.error, 481, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                              yield break;
                                            }
                                            else if (!deserializationTask.isCompleted)
                                              flag = true;
                                          }
                                          if (flag)
                                            yield return (object) null;
                                          else
                                            break;
                                        }
                                        for (int index = 0; index < deserializationTaskCount; ++index)
                                          deserializationTasks[index].Dispose();
                                        deserializationTasks.Clear();
                                        foreach (ReserveDefinition reserveDefinition in reserveDefinitionsById.Values)
                                          RuntimeData.reserveDefinitions.Add(reserveDefinition.god, reserveDefinition);
                                        foreach (GodDefinition godDefinition in godDefinitionsById.Values)
                                          RuntimeData.godDefinitions.Add(godDefinition.god, godDefinition);
                                        RuntimeData.isReady = true;
                                        Log.Info(string.Format("Loaded {0} character skins definitions.", (object) RuntimeData.characterSkinDefinitions.Count), 524, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} companion definitions.", (object) RuntimeData.companionDefinitions.Count), 525, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} floor mechanism definitions.", (object) RuntimeData.floorMechanismDefinitions.Count), 526, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} god definitions.", (object) godDefinitionsById.Count), 527, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} hero definitions.", (object) RuntimeData.weaponDefinitions.Count), 528, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} object mechanism definitions.", (object) RuntimeData.objectMechanismDefinitions.Count), 529, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} squad definitions.", (object) RuntimeData.squadDefinitions.Count), 530, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} spell definitions.", (object) RuntimeData.spellDefinitions.Count), 531, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} summoning definitions.", (object) RuntimeData.summoningDefinitions.Count), 532, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                        Log.Info(string.Format("Loaded {0} reserve definitions.", (object) reserveDefinitionsById.Count), 533, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                                      }
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }

                    bool CompanionsPredicate(int companionId) => validCompanionIds.Contains(companionId);
                  }
                }
              }
            }
          }
        }
      }
    }

    public static IEnumerator LoadOffline()
    {
      if (RuntimeData.isReady)
        Log.Warning("LoadOffline called while runtime data is already ready.", 546, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      else if (!AssetManager.AddActiveVariant(RuntimeData.currentCultureCode.ToString().ToLowerInvariant()))
      {
        RuntimeData.error = new AssetManagerError(10, string.Format("Could not setup variant for culture code {0}.", (object) RuntimeData.currentCultureCode));
        Log.Error(string.Format("Error while loading text collections bundle: {0}", (object) RuntimeData.error), 557, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      }
      else
      {
        RuntimeData.s_setupCultureCodeVariant = true;
        AssetBundleLoadRequest textCollectionsBundleLoadRequest = AssetManager.LoadAssetBundle("core/localization");
        while (!textCollectionsBundleLoadRequest.isDone)
          yield return (object) null;
        if ((int) textCollectionsBundleLoadRequest.error != 0)
        {
          RuntimeData.error = textCollectionsBundleLoadRequest.error;
          Log.Error(string.Format("Error while loading text collections bundle: {0}", (object) RuntimeData.error), 573, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        }
        else
        {
          RuntimeData.s_loadedTextCollectionsBundle = true;
          yield return (object) RuntimeData.LoadTextCollectionAsync("Application");
        }
      }
    }

    public static IEnumerator Unload()
    {
      int deserializationTaskCount = RuntimeData.s_deserializationTasks.Count;
      for (int i = 0; i < deserializationTaskCount; ++i)
        yield return (object) RuntimeData.s_deserializationTasks[i].Cancel();
      RuntimeData.s_deserializationTasks.Clear();
      AssetBundleUnloadRequest unloadRequest;
      if (RuntimeData.s_loadedDataBundleName)
      {
        unloadRequest = AssetManager.UnloadAssetBundle("core/data");
        while (!unloadRequest.isDone)
          yield return (object) null;
        RuntimeData.s_loadedDataBundleName = false;
        unloadRequest = (AssetBundleUnloadRequest) null;
      }
      if (RuntimeData.s_setupCultureCodeVariant)
      {
        string lowerInvariant = RuntimeData.currentCultureCode.ToString().ToLowerInvariant();
        if (!AssetManager.RemoveActiveVariant(lowerInvariant))
          Log.Warning("Error while trying to unload runtime data, could not remove variant: " + lowerInvariant + ".", 617, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        RuntimeData.s_setupCultureCodeVariant = false;
      }
      if (RuntimeData.s_loadedTextCollectionsBundle)
      {
        foreach (string key in RuntimeData.s_textCollectionsRefCount.Keys)
        {
          AssetReference textCollectionReference;
          if (!RuntimeData.s_localizedTextData.TryGetTextCollectionReference(key, out textCollectionReference))
          {
            Log.Error("Could not load text collection asset named '" + key + "' because it doesn't exist in the localized text data.", 633, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          }
          else
          {
            TextCollection textCollection = textCollectionReference.LoadFromAssetBundle<TextCollection>("core/localization");
            if ((UnityEngine.Object) null == (UnityEngine.Object) textCollection)
              Log.Error("Could not unload text collection asset named '" + key + "'.", 643, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
            else
              RuntimeData.UnloadTextCollectionData(textCollection);
          }
        }
        RuntimeData.s_textCollectionsRefCount.Clear();
        unloadRequest = AssetManager.UnloadAssetBundle("core/localization");
        while (!unloadRequest.isDone)
          yield return (object) null;
        RuntimeData.s_loadedTextCollectionsBundle = false;
        unloadRequest = (AssetBundleUnloadRequest) null;
      }
      RuntimeData.fightDefinitions.Clear();
      RuntimeData.weaponDefinitions.Clear();
      RuntimeData.companionDefinitions.Clear();
      RuntimeData.summoningDefinitions.Clear();
      RuntimeData.floorMechanismDefinitions.Clear();
      RuntimeData.objectMechanismDefinitions.Clear();
      RuntimeData.spellDefinitions.Clear();
      RuntimeData.squadDefinitions.Clear();
      RuntimeData.reserveDefinitions.Clear();
      RuntimeData.godDefinitions.Clear();
      RuntimeData.error = (AssetManagerError) 0;
      RuntimeData.isReady = false;
    }

    public static void Release()
    {
      FontCollection fontCollection;
      if (RuntimeData.s_fontCollectionsByName == null || !RuntimeData.s_fontCollectionsByName.TryGetValue("Default", out fontCollection))
        return;
      fontCollection.Unload();
    }

    public static IEnumerator ChangeLanguage(CultureCode cultureCode)
    {
      if (!(cultureCode == RuntimeData.currentCultureCode))
      {
        AssetBundleUnloadRequest unloadRequest = AssetManager.UnloadAssetBundle("core/localization");
        while (!unloadRequest.isDone)
          yield return (object) null;
        if ((int) unloadRequest.error != 0)
          Log.Error(string.Format("Failed to change language from {0} to {1} because the asset bundle could not be unloaded.", (object) RuntimeData.currentCultureCode, (object) cultureCode), 716, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        else if (!AssetManager.RemoveActiveVariant(RuntimeData.currentCultureCode.ToString().ToLowerInvariant()))
          Log.Error(string.Format("Failed to change language from {0} to {1} because the asset bundle variant could not be unset.", (object) RuntimeData.currentCultureCode, (object) cultureCode), 726, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        else if (!AssetManager.AddActiveVariant(cultureCode.ToString().ToLowerInvariant()))
        {
          Log.Error(string.Format("Failed to change language from {0} to {1} because the asset bundle variant could not be set.", (object) RuntimeData.currentCultureCode, (object) cultureCode), 736, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        }
        else
        {
          AssetBundleLoadRequest loadRequest = AssetManager.LoadAssetBundle("core/localization");
          while (!loadRequest.isDone)
            yield return (object) null;
          if ((int) loadRequest.error != 0)
          {
            Log.Error(string.Format("Failed to change language from {0} to {1} because the asset bundle could not be loaded.", (object) RuntimeData.currentCultureCode, (object) cultureCode), 751, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          }
          else
          {
            foreach (string key in RuntimeData.s_textCollectionsRefCount.Keys)
            {
              AssetReference textCollectionReference;
              if (!RuntimeData.s_localizedTextData.TryGetTextCollectionReference(key, out textCollectionReference))
              {
                Log.Error("Could not load text collection asset named '" + key + "' because it doesn't exist in the localized text data.", 763, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
              }
              else
              {
                TextCollection textCollection = textCollectionReference.LoadFromAssetBundle<TextCollection>("core/localization");
                if ((UnityEngine.Object) null == (UnityEngine.Object) textCollection)
                  Log.Error("Could not load text collection asset named '" + key + "'.", 773, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
                else
                  textCollection.FeedDictionary(RuntimeData.s_textCollectionsData);
              }
            }
            RuntimeData.SetCultureCode(cultureCode);
          }
        }
      }
    }

    public static void SetCultureCode(CultureCode cultureCode)
    {
      FontLanguage fontLanguage = cultureCode.GetFontLanguage();
      RuntimeData.currentCultureCode = cultureCode;
      RuntimeData.currentFontLanguage = fontLanguage;
      RuntimeData.textFormatter.pluralRules = cultureCode.GetPluralRules();
      RuntimeData.CultureCodeChangedEventHandler cultureCodeChanged = RuntimeData.CultureCodeChanged;
      if (cultureCodeChanged == null)
        return;
      cultureCodeChanged(cultureCode, fontLanguage);
    }

    public static IEnumerator LoadFontCollection([NotNull] string fontCollectionName)
    {
      FontCollection fontCollection;
      if (!RuntimeData.s_fontCollectionsByName.TryGetValue(fontCollectionName, out fontCollection))
        Log.Error("Could not find a font collection named '" + fontCollectionName + "'.", 801, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      else
        yield return (object) fontCollection.LoadAsync();
    }

    public static void UnloadFontCollection([NotNull] string fontCollectionName)
    {
      FontCollection fontCollection;
      if (!RuntimeData.s_fontCollectionsByName.TryGetValue(fontCollectionName, out fontCollection))
        Log.Error("Could not find a font collection named '" + fontCollectionName + "'.", 815, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      else
        fontCollection.Unload();
    }

    public static bool LoadTextCollection([NotNull] string textCollectionName)
    {
      int num1;
      if (RuntimeData.s_textCollectionsRefCount.TryGetValue(textCollectionName, out num1))
      {
        int num2 = num1 + 1;
        RuntimeData.s_textCollectionsRefCount[textCollectionName] = num2;
        return true;
      }
      AssetReference textCollectionReference;
      if (!RuntimeData.s_localizedTextData.TryGetTextCollectionReference(textCollectionName, out textCollectionReference))
      {
        Log.Error("Could not load text collection asset named '" + textCollectionName + "' because it doesn't exist in the localized text data.", 837, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      TextCollection textCollection = textCollectionReference.LoadFromAssetBundle<TextCollection>("core/localization");
      if ((UnityEngine.Object) null == (UnityEngine.Object) textCollection)
      {
        Log.Error("Could not load text collection asset named '" + textCollectionName + "'.", 847, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      RuntimeData.LoadTextCollectionData(textCollection);
      RuntimeData.s_textCollectionsRefCount.Add(textCollectionName, 1);
      return true;
    }

    public static bool UnloadTextCollection(string textCollectionName)
    {
      int num1;
      if (!RuntimeData.s_textCollectionsRefCount.TryGetValue(textCollectionName, out num1))
      {
        Log.Warning("Tried to unload text collection named '" + textCollectionName + "' but it was not loaded.", 864, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      int num2 = num1 - 1;
      if (num2 == 0)
        RuntimeData.s_textCollectionsRefCount.Remove(textCollectionName);
      else
        RuntimeData.s_textCollectionsRefCount[textCollectionName] = num2;
      AssetReference textCollectionReference;
      if (!RuntimeData.s_localizedTextData.TryGetTextCollectionReference(textCollectionName, out textCollectionReference))
      {
        Log.Error("Could not load text collection asset named '" + textCollectionName + "' because it doesn't exist in the localized text data.", 881, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      TextCollection textCollection = textCollectionReference.LoadFromAssetBundle<TextCollection>("core/localization");
      if ((UnityEngine.Object) null == (UnityEngine.Object) textCollection)
      {
        Log.Error("Could not unload text collection asset named '" + textCollectionName + "'.", 891, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return false;
      }
      RuntimeData.UnloadTextCollectionData(textCollection);
      return true;
    }

    public static IEnumerator LoadTextCollectionAsync(string textCollectionName)
    {
      int num1;
      if (RuntimeData.s_textCollectionsRefCount.TryGetValue(textCollectionName, out num1))
      {
        int num2 = num1 + 1;
        RuntimeData.s_textCollectionsRefCount[textCollectionName] = num2;
      }
      else
      {
        AssetReference textCollectionReference;
        if (!RuntimeData.s_localizedTextData.TryGetTextCollectionReference(textCollectionName, out textCollectionReference))
        {
          Log.Error("Could not load text collection asset named '" + textCollectionName + "' because it doesn't exist in the localized text data.", 914, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        }
        else
        {
          AssetLoadRequest<TextCollection> loadRequest = textCollectionReference.LoadFromAssetBundleAsync<TextCollection>("core/localization");
          while (!loadRequest.isDone)
            yield return (object) null;
          if ((int) loadRequest.error != 0)
          {
            Log.Error(string.Format("Could not load text collection asset named '{0}': {1}.", (object) textCollectionName, (object) loadRequest.error), 929, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          }
          else
          {
            TextCollection asset = loadRequest.asset;
            if ((UnityEngine.Object) null == (UnityEngine.Object) asset)
            {
              Log.Error("Found text collection asset named '" + textCollectionName + "' but it is invalid.", 936, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
            }
            else
            {
              RuntimeData.LoadTextCollectionData(asset);
              RuntimeData.s_textCollectionsRefCount.Add(textCollectionName, 1);
            }
          }
        }
      }
    }

    public static IEnumerator UnloadTextCollectionAsync(string textCollectionName)
    {
      int num1;
      if (!RuntimeData.s_textCollectionsRefCount.TryGetValue(textCollectionName, out num1))
      {
        Log.Warning("Tried to unload text collection named '" + textCollectionName + "' but it was not loaded.", 952, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      }
      else
      {
        int num2 = num1 - 1;
        if (num2 == 0)
          RuntimeData.s_textCollectionsRefCount.Remove(textCollectionName);
        else
          RuntimeData.s_textCollectionsRefCount[textCollectionName] = num2;
        AssetReference textCollectionReference;
        if (!RuntimeData.s_localizedTextData.TryGetTextCollectionReference(textCollectionName, out textCollectionReference))
        {
          Log.Error("Could not unload text collection asset named '" + textCollectionName + "' because it doesn't exist in the localized text data.", 969, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        }
        else
        {
          AssetLoadRequest<TextCollection> unloadRequest = textCollectionReference.LoadFromAssetBundleAsync<TextCollection>("core/localization");
          while (!unloadRequest.isDone)
            yield return (object) null;
          if ((int) unloadRequest.error != 0)
          {
            Log.Error(string.Format("Could not unload text collection asset named '{0}': {1}.", (object) textCollectionName, (object) unloadRequest.error), 984, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          }
          else
          {
            TextCollection asset = unloadRequest.asset;
            if ((UnityEngine.Object) null == (UnityEngine.Object) asset)
              Log.Error("Found text collection asset named '" + textCollectionName + "' but it is invalid.", 992, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
            else
              RuntimeData.UnloadTextCollectionData(asset);
          }
        }
      }
    }

    public static bool TryGetFontCollection(string identifier, out FontCollection fontCollection) => RuntimeData.s_fontCollections.TryGetValue(identifier, out fontCollection);

    public static bool TryGetTextKeyId([NotNull] string name, out int id) => RuntimeData.s_localizedTextData.TryGetKeyId(name, out id);

    public static string FormattedText(int textKeyId, params string[] args) => args == null || args.Length == 0 ? RuntimeData.FormattedText(textKeyId, (IValueProvider) null) : RuntimeData.FormattedText(textKeyId, (IValueProvider) new IndexedValueProvider(args));

    public static string FormattedText(string name, params string[] args) => args == null || args.Length == 0 ? RuntimeData.FormattedText(name, (IValueProvider) null) : RuntimeData.FormattedText(name, (IValueProvider) new IndexedValueProvider(args));

    public static string FormattedText(string name, IValueProvider valueProvider = null)
    {
      string pattern;
      if (!RuntimeData.TryGetText(name, out pattern))
      {
        Log.Error("Text key with name " + name + " does not exist.", 1047, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return string.Empty;
      }
      try
      {
        FormatterParams formatterParams = new FormatterParams(RuntimeData.textFormatter, valueProvider)
        {
          context = RuntimeData.currentKeywordContext
        };
        return RuntimeData.textFormatter.Format(pattern, formatterParams);
      }
      catch (Exception ex)
      {
        Log.Error(string.Format("Text value '{0}' could not be formatted with specified {1} params: {2}", (object) pattern, (object) valueProvider, (object) ex.Message), 1061, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      }
      return pattern;
    }

    public static string FormattedText(int textKeyId, IValueProvider valueProvider = null)
    {
      string pattern;
      if (!RuntimeData.TryGetText(textKeyId, out pattern))
      {
        Log.Error(string.Format("Text key with id {0} does not exist.", (object) textKeyId), 1072, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
        return string.Empty;
      }
      try
      {
        FormatterParams formatterParams = new FormatterParams(RuntimeData.textFormatter, valueProvider)
        {
          context = RuntimeData.currentKeywordContext
        };
        return RuntimeData.textFormatter.Format(pattern, formatterParams);
      }
      catch (Exception ex)
      {
        Log.Error(string.Format("Text value '{0}' could not be formatted with specified {1} params: {2}", (object) pattern, (object) valueProvider, (object) ex.Message), 1086, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      }
      return pattern;
    }

    public static bool TryGetText(int id, [NotNull] out string value) => RuntimeData.s_textCollectionsData.TryGetValue(id, out value);

    public static bool TryGetText([NotNull] string name, [NotNull] out string value)
    {
      int id;
      if (RuntimeData.s_localizedTextData.TryGetKeyId(name, out id))
        return RuntimeData.s_textCollectionsData.TryGetValue(id, out value);
      Log.Warning("Could not found a text key named '" + name + "'.", 1140, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
      value = string.Empty;
      return false;
    }

    private static IEnumerator LoadDataDefinition<T>([NotNull] Action<T> assignationCallback) where T : EditableData
    {
      AllAssetsLoadRequest<T> assetLoadRequest = AssetManager.LoadAllAssetsAsync<T>("core/data");
      while (!assetLoadRequest.isDone)
        yield return (object) null;
      if ((int) assetLoadRequest.error != 0)
      {
        RuntimeData.error = assetLoadRequest.error;
      }
      else
      {
        T[] assets = assetLoadRequest.assets;
        int length = assets.Length;
        if (length == 0)
        {
          RuntimeData.error = new AssetManagerError(10, "[RuntimeData] Could not find any data definition of type T.");
        }
        else
        {
          assignationCallback(assets[0]);
          if (length > 1)
            Log.Warning("Data definition of type T is loaded using LoadDataDefinition but multiple assets were found.", 1283, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
          RuntimeData.DeserializationTask deserializationTask = new RuntimeData.DeserializationTask((EditableData[]) assets, 0, length, typeof (T));
          deserializationTask.Run();
          RuntimeData.s_deserializationTasks.Add(deserializationTask);
        }
      }
    }

    private static IEnumerator LoadDataDefinitions<T>(
      Dictionary<int, T> dictionary,
      Predicate<int> filter = null)
      where T : EditableData
    {
      AllAssetsLoadRequest<T> assetLoadRequest = AssetManager.LoadAllAssetsAsync<T>("core/data");
      while (!assetLoadRequest.isDone)
        yield return (object) null;
      if ((int) assetLoadRequest.error != 0)
      {
        RuntimeData.error = assetLoadRequest.error;
      }
      else
      {
        T[] assets1 = assetLoadRequest.assets;
        T[] assets2;
        if (filter == null)
        {
          assets2 = assets1;
        }
        else
        {
          int length = assets1.Length;
          List<T> objList = new List<T>();
          for (int index = 0; index < length; ++index)
          {
            T obj = assets1[index];
            int result;
            if (int.TryParse(obj.name, out result) && filter(result))
              objList.Add(obj);
          }
          assets2 = objList.ToArray();
        }
        int length1 = assets2.Length;
        for (int index = 0; index < length1; ++index)
        {
          T obj = assets2[index];
          int result;
          if (!int.TryParse(obj.name, out result))
          {
            RuntimeData.error = new AssetManagerError(10, "[RuntimeData] ID of asset of type " + typeof (T).Name + " and named '" + obj.name + "' cannot be inferred.");
            dictionary.Clear();
            yield break;
          }
          else if (dictionary.ContainsKey(result))
          {
            RuntimeData.error = new AssetManagerError(10, string.Format("[RuntimeData] Duplicate asset of type {0} with id {1} from bundle named '{2}'.", (object) typeof (T).Name, (object) result, (object) "core/data"));
            dictionary.Clear();
            yield break;
          }
          else
            dictionary.Add(result, obj);
        }
        for (int index = 0; index < length1; index += 128)
        {
          int startIndex = index;
          int endIndex = Math.Min(startIndex + 128, length1);
          RuntimeData.DeserializationTask deserializationTask = new RuntimeData.DeserializationTask((EditableData[]) assets2, startIndex, endIndex, typeof (T));
          deserializationTask.Run();
          RuntimeData.s_deserializationTasks.Add(deserializationTask);
        }
      }
    }

    private static void LoadTextCollectionData([NotNull] TextCollection textCollection)
    {
      textCollection.FeedDictionary(RuntimeData.s_textCollectionsData);
      Log.Info(string.Format("Text collection data now has {0} entries after loading {1}.", (object) RuntimeData.s_textCollectionsData.Count, (object) textCollection.name), 1384, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
    }

    private static void UnloadTextCollectionData([NotNull] TextCollection textCollection)
    {
      textCollection.StarveDictionary(RuntimeData.s_textCollectionsData);
      Log.Info(string.Format("Text collection data now has {0} entries after unloading {1}.", (object) RuntimeData.s_textCollectionsData.Count, (object) textCollection.name), 1392, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\RuntimeData.cs");
    }

    public static bool IsPlayable(God god)
    {
      IReadOnlyList<GodAvailability> gods = RuntimeData.availabilityDefinition.gods;
      int count = ((IReadOnlyCollection<GodAvailability>) gods).Count;
      for (int index = 0; index < count; ++index)
      {
        GodAvailability godAvailability = gods[index];
        if (godAvailability.god == god)
          return godAvailability.availability != 0;
      }
      return false;
    }

    public delegate void CultureCodeChangedEventHandler(
      CultureCode cultureCode,
      FontLanguage fontLanguage);

    private struct DeserializationTask
    {
      private readonly EditableData[] m_assets;
      private readonly int m_startIndex;
      private readonly int m_endIndex;
      private readonly System.Type m_assetType;
      private Task m_task;
      private CancellationTokenSource m_cancellationTokenSource;
      private CancellationToken m_cancellationToken;

      public DeserializationTask(
        EditableData[] assets,
        int startIndex,
        int endIndex,
        System.Type assetType)
      {
        this.m_assets = assets;
        this.m_startIndex = startIndex;
        this.m_endIndex = endIndex;
        this.m_assetType = assetType;
        this.m_cancellationTokenSource = (CancellationTokenSource) null;
        this.m_task = (Task) null;
      }

      public bool isFaultedOrCancelled => this.m_task == null || this.m_task.IsFaulted || this.m_task.IsCanceled;

      public bool isCompleted => this.m_task == null || this.m_task.IsCompleted;

      public string errorMessage => "[RuntimeData] Failed to parse an asset of type " + this.m_assetType.Name + "'.\n" + string.Format("Reason: {0}", (object) this.m_task.Exception?.InnerException);

      public void Run()
      {
        this.m_cancellationTokenSource = new CancellationTokenSource();
        this.m_cancellationToken = this.m_cancellationTokenSource.Token;
        this.m_task = Task.Run(new Action(this.Deserialize), this.m_cancellationToken);
      }

      public IEnumerator Cancel()
      {
        if (this.m_cancellationTokenSource != null && this.m_task != null)
        {
          this.m_cancellationTokenSource.Cancel();
          while (!this.m_task.IsCompleted)
            yield return (object) null;
          this.m_cancellationTokenSource.Dispose();
          this.m_task.Dispose();
          this.m_cancellationTokenSource = (CancellationTokenSource) null;
          this.m_task = (Task) null;
        }
      }

      public void Dispose()
      {
        if (this.m_cancellationTokenSource != null)
        {
          this.m_cancellationTokenSource.Dispose();
          this.m_cancellationTokenSource = (CancellationTokenSource) null;
        }
        if (this.m_task == null)
          return;
        this.m_task.Dispose();
        this.m_task = (Task) null;
      }

      private void Deserialize()
      {
        EditableData[] assets = this.m_assets;
        int endIndex = this.m_endIndex;
        for (int startIndex = this.m_startIndex; startIndex < endIndex; ++startIndex)
        {
          if (this.m_cancellationToken.IsCancellationRequested)
          {
            this.m_cancellationToken.ThrowIfCancellationRequested();
            break;
          }
          assets[startIndex].LoadFromJson();
        }
      }
    }
  }
}
