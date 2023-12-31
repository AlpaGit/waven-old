﻿// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TStreamTransport
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System.IO;

namespace Thrift.Transport
{
  public class TStreamTransport : TTransport
  {
    protected Stream inputStream;
    protected Stream outputStream;
    private bool _IsDisposed;

    protected TStreamTransport()
    {
    }

    public TStreamTransport(Stream inputStream, Stream outputStream)
    {
      this.inputStream = inputStream;
      this.outputStream = outputStream;
    }

    public Stream OutputStream => this.outputStream;

    public Stream InputStream => this.inputStream;

    public override bool IsOpen => true;

    public override void Open()
    {
    }

    public override void Close()
    {
      if (this.inputStream != null)
      {
        this.inputStream.Close();
        this.inputStream = (Stream) null;
      }
      if (this.outputStream == null)
        return;
      this.outputStream.Close();
      this.outputStream = (Stream) null;
    }

    public override int Read(byte[] buf, int off, int len)
    {
      if (this.inputStream == null)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot read from null inputstream");
      return this.inputStream.Read(buf, off, len);
    }

    public override void Write(byte[] buf, int off, int len)
    {
      if (this.outputStream == null)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot write to null outputstream");
      this.outputStream.Write(buf, off, len);
    }

    public override void Flush()
    {
      if (this.outputStream == null)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot flush null outputstream");
      this.outputStream.Flush();
    }

    protected override void Dispose(bool disposing)
    {
      if (!this._IsDisposed && disposing)
      {
        if (this.InputStream != null)
          this.InputStream.Dispose();
        if (this.OutputStream != null)
          this.OutputStream.Dispose();
      }
      this._IsDisposed = true;
    }
  }
}
