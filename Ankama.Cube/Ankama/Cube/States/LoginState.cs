// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.LoginState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Configuration;
using Ankama.Cube.Network;
using Ankama.Cube.Player;
using Ankama.Cube.TEMPFastEnterMatch.Auth;
using Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.Utility;
using Ankama.Launcher;
using Ankama.Launcher.Zaap;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.States
{
  public class LoginState : StateContext
  {
    protected override IEnumerator Load()
    {
      LoginState loginState = this;
      ConnectionHandler.instance.autoReconnect = false;
      PlayerData.OnPlayerDataInitialized += new PlayerData.PlayerDataInitialized(loginState.OnPlayerDataInitialized);
      yield return (object) LoginState.AwaitLauncherConnection();
      yield return (object) loginState.OpenLoginOrAutoConnect();
    }

    protected override void Enable()
    {
    }

    protected override void Disable()
    {
    }

    protected override IEnumerator Unload()
    {
      ConnectionHandler.instance.autoReconnect = true;
      PlayerData.OnPlayerDataInitialized -= new PlayerData.PlayerDataInitialized(this.OnPlayerDataInitialized);
      return base.Unload();
    }

    private void OnPlayerDataInitialized(bool pendingFightFound)
    {
      PlayerData.OnPlayerDataInitialized -= new PlayerData.PlayerDataInitialized(this.OnPlayerDataInitialized);
      if (pendingFightFound)
      {
        Log.Info("Pending fight found. Going to fight mode", 58, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\LoginState.cs");
        StatesUtility.DoTransition((StateContext) new ReconnectingFightState(), StateManager.GetDefaultLayer().GetChildState());
      }
      else
      {
        LoginUI.StartState startState = (LoginUI.StartState) PlayerPreferences.startState;
        switch (startState)
        {
          case LoginUI.StartState.MatchMaking:
            StatesUtility.GotoMatchMakingState();
            break;
          case LoginUI.StartState.HavreDimension:
            StatesUtility.GotoDimensionState();
            break;
          default:
            Log.Warning(string.Format("Start in state '{0}' unknown: goto Dimension state", (object) startState), 77, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\LoginState.cs");
            StatesUtility.GotoDimensionState();
            break;
        }
      }
    }

    private void OnPlayerAccountLoadingErrorEvent()
    {
    }

    private void OnAccountReady()
    {
    }

    private IEnumerator OpenLoginOrAutoConnect()
    {
      if (!ApplicationConfig.gameServerIsLocal && (ApplicationConfig.gameServerProfile == RemoteConfig.ServerProfile.Beta || ApplicationConfig.gameServerProfile == RemoteConfig.ServerProfile.Development) && !(LauncherConnection.instance is ZaapLink))
        return this.OpenZaapRequiredState();
      if (CredentialProvider.gameCredentialProvider.AutoConnectLevel() != AutoConnectLevel.NoAutoConnect)
        return this.Connect();
      return CredentialProvider.gameCredentialProvider.HasGuestMode() && CredentialProvider.gameCredentialProvider.LoginUIType() == LoginUIType.Guest ? this.OpenSelectLoginUI() : this.OpenLoginUICoroutine();
    }

    private IEnumerator Connect()
    {
      int gameServerProfile = (int) ApplicationConfig.gameServerProfile;
      ConnectionHandler.instance.Connect();
      yield break;
    }

    private IEnumerator OpenZaapRequiredState()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      LoginState loginState = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loginState.SetChildState((StateContext) new ZaapRequiredState());
      return false;
    }

    private IEnumerator OpenSelectLoginUI()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      LoginState loginState = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loginState.SetChildState((StateContext) new SelectLoginUIState());
      return false;
    }

    private IEnumerator OpenLoginUICoroutine()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      LoginState loginState = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loginState.SetChildState((StateContext) new LoginUIState());
      return false;
    }

    private static IEnumerator AwaitLauncherConnection()
    {
      ILauncherLink conn = LauncherConnection.instance;
      if (conn == null)
      {
        yield return (object) null;
      }
      else
      {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (conn.opening)
          yield return (object) waitForEndOfFrame;
        waitForEndOfFrame = (WaitForEndOfFrame) null;
      }
    }
  }
}
