// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.VersionChecker
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Configuration
{
  public static class VersionChecker
  {
    public static VersionChecker.Result ParseVersionFile([NotNull] string text)
    {
      ApplicationVersion other;
      try
      {
        other = new ApplicationVersion("0.1.1.6169");
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, 125, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\VersionChecker.cs");
        return VersionChecker.Result.RuntimeError;
      }
      ApplicationVersion applicationVersion;
      try
      {
        applicationVersion = new ApplicationVersion(text);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, 136, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\VersionChecker.cs");
        return VersionChecker.Result.VersionFileError;
      }
      if (applicationVersion.Matches(other, ApplicationVersion.MatchMask.None))
        return VersionChecker.Result.Success;
      return applicationVersion.Matches(other, ApplicationVersion.MatchMask.Patch) ? VersionChecker.Result.PatchAvailable : VersionChecker.Result.UpdateNeeded;
    }

    public enum Result
    {
      None,
      Success,
      PatchAvailable,
      UpdateNeeded,
      VersionFileError,
      RuntimeError,
    }
  }
}
