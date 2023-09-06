// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.CredentialProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Configuration;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod
{
  public static class CredentialProvider
  {
    private static readonly CredentialProviderWrapper s_gameProvider = new CredentialProviderWrapper();
    private static readonly CredentialProviderWrapper s_chatProvider = new CredentialProviderWrapper();

    public static bool HasGuestAccount() => false;

    public static bool HasGuestMode() => false;

    public static Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType LoginUIType() => Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod.LoginUIType.LoginPassword;

    public static ICredentialProvider chatCredentialProvider
    {
      get
      {
        if (!CredentialProvider.s_chatProvider.IsInit())
          CredentialProvider.s_chatProvider.SetProvider(CredentialProvider.ChatProvider());
        return (ICredentialProvider) CredentialProvider.s_chatProvider;
      }
    }

    private static ICredentialProvider ChatProvider() => !LauncherConnection.instance.opened ? (ICredentialProvider) new LoginPasswordCredentialProvider() : (ICredentialProvider) new LauncherLinkCredentialProvider(ApplicationConfig.ChatAppId);

    public static ICredentialProvider gameCredentialProvider
    {
      get
      {
        if (!CredentialProvider.s_gameProvider.IsInit())
          CredentialProvider.s_gameProvider.SetProvider(CredentialProvider.GameProvider());
        return (ICredentialProvider) CredentialProvider.s_gameProvider;
      }
    }

    private static ICredentialProvider GameProvider()
    {
      if (!ApplicationConfig.haapiAllowed)
        return (ICredentialProvider) new NoHaapiLoginPasswordCredentialProvider();
      return LauncherConnection.instance.opened ? (ICredentialProvider) new LauncherLinkCredentialProvider(ApplicationConfig.GameAppId) : (ICredentialProvider) new LoginPasswordCredentialProvider();
    }

    public static void DeteteCredentialProviders()
    {
      CredentialProvider.s_chatProvider.SetProvider((ICredentialProvider) null);
      CredentialProvider.s_gameProvider.SetProvider((ICredentialProvider) null);
    }
  }
}
