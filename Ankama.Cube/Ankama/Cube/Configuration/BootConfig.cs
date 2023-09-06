// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.BootConfig
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;

namespace Ankama.Cube.Configuration
{
  public static class BootConfig
  {
    public static bool initialized { get; private set; }

    public static string remoteConfigUrl { get; private set; } = string.Empty;

    public static void Read([NotNull] ConfigReader reader)
    {
      BootConfig.remoteConfigUrl = RemoteConfig.ReplaceVars(reader.GetUrl("remoteConfigUrl", string.Empty));
      BootConfig.initialized = true;
    }
  }
}
