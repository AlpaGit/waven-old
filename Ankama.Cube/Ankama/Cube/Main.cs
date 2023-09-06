// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Main
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Audio;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Configuration;
using Ankama.Cube.Network;
using Ankama.Cube.Player;
using Ankama.Cube.SRP;
using Ankama.Cube.States;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube
{
  public class Main : MonoBehaviour
  {
    private bool m_serverStatusChecked;

    public static MonoBehaviour monoBehaviour { get; private set; }

    public static void Quit() => Application.Quit();

    private void Awake()
    {
      UnityRemoteSettings.Load();
      string str = "START version: 0.1.1.6169 " + DateTime.Now.ToShortDateString();
      Log.Info(str + "\n" + new string('-', 35 + str.Length), 42, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Main.cs");
      Device.LogInfo();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      PlayerPreferences.Load();
      ApplicationConfig.Read();
      QualityManager.Load();
      if (PlayerPreferences.graphicPresetIndex == -1)
      {
        PlayerPreferences.graphicPresetIndex = QualityManager.GetQualityPresetIndex();
        PlayerPreferences.Save();
      }
      QualityManager.SetQualityPresetIndex(PlayerPreferences.graphicPresetIndex);
      Main.monoBehaviour = (MonoBehaviour) this;
      StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.Escape, 1));
      StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.Return, 2));
      StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.KeypadEnter, 3));
      StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.Tab, 4));
    }

    private IEnumerator Start() => this.Initialize();

    private void Update() => Device.CheckScreenStateChanged();

    private void OnDestroy()
    {
      LauncherConnection.Release();
      RuntimeData.Release();
      StateManager.UnregisterInputDefinition(1);
      StateManager.UnregisterInputDefinition(2);
      StateManager.UnregisterInputDefinition(3);
      StateManager.UnregisterInputDefinition(4);
    }

    private IEnumerator Initialize()
    {
      Main main = this;
      yield return (object) LauncherConnection.InitializeConnection();
      if (!ApplicationStarter.InitializeAssetManager())
      {
        Log.Error("InitializeAssetManager: misssing AssetReferenceMap", 102, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Main.cs");
        Main.Quit();
      }
      if (!ApplicationStarter.InitializeRuntimeData())
      {
        Log.Error("InitializeAssetManager: missing LocalizedTextData or BootTextCollection", 109, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Main.cs");
        Main.Quit();
      }
      SplashState childState = new SplashState();
      StateManager.GetDefaultLayer().SetChildState((StateContext) childState);
      yield return (object) ApplicationStarter.ReadBootConfig();
      if (!BootConfig.initialized)
      {
        yield return (object) Main.GoToCatastrophicFailureState(Main.InitializationFailure.BootConfigInitialisation);
      }
      else
      {
        if (ApplicationConfig.showServerSelection)
        {
          ApplicationConfig.versionCheckResult = VersionChecker.Result.Success;
        }
        else
        {
          string txt = ApplicationConfig.configUrl;
          if (string.IsNullOrEmpty(txt))
            txt = BootConfig.remoteConfigUrl;
          string remoteSettingsFileUrl = RemoteConfig.ReplaceVars(txt);
          Log.Info("configUrl=" + remoteSettingsFileUrl, 139, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Main.cs");
          yield return (object) ApplicationStarter.ReadRemoteConfig(remoteSettingsFileUrl);
          if (!ApplicationConfig.initialized)
          {
            yield return (object) Main.GoToInitializationFailedState(Main.InitializationFailure.ApplicationConfigInitialisation);
            yield break;
          }
        }
        ApplicationConfig.PrintConfig();
        if (ApplicationConfig.versionCheckResult == VersionChecker.Result.None)
          yield return (object) ApplicationStarter.ReadVersion(ApplicationConfig.versionFileUrl);
        if (!ApplicationConfig.IsVersionValid())
        {
          yield return (object) Main.GoToInitializationFailedState(Main.InitializationFailure.UnvalidVersion);
        }
        else
        {
          main.StartCoroutine(main.CheckServerStatus());
          if (ApplicationConfig.haapiAllowed)
            HaapiManager.Initialize();
          string bundlesUrl = ApplicationConfig.bundlesUrl;
          bool patchAvailable = ApplicationConfig.versionCheckResult == VersionChecker.Result.PatchAvailable;
          yield return (object) ApplicationStarter.ConfigureAssetManager(bundlesUrl, patchAvailable);
          if (!AssetManager.isReady)
          {
            yield return (object) Main.GoToInitializationFailedState(Main.InitializationFailure.AssetManagerInitialisation);
          }
          else
          {
            if (patchAvailable)
              yield return (object) ApplicationStarter.CheckPatch();
            yield return (object) AudioManager.Load();
            PlayerPreferences.InitializeAudioPreference();
            yield return (object) RuntimeData.Load();
            if (!RuntimeData.isReady)
            {
              yield return (object) Main.GoToInitializationFailedState(Main.InitializationFailure.RuntimeDataInitialisation);
            }
            else
            {
              ConnectionHandler.Initialize();
              while (!main.m_serverStatusChecked)
                yield return (object) null;
              StatesUtility.GotoLoginState();
            }
          }
        }
      }
    }

    private IEnumerator CheckServerStatus()
    {
      Main main = this;
      int code = (int) ApplicationConfig.serverStatus.code;
      if (ApplicationConfig.serverStatus.code == ServerStatus.StatusCode.Error)
        yield return (object) Main.GoToInitializationFailedState(Main.InitializationFailure.ServerStatusError);
      else if (ApplicationConfig.serverStatus.code == ServerStatus.StatusCode.Maintenance)
        yield return (object) Main.GoToInitializationFailedState(Main.InitializationFailure.ServerStatusMaintenance);
      else if (ApplicationConfig.serverStatus.code == ServerStatus.StatusCode.MaintenanceExpected)
      {
        main.m_serverStatusChecked = false;
        DateTime dateTime = ApplicationConfig.serverStatus.maintenanceStartTimeUtc.ToLocalTime();
        string shortTimeString1 = dateTime.ToShortTimeString();
        dateTime = ApplicationConfig.serverStatus.maintenanceEstimatedEndTimeUtc;
        dateTime = dateTime.ToLocalTime();
        string shortTimeString2 = dateTime.ToShortTimeString();
        // ISSUE: reference to a compiler-generated method
        PopupInfoManager.Show(StateManager.GetDefaultLayer().GetChainEnd(), new PopupInfo()
        {
          title = (RawTextData) 75142,
          message = new RawTextData(85153, new string[2]
          {
            shortTimeString1,
            shortTimeString2
          }),
          buttons = new ButtonData[1]
          {
            new ButtonData((TextData) 27169, new Action(main.\u003CCheckServerStatus\u003Eb__11_0))
          },
          selectedButton = 1,
          style = PopupStyle.Warning
        });
      }
      else
        main.m_serverStatusChecked = true;
    }

    private static IEnumerator GoToInitializationFailedState(Main.InitializationFailure cause)
    {
      Log.Error("Switching to initialization failed state.", 285, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Main.cs");
      yield return (object) AudioManager.Unload();
      yield return (object) RuntimeData.Unload();
      if ((int) RuntimeData.error != 0)
      {
        yield return (object) Main.GoToCatastrophicFailureState(Main.InitializationFailure.RuntimeDataLoad);
      }
      else
      {
        yield return (object) ApplicationStarter.ConfigureLocalAssetManager();
        if (!AssetManager.isReady)
        {
          yield return (object) Main.GoToCatastrophicFailureState(cause);
        }
        else
        {
          yield return (object) AudioManager.Load();
          yield return (object) RuntimeData.LoadOffline();
          InitializationFailedState childState = new InitializationFailedState(cause);
          StateManager.GetDefaultLayer().GetChainEnd().SetChildState((StateContext) childState);
        }
      }
    }

    private static IEnumerator GoToCatastrophicFailureState(Main.InitializationFailure cause)
    {
      CatastrophicFailureState childState = new CatastrophicFailureState(cause);
      StateManager.GetDefaultLayer().GetChainEnd().SetChildState((StateContext) childState);
      yield break;
    }

    public enum InitializationFailure
    {
      RuntimeDataInitialisation,
      BootConfigInitialisation,
      ApplicationConfigInitialisation,
      UnvalidVersion,
      ServerStatusError,
      ServerStatusMaintenance,
      RuntimeDataLoad,
      AssetManagerInitialisation,
    }
  }
}
