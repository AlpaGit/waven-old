// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.CredentialProviderWrapper
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2;
using System.Threading.Tasks;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod
{
  internal class CredentialProviderWrapper : ICredentialProvider, ISpinCredentialsProvider
  {
    private ICredentialProvider m_provider;

    public bool IsInit() => this.m_provider != null;

    public void SetProvider(ICredentialProvider provider) => this.m_provider = provider;

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel AutoConnectLevel() => this.m_provider.AutoConnectLevel();

    public bool CanDisconnect() => this.m_provider.CanDisconnect();

    public bool CanDisplayDisconnectButton() => this.m_provider.CanDisplayDisconnectButton();

    public bool HasGuestMode() => this.m_provider.HasGuestMode();

    public Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType LoginUIType() => this.m_provider.LoginUIType();

    public bool HasGuestAccount() => this.m_provider.HasGuestAccount();

    public void CleanCredentials() => this.m_provider.CleanCredentials();

    public async Task<ISpinCredentials> GetCredentials() => await this.m_provider.GetCredentials();
  }
}
