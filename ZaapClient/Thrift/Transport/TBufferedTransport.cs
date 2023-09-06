// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TBufferedTransport
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.IO;

namespace Thrift.Transport
{
  public class TBufferedTransport : TTransport, IDisposable
  {
    private readonly int bufSize;
    private readonly MemoryStream inputBuffer = new MemoryStream(0);
    private readonly MemoryStream outputBuffer = new MemoryStream(0);
    private readonly TTransport transport;
    private bool _IsDisposed;

    public TBufferedTransport(TTransport transport, int bufSize = 1024)
    {
      if (transport == null)
        throw new ArgumentNullException(nameof (transport));
      if (bufSize <= 0)
        throw new ArgumentException(nameof (bufSize), "Buffer size must be a positive number.");
      this.transport = transport;
      this.bufSize = bufSize;
    }

    public TTransport UnderlyingTransport
    {
      get
      {
        this.CheckNotDisposed();
        return this.transport;
      }
    }

    public override bool IsOpen => !this._IsDisposed && this.transport.IsOpen;

    public override void Open()
    {
      this.CheckNotDisposed();
      this.transport.Open();
    }

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
      if (this.inputBuffer.Capacity < this.bufSize)
        this.inputBuffer.Capacity = this.bufSize;
      int num1 = this.inputBuffer.Read(buf, off, len);
      if (num1 > 0)
        return num1;
      this.inputBuffer.Seek(0L, SeekOrigin.Begin);
      this.inputBuffer.SetLength((long) this.inputBuffer.Capacity);
      int num2 = this.transport.Read(this.inputBuffer.GetBuffer(), 0, (int) this.inputBuffer.Length);
      this.inputBuffer.SetLength((long) num2);
      return num2 == 0 ? 0 : this.Read(buf, off, len);
    }

    public override void Write(byte[] buf, int off, int len)
    {
      this.CheckNotDisposed();
      TTransport.ValidateBufferArgs(buf, off, len);
      if (!this.IsOpen)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen);
      int num1 = 0;
      if (this.outputBuffer.Length > 0L)
      {
        int num2 = (int) ((long) this.outputBuffer.Capacity - this.outputBuffer.Length);
        int count = num2 > len ? len : num2;
        this.outputBuffer.Write(buf, off, count);
        num1 += count;
        if (count == num2)
        {
          this.transport.Write(this.outputBuffer.GetBuffer(), 0, (int) this.outputBuffer.Length);
          this.outputBuffer.SetLength(0L);
        }
      }
      for (; len - num1 >= this.bufSize; num1 += this.bufSize)
        this.transport.Write(buf, off + num1, this.bufSize);
      int count1 = len - num1;
      if (count1 <= 0)
        return;
      if (this.outputBuffer.Capacity < this.bufSize)
        this.outputBuffer.Capacity = this.bufSize;
      this.outputBuffer.Write(buf, off + num1, count1);
    }

    public override void Flush()
    {
      this.CheckNotDisposed();
      if (!this.IsOpen)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen);
      if (this.outputBuffer.Length > 0L)
      {
        this.transport.Write(this.outputBuffer.GetBuffer(), 0, (int) this.outputBuffer.Length);
        this.outputBuffer.SetLength(0L);
      }
      this.transport.Flush();
    }

    private void CheckNotDisposed()
    {
      if (this._IsDisposed)
        throw new ObjectDisposedException(nameof (TBufferedTransport));
    }

    protected override void Dispose(bool disposing)
    {
      if (!this._IsDisposed && disposing)
      {
        this.inputBuffer.Dispose();
        this.outputBuffer.Dispose();
      }
      this._IsDisposed = true;
    }
  }
}
