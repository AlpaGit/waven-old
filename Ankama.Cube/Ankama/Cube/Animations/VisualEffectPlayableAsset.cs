// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.VisualEffectPlayableAsset
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  [Serializable]
  public sealed class VisualEffectPlayableAsset : 
    PlayableAsset,
    ITimelineClipAsset,
    ITimelineResourcesProvider
  {
    [SerializeField]
    private AssetReference m_prefabReference;
    [SerializeField]
    private string m_assetBundleName;
    [SerializeField]
    private VisualEffectPlayableAsset.StopMode m_stopMode = VisualEffectPlayableAsset.StopMode.Stop;
    [SerializeField]
    private VisualEffectPlayableAsset.ParentingMode m_parentingMode = VisualEffectPlayableAsset.ParentingMode.Parent;
    [SerializeField]
    private VisualEffectPlayableAsset.OrientationMethod m_orientationMethod;
    [SerializeField]
    private Vector3 m_offset = Vector3.zero;
    [NonSerialized]
    private bool m_loadedAssetBundle;
    [NonSerialized]
    private VisualEffect m_prefab;

    public IEnumerator LoadResources()
    {
      if (this.m_prefabReference.hasValue)
      {
        GameObject asset;
        if (string.IsNullOrEmpty(this.m_assetBundleName))
        {
          AssetReferenceRequest<GameObject> assetReferenceRequest = this.m_prefabReference.LoadFromResourcesAsync<GameObject>();
          while (!assetReferenceRequest.isDone)
            yield return (object) null;
          asset = assetReferenceRequest.asset;
          assetReferenceRequest = (AssetReferenceRequest<GameObject>) null;
        }
        else
        {
          AssetBundleLoadRequest bundleRequest = AssetManager.LoadAssetBundle(this.m_assetBundleName);
          while (!bundleRequest.isDone)
            yield return (object) null;
          if ((int) bundleRequest.error != 0)
          {
            Log.Error(string.Format("Could not load bundle named '{0}': {1}", (object) this.m_assetBundleName, (object) bundleRequest.error), 99, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\VisualEffectPlayableAsset.cs");
            yield break;
          }
          else
          {
            this.m_loadedAssetBundle = true;
            AssetLoadRequest<GameObject> assetReferenceRequest = this.m_prefabReference.LoadFromAssetBundleAsync<GameObject>(this.m_assetBundleName);
            while (!assetReferenceRequest.isDone)
              yield return (object) null;
            if ((int) assetReferenceRequest.error != 0)
            {
              Log.Error(string.Format("Could not load requested asset ({0}) from bundle named '{1}': {2}", (object) this.m_prefabReference.value, (object) this.m_assetBundleName, (object) assetReferenceRequest.error), 113, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\VisualEffectPlayableAsset.cs");
              yield break;
            }
            else
            {
              asset = assetReferenceRequest.asset;
              bundleRequest = (AssetBundleLoadRequest) null;
              assetReferenceRequest = (AssetLoadRequest<GameObject>) null;
            }
          }
        }
        VisualEffect component = asset.GetComponent<VisualEffect>();
        if ((UnityEngine.Object) null == (UnityEngine.Object) component)
        {
          Log.Error("Could not use prefab because it doesn't have a VisualEffect component.", 123, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\VisualEffectPlayableAsset.cs");
        }
        else
        {
          VisualEffectFactory.PreparePool(asset);
          this.m_prefab = component;
        }
      }
    }

    public void UnloadResources()
    {
      if (this.m_loadedAssetBundle)
      {
        AssetManager.UnloadAssetBundle(this.m_assetBundleName);
        this.m_loadedAssetBundle = false;
      }
      this.m_prefab = (VisualEffect) null;
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      if (!this.m_prefabReference.hasValue)
        return Playable.Null;
      VisualEffectContext context = TimelineContextUtility.GetContext<VisualEffectContext>(graph);
      VisualEffectPlayableBehaviour template = new VisualEffectPlayableBehaviour(owner, context, this.m_prefab, this.m_stopMode, this.m_parentingMode, this.m_orientationMethod, this.m_offset);
      return (Playable) ScriptPlayable<VisualEffectPlayableBehaviour>.Create(graph, template);
    }

    public ClipCaps clipCaps { get; }

    public enum StopMode
    {
      None,
      Stop,
      Destroy,
    }

    public enum ParentingMode
    {
      Owner,
      Parent,
      ContextOwner,
      ContextParent,
      World,
    }

    public enum OrientationMethod
    {
      None,
      Context,
      Director,
      Transform,
    }
  }
}
