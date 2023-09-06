// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.ApplicationStarter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.AssetManagement.StreamingAssets;
using Ankama.Cube.Data.UI;
using Ankama.Cube.Network;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Configuration
{
  public static class ApplicationStarter
  {
    public static bool InitializeRuntimeData()
    {
      if (!RuntimeData.InitializeLanguage(ApplicationStarter.GetCurrentCulture()))
      {
        Log.Error("[CRITICAL] Could not initialize language.", 23, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
        return false;
      }
      if (RuntimeData.InitializeFonts())
        return true;
      Log.Error("[CRITICAL] Could not initialize fonts.", 29, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
      return false;
    }

    public static bool InitializeAssetManager()
    {
      AssetManager.Initialize();
      AssetReferenceMap assetReferenceMap = Resources.Load<AssetReferenceMap>("AssetReferenceMap");
      if ((Object) null == (Object) assetReferenceMap)
      {
        Log.Error("[CRITICAL] Could not load AssetReferenceMap.", 43, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
        return false;
      }
      AssetManager.SetAssetReferenceMap(assetReferenceMap);
      Resources.UnloadAsset((Object) assetReferenceMap);
      return true;
    }

    public static IEnumerator ReadBootConfig()
    {
      StreamingAssetLoadRequest loadOperation = AssetManager.LoadStreamingAssetAsync("application.json");
      while (!loadOperation.isDone)
        yield return (object) null;
      if ((int) loadOperation.error != 0)
        Log.Error(string.Format("Error while reading application.json: {0}", (object) loadOperation.error), 64, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
      else
        BootConfig.Read(new ConfigReader(loadOperation.text, loadOperation.assetPath));
    }

    public static IEnumerator ReadRemoteConfig(string remoteSettingsFileUrl)
    {
      TextWebRequest.AsyncResult remoteSettingsFileLoadResult = new TextWebRequest.AsyncResult();
      yield return (object) TextWebRequest.ReadFile(remoteSettingsFileUrl, remoteSettingsFileLoadResult);
      if (remoteSettingsFileLoadResult.hasException)
      {
        long responseCode = remoteSettingsFileLoadResult.exception.responseCode;
        if (responseCode == 404L)
          Log.Error("Could not find remote settings file at URL '" + remoteSettingsFileUrl + "'.", 83, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
        else
          Log.Error(string.Format("Error {0} when trying to download remote settings file at URL '{1}'.", (object) responseCode, (object) remoteSettingsFileUrl), 90, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
      }
      else
        ApplicationConfig.SetFromRemoteConfig(RemoteConfig.From(new ConfigReader(remoteSettingsFileLoadResult.value, remoteSettingsFileUrl)));
    }

    public static IEnumerator ReadVersion(string versionFileUrl)
    {
      if (string.IsNullOrEmpty(versionFileUrl))
      {
        Log.Warning("No version file defined: force version valid", 106, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
        ApplicationConfig.versionCheckResult = VersionChecker.Result.Success;
      }
      else
      {
        TextWebRequest.AsyncResult versionFileLoadResult = new TextWebRequest.AsyncResult();
        yield return (object) TextWebRequest.ReadFile(versionFileUrl, versionFileLoadResult);
        if (versionFileLoadResult.hasException)
        {
          long responseCode = versionFileLoadResult.exception.responseCode;
          if (responseCode == 404L)
            Log.Error("Could not find version file at URL '" + versionFileUrl + "'.", 121, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
          else
            Log.Error(string.Format("Error {0} when trying to download version file at URL '{1}'.", (object) responseCode, (object) versionFileUrl), 125, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
          ApplicationConfig.versionCheckResult = VersionChecker.Result.VersionFileError;
        }
        else
          ApplicationConfig.versionCheckResult = VersionChecker.ParseVersionFile(versionFileLoadResult.value);
      }
    }

    public static IEnumerator ConfigureAssetManager(string serverUrl, bool patchAvailable)
    {
      AssetManager.UnloadAssetBundleManifest();
      AssetManager.SetAssetBundlesConfiguration(serverUrl, true);
      AssetBundleManifestLoadRequest loadOperation = AssetManager.LoadAssetBundleManifest(patchAvailable ? AssetBundleSource.Web : AssetBundleSource.StreamingAssets);
      while (!loadOperation.isDone)
        yield return (object) null;
      if ((int) loadOperation.error != 0)
        Log.Error(string.Format("Could not load manifests data: {0}.", (object) loadOperation.error), 159, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
    }

    public static IEnumerator ConfigureLocalAssetManager()
    {
      AssetManager.UnloadAssetBundleManifest();
      AssetManager.SetAssetBundlesConfiguration(string.Empty, true);
      AssetBundleManifestLoadRequest loadOperation = AssetManager.LoadAssetBundleManifest(AssetBundleSource.StreamingAssets);
      while (!loadOperation.isDone)
        yield return (object) null;
      if ((int) loadOperation.error != 0)
        Log.Error(string.Format("Could not load manifests data: {0}.", (object) loadOperation.error), 185, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationStarter.cs");
    }

    public static IEnumerator CheckPatch()
    {
      foreach (CachedAssetBundle cachedAssetBundle in AssetManager.EnumerateCachedAssetBundles())
      {
        if (!Caching.IsVersionCached(cachedAssetBundle))
        {
          string assetBundleName = cachedAssetBundle.name;
          AssetBundleLoadRequest loadRequest = AssetManager.LoadAssetBundle(assetBundleName);
          while (!loadRequest.isDone)
            yield return (object) null;
          AssetBundleUnloadRequest unloadRequest = AssetManager.UnloadAssetBundle(assetBundleName);
          while (!unloadRequest.isDone)
            yield return (object) null;
          assetBundleName = (string) null;
          loadRequest = (AssetBundleLoadRequest) null;
          unloadRequest = (AssetBundleUnloadRequest) null;
        }
      }
    }

    private static CultureCode GetCurrentCulture()
    {
      CultureCode cultureCode;
      if (ApplicationStarter.TryGetCultureFromCode(LauncherConnection.launcherLanguage, out cultureCode) || ApplicationStarter.TryGetCultureFromCode(ApplicationConfig.langCode, out cultureCode))
        return cultureCode;
      switch (Application.systemLanguage)
      {
        case SystemLanguage.English:
          return CultureCode.EN_US;
        case SystemLanguage.French:
          return CultureCode.FR_FR;
        default:
          return CultureCode.Fallback;
      }
    }

    private static bool TryGetCultureFromCode(string code, out CultureCode cultureCode)
    {
      switch (code)
      {
        case "FR":
          cultureCode = CultureCode.FR_FR;
          return true;
        case "EN":
          cultureCode = CultureCode.EN_US;
          return true;
        case "ES":
          cultureCode = CultureCode.ES_ES;
          return true;
        default:
          cultureCode = new CultureCode();
          return false;
      }
    }
  }
}
