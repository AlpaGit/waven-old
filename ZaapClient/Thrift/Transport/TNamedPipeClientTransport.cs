// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TNamedPipeClientTransport
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System.IO.Pipes;

namespace Thrift.Transport
{
  public class TNamedPipeClientTransport : TTransport
  {
    private NamedPipeClientStream client;
    private string ServerName;
    private string PipeName;

    public TNamedPipeClientTransport(string pipe)
    {
      this.ServerName = ".";
      this.PipeName = pipe;
    }

    public TNamedPipeClientTransport(string server, string pipe)
    {
      this.ServerName = !(server != string.Empty) ? "." : server;
      this.PipeName = pipe;
    }

    public override bool IsOpen => this.client != null && this.client.IsConnected;

    public override void Open()
    {
      if (this.IsOpen)
        throw new TTransportException(TTransportException.ExceptionType.AlreadyOpen);
      this.client = new NamedPipeClientStream(this.ServerName, this.PipeName, PipeDirection.InOut, PipeOptions.None);
      this.client.Connect();
    }

    public override void Close()
    {
      if (this.client == null)
        return;
      this.client.Close();
      this.client = (NamedPipeClientStream) null;
    }

    public override int Read(byte[] buf, int off, int len)
    {
      if (this.client == null)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen);
      return this.client.Read(buf, off, len);
    }

    public override void Write(byte[] buf, int off, int len)
    {
      if (this.client == null)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen);
      this.client.Write(buf, off, len);
    }

    protected override void Dispose(bool disposing) => this.client.Dispose();
  }
}
