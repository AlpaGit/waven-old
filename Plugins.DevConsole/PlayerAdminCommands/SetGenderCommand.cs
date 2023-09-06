// Decompiled with JetBrains decompiler
// Type: DevConsole.PlayerAdminCommands.SetGenderCommand
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using Ankama.Cube.Protocols.AdminCommandsProtocol;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace DevConsole.PlayerAdminCommands
{
  [UsedImplicitly]
  [ConsoleCommand(new string[] {"player/setGender"})]
  internal class SetGenderCommand
  {
    public static string Help(string command, bool verbose) => "Set the level of the player. Parameters: '1' for Male, '2' for Female.";

    public static AdminCmd NetworkCommand(string[] tokens)
    {
      int result = 0;
      if (tokens.Length >= 1)
        int.TryParse(tokens[0], out result);
      return new AdminCmd()
      {
        SetGender = new AdminCmd.Types.SetGender()
        {
          Gender = result
        }
      };
    }

    public static List<string> FetchAutocompleteOptions(string command, string[] tokens) => (List<string>) null;
  }
}
