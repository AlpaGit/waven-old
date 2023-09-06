// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.Options
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ankama.Cube.Configuration
{
  public class Options
  {
    private readonly List<Options.ICommandArg> m_commands = new List<Options.ICommandArg>();
    private readonly StringBuilder m_stringBuilder = new StringBuilder();

    public void Register(string name, Action<bool> set) => this.m_commands.Add((Options.ICommandArg) new Options.CommandArgBool(name, set));

    public void Register(string name, Action<string> set, string possibleValue) => this.m_commands.Add((Options.ICommandArg) new Options.CommandArgString(name, set, possibleValue));

    public bool ParseArgument(string arg, ref int index)
    {
      foreach (Options.ICommandArg command in this.m_commands)
      {
        if (command.Parse(arg, ref index))
          return true;
      }
      if (arg.StartsWith("--"))
        Log.Warning("Argument not handle " + arg + ".", 396, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationConfig.cs");
      return false;
    }

    public string Usage()
    {
      this.m_stringBuilder.Clear();
      for (int index = 0; index < this.m_commands.Count; ++index)
        this.m_stringBuilder.AppendLine(this.m_commands[index].usage);
      return this.m_stringBuilder.ToString();
    }

    private interface ICommandArg
    {
      string usage { get; }

      bool Parse(string arg, ref int index);
    }

    private class CommandArgBool : Options.ICommandArg
    {
      private readonly string m_prefix;
      private readonly string m_longPrefix;
      private readonly Action<bool> m_action;

      public string usage { get; }

      public CommandArgBool(string name, Action<bool> set)
      {
        this.m_prefix = "--" + name;
        this.m_longPrefix = "--" + name + "=";
        this.usage = this.m_prefix + " | " + this.m_longPrefix + "true|false";
        this.m_action = set;
      }

      public bool Parse(string arg, ref int index)
      {
        if (arg == this.m_prefix)
        {
          this.m_action(true);
          return true;
        }
        if (!arg.StartsWithFast(this.m_longPrefix))
          return false;
        this.m_action(bool.Parse(arg.Substring(this.m_longPrefix.Length)));
        return true;
      }
    }

    private class CommandArgString : Options.ICommandArg
    {
      private readonly string m_prefix;
      private readonly Action<string> m_action;

      public string usage { get; }

      public CommandArgString(string name, Action<string> set, string possibleValue)
      {
        this.m_prefix = "--" + name + "=";
        this.usage = this.m_prefix + possibleValue;
        this.m_action = set;
      }

      public bool Parse(string arg, ref int index)
      {
        if (!arg.StartsWithFast(this.m_prefix))
          return false;
        this.m_action(arg.Substring(this.m_prefix.Length));
        return true;
      }
    }
  }
}
