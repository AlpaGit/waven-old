// Decompiled with JetBrains decompiler
// Type: com.ankama.zaap.ZaapService
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;

namespace com.ankama.zaap
{
  public class ZaapService
  {
    public interface ISync
    {
      string connect(string gameName, string releaseName, int instanceId, string hash);

      string auth_getGameToken(string gameSession, int gameId);

      bool updater_isUpdateAvailable(string gameSession);

      string settings_get(string gameSession, string key);

      void settings_set(string gameSession, string key, string value);
    }

    public interface Iface : ZaapService.ISync
    {
    }

    public class Client : IDisposable, ZaapService.Iface, ZaapService.ISync
    {
      protected TProtocol iprot_;
      protected TProtocol oprot_;
      protected int seqid_;
      private bool _IsDisposed;

      public Client(TProtocol prot)
        : this(prot, prot)
      {
      }

      public Client(TProtocol iprot, TProtocol oprot)
      {
        this.iprot_ = iprot;
        this.oprot_ = oprot;
      }

      public TProtocol InputProtocol => this.iprot_;

      public TProtocol OutputProtocol => this.oprot_;

      public void Dispose() => this.Dispose(true);

      protected virtual void Dispose(bool disposing)
      {
        if (!this._IsDisposed && disposing)
        {
          if (this.iprot_ != null)
            this.iprot_.Dispose();
          if (this.oprot_ != null)
            this.oprot_.Dispose();
        }
        this._IsDisposed = true;
      }

      public string connect(string gameName, string releaseName, int instanceId, string hash)
      {
        this.send_connect(gameName, releaseName, instanceId, hash);
        return this.recv_connect();
      }

      public void send_connect(string gameName, string releaseName, int instanceId, string hash)
      {
        this.oprot_.WriteMessageBegin(new TMessage("connect", TMessageType.Call, this.seqid_));
        new ZaapService.connect_args()
        {
          GameName = gameName,
          ReleaseName = releaseName,
          InstanceId = instanceId,
          Hash = hash
        }.Write(this.oprot_);
        this.oprot_.WriteMessageEnd();
        this.oprot_.Transport.Flush();
      }

      public string recv_connect()
      {
        if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
        {
          TApplicationException tapplicationException = TApplicationException.Read(this.iprot_);
          this.iprot_.ReadMessageEnd();
          throw tapplicationException;
        }
        ZaapService.connect_result connectResult = new ZaapService.connect_result();
        connectResult.Read(this.iprot_);
        this.iprot_.ReadMessageEnd();
        if (connectResult.__isset.success)
          return connectResult.Success;
        if (connectResult.__isset.error)
          throw connectResult.Error;
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "connect failed: unknown result");
      }

      public string auth_getGameToken(string gameSession, int gameId)
      {
        this.send_auth_getGameToken(gameSession, gameId);
        return this.recv_auth_getGameToken();
      }

      public void send_auth_getGameToken(string gameSession, int gameId)
      {
        this.oprot_.WriteMessageBegin(new TMessage("auth_getGameToken", TMessageType.Call, this.seqid_));
        new ZaapService.auth_getGameToken_args()
        {
          GameSession = gameSession,
          GameId = gameId
        }.Write(this.oprot_);
        this.oprot_.WriteMessageEnd();
        this.oprot_.Transport.Flush();
      }

      public string recv_auth_getGameToken()
      {
        if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
        {
          TApplicationException tapplicationException = TApplicationException.Read(this.iprot_);
          this.iprot_.ReadMessageEnd();
          throw tapplicationException;
        }
        ZaapService.auth_getGameToken_result getGameTokenResult = new ZaapService.auth_getGameToken_result();
        getGameTokenResult.Read(this.iprot_);
        this.iprot_.ReadMessageEnd();
        if (getGameTokenResult.__isset.success)
          return getGameTokenResult.Success;
        if (getGameTokenResult.__isset.error)
          throw getGameTokenResult.Error;
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "auth_getGameToken failed: unknown result");
      }

      public bool updater_isUpdateAvailable(string gameSession)
      {
        this.send_updater_isUpdateAvailable(gameSession);
        return this.recv_updater_isUpdateAvailable();
      }

