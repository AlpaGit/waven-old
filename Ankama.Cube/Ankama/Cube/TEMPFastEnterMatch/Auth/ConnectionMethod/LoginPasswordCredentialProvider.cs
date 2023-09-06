// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginPasswordCredentialProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Configuration;
using Ankama.Cube.Network.Spin2;
using Ankama.Cube.Player;
using Ankama.Utilities;
using Com.Ankama.Haapi.Swagger.Api;
using Com.Ankama.Haapi.Swagger.Model;
using IO.Swagger.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod
{
  public class LoginPasswordCredentialProvider : ICredentialProvider, ISpinCredentialsProvider
  {
    public async Task<ISpinCredentials> GetCredentials()
    {
      TaskCompletionSource<ISpinCredentials> task = new TaskCompletionSource<ISpinCredentials>();
      this.DoRequest(task);
      return await task.Task;
    }

    private void DoRequest(TaskCompletionSource<ISpinCredentials> task)
    {
      string login = PlayerPreferences.useGuest ? PlayerPreferences.guestLogin : PlayerPreferences.lastLogin;
      string password = PlayerPreferences.useGuest ? PlayerPreferences.guestPassword : PlayerPreferences.lastPassword;
      HaapiManager.ExecuteRequest<RAccountApi<Token>>((Func<RAccountApi<Token>>) (() => HaapiManager.accountApi.CreateTokenWithPassword(login, password, new long?((long) ApplicationConfig.GameAppId))), (Action<RAccountApi<Token>>) (res =>
      {
        Log.Info("CreateTokenWithPassword success ! Login=" + res.Data._Token, 277, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Auth\\ConnectionMethod\\ICredentialProvider.cs");
        task.SetResult((ISpinCredentials) new AnkamaTokenCredentials(res.Data._Token));
      }), (Action<Exception>) (exception =>
      {
        if (exception is ApiException apiException2 && apiException2.ErrorCode == 601)
        {
          Log.Error(string.Format("CreateTokenWithPassword error: {0}", (object) exception), 284, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Auth\\ConnectionMethod\\ICredentialProvider.cs");
          ErrorAccountLogin error = JsonConvert.DeserializeObject<ErrorAccountLogin>((string) apiException2.ErrorContent);
          task.SetException((error == null ? (Exception) null : (Exception) HaapiHelper.From(error)) ?? exception);
        }
        else
        {
          Log.Error(string.Format("CreateTokenWithPassword error: {0}", (object) exception), 291, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Auth\\ConnectionMethod\\ICredentialProvider.cs");
          task.SetException(exception);
        }
      }));
    }

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel AutoConnectLevel() => PlayerPreferences.autoLogin && (PlayerPreferences.useGuest && CredentialProvider.HasGuestAccount() && CredentialProvider.HasGuestMode() || PlayerPreferences.rememberPassword && !string.IsNullOrEmpty(PlayerPreferences.lastLogin) && !string.IsNullOrEmpty(PlayerPreferences.lastPassword)) ? Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel.IfAvailable : Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel.NoAutoConnect;

    public bool CanDisconnect() => true;

    public bool CanDisplayDisconnectButton() => true;

    public bool HasGuestMode() => CredentialProvider.HasGuestMode();

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType LoginUIType() => CredentialProvider.LoginUIType();

    public bool HasGuestAccount() => CredentialProvider.HasGuestAccount();

    public void CleanCredentials()
    {
      PlayerPreferences.autoLogin = false;
      PlayerPreferences.lastPassword = "";
      PlayerPreferences.Save();
    }
  }
}
