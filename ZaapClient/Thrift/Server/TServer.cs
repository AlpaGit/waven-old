// Decompiled with JetBrains decompiler
// Type: Thrift.Server.TServer
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
  public abstract class TServer
  {
    protected TProcessorFactory processorFactory;
    protected TServerTransport serverTransport;
    protected TTransportFactory inputTransportFactory;
    protected TTransportFactory outputTransportFactory;
    protected TProtocolFactory inputProtocolFactory;
    protected TProtocolFactory outputProtocolFactory;
    protected TServerEventHandler serverEventHandler;
    private TServer.LogDelegate _logDelegate;

    public TServer(TProcessor processor, TServerTransport serverTransport)
    {
      TProcessor processor1 = processor;
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = new TTransportFactory();
      TTransportFactory outputTransportFactory = new TTransportFactory();
      TBinaryProtocol.Factory inputProtocolFactory = new TBinaryProtocol.Factory();
      TBinaryProtocol.Factory outputProtocolFactory = new TBinaryProtocol.Factory();
      // ISSUE: reference to a compiler-generated field
      if (TServer.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TServer.\u003C\u003Ef__mg\u0024cache1 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache1 = TServer.\u003C\u003Ef__mg\u0024cache1;
      // ISSUE: explicit constructor call
      this.\u002Ector(processor1, serverTransport1, inputTransportFactory, outputTransportFactory, (TProtocolFactory) inputProtocolFactory, (TProtocolFactory) outputProtocolFactory, fMgCache1);
    }

    public TServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TServer.LogDelegate logDelegate)
      : this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), (TProtocolFactory) new TBinaryProtocol.Factory(), (TProtocolFactory) new TBinaryProtocol.Factory(), logDelegate)
    {
    }

    public TServer(
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
      if (TServer.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TServer.\u003C\u003Ef__mg\u0024cache2 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache2 = TServer.\u003C\u003Ef__mg\u0024cache2;
      // ISSUE: explicit constructor call
      this.\u002Ector(processor1, serverTransport1, inputTransportFactory, outputTransportFactory, (TProtocolFactory) inputProtocolFactory, (TProtocolFactory) outputProtocolFactory, fMgCache2);
    }

    public TServer(
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
      if (TServer.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TServer.\u003C\u003Ef__mg\u0024cache3 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache3 = TServer.\u003C\u003Ef__mg\u0024cache3;
      // ISSUE: explicit constructor call
      this.\u002Ector(processor1, serverTransport1, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, fMgCache3);
    }

    public TServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TTransportFactory inputTransportFactory,
      TTransportFactory outputTransportFactory,
      TProtocolFactory inputProtocolFactory,
      TProtocolFactory outputProtocolFactory,
      TServer.LogDelegate logDelegate)
    {
      this.processorFactory = (TProcessorFactory) new TSingletonProcessorFactory(processor);
      this.serverTransport = serverTransport;
      this.inputTransportFactory = inputTransportFactory;
      this.outputTransportFactory = outputTransportFactory;
      this.inputProtocolFactory = inputProtocolFactory;
      this.outputProtocolFactory = outputProtocolFactory;
      TServer.LogDelegate logDelegate1;
      if (logDelegate != null)
      {
        logDelegate1 = logDelegate;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (TServer.\u003C\u003Ef__mg\u0024cache4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          TServer.\u003C\u003Ef__mg\u0024cache4 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
        }
        // ISSUE: reference to a compiler-generated field
        logDelegate1 = TServer.\u003C\u003Ef__mg\u0024cache4;
      }
      this.logDelegate = logDelegate1;
    }

    public TServer(
      TProcessorFactory processorFactory,
      TServerTransport serverTransport,
      TTransportFactory inputTransportFactory,
      TTransportFactory outputTransportFactory,
      TProtocolFactory inputProtocolFactory,
      TProtocolFactory outputProtocolFactory,
      TServer.LogDelegate logDelegate)
    {
      this.processorFactory = processorFactory;
      this.serverTransport = serverTransport;
      this.inputTransportFactory = inputTransportFactory;
      this.outputTransportFactory = outputTransportFactory;
      this.inputProtocolFactory = inputProtocolFactory;
      this.outputProtocolFactory = outputProtocolFactory;
      TServer.LogDelegate logDelegate1;
      if (logDelegate != null)
      {
        logDelegate1 = logDelegate;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (TServer.\u003C\u003Ef__mg\u0024cache5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          TServer.\u003C\u003Ef__mg\u0024cache5 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
        }
        // ISSUE: reference to a compiler-generated field
        logDelegate1 = TServer.\u003C\u003Ef__mg\u0024cache5;
      }
      this.logDelegate = logDelegate1;
    }

    public void setEventHandler(TServerEventHandler seh) => this.serverEventHandler = seh;

    public TServerEventHandler getEventHandler() => this.serverEventHandler;

    protected TServer.LogDelegate logDelegate
    {
      get => this._logDelegate;
      set
      {
        TServer.LogDelegate logDelegate;
        if (value != null)
        {
          logDelegate = value;
        }
        else
        {
          if (TServer.\u003C\u003Ef__mg\u0024cache0 == null)
            TServer.\u003C\u003Ef__mg\u0024cache0 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
          logDelegate = TServer.\u003C\u003Ef__mg\u0024cache0;
        }
        this._logDelegate = logDelegate;
      }
    }

    protected static void DefaultLogDelegate(string s) => Console.Error.WriteLine(s);

    public abstract void Serve();

    public abstract void Stop();

    public delegate void LogDelegate(string str);
  }
}
