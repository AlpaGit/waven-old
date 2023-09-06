// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.ICredentialProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod
{
  public interface ICredentialProvider : ISpinCredentialsProvider
  {
    Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.AutoConnectLevel AutoConnectLevel();

    bool CanDisconnect();

    bool CanDisplayDisconnectButton();

    bool HasGuestMode();

    Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType LoginUIType();

    bool HasGuestAccount();

    void CleanCredentials();
  }
}
