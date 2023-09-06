// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightObjectFactory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Fight
{
  public class FightObjectFactory : ScriptableObject
  {
    private static FightObjectFactory s_instance;
    [Header("Character Prefabs")]
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_heroCharacterPrefab;
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_companionCharacterPrefab;
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_summoningCharacterPrefab;
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_objectMechanismPrefab;
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_floorMechanismPrefab;
    [Header("Character Effects")]
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_animatedObjectEffectPrefab;
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_timelineAssetEffectPrefab;
    [Header("Feedbacks")]
    [UsedImplicitly]
    [SerializeField]
    private GameObject m_valueChangedFeedbackPrefab;
    private static GameObjectPool s_companionCharacterPool;
    private static GameObjectPool s_summoningCharacterPool;
    private static GameObjectPool s_objectMechanismCharacterPool;
    private static GameObjectPool s_floorMechanismCharacterPool;
    private static GameObjectPool s_animatedObjectEffectPool;
    private static GameObjectPool s_timelineAssetEffectPool;
    private static GameObjectPool s_valueChangedFeedbackPool;
    private static readonly Dictionary<Transform, int> s_valueChangedFeedbackCountPerTransform = new Dictionary<Transform, int>();

    public static bool isReady { get; private set; }

    public static IEnumerator Load()
    {
      if (FightObjectFactory.isReady)
      {
        Log.Error("Load called while the fight object factory is already ready.", 61, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
      }
      else
      {
        AssetBundleLoadRequest bundleRequest = AssetManager.LoadAssetBundle("core/factories/fight_object_factory");
        while (!bundleRequest.isDone)
          yield return (object) null;
        if ((int) bundleRequest.error != 0)
        {
          Log.Error(string.Format("Error while loading bundle: {0} error={1}", (object) "core/factories/fight_object_factory", (object) bundleRequest.error), 74, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        }
        else
        {
          AllAssetsLoadRequest<FightObjectFactory> assetLoadRequest = AssetManager.LoadAllAssetsAsync<FightObjectFactory>("core/factories/fight_object_factory");
          while (!assetLoadRequest.isDone)
            yield return (object) null;
          if ((int) assetLoadRequest.error != 0)
          {
            Log.Error(string.Format("Error while loading asset: {0} error={1}", (object) nameof (FightObjectFactory), (object) assetLoadRequest.error), 85, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
          }
          else
          {
            FightObjectFactory.s_instance = assetLoadRequest.assets[0];
            FightObjectFactory.s_companionCharacterPool = new GameObjectPool(FightObjectFactory.s_instance.m_companionCharacterPrefab, 2);
            FightObjectFactory.s_summoningCharacterPool = new GameObjectPool(FightObjectFactory.s_instance.m_summoningCharacterPrefab, 2);
            FightObjectFactory.s_objectMechanismCharacterPool = new GameObjectPool(FightObjectFactory.s_instance.m_objectMechanismPrefab, 2);
            FightObjectFactory.s_floorMechanismCharacterPool = new GameObjectPool(FightObjectFactory.s_instance.m_floorMechanismPrefab, 2);
            FightObjectFactory.s_animatedObjectEffectPool = new GameObjectPool(FightObjectFactory.s_instance.m_animatedObjectEffectPrefab, 2);
            FightObjectFactory.s_timelineAssetEffectPool = new GameObjectPool(FightObjectFactory.s_instance.m_timelineAssetEffectPrefab, 4);
            FightObjectFactory.s_valueChangedFeedbackPool = new GameObjectPool(FightObjectFactory.s_instance.m_valueChangedFeedbackPrefab, 4);
            FightObjectFactory.isReady = true;
          }
        }
      }
    }

    public static IEnumerator Unload()
    {
      if (FightObjectFactory.isReady)
      {
        FightObjectFactory.isReady = false;
        FightObjectFactory.s_instance = (FightObjectFactory) null;
        FightObjectFactory.DisposePool(ref FightObjectFactory.s_companionCharacterPool);
        FightObjectFactory.DisposePool(ref FightObjectFactory.s_summoningCharacterPool);
        FightObjectFactory.DisposePool(ref FightObjectFactory.s_objectMechanismCharacterPool);
        FightObjectFactory.DisposePool(ref FightObjectFactory.s_floorMechanismCharacterPool);
        FightObjectFactory.DisposePool(ref FightObjectFactory.s_animatedObjectEffectPool);
        FightObjectFactory.DisposePool(ref FightObjectFactory.s_timelineAssetEffectPool);
        FightObjectFactory.DisposePool(ref FightObjectFactory.s_valueChangedFeedbackPool);
        FightObjectFactory.s_valueChangedFeedbackCountPerTransform.Clear();
        AssetBundleUnloadRequest unloadRequest = AssetManager.UnloadAssetBundle("core/factories/fight_object_factory");
        while (!unloadRequest.isDone)
          yield return (object) null;
      }
    }

    private static void DisposePool(ref GameObjectPool pool)
    {
      if (pool == null)
        return;
      pool.Dispose();
      pool = (GameObjectPool) null;
    }

    public static HeroCharacterObject CreateHeroCharacterObject(
      WeaponDefinition definition,
      int x,
      int y,
      Ankama.Cube.Data.Direction direction)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightObjectFactory.s_instance)
      {
        Log.Error("CreateHeroCharacterObject called while the factory is not ready.", 175, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (HeroCharacterObject) null;
      }
      CellObject cellObject;
      if (!FightMap.current.TryGetCellObject(x, y, out cellObject))
      {
        Log.Error(string.Format("{0} called with an invalid position {1}, {2}.", (object) nameof (CreateHeroCharacterObject), (object) x, (object) y), 184, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (HeroCharacterObject) null;
      }
      Transform transform = cellObject.transform;
      Vector3 position = transform.position;
      position.y += 0.5f;
      HeroCharacterObject component = UnityEngine.Object.Instantiate<GameObject>(FightObjectFactory.s_instance.m_heroCharacterPrefab, position, Quaternion.identity, transform).GetComponent<HeroCharacterObject>();
      component.InitializeDefinitionAndArea((IsoObjectDefinition) definition, x, y);
      component.SetCellObject(cellObject);
      component.direction = direction;
      return component;
    }

    public static CompanionCharacterObject CreateCompanionCharacterObject(
      CompanionDefinition definition,
      int x,
      int y,
      Ankama.Cube.Data.Direction direction)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightObjectFactory.s_instance)
      {
        Log.Error("CreateCompanionCharacterObject called while the factory is not ready.", 206, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (CompanionCharacterObject) null;
      }
      CellObject cellObject;
      if (!FightMap.current.TryGetCellObject(x, y, out cellObject))
      {
        Log.Error(string.Format("{0} called with an invalid position {1}, {2}.", (object) nameof (CreateCompanionCharacterObject), (object) x, (object) y), 215, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (CompanionCharacterObject) null;
      }
      Transform transform = cellObject.transform;
      Vector3 position = transform.position;
      position.y += 0.5f;
      CompanionCharacterObject component = FightObjectFactory.s_companionCharacterPool.Instantiate(position, Quaternion.identity, transform).GetComponent<CompanionCharacterObject>();
      component.InitializeDefinitionAndArea((IsoObjectDefinition) definition, x, y);
      component.SetCellObject(cellObject);
      component.direction = direction;
      return component;
    }

    public static void ReleaseCompanionCharacterObject([NotNull] CompanionCharacterObject instance)
    {
      if (FightObjectFactory.s_companionCharacterPool == null)
        return;
      FightObjectFactory.s_companionCharacterPool.Release(instance.gameObject);
    }

    public static SummoningCharacterObject CreateSummoningCharacterObject(
      SummoningDefinition definition,
      int x,
      int y,
      Ankama.Cube.Data.Direction direction)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightObjectFactory.s_instance)
      {
        Log.Error("CreateSummoningCharacterObject called while the factory is not ready.", 245, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (SummoningCharacterObject) null;
      }
      CellObject cellObject;
      if (!FightMap.current.TryGetCellObject(x, y, out cellObject))
      {
        Log.Error(string.Format("{0} called with an invalid position {1}, {2}.", (object) nameof (CreateSummoningCharacterObject), (object) x, (object) y), 254, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (SummoningCharacterObject) null;
      }
      Transform transform = cellObject.transform;
      Vector3 position = transform.position;
      position.y += 0.5f;
      SummoningCharacterObject component = FightObjectFactory.s_summoningCharacterPool.Instantiate(position, Quaternion.identity, transform).GetComponent<SummoningCharacterObject>();
      component.InitializeDefinitionAndArea((IsoObjectDefinition) definition, x, y);
      component.SetCellObject(cellObject);
      component.direction = direction;
      return component;
    }

    public static void ReleaseSummoningCharacterObject([NotNull] SummoningCharacterObject instance)
    {
      if (FightObjectFactory.s_summoningCharacterPool == null)
        return;
      FightObjectFactory.s_summoningCharacterPool.Release(instance.gameObject);
    }

    public static ObjectMechanismObject CreateObjectMechanismObject(
      ObjectMechanismDefinition definition,
      int x,
      int y)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightObjectFactory.s_instance)
      {
        Log.Error("CreateObjectMechanismObject called while the factory is not ready.", 284, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (ObjectMechanismObject) null;
      }
      CellObject cellObject;
      if (!FightMap.current.TryGetCellObject(x, y, out cellObject))
      {
        Log.Error(string.Format("{0} called with an invalid position {1}, {2}.", (object) nameof (CreateObjectMechanismObject), (object) x, (object) y), 293, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (ObjectMechanismObject) null;
      }
      Transform transform = cellObject.transform;
      Vector3 position = transform.position;
      position.y += 0.5f;
      ObjectMechanismObject component = FightObjectFactory.s_objectMechanismCharacterPool.Instantiate(position, Quaternion.identity, transform).GetComponent<ObjectMechanismObject>();
      component.InitializeDefinitionAndArea((IsoObjectDefinition) definition, x, y);
      component.SetCellObject(cellObject);
      return component;
    }

    public static void ReleaseObjectMechanismObject([NotNull] ObjectMechanismObject instance)
    {
      if (FightObjectFactory.s_objectMechanismCharacterPool == null)
        return;
      FightObjectFactory.s_objectMechanismCharacterPool.Release(instance.gameObject);
    }

    public static FloorMechanismObject CreateFloorMechanismObject(
      FloorMechanismDefinition definition,
      int x,
      int y)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightObjectFactory.s_instance)
      {
        Log.Error("CreateFloorMechanismObject called while the factory is not ready.", 322, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (FloorMechanismObject) null;
      }
      CellObject cellObject;
      if (!FightMap.current.TryGetCellObject(x, y, out cellObject))
      {
        Log.Error(string.Format("{0} called with an invalid position {1}, {2}.", (object) nameof (CreateFloorMechanismObject), (object) x, (object) y), 331, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (FloorMechanismObject) null;
      }
      Transform transform = cellObject.transform;
      Vector3 position = transform.position;
      position.y += 0.5f;
      FloorMechanismObject component = FightObjectFactory.s_floorMechanismCharacterPool.Instantiate(position, Quaternion.identity, transform).GetComponent<FloorMechanismObject>();
      component.InitializeDefinitionAndArea((IsoObjectDefinition) definition, x, y);
      component.SetCellObject(cellObject);
      return component;
    }

    public static void ReleaseFloorMechanismObject([NotNull] FloorMechanismObject instance)
    {
      if (FightObjectFactory.s_floorMechanismCharacterPool == null)
        return;
      FightObjectFactory.s_floorMechanismCharacterPool.Release(instance.gameObject);
    }

    public static Animator2D CreateAnimatedObjectEffectInstance(
      [NotNull] AnimatedObjectDefinition definition,
      string animationName,
      [NotNull] Transform parent)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) definition)
        throw new NullReferenceException();
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightObjectFactory.s_instance)
      {
        Log.Error("CreateAnimatedObjectEffectInstance called while the factory is not ready.", 367, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (Animator2D) null;
      }
      Animator2D component = FightObjectFactory.s_animatedObjectEffectPool.Instantiate(parent.position, Quaternion.identity, parent).GetComponent<Animator2D>();
      if (!string.IsNullOrEmpty(animationName))
        component.SetAnimation(animationName, false, false, true);
      else
        component.animationLoops = false;
      component.SetDefinition(definition);
      return component;
    }

    public static void DestroyAnimatedObjectEffectInstance([NotNull] Animator2D instance)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) FightObjectFactory.s_instance)
        FightObjectFactory.s_animatedObjectEffectPool.Release(instance.gameObject);
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) instance.gameObject);
    }

    public static PlayableDirector CreateTimelineAssetEffectInstance(
      [NotNull] TimelineAsset timelineAsset,
      [NotNull] Transform parent,
      Quaternion rotation,
      Vector3 scale,
      [CanBeNull] FightContext fightContext,
      [CanBeNull] ITimelineContextProvider contextProvider)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) timelineAsset)
        throw new NullReferenceException();
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightObjectFactory.s_instance)
      {
        Log.Error("CreateTimelineAssetEffectInstance called while the factory is not ready.", 412, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        return (PlayableDirector) null;
      }
      GameObject gameObject = FightObjectFactory.s_timelineAssetEffectPool.Instantiate(parent.position, rotation, parent);
      Vector3 localScale = gameObject.transform.localScale;
      localScale.Scale(scale);
      gameObject.transform.localScale = localScale;
      PlayableDirector component = gameObject.GetComponent<PlayableDirector>();
      if ((UnityEngine.Object) null != (UnityEngine.Object) fightContext)
        TimelineContextUtility.SetFightContext(component, fightContext);
      if (contextProvider != null)
        TimelineContextUtility.SetContextProvider(component, contextProvider);
      component.Play((PlayableAsset) timelineAsset, DirectorWrapMode.None);
      return component;
    }

    public static void DestroyTimelineAssetEffectInstance(
      PlayableDirector instance,
      bool clearFightContext)
    {
      if (clearFightContext)
        TimelineContextUtility.ClearFightContext(instance);
      TimelineContextUtility.ClearContextProvider(instance);
      instance.playableAsset = (PlayableAsset) null;
      if ((UnityEngine.Object) null != (UnityEngine.Object) FightObjectFactory.s_instance)
        FightObjectFactory.s_timelineAssetEffectPool.Release(instance.gameObject);
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) instance.gameObject);
    }

    public static ValueChangedFeedback CreateValueChangedFeedback(
      Transform parentTransform,
      out int instanceCountInTransform)
    {
      if (FightObjectFactory.s_valueChangedFeedbackPool == null)
      {
        Log.Error("CreateValueChangedFeedback called while the factory is not ready.", 465, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightObjectFactory.cs");
        instanceCountInTransform = 0;
        return (ValueChangedFeedback) null;
      }
      CameraHandler current = CameraHandler.current;
      Vector3 position;
      Quaternion rotation;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
      {
        position = parentTransform.position;
        rotation = Quaternion.LookRotation(current.transform.forward, Vector3.up);
      }
      else
      {
        Transform transform = FightObjectFactory.s_valueChangedFeedbackPool.prefab.transform;
        position = parentTransform.position + transform.localPosition;
        rotation = transform.localRotation;
      }
      FightObjectFactory.s_valueChangedFeedbackCountPerTransform.TryGetValue(parentTransform, out instanceCountInTransform);
      FightObjectFactory.s_valueChangedFeedbackCountPerTransform[parentTransform] = instanceCountInTransform + 1;
      return FightObjectFactory.s_valueChangedFeedbackPool.Instantiate(position, rotation, parentTransform).GetComponent<ValueChangedFeedback>();
    }

    public static void ReleaseValueChangedFeedback(GameObject instance)
    {
      if (FightObjectFactory.s_valueChangedFeedbackPool == null)
        return;
      Transform parent = instance.transform.parent;
      int num;
      if ((UnityEngine.Object) null != (UnityEngine.Object) parent && FightObjectFactory.s_valueChangedFeedbackCountPerTransform.TryGetValue(parent, out num))
        FightObjectFactory.s_valueChangedFeedbackCountPerTransform[parent] = num - 1;
      FightObjectFactory.s_valueChangedFeedbackPool.Release(instance);
    }
  }
}
