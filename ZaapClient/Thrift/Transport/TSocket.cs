// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TSocket
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.IO;
using System.Net.Sockets;

namespace Thrift.Transport
{
  public class TSocket : TStreamTransport
  {
    private TcpClient client;
    private string host;
    private int port;
    private int timeout;
    private bool _IsDisposed;

    public TSocket(TcpClient client)
    {
      this.client = client;
      if (!this.IsOpen)
        return;
      this.inputStream = (Stream) client.GetStream();
      this.outputStream = (Stream) client.GetStream();
    }

    public TSocket(string host, int port)
      : this(host, port, 0)
    {
    }

    public TSocket(string host, int port, int timeout)
    {
      this.host = host;
      this.port = port;
      this.timeout = timeout;
      this.InitSocket();
    }

    private void InitSocket()
    {
      this.client = new TcpClient();
      TcpClient client = this.client;
      int timeout = this.timeout;
      this.client.SendTimeout = timeout;
      int num = timeout;
      client.ReceiveTimeout = num;
      this.client.Client.NoDelay = true;
    }

    public int Timeout
    {
      set
      {
        TcpClient client = this.client;
        int num1 = this.timeout = value;
        this.client.SendTimeout = num1;
        int num2 = num1;
        client.ReceiveTimeout = num2;
      }
    }

    public TcpClient TcpClient => this.client;

    public string Host => this.host;

    public int Port => this.port;

    public override bool IsOpen => this.client != null && this.client.Connected;

    public override void Open()
    {
      if (this.IsOpen)
        throw new TTransportException(TTransportException.ExceptionType.AlreadyOpen, "Socket already connected");
      if (string.IsNullOrEmpty(this.host))
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open null host");
      if (this.port <= 0)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open without port");
      if (this.client == null)
        this.InitSocket();
      if (this.timeout == 0)
      {
        this.client.Connect(this.host, this.port);
      }
      else
      {
        TSocket.ConnectHelper state = new TSocket.ConnectHelper(this.client);
        IAsyncResult asyncResult = this.client.BeginConnect(this.host, this.port, new AsyncCallback(TSocket.ConnectCallback), (object) state);
        if (!asyncResult.AsyncWaitHandle.WaitOne(this.timeout) || !this.client.Connected)
        {
          lock (state.Mutex)
          {
            if (state.CallbackDone)
            {
              asyncResult.AsyncWaitHandle.Close();
              this.client.Close();
            }
            else
            {
              state.DoCleanup = true;
              this.client = (TcpClient) null;
            }
          }
          throw new TTransportException(TTransportException.ExceptionType.TimedOut, "Connect timed out");
        }
      }
      this.inputStream = (Stream) this.client.GetStream();
      this.outputStream = (Stream) this.client.GetStream();
    }

    private static void ConnectCallback(IAsyncResult asyncres)
    {
      TSocket.ConnectHelper asyncState = asyncres.AsyncState as TSocket.ConnectHelper;
      lock (asyncState.Mutex)
      {
        asyncState.CallbackDone = true;
        try
        {
          if (asyncState.Client.Client != null)
            asyncState.Client.EndConnect(asyncres);
        }
        catch (Exception ex)
        {
        }
        if (!asyncState.DoCleanup)
          return;
        try
        {
          asyncres.AsyncWaitHandle.Close();
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (asyncState.Client != null)
            ((IDisposable) asyncState.Client).Dispose();
        }
        catch (Exception ex)
        {
        }
        asyncState.Client = (TcpClient) null;
      }
    }

    public override void Close()
    {
      base.Close();
      if (this.client == null)
        return;
      this.client.Close();
      this.client = (TcpClient) null;
    }

    protected override void Dispose(bool disposing)
    {
      if (!this._IsDisposed && disposing)
      {
        if (this.client != null)
          ((IDisposable) this.client).Dispose();
        base.Dispose(disposing);
      }
      this._IsDisposed = true;
    }

    private class ConnectHelper
    {
      public object Mutex = new object();
      public bool DoCleanup;
      public bool CallbackDone;
      public TcpClient Client;

      public ConnectHelper(TcpClient client) => this.Client = client;
    }
  }
}
