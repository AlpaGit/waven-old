// Decompiled with JetBrains decompiler
// Type: DevConsole.VersionCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using JetBrains.Annotations;
using System.Collections.Generic;

namespace DevConsole
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"version", "ver"})]
  internal class VersionCommand
  {
    public static string Help(string command, bool verbose) => "Displays the version of the game.";

    public static string Execute(string[] tokens) => "Waven 0.1.1.6169";

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens) => (List<string>) null;
  }
}
