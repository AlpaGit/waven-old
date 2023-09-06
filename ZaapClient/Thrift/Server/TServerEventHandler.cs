// Decompiled with JetBrains decompiler
// Type: Thrift.Server.TServerEventHandler
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
  public interface TServerEventHandler
  {
    void preServe();

    object createContext(TProtocol input, TProtocol output);

    void deleteContext(object serverContext, TProtocol input, TProtocol output);

    void processContext(object serverContext, TTransport transport);
  }
}
