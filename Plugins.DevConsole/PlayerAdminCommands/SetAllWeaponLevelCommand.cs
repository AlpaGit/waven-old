// Decompiled with JetBrains decompiler
// Type: DevConsole.PlayerAdminCommands.SetAllWeaponLevelCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using Ankama.Cube.Protocols.AdminCommandsProtocol;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace DevConsole.PlayerAdminCommands
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"player/setAllWeaponLevels"})]
  internal class SetAllWeaponLevelCommand
  {
    public static string Help(string command, bool verbose) => "Set the level of all weapons. Parameters : <level>";

    public static AdminCmd NetworkCommand(string[] tokens)
    {
      int result = 0;
      if (tokens.Length >= 1)
        int.TryParse(tokens[0], out result);
      return new AdminCmd()
      {
        SetAllWeaponLevels = new AdminCmd.Types.SetAllLevels()
        {
          Level = result
        }
      };
    }

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens) => (List<string>) null;
  }
}
