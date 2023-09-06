// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TBinaryProtocol
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
  public class TBinaryProtocol : TProtocol
  {
    protected const uint VERSION_MASK = 4294901760;
    protected const uint VERSION_1 = 2147549184;
    protected bool strictRead_;
    protected bool strictWrite_ = true;
    private byte[] bout = new byte[1];
    private byte[] i16out = new byte[2];
    private byte[] i32out = new byte[4];
    private byte[] i64out = new byte[8];
    private byte[] bin = new byte[1];
    private byte[] i16in = new byte[2];
    private byte[] i32in = new byte[4];
    private byte[] i64in = new byte[8];

    public TBinaryProtocol(TTransport trans)
      : this(trans, false, true)
    {
    }

    public TBinaryProtocol(TTransport trans, bool strictRead, bool strictWrite)
      : base(trans)
    {
      this.strictRead_ = strictRead;
      this.strictWrite_ = strictWrite;
    }

    public override void WriteMessageBegin(TMessage message)
    {
      if (this.strictWrite_)
      {
        this.WriteI32((int) ((TMessageType) -2147418112 | message.Type));
        this.WriteString(message.Name);
        this.WriteI32(message.SeqID);
      }
      else
      {
        this.WriteString(message.Name);
        this.WriteByte((sbyte) message.Type);
        this.WriteI32(message.SeqID);
      }
    }

    public override void WriteMessageEnd()
    {
    }

    public override void WriteStructBegin(TStruct struc)
    {
    }

    public override void WriteStructEnd()
    {
    }

    public override void WriteFieldBegin(TField field)
    {
      this.WriteByte((sbyte) field.Type);
      this.WriteI16(field.ID);
    }

    public override void WriteFieldEnd()
    {
    }

    public override void WriteFieldStop() => this.WriteByte((sbyte) 0);

    public override void WriteMapBegin(TMap map)
    {
      this.WriteByte((sbyte) map.KeyType);
      this.WriteByte((sbyte) map.ValueType);
      this.WriteI32(map.Count);
    }

    public override void WriteMapEnd()
    {
    }

    public override void WriteListBegin(TList list)
    {
      this.WriteByte((sbyte) list.ElementType);
      this.WriteI32(list.Count);
    }

    public override void WriteListEnd()
    {
    }

    public override void WriteSetBegin(TSet set)
    {
      this.WriteByte((sbyte) set.ElementType);
      this.WriteI32(set.Count);
    }

    public override void WriteSetEnd()
    {
    }

    public override void WriteBool(bool b) => this.WriteByte(!b ? (sbyte) 0 : (sbyte) 1);

    public override void WriteByte(sbyte b)
    {
      this.bout[0] = (byte) b;
      this.trans.Write(this.bout, 0, 1);
    }

    public override void WriteI16(short s)
    {
      this.i16out[0] = (byte) ((int) byte.MaxValue & (int) s >> 8);
      this.i16out[1] = (byte) ((uint) byte.MaxValue & (uint) s);
      this.trans.Write(this.i16out, 0, 2);
    }

    public override void WriteI32(int i32)
    {
      this.i32out[0] = (byte) ((int) byte.MaxValue & i32 >> 24);
      this.i32out[1] = (byte) ((int) byte.MaxValue & i32 >> 16);
      this.i32out[2] = (byte) ((int) byte.MaxValue & i32 >> 8);
      this.i32out[3] = (byte) ((int) byte.MaxValue & i32);
      this.trans.Write(this.i32out, 0, 4);
    }

    public override void WriteI64(long i64)
    {
      this.i64out[0] = (byte) ((ulong) byte.MaxValue & (ulong) (i64 >> 56));
      this.i64out[1] = (byte) ((ulong) byte.MaxValue & (ulong) (i64 >> 48));
      this.i64out[2] = (byte) ((ulong) byte.MaxValue & (ulong) (i64 >> 40));
      this.i64out[3] = (byte) ((ulong) byte.MaxValue & (ulong) (i64 >> 32));
      this.i64out[4] = (byte) ((ulong) byte.MaxValue & (ulong) (i64 >> 24));
      this.i64out[5] = (byte) ((ulong) byte.MaxValue & (ulong) (i64 >> 16));
      this.i64out[6] = (byte) ((ulong) byte.MaxValue & (ulong) (i64 >> 8));
      this.i64out[7] = (byte) ((ulong) byte.MaxValue & (ulong) i64);
      this.trans.Write(this.i64out, 0, 8);
    }

    public override void WriteDouble(double d) => this.WriteI64(BitConverter.DoubleToInt64Bits(d));

    public override void WriteBinary(byte[] b)
    {
      this.WriteI32(b.Length);
      this.trans.Write(b, 0, b.Length);
    }

    public override TMessage ReadMessageBegin()
    {
      TMessage tmessage = new TMessage();
      int size = this.ReadI32();
      if (size < 0)
      {
        uint num = (uint) (size & -65536);
        if (num != 2147549184U)
          throw new TProtocolException(4, "Bad version in ReadMessageBegin: " + (object) num);
        tmessage.Type = (TMessageType) (size & (int) byte.MaxValue);
        tmessage.Name = this.ReadString();
        tmessage.SeqID = this.ReadI32();
      }
      else
      {
        if (this.strictRead_)
          throw new TProtocolException(4, "Missing version in readMessageBegin, old client?");
        tmessage.Name = this.ReadStringBody(size);
        tmessage.Type = (TMessageType) this.ReadByte();
        tmessage.SeqID = this.ReadI32();
      }
      return tmessage;
    }

    public override void ReadMessageEnd()
    {
    }

    public override TStruct ReadStructBegin() => new TStruct();

    public override void ReadStructEnd()
    {
    }

    public override TField ReadFieldBegin()
    {
      TField tfield = new TField();
      tfield.Type = (TType) this.ReadByte();
      if (tfield.Type != TType.Stop)
        tfield.ID = this.ReadI16();
      return tfield;
    }

    public override void ReadFieldEnd()
    {
    }

    public override TMap ReadMapBegin() => new TMap()
    {
      KeyType = (TType) this.ReadByte(),
      ValueType = (TType) this.ReadByte(),
      Count = this.ReadI32()
    };

    public override void ReadMapEnd()
    {
    }

    public override TList ReadListBegin() => new TList()
    {
      ElementType = (TType) this.ReadByte(),
      Count = this.ReadI32()
    };

    public override void ReadListEnd()
    {
    }

    public override TSet ReadSetBegin() => new TSet()
    {
      ElementType = (TType) this.ReadByte(),
      Count = this.ReadI32()
    };

    public override void ReadSetEnd()
    {
    }

    public override bool ReadBool() => this.ReadByte() == (sbyte) 1;

    public override sbyte ReadByte()
    {
      this.ReadAll(this.bin, 0, 1);
      return (sbyte) this.bin[0];
    }

    public override short ReadI16()
    {
      this.ReadAll(this.i16in, 0, 2);
      return (short) (((int) this.i16in[0] & (int) byte.MaxValue) << 8 | (int) this.i16in[1] & (int) byte.MaxValue);
    }

    public override int ReadI32()
    {
      this.ReadAll(this.i32in, 0, 4);
      return ((int) this.i32in[0] & (int) byte.MaxValue) << 24 | ((int) this.i32in[1] & (int) byte.MaxValue) << 16 | ((int) this.i32in[2] & (int) byte.MaxValue) << 8 | (int) this.i32in[3] & (int) byte.MaxValue;
    }

    public override long ReadI64()
    {
      this.ReadAll(this.i64in, 0, 8);
      return (long) ((int) this.i64in[0] & (int) byte.MaxValue) << 56 | (long) ((int) this.i64in[1] & (int) byte.MaxValue) << 48 | (long) ((int) this.i64in[2] & (int) byte.MaxValue) << 40 | (long) ((int) this.i64in[3] & (int) byte.MaxValue) << 32 | (long) ((int) this.i64in[4] & (int) byte.MaxValue) << 24 | (long) ((int) this.i64in[5] & (int) byte.MaxValue) << 16 | (long) ((int) this.i64in[6] & (int) byte.MaxValue) << 8 | (long) ((int) this.i64in[7] & (int) byte.MaxValue);
    }

    public override double ReadDouble() => BitConverter.Int64BitsToDouble(this.ReadI64());

    public override byte[] ReadBinary()
    {
      int len = this.ReadI32();
      byte[] buf = new byte[len];
      this.trans.ReadAll(buf, 0, len);
      return buf;
    }

    private string ReadStringBody(int size)
    {
      byte[] numArray = new byte[size];
      this.trans.ReadAll(numArray, 0, size);
      return Encoding.UTF8.GetString(numArray, 0, numArray.Length);
    }

    private int ReadAll(byte[] buf, int off, int len) => this.trans.ReadAll(buf, off, len);

    public class Factory : TProtocolFactory
    {
      protected bool strictRead_;
      protected bool strictWrite_ = true;

      public Factory()
        : this(false, true)
      {
      }

      public Factory(bool strictRead, bool strictWrite)
      {
        this.strictRead_ = strictRead;
        this.strictWrite_ = strictWrite;
      }

      public TProtocol GetProtocol(TTransport trans) => (TProtocol) new TBinaryProtocol(trans, this.strictRead_, this.strictWrite_);
    }
  }
}
