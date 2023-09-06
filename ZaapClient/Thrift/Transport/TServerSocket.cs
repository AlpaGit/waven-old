// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TServerSocket
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Net;
using System.Net.Sockets;

namespace Thrift.Transport
{
  public class TServerSocket : TServerTransport
  {
    private TcpListener server;
    private int port;
    private int clientTimeout;
    private bool useBufferedSockets;

    public TServerSocket(TcpListener listener)
      : this(listener, 0)
    {
    }

    public TServerSocket(TcpListener listener, int clientTimeout)
    {
      this.server = listener;
      this.clientTimeout = clientTimeout;
    }

    public TServerSocket(int port)
      : this(port, 0)
    {
    }

    public TServerSocket(int port, int clientTimeout)
      : this(port, clientTimeout, false)
    {
    }

    public TServerSocket(int port, int clientTimeout, bool useBufferedSockets)
    {
      this.port = port;
      this.clientTimeout = clientTimeout;
      this.useBufferedSockets = useBufferedSockets;
      try
      {
        this.server = new TcpListener(IPAddress.Any, this.port);
        this.server.Server.NoDelay = true;
      }
      catch (Exception ex)
      {
        this.server = (TcpListener) null;
        throw new TTransportException("Could not create ServerSocket on port " + (object) port + ".");
      }
    }

    public override void Listen()
    {
      if (this.server == null)
        return;
      try
      {
        this.server.Start();
      }
      catch (SocketException ex)
      {
        throw new TTransportException("Could not accept on listening socket: " + ex.Message);
      }
    }

    protected override TTransport AcceptImpl()
    {
      if (this.server == null)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, "No underlying server socket.");
      try
      {
        TSocket transport = (TSocket) null;
        TcpClient client = this.server.AcceptTcpClient();
        try
        {
          transport = new TSocket(client);
          transport.Timeout = this.clientTimeout;
          return this.useBufferedSockets ? (TTransport) new TBufferedTransport((TTransport) transport) : (TTransport) transport;
        }
        catch (Exception ex)
        {
          if (transport != null)
            transport.Dispose();
          else
            ((IDisposable) client).Dispose();
          throw;
        }
      }
      catch (Exception ex)
      {
        throw new TTransportException(ex.ToString());
      }
    }

    public override void Close()
    {
      if (this.server == null)
        return;
      try
      {
        this.server.Stop();
      }
      catch (Exception ex)
      {
        throw new TTransportException("WARNING: Could not close server socket: " + (object) ex);
      }
      this.server = (TcpListener) null;
    }
  }
}
