// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Animation
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using Ankama.Animations.Management;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Animations
{
  [Serializable]
  internal sealed class Animation
  {
    [UsedImplicitly]
    [SerializeField]
    public string name;
    [UsedImplicitly]
    [SerializeField]
    public AssetReference data;
    [NonSerialized]
    public AnimationInstance instance;

    public Animation(string name) => this.name = name;

    public bool isReady => this.instance != null;

    public void Load(string assetBundleName)
    {
      if (this.instance != null)
        ++this.instance.referenceCount;
      else if (!this.data.hasValue)
      {
        Debug.LogWarning((object) ("[Animator2D] Tried to load animation named '" + this.name + "' but the asset reference has no value."));
      }
      else
      {
        if (AnimationManager.TryGetAnimationInstance(this.data.value, out this.instance))
          return;
        TextAsset assetToUnload;
        if (string.IsNullOrEmpty(assetBundleName))
        {
          assetToUnload = this.data.LoadFromResources<TextAsset>();
          if ((UnityEngine.Object) null == (UnityEngine.Object) assetToUnload)
          {
            Debug.LogWarning((object) ("[Animator2D] Could not load animation named '" + this.name + "' from resources."));
            return;
          }
        }
        else
        {
          assetToUnload = this.data.LoadFromAssetBundle<TextAsset>(assetBundleName);
          if ((UnityEngine.Object) null == (UnityEngine.Object) assetToUnload)
          {
            Debug.LogWarning((object) ("[Animator2D] Could not load animation named '" + this.name + "' from asset bundle named '" + assetBundleName + "'."));
            return;
          }
        }
        this.instance = AnimationManager.CreateAnimationInstance(this.data.value, assetToUnload.bytes);
        Resources.UnloadAsset((UnityEngine.Object) assetToUnload);
      }
    }

    public IEnumerator LoadAsync(string assetBundleName)
    {
      if (this.instance != null)
        ++this.instance.referenceCount;
      else if (!this.data.hasValue)
        Debug.LogWarning((object) ("[Animator2D] Tried to load animation named '" + this.name + "' but the asset reference has no value."));
      else if (!AnimationManager.TryGetAnimationInstance(this.data.value, out this.instance))
      {
        TextAsset asset;
        if (string.IsNullOrEmpty(assetBundleName))
        {
          AssetReferenceRequest<TextAsset> request = this.data.LoadFromResourcesAsync<TextAsset>();
          while (!request.isDone)
            yield return (object) null;
          asset = request.asset;
          if ((UnityEngine.Object) null == (UnityEngine.Object) asset)
          {
            Debug.LogWarning((object) ("[Animator2D] Could not load animation named '" + this.name + "' from resources."));
            yield break;
          }
          else
            request = (AssetReferenceRequest<TextAsset>) null;
        }
        else
        {
          AssetLoadRequest<TextAsset> request = this.data.LoadFromAssetBundleAsync<TextAsset>(assetBundleName);
          while (!request.isDone)
            yield return (object) null;
          asset = request.asset;
          if ((UnityEngine.Object) null == (UnityEngine.Object) asset)
          {
            Debug.LogWarning((object) ("[Animator2D] Could not load animation named '" + this.name + "' from asset bundle named '" + assetBundleName + "'."));
            yield break;
          }
          else
            request = (AssetLoadRequest<TextAsset>) null;
        }
        this.instance = AnimationManager.CreateAnimationInstance(this.data.value, asset.bytes);
      }
    }

    public void Unload()
    {
      if (this.instance == null || !AnimationManager.ReleaseAnimationInstance(this.data.value))
        return;
      this.instance = (AnimationInstance) null;
    }

    [Flags]
    public enum NodeState : byte
    {
      None = 0,
      SpriteIndex = 1,
      SpriteOpacity = 2,
      SpriteColorMultiply = 4,
      SpriteColorAdditive = 8,
      Matrix = 16, // 0x10
      CustomisationIndex = 32, // 0x20
    }
  }
}
