// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.ConnectionHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Configuration;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Network.Spin2;
using Ankama.Cube.Player;
using Ankama.Cube.TEMPFastEnterMatch.Auth;
using Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.NicknameRequest;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using Google.Protobuf;
using System;
using UnityEngine;

namespace Ankama.Cube.Network
{
  public class ConnectionHandler
  {
    private static ConnectionHandler s_instance;
    private readonly CubeServerConnection m_connection;
    private BasicAccountLoadingHandler m_accountLoadingHandler;
    private GlobalFrame m_globalFrame;
    private Coroutine m_currentReconnectionCoroutine;
    private bool m_reconnecting;
    private int m_count;
    private ConnectionHandler.Status m_currentStatus;
    private bool m_autoReconnect;
    private int m_currentPopupId = -1;

    public static ConnectionHandler instance => ConnectionHandler.s_instance;

    public ConnectionHandler.Status status => this.m_currentStatus;

    public bool autoReconnect
    {
      get => this.m_autoReconnect;
      set => this.m_autoReconnect = value;
    }

    public event ConnectionHandler.ConnectionStatusChangedHandler OnConnectionStatusChanged;

    public static bool Initialized => ConnectionHandler.s_instance != null;

    public static void Initialize()
    {
      if (ConnectionHandler.s_instance != null)
        return;
      Log.Info("Adding CubeServerConnection to scene.", 69, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
      CubeServerConnection serverConnection = new GameObject("CubeServerConnection").AddComponent<CubeServerConnection>();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) serverConnection);
      ConnectionHandler.s_instance = new ConnectionHandler(serverConnection);
    }

