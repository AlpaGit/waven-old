// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TProtocol
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
  public abstract class TProtocol : IDisposable
  {
    private const int DEFAULT_RECURSION_DEPTH = 64;
    protected TTransport trans;
    protected int recursionLimit;
    protected int recursionDepth;
    private bool _IsDisposed;

    protected TProtocol(TTransport trans)
    {
      this.trans = trans;
      this.recursionLimit = 64;
      this.recursionDepth = 0;
    }

    public TTransport Transport => this.trans;

    public int RecursionLimit
    {
      get => this.recursionLimit;
      set => this.recursionLimit = value;
    }

    public void IncrementRecursionDepth()
    {
      if (this.recursionDepth >= this.recursionLimit)
        throw new TProtocolException(6, "Depth limit exceeded");
      ++this.recursionDepth;
    }

    public void DecrementRecursionDepth() => --this.recursionDepth;

    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
      if (!this._IsDisposed && disposing && this.trans != null)
        this.trans.Dispose();
      this._IsDisposed = true;
    }

    public abstract void WriteMessageBegin(TMessage message);

    public abstract void WriteMessageEnd();

    public abstract void WriteStructBegin(TStruct struc);

    public abstract void WriteStructEnd();

    public abstract void WriteFieldBegin(TField field);

    public abstract void WriteFieldEnd();

    public abstract void WriteFieldStop();

    public abstract void WriteMapBegin(TMap map);

    public abstract void WriteMapEnd();

    public abstract void WriteListBegin(TList list);

    public abstract void WriteListEnd();

    public abstract void WriteSetBegin(TSet set);

    public abstract void WriteSetEnd();

    public abstract void WriteBool(bool b);

    public abstract void WriteByte(sbyte b);

    public abstract void WriteI16(short i16);

    public abstract void WriteI32(int i32);

    public abstract void WriteI64(long i64);

    public abstract void WriteDouble(double d);

    public virtual void WriteString(string s) => this.WriteBinary(Encoding.UTF8.GetBytes(s));

    public abstract void WriteBinary(byte[] b);

    public abstract TMessage ReadMessageBegin();

    public abstract void ReadMessageEnd();

    public abstract TStruct ReadStructBegin();

    public abstract void ReadStructEnd();

    public abstract TField ReadFieldBegin();

    public abstract void ReadFieldEnd();

    public abstract TMap ReadMapBegin();

    public abstract void ReadMapEnd();

    public abstract TList ReadListBegin();

    public abstract void ReadListEnd();

    public abstract TSet ReadSetBegin();

    public abstract void ReadSetEnd();

    public abstract bool ReadBool();

    public abstract sbyte ReadByte();

    public abstract short ReadI16();

    public abstract int ReadI32();

    public abstract long ReadI64();

    public abstract double ReadDouble();

    public virtual string ReadString()
    {
      byte[] bytes = this.ReadBinary();
      return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }

    public abstract byte[] ReadBinary();
  }
}
