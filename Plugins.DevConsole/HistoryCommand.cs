// Decompiled with JetBrains decompiler
// Type: DevConsole.HistoryCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace DevConsole
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"history"})]
  internal class HistoryCommand
  {
    public static string Help(string command, bool verbose) => verbose ? command + Environment.NewLine + "    Displays a list of all of the recently executed commands." : "Displays a list of all of the recently executed commands.";

    public static string Execute(string[] tokens)
    {
      List<string> history = ConsoleDaemon.Instance.History;
      string str1 = "";
      foreach (string str2 in history)
      {
        if (!string.IsNullOrEmpty(str2))
          str1 += Environment.NewLine;
        str1 = str1 + "    " + str2;
      }
      return str1;
    }

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens) => (List<string>) null;
  }
}
