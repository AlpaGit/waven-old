// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetReferences.AssetReference
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using Ankama.AssetManagement.AssetBundles;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.AssetManagement.AssetReferences
{
  [PublicAPI]
  [Serializable]
  public struct AssetReference
  {
    [UsedImplicitly]
    [SerializeField]
    internal string assetGuid;

    [PublicAPI]
    [CanBeNull]
    public string value => this.assetGuid;

    [PublicAPI]
    public bool hasValue => !string.IsNullOrEmpty(this.assetGuid);

    [PublicAPI]
    [CanBeNull]
    public T LoadFromResources<T>() where T : UnityEngine.Object
    {
      if (string.IsNullOrEmpty(this.assetGuid))
        return default (T);
      Dictionary<string, string> assetReferenceMap = AssetManager.assetReferenceMap;
      if (assetReferenceMap == null)
      {
        Debug.LogWarning((object) "[AssetManager] Tried to load an asset reference from resources but the AssetManager.SetReferenceMap was not called.");
        return default (T);
      }
      string path;
      if (assetReferenceMap.TryGetValue(this.assetGuid, out path))
        return Resources.Load<T>(path);
      Debug.LogWarning((object) ("[AssetManager] Could not find asset reference with GUID '" + this.assetGuid + "' is the asset reference map."));
      return default (T);
    }

    [PublicAPI]
    [NotNull]
    public AssetReferenceRequest<T> LoadFromResourcesAsync<T>() where T : UnityEngine.Object
    {
      if (string.IsNullOrEmpty(this.assetGuid))
        return AssetReferenceRequest<T>.empty;
      Dictionary<string, string> assetReferenceMap = AssetManager.assetReferenceMap;
      if (assetReferenceMap == null)
      {
        Debug.LogWarning((object) "[AssetManager] Tried to load an asset reference from resources but the AssetManager.SetReferenceMap was not called.");
        return AssetReferenceRequest<T>.empty;
      }
      string assetPath;
      if (assetReferenceMap.TryGetValue(this.assetGuid, out assetPath))
        return new AssetReferenceRequest<T>(assetPath);
      Debug.LogWarning((object) ("[AssetManager] Could not find asset reference with GUID '" + this.assetGuid + "' is the asset reference map."));
      return AssetReferenceRequest<T>.empty;
    }

    [PublicAPI]
    [CanBeNull]
    public T LoadFromAssetBundle<T>(string assetBundleName) where T : UnityEngine.Object
    {
      if (string.IsNullOrEmpty(this.assetGuid))
        return default (T);
      if (AssetManager.assetBundleOverrideAssetsNames)
        return AssetManager.LoadAsset<T>(this.assetGuid, assetBundleName);
      Dictionary<string, string> assetReferenceMap = AssetManager.assetReferenceMap;
      if (assetReferenceMap == null)
      {
        Debug.LogWarning((object) "[AssetManager] Tried to load an asset reference from an asset bundle but the AssetManager.SetReferenceMap was not called.");
        return default (T);
      }
      string assetName;
      if (assetReferenceMap.TryGetValue(this.assetGuid, out assetName))
        return AssetManager.LoadAsset<T>(assetName, assetBundleName);
      Debug.LogWarning((object) ("[AssetManager] Could not find asset reference with GUID '" + this.assetGuid + "' is the asset reference map."));
      return default (T);
    }

    [PublicAPI]
    [NotNull]
    public AssetLoadRequest<T> LoadFromAssetBundleAsync<T>(string assetBundleName) where T : UnityEngine.Object
    {
      if (string.IsNullOrEmpty(this.assetGuid))
        return AssetLoadRequest<T>.empty;
      if (AssetManager.assetBundleOverrideAssetsNames)
        return AssetManager.LoadAssetAsync<T>(this.assetGuid, assetBundleName);
      Dictionary<string, string> assetReferenceMap = AssetManager.assetReferenceMap;
      if (assetReferenceMap == null)
      {
        Debug.LogWarning((object) "[AssetManager] Tried to load an asset reference from an asset bundle but the AssetManager.SetReferenceMap was not called.");
        return AssetLoadRequest<T>.empty;
      }
      string assetName;
      if (assetReferenceMap.TryGetValue(this.assetGuid, out assetName))
        return AssetManager.LoadAssetAsync<T>(assetName, assetBundleName);
      Debug.LogWarning((object) ("[AssetManager] Could not find asset reference with GUID '" + this.assetGuid + "' is the asset reference map."));
      return AssetLoadRequest<T>.empty;
    }
  }
}
