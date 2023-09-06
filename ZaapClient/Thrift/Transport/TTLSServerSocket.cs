// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TTLSServerSocket
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Thrift.Transport
{
  public class TTLSServerSocket : TServerTransport
  {
    private TcpListener server;
    private int port;
    private readonly int clientTimeout;
    private bool useBufferedSockets;
    private X509Certificate serverCertificate;
    private RemoteCertificateValidationCallback clientCertValidator;
    private LocalCertificateSelectionCallback localCertificateSelectionCallback;
    private readonly SslProtocols sslProtocols;

    public TTLSServerSocket(int port, X509Certificate2 certificate)
      : this(port, 0, certificate)
    {
    }

    public TTLSServerSocket(int port, int clientTimeout, X509Certificate2 certificate)
      : this(port, clientTimeout, false, certificate)
    {
    }

    public TTLSServerSocket(
      int port,
      int clientTimeout,
      bool useBufferedSockets,
      X509Certificate2 certificate,
      RemoteCertificateValidationCallback clientCertValidator = null,
      LocalCertificateSelectionCallback localCertificateSelectionCallback = null,
      SslProtocols sslProtocols = SslProtocols.Tls)
    {
      if (!certificate.HasPrivateKey)
        throw new TTransportException(TTransportException.ExceptionType.Unknown, "Your server-certificate needs to have a private key");
      this.port = port;
      this.clientTimeout = clientTimeout;
      this.serverCertificate = (X509Certificate) certificate;
      this.useBufferedSockets = useBufferedSockets;
      this.clientCertValidator = clientCertValidator;
      this.localCertificateSelectionCallback = localCertificateSelectionCallback;
      this.sslProtocols = sslProtocols;
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
        TcpClient client = this.server.AcceptTcpClient();
        TcpClient tcpClient = client;
        int clientTimeout = this.clientTimeout;
        client.ReceiveTimeout = clientTimeout;
        int num = clientTimeout;
        tcpClient.SendTimeout = num;
        TTLSSocket transport = new TTLSSocket(client, this.serverCertificate, true, this.clientCertValidator, this.localCertificateSelectionCallback, this.sslProtocols);
        transport.setupTLS();
        return this.useBufferedSockets ? (TTransport) new TBufferedTransport((TTransport) transport) : (TTransport) transport;
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
