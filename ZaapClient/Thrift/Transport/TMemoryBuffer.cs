// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TMemoryBuffer
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.IO;
using System.Reflection;
using Thrift.Protocol;

namespace Thrift.Transport
{
  public class TMemoryBuffer : TTransport
  {
    private readonly MemoryStream byteStream;
    private bool _IsDisposed;

    public TMemoryBuffer() => this.byteStream = new MemoryStream();

    public TMemoryBuffer(byte[] buf) => this.byteStream = new MemoryStream(buf);

    public override void Open()
    {
    }

    public override void Close()
    {
    }

    public override int Read(byte[] buf, int off, int len) => this.byteStream.Read(buf, off, len);

    public override void Write(byte[] buf, int off, int len) => this.byteStream.Write(buf, off, len);

    public byte[] GetBuffer() => this.byteStream.ToArray();

    public override bool IsOpen => true;

    public static byte[] Serialize(TAbstractBase s)
    {
      TMemoryBuffer trans = new TMemoryBuffer();
      TBinaryProtocol tbinaryProtocol = new TBinaryProtocol((TTransport) trans);
      s.Write((TProtocol) tbinaryProtocol);
      return trans.GetBuffer();
    }

    public static T DeSerialize<T>(byte[] buf) where T : TAbstractBase
    {
      TBinaryProtocol tbinaryProtocol = new TBinaryProtocol((TTransport) new TMemoryBuffer(buf));
      if (typeof (TBase).IsAssignableFrom(typeof (T)))
      {
        MethodInfo method = typeof (T).GetMethod("Read", BindingFlags.Instance | BindingFlags.Public);
        T instance = Activator.CreateInstance<T>();
        method.Invoke((object) instance, new object[1]
        {
          (object) tbinaryProtocol
        });
        return instance;
      }
      return (T) typeof (T).GetMethod("Read", BindingFlags.Static | BindingFlags.Public).Invoke((object) null, new object[1]
      {
        (object) tbinaryProtocol
      });
    }

    protected override void Dispose(bool disposing)
    {
      if (!this._IsDisposed && disposing && this.byteStream != null)
        this.byteStream.Dispose();
      this._IsDisposed = true;
    }
  }
}
