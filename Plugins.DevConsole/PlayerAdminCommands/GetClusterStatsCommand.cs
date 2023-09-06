// Decompiled with JetBrains decompiler
// Type: DevConsole.PlayerAdminCommands.GetClusterStatsCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using Ankama.Cube.Protocols.AdminCommandsProtocol;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace DevConsole.PlayerAdminCommands
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"stats"})]
  internal class GetClusterStatsCommand
  {
    public static string Help(string command, bool verbose) => "Get stats for the cluster (players, connections, fights). Parameters: 'verbose' for detailled stats.";

    public static AdminCmd NetworkCommand(string[] tokens)
    {
      bool flag = false;
      for (int index = tokens.Length - 1; index >= 0; --index)
      {
        if (tokens[index].Equals("verbose"))
        {
          flag = true;
          break;
        }
      }
      return new AdminCmd()
      {
        GetClusterStatistics = new AdminCmd.Types.GetClusterStatistics()
        {
          Detailed = flag,
          ConnectionsCounts = true,
          FightsCount = true,
          PlayersEntitiesCount = true
        }
      };
    }

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens) => (List<string>) null;
  }
}