      public void send_updater_isUpdateAvailable(string gameSession)
      {
        this.oprot_.WriteMessageBegin(new TMessage("updater_isUpdateAvailable", TMessageType.Call, this.seqid_));
        new ZaapService.updater_isUpdateAvailable_args()
        {
          GameSession = gameSession
        }.Write(this.oprot_);
        this.oprot_.WriteMessageEnd();
        this.oprot_.Transport.Flush();
      }

      public bool recv_updater_isUpdateAvailable()
      {
        if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
        {
          TApplicationException tapplicationException = TApplicationException.Read(this.iprot_);
          this.iprot_.ReadMessageEnd();
          throw tapplicationException;
        }
        ZaapService.updater_isUpdateAvailable_result updateAvailableResult = new ZaapService.updater_isUpdateAvailable_result();
        updateAvailableResult.Read(this.iprot_);
        this.iprot_.ReadMessageEnd();
        if (updateAvailableResult.__isset.success)
          return updateAvailableResult.Success;
        if (updateAvailableResult.__isset.error)
          throw updateAvailableResult.Error;
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "updater_isUpdateAvailable failed: unknown result");
      }

      public string settings_get(string gameSession, string key)
      {
        this.send_settings_get(gameSession, key);
        return this.recv_settings_get();
      }

      public void send_settings_get(string gameSession, string key)
      {
        this.oprot_.WriteMessageBegin(new TMessage("settings_get", TMessageType.Call, this.seqid_));
        new ZaapService.settings_get_args()
        {
          GameSession = gameSession,
          Key = key
        }.Write(this.oprot_);
        this.oprot_.WriteMessageEnd();
        this.oprot_.Transport.Flush();
      }

      public string recv_settings_get()
      {
        if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
        {
          TApplicationException tapplicationException = TApplicationException.Read(this.iprot_);
          this.iprot_.ReadMessageEnd();
          throw tapplicationException;
        }
        ZaapService.settings_get_result settingsGetResult = new ZaapService.settings_get_result();
        settingsGetResult.Read(this.iprot_);
        this.iprot_.ReadMessageEnd();
        if (settingsGetResult.__isset.success)
          return settingsGetResult.Success;
        if (settingsGetResult.__isset.error)
          throw settingsGetResult.Error;
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "settings_get failed: unknown result");
      }

      public void settings_set(string gameSession, string key, string value)
      {
        this.send_settings_set(gameSession, key, value);
        this.recv_settings_set();
      }

      public void send_settings_set(string gameSession, string key, string value)
      {
        this.oprot_.WriteMessageBegin(new TMessage("settings_set", TMessageType.Call, this.seqid_));
        new ZaapService.settings_set_args()
        {
          GameSession = gameSession,
          Key = key,
          Value = value
        }.Write(this.oprot_);
        this.oprot_.WriteMessageEnd();
        this.oprot_.Transport.Flush();
      }

      public void recv_settings_set()
      {
        if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
        {
          TApplicationException tapplicationException = TApplicationException.Read(this.iprot_);
          this.iprot_.ReadMessageEnd();
          throw tapplicationException;
        }
        ZaapService.settings_set_result settingsSetResult = new ZaapService.settings_set_result();
        settingsSetResult.Read(this.iprot_);
        this.iprot_.ReadMessageEnd();
        if (settingsSetResult.__isset.error)
          throw settingsSetResult.Error;
      }
    }

    public class Processor : TProcessor
    {
      private ZaapService.ISync iface_;
      protected Dictionary<string, ZaapService.Processor.ProcessFunction> processMap_ = new Dictionary<string, ZaapService.Processor.ProcessFunction>();

      public Processor(ZaapService.ISync iface)
      {
        this.iface_ = iface;
        this.processMap_["connect"] = new ZaapService.Processor.ProcessFunction(this.connect_Process);
        this.processMap_["auth_getGameToken"] = new ZaapService.Processor.ProcessFunction(this.auth_getGameToken_Process);
        this.processMap_["updater_isUpdateAvailable"] = new ZaapService.Processor.ProcessFunction(this.updater_isUpdateAvailable_Process);
        this.processMap_["settings_get"] = new ZaapService.Processor.ProcessFunction(this.settings_get_Process);
        this.processMap_["settings_set"] = new ZaapService.Processor.ProcessFunction(this.settings_set_Process);
      }

      public bool Process(TProtocol iprot, TProtocol oprot)
      {
        try
        {
          TMessage tmessage = iprot.ReadMessageBegin();
          ZaapService.Processor.ProcessFunction processFunction;
          this.processMap_.TryGetValue(tmessage.Name, out processFunction);
          if (processFunction == null)
          {
            TProtocolUtil.Skip(iprot, TType.Struct);
            iprot.ReadMessageEnd();
            TApplicationException tapplicationException = new TApplicationException(TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + tmessage.Name + "'");
            oprot.WriteMessageBegin(new TMessage(tmessage.Name, TMessageType.Exception, tmessage.SeqID));
            tapplicationException.Write(oprot);
            oprot.WriteMessageEnd();
            oprot.Transport.Flush();
            return true;
          }
          processFunction(tmessage.SeqID, iprot, oprot);
        }
        catch (IOException ex)
        {
          return false;
        }
        return true;
      }

      public void connect_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        ZaapService.connect_args connectArgs = new ZaapService.connect_args();
        connectArgs.Read(iprot);
        iprot.ReadMessageEnd();
        ZaapService.connect_result connectResult = new ZaapService.connect_result();
        try
        {
          try
          {
            connectResult.Success = this.iface_.connect(connectArgs.GameName, connectArgs.ReleaseName, connectArgs.InstanceId, connectArgs.Hash);
          }
          catch (ZaapError ex)
          {
            connectResult.Error = ex;
          }
          oprot.WriteMessageBegin(new TMessage("connect", TMessageType.Reply, seqid));
          connectResult.Write(oprot);
        }
        catch (TTransportException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException tapplicationException = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
          oprot.WriteMessageBegin(new TMessage("connect", TMessageType.Exception, seqid));
          tapplicationException.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

      public void auth_getGameToken_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        ZaapService.auth_getGameToken_args getGameTokenArgs = new ZaapService.auth_getGameToken_args();
        getGameTokenArgs.Read(iprot);
        iprot.ReadMessageEnd();
        ZaapService.auth_getGameToken_result getGameTokenResult = new ZaapService.auth_getGameToken_result();
        try
        {
          try
          {
            getGameTokenResult.Success = this.iface_.auth_getGameToken(getGameTokenArgs.GameSession, getGameTokenArgs.GameId);
          }
          catch (ZaapError ex)
          {
            getGameTokenResult.Error = ex;
          }
          oprot.WriteMessageBegin(new TMessage("auth_getGameToken", TMessageType.Reply, seqid));
          getGameTokenResult.Write(oprot);
        }
        catch (TTransportException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException tapplicationException = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
          oprot.WriteMessageBegin(new TMessage("auth_getGameToken", TMessageType.Exception, seqid));
          tapplicationException.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

      public void updater_isUpdateAvailable_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        ZaapService.updater_isUpdateAvailable_args updateAvailableArgs = new ZaapService.updater_isUpdateAvailable_args();
        updateAvailableArgs.Read(iprot);
        iprot.ReadMessageEnd();
        ZaapService.updater_isUpdateAvailable_result updateAvailableResult = new ZaapService.updater_isUpdateAvailable_result();
        try
        {
          try
          {
            updateAvailableResult.Success = this.iface_.updater_isUpdateAvailable(updateAvailableArgs.GameSession);
          }
          catch (ZaapError ex)
          {
            updateAvailableResult.Error = ex;
          }
          oprot.WriteMessageBegin(new TMessage("updater_isUpdateAvailable", TMessageType.Reply, seqid));
          updateAvailableResult.Write(oprot);
        }
        catch (TTransportException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException tapplicationException = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
          oprot.WriteMessageBegin(new TMessage("updater_isUpdateAvailable", TMessageType.Exception, seqid));
          tapplicationException.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

      public void settings_get_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        ZaapService.settings_get_args settingsGetArgs = new ZaapService.settings_get_args();
        settingsGetArgs.Read(iprot);
        iprot.ReadMessageEnd();
        ZaapService.settings_get_result settingsGetResult = new ZaapService.settings_get_result();
        try
        {
          try
          {
            settingsGetResult.Success = this.iface_.settings_get(settingsGetArgs.GameSession, settingsGetArgs.Key);
          }
          catch (ZaapError ex)
          {
            settingsGetResult.Error = ex;
          }
          oprot.WriteMessageBegin(new TMessage("settings_get", TMessageType.Reply, seqid));
          settingsGetResult.Write(oprot);
        }
        catch (TTransportException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException tapplicationException = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
          oprot.WriteMessageBegin(new TMessage("settings_get", TMessageType.Exception, seqid));
          tapplicationException.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

      public void settings_set_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        ZaapService.settings_set_args settingsSetArgs = new ZaapService.settings_set_args();
        settingsSetArgs.Read(iprot);
        iprot.ReadMessageEnd();
        ZaapService.settings_set_result settingsSetResult = new ZaapService.settings_set_result();
        try
        {
          try
          {
            this.iface_.settings_set(settingsSetArgs.GameSession, settingsSetArgs.Key, settingsSetArgs.Value);
          }
          catch (ZaapError ex)
          {
            settingsSetResult.Error = ex;
          }
          oprot.WriteMessageBegin(new TMessage("settings_set", TMessageType.Reply, seqid));
          settingsSetResult.Write(oprot);
        }
        catch (TTransportException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException tapplicationException = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
          oprot.WriteMessageBegin(new TMessage("settings_set", TMessageType.Exception, seqid));
          tapplicationException.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

      protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
    }

    [Serializable]
    public class connect_args : TBase, TAbstractBase
    {
      private string _gameName;
      private string _releaseName;
      private int _instanceId;
      private string _hash;
      public ZaapService.connect_args.Isset __isset;

      public string GameName
      {
        get => this._gameName;
        set
        {
          this.__isset.gameName = true;
          this._gameName = value;
        }
      }

      public string ReleaseName
      {
        get => this._releaseName;
        set
        {
          this.__isset.releaseName = true;
          this._releaseName = value;
        }
      }

      public int InstanceId
      {
        get => this._instanceId;
        set
        {
          this.__isset.instanceId = true;
          this._instanceId = value;
        }
      }

      public string Hash
      {
        get => this._hash;
        set
        {
          this.__isset.hash = true;
          this._hash = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 1:
                  if (tfield.Type == TType.String)
                  {
                    this.GameName = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 2:
                  if (tfield.Type == TType.String)
                  {
                    this.ReleaseName = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 3:
                  if (tfield.Type == TType.I32)
                  {
                    this.InstanceId = iprot.ReadI32();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 4:
                  if (tfield.Type == TType.String)
                  {
                    this.Hash = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (connect_args));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.GameName != null && this.__isset.gameName)
          {
            field.Name = "gameName";
            field.Type = TType.String;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.GameName);
            oprot.WriteFieldEnd();
          }
          if (this.ReleaseName != null && this.__isset.releaseName)
          {
            field.Name = "releaseName";
            field.Type = TType.String;
            field.ID = (short) 2;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.ReleaseName);
            oprot.WriteFieldEnd();
          }
          if (this.__isset.instanceId)
          {
            field.Name = "instanceId";
            field.Type = TType.I32;
            field.ID = (short) 3;
            oprot.WriteFieldBegin(field);
            oprot.WriteI32(this.InstanceId);
            oprot.WriteFieldEnd();
          }
          if (this.Hash != null && this.__isset.hash)
          {
            field.Name = "hash";
            field.Type = TType.String;
            field.ID = (short) 4;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.Hash);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("connect_args(");
        bool flag = true;
        if (this.GameName != null && this.__isset.gameName)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("GameName: ");
          stringBuilder.Append(this.GameName);
        }
        if (this.ReleaseName != null && this.__isset.releaseName)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("ReleaseName: ");
          stringBuilder.Append(this.ReleaseName);
        }
        if (this.__isset.instanceId)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("InstanceId: ");
          stringBuilder.Append(this.InstanceId);
        }
        if (this.Hash != null && this.__isset.hash)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Hash: ");
          stringBuilder.Append(this.Hash);
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool gameName;
        public bool releaseName;
        public bool instanceId;
        public bool hash;
      }
    }

    [Serializable]
    public class connect_result : TBase, TAbstractBase
    {
      private string _success;
      private ZaapError _error;
      public ZaapService.connect_result.Isset __isset;

      public string Success
      {
        get => this._success;
        set
        {
          this.__isset.success = true;
          this._success = value;
        }
      }

      public ZaapError Error
      {
        get => this._error;
        set
        {
          this.__isset.error = true;
          this._error = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 0:
                  if (tfield.Type == TType.String)
                  {
                    this.Success = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 1:
                  if (tfield.Type == TType.Struct)
                  {
                    this.Error = new ZaapError();
                    this.Error.Read(iprot);
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (connect_result));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.__isset.success)
          {
            if (this.Success != null)
            {
              field.Name = "Success";
              field.Type = TType.String;
              field.ID = (short) 0;
              oprot.WriteFieldBegin(field);
              oprot.WriteString(this.Success);
              oprot.WriteFieldEnd();
            }
          }
          else if (this.__isset.error && this.Error != null)
          {
            field.Name = "Error";
            field.Type = TType.Struct;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            this.Error.Write(oprot);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("connect_result(");
        bool flag = true;
        if (this.Success != null && this.__isset.success)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("Success: ");
          stringBuilder.Append(this.Success);
        }
        if (this.Error != null && this.__isset.error)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Error: ");
          stringBuilder.Append(this.Error != null ? this.Error.ToString() : "<null>");
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool success;
        public bool error;
      }
    }

    [Serializable]
    public class auth_getGameToken_args : TBase, TAbstractBase
    {
      private string _gameSession;
      private int _gameId;
      public ZaapService.auth_getGameToken_args.Isset __isset;

      public string GameSession
      {
        get => this._gameSession;
        set
        {
          this.__isset.gameSession = true;
          this._gameSession = value;
        }
      }

      public int GameId
      {
        get => this._gameId;
        set
        {
          this.__isset.gameId = true;
          this._gameId = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 1:
                  if (tfield.Type == TType.String)
                  {
                    this.GameSession = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 2:
                  if (tfield.Type == TType.I32)
                  {
                    this.GameId = iprot.ReadI32();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (auth_getGameToken_args));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.GameSession != null && this.__isset.gameSession)
          {
            field.Name = "gameSession";
            field.Type = TType.String;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.GameSession);
            oprot.WriteFieldEnd();
          }
          if (this.__isset.gameId)
          {
            field.Name = "gameId";
            field.Type = TType.I32;
            field.ID = (short) 2;
            oprot.WriteFieldBegin(field);
            oprot.WriteI32(this.GameId);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("auth_getGameToken_args(");
        bool flag = true;
        if (this.GameSession != null && this.__isset.gameSession)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("GameSession: ");
          stringBuilder.Append(this.GameSession);
        }
        if (this.__isset.gameId)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("GameId: ");
          stringBuilder.Append(this.GameId);
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool gameSession;
        public bool gameId;
      }
    }

    [Serializable]
    public class auth_getGameToken_result : TBase, TAbstractBase
    {
      private string _success;
      private ZaapError _error;
      public ZaapService.auth_getGameToken_result.Isset __isset;

      public string Success
      {
        get => this._success;
        set
        {
          this.__isset.success = true;
          this._success = value;
        }
      }

      public ZaapError Error
      {
        get => this._error;
        set
        {
          this.__isset.error = true;
          this._error = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 0:
                  if (tfield.Type == TType.String)
                  {
                    this.Success = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 1:
                  if (tfield.Type == TType.Struct)
                  {
                    this.Error = new ZaapError();
                    this.Error.Read(iprot);
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (auth_getGameToken_result));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.__isset.success)
          {
            if (this.Success != null)
            {
              field.Name = "Success";
              field.Type = TType.String;
              field.ID = (short) 0;
              oprot.WriteFieldBegin(field);
              oprot.WriteString(this.Success);
              oprot.WriteFieldEnd();
            }
          }
          else if (this.__isset.error && this.Error != null)
          {
            field.Name = "Error";
            field.Type = TType.Struct;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            this.Error.Write(oprot);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("auth_getGameToken_result(");
        bool flag = true;
        if (this.Success != null && this.__isset.success)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("Success: ");
          stringBuilder.Append(this.Success);
        }
        if (this.Error != null && this.__isset.error)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Error: ");
          stringBuilder.Append(this.Error != null ? this.Error.ToString() : "<null>");
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool success;
        public bool error;
      }
    }

    [Serializable]
    public class updater_isUpdateAvailable_args : TBase, TAbstractBase
    {
      private string _gameSession;
      public ZaapService.updater_isUpdateAvailable_args.Isset __isset;

      public string GameSession
      {
        get => this._gameSession;
        set
        {
          this.__isset.gameSession = true;
          this._gameSession = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              if (tfield.ID == (short) 1)
              {
                if (tfield.Type == TType.String)
                  this.GameSession = iprot.ReadString();
                else
                  TProtocolUtil.Skip(iprot, tfield.Type);
              }
              else
                TProtocolUtil.Skip(iprot, tfield.Type);
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (updater_isUpdateAvailable_args));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.GameSession != null && this.__isset.gameSession)
          {
            field.Name = "gameSession";
            field.Type = TType.String;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.GameSession);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("updater_isUpdateAvailable_args(");
        bool flag = true;
        if (this.GameSession != null && this.__isset.gameSession)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("GameSession: ");
          stringBuilder.Append(this.GameSession);
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool gameSession;
      }
    }

    [Serializable]
    public class updater_isUpdateAvailable_result : TBase, TAbstractBase
    {
      private bool _success;
      private ZaapError _error;
      public ZaapService.updater_isUpdateAvailable_result.Isset __isset;

      public bool Success
      {
        get => this._success;
        set
        {
          this.__isset.success = true;
          this._success = value;
        }
      }

      public ZaapError Error
      {
        get => this._error;
        set
        {
          this.__isset.error = true;
          this._error = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 0:
                  if (tfield.Type == TType.Bool)
                  {
                    this.Success = iprot.ReadBool();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 1:
                  if (tfield.Type == TType.Struct)
                  {
                    this.Error = new ZaapError();
                    this.Error.Read(iprot);
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (updater_isUpdateAvailable_result));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.__isset.success)
          {
            field.Name = "Success";
            field.Type = TType.Bool;
            field.ID = (short) 0;
            oprot.WriteFieldBegin(field);
            oprot.WriteBool(this.Success);
            oprot.WriteFieldEnd();
          }
          else if (this.__isset.error && this.Error != null)
          {
            field.Name = "Error";
            field.Type = TType.Struct;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            this.Error.Write(oprot);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("updater_isUpdateAvailable_result(");
        bool flag = true;
        if (this.__isset.success)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("Success: ");
          stringBuilder.Append(this.Success);
        }
        if (this.Error != null && this.__isset.error)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Error: ");
          stringBuilder.Append(this.Error != null ? this.Error.ToString() : "<null>");
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool success;
        public bool error;
      }
    }

    [Serializable]
    public class settings_get_args : TBase, TAbstractBase
    {
      private string _gameSession;
      private string _key;
      public ZaapService.settings_get_args.Isset __isset;

      public string GameSession
      {
        get => this._gameSession;
        set
        {
          this.__isset.gameSession = true;
          this._gameSession = value;
        }
      }

      public string Key
      {
        get => this._key;
        set
        {
          this.__isset.key = true;
          this._key = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 1:
                  if (tfield.Type == TType.String)
                  {
                    this.GameSession = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 2:
                  if (tfield.Type == TType.String)
                  {
                    this.Key = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (settings_get_args));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.GameSession != null && this.__isset.gameSession)
          {
            field.Name = "gameSession";
            field.Type = TType.String;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.GameSession);
            oprot.WriteFieldEnd();
          }
          if (this.Key != null && this.__isset.key)
          {
            field.Name = "key";
            field.Type = TType.String;
            field.ID = (short) 2;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.Key);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("settings_get_args(");
        bool flag = true;
        if (this.GameSession != null && this.__isset.gameSession)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("GameSession: ");
          stringBuilder.Append(this.GameSession);
        }
        if (this.Key != null && this.__isset.key)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Key: ");
          stringBuilder.Append(this.Key);
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool gameSession;
        public bool key;
      }
    }

    [Serializable]
    public class settings_get_result : TBase, TAbstractBase
    {
      private string _success;
      private ZaapError _error;
      public ZaapService.settings_get_result.Isset __isset;

      public string Success
      {
        get => this._success;
        set
        {
          this.__isset.success = true;
          this._success = value;
        }
      }

      public ZaapError Error
      {
        get => this._error;
        set
        {
          this.__isset.error = true;
          this._error = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 0:
                  if (tfield.Type == TType.String)
                  {
                    this.Success = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 1:
                  if (tfield.Type == TType.Struct)
                  {
                    this.Error = new ZaapError();
                    this.Error.Read(iprot);
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (settings_get_result));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.__isset.success)
          {
            if (this.Success != null)
            {
              field.Name = "Success";
              field.Type = TType.String;
              field.ID = (short) 0;
              oprot.WriteFieldBegin(field);
              oprot.WriteString(this.Success);
              oprot.WriteFieldEnd();
            }
          }
          else if (this.__isset.error && this.Error != null)
          {
            field.Name = "Error";
            field.Type = TType.Struct;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            this.Error.Write(oprot);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("settings_get_result(");
        bool flag = true;
        if (this.Success != null && this.__isset.success)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("Success: ");
          stringBuilder.Append(this.Success);
        }
        if (this.Error != null && this.__isset.error)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Error: ");
          stringBuilder.Append(this.Error != null ? this.Error.ToString() : "<null>");
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool success;
        public bool error;
      }
    }

    [Serializable]
    public class settings_set_args : TBase, TAbstractBase
    {
      private string _gameSession;
      private string _key;
      private string _value;
      public ZaapService.settings_set_args.Isset __isset;

      public string GameSession
      {
        get => this._gameSession;
        set
        {
          this.__isset.gameSession = true;
          this._gameSession = value;
        }
      }

      public string Key
      {
        get => this._key;
        set
        {
          this.__isset.key = true;
          this._key = value;
        }
      }

      public string Value
      {
        get => this._value;
        set
        {
          this.__isset.value = true;
          this._value = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              switch (tfield.ID)
              {
                case 1:
                  if (tfield.Type == TType.String)
                  {
                    this.GameSession = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 2:
                  if (tfield.Type == TType.String)
                  {
                    this.Key = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                case 3:
                  if (tfield.Type == TType.String)
                  {
                    this.Value = iprot.ReadString();
                    break;
                  }
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
                default:
                  TProtocolUtil.Skip(iprot, tfield.Type);
                  break;
              }
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (settings_set_args));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.GameSession != null && this.__isset.gameSession)
          {
            field.Name = "gameSession";
            field.Type = TType.String;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.GameSession);
            oprot.WriteFieldEnd();
          }
          if (this.Key != null && this.__isset.key)
          {
            field.Name = "key";
            field.Type = TType.String;
            field.ID = (short) 2;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.Key);
            oprot.WriteFieldEnd();
          }
          if (this.Value != null && this.__isset.value)
          {
            field.Name = "value";
            field.Type = TType.String;
            field.ID = (short) 3;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(this.Value);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("settings_set_args(");
        bool flag = true;
        if (this.GameSession != null && this.__isset.gameSession)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("GameSession: ");
          stringBuilder.Append(this.GameSession);
        }
        if (this.Key != null && this.__isset.key)
        {
          if (!flag)
            stringBuilder.Append(", ");
          flag = false;
          stringBuilder.Append("Key: ");
          stringBuilder.Append(this.Key);
        }
        if (this.Value != null && this.__isset.value)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Value: ");
          stringBuilder.Append(this.Value);
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool gameSession;
        public bool key;
        public bool value;
      }
    }

    [Serializable]
    public class settings_set_result : TBase, TAbstractBase
    {
      private ZaapError _error;
      public ZaapService.settings_set_result.Isset __isset;

      public ZaapError Error
      {
        get => this._error;
        set
        {
          this.__isset.error = true;
          this._error = value;
        }
      }

      public void Read(TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          iprot.ReadStructBegin();
          while (true)
          {
            TField tfield = iprot.ReadFieldBegin();
            if (tfield.Type != TType.Stop)
            {
              if (tfield.ID == (short) 1)
              {
                if (tfield.Type == TType.Struct)
                {
                  this.Error = new ZaapError();
                  this.Error.Read(iprot);
                }
                else
                  TProtocolUtil.Skip(iprot, tfield.Type);
              }
              else
                TProtocolUtil.Skip(iprot, tfield.Type);
              iprot.ReadFieldEnd();
            }
            else
              break;
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot)
      {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct(nameof (settings_set_result));
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (this.__isset.error && this.Error != null)
          {
            field.Name = "Error";
            field.Type = TType.Struct;
            field.ID = (short) 1;
            oprot.WriteFieldBegin(field);
            this.Error.Write(oprot);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder("settings_set_result(");
        bool flag = true;
        if (this.Error != null && this.__isset.error)
        {
          if (!flag)
            stringBuilder.Append(", ");
          stringBuilder.Append("Error: ");
          stringBuilder.Append(this.Error != null ? this.Error.ToString() : "<null>");
        }
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }

      [Serializable]
      public struct Isset
      {
        public bool error;
      }
    }
  }
}
