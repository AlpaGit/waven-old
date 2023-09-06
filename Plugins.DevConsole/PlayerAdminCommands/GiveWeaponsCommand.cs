// Decompiled with JetBrains decompiler
// Type: DevConsole.PlayerAdminCommands.GiveWeaponsCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using Ankama.Cube.Protocols.AdminCommandsProtocol;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace DevConsole.PlayerAdminCommands
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"player/giveAllWeapons"})]
  internal class GiveWeaponsCommand
  {
    public static string Help(string command, bool verbose) => "Add all the existing weapons to the current player.";

    public static AdminCmd NetworkCommand(string[] tokens) => new AdminCmd()
    {
      GiveAllWeapons = true
    };

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens) => (List<string>) null;
  }
}
