// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetManager
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.AssetManagement.StreamingAssets;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

namespace Ankama.AssetManagement
{
  [PublicAPI]
  public static class AssetManager
  {
    private static AssetBundleManifestLoadRequest s_manifestLoadRequest;
    private static AssetManagerCallbackSource s_callbackSource;
    private static bool s_bundleRequestsCoroutineRunning;
    private static bool s_streamingAssetRequestsCoroutineRunning;
    internal static Dictionary<string, string> assetReferenceMap;
    internal static Dictionary<string, AssetBundleLoadInfo> assetBundleLoadInfos;
    internal static AssetBundleVariantCollection assetBundleVariantCollections;
    internal static Dictionary<string, StreamingAssetIdentifier> patchedStreamingAssets;
    internal static readonly HashSet<string> activeVariants = new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal);
    internal static readonly Dictionary<string, SpriteAtlas> registeredSpriteAtlases = new Dictionary<string, SpriteAtlas>((IEqualityComparer<string>) StringComparer.Ordinal);
    internal static readonly HashSet<AssetBundleLoadInfo> assetBundleRequests = new HashSet<AssetBundleLoadInfo>();
    internal static readonly Dictionary<AssetLoadRequestIdentifier, AssetLoadAsyncRequest> assetLoadingRequests = new Dictionary<AssetLoadRequestIdentifier, AssetLoadAsyncRequest>((IEqualityComparer<AssetLoadRequestIdentifier>) AssetLoadRequestIdentifier.equalityComparer);
    internal static readonly List<StreamingAssetLoadRequest> streamingAssetLoadingRequests = new List<StreamingAssetLoadRequest>();
    private static readonly WaitForEndOfFrame s_waitForEndOfFrame = new WaitForEndOfFrame();
    private static readonly List<AssetBundleLoadInfo> s_completedAssetBundleRequests = new List<AssetBundleLoadInfo>(1);
    private static readonly List<AssetLoadRequestIdentifier> s_completedAssetLoadingRequests = new List<AssetLoadRequestIdentifier>(1);

    [PublicAPI]
    public static string assetBundleServerURL { get; private set; }

    [PublicAPI]
    public static string patchedStreamingAssetsServerURL { get; private set; }

    [PublicAPI]
    public static bool assetBundleOverrideAssetsNames { get; private set; }

    [PublicAPI]
    public static bool isInitialized => (UnityEngine.Object) null != (UnityEngine.Object) AssetManager.s_callbackSource;

    [PublicAPI]
    public static bool isReady => AssetManager.assetBundleLoadInfos != null;

