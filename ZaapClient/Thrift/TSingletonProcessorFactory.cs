// Decompiled with JetBrains decompiler
// Type: Thrift.TSingletonProcessorFactory
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using Thrift.Server;
using Thrift.Transport;

namespace Thrift
{
  public class TSingletonProcessorFactory : TProcessorFactory
  {
    private readonly TProcessor processor_;

    public TSingletonProcessorFactory(TProcessor processor) => this.processor_ = processor;

    public TProcessor GetProcessor(TTransport trans, TServer server = null) => this.processor_;
  }
}
