// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LauncherLinkCredentialProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Configuration;
using Ankama.Cube.Network.Spin2;
using Ankama.Launcher.Messages;
using Ankama.Utilities;
using System;
using System.Threading.Tasks;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod
{
  public class LauncherLinkCredentialProvider : ICredentialProvider, ISpinCredentialsProvider
  {
    private readonly int m_serviceId;

    public LauncherLinkCredentialProvider(int serviceId) => this.m_serviceId = serviceId;

    public async Task<ISpinCredentials> GetCredentials()
    {
      TaskCompletionSource<ISpinCredentials> task = new TaskCompletionSource<ISpinCredentials>();
      this.DoRequest(task);
      return await task.Task;
    }

    private void DoRequest(TaskCompletionSource<ISpinCredentials> task) => LauncherConnection.RequestApiToken((Action<ApiToken>) (token =>
    {
      if (token != null)
      {
        Log.Info("Launcher token: " + token.Value, 210, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Auth\\ConnectionMethod\\ICredentialProvider.cs");
        task.SetResult((ISpinCredentials) new AnkamaTokenCredentials(token.Value));
      }
      else
        task.SetException((Exception) new CredentialException(string.Format("Unable to get token from launcher {0}={1}", (object) "m_serviceId", (object) this.m_serviceId)));
    }), this.m_serviceId);

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel AutoConnectLevel() => Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel.Mandatory;

    public bool CanDisconnect() => false;

    public bool CanDisplayDisconnectButton() => false;

    public bool HasGuestMode() => false;

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType LoginUIType() => Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType.LoginPassword;

    public bool HasGuestAccount() => false;

    public void CleanCredentials()
    {
    }
  }
}
