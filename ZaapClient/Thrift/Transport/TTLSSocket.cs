// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TTLSSocket
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Thrift.Transport
{
  public class TTLSSocket : TStreamTransport
  {
    private TcpClient client;
    private string host;
    private int port;
    private int timeout;
    private SslStream secureStream;
    private bool isServer;
    private X509Certificate certificate;
    private RemoteCertificateValidationCallback certValidator;
    private LocalCertificateSelectionCallback localCertificateSelectionCallback;
    private readonly SslProtocols sslProtocols;

    public TTLSSocket(
      TcpClient client,
      X509Certificate certificate,
      bool isServer = false,
      RemoteCertificateValidationCallback certValidator = null,
      LocalCertificateSelectionCallback localCertificateSelectionCallback = null,
      SslProtocols sslProtocols = SslProtocols.Tls)
    {
      this.client = client;
      this.certificate = certificate;
      this.certValidator = certValidator;
      this.localCertificateSelectionCallback = localCertificateSelectionCallback;
      this.sslProtocols = sslProtocols;
      this.isServer = isServer;
      if (isServer && certificate == null)
        throw new ArgumentException("TTLSSocket needs certificate to be used for server", nameof (certificate));
      if (!this.IsOpen)
        return;
      this.inputStream = (Stream) client.GetStream();
      this.outputStream = (Stream) client.GetStream();
    }

    public TTLSSocket(
      string host,
      int port,
      string certificatePath,
      RemoteCertificateValidationCallback certValidator = null,
      LocalCertificateSelectionCallback localCertificateSelectionCallback = null,
      SslProtocols sslProtocols = SslProtocols.Tls)
      : this(host, port, 0, X509Certificate.CreateFromCertFile(certificatePath), certValidator, localCertificateSelectionCallback, sslProtocols)
    {
    }

    public TTLSSocket(
      string host,
      int port,
      X509Certificate certificate = null,
      RemoteCertificateValidationCallback certValidator = null,
      LocalCertificateSelectionCallback localCertificateSelectionCallback = null,
      SslProtocols sslProtocols = SslProtocols.Tls)
      : this(host, port, 0, certificate, certValidator, localCertificateSelectionCallback, sslProtocols)
    {
    }

    public TTLSSocket(
      string host,
      int port,
      int timeout,
      X509Certificate certificate,
      RemoteCertificateValidationCallback certValidator = null,
      LocalCertificateSelectionCallback localCertificateSelectionCallback = null,
      SslProtocols sslProtocols = SslProtocols.Tls)
    {
      this.host = host;
      this.port = port;
      this.timeout = timeout;
      this.certificate = certificate;
      this.certValidator = certValidator;
      this.localCertificateSelectionCallback = localCertificateSelectionCallback;
      this.sslProtocols = sslProtocols;
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

    private bool DefaultCertificateValidator(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslValidationErrors)
    {
      return sslValidationErrors == SslPolicyErrors.None;
    }

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
      this.client.Connect(this.host, this.port);
      this.setupTLS();
    }

    public void setupTLS()
    {
      RemoteCertificateValidationCallback userCertificateValidationCallback = this.certValidator ?? new RemoteCertificateValidationCallback(this.DefaultCertificateValidator);
      this.secureStream = this.localCertificateSelectionCallback == null ? new SslStream((Stream) this.client.GetStream(), false, userCertificateValidationCallback) : new SslStream((Stream) this.client.GetStream(), false, userCertificateValidationCallback, this.localCertificateSelectionCallback);
      try
      {
        if (this.isServer)
        {
          this.secureStream.AuthenticateAsServer(this.certificate, this.certValidator != null, this.sslProtocols, true);
        }
        else
        {
          X509CertificateCollection clientCertificates;
          if (this.certificate != null)
            clientCertificates = new X509CertificateCollection()
            {
              this.certificate
            };
          else
            clientCertificates = new X509CertificateCollection();
          this.secureStream.AuthenticateAsClient(this.host, clientCertificates, this.sslProtocols, true);
        }
      }
      catch (Exception ex)
      {
        this.Close();
        throw;
      }
      this.inputStream = (Stream) this.secureStream;
      this.outputStream = (Stream) this.secureStream;
    }

    public override void Close()
    {
      base.Close();
      if (this.client != null)
      {
        this.client.Close();
        this.client = (TcpClient) null;
      }
      if (this.secureStream == null)
        return;
      this.secureStream.Close();
      this.secureStream = (SslStream) null;
    }
  }
}
