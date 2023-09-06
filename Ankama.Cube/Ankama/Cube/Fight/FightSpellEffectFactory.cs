// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightSpellEffectFactory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight
{
  public class FightSpellEffectFactory : ScriptableObject
  {
    private const string BundleName = "core/spells/effects";
    private static FightSpellEffectFactory s_instance;
    [SerializeField]
    private SpellEffectReferenceDictionary m_genericSpellEffects = new SpellEffectReferenceDictionary();
    [SerializeField]
    private PropertyEffectReferenceDictionary m_propertyEffects = new PropertyEffectReferenceDictionary();
    [SerializeField]
    private FloatingCounterEffectReferenceDictionary m_floatingCounterEffects = new FloatingCounterEffectReferenceDictionary();
    [SerializeField]
    private SightEffectReferenceDictionary m_sightEffects = new SightEffectReferenceDictionary();
    [SerializeField]
    private FloatingCounterFeedback m_floatingCounterFeedbackPrefab;
    private static Dictionary<SpellEffectKey, SpellEffect> s_spellEffectCache;
    private static Dictionary<PropertyId, AttachableEffect> s_propertyEffectCache;
    private static Dictionary<CaracId, FloatingCounterEffect> s_floatingCounterEffectCache;
    private static Dictionary<PropertyId, FloatingCounterEffect> s_sightEffectCache;
    private static GameObjectPool s_floatingCounterFeedbackPool;
    private static FightSpellEffectFactory.SpellEffectOverrideData[] s_currentSpellEffectOverrideData;
    private static List<SpellDefinition> s_loadedSpellDefinitions;

    public static bool isReady { get; private set; }

    public static IEnumerator Load(int fightCount)
    {
      if (FightSpellEffectFactory.isReady)
      {
        Log.Error("Load called while the fight object factory is already ready.", 91, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
      }
      else
      {
        AssetBundleLoadRequest bundleRequest = AssetManager.LoadAssetBundle("core/spells/effects");
        while (!bundleRequest.isDone)
          yield return (object) null;
        if ((int) bundleRequest.error != 0)
        {
          Log.Error(string.Format("Error while loading bundle '{0}': {1}", (object) "core/spells/effects", (object) bundleRequest.error), 103, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
        }
        else
        {
          AllAssetsLoadRequest<FightSpellEffectFactory> assetLoadRequest = AssetManager.LoadAllAssetsAsync<FightSpellEffectFactory>("core/spells/effects");
          while (!assetLoadRequest.isDone)
            yield return (object) null;
          if ((int) assetLoadRequest.error != 0)
          {
            Log.Error(string.Format("Error while loading asset {0}: {1}", (object) nameof (FightSpellEffectFactory), (object) assetLoadRequest.error), 115, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
          }
          else
          {
            FightSpellEffectFactory.s_instance = assetLoadRequest.assets[0];
            SpellEffectReferenceDictionary genericSpellEffects = FightSpellEffectFactory.s_instance.m_genericSpellEffects;
            Dictionary<SpellEffectKey, SpellEffect> spellEffectCache = new Dictionary<SpellEffectKey, SpellEffect>(genericSpellEffects.Count, (IEqualityComparer<SpellEffectKey>) SpellEffectKeyComparer.instance);
            FightSpellEffectFactory.s_spellEffectCache = spellEffectCache;
            yield return (object) FightSpellEffectFactory.PreloadEffectAssets<SpellEffectKey, SpellEffect>((SerializableDictionaryLogic<SpellEffectKey, AssetReference>) genericSpellEffects, spellEffectCache, "core/spells/effects");
            yield return (object) FightSpellEffectFactory.LoadEffectsResources<SpellEffectKey, SpellEffect>(spellEffectCache);
            PropertyEffectReferenceDictionary propertyEffects = FightSpellEffectFactory.s_instance.m_propertyEffects;
            Dictionary<PropertyId, AttachableEffect> propertyEffectCache = new Dictionary<PropertyId, AttachableEffect>(propertyEffects.Count, (IEqualityComparer<PropertyId>) PropertyIdComparer.instance);
            FightSpellEffectFactory.s_propertyEffectCache = propertyEffectCache;
            yield return (object) FightSpellEffectFactory.PreloadEffectAssets<PropertyId, AttachableEffect>((SerializableDictionaryLogic<PropertyId, AssetReference>) propertyEffects, propertyEffectCache, "core/spells/effects");
            yield return (object) FightSpellEffectFactory.LoadEffectsResources<PropertyId, AttachableEffect>(propertyEffectCache);
            SightEffectReferenceDictionary sightEffects = FightSpellEffectFactory.s_instance.m_sightEffects;
            Dictionary<PropertyId, FloatingCounterEffect> sightEffectCache = new Dictionary<PropertyId, FloatingCounterEffect>(sightEffects.Count, (IEqualityComparer<PropertyId>) PropertyIdComparer.instance);
            FightSpellEffectFactory.s_sightEffectCache = sightEffectCache;
            yield return (object) FightSpellEffectFactory.PreloadEffectAssets<PropertyId, FloatingCounterEffect>((SerializableDictionaryLogic<PropertyId, AssetReference>) sightEffects, sightEffectCache, "core/spells/effects");
            yield return (object) FightSpellEffectFactory.LoadEffectsResources<PropertyId, FloatingCounterEffect>(sightEffectCache);
            FloatingCounterEffectReferenceDictionary floatingCounterEffects = FightSpellEffectFactory.s_instance.m_floatingCounterEffects;
            Dictionary<CaracId, FloatingCounterEffect> floatingCounterEffectCache = new Dictionary<CaracId, FloatingCounterEffect>(floatingCounterEffects.Count, (IEqualityComparer<CaracId>) CaracIdComparer.instance);
            FightSpellEffectFactory.s_floatingCounterEffectCache = floatingCounterEffectCache;
            yield return (object) FightSpellEffectFactory.PreloadEffectAssets<CaracId, FloatingCounterEffect>((SerializableDictionaryLogic<CaracId, AssetReference>) floatingCounterEffects, floatingCounterEffectCache, "core/spells/effects");
            yield return (object) FightSpellEffectFactory.LoadEffectsResources<CaracId, FloatingCounterEffect>(floatingCounterEffectCache);
            FightSpellEffectFactory.s_floatingCounterFeedbackPool = new GameObjectPool(FightSpellEffectFactory.s_instance.m_floatingCounterFeedbackPrefab.gameObject);
            FightSpellEffectFactory.s_loadedSpellDefinitions = new List<SpellDefinition>(24);
            FightSpellEffectFactory.s_currentSpellEffectOverrideData = new FightSpellEffectFactory.SpellEffectOverrideData[fightCount];
            FightSpellEffectFactory.isReady = true;
          }
        }
      }
    }

    public static IEnumerator Unload()
    {
      if (FightSpellEffectFactory.isReady)
      {
        FightSpellEffectFactory.isReady = false;
        if (FightSpellEffectFactory.s_spellEffectCache != null)
        {
          FightSpellEffectFactory.UnloadCache<SpellEffectKey, SpellEffect>(FightSpellEffectFactory.s_spellEffectCache);
          FightSpellEffectFactory.s_spellEffectCache = (Dictionary<SpellEffectKey, SpellEffect>) null;
        }
        if (FightSpellEffectFactory.s_propertyEffectCache != null)
        {
          FightSpellEffectFactory.UnloadCache<PropertyId, AttachableEffect>(FightSpellEffectFactory.s_propertyEffectCache);
          FightSpellEffectFactory.s_propertyEffectCache = (Dictionary<PropertyId, AttachableEffect>) null;
        }
        if (FightSpellEffectFactory.s_sightEffectCache != null)
        {
          FightSpellEffectFactory.UnloadCache<PropertyId, FloatingCounterEffect>(FightSpellEffectFactory.s_sightEffectCache);
          FightSpellEffectFactory.s_sightEffectCache = (Dictionary<PropertyId, FloatingCounterEffect>) null;
        }
        if (FightSpellEffectFactory.s_floatingCounterEffectCache != null)
        {
          FightSpellEffectFactory.UnloadCache<CaracId, FloatingCounterEffect>(FightSpellEffectFactory.s_floatingCounterEffectCache);
          FightSpellEffectFactory.s_floatingCounterEffectCache = (Dictionary<CaracId, FloatingCounterEffect>) null;
        }
        if (FightSpellEffectFactory.s_floatingCounterFeedbackPool != null)
        {
          FightSpellEffectFactory.s_floatingCounterFeedbackPool.Clear();
          FightSpellEffectFactory.s_floatingCounterFeedbackPool = (GameObjectPool) null;
        }
        if (FightSpellEffectFactory.s_loadedSpellDefinitions != null)
        {
          foreach (SpellDefinition loadedSpellDefinition in FightSpellEffectFactory.s_loadedSpellDefinitions)
          {
            if ((UnityEngine.Object) null != (UnityEngine.Object) loadedSpellDefinition)
              loadedSpellDefinition.UnloadResources();
          }
          FightSpellEffectFactory.s_loadedSpellDefinitions.Clear();
          FightSpellEffectFactory.s_loadedSpellDefinitions = (List<SpellDefinition>) null;
        }
        FightSpellEffectFactory.s_currentSpellEffectOverrideData = (FightSpellEffectFactory.SpellEffectOverrideData[]) null;
        FightSpellEffectFactory.s_instance = (FightSpellEffectFactory) null;
        AssetBundleUnloadRequest unloadRequest = AssetManager.UnloadAssetBundle("core/factories/fight_object_factory");
        while (!unloadRequest.isDone)
          yield return (object) null;
      }
    }

    public static void NotifySpellDefinitionLoaded(SpellDefinition spellDefinition)
    {
      if (FightSpellEffectFactory.s_loadedSpellDefinitions == null)
        Log.Error("NotifySpellDefinitionLoaded called while the factory is not ready.", 231, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
      else
        FightSpellEffectFactory.s_loadedSpellDefinitions.Add(spellDefinition);
    }

    public static void SetupSpellEffectOverrides(
      ISpellEffectOverrideProvider definition,
      int fightId,
      int eventId)
    {
      if (FightSpellEffectFactory.s_currentSpellEffectOverrideData == null)
        Log.Error("SetupSpellEffectOverrides called while the factory is not ready.", 242, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
      else
        FightSpellEffectFactory.s_currentSpellEffectOverrideData[fightId] = new FightSpellEffectFactory.SpellEffectOverrideData(definition, eventId);
    }

    public static void ClearSpellEffectOverrides(int fightId)
    {
      if (FightSpellEffectFactory.s_currentSpellEffectOverrideData == null)
        return;
      FightSpellEffectFactory.s_currentSpellEffectOverrideData[fightId] = new FightSpellEffectFactory.SpellEffectOverrideData((ISpellEffectOverrideProvider) null, 0);
    }

    public static bool TryGetSpellEffect(
      SpellEffectKey key,
      int fightId,
      int? parentEventId,
      out SpellEffect spellEffect)
    {
      if (FightSpellEffectFactory.s_spellEffectCache == null || FightSpellEffectFactory.s_currentSpellEffectOverrideData == null)
      {
        Log.Error("TryGetSpellEffect called while the factory is not ready.", 270, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
        spellEffect = (SpellEffect) null;
        return false;
      }
      return parentEventId.HasValue && FightSpellEffectFactory.s_currentSpellEffectOverrideData[fightId].TryGetSpellEffectOverride(key, parentEventId.Value, out spellEffect) || FightSpellEffectFactory.s_spellEffectCache.TryGetValue(key, out spellEffect);
    }

    public static IEnumerator PlayGenericEffect(
      SpellEffectKey key,
      int fightId,
      int? parentEventId,
      [NotNull] IsoObject target,
      [CanBeNull] FightContext fightContext)
    {
      if (FightSpellEffectFactory.s_spellEffectCache == null || FightSpellEffectFactory.s_currentSpellEffectOverrideData == null)
      {
        Log.Error("PlayGenericEffect called while the factory is not ready.", 290, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
      }
      else
      {
        SpellEffect spellEffect;
        if ((parentEventId.HasValue && FightSpellEffectFactory.s_currentSpellEffectOverrideData[fightId].TryGetSpellEffectOverride(key, parentEventId.Value, out spellEffect) || FightSpellEffectFactory.s_spellEffectCache.TryGetValue(key, out spellEffect)) && !((UnityEngine.Object) null == (UnityEngine.Object) spellEffect))
        {
          CellObject cellObject = target.cellObject;
          if ((UnityEngine.Object) null == (UnityEngine.Object) cellObject)
          {
            Log.Warning(string.Format("Tried to play generic effect {0} on target named {1} ({2}) but the target is no longer on the board.", (object) key, (object) target.name, (object) target.GetType().Name), 313, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
          }
          else
          {
            Transform transform = cellObject.transform;
            Quaternion rotation = Quaternion.identity;
            Vector3 scale = Vector3.one;
            ITimelineContextProvider contextProvider = target as ITimelineContextProvider;
            switch (spellEffect.orientationMethod)
            {
              case SpellEffect.OrientationMethod.None:
                CameraHandler current = CameraHandler.current;
                if ((UnityEngine.Object) null != (UnityEngine.Object) current)
                {
                  rotation = current.mapRotation.GetInverseRotation();
                  break;
                }
                break;
              case SpellEffect.OrientationMethod.Context:
                if (contextProvider != null && contextProvider.GetTimelineContext() is VisualEffectContext timelineContext)
                {
                  timelineContext.GetVisualEffectTransformation(out rotation, out scale);
                  break;
                }
                break;
              case SpellEffect.OrientationMethod.SpellEffectTarget:
                Log.Warning(string.Format("Spell effect named '{0}' orientation method is {1} but is not played from a spell.", (object) spellEffect.name, (object) SpellEffect.OrientationMethod.SpellEffectTarget), 346, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
                break;
              default:
                throw new ArgumentOutOfRangeException();
            }
            yield return (object) FightSpellEffectFactory.PlaySpellEffect(spellEffect, transform, rotation, scale, 0.0f, fightContext, contextProvider);
          }
        }
      }
    }

    public static IEnumerator PlayGenericEffect(
      SpellEffectKey key,
      int fightId,
      int? parentEventId,
      [NotNull] Transform parent,
      Quaternion rotation,
      Vector3 scale,
      [CanBeNull] FightContext fightContext,
      [CanBeNull] ITimelineContextProvider contextProvider)
    {
      if (FightSpellEffectFactory.s_spellEffectCache == null || FightSpellEffectFactory.s_currentSpellEffectOverrideData == null)
      {
        Log.Error("PlayGenericEffect called while the factory is not ready.", 361, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
      }
      else
      {
        SpellEffect spellEffect;
        if ((parentEventId.HasValue && FightSpellEffectFactory.s_currentSpellEffectOverrideData[fightId].TryGetSpellEffectOverride(key, parentEventId.Value, out spellEffect) || FightSpellEffectFactory.s_spellEffectCache.TryGetValue(key, out spellEffect)) && !((UnityEngine.Object) null == (UnityEngine.Object) spellEffect))
          yield return (object) FightSpellEffectFactory.PlaySpellEffect(spellEffect, parent, rotation, scale, 0.0f, fightContext, contextProvider);
      }
    }

    public static IEnumerator PlaySpellEffect(
      [NotNull] SpellEffect spellEffect,
      Vector2Int coords,
      [NotNull] SpellEffectInstantiationData instantiationData,
      [NotNull] CastTargetContext castTargetContext)
    {
      CellObject cellObject;
      if (FightMap.current.TryGetCellObject(coords.x, coords.y, out cellObject))
      {
        Transform transform = cellObject.transform;
        FightContext context = castTargetContext.fightStatus.context;
        ITimelineContextProvider contextProvider = (ITimelineContextProvider) null;
        Quaternion rotation = Quaternion.identity;
        Vector3 scale = Vector3.one;
        switch (spellEffect.orientationMethod)
        {
          case SpellEffect.OrientationMethod.None:
            CameraHandler current = CameraHandler.current;
            if ((UnityEngine.Object) null != (UnityEngine.Object) current)
            {
              rotation = current.mapRotation.GetInverseRotation();
              break;
            }
            break;
          case SpellEffect.OrientationMethod.Context:
            CharacterObject isoObject;
            if (cellObject.TryGetIsoObject<CharacterObject>(out isoObject))
            {
              contextProvider = (ITimelineContextProvider) isoObject;
              if (contextProvider.GetTimelineContext() is VisualEffectContext timelineContext)
              {
                timelineContext.GetVisualEffectTransformation(out rotation, out scale);
                break;
              }
              break;
            }
            Log.Warning(string.Format("Spell effect named '{0}' orientation method is {1} but context provider could not be found.", (object) spellEffect.name, (object) SpellEffect.OrientationMethod.Context), 426, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
            break;
          case SpellEffect.OrientationMethod.SpellEffectTarget:
            rotation = instantiationData.GetOrientation(coords, castTargetContext);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        float delayOverDistance = instantiationData.GetDelayOverDistance(coords);
        yield return (object) FightSpellEffectFactory.PlaySpellEffect(spellEffect, transform, rotation, scale, delayOverDistance, context, contextProvider);
      }
    }

    public static IEnumerator PlaySpellEffect(
      [NotNull] SpellEffect spellEffect,
      [NotNull] IsoObject view,
      [NotNull] SpellEffectInstantiationData instantiationData,
      [NotNull] CastTargetContext castTargetContext)
    {
      CellObject cellObject = view.cellObject;
      if ((UnityEngine.Object) null == (UnityEngine.Object) cellObject)
      {
        Log.Warning("Tried to play spell effect " + spellEffect.name + " on target named " + view.name + " (" + view.GetType().Name + ") but the target is no longer on the board.", 449, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
      }
      else
      {
        Transform transform = cellObject.transform;
        FightContext context = castTargetContext.fightStatus.context;
        ITimelineContextProvider contextProvider = view as ITimelineContextProvider;
        Quaternion rotation = Quaternion.identity;
        Vector3 scale = Vector3.one;
        switch (spellEffect.orientationMethod)
        {
          case SpellEffect.OrientationMethod.None:
            CameraHandler current = CameraHandler.current;
            if ((UnityEngine.Object) null != (UnityEngine.Object) current)
            {
              rotation = current.mapRotation.GetInverseRotation();
              break;
            }
            break;
          case SpellEffect.OrientationMethod.Context:
            if (contextProvider != null && contextProvider.GetTimelineContext() is VisualEffectContext timelineContext)
            {
              timelineContext.GetVisualEffectTransformation(out rotation, out scale);
              break;
            }
            break;
          case SpellEffect.OrientationMethod.SpellEffectTarget:
            rotation = instantiationData.GetOrientation(cellObject.coords, castTargetContext);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        float delayOverDistance = instantiationData.GetDelayOverDistance(cellObject.coords);
        yield return (object) FightSpellEffectFactory.PlaySpellEffect(spellEffect, transform, rotation, scale, delayOverDistance, context, contextProvider);
      }
    }

    public static IEnumerator PlaySpellEffect(
      [NotNull] SpellEffect spellEffect,
      [NotNull] Transform transform,
      Quaternion rotation,
      Vector3 scale,
      float delay,
      [CanBeNull] FightContext fightContext,
      [CanBeNull] ITimelineContextProvider contextProvider)
    {
      if ((double) delay > 0.0)
        yield return (object) new WaitForTime(delay);
      Component instance = spellEffect.Instantiate(transform, rotation, scale, fightContext, contextProvider);
      if ((UnityEngine.Object) null != (UnityEngine.Object) instance)
      {
        switch (spellEffect.waitMethod)
        {
          case SpellEffect.WaitMethod.None:
            MonoBehaviour current1 = (MonoBehaviour) FightMap.current;
            if (!((UnityEngine.Object) null != (UnityEngine.Object) current1))
              break;
            current1.StartCoroutine(spellEffect.DestroyWhenFinished(instance));
            break;
          case SpellEffect.WaitMethod.Delay:
            yield return (object) new WaitForTime(spellEffect.waitDelay);
            if (!((UnityEngine.Object) null != (UnityEngine.Object) instance))
              break;
            MonoBehaviour current2 = (MonoBehaviour) FightMap.current;
            if (!((UnityEngine.Object) null != (UnityEngine.Object) current2))
              break;
            current2.StartCoroutine(spellEffect.DestroyWhenFinished(instance));
            break;
          case SpellEffect.WaitMethod.Destruction:
            yield return (object) spellEffect.DestroyWhenFinished(instance);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    public static bool TryGetPropertyEffect(
      PropertyId propertyId,
      out AttachableEffect attachableEffect)
    {
      return FightSpellEffectFactory.s_propertyEffectCache.TryGetValue(propertyId, out attachableEffect);
    }

    public static bool TryGetFloatingCounterEffect(
      CaracId counterId,
      PropertyId? propertyId,
      out FloatingCounterEffect floatingEffectCounter)
    {
      return counterId == CaracId.FloatingCounterSight && propertyId.HasValue ? FightSpellEffectFactory.s_sightEffectCache.TryGetValue(propertyId.Value, out floatingEffectCounter) : FightSpellEffectFactory.s_floatingCounterEffectCache.TryGetValue(counterId, out floatingEffectCounter);
    }

    public static FloatingCounterFeedback InstantiateFloatingCounterFeedback(Transform parent) => FightSpellEffectFactory.s_floatingCounterFeedbackPool.Instantiate(parent.position, parent.rotation, parent).GetComponent<FloatingCounterFeedback>();

    public static void DestroyFloatingCounterFeedback([NotNull] FloatingCounterFeedback instance)
    {
      instance.Clear();
      FightSpellEffectFactory.s_floatingCounterFeedbackPool.Release(instance.gameObject);
    }

    private static IEnumerator PreloadEffectAssets<K, V>(
      SerializableDictionaryLogic<K, AssetReference> effects,
      Dictionary<K, V> effectCache,
      string bundleName)
      where V : ScriptableEffect
    {
      int count = ((Dictionary<K, AssetReference>) effects).Count;
      if (count != 0)
      {
        AssetLoadRequest<V>[] loadRequests = new AssetLoadRequest<V>[count];
        int index1 = 0;
        foreach (AssetReference assetReference in ((Dictionary<K, AssetReference>) effects).Values)
        {
          if (assetReference.hasValue)
            loadRequests[index1] = assetReference.LoadFromAssetBundleAsync<V>(bundleName);
          ++index1;
        }
        yield return (object) EnumeratorUtility.ParallelRecursiveImmediateExecution((IEnumerator[]) loadRequests);
        int index2 = 0;
        foreach (K key in ((Dictionary<K, AssetReference>) effects).Keys)
        {
          AssetLoadRequest<V> assetLoadRequest = loadRequests[index2];
          ++index2;
          if (assetLoadRequest != null)
          {
            if ((int) assetLoadRequest.error != 0)
              Log.Error(string.Format("Failed to load effect for '{0}': {1}", (object) key, (object) assetLoadRequest.error), 608, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightSpellEffectFactory.cs");
            else
              effectCache.Add(key, assetLoadRequest.asset);
          }
        }
      }
    }

    private static IEnumerator LoadEffectsResources<K, V>(Dictionary<K, V> effectCache) where V : ScriptableEffect
    {
      yield return (object) ScriptableEffect.LoadAll<V>((ICollection<V>) effectCache.Values);
    }

    private static void UnloadCache<K, V>(Dictionary<K, V> effectCache) where V : ScriptableEffect
    {
      foreach (V v in effectCache.Values)
      {
        if ((UnityEngine.Object) null != (UnityEngine.Object) v)
          v.Unload();
      }
      effectCache.Clear();
    }

    private struct SpellEffectOverrideData
    {
      private readonly ISpellEffectOverrideProvider m_spellDefinition;
      private readonly int m_eventId;

      public SpellEffectOverrideData(ISpellEffectOverrideProvider spellDefinition, int eventId)
      {
        this.m_spellDefinition = spellDefinition;
        this.m_eventId = eventId;
      }

      public bool TryGetSpellEffectOverride(
        SpellEffectKey key,
        int parentEventId,
        out SpellEffect spellEffect)
      {
        if (this.m_spellDefinition != null && parentEventId == this.m_eventId)
          return this.m_spellDefinition.TryGetSpellEffectOverride(key, out spellEffect);
        spellEffect = (SpellEffect) null;
        return false;
      }
    }
  }
}
