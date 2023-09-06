// Decompiled with JetBrains decompiler
// Type: DevConsole.HelpCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevConsole
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"help", "?"})]
  internal class HelpCommand
  {
    public static string Help(string command, bool verbose) => verbose ? command + " [command]" + Environment.NewLine + "    Displays detailed help information about [command]. If no command is included then lists all commands." : "Displays help information.";

    public static string Execute(string[] tokens)
    {
      if (tokens.Length != 0)
        return ConsoleDaemon.Instance.GetHelp(tokens[0], true);
      List<string> commandList = ConsoleDaemon.Instance.CommandList;
      int a = 0;
      foreach (string str in commandList)
        a = Mathf.Max(a, str.Length);
      string str1 = "";
      for (int index = 0; index < commandList.Count; ++index)
        str1 = str1 + "<color=#00ff00ff>" + commandList[index].PadRight(a + 4) + "</color>" + "<i>" + ConsoleDaemon.Instance.GetHelp(commandList[index], false) + "</i>" + Environment.NewLine;
      return str1;
    }

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens)
    {
      string enteredText = tokens.Length != 0 ? tokens[0] : "";
      List<string> list = ConsoleDaemon.Instance.CommandList.Where<string>((Func<string, bool>) (name => name.StartsWith(enteredText))).ToList<string>();
      if (list == null || list.Count == 0)
        return (List<string>) null;
      List<string> stringList = new List<string>();
      foreach (string str in list)
        stringList.Add(command + " " + str);
      return stringList;
    }
  }
}
