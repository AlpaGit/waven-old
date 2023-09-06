// Decompiled with JetBrains decompiler
// Type: Thrift.TPrototypeProcessorFactory`2
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using Thrift.Server;
using Thrift.Transport;

namespace Thrift
{
  public class TPrototypeProcessorFactory<P, H> : TProcessorFactory where P : TProcessor
  {
    private object[] handlerArgs;

    public TPrototypeProcessorFactory() => this.handlerArgs = new object[0];

    public TPrototypeProcessorFactory(params object[] handlerArgs) => this.handlerArgs = handlerArgs;

    public TProcessor GetProcessor(TTransport trans, TServer server = null)
    {
      H instance = (H) Activator.CreateInstance(typeof (H), this.handlerArgs);
      if (instance is TControllingHandler tcontrollingHandler)
        tcontrollingHandler.server = server;
      return Activator.CreateInstance(typeof (P), (object) instance) as TProcessor;
    }
  }
}
