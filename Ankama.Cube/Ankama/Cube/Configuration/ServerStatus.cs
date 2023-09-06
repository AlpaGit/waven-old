// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.ServerStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System;

namespace Ankama.Cube.Configuration
{
  public struct ServerStatus
  {
    public readonly ServerStatus.StatusCode code;
    public readonly DateTime maintenanceStartTimeUtc;
    public readonly TimeSpan maintenanceDuration;
    public static readonly ServerStatus ok = new ServerStatus(ServerStatus.StatusCode.OK);
    public static readonly ServerStatus none = new ServerStatus(ServerStatus.StatusCode.None);
    public static readonly ServerStatus error = new ServerStatus(ServerStatus.StatusCode.Error);

    public DateTime maintenanceEstimatedEndTimeUtc => this.maintenanceStartTimeUtc + this.maintenanceDuration;

    private ServerStatus(ServerStatus.StatusCode code)
    {
      this.code = code;
      this.maintenanceStartTimeUtc = DateTime.UtcNow;
      this.maintenanceDuration = TimeSpan.Zero;
    }

    public ServerStatus(DateTime maintenanceStartTimeUtc, TimeSpan maintenanceDuration)
    {
      this.code = maintenanceStartTimeUtc.CompareTo(DateTime.UtcNow) <= 0 ? ServerStatus.StatusCode.Maintenance : ServerStatus.StatusCode.MaintenanceExpected;
      this.maintenanceStartTimeUtc = maintenanceStartTimeUtc;
      this.maintenanceDuration = maintenanceDuration;
    }

    public static ServerStatus Parse(string text)
    {
      ConfigReader configReader = new ConfigReader(text);
      if (!configReader.HasProperty("maintenanceStartTimeUtc"))
        return new ServerStatus(ServerStatus.StatusCode.OK);
      try
      {
        string s1 = configReader.GetString("maintenanceStartTimeUtc");
        string s2 = configReader.GetString("maintenanceDuration");
        return new ServerStatus(DateTime.Parse(s1), TimeSpan.Parse(s2));
      }
      catch (Exception ex)
      {
        Log.Error("Error parsing serverStatus: {text}", (object) ex, 56, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ServerStatus.cs");
        return new ServerStatus(ServerStatus.StatusCode.Error);
      }
    }

    public enum StatusCode
    {
      None,
      OK,
      Error,
      Maintenance,
      MaintenanceExpected,
    }
  }
}
