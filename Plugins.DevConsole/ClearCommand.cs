// Decompiled with JetBrains decompiler
// Type: DevConsole.ClearCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace DevConsole
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"clear", "cls"})]
  internal class ClearCommand
  {
    public static string Help(string command, bool verbose) => verbose ? command + Environment.NewLine + "    Clears the output of the console." : "Clears the output of the console.";

    public static string Execute(string[] tokens)
    {
      ConsoleDaemon.Instance.OnClearConsole.Invoke();
      return "";
    }

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens) => (List<string>) null;
  }
}
