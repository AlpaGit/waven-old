// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.NoHaapiLoginPasswordCredentialProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2;
using Ankama.Cube.Player;
using System.Threading.Tasks;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod
{
  public class NoHaapiLoginPasswordCredentialProvider : ICredentialProvider, ISpinCredentialsProvider
  {
    public async Task<ISpinCredentials> GetCredentials()
    {
      TaskCompletionSource<ISpinCredentials> task = new TaskCompletionSource<ISpinCredentials>();
      this.DoRequest(task);
      return await task.Task;
    }

    private void DoRequest(TaskCompletionSource<ISpinCredentials> task)
    {
      string lastLogin = PlayerPreferences.lastLogin;
      task.SetResult((ISpinCredentials) new AnkamaNoHaapiCredentials(lastLogin));
    }

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel AutoConnectLevel() => PlayerPreferences.autoLogin && (CredentialProvider.HasGuestAccount() && this.HasGuestMode() && PlayerPreferences.useGuest || PlayerPreferences.rememberPassword && !string.IsNullOrEmpty(PlayerPreferences.lastLogin) && !string.IsNullOrEmpty(PlayerPreferences.lastPassword)) ? Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel.IfAvailable : Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel.NoAutoConnect;

    public bool CanDisconnect() => true;

    public bool CanDisplayDisconnectButton() => true;

    public bool HasGuestMode() => false;

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType LoginUIType() => Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType.LoginPassword;

    public bool HasGuestAccount() => false;

    public void CleanCredentials()
    {
      PlayerPreferences.autoLogin = false;
      PlayerPreferences.lastPassword = "";
      PlayerPreferences.Save();
    }
  }
}
