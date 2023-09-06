// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.LauncherConnection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI;
using Ankama.Launcher;
using Ankama.Launcher.Messages;
using Ankama.Launcher.Zaap;
using Ankama.Utilities;
using com.ankama.zaap;
using JetBrains.Annotations;
using System;
using System.Collections;

namespace Ankama.Cube.Configuration
{
  public static class LauncherConnection
  {
    private static ILauncherLink s_launcherLink;
    private static bool s_requestingLanguage;
    private static string s_language;

    public static ILauncherLink instance => LauncherConnection.s_launcherLink;

    [NotNull]
    public static string launcherLanguage => LauncherConnection.s_language ?? string.Empty;

    public static IEnumerator InitializeConnection()
    {
      RuntimeData.CultureCodeChanged += new RuntimeData.CultureCodeChangedEventHandler(LauncherConnection.OnCultureCodeChanged);
      LauncherConnection.s_launcherLink = (ILauncherLink) ZaapLink.Create();
      if (LauncherConnection.s_launcherLink == null)
      {
        Log.Warning("Unable to get Zaap connection, falling back to NoConnection.", 42, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\LauncherConnection.cs");
        LauncherConnection.s_launcherLink = NoConnection.instance;
      }
      else
        Log.Info("Connection to Zaap: OK", 47, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\LauncherConnection.cs");
      LauncherConnection.s_requestingLanguage = true;
      LauncherConnection.s_launcherLink.RequestLanguage(new Action<string>(LauncherConnection.OnLauncherLanguage), new Action<Exception>(LauncherConnection.OnLauncherLanguageError));
      while (LauncherConnection.s_requestingLanguage)
        yield return (object) null;
    }

    public static void Release() => RuntimeData.CultureCodeChanged -= new RuntimeData.CultureCodeChangedEventHandler(LauncherConnection.OnCultureCodeChanged);

    public static void RequestApiToken(Action<ApiToken> onApiToken, int serviceId) => LauncherConnection.s_launcherLink.RequestApiToken(serviceId, onApiToken, (Action<Exception>) (error =>
    {
      Log.Error(string.Format("Error received while expecting apiToken: {0}", (object) error), 70, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\LauncherConnection.cs");
      onApiToken((ApiToken) null);
    }));

    private static void OnLauncherLanguage(string language)
    {
      LauncherConnection.s_language = language;
      LauncherConnection.s_requestingLanguage = false;
    }

    private static void OnLauncherLanguageError(Exception e)
    {
      Log.Warning(!(e is ZaapError zaapError) ? "Unable to get language : " + (e.InnerException?.Message ?? e.Message) : string.Format("Unable to get language because of ZaapError code {0} : {1}", (object) zaapError.Code, (object) zaapError.Details), 87, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\LauncherConnection.cs");
      LauncherConnection.OnLauncherLanguage((string) null);
    }

    private static void OnCultureCodeChanged(CultureCode cultureCode, FontLanguage fontLanguage)
    {
      string language = cultureCode.GetLanguage();
      if (language.Equals(LauncherConnection.s_language))
        return;
      LauncherConnection.s_language = language;
      LauncherConnection.s_launcherLink.UpdateLanguage(language, new Action<bool>(LauncherConnection.OnLanguageUpdate), new Action<Exception>(LauncherConnection.OnLanguageUpdateError));
    }

    private static void OnLanguageUpdateError(Exception e) => Log.Warning(!(e is ZaapError zaapError) ? "Unable to update language : " + (e.InnerException?.Message ?? e.Message) : string.Format("Unable to update language because of ZaapError code {0} : {1}", (object) zaapError.Code, (object) zaapError.Details), 115, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\LauncherConnection.cs");

    private static void OnLanguageUpdate(bool obj)
    {
    }
  }
}
