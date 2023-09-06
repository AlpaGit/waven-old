// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TTransport
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.IO;

namespace Thrift.Transport
{
  public abstract class TTransport : IDisposable
  {
    private byte[] _peekBuffer = new byte[1];
    private bool _hasPeekByte;

    public abstract bool IsOpen { get; }

    public bool Peek()
    {
      if (this._hasPeekByte)
        return true;
      if (!this.IsOpen)
        return false;
      try
      {
        if (this.Read(this._peekBuffer, 0, 1) == 0)
          return false;
      }
      catch (IOException ex)
      {
        return false;
      }
      this._hasPeekByte = true;
      return true;
    }

    public abstract void Open();

    public abstract void Close();

    protected static void ValidateBufferArgs(byte[] buf, int off, int len)
    {
      if (buf == null)
        throw new ArgumentNullException(nameof (buf));
      if (off < 0)
        throw new ArgumentOutOfRangeException("Buffer offset is smaller than zero.");
      if (len < 0)
        throw new ArgumentOutOfRangeException("Buffer length is smaller than zero.");
      if (off + len > buf.Length)
        throw new ArgumentOutOfRangeException("Not enough data.");
    }

    public abstract int Read(byte[] buf, int off, int len);

    public int ReadAll(byte[] buf, int off, int len)
    {
      TTransport.ValidateBufferArgs(buf, off, len);
      int num1 = 0;
      if (this._hasPeekByte)
      {
        buf[off + num1++] = this._peekBuffer[0];
        this._hasPeekByte = false;
      }
      int num2;
      for (; num1 < len; num1 += num2)
      {
        num2 = this.Read(buf, off + num1, len - num1);
        if (num2 <= 0)
          throw new TTransportException(TTransportException.ExceptionType.EndOfFile, "Cannot read, Remote side has closed");
      }
      return num1;
    }

    public virtual void Write(byte[] buf) => this.Write(buf, 0, buf.Length);

    public abstract void Write(byte[] buf, int off, int len);

    public virtual void Flush()
    {
    }

    public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state) => throw new TTransportException(TTransportException.ExceptionType.Unknown, "Asynchronous operations are not supported by this transport.");

    public virtual void EndFlush(IAsyncResult asyncResult) => throw new TTransportException(TTransportException.ExceptionType.Unknown, "Asynchronous operations are not supported by this transport.");

    protected abstract void Dispose(bool disposing);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
  }
}