    public static void Destroy()
    {
      if (ConnectionHandler.s_instance == null)
        return;
      Log.Info("Destroy connection handler", 81, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
      ConnectionHandler.s_instance.m_connection.Disconnect();
      UnityEngine.Object.Destroy((UnityEngine.Object) ConnectionHandler.s_instance.m_connection);
      ConnectionHandler.s_instance = (ConnectionHandler) null;
    }

    private ConnectionHandler(CubeServerConnection connection)
    {
      this.m_connection = connection;
      this.m_connection.OnConnectionOpened += new Action(this.OnConnectionOpened);
      this.m_connection.OnConnectionClosed += new Action<IDisconnectionInfo>(this.OnConnectionClosed);
      this.m_connection.OnOpenConnectionFailed += new Action<IConnectionError>(this.OnOpenConnectionFailed);
    }

    private void UpdateStatus(ConnectionHandler.Status newStatus)
    {
      if (this.m_globalFrame != null && newStatus == ConnectionHandler.Status.Disconnected)
      {
        this.m_globalFrame.Dispose();
        this.m_globalFrame = (GlobalFrame) null;
      }
      ConnectionHandler.ConnectionStatusChangedHandler connectionStatusChanged = this.OnConnectionStatusChanged;
      if (connectionStatusChanged != null)
        connectionStatusChanged(this.m_currentStatus, newStatus);
      this.m_currentStatus = newStatus;
      this.m_reconnecting = this.m_currentStatus == ConnectionHandler.Status.Connecting;
    }

    public void Connect()
    {
      if (this.m_globalFrame != null)
        this.m_globalFrame.Dispose();
      this.m_globalFrame = new GlobalFrame();
      this.UpdateStatus(ConnectionHandler.Status.Connecting);
      Log.Info(string.Format("Connecting to {0}:{1}", (object) ApplicationConfig.gameServerHost, (object) ApplicationConfig.gameServerPort), 122, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
      this.m_connection.Connect(ApplicationConfig.gameServerHost, ApplicationConfig.gameServerPort);
    }

    public IConnection<IMessage> connection => (IConnection<IMessage>) this.m_connection;

    public void Disconnect() => this.m_connection.Disconnect();

    private void Reconnect()
    {
      if (this.m_reconnecting)
        return;
      ++this.m_count;
      this.m_reconnecting = true;
      Log.Info(string.Format("[ConnectionHandler] Trying to reconnect. Retry {0}.", (object) this.m_count), 142, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
      this.CreateReconnectionPopup();
      this.Connect();
    }

    private void StopReconnection()
    {
      this.m_connection.Disconnect();
      StatesUtility.ClearSecondaryLayers();
      this.ReleaseReconnection();
      PlayerPreferences.autoLogin = false;
      StatesUtility.GotoLoginState();
    }

    private void OnOpenConnectionFailed(IConnectionError obj)
    {
      this.UpdateStatus(ConnectionHandler.Status.Disconnected);
      switch (obj)
      {
        case SpinConnectionError spinConnectionError:
          string formattedText = TextCollectionUtility.AuthenticationErrorKeys.GetFormattedText(spinConnectionError.error);
          switch (spinConnectionError.error)
          {
            case SpinProtocol.ConnectionErrors.BadCredentials:
            case SpinProtocol.ConnectionErrors.InvalidAuthenticationInfo:
            case SpinProtocol.ConnectionErrors.SubscriptionRequired:
            case SpinProtocol.ConnectionErrors.AdminRightsRequired:
            case SpinProtocol.ConnectionErrors.AccountKnonwButBanned:
            case SpinProtocol.ConnectionErrors.AccountKnonwButBlocked:
            case SpinProtocol.ConnectionErrors.BetaAccessRequired:
              this.CreateDisconnectedPopup(formattedText, DisconnectionStrategy.ReturnToLoginAndChangeAccount);
              return;
            case SpinProtocol.ConnectionErrors.IpAddressRefused:
              this.CreateDisconnectedPopup(formattedText, DisconnectionStrategy.QuitApplication);
              return;
            case SpinProtocol.ConnectionErrors.ServerTimeout:
            case SpinProtocol.ConnectionErrors.ServerError:
            case SpinProtocol.ConnectionErrors.AccountsBackendError:
              this.CreateDisconnectedPopup(formattedText, DisconnectionStrategy.ReturnToLogin);
              return;
            case SpinProtocol.ConnectionErrors.NickNameRequired:
              this.CreateDisconnectedPopup(formattedText, DisconnectionStrategy.QuitApplication);
              return;
            default:
              throw new ArgumentOutOfRangeException();
          }
        case ConnectionInterruptedError interruptedError:
          if (interruptedError.disconnectionInfo is ServerDisconnectionInfo disconnectionInfo)
          {
            Log.Info(string.Format("Disconnection occured during authentication {0}", (object) interruptedError.disconnectionInfo), 211, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
            this.CreateDisconnectedPopup(TextCollectionUtility.DisconnectionReasonKeys.GetFormattedText(disconnectionInfo.reason), DisconnectionStrategy.ReturnToLogin);
            break;
          }
          if (!(interruptedError.disconnectionInfo is NetworkDisconnectionInfo))
            break;
          if (this.m_autoReconnect)
          {
            this.Reconnect();
            break;
          }
          Log.Info(string.Format("Disconnection occured during authentication {0}", (object) interruptedError.disconnectionInfo), 226, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
          this.CreateDisconnectedPopup(RuntimeData.FormattedText(94930, (IValueProvider) null), DisconnectionStrategy.ReturnToLogin);
          break;
        case NetworkConnectionError networkConnectionError:
          if (this.m_autoReconnect)
          {
            this.Reconnect();
            break;
          }
          Log.Info(string.Format("Error occured during authentication {0}", (object) networkConnectionError.exception), 246, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
          this.CreateDisconnectedPopup(RuntimeData.FormattedText(34942, (IValueProvider) null), DisconnectionStrategy.ReturnToLogin);
          break;
        default:
          Log.Info(string.Format("Error while connecting: {0}", (object) obj), 253, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
          this.CreateDisconnectedPopup(RuntimeData.FormattedText(36698, obj.ToString()), DisconnectionStrategy.ReturnToLogin);
          break;
      }
    }

    private void OnConnectionOpened()
    {
      this.m_count = 0;
      this.UpdateStatus(ConnectionHandler.Status.Connected);
      if (this.m_accountLoadingHandler == null)
        this.m_accountLoadingHandler = new BasicAccountLoadingHandler();
      this.m_accountLoadingHandler.LoadAccount();
      this.ReleaseReconnection();
    }

    private void OnConnectionClosed(IDisconnectionInfo obj)
    {
      this.UpdateStatus(ConnectionHandler.Status.Disconnected);
      switch (obj)
      {
        case ServerDisconnectionInfo disconnectionInfo:
          this.CreateDisconnectedPopup(TextCollectionUtility.DisconnectionReasonKeys.GetFormattedText(disconnectionInfo.reason), DisconnectionStrategy.ReturnToLogin);
          break;
        case ClientDisconnectionInfo _:
          break;
        case NetworkDisconnectionInfo _:
          this.Reconnect();
          break;
        default:
          Log.Error(string.Format("Connection closed for unknown reason: {0}. Leaving application.", (object) obj), 300, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\ConnectionHandler.cs");
          this.CreateDisconnectedPopup(RuntimeData.FormattedText(36698, obj.ToString()), DisconnectionStrategy.QuitApplication);
          break;
      }
    }

    private void CreateDisconnectedPopup(string cause, DisconnectionStrategy errorStrategy)
    {
      bool flag = false;
      AutoConnectLevel autoConnectLevel = CredentialProvider.gameCredentialProvider.AutoConnectLevel();
      Action onClick;
      switch (errorStrategy)
      {
        case DisconnectionStrategy.ReturnToLogin:
          if (autoConnectLevel == AutoConnectLevel.Mandatory)
          {
            onClick = (Action) (() =>
            {
              CredentialProvider.gameCredentialProvider.CleanCredentials();
              Main.Quit();
            });
            flag = true;
            break;
          }
          onClick = (Action) (() =>
          {
            PlayerPreferences.autoLogin = false;
            StatesUtility.GotoLoginState();
          });
          break;
        case DisconnectionStrategy.ReturnToLoginAndChangeAccount:
          if (autoConnectLevel == AutoConnectLevel.Mandatory)
          {
            onClick = (Action) (() =>
            {
              CredentialProvider.gameCredentialProvider.CleanCredentials();
              Main.Quit();
            });
            flag = true;
            break;
          }
          onClick = (Action) (() =>
          {
            CredentialProvider.gameCredentialProvider.CleanCredentials();
            StatesUtility.GotoLoginState();
          });
          break;
        case DisconnectionStrategy.QuitApplication:
          onClick = new Action(Main.Quit);
          flag = true;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (errorStrategy), (object) errorStrategy, (string) null);
      }
      ButtonData[] buttonDataArray;
      if (flag)
        buttonDataArray = new ButtonData[1]
        {
          new ButtonData((TextData) 75192, onClick)
        };
      else
        buttonDataArray = new ButtonData[2]
        {
          new ButtonData((TextData) 27169, onClick),
          new ButtonData((TextData) 75192, new Action(Main.Quit))
        };
      PopupInfoManager.ClearAllMessages();
      PopupInfoManager.ShowApplicationError(new PopupInfo()
      {
        title = (RawTextData) 20267,
        message = (RawTextData) cause,
        buttons = buttonDataArray,
        selectedButton = 1
      });
    }

    private void CreateReconnectionPopup()
    {
      if (this.m_currentPopupId >= 0)
        return;
      RawTextData rawTextData;
      ButtonData[] buttonDataArray;
      if (CredentialProvider.gameCredentialProvider.AutoConnectLevel() == AutoConnectLevel.Mandatory)
      {
        rawTextData = (RawTextData) 34942;
        buttonDataArray = new ButtonData[1]
        {
          new ButtonData((TextData) 75192, (Action) (() =>
          {
            CredentialProvider.gameCredentialProvider.CleanCredentials();
            StatesUtility.ClearSecondaryLayers();
            Main.Quit();
          }))
        };
      }
      else
      {
        rawTextData = (RawTextData) 30166;
        buttonDataArray = new ButtonData[1]
        {
          new ButtonData((TextData) 59515, (Action) (() =>
          {
            this.StopReconnection();
            this.m_currentPopupId = -1;
          }))
        };
      }
      this.m_currentPopupId = 1;
      PopupInfoManager.ShowApplicationError(new PopupInfo()
      {
        title = (RawTextData) 20267,
        message = rawTextData,
        buttons = buttonDataArray,
        selectedButton = 1,
        style = PopupStyle.Error
      });
    }

    private void RequestNickname()
    {
      NicknameRequestState childState = new NicknameRequestState();
      childState.OnSuccess += new Action<string>(this.OnNicknameUpdated);
      StateManager.GetDefaultLayer().GetChainEnd().SetChildState((StateContext) childState);
    }

    private void OnNicknameUpdated(string nickname) => PlayerData.instance?.UpdateNickname(nickname);

    private void ReleaseReconnection()
    {
      if (this.m_currentPopupId >= 0)
      {
        PopupInfoManager.RemoveById(this.m_currentPopupId);
        this.m_currentPopupId = -1;
      }
      this.m_reconnecting = false;
      this.m_count = 0;
    }

    public void Dispose()
    {
      if (this.m_globalFrame != null)
      {
        this.m_globalFrame.Dispose();
        this.m_globalFrame = (GlobalFrame) null;
      }
      this.m_connection.Disconnect();
      this.ReleaseReconnection();
    }

    public enum Status
    {
      Disconnected,
      Connecting,
      Connected,
    }

    public delegate void ConnectionStatusChangedHandler(
      ConnectionHandler.Status from,
      ConnectionHandler.Status to);
  }
}
