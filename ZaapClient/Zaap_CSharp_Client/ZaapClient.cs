// Decompiled with JetBrains decompiler
// Type: Zaap_CSharp_Client.ZaapClient
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using com.ankama.zaap;
using System;
using System.IO;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;

namespace Zaap_CSharp_Client
{
  public class ZaapClient
  {
    public const string PORT_VAR = "ZAAP_PORT";
    public const string GAME_NAME_VAR = "ZAAP_GAME";
    public const string GAME_RELEASE_VAR = "ZAAP_RELEASE";
    public const string INSTANCE_ID_VAR = "ZAAP_INSTANCE_ID";
    public const string HASH_VAR = "ZAAP_HASH";
    public const string CONFIGURATION_FILE = "credentials.json";
    public static string Host = "127.0.0.1";
    public static int DefaultPort = 26116;
    public string Session;
    private ZaapService.Client m_client;
    private TTransport m_connection;

    private ZaapClient(ZaapService.Client client, TTransport connection, string session)
    {
      this.m_client = client;
      this.m_connection = connection;
      this.Session = session;
    }

    public static ZaapClient Connect(ZaapClient.ParametersSources source = ZaapClient.ParametersSources.FILE_FIRST)
    {
      ZaapClientParameters parameters = (ZaapClientParameters) null;
      switch (source)
      {
        case ZaapClient.ParametersSources.ENV_VARIABLES_FIRST:
          parameters = ZaapClient.GetParametersFromEnv() ?? ZaapClient.GetParametersFromFile();
          break;
        case ZaapClient.ParametersSources.FILE_FIRST:
          parameters = ZaapClient.GetParametersFromFile() ?? ZaapClient.GetParametersFromEnv();
          break;
        case ZaapClient.ParametersSources.ONLY_ENV_VARIABLES:
          parameters = ZaapClient.GetParametersFromEnv();
          break;
        case ZaapClient.ParametersSources.ONLY_FILE:
          parameters = ZaapClient.GetParametersFromFile();
          break;
      }
      return ZaapClient.Connect(parameters);
    }

    public static ZaapClient Connect(ZaapClientParameters parameters)
    {
      if (parameters == null)
      {
        Console.WriteLine("[ZaapClient] Unable to connect to Zaap: No ZaapClientParameters");
        return (ZaapClient) null;
      }
      if (!parameters.Valid())
      {
        Console.WriteLine("[ZaapClient] Unable to connect to Zaap: Invalid ZaapClientParameters");
        return (ZaapClient) null;
      }
      Console.WriteLine("[ZaapClient] Trying to connect to port " + (object) parameters.port + " with game " + parameters.name + "/" + parameters.release + " (id: " + (object) parameters.instanceId + ", hash: " + parameters.hash + ")");
      return ZaapClient.Connect(parameters.port, parameters.name, parameters.release, parameters.instanceId, parameters.hash);
    }

    public static ZaapClient Connect(
      int port,
      string name,
      string release,
      int instanceId,
      string hash)
    {
      ZaapClient zaapClient = (ZaapClient) null;
      try
      {
        TTransport ttransport = (TTransport) new TSocket(ZaapClient.Host, port);
        ttransport.Open();
        ZaapService.Client client = new ZaapService.Client((TProtocol) new TBinaryProtocol(ttransport));
        string session = client.connect(name, release, instanceId, hash);
        zaapClient = new ZaapClient(client, ttransport, session);
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("[ZaapClient] Exception while connecting to Zaap: " + (object) ex);
      }
      return zaapClient;
    }

    public ZaapService.Client GetClient() => this.m_client != null && this.Session != null ? this.m_client : throw new TException("Client is not connected");

    public void Disconnect()
    {
      this.m_connection.Close();
      this.m_connection = (TTransport) null;
      this.m_client = (ZaapService.Client) null;
      this.Session = (string) null;
    }

    private static ZaapClientParameters GetParametersFromFile()
    {
      ZaapClientParameters parametersFromFile = (ZaapClientParameters) null;
      try
      {
        if (File.Exists("credentials.json"))
        {
          parametersFromFile = File.ReadAllText("credentials.json").FromJson<ZaapClientParameters>();
          if (!parametersFromFile.Valid())
          {
            Console.Error.WriteLine("Configuration file credentials.json is not a valid Json parameters file.");
            parametersFromFile = (ZaapClientParameters) null;
          }
        }
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Unable to read configuration file credentials.json: " + (object) ex);
      }
      return parametersFromFile;
    }

    private static ZaapClientParameters GetParametersFromEnv()
    {
      int environmentVariable1 = ZaapClient.GetIntFromEnvironmentVariable("ZAAP_PORT", ZaapClient.DefaultPort);
      string environmentVariable2 = Environment.GetEnvironmentVariable("ZAAP_GAME");
      string environmentVariable3 = Environment.GetEnvironmentVariable("ZAAP_RELEASE");
      int environmentVariable4 = ZaapClient.GetIntFromEnvironmentVariable("ZAAP_INSTANCE_ID", 0);
      string environmentVariable5 = Environment.GetEnvironmentVariable("ZAAP_HASH");
      return new ZaapClientParameters()
      {
        port = environmentVariable1,
        name = environmentVariable2,
        release = environmentVariable3,
        instanceId = environmentVariable4,
        hash = environmentVariable5
      };
    }

    private static int GetIntFromEnvironmentVariable(string name, int defaultValue)
    {
      string environmentVariable = Environment.GetEnvironmentVariable(name);
      if (string.IsNullOrEmpty(environmentVariable))
        return defaultValue;
      int result;
      int.TryParse(environmentVariable, out result);
      return result;
    }

    public enum ParametersSources
    {
      ENV_VARIABLES_FIRST,
      FILE_FIRST,
      ONLY_ENV_VARIABLES,
      ONLY_FILE,
    }
  }
}
