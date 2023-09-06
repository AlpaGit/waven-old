// Decompiled with JetBrains decompiler
// Type: DevConsole.ConsoleDaemon
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using Ankama.Cube.Network;
using Ankama.Cube.Protocols.AdminCommandsProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace DevConsole
{
  public class ConsoleDaemon : MonoBehaviour
  {
    public const string Version = "1.0.0";
    public const string Colour_Command = "#00ff00ff";
    public const string Colour_Error = "#ff0000ff";
    public const string Colour_NetworkSuccess = "#00ff00ff";
    public const string Colour_NetworkError = "#ff0000ff";
    public const string Colour_NetworkCmdSent = "#aaaaaaaa";
    protected static ConsoleDaemon _Instance;
    protected static bool _IsQuitting;
    protected Dictionary<string, string> AlternateCommandNames = new Dictionary<string, string>();
    protected Dictionary<string, MethodInfo> ExecuteMethods = new Dictionary<string, MethodInfo>();
    protected Dictionary<string, MethodInfo> NetworkExecuteMethods = new Dictionary<string, MethodInfo>();
    protected Dictionary<string, MethodInfo> HelpMethods = new Dictionary<string, MethodInfo>();
    protected Dictionary<string, MethodInfo> FetchAutocompleteMethods = new Dictionary<string, MethodInfo>();
    public List<string> CommandList;
    public UnityEvent OnClearConsole = new UnityEvent();
    public UnityStringEvent OnCommandEntered = new UnityStringEvent();
    public UnityStringEvent OnAddTextToConsole = new UnityStringEvent();
    protected List<string> CommandHistory = new List<string>();
    protected int MaxHistory = 100;
    protected int HistoryIndex;
    protected int NextAsyncCommandId = 1;
    private ConsoleCommandsFrame _netorkCommandsFrame;

    public void OnDestroy() => ConsoleDaemon._IsQuitting = true;

    public static ConsoleDaemon Instance
    {
      get
      {
        if (ConsoleDaemon._IsQuitting)
          return (ConsoleDaemon) null;
        if ((UnityEngine.Object) ConsoleDaemon._Instance == (UnityEngine.Object) null)
        {
          ConsoleDaemon._Instance = UnityEngine.Object.FindObjectOfType<ConsoleDaemon>();
          if ((UnityEngine.Object) ConsoleDaemon._Instance == (UnityEngine.Object) null)
          {
            GameObject target = new GameObject(nameof (ConsoleDaemon));
            ConsoleDaemon._Instance = target.AddComponent<ConsoleDaemon>();
            UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
          }
        }
        return ConsoleDaemon._Instance;
      }
    }

    private void Awake() => this.PopulateCommandRegistry();

    private void PopulateCommandRegistry()
    {
      foreach (var data in ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (assembly => !assembly.GlobalAssemblyCache)).SelectMany((Func<Assembly, IEnumerable<Type>>) (assembly => (IEnumerable<Type>) assembly.GetTypes()), (assembly, type) => new
      {
        assembly = assembly,
        type = type
      }).Select(_param1 => new
      {
        \u003C\u003Eh__TransparentIdentifier0 = _param1,
        attributes = _param1.type.GetCustomAttributes(typeof (ConsoleCommandAttribute), true)
      }).Where(_param1 => _param1.attributes != null && _param1.attributes.Length != 0).Select(_param1 => new
      {
        Type = _param1.\u003C\u003Eh__TransparentIdentifier0.type,
        Attributes = _param1.attributes.Cast<ConsoleCommandAttribute>()
      }))
      {
        MethodInfo method1 = data.Type.GetMethod("Execute", BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Standard, new Type[1]
        {
          typeof (string[])
        }, (ParameterModifier[]) null);
        MethodInfo method2 = data.Type.GetMethod("NetworkCommand", BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Standard, new Type[1]
        {
          typeof (string[])
        }, (ParameterModifier[]) null);
        MethodInfo method3 = data.Type.GetMethod("Help", BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Standard, new Type[2]
        {
          typeof (string),
          typeof (bool)
        }, (ParameterModifier[]) null);
        MethodInfo method4 = data.Type.GetMethod("FetchAutocompleteOptions", BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Standard, new Type[2]
        {
          typeof (string),
          typeof (string[])
        }, (ParameterModifier[]) null);
        if (method2 != (MethodInfo) null && method1 != (MethodInfo) null)
          Debug.LogError((object) ("Both Execute and NetworkCommand methods found for class " + data.Type.Name + ". Execute will be used."));
        if ((method2 != (MethodInfo) null || method1 != (MethodInfo) null) && method3 != (MethodInfo) null && method4 != (MethodInfo) null)
        {
          bool flag = false;
          string str = "";
          foreach (ConsoleCommandAttribute attribute in data.Attributes)
          {
            foreach (string name in attribute.Names)
            {
              if (!flag)
              {
                if (method1 != (MethodInfo) null)
                  this.ExecuteMethods[name] = method1;
                else
                  this.NetworkExecuteMethods[name] = method2;
                this.HelpMethods[name] = method3;
                this.FetchAutocompleteMethods[name] = method4;
                flag = true;
                str = name;
                this.AlternateCommandNames[name] = name;
              }
              else
                this.AlternateCommandNames[name] = str;
            }
          }
        }
        else
        {
          if (method1 == (MethodInfo) null)
          {
            Debug.LogError((object) ("DevConsole: Failed to find Execute method for " + data.Type.Name));
            Debug.LogError((object) "Method should have signature: public string Execute(string[] tokens)");
          }
          if (method3 == (MethodInfo) null)
          {
            Debug.LogError((object) ("DevConsole: Failed to find Help method for " + data.Type.Name));
            Debug.LogError((object) "Method should have signature: public string Help(bool verbose)");
          }
          if (method4 == (MethodInfo) null)
          {
            Debug.LogError((object) ("DevConsole: Failed to find Autocomplete method for " + data.Type.Name));
            Debug.LogError((object) "Method should have signature: public List<string> FetchAutocompleteOptions(string command, string[] tokens)");
          }
        }
      }
      List<string> list = this.HelpMethods.Keys.ToList<string>();
      list.Sort();
      this.CommandList = list;
    }

    private ConsoleCommandsFrame NetworkCommandsFrame
    {
      get
      {
        if (this._netorkCommandsFrame != null)
          return this._netorkCommandsFrame;
        if (!ConnectionHandler.Initialized)
          return (ConsoleCommandsFrame) null;
        this._netorkCommandsFrame = new ConsoleCommandsFrame();
        this._netorkCommandsFrame.OnCommandResult = new Action<AdminCmdResultEvent>(ConsoleDaemon._Instance.OnNetworkCommandResultEvent);
        return this._netorkCommandsFrame;
      }
    }

    private void OnNetworkCommandResultEvent(AdminCmdResultEvent result) => this.OnAddTextToConsole.Invoke(string.Format("<color={0}>[{1}]: {2}</color> ", result.Success ? (object) "#00ff00ff" : (object) "#ff0000ff", (object) result.Id, (object) result.Result) + Environment.NewLine);

    public string GetWelcomeMessage() => "<b>Waven 0.1.1.6169 Developer Console </b>" + Environment.NewLine + "Type <b>help</b> and press <b>enter/return</b> to view available commands. Press <b>tab</b> when entering a command to attempt autocomplete.";

    public string GetHelp(string commandId, bool verbose)
    {
      if (!this.AlternateCommandNames.ContainsKey(commandId))
        return "";
      return this.HelpMethods[this.AlternateCommandNames[commandId]].Invoke((object) null, new object[2]
      {
        (object) commandId,
        (object) verbose
      }) as string;
    }

    public List<string> History => new List<string>((IEnumerable<string>) this.CommandHistory);

    public string GetHistory_Previous()
    {
      if (this.HistoryIndex >= this.CommandHistory.Count)
        return (string) null;
      string historyPrevious = this.CommandHistory[this.HistoryIndex];
      this.HistoryIndex = this.HistoryIndex >= 1 ? this.HistoryIndex - 1 : this.HistoryIndex;
      return historyPrevious;
    }

    public string GetHistory_Next()
    {
      if (this.HistoryIndex >= this.CommandHistory.Count)
        return (string) null;
      string historyNext = this.CommandHistory[this.HistoryIndex];
      this.HistoryIndex = this.HistoryIndex < this.CommandHistory.Count - 1 ? this.HistoryIndex + 1 : this.HistoryIndex;
      return historyNext;
    }

    public void WriteToConsole(string rawString) => this.OnAddTextToConsole.Invoke(rawString);

    public string ExecuteCommand(string commandString) => this.ExecuteCommandInternal(commandString);

    protected string ExecuteCommandInternal(string commandString)
    {
      this.OnCommandEntered.Invoke(commandString);
      this.CommandHistory.Add(commandString);
      while (this.CommandHistory.Count > this.MaxHistory)
        this.CommandHistory.RemoveAt(0);
      this.HistoryIndex = this.CommandHistory.Count - 1;
      List<string> stringList = this.TokeniseString(commandString);
      if (stringList.Count == 0)
        return "[Error] Tried to execute a blank command.";
      string key = stringList[0];
      stringList.RemoveAt(0);
      if (!this.AlternateCommandNames.ContainsKey(key))
        return "[Error] The command '" + key + "' could not be found.";
      string alternateCommandName = this.AlternateCommandNames[key];
      if (this.ExecuteMethods.ContainsKey(alternateCommandName))
        return this.ExecuteMethods[alternateCommandName].Invoke((object) null, new object[1]
        {
          (object) stringList.ToArray()
        }) as string;
      if (this.NetworkCommandsFrame == null)
        return "Network not initialized";
      int num = this.NextAsyncCommandId++;
      if (!(this.NetworkExecuteMethods[alternateCommandName].Invoke((object) null, new object[1]
      {
        (object) stringList.ToArray()
      }) is AdminCmd cmd))
        return "no command started.";
      cmd.Id = num;
      this.NetworkCommandsFrame.Send(cmd);
      return string.Format("<color={0}>[{1}] {2}</color>", (object) "#aaaaaaaa", (object) num, (object) key);
    }

    public List<string> FetchAutocompleteOptions(string commandString)
    {
      if (string.IsNullOrEmpty(commandString))
        return (List<string>) null;
      List<string> tokens = this.TokeniseString(commandString);
      if (tokens.Count > 1)
      {
        List<string> stringList = this.FetchCommandAutocomplete(tokens);
        stringList?.Sort();
        return stringList;
      }
      List<string> list = this.AlternateCommandNames.Keys.Where<string>((Func<string, bool>) (name => name.StartsWith(tokens[0]))).ToList<string>();
      if (list.Count == 0)
      {
        string lowerToken = tokens[0].ToLower();
        list = this.AlternateCommandNames.Keys.Where<string>((Func<string, bool>) (name => name.ToLower().StartsWith(lowerToken))).ToList<string>();
      }
      if (!list.Contains(tokens[0]))
        return list;
      List<string> stringList1 = this.FetchCommandAutocomplete(tokens);
      stringList1?.Sort();
      return stringList1;
    }

    protected List<string> FetchCommandAutocomplete(List<string> tokens)
    {
      string token = tokens[0];
      if (!this.AlternateCommandNames.ContainsKey(token))
        return (List<string>) null;
      string alternateCommandName = this.AlternateCommandNames[token];
      tokens.RemoveAt(0);
      if (this.FetchAutocompleteMethods[alternateCommandName].Invoke((object) null, new object[2]
      {
        (object) token,
        (object) tokens.ToArray()
      }) is List<string> stringList)
        stringList.Sort();
      return stringList;
    }

    private List<string> TokeniseString(string inputString)
    {
      List<string> stringList = new List<string>();
      string str1 = "";
      bool flag = false;
      char ch;
      for (int index = 0; index < inputString.Length; ++index)
      {
        if (inputString[index] == ' ')
        {
          if (!flag)
          {
            if (str1.Length > 0)
              stringList.Add(str1);
            str1 = "";
          }
          else
          {
            string str2 = str1;
            ch = inputString[index];
            string str3 = ch.ToString();
            str1 = str2 + str3;
          }
        }
        else if (inputString[index] == '"')
        {
          flag = !flag;
        }
        else
        {
          string str4 = str1;
          ch = inputString[index];
          string str5 = ch.ToString();
          str1 = str4 + str5;
        }
      }
      if (str1.Length > 0)
        stringList.Add(str1);
      return stringList;
    }
  }
}
