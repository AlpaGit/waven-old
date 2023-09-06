// Decompiled with JetBrains decompiler
// Type: Thrift.Server.TSimpleServer
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
  public class TSimpleServer : TServer
  {
    private bool stop;

    public TSimpleServer(TProcessor processor, TServerTransport serverTransport)
    {
      TProcessor processor1 = processor;
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = new TTransportFactory();
      TTransportFactory outputTransportFactory = new TTransportFactory();
      TBinaryProtocol.Factory inputProtocolFactory = new TBinaryProtocol.Factory();
      TBinaryProtocol.Factory outputProtocolFactory = new TBinaryProtocol.Factory();
      // ISSUE: reference to a compiler-generated field
      if (TSimpleServer.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TSimpleServer.\u003C\u003Ef__mg\u0024cache0 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache0 = TSimpleServer.\u003C\u003Ef__mg\u0024cache0;
      // ISSUE: explicit constructor call
      base.\u002Ector(processor1, serverTransport1, inputTransportFactory, outputTransportFactory, (TProtocolFactory) inputProtocolFactory, (TProtocolFactory) outputProtocolFactory, fMgCache0);
    }

    public TSimpleServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TServer.LogDelegate logDel)
      : base(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), (TProtocolFactory) new TBinaryProtocol.Factory(), (TProtocolFactory) new TBinaryProtocol.Factory(), logDel)
    {
    }

    public TSimpleServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TTransportFactory transportFactory)
    {
      TProcessor processor1 = processor;
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = transportFactory;
      TTransportFactory outputTransportFactory = transportFactory;
      TBinaryProtocol.Factory inputProtocolFactory = new TBinaryProtocol.Factory();
      TBinaryProtocol.Factory outputProtocolFactory = new TBinaryProtocol.Factory();
      // ISSUE: reference to a compiler-generated field
      if (TSimpleServer.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TSimpleServer.\u003C\u003Ef__mg\u0024cache1 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache1 = TSimpleServer.\u003C\u003Ef__mg\u0024cache1;
      // ISSUE: explicit constructor call
      base.\u002Ector(processor1, serverTransport1, inputTransportFactory, outputTransportFactory, (TProtocolFactory) inputProtocolFactory, (TProtocolFactory) outputProtocolFactory, fMgCache1);
    }

    public TSimpleServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TTransportFactory transportFactory,
      TProtocolFactory protocolFactory)
    {
      TProcessor processor1 = processor;
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = transportFactory;
      TTransportFactory outputTransportFactory = transportFactory;
      TProtocolFactory inputProtocolFactory = protocolFactory;
      TProtocolFactory outputProtocolFactory = protocolFactory;
      // ISSUE: reference to a compiler-generated field
      if (TSimpleServer.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TSimpleServer.\u003C\u003Ef__mg\u0024cache2 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache2 = TSimpleServer.\u003C\u003Ef__mg\u0024cache2;
      // ISSUE: explicit constructor call
      base.\u002Ector(processor1, serverTransport1, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, fMgCache2);
    }

    public TSimpleServer(
      TProcessorFactory processorFactory,
      TServerTransport serverTransport,
      TTransportFactory transportFactory,
      TProtocolFactory protocolFactory)
    {
      TProcessorFactory processorFactory1 = processorFactory;
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = transportFactory;
      TTransportFactory outputTransportFactory = transportFactory;
      TProtocolFactory inputProtocolFactory = protocolFactory;
      TProtocolFactory outputProtocolFactory = protocolFactory;
      // ISSUE: reference to a compiler-generated field
      if (TSimpleServer.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TSimpleServer.\u003C\u003Ef__mg\u0024cache3 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache3 = TSimpleServer.\u003C\u003Ef__mg\u0024cache3;
      // ISSUE: explicit constructor call
      base.\u002Ector(processorFactory1, serverTransport1, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, fMgCache3);
    }

    public override void Serve()
    {
      try
      {
        this.serverTransport.Listen();
      }
      catch (TTransportException ex)
      {
        this.logDelegate(ex.ToString());
        return;
      }
      if (this.serverEventHandler != null)
        this.serverEventHandler.preServe();
      while (!this.stop)
      {
        TProtocol tprotocol1 = (TProtocol) null;
        TProtocol tprotocol2 = (TProtocol) null;
        object serverContext = (object) null;
        try
        {
          TTransport trans;
          using (trans = this.serverTransport.Accept())
          {
            TProcessor processor = this.processorFactory.GetProcessor(trans);
            if (trans != null)
            {
              TTransport transport1;
              using (transport1 = this.inputTransportFactory.GetTransport(trans))
              {
                TTransport transport2;
                using (transport2 = this.outputTransportFactory.GetTransport(trans))
                {
                  tprotocol1 = this.inputProtocolFactory.GetProtocol(transport1);
                  tprotocol2 = this.outputProtocolFactory.GetProtocol(transport2);
                  if (this.serverEventHandler != null)
                    serverContext = this.serverEventHandler.createContext(tprotocol1, tprotocol2);
                  while (!this.stop)
                  {
                    if (transport1.Peek())
                    {
                      if (this.serverEventHandler != null)
                        this.serverEventHandler.processContext(serverContext, transport1);
                      if (!processor.Process(tprotocol1, tprotocol2))
                        break;
                    }
                    else
                      break;
                  }
                }
              }
            }
          }
        }
        catch (TTransportException ex)
        {
          if (this.stop)
          {
            if (ex.Type == TTransportException.ExceptionType.Interrupted)
              goto label_30;
          }
          this.logDelegate(ex.ToString());
        }
        catch (Exception ex)
        {
          this.logDelegate(ex.ToString());
        }
label_30:
        if (this.serverEventHandler != null)
          this.serverEventHandler.deleteContext(serverContext, tprotocol1, tprotocol2);
      }
    }

    public override void Stop()
    {
      this.stop = true;
      this.serverTransport.Close();
    }
  }
}