    [PublicAPI]
    [NotNull]
    public static Coroutine Bootstrap(
      AssetBundleSource source,
      string serverURL = "",
      bool overrideAssetNames = true)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) AssetManager.s_callbackSource)
        return AssetManager.s_callbackSource.StartCoroutine(AssetManager.BootstrapReuseRoutine());
      AssetManager.Initialize();
      if (string.IsNullOrWhiteSpace(serverURL))
        serverURL = string.Empty;
      AssetManager.SetAssetBundlesConfiguration(serverURL, overrideAssetNames);
      AssetManager.SetAssetReferenceMap(Resources.Load<AssetReferenceMap>("AssetReferenceMap"));
      return AssetManager.s_callbackSource.StartCoroutine(AssetManager.BootstrapRoutine(source));
    }

    [PublicAPI]
    public static void Initialize(bool initializeStateManager = true)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) AssetManager.s_callbackSource)
        return;
      SpriteAtlasManager.atlasRequested += new Action<string, Action<SpriteAtlas>>(AssetManager.OnSpriteAtlasRequested);
      if (initializeStateManager)
        StateManager.Initialize();
      AssetManager.s_callbackSource = new GameObject("AssetManagerCallbackSource", new System.Type[1]
      {
        typeof (AssetManagerCallbackSource)
      }).GetComponent<AssetManagerCallbackSource>();
    }

    [PublicAPI]
    public static void Release()
    {
      SpriteAtlasManager.atlasRequested -= new Action<string, Action<SpriteAtlas>>(AssetManager.OnSpriteAtlasRequested);
      if (AssetManager.s_manifestLoadRequest != null)
      {
        AssetManager.s_manifestLoadRequest.Abort();
        AssetManager.s_manifestLoadRequest = (AssetBundleManifestLoadRequest) null;
      }
      if ((UnityEngine.Object) null != (UnityEngine.Object) AssetManager.s_callbackSource)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) AssetManager.s_callbackSource.gameObject);
        AssetManager.s_callbackSource = (AssetManagerCallbackSource) null;
      }
      AssetManager.s_bundleRequestsCoroutineRunning = false;
      AssetManager.s_streamingAssetRequestsCoroutineRunning = false;
      if (AssetManager.assetBundleLoadInfos != null)
      {
        foreach (AssetBundleLoadInfo assetBundleLoadInfo in AssetManager.assetBundleLoadInfos.Values)
        {
          UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
          if ((UnityEngine.Object) null != (UnityEngine.Object) assetBundle)
          {
            assetBundle.Unload(true);
            assetBundleLoadInfo.assetBundle = (UnityEngine.AssetBundle) null;
          }
          assetBundleLoadInfo.referenceCount = 0;
          assetBundleLoadInfo.dependencyReferenceCount = 0;
        }
        AssetManager.assetBundleLoadInfos = (Dictionary<string, AssetBundleLoadInfo>) null;
      }
      AssetManager.assetReferenceMap = (Dictionary<string, string>) null;
      AssetManager.assetBundleVariantCollections = (AssetBundleVariantCollection) null;
      AssetManager.assetBundleLoadInfos = (Dictionary<string, AssetBundleLoadInfo>) null;
      AssetManager.patchedStreamingAssets = (Dictionary<string, StreamingAssetIdentifier>) null;
      AssetManager.activeVariants.Clear();
      AssetManager.registeredSpriteAtlases.Clear();
      AssetManager.assetBundleRequests.Clear();
      AssetManager.assetLoadingRequests.Clear();
      AssetManager.streamingAssetLoadingRequests.Clear();
      AssetManager.s_completedAssetBundleRequests.Clear();
      AssetManager.s_completedAssetLoadingRequests.Clear();
    }

    [PublicAPI]
    public static void SetAssetReferenceMap(AssetReferenceMap asset)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) asset)
      {
        AssetManager.assetReferenceMap = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.Ordinal);
      }
      else
      {
        Dictionary<string, string> dictionary = new Dictionary<string, string>(asset.dictionary.Count, (IEqualityComparer<string>) StringComparer.Ordinal);
        foreach (KeyValuePair<string, string> keyValuePair in asset.dictionary)
        {
          string key = keyValuePair.Key;
          string str1 = keyValuePair.Value;
          int num1 = str1.LastIndexOf("/Resources/", StringComparison.OrdinalIgnoreCase);
          if (num1 < 0)
          {
            dictionary.Add(key, str1);
          }
          else
          {
            int startIndex1 = num1 + "/Resources/".Length;
            int startIndex2 = str1.Length - 2;
            int num2 = str1.LastIndexOf('.', startIndex2, startIndex2 - startIndex1);
            int length = num2 < 0 ? str1.Length - startIndex1 : num2 - startIndex1;
            string str2 = str1.Substring(startIndex1, length);
            dictionary.Add(key, str2);
          }
        }
        AssetManager.assetReferenceMap = dictionary;
      }
    }

    [PublicAPI]
    public static void SetAssetBundlesConfiguration([NotNull] string serverURL, bool overrideAssetNames = false)
    {
      AssetManager.assetBundleServerURL = serverURL != null ? AssetBundleUtility.EnsureValidAssetBundleServerURL(serverURL) : throw new ArgumentNullException(nameof (serverURL));
      AssetManager.patchedStreamingAssetsServerURL = AssetBundleUtility.EnsureValidPatchedStreamingAssetsServerURL(serverURL);
      AssetManager.assetBundleOverrideAssetsNames = overrideAssetNames;
    }

    [PublicAPI]
    [NotNull]
    public static AssetBundleManifestLoadRequest LoadAssetBundleManifest(
      AssetBundleSource source,
      bool automaticUnload = true)
    {
      if (AssetManager.assetBundleLoadInfos != null)
        return new AssetBundleManifestLoadRequest();
      if (AssetManager.s_manifestLoadRequest != null)
        return AssetManager.s_manifestLoadRequest;
      AssetBundleManifestLoadRequest manifestLoadRequest;
      AssetManager.s_manifestLoadRequest = manifestLoadRequest = new AssetBundleManifestLoadRequest(source);
      AssetManager.s_callbackSource.StartCoroutine(AssetManager.LoadAssetBundleManifestRoutine(automaticUnload));
      return manifestLoadRequest;
    }

    [PublicAPI]
    public static void UnloadAssetBundleManifest()
    {
      if (AssetManager.s_manifestLoadRequest != null)
      {
        AssetManager.s_manifestLoadRequest.Abort();
        AssetManager.s_manifestLoadRequest = (AssetBundleManifestLoadRequest) null;
      }
      AssetManager.s_bundleRequestsCoroutineRunning = false;
      AssetManager.s_streamingAssetRequestsCoroutineRunning = false;
      if (AssetManager.assetBundleLoadInfos != null)
      {
        foreach (AssetBundleLoadInfo assetBundleLoadInfo in AssetManager.assetBundleLoadInfos.Values)
        {
          UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
          if ((UnityEngine.Object) null != (UnityEngine.Object) assetBundle)
          {
            assetBundle.Unload(true);
            assetBundleLoadInfo.assetBundle = (UnityEngine.AssetBundle) null;
          }
          assetBundleLoadInfo.referenceCount = 0;
          assetBundleLoadInfo.dependencyReferenceCount = 0;
        }
        AssetManager.assetBundleLoadInfos = (Dictionary<string, AssetBundleLoadInfo>) null;
      }
      AssetManager.assetBundleVariantCollections = (AssetBundleVariantCollection) null;
      AssetManager.assetBundleLoadInfos = (Dictionary<string, AssetBundleLoadInfo>) null;
      AssetManager.patchedStreamingAssets = (Dictionary<string, StreamingAssetIdentifier>) null;
      AssetManager.activeVariants.Clear();
      AssetManager.registeredSpriteAtlases.Clear();
      AssetManager.assetBundleRequests.Clear();
      AssetManager.assetLoadingRequests.Clear();
      AssetManager.streamingAssetLoadingRequests.Clear();
      AssetManager.s_completedAssetBundleRequests.Clear();
      AssetManager.s_completedAssetLoadingRequests.Clear();
    }

    [PublicAPI]
    public static bool AddActiveVariant([NotNull] string variant)
    {
      if (variant == null)
        throw new ArgumentNullException(nameof (variant));
      if (AssetManager.assetBundleVariantCollections == null)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot add variant '" + variant + "' because the manifests data were not loaded."));
        return false;
      }
      if (!AssetManager.assetBundleVariantCollections.VariantExists(variant))
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot add variant '" + variant + "' because it does not exist."));
        return false;
      }
      if (AssetManager.activeVariants.Contains(variant))
      {
        UnityEngine.Debug.LogWarning((object) ("[AssetManager] Tried to add variant '" + variant + "' to the list of active variants but it was already present."));
        return false;
      }
      if (AssetManager.assetBundleVariantCollections.VariantConflicts(variant, AssetManager.activeVariants))
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot add variant '" + variant + "' because it conflicts with an other active variant."));
        return false;
      }
      AssetManager.activeVariants.Add(variant);
      return true;
    }

    [PublicAPI]
    public static bool IsVariantActive([NotNull] string variant) => variant != null ? AssetManager.activeVariants.Contains(variant) : throw new ArgumentNullException(nameof (variant));

    [PublicAPI]
    public static bool RemoveActiveVariant([NotNull] string variant)
    {
      if (variant == null)
        throw new ArgumentNullException(nameof (variant));
      if (AssetManager.assetBundleVariantCollections == null)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot remove variant '" + variant + "' because the manifests data were not loaded."));
        return false;
      }
      if (!AssetManager.activeVariants.Contains(variant))
      {
        UnityEngine.Debug.LogWarning((object) ("[AssetManager] Tried to remove variant '" + variant + "' from the list of active variants but it was not present."));
        return false;
      }
      foreach (AssetBundleLoadInfo assetBundleLoadInfo in AssetManager.assetBundleLoadInfos.Values)
      {
        if (assetBundleLoadInfo.hasVariants && (assetBundleLoadInfo.isAssetBundleLoaded || assetBundleLoadInfo.isAssetBundleLoading))
        {
          AssetBundleFileInfo[] files = assetBundleLoadInfo.files;
          int length = files.Length;
          for (int index = 0; index < length; ++index)
          {
            if (files[index].variant.Equals(variant))
            {
              UnityEngine.Debug.LogError((object) ("[AssetManager] Tried to remove variant '" + variant + "' from the list of active variants but at least one bundle using it is loaded."));
              return false;
            }
          }
        }
      }
      AssetManager.activeVariants.Remove(variant);
      return true;
    }

    [PublicAPI]
    [NotNull]
    public static AssetBundleLoadRequest LoadAssetBundle([NotNull] string bundleName)
    {
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        string errorMessage = "[AssetManager] Cannot load bundle named '" + bundleName + "' because the manifests data were not loaded.";
        return new AssetBundleLoadRequest(bundleName, 60, errorMessage);
      }
      AssetBundleLoadInfo loadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out loadInfo))
      {
        string errorMessage = "[AssetManager] Could not find a bundle named '" + bundleName + "'.";
        return new AssetBundleLoadRequest(bundleName, 10, errorMessage);
      }
      AssetBundleFileInfo fileInfo = AssetManager.MapVariant(loadInfo);
      ++loadInfo.referenceCount;
      AssetBundleLoadInfo[] dependencies = fileInfo.dependencies;
      int length = dependencies.Length;
      for (int index = 0; index < length; ++index)
        AssetManager.LoadDependencyAssetBundle(dependencies[index]);
      AssetBundleLoadRequest bundleLoadRequest = loadInfo.loadRequest;
      if (bundleLoadRequest != null)
      {
        loadInfo.error = (AssetManagerError) 0;
        bundleLoadRequest.Restart();
      }
      else
      {
        bundleLoadRequest = new AssetBundleLoadRequest(fileInfo, loadInfo);
        loadInfo.loadRequest = bundleLoadRequest;
      }
      if (bundleLoadRequest.Update())
        loadInfo.error = bundleLoadRequest.error;
      else
        AssetManager.RegisterAssetBundleRequest(loadInfo);
      return bundleLoadRequest;
    }

    [PublicAPI]
    [NotNull]
    public static AssetBundleUnloadRequest UnloadAssetBundle([NotNull] string bundleName)
    {
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        string errorMessage = "[AssetManager] Cannot unload bundle named '" + bundleName + "' because the manifests data were not loaded.";
        return new AssetBundleUnloadRequest(bundleName, 60, errorMessage);
      }
      AssetBundleLoadInfo loadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out loadInfo))
      {
        string errorMessage = "[AssetManager] Could not find a bundle named '" + bundleName + "'.";
        return new AssetBundleUnloadRequest(bundleName, 10, errorMessage);
      }
      if (loadInfo.referenceCount == 0)
      {
        string errorMessage = "[AssetManager] Tried to unload bundle bundle named '" + bundleName + "' but it is not loaded or is no longer loaded.";
        return new AssetBundleUnloadRequest(bundleName, 60, errorMessage);
      }
      AssetBundleFileInfo fileInfo = AssetManager.MapVariant(loadInfo);
      --loadInfo.referenceCount;
      AssetBundleLoadInfo[] dependencies = fileInfo.dependencies;
      int length = dependencies.Length;
      for (int index = 0; index < length; ++index)
        AssetManager.UnloadDependencyAssetBundle(dependencies[index]);
      AssetBundleLoadRequest loadRequest = loadInfo.loadRequest;
      if (loadRequest != null && !loadRequest.Cancel())
        AssetManager.RegisterAssetBundleRequest(loadInfo);
      AssetBundleUnloadRequest bundleUnloadRequest = loadInfo.unloadRequest;
      if (bundleUnloadRequest != null)
      {
        bundleUnloadRequest.checkDependencies = true;
        bundleUnloadRequest.Restart();
      }
      else
      {
        bundleUnloadRequest = new AssetBundleUnloadRequest(fileInfo, loadInfo, true);
        loadInfo.unloadRequest = bundleUnloadRequest;
      }
      if (!bundleUnloadRequest.Update())
        AssetManager.RegisterAssetBundleRequest(loadInfo);
      return bundleUnloadRequest;
    }

    [PublicAPI]
    [NotNull]
    public static IEnumerable<string> EnumerateAssetBundleNames()
    {
      if (AssetManager.assetBundleLoadInfos == null)
      {
        UnityEngine.Debug.LogError((object) "[AssetManager] Cannot get asset bundle enumerator because the manifests data were not loaded.");
      }
      else
      {
        foreach (string key in AssetManager.assetBundleLoadInfos.Keys)
          yield return key;
      }
    }

    [PublicAPI]
    [NotNull]
    public static IEnumerable<CachedAssetBundle> EnumerateCachedAssetBundles()
    {
      if (AssetManager.assetBundleLoadInfos == null)
      {
        UnityEngine.Debug.LogError((object) "[AssetManager] Cannot get cached asset bundles because the manifests data were not loaded.");
      }
      else
      {
        foreach (KeyValuePair<string, AssetBundleLoadInfo> assetBundleLoadInfo in AssetManager.assetBundleLoadInfos)
        {
          AssetBundleLoadInfo loadInfo = assetBundleLoadInfo.Value;
          AssetBundleFileInfo fileInfo;
          if (AssetManager.TryMapVariant(loadInfo, out fileInfo))
          {
            if (fileInfo.source == AssetBundleSource.Web)
              yield return new CachedAssetBundle(fileInfo.fileName, fileInfo.hash);
          }
          else
          {
            AssetBundleFileInfo[] files = loadInfo.files;
            int fileCount = files.Length;
            for (int i = 0; i < fileCount; ++i)
            {
              AssetBundleFileInfo assetBundleFileInfo = files[i];
              if (assetBundleFileInfo.source == AssetBundleSource.Web)
                yield return new CachedAssetBundle(assetBundleFileInfo.fileName, assetBundleFileInfo.hash);
            }
            files = (AssetBundleFileInfo[]) null;
          }
        }
      }
    }

    [PublicAPI]
    public static AssetBundleState GetAssetBundleState([NotNull] string bundleName)
    {
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot get asset bundle state for bundle named '" + bundleName + "' because the manifests data were not loaded."));
        return new AssetBundleState(bundleName, AssetBundleState.LoadState.Undefined, 0, 0);
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        UnityEngine.Debug.LogWarning((object) ("[AssetManager] Could not find a bundle named '" + bundleName + "'."));
        return new AssetBundleState(bundleName, AssetBundleState.LoadState.Undefined, 0, 0);
      }
      AssetBundleState.LoadState loadState = assetBundleLoadInfo.referenceCount > 0 || assetBundleLoadInfo.dependencyReferenceCount > 0 ? ((UnityEngine.Object) null != (UnityEngine.Object) assetBundleLoadInfo.assetBundle ? AssetBundleState.LoadState.Loaded : AssetBundleState.LoadState.Loading) : ((UnityEngine.Object) null == (UnityEngine.Object) assetBundleLoadInfo.assetBundle ? AssetBundleState.LoadState.Unloaded : AssetBundleState.LoadState.Unloading);
      return new AssetBundleState(assetBundleLoadInfo.name, loadState, assetBundleLoadInfo.referenceCount, assetBundleLoadInfo.dependencyReferenceCount);
    }

    [PublicAPI]
    [NotNull]
    public static SpriteAtlasRegisteringRequest RegisterSpriteAtlasAsync(
      [NotNull] string assetName,
      [NotNull] string bundleName)
    {
      if (assetName == null)
        throw new ArgumentNullException(nameof (assetName));
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        string errorMessage = "[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because the manifests data were not loaded.";
        return new SpriteAtlasRegisteringRequest(assetName, bundleName, 60, errorMessage);
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        string errorMessage = "[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because no corresponding bundle could be found.";
        return new SpriteAtlasRegisteringRequest(assetName, bundleName, 10, errorMessage);
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        string errorMessage = "[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded.";
        return new SpriteAtlasRegisteringRequest(assetName, bundleName, 60, errorMessage);
      }
      AssetLoadRequestIdentifier requestIdentifier = new AssetLoadRequestIdentifier(bundleName, typeof (SpriteAtlasRegisteringRequest), assetName);
      AssetLoadAsyncRequest loadAsyncRequest;
      if (AssetManager.assetLoadingRequests.TryGetValue(requestIdentifier, out loadAsyncRequest))
        return (SpriteAtlasRegisteringRequest) loadAsyncRequest;
      AssetBundleLoadRequest loadRequest = assetBundleLoadInfo.loadRequest;
      SpriteAtlasRegisteringRequest request;
      if (loadRequest != null && !loadRequest.isDone)
      {
        request = new SpriteAtlasRegisteringRequest(assetName, loadRequest);
      }
      else
      {
        UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
        if (assetBundle.isStreamedSceneAssetBundle)
        {
          string errorMessage = "Cannot register sprite atlas named '" + assetName + "' from bundle named '" + bundleName + "' because it is a streamed scene asset bundle.";
          return new SpriteAtlasRegisteringRequest(assetName, bundleName, 10, errorMessage);
        }
        request = new SpriteAtlasRegisteringRequest(assetName, assetBundle);
      }
      AssetManager.RegisterAssetLoadRequest(requestIdentifier, (AssetLoadAsyncRequest) request);
      return request;
    }

    [PublicAPI]
    [CanBeNull]
    public static SpriteAtlas RegisterSpriteAtlas([NotNull] string assetName, [NotNull] string bundleName)
    {
      if (assetName == null)
        throw new ArgumentNullException(nameof (assetName));
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not register sprite atlas named '" + assetName + "' from bundle named '" + bundleName + "' because the manifests data were not loaded."));
        return (SpriteAtlas) null;
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not register sprite atlas named '" + assetName + "' from bundle named '" + bundleName + "' because no corresponding bundle could be found."));
        return (SpriteAtlas) null;
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not register sprite atlas asset named '" + assetName + "' from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded."));
        return (SpriteAtlas) null;
      }
      UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
      if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Tried to load asset named '" + assetName + "' from bundle named '" + bundleName + "' but it is not loaded yet."));
        return (SpriteAtlas) null;
      }
      if (assetBundle.isStreamedSceneAssetBundle)
      {
        UnityEngine.Debug.LogError((object) ("Cannot load asset named '" + assetName + "' from bundle named '" + bundleName + "' because it is a streamed scene asset bundle."));
        return (SpriteAtlas) null;
      }
      SpriteAtlas spriteAtlas = assetBundle.LoadAsset<SpriteAtlas>(assetName);
      if ((UnityEngine.Object) null != (UnityEngine.Object) spriteAtlas)
        AssetManager.registeredSpriteAtlases[spriteAtlas.tag] = spriteAtlas;
      return spriteAtlas;
    }

    [PublicAPI]
    [NotNull]
    public static SpriteAtlasesRegisteringRequest RegisterSpriteAtlasesAsync([NotNull] string bundleName)
    {
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        string errorMessage = "[AssetManager] Could not register sprite atlases from bundle named '" + bundleName + "' because the manifests data were not loaded.";
        return new SpriteAtlasesRegisteringRequest(bundleName, 60, errorMessage);
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        string errorMessage = "[AssetManager] Could not register sprite atlases from bundle named '" + bundleName + "' because no corresponding bundle could be found.";
        return new SpriteAtlasesRegisteringRequest(bundleName, 10, errorMessage);
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        string errorMessage = "[AssetManager] Could not register sprite atlases from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded.";
        return new SpriteAtlasesRegisteringRequest(bundleName, 60, errorMessage);
      }
      AssetLoadRequestIdentifier requestIdentifier = new AssetLoadRequestIdentifier(bundleName, typeof (SpriteAtlasesRegisteringRequest));
      AssetLoadAsyncRequest loadAsyncRequest;
      if (AssetManager.assetLoadingRequests.TryGetValue(requestIdentifier, out loadAsyncRequest))
        return (SpriteAtlasesRegisteringRequest) loadAsyncRequest;
      AssetBundleLoadRequest loadRequest = assetBundleLoadInfo.loadRequest;
      SpriteAtlasesRegisteringRequest request;
      if (loadRequest != null && !loadRequest.isDone)
      {
        request = new SpriteAtlasesRegisteringRequest(loadRequest);
      }
      else
      {
        UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
        if (assetBundle.isStreamedSceneAssetBundle)
        {
          string errorMessage = "Cannot register sprite atlases from bundle named '" + bundleName + "' because it is a streamed scene asset bundle.";
          return new SpriteAtlasesRegisteringRequest(bundleName, 10, errorMessage);
        }
        request = new SpriteAtlasesRegisteringRequest(assetBundle);
      }
      AssetManager.RegisterAssetLoadRequest(requestIdentifier, (AssetLoadAsyncRequest) request);
      return request;
    }

    [PublicAPI]
    [CanBeNull]
    public static SpriteAtlas[] RegisterSpriteAtlases([NotNull] string bundleName)
    {
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not register sprite atlases from bundle named '" + bundleName + "' because the manifests data were not loaded."));
        return (SpriteAtlas[]) null;
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not register sprite atlases from bundle named '" + bundleName + "' because no corresponding bundle could be found."));
        return (SpriteAtlas[]) null;
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not register sprite atlases from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded."));
        return (SpriteAtlas[]) null;
      }
      UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
      if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Tried to register sprite atlases from bundle named '" + bundleName + "' but it is not loaded yet."));
        return (SpriteAtlas[]) null;
      }
      if (assetBundle.isStreamedSceneAssetBundle)
      {
        UnityEngine.Debug.LogError((object) ("Cannot register sprite atlases from bundle named '" + bundleName + "' because it is a streamed scene asset bundle."));
        return (SpriteAtlas[]) null;
      }
      SpriteAtlas[] spriteAtlasArray = assetBundle.LoadAllAssets<SpriteAtlas>();
      int length = spriteAtlasArray.Length;
      for (int index = 0; index < length; ++index)
      {
        SpriteAtlas spriteAtlas = spriteAtlasArray[index];
        AssetManager.registeredSpriteAtlases[spriteAtlas.tag] = spriteAtlas;
      }
      return spriteAtlasArray;
    }

    [PublicAPI]
    [NotNull]
    public static AssetLoadRequest<T> LoadAssetAsync<T>([NotNull] string assetName, [NotNull] string bundleName) where T : UnityEngine.Object
    {
      if (assetName == null)
        throw new ArgumentNullException(nameof (assetName));
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        string errorMessage = "[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because the manifests data were not loaded.";
        return new AssetLoadRequest<T>(assetName, bundleName, 60, errorMessage);
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        string errorMessage = "[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because no corresponding bundle could be found.";
        return new AssetLoadRequest<T>(assetName, bundleName, 10, errorMessage);
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        string errorMessage = "[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded.";
        return new AssetLoadRequest<T>(assetName, bundleName, 60, errorMessage);
      }
      AssetLoadRequestIdentifier requestIdentifier = new AssetLoadRequestIdentifier(bundleName, typeof (T), assetName);
      AssetLoadAsyncRequest loadAsyncRequest;
      if (AssetManager.assetLoadingRequests.TryGetValue(requestIdentifier, out loadAsyncRequest))
        return (AssetLoadRequest<T>) loadAsyncRequest;
      AssetBundleLoadRequest loadRequest = assetBundleLoadInfo.loadRequest;
      AssetLoadRequest<T> request;
      if (loadRequest != null && !loadRequest.isDone)
      {
        request = new AssetLoadRequest<T>(assetName, loadRequest);
      }
      else
      {
        UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
        if (assetBundle.isStreamedSceneAssetBundle)
        {
          string errorMessage = "Cannot load asset named '" + assetName + "' from bundle named '" + bundleName + "' because it is a streamed scene asset bundle.";
          return new AssetLoadRequest<T>(assetName, bundleName, 10, errorMessage);
        }
        request = new AssetLoadRequest<T>(assetName, assetBundle);
      }
      AssetManager.RegisterAssetLoadRequest(requestIdentifier, (AssetLoadAsyncRequest) request);
      return request;
    }

    [PublicAPI]
    [CanBeNull]
    public static T LoadAsset<T>([NotNull] string assetName, [NotNull] string bundleName) where T : UnityEngine.Object
    {
      if (assetName == null)
        throw new ArgumentNullException(nameof (assetName));
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because the manifests data were not loaded."));
        return default (T);
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because no corresponding bundle could be found."));
        return default (T);
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not load asset named '" + assetName + "' from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded."));
        return default (T);
      }
      UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
      if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Tried to load asset named '" + assetName + "' from bundle named '" + bundleName + "' but it is not loaded yet."));
        return default (T);
      }
      if (!assetBundle.isStreamedSceneAssetBundle)
        return assetBundle.LoadAsset<T>(assetName);
      UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot load asset named '" + assetName + "' from bundle named '" + bundleName + "' because it is a streamed scene asset bundle."));
      return default (T);
    }

    [PublicAPI]
    [NotNull]
    public static AllAssetsLoadRequest<T> LoadAllAssetsAsync<T>([NotNull] string bundleName) where T : UnityEngine.Object
    {
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        string errorMessage = "[AssetManager] Could not load all assets from bundle named '" + bundleName + "' because the manifests data were not loaded.";
        return new AllAssetsLoadRequest<T>(bundleName, 60, errorMessage);
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        string errorMessage = "[AssetManager] Could not load all assets from bundle named '" + bundleName + "' because no corresponding bundle could be found.";
        return new AllAssetsLoadRequest<T>(bundleName, 10, errorMessage);
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        string errorMessage = "[AssetManager] Could not load all assets from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded.";
        return new AllAssetsLoadRequest<T>(bundleName, 60, errorMessage);
      }
      AssetLoadRequestIdentifier requestIdentifier = new AssetLoadRequestIdentifier(bundleName, typeof (T));
      AssetLoadAsyncRequest loadAsyncRequest;
      if (AssetManager.assetLoadingRequests.TryGetValue(requestIdentifier, out loadAsyncRequest))
        return (AllAssetsLoadRequest<T>) loadAsyncRequest;
      AssetBundleLoadRequest loadRequest = assetBundleLoadInfo.loadRequest;
      AllAssetsLoadRequest<T> request;
      if (loadRequest != null && !loadRequest.isDone)
      {
        request = new AllAssetsLoadRequest<T>(loadRequest);
      }
      else
      {
        UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
        if (assetBundle.isStreamedSceneAssetBundle)
        {
          string errorMessage = "Cannot load all assets from bundle named '" + bundleName + "' because it is a streamed scene asset bundle.";
          return new AllAssetsLoadRequest<T>(bundleName, 10, errorMessage);
        }
        request = new AllAssetsLoadRequest<T>(assetBundle);
      }
      AssetManager.RegisterAssetLoadRequest(requestIdentifier, (AssetLoadAsyncRequest) request);
      return request;
    }

    [PublicAPI]
    [CanBeNull]
    public static T[] LoadAllAssets<T>([NotNull] string bundleName) where T : UnityEngine.Object
    {
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not load all assets from bundle named '" + bundleName + "' because the manifests data were not loaded."));
        return (T[]) null;
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not load all assets from bundle named '" + bundleName + "' because no corresponding bundle could be found."));
        return (T[]) null;
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Could not load all assets from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded."));
        return (T[]) null;
      }
      UnityEngine.AssetBundle assetBundle = assetBundleLoadInfo.assetBundle;
      if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Tried to load all assets from bundle named '" + bundleName + "' but it is not loaded yet."));
        return (T[]) null;
      }
      if (!assetBundle.isStreamedSceneAssetBundle)
        return assetBundle.LoadAllAssets<T>();
      UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot load all assets from bundle named '" + bundleName + "' because it is a streamed scene asset bundle."));
      return (T[]) null;
    }

    [PublicAPI]
    public static SceneLoadRequest LoadScene(
      [NotNull] string sceneName,
      [NotNull] string bundleName,
      LoadSceneParameters loadSceneParameters,
      bool allowSceneActivation = true,
      Action<SceneLoadRequest> completed = null)
    {
      if (sceneName == null)
        throw new ArgumentNullException(nameof (sceneName));
      if (bundleName == null)
        throw new ArgumentNullException(nameof (bundleName));
      if (AssetManager.assetBundleLoadInfos == null)
      {
        string errorMessage = "[AssetManager] Could not load scene named '" + sceneName + "' from bundle named '" + bundleName + "' because the manifests data were not loaded.";
        return new SceneLoadRequest(sceneName, loadSceneParameters, allowSceneActivation, bundleName, 60, errorMessage);
      }
      AssetBundleLoadInfo assetBundleLoadInfo;
      if (!AssetManager.assetBundleLoadInfos.TryGetValue(bundleName, out assetBundleLoadInfo))
      {
        string errorMessage = "[AssetManager] Could not load scene named '" + sceneName + "' from bundle named '" + bundleName + "' because no corresponding bundle could be found.";
        return new SceneLoadRequest(sceneName, loadSceneParameters, allowSceneActivation, bundleName, 10, errorMessage);
      }
      if (assetBundleLoadInfo.referenceCount == 0)
      {
        string errorMessage = "[AssetManager] Could not load scene named '" + sceneName + "' from bundle named '" + bundleName + "' because the bundle has not been explicitly loaded.";
        return new SceneLoadRequest(sceneName, loadSceneParameters, allowSceneActivation, bundleName, 60, errorMessage);
      }
      AssetBundleLoadRequest loadRequest = assetBundleLoadInfo.loadRequest;
      return loadRequest != null && !loadRequest.isDone ? new SceneLoadRequest(sceneName, loadSceneParameters, allowSceneActivation, loadRequest, completed) : new SceneLoadRequest(sceneName, loadSceneParameters, allowSceneActivation, bundleName, completed);
    }

    [PublicAPI]
    public static string GetStreamingAssetFilePath([NotNull] string assetPath)
    {
      if (assetPath == null)
        throw new ArgumentNullException(nameof (assetPath));
      StreamingAssetIdentifier streamingAssetIdentifier;
      return AssetManager.patchedStreamingAssets != null && AssetManager.patchedStreamingAssets.TryGetValue(assetPath, out streamingAssetIdentifier) ? Path.Combine(CacheManager.streamingAssetsCachePath, streamingAssetIdentifier.guid, streamingAssetIdentifier.md5) : Path.Combine(Application.streamingAssetsPath, assetPath);
    }

    [PublicAPI]
    [NotNull]
    public static StreamingAssetLoadRequest LoadStreamingAssetAsync(
      [NotNull] string assetPath,
      int bufferSize = 4096)
    {
      if (assetPath == null)
        throw new ArgumentNullException(nameof (assetPath));
      if ((UnityEngine.Object) null == (UnityEngine.Object) AssetManager.s_callbackSource)
      {
        UnityEngine.Debug.LogError((object) ("[AssetManager] Cannot load streaming asset '" + assetPath + "' because the AssetManager has not been initialized."));
        return new StreamingAssetLoadRequest(assetPath, (AssetManagerError) 60);
      }
      StreamingAssetIdentifier streamingAssetIdentifier;
      StreamingAssetLoadRequest request;
      if (AssetManager.patchedStreamingAssets != null && AssetManager.patchedStreamingAssets.TryGetValue(assetPath, out streamingAssetIdentifier))
      {
        string filePath = Path.Combine(CacheManager.streamingAssetsCachePath, streamingAssetIdentifier.guid, streamingAssetIdentifier.md5);
        request = new StreamingAssetLoadRequest(assetPath, filePath, bufferSize, true);
      }
      else
      {
        string filePath = Path.Combine(Application.streamingAssetsPath, assetPath);
        request = !filePath.Contains("://") ? new StreamingAssetLoadRequest(assetPath, filePath, bufferSize, false) : new StreamingAssetLoadRequest(assetPath, filePath);
      }
      if (!request.isDone)
        AssetManager.RegisterStreamingAssetLoadRequest(request);
      return request;
    }

    private static IEnumerator BootstrapRoutine(AssetBundleSource source)
    {
      AssetBundleManifestLoadRequest manifestLoadRequest = AssetManager.LoadAssetBundleManifest(source);
      while (!manifestLoadRequest.isDone)
        yield return (object) null;
      if ((int) manifestLoadRequest.error != 0)
        UnityEngine.Debug.LogError((object) string.Format("[AssetManager] Bootstrap failed: Could not load manifest: ({0}).", (object) manifestLoadRequest.error));
    }

    private static IEnumerator BootstrapReuseRoutine()
    {
      while (AssetManager.s_manifestLoadRequest != null && !AssetManager.s_manifestLoadRequest.isDone)
        yield return (object) null;
    }

    private static IEnumerator LoadAssetBundleManifestRoutine(bool automaticUnload)
    {
      while (!AssetManager.s_manifestLoadRequest.Update())
        yield return (object) AssetManager.s_waitForEndOfFrame;
      if ((int) AssetManager.s_manifestLoadRequest.error == 0)
      {
        AssetBundleManifest assetBundleManifest = AssetManager.s_manifestLoadRequest.assetBundleManifest;
        string[] allAssetBundles = assetBundleManifest.GetAllAssetBundles();
        int length1 = allAssetBundles.Length;
        Dictionary<string, AssetBundleFileInfo> dictionary1 = new Dictionary<string, AssetBundleFileInfo>(length1, (IEqualityComparer<string>) StringComparer.Ordinal);
        Dictionary<string, List<AssetBundleFileInfo>> dictionary2 = new Dictionary<string, List<AssetBundleFileInfo>>(length1, (IEqualityComparer<string>) StringComparer.Ordinal);
        AssetBundleVariantCollectionCreator collectionCreator = new AssetBundleVariantCollectionCreator();
        Dictionary<string, StreamingAssetIdentifier> dictionary3 = new Dictionary<string, StreamingAssetIdentifier>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        for (int index = 0; index < length1; ++index)
        {
          string str = allAssetBundles[index];
          Hash128 assetBundleHash = assetBundleManifest.GetAssetBundleHash(str);
          string variant;
          string nameWithoutVariant = AssetBundleUtility.GetBundleNameWithoutVariant(str, out variant);
          AssetBundleFileInfo assetBundleFileInfo = new AssetBundleFileInfo(str, variant, assetBundleHash);
          dictionary1.Add(str, assetBundleFileInfo);
          List<AssetBundleFileInfo> files;
          if (dictionary2.TryGetValue(nameWithoutVariant, out files))
          {
            if (variant.Length > 0)
              collectionCreator.AddVariant(variant, files);
            files.Add(assetBundleFileInfo);
          }
          else
          {
            if (variant.Length > 0)
              collectionCreator.AddSingleVariant(variant);
            files = new List<AssetBundleFileInfo>()
            {
              assetBundleFileInfo
            };
            dictionary2.Add(nameWithoutVariant, files);
          }
        }
        if (AssetManager.s_manifestLoadRequest.assetBundleManifestSource != AssetBundleSource.StreamingAssets)
        {
          AssetBundleExtendedManifest extendedManifest = AssetManager.s_manifestLoadRequest.assetBundleExtendedManifest;
          List<AssetBundleExtendedManifest.AssetBundle> assetBundles = extendedManifest.assetBundles;
          int count1 = assetBundles.Count;
          for (int index = 0; index < count1; ++index)
          {
            AssetBundleExtendedManifest.AssetBundle assetBundle = assetBundles[index];
            AssetBundleFileInfo assetBundleFileInfo = dictionary1[assetBundle.name];
            assetBundleFileInfo.crc = assetBundle.crc;
            assetBundleFileInfo.source = assetBundle.source;
          }
          List<AssetBundleExtendedManifest.StreamingAsset> streamingAssets = extendedManifest.streamingAssets;
          int count2 = streamingAssets.Count;
          for (int index = 0; index < count2; ++index)
          {
            AssetBundleExtendedManifest.StreamingAsset streamingAsset = streamingAssets[index];
            if (streamingAsset.patched)
            {
              StreamingAssetIdentifier streamingAssetIdentifier;
              streamingAssetIdentifier.guid = streamingAsset.guid;
              streamingAssetIdentifier.md5 = streamingAsset.md5;
              dictionary3.Add(streamingAsset.path, streamingAssetIdentifier);
            }
          }
        }
        Dictionary<string, AssetBundleLoadInfo> dictionary4 = new Dictionary<string, AssetBundleLoadInfo>(dictionary2.Count);
        foreach (KeyValuePair<string, List<AssetBundleFileInfo>> keyValuePair in dictionary2)
        {
          string key = keyValuePair.Key;
          List<AssetBundleFileInfo> assetBundleFileInfoList = keyValuePair.Value;
          bool hasVariants = false;
          int count = assetBundleFileInfoList.Count;
          for (int index = 0; index < count; ++index)
          {
            if (assetBundleFileInfoList[index].variant.Length > 0)
            {
              hasVariants = true;
              break;
            }
          }
          AssetBundleLoadInfo assetBundleLoadInfo = new AssetBundleLoadInfo(key, hasVariants, assetBundleFileInfoList.ToArray());
          dictionary4.Add(key, assetBundleLoadInfo);
        }
        HashSet<AssetBundleLoadInfo> assetBundleLoadInfoSet = new HashSet<AssetBundleLoadInfo>();
        foreach (AssetBundleFileInfo assetBundleFileInfo in dictionary1.Values)
        {
          string[] allDependencies = assetBundleManifest.GetAllDependencies(assetBundleFileInfo.fileName);
          int length2 = allDependencies.Length;
          for (int index = 0; index < length2; ++index)
          {
            string nameWithoutVariant = AssetBundleUtility.GetBundleNameWithoutVariant(allDependencies[index]);
            AssetBundleLoadInfo assetBundleLoadInfo = dictionary4[nameWithoutVariant];
            assetBundleLoadInfoSet.Add(assetBundleLoadInfo);
          }
          int index1 = 0;
          AssetBundleLoadInfo[] assetBundleLoadInfoArray = new AssetBundleLoadInfo[assetBundleLoadInfoSet.Count];
          foreach (AssetBundleLoadInfo assetBundleLoadInfo in assetBundleLoadInfoSet)
          {
            assetBundleLoadInfoArray[index1] = assetBundleLoadInfo;
            ++index1;
          }
          assetBundleFileInfo.dependencies = assetBundleLoadInfoArray;
          assetBundleLoadInfoSet.Clear();
        }
        AssetManager.assetBundleLoadInfos = dictionary4;
        AssetManager.assetBundleVariantCollections = collectionCreator.Build();
        AssetManager.patchedStreamingAssets = dictionary3;
      }
      else
        UnityEngine.Debug.LogError((object) string.Format("[AssetManager] Failed to load Asset Bundle Manifest file: {0} {1}", (object) AssetManager.s_manifestLoadRequest.error.code, (object) AssetManager.s_manifestLoadRequest.error.message));
      if (automaticUnload)
        AssetManager.s_manifestLoadRequest.Unload();
      AssetManager.s_manifestLoadRequest = (AssetBundleManifestLoadRequest) null;
    }

    private static AssetBundleFileInfo MapVariant(AssetBundleLoadInfo loadInfo)
    {
      if (!loadInfo.hasVariants)
        return loadInfo.files[0];
      AssetBundleFileInfo[] files = loadInfo.files;
      int length = files.Length;
      for (int index = 0; index < length; ++index)
      {
        AssetBundleFileInfo assetBundleFileInfo = files[index];
        string variant = assetBundleFileInfo.variant;
        foreach (string activeVariant in AssetManager.activeVariants)
        {
          if (activeVariant.Equals(variant))
            return assetBundleFileInfo;
        }
      }
      AssetBundleFileInfo file = loadInfo.files[0];
      UnityEngine.Debug.LogWarning((object) ("[AssetManager] No active variant matches variants of bundle named '" + loadInfo.name + "', defaulting to first bundle variant '" + file.variant + "'."));
      return file;
    }

    private static bool TryMapVariant(
      AssetBundleLoadInfo loadInfo,
      out AssetBundleFileInfo fileInfo)
    {
      if (!loadInfo.hasVariants)
      {
        fileInfo = loadInfo.files[0];
        return true;
      }
      AssetBundleFileInfo[] files = loadInfo.files;
      int length = files.Length;
      for (int index = 0; index < length; ++index)
      {
        AssetBundleFileInfo assetBundleFileInfo = files[index];
        string variant = assetBundleFileInfo.variant;
        foreach (string activeVariant in AssetManager.activeVariants)
        {
          if (activeVariant.Equals(variant))
          {
            fileInfo = assetBundleFileInfo;
            return true;
          }
        }
      }
      fileInfo = (AssetBundleFileInfo) null;
      return false;
    }

    private static void OnSpriteAtlasRequested(string tag, Action<SpriteAtlas> action)
    {
      if (tag.Equals("Untagged"))
        return;
      SpriteAtlas spriteAtlas;
      if (!AssetManager.registeredSpriteAtlases.TryGetValue(tag, out spriteAtlas))
        UnityEngine.Debug.LogWarning((object) ("[AssetManager] SpriteAtlas with tag '" + tag + "' was requested but no SpriteAtlas with this tag was registered."));
      else if ((UnityEngine.Object) null != (UnityEngine.Object) spriteAtlas)
      {
        if (action == null)
          return;
        action(spriteAtlas);
      }
      else
        UnityEngine.Debug.LogWarning((object) ("[AssetManager] SpriteAtlas with tag '" + tag + "' was requested but the registered SpriteAtlas has been unloaded."));
    }

    private static void LoadDependencyAssetBundle([NotNull] AssetBundleLoadInfo loadInfo)
    {
      ++loadInfo.dependencyReferenceCount;
      AssetBundleLoadRequest bundleLoadRequest = loadInfo.loadRequest;
      if (bundleLoadRequest != null)
      {
        bundleLoadRequest.Restart();
      }
      else
      {
        bundleLoadRequest = new AssetBundleLoadRequest(AssetManager.MapVariant(loadInfo), loadInfo);
        loadInfo.loadRequest = bundleLoadRequest;
      }
      if (bundleLoadRequest.Update())
        loadInfo.error = bundleLoadRequest.error;
      else
        AssetManager.RegisterAssetBundleRequest(loadInfo);
    }

    private static void UnloadDependencyAssetBundle([NotNull] AssetBundleLoadInfo loadInfo)
    {
      --loadInfo.dependencyReferenceCount;
      if (loadInfo.referenceCount > 0 || loadInfo.dependencyReferenceCount > 0)
        return;
      AssetBundleLoadRequest loadRequest = loadInfo.loadRequest;
      if (loadRequest != null && !loadRequest.Cancel())
        AssetManager.RegisterAssetBundleRequest(loadInfo);
      AssetBundleUnloadRequest bundleUnloadRequest = loadInfo.unloadRequest;
      if (bundleUnloadRequest != null)
      {
        bundleUnloadRequest.Restart();
      }
      else
      {
        bundleUnloadRequest = new AssetBundleUnloadRequest(AssetManager.MapVariant(loadInfo), loadInfo, false);
        loadInfo.unloadRequest = bundleUnloadRequest;
      }
      if (bundleUnloadRequest.Update())
        return;
      AssetManager.RegisterAssetBundleRequest(loadInfo);
    }

    internal static void RevertAssetBundleRequest(
      AssetBundleFileInfo fileInfo,
      AssetBundleLoadInfo loadInfo)
    {
      --loadInfo.referenceCount;
      AssetBundleLoadInfo[] dependencies = fileInfo.dependencies;
      int length = dependencies.Length;
      for (int index = 0; index < length; ++index)
        AssetManager.RevertDependencyAssetBundleRequest(dependencies[index]);
      if (loadInfo.referenceCount > 0 || loadInfo.dependencyReferenceCount > 0)
        return;
      AssetBundleLoadRequest loadRequest = loadInfo.loadRequest;
      if (loadRequest == null)
        return;
      UnityEngine.AssetBundle assetBundle = loadRequest.assetBundle;
      if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
        return;
      assetBundle.Unload(true);
    }

    internal static void RevertDependencyAssetBundleRequest(AssetBundleLoadInfo loadInfo)
    {
      --loadInfo.dependencyReferenceCount;
      if (loadInfo.referenceCount > 0 || loadInfo.dependencyReferenceCount > 0)
        return;
      AssetBundleLoadRequest loadRequest = loadInfo.loadRequest;
      if (loadRequest == null)
        return;
      UnityEngine.AssetBundle assetBundle = loadRequest.assetBundle;
      if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
        return;
      assetBundle.Unload(true);
    }

    internal static bool HasActiveAssetLoadingRequest([NotNull] string bundleName)
    {
      foreach (KeyValuePair<AssetLoadRequestIdentifier, AssetLoadAsyncRequest> assetLoadingRequest in AssetManager.assetLoadingRequests)
      {
        if (assetLoadingRequest.Key.bundleName.Equals(bundleName))
          return true;
      }
      return false;
    }

    private static void RegisterAssetBundleRequest(AssetBundleLoadInfo loadInfo)
    {
      AssetManager.assetBundleRequests.Add(loadInfo);
      if (AssetManager.s_bundleRequestsCoroutineRunning)
        return;
      AssetManager.s_callbackSource.StartCoroutine(AssetManager.BundleRequestsRoutine());
    }

    private static void RegisterAssetLoadRequest(
      AssetLoadRequestIdentifier identifier,
      AssetLoadAsyncRequest request)
    {
      AssetManager.assetLoadingRequests.Add(identifier, request);
      if (AssetManager.s_bundleRequestsCoroutineRunning)
        return;
      AssetManager.s_callbackSource.StartCoroutine(AssetManager.BundleRequestsRoutine());
    }

    private static IEnumerator BundleRequestsRoutine()
    {
      AssetManager.s_bundleRequestsCoroutineRunning = true;
      int count1 = AssetManager.assetBundleRequests.Count;
      for (int count2 = AssetManager.assetLoadingRequests.Count; count1 > 0 || count2 > 0; count2 = AssetManager.assetLoadingRequests.Count)
      {
        foreach (KeyValuePair<AssetLoadRequestIdentifier, AssetLoadAsyncRequest> assetLoadingRequest in AssetManager.assetLoadingRequests)
        {
          if (assetLoadingRequest.Value.Update())
            AssetManager.s_completedAssetLoadingRequests.Add(assetLoadingRequest.Key);
        }
        int count3 = AssetManager.s_completedAssetLoadingRequests.Count;
        for (int index = 0; index < count3; ++index)
        {
          AssetLoadRequestIdentifier assetLoadingRequest = AssetManager.s_completedAssetLoadingRequests[index];
          AssetManager.assetLoadingRequests.Remove(assetLoadingRequest);
        }
        AssetManager.s_completedAssetLoadingRequests.Clear();
        foreach (AssetBundleLoadInfo assetBundleRequest in AssetManager.assetBundleRequests)
        {
          bool flag = true;
          AssetBundleLoadRequest loadRequest = assetBundleRequest.loadRequest;
          if (loadRequest != null)
          {
            if (loadRequest.Update())
              assetBundleRequest.error = loadRequest.error;
            else
              flag = false;
          }
          AssetBundleUnloadRequest unloadRequest = assetBundleRequest.unloadRequest;
          if (unloadRequest != null && !unloadRequest.Update())
            flag = false;
          if (flag)
          {
            if (!assetBundleRequest.isAssetBundleLoaded)
            {
              assetBundleRequest.loadRequest = (AssetBundleLoadRequest) null;
              assetBundleRequest.unloadRequest = (AssetBundleUnloadRequest) null;
            }
            AssetManager.s_completedAssetBundleRequests.Add(assetBundleRequest);
          }
        }
        int count4 = AssetManager.s_completedAssetBundleRequests.Count;
        for (int index = 0; index < count4; ++index)
        {
          AssetBundleLoadInfo assetBundleRequest = AssetManager.s_completedAssetBundleRequests[index];
          AssetManager.assetBundleRequests.Remove(assetBundleRequest);
        }
        AssetManager.s_completedAssetBundleRequests.Clear();
        yield return (object) AssetManager.s_waitForEndOfFrame;
        count1 = AssetManager.assetBundleRequests.Count;
      }
      AssetManager.s_bundleRequestsCoroutineRunning = false;
    }

    [Conditional("UNITY_EDITOR")]
    private static void NotifyAssetBundleStateChanged()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private static void NotifySpriteAtlasRequested(string tag)
    {
    }

    [Conditional("UNITY_EDITOR")]
    internal static void NotifySpriteAtlasRegistered(SpriteAtlas spriteAtlas)
    {
    }

    private static void RegisterStreamingAssetLoadRequest(StreamingAssetLoadRequest request)
    {
      AssetManager.streamingAssetLoadingRequests.Add(request);
      if (AssetManager.s_streamingAssetRequestsCoroutineRunning)
        return;
      AssetManager.s_callbackSource.StartCoroutine(AssetManager.StreamingAssetRequestsRoutine());
    }

    private static IEnumerator StreamingAssetRequestsRoutine()
    {
      AssetManager.s_streamingAssetRequestsCoroutineRunning = true;
      for (int count = AssetManager.streamingAssetLoadingRequests.Count; count > 0; count = AssetManager.streamingAssetLoadingRequests.Count)
      {
        for (int index = count - 1; index >= 0; --index)
        {
          if (AssetManager.streamingAssetLoadingRequests[index].Update())
            AssetManager.streamingAssetLoadingRequests.RemoveAt(index);
        }
        yield return (object) AssetManager.s_waitForEndOfFrame;
      }
      AssetManager.s_streamingAssetRequestsCoroutineRunning = false;
    }
  }
}
