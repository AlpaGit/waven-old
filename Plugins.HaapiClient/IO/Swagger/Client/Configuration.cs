// Decompiled with JetBrains decompiler
// Type: IO.Swagger.Client.Configuration
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IO.Swagger.Client
{
  public class Configuration
  {
    public const string Version = "1.0.0";
    public static ApiClient DefaultApiClient = new ApiClient();
    public static Dictionary<string, string> ApiKey = new Dictionary<string, string>();
    public static Dictionary<string, string> ApiKeyPrefix = new Dictionary<string, string>();
    private static string _tempFolderPath = Path.GetTempPath();
    private const string ISO8601_DATETIME_FORMAT = "o";
    private static string _dateTimeFormat = "o";

    public static string Username { get; set; }

    public static string Password { get; set; }

    public static string TempFolderPath
    {
      get => Configuration._tempFolderPath;
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          Configuration._tempFolderPath = value;
        }
        else
        {
          if (!Directory.Exists(value))
            Directory.CreateDirectory(value);
          if ((int) value[value.Length - 1] == (int) Path.DirectorySeparatorChar)
            Configuration._tempFolderPath = value;
          else
            Configuration._tempFolderPath = value + Path.DirectorySeparatorChar.ToString();
        }
      }
    }

    public static string DateTimeFormat
    {
      get => Configuration._dateTimeFormat;
      set
      {
        if (string.IsNullOrEmpty(value))
          Configuration._dateTimeFormat = "o";
        else
          Configuration._dateTimeFormat = value;
      }
    }

    public static string ToDebugReport() => "C# SDK (Com.Ankama.Haapi.Swagger) Debug Report:\n" + "    OS: " + (object) Environment.OSVersion + "\n" + "    .NET Framework Version: " + ((IEnumerable<AssemblyName>) Assembly.GetExecutingAssembly().GetReferencedAssemblies()).Where<AssemblyName>((Func<AssemblyName, bool>) (x => x.Name == "System.Core")).First<AssemblyName>().Version.ToString() + "\n" + "    Version of the API: 2\n" + "    SDK Package Version: 1.0.0\n";
  }
}
