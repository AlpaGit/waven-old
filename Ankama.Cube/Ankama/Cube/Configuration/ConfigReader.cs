// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.ConfigReader
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Ankama.Cube.Configuration
{
  public class ConfigReader
  {
    private readonly JObject m_json;
    private readonly string m_context;

    public ConfigReader(string text, string context = null)
    {
      this.m_json = JObject.Parse(text);
      this.m_context = context ?? text;
    }

    public ConfigReader GetConfig(string propertyName)
    {
      JToken jtoken;
      if (this.m_json.TryGetValue(propertyName, out jtoken))
        return new ConfigReader(jtoken.ToString(), propertyName + " in " + this.m_context);
      this.LogPropertyNotFound(propertyName);
      return (ConfigReader) null;
    }

    public bool HasProperty(string propertyName) => this.m_json.Property(propertyName) != null;

    public string GetString(string propertyName, string defaultValue = "")
    {
      JToken jtoken;
      if (this.m_json.TryGetValue(propertyName, out jtoken))
        return (string) jtoken;
      this.LogPropertyNotFound(propertyName);
      return defaultValue;
    }

    public int GetInt(string propertyName, int defaultValue = 0)
    {
      JToken s;
      if (!this.m_json.TryGetValue(propertyName, out s))
      {
        this.LogPropertyNotFound(propertyName);
        return defaultValue;
      }
      int result;
      if (int.TryParse((string) s, out result))
        return result;
      this.LogWrongType<int>(s);
      return defaultValue;
    }

    public bool GetBool(string propertyName, bool defaultValue = false)
    {
      JToken jtoken;
      if (!this.m_json.TryGetValue(propertyName, out jtoken))
      {
        this.LogPropertyNotFound(propertyName);
        return defaultValue;
      }
      bool result;
      if (bool.TryParse((string) jtoken, out result))
        return result;
      this.LogWrongType<bool>(jtoken);
      return defaultValue;
    }

    public float GetFloat(string propertyName, float defaultValue = 0.0f)
    {
      JToken s;
      if (!this.m_json.TryGetValue(propertyName, out s))
      {
        this.LogPropertyNotFound(propertyName);
        return defaultValue;
      }
      float result;
      if (float.TryParse((string) s, out result))
        return result;
      this.LogWrongType<float>(s);
      return defaultValue;
    }

    public string GetIPAddress(string propertyName, string defaultValue = "")
    {
      JToken ipString;
      if (!this.m_json.TryGetValue(propertyName, out ipString))
      {
        this.LogPropertyNotFound(propertyName);
        return defaultValue;
      }
      IPAddress address;
      if (IPAddress.TryParse((string) ipString, out address))
        return address.ToString();
      this.LogWrongType<IPAddress>(ipString);
      return defaultValue;
    }

    public string GetUrl(string propertyName, string defaultValue = "")
    {
      JToken uriString;
      if (!this.m_json.TryGetValue(propertyName, out uriString))
      {
        this.LogPropertyNotFound(propertyName);
        return defaultValue;
      }
      Uri result;
      if (Uri.TryCreate((string) uriString, UriKind.Absolute, out result) && (!(result.Scheme != Uri.UriSchemeHttp) || !(result.Scheme != Uri.UriSchemeHttps)))
        return result.AbsoluteUri;
      this.LogWrongType<Uri>(uriString);
      return defaultValue;
    }

    public T GetEnum<T>(string propertyName, T defaultValue) where T : struct
    {
      JToken jtoken;
      if (!this.m_json.TryGetValue(propertyName, out jtoken))
      {
        this.LogPropertyNotFound(propertyName);
        return defaultValue;
      }
      T result;
      if (System.Enum.TryParse<T>((string) jtoken, true, out result))
        return result;
      this.LogWrongType<T>(jtoken);
      return defaultValue;
    }

    private void LogPropertyNotFound(string propertyName) => Log.Warning("'" + propertyName + "' not found in " + this.m_context + ".", 152, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ConfigReader.cs");

    private void LogWrongType<T>(JToken value) => Log.Warning(string.Format("'{0}' is not a valid {1} in {2}", (object) value, (object) typeof (T), (object) this.m_context), 157, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ConfigReader.cs");
  }
}
