// Decompiled with JetBrains decompiler
// Type: Thrift.Server.TThreadedServer
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Collections.Generic;
using System.Threading;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
  public class TThreadedServer : TServer
  {
    private const int DEFAULT_MAX_THREADS = 100;
    private volatile bool stop;
    private readonly int maxThreads;
    private Queue<TTransport> clientQueue;
    private THashSet<Thread> clientThreads;
    private object clientLock;
    private Thread workerThread;

    public TThreadedServer(TProcessor processor, TServerTransport serverTransport)
    {
      TSingletonProcessorFactory processorFactory = new TSingletonProcessorFactory(processor);
      TServerTransport serverTransport1 = serverTransport;
      TTransportFactory inputTransportFactory = new TTransportFactory();
      TTransportFactory outputTransportFactory = new TTransportFactory();
      TBinaryProtocol.Factory inputProtocolFactory = new TBinaryProtocol.Factory();
      TBinaryProtocol.Factory outputProtocolFactory = new TBinaryProtocol.Factory();
      // ISSUE: reference to a compiler-generated field
      if (TThreadedServer.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TThreadedServer.\u003C\u003Ef__mg\u0024cache0 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache0 = TThreadedServer.\u003C\u003Ef__mg\u0024cache0;
      // ISSUE: explicit constructor call
      this.\u002Ector((TProcessorFactory) processorFactory, serverTransport1, inputTransportFactory, outputTransportFactory, (TProtocolFactory) inputProtocolFactory, (TProtocolFactory) outputProtocolFactory, 100, fMgCache0);
    }

    public TThreadedServer(
      TProcessor processor,
      TServerTransport serverTransport,
      TServer.LogDelegate logDelegate)
      : this((TProcessorFactory) new TSingletonProcessorFactory(processor), serverTransport, new TTransportFactory(), new TTransportFactory(), (TProtocolFactory) new TBinaryProtocol.Factory(), (TProtocolFactory) new TBinaryProtocol.Factory(), 100, logDelegate)
    {
    }

    public TThreadedServer(
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
      if (TThreadedServer.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TThreadedServer.\u003C\u003Ef__mg\u0024cache1 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache1 = TThreadedServer.\u003C\u003Ef__mg\u0024cache1;
      // ISSUE: explicit constructor call
      this.\u002Ector((TProcessorFactory) processorFactory, serverTransport1, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, 100, fMgCache1);
    }

    public TThreadedServer(
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
      if (TThreadedServer.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TThreadedServer.\u003C\u003Ef__mg\u0024cache2 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
      }
      // ISSUE: reference to a compiler-generated field
      TServer.LogDelegate fMgCache2 = TThreadedServer.\u003C\u003Ef__mg\u0024cache2;
      // ISSUE: explicit constructor call
      this.\u002Ector(processorFactory1, serverTransport1, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, 100, fMgCache2);
    }

    public TThreadedServer(
      TProcessorFactory processorFactory,
      TServerTransport serverTransport,
      TTransportFactory inputTransportFactory,
      TTransportFactory outputTransportFactory,
      TProtocolFactory inputProtocolFactory,
      TProtocolFactory outputProtocolFactory,
      int maxThreads,
      TServer.LogDelegate logDel)
      : base(processorFactory, serverTransport, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, logDel)
    {
      this.maxThreads = maxThreads;
      this.clientQueue = new Queue<TTransport>();
      this.clientLock = new object();
      this.clientThreads = new THashSet<Thread>();
    }

    public int ClientThreadsCount => this.clientThreads.Count;

    public override void Serve()
    {
      try
      {
        this.workerThread = new Thread(new ThreadStart(this.Execute));
        this.workerThread.Start();
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
          TTransport ttransport = this.serverTransport.Accept();
          lock (this.clientLock)
          {
            this.clientQueue.Enqueue(ttransport);
            Monitor.Pulse(this.clientLock);
          }
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
        this.logDelegate("TServeTransport failed on close: " + ex.Message);
      }
      this.stop = false;
    }

    private void Execute()
    {
      while (!this.stop)
      {
        TTransport parameter;
        Thread thread;
        lock (this.clientLock)
        {
          while (this.clientThreads.Count >= this.maxThreads)
            Monitor.Wait(this.clientLock);
          while (this.clientQueue.Count == 0)
            Monitor.Wait(this.clientLock);
          parameter = this.clientQueue.Dequeue();
          thread = new Thread(new ParameterizedThreadStart(this.ClientWorker));
          this.clientThreads.Add(thread);
        }
        thread.Start((object) parameter);
      }
    }

    private void ClientWorker(object context)
    {
      TTransport trans = (TTransport) context;
      TProcessor processor = this.processorFactory.GetProcessor(trans);
      TProtocol tprotocol1 = (TProtocol) null;
      TProtocol tprotocol2 = (TProtocol) null;
      object serverContext = (object) null;
      try
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
      catch (TTransportException ex)
      {
      }
      catch (Exception ex)
      {
        this.logDelegate("Error: " + (object) ex);
      }
      if (this.serverEventHandler != null)
        this.serverEventHandler.deleteContext(serverContext, tprotocol1, tprotocol2);
      lock (this.clientLock)
      {
        this.clientThreads.Remove(Thread.CurrentThread);
        Monitor.Pulse(this.clientLock);
      }
    }

    public override void Stop()
    {
      this.stop = true;
      this.serverTransport.Close();
      this.workerThread.Abort();
      foreach (Thread clientThread in this.clientThreads)
        clientThread.Abort();
    }
  }
}
