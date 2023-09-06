// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TFramedTransport
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.IO;

namespace Thrift.Transport
{
  public class TFramedTransport : TTransport, IDisposable
  {
    private readonly TTransport transport;
    private readonly MemoryStream writeBuffer = new MemoryStream(1024);
    private readonly MemoryStream readBuffer = new MemoryStream(1024);
    private const int HeaderSize = 4;
    private readonly byte[] headerBuf = new byte[4];
    private bool _IsDisposed;

    public TFramedTransport(TTransport transport)
    {
      this.transport = transport != null ? transport : throw new ArgumentNullException(nameof (transport));
      this.InitWriteBuffer();
    }

    public override void Open()
    {
      this.CheckNotDisposed();
      this.transport.Open();
    }

    public override bool IsOpen => !this._IsDisposed && this.transport.IsOpen;

    public override void Close()
    {
      this.CheckNotDisposed();
      this.transport.Close();
    }

    public override int Read(byte[] buf, int off, int len)
    {
      this.CheckNotDisposed();
      TTransport.ValidateBufferArgs(buf, off, len);
      if (!this.IsOpen)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen);
      int num = this.readBuffer.Read(buf, off, len);
      if (num > 0)
        return num;
      this.ReadFrame();
      return this.readBuffer.Read(buf, off, len);
    }

    private void ReadFrame()
    {
      this.transport.ReadAll(this.headerBuf, 0, 4);
      int len = TFramedTransport.DecodeFrameSize(this.headerBuf);
      this.readBuffer.SetLength((long) len);
      this.readBuffer.Seek(0L, SeekOrigin.Begin);
      this.transport.ReadAll(this.readBuffer.GetBuffer(), 0, len);
    }

    public override void Write(byte[] buf, int off, int len)
    {
      this.CheckNotDisposed();
      TTransport.ValidateBufferArgs(buf, off, len);
      if (!this.IsOpen)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen);
      if (this.writeBuffer.Length + (long) len > (long) int.MaxValue)
        this.Flush();
      this.writeBuffer.Write(buf, off, len);
    }

    public override void Flush()
    {
      this.CheckNotDisposed();
      if (!this.IsOpen)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen);
      byte[] buffer = this.writeBuffer.GetBuffer();
      int length = (int) this.writeBuffer.Length;
      int frameSize = length - 4;
      if (frameSize < 0)
        throw new InvalidOperationException();
      TFramedTransport.EncodeFrameSize(frameSize, buffer);
      this.transport.Write(buffer, 0, length);
      this.InitWriteBuffer();
      this.transport.Flush();
    }

    private void InitWriteBuffer()
    {
      this.writeBuffer.SetLength(4L);
      this.writeBuffer.Seek(0L, SeekOrigin.End);
    }

    private static void EncodeFrameSize(int frameSize, byte[] buf)
    {
      buf[0] = (byte) ((int) byte.MaxValue & frameSize >> 24);
      buf[1] = (byte) ((int) byte.MaxValue & frameSize >> 16);
      buf[2] = (byte) ((int) byte.MaxValue & frameSize >> 8);
      buf[3] = (byte) ((int) byte.MaxValue & frameSize);
    }

    private static int DecodeFrameSize(byte[] buf) => ((int) buf[0] & (int) byte.MaxValue) << 24 | ((int) buf[1] & (int) byte.MaxValue) << 16 | ((int) buf[2] & (int) byte.MaxValue) << 8 | (int) buf[3] & (int) byte.MaxValue;

    private void CheckNotDisposed()
    {
      if (this._IsDisposed)
        throw new ObjectDisposedException(nameof (TFramedTransport));
    }

    protected override void Dispose(bool disposing)
    {
      if (!this._IsDisposed && disposing)
      {
        this.readBuffer.Dispose();
        this.writeBuffer.Dispose();
      }
      this._IsDisposed = true;
    }

    public class Factory : TTransportFactory
    {
      public override TTransport GetTransport(TTransport trans) => (TTransport) new TFramedTransport(trans);
    }
  }
}
