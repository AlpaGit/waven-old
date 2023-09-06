// Decompiled with JetBrains decompiler
// Type: Thrift.Server.TThreadPoolServer
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Threading;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
  public class TThreadPoolServer : TServer
  {
    private const int DEFAULT_MIN_THREADS = 10;
    private const int DEFAULT_MAX_THREADS = 100;
    private volatile bool stop;

    public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport)
    {
      TSingletonProcessorFactory processorFactory = new TSingletonProcessorFactory(processor);
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = new TTransportFactory();
      TTransportFactory outputTransportFactory = new TTransportFactory();
      TBinaryProtocol.Factory inputProtocolFactory = new TBinaryProtocol.Factory();
      TBinaryProtocol.Factory outputProtocolFactory = new TBinaryProtocol.Factory();
      // ISSUE: reference to a compiler-generated field
      if (TThreadPoolServer.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TThreadPoolServer.\u003C\u003Ef__mg\u0024cache0 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache0 = TThreadPoolServer.\u003C\u003Ef__mg\u0024cache0;
      // ISSUE: explicit constructor call
      this.\u002Ector((TProcessorFactory) processorFactory, serverTransport1, inputTransportFactory, outputTransportFactory, (TProtocolFactory) inputProtocolFactory, (TProtocolFactory) outputProtocolFactory, 10, 100, fMgCache0);
    }

    public TThreadPoolServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TServer.LogDelegate logDelegate)
      : this((TProcessorFactory) new TSingletonProcessorFactory(processor), serverTransport, new TTransportFactory(), new TTransportFactory(), (TProtocolFactory) new TBinaryProtocol.Factory(), (TProtocolFactory) new TBinaryProtocol.Factory(), 10, 100, logDelegate)
    {
    }

    public TThreadPoolServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TTransportFactory transportFactory,
      TProtocolFactory protocolFactory)
    {
      TSingletonProcessorFactory processorFactory = new TSingletonProcessorFactory(processor);
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = transportFactory;
      TTransportFactory outputTransportFactory = transportFactory;
      TProtocolFactory inputProtocolFactory = protocolFactory;
      TProtocolFactory outputProtocolFactory = protocolFactory;
      // ISSUE: reference to a compiler-generated field
      if (TThreadPoolServer.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TThreadPoolServer.\u003C\u003Ef__mg\u0024cache1 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache1 = TThreadPoolServer.\u003C\u003Ef__mg\u0024cache1;
      // ISSUE: explicit constructor call
      this.\u002Ector((TProcessorFactory) processorFactory, serverTransport1, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, 10, 100, fMgCache1);
    }

    public TThreadPoolServer(
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
      if (TThreadPoolServer.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TThreadPoolServer.\u003C\u003Ef__mg\u0024cache2 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache2 = TThreadPoolServer.\u003C\u003Ef__mg\u0024cache2;
      // ISSUE: explicit constructor call
      this.\u002Ector(processorFactory1, serverTransport1, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, 10, 100, fMgCache2);
    }

    public TThreadPoolServer(
      TProcessorFactory processorFactory,
      TServerTransport serverTransport,
      TTransportFactory inputTransportFactory,
      TTransportFactory outputTransportFactory,
      TProtocolFactory inputProtocolFactory,
      TProtocolFactory outputProtocolFactory,
      int minThreadPoolThreads,
      int maxThreadPoolThreads,
      TServer.LogDelegate logDel)
      : base(processorFactory, serverTransport, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, logDel)
    {
      lock ((object) typeof (TThreadPoolServer))
      {
        if (!ThreadPool.SetMaxThreads(maxThreadPoolThreads, maxThreadPoolThreads))
          throw new Exception("Error: could not SetMaxThreads in ThreadPool");
        if (!ThreadPool.SetMinThreads(minThreadPoolThreads, minThreadPoolThreads))
          throw new Exception("Error: could not SetMinThreads in ThreadPool");
      }
    }

    public override void Serve()
    {
      try
      {
        this.serverTransport.Listen();
      }
      catch (TTransportException ex)
      {
        this.logDelegate("Error, could not listen on ServerTransport: " + (object) ex);
        return;
      }
      if (this.serverEventHandler != null)
        this.serverEventHandler.preServe();
      while (!this.stop)
      {
        int num1 = 0;
        try
        {
          ThreadPool.QueueUserWorkItem(new WaitCallback(this.Execute), (object) this.serverTransport.Accept());
        }
        catch (TTransportException ex)
        {
          if (this.stop)
          {
            if (ex.Type == TTransportException.ExceptionType.Interrupted)
              continue;
          }
          int num2 = num1 + 1;
          this.logDelegate(ex.ToString());
        }
      }
      if (!this.stop)
        return;
      try
      {
        this.serverTransport.Close();
      }
      catch (TTransportException ex)
      {
        this.logDelegate("TServerTransport failed on close: " + ex.Message);
      }
      this.stop = false;
    }

    private void Execute(object threadContext)
    {
      TTransport trans1 = (TTransport) threadContext;
      TProcessor processor = this.processorFactory.GetProcessor(trans1, (TServer) this);
      TTransport ttransport = (TTransport) null;
      TTransport trans2 = (TTransport) null;
      TProtocol tprotocol1 = (TProtocol) null;
      TProtocol tprotocol2 = (TProtocol) null;
      object serverContext = (object) null;
      try
      {
        ttransport = this.inputTransportFactory.GetTransport(trans1);
        trans2 = this.outputTransportFactory.GetTransport(trans1);
        tprotocol1 = this.inputProtocolFactory.GetProtocol(ttransport);
        tprotocol2 = this.outputProtocolFactory.GetProtocol(trans2);
        if (this.serverEventHandler != null)
          serverContext = this.serverEventHandler.createContext(tprotocol1, tprotocol2);
        while (!this.stop)
        {
          if (ttransport.Peek())
          {
            if (this.serverEventHandler != null)
              this.serverEventHandler.processContext(serverContext, ttransport);
            if (!processor.Process(tprotocol1, tprotocol2))
              break;
          }
          else
            break;
        }
      }
      catch (TTransportException ex)
      {
      }
      catch (Exception ex)
      {
        this.logDelegate("Error: " + (object) ex);
      }
      if (this.serverEventHandler != null)
        this.serverEventHandler.deleteContext(serverContext, tprotocol1, tprotocol2);
      ttransport?.Close();
      trans2?.Close();
    }

    public override void Stop()
    {
      this.stop = true;
      this.serverTransport.Close();
    }
  }
}
