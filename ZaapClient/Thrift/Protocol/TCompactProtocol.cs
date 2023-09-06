// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TCompactProtocol
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
  public class TCompactProtocol : TProtocol
  {
    private static TStruct ANONYMOUS_STRUCT = new TStruct(string.Empty);
    private static TField TSTOP = new TField(string.Empty, TType.Stop, (short) 0);
    private static byte[] ttypeToCompactType = new byte[16];
    private const byte PROTOCOL_ID = 130;
    private const byte VERSION = 1;
    private const byte VERSION_MASK = 31;
    private const byte TYPE_MASK = 224;
    private const byte TYPE_BITS = 7;
    private const int TYPE_SHIFT_AMOUNT = 5;
    private Stack<short> lastField_ = new Stack<short>(15);
    private short lastFieldId_;
    private TField? booleanField_;
    private bool? boolValue_;
    private byte[] byteDirectBuffer = new byte[1];
    private byte[] i32buf = new byte[5];
    private byte[] varint64out = new byte[10];
    private byte[] byteRawBuf = new byte[1];

    public TCompactProtocol(TTransport trans)
      : base(trans)
    {
      TCompactProtocol.ttypeToCompactType[0] = (byte) 0;
      TCompactProtocol.ttypeToCompactType[2] = (byte) 1;
      TCompactProtocol.ttypeToCompactType[3] = (byte) 3;
      TCompactProtocol.ttypeToCompactType[6] = (byte) 4;
      TCompactProtocol.ttypeToCompactType[8] = (byte) 5;
      TCompactProtocol.ttypeToCompactType[10] = (byte) 6;
      TCompactProtocol.ttypeToCompactType[4] = (byte) 7;
      TCompactProtocol.ttypeToCompactType[11] = (byte) 8;
      TCompactProtocol.ttypeToCompactType[15] = (byte) 9;
      TCompactProtocol.ttypeToCompactType[14] = (byte) 10;
      TCompactProtocol.ttypeToCompactType[13] = (byte) 11;
      TCompactProtocol.ttypeToCompactType[12] = (byte) 12;
    }

    public void reset()
    {
      this.lastField_.Clear();
      this.lastFieldId_ = (short) 0;
    }

    private void WriteByteDirect(byte b)
    {
      this.byteDirectBuffer[0] = b;
      this.trans.Write(this.byteDirectBuffer);
    }

    private void WriteByteDirect(int n) => this.WriteByteDirect((byte) n);

    private void WriteVarint32(uint n)
    {
      int num1 = 0;
      for (; ((long) n & (long) sbyte.MinValue) != 0L; n >>= 7)
        this.i32buf[num1++] = (byte) ((int) n & (int) sbyte.MaxValue | 128);
      byte[] i32buf = this.i32buf;
      int index = num1;
      int len = index + 1;
      int num2 = (int) (byte) n;
      i32buf[index] = (byte) num2;
      this.trans.Write(this.i32buf, 0, len);
    }

    public override void WriteMessageBegin(TMessage message)
    {
      this.WriteByteDirect((byte) 130);
      this.WriteByteDirect((byte) (1 | (int) message.Type << 5 & 224));
      this.WriteVarint32((uint) message.SeqID);
      this.WriteString(message.Name);
    }

    public override void WriteStructBegin(TStruct strct)
    {
      this.lastField_.Push(this.lastFieldId_);
      this.lastFieldId_ = (short) 0;
    }

    public override void WriteStructEnd() => this.lastFieldId_ = this.lastField_.Pop();

    public override void WriteFieldBegin(TField field)
    {
      if (field.Type == TType.Bool)
        this.booleanField_ = new TField?(field);
      else
        this.WriteFieldBeginInternal(field, byte.MaxValue);
    }

    private void WriteFieldBeginInternal(TField field, byte typeOverride)
    {
      byte b = typeOverride != byte.MaxValue ? typeOverride : this.getCompactType(field.Type);
      if ((int) field.ID > (int) this.lastFieldId_ && (int) field.ID - (int) this.lastFieldId_ <= 15)
      {
        this.WriteByteDirect((int) field.ID - (int) this.lastFieldId_ << 4 | (int) b);
      }
      else
      {
        this.WriteByteDirect(b);
        this.WriteI16(field.ID);
      }
      this.lastFieldId_ = field.ID;
    }

    public override void WriteFieldStop() => this.WriteByteDirect((byte) 0);

    public override void WriteMapBegin(TMap map)
    {
      if (map.Count == 0)
      {
        this.WriteByteDirect(0);
      }
      else
      {
        this.WriteVarint32((uint) map.Count);
        this.WriteByteDirect((int) this.getCompactType(map.KeyType) << 4 | (int) this.getCompactType(map.ValueType));
      }
    }

    public override void WriteListBegin(TList list) => this.WriteCollectionBegin(list.ElementType, list.Count);

    public override void WriteSetBegin(TSet set) => this.WriteCollectionBegin(set.ElementType, set.Count);

    public override void WriteBool(bool b)
    {
      if (this.booleanField_.HasValue)
      {
        this.WriteFieldBeginInternal(this.booleanField_.Value, !b ? (byte) 2 : (byte) 1);
        this.booleanField_ = new TField?();
      }
      else
        this.WriteByteDirect(!b ? (byte) 2 : (byte) 1);
    }

    public override void WriteByte(sbyte b) => this.WriteByteDirect((byte) b);

    public override void WriteI16(short i16) => this.WriteVarint32(this.intToZigZag((int) i16));

    public override void WriteI32(int i32) => this.WriteVarint32(this.intToZigZag(i32));

    public override void WriteI64(long i64) => this.WriteVarint64(this.longToZigzag(i64));

    public override void WriteDouble(double dub)
    {
      byte[] buf = new byte[8];
      this.fixedLongToBytes(BitConverter.DoubleToInt64Bits(dub), buf, 0);
      this.trans.Write(buf);
    }

    public override void WriteString(string str)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(str);
      this.WriteBinary(bytes, 0, bytes.Length);
    }

    public override void WriteBinary(byte[] bin) => this.WriteBinary(bin, 0, bin.Length);

    private void WriteBinary(byte[] buf, int offset, int length)
    {
      this.WriteVarint32((uint) length);
      this.trans.Write(buf, offset, length);
    }

    public override void WriteMessageEnd()
    {
    }

    public override void WriteMapEnd()
    {
    }

    public override void WriteListEnd()
    {
    }

    public override void WriteSetEnd()
    {
    }

    public override void WriteFieldEnd()
    {
    }

    protected void WriteCollectionBegin(TType elemType, int size)
    {
      if (size <= 14)
      {
        this.WriteByteDirect(size << 4 | (int) this.getCompactType(elemType));
      }
      else
      {
        this.WriteByteDirect(240 | (int) this.getCompactType(elemType));
        this.WriteVarint32((uint) size);
      }
    }

    private void WriteVarint64(ulong n)
    {
      int num1 = 0;
      for (; ((long) n & (long) sbyte.MinValue) != 0L; n >>= 7)
        this.varint64out[num1++] = (byte) (n & (ulong) sbyte.MaxValue | 128UL);
      byte[] varint64out = this.varint64out;
      int index = num1;
      int len = index + 1;
      int num2 = (int) (byte) n;
      varint64out[index] = (byte) num2;
      this.trans.Write(this.varint64out, 0, len);
    }

    private ulong longToZigzag(long n) => (ulong) (n << 1 ^ n >> 63);

    private uint intToZigZag(int n) => (uint) (n << 1 ^ n >> 31);

    private void fixedLongToBytes(long n, byte[] buf, int off)
    {
      buf[off] = (byte) ((ulong) n & (ulong) byte.MaxValue);
      buf[off + 1] = (byte) ((ulong) (n >> 8) & (ulong) byte.MaxValue);
      buf[off + 2] = (byte) ((ulong) (n >> 16) & (ulong) byte.MaxValue);
      buf[off + 3] = (byte) ((ulong) (n >> 24) & (ulong) byte.MaxValue);
      buf[off + 4] = (byte) ((ulong) (n >> 32) & (ulong) byte.MaxValue);
      buf[off + 5] = (byte) ((ulong) (n >> 40) & (ulong) byte.MaxValue);
      buf[off + 6] = (byte) ((ulong) (n >> 48) & (ulong) byte.MaxValue);
      buf[off + 7] = (byte) ((ulong) (n >> 56) & (ulong) byte.MaxValue);
    }

    public override TMessage ReadMessageBegin()
    {
      byte num1 = (byte) this.ReadByte();
      if (num1 != (byte) 130)
        throw new TProtocolException("Expected protocol id " + (byte) 130.ToString("X") + " but got " + num1.ToString("X"));
      byte num2 = (byte) this.ReadByte();
      byte num3 = (byte) ((uint) num2 & 31U);
      if (num3 != (byte) 1)
        throw new TProtocolException("Expected version " + (object) (byte) 1 + " but got " + (object) num3);
      byte type = (byte) ((int) num2 >> 5 & 7);
      int seqid = (int) this.ReadVarint32();
      return new TMessage(this.ReadString(), (TMessageType) type, seqid);
    }

    public override TStruct ReadStructBegin()
    {
      this.lastField_.Push(this.lastFieldId_);
      this.lastFieldId_ = (short) 0;
      return TCompactProtocol.ANONYMOUS_STRUCT;
    }

    public override void ReadStructEnd() => this.lastFieldId_ = this.lastField_.Pop();

    public override TField ReadFieldBegin()
    {
      byte b = (byte) this.ReadByte();
      if (b == (byte) 0)
        return TCompactProtocol.TSTOP;
      short num = (short) (((int) b & 240) >> 4);
      short id = num != (short) 0 ? (short) ((int) this.lastFieldId_ + (int) num) : this.ReadI16();
      TField tfield = new TField(string.Empty, this.getTType((byte) ((uint) b & 15U)), id);
      if (this.isBoolType(b))
        this.boolValue_ = new bool?((byte) ((uint) b & 15U) == (byte) 1);
      this.lastFieldId_ = tfield.ID;
      return tfield;
    }

    public override TMap ReadMapBegin()
    {
      int count = (int) this.ReadVarint32();
      byte num = count != 0 ? (byte) this.ReadByte() : (byte) 0;
      return new TMap(this.getTType((byte) ((uint) num >> 4)), this.getTType((byte) ((uint) num & 15U)), count);
    }

    public override TList ReadListBegin()
    {
      byte type = (byte) this.ReadByte();
      int count = (int) type >> 4 & 15;
      if (count == 15)
        count = (int) this.ReadVarint32();
      return new TList(this.getTType(type), count);
    }

    public override TSet ReadSetBegin() => new TSet(this.ReadListBegin());

    public override bool ReadBool()
    {
      if (!this.boolValue_.HasValue)
        return this.ReadByte() == (sbyte) 1;
      bool flag = this.boolValue_.Value;
      this.boolValue_ = new bool?();
      return flag;
    }

    public override sbyte ReadByte()
    {
      this.trans.ReadAll(this.byteRawBuf, 0, 1);
      return (sbyte) this.byteRawBuf[0];
    }

    public override short ReadI16() => (short) this.zigzagToInt(this.ReadVarint32());

    public override int ReadI32() => this.zigzagToInt(this.ReadVarint32());

    public override long ReadI64() => this.zigzagToLong(this.ReadVarint64());

    public override double ReadDouble()
    {
      byte[] numArray = new byte[8];
      this.trans.ReadAll(numArray, 0, 8);
      return BitConverter.Int64BitsToDouble(this.bytesToLong(numArray));
    }

    public override string ReadString()
    {
      int length = (int) this.ReadVarint32();
      return length == 0 ? string.Empty : Encoding.UTF8.GetString(this.ReadBinary(length));
    }

    public override byte[] ReadBinary()
    {
      int len = (int) this.ReadVarint32();
      if (len == 0)
        return new byte[0];
      byte[] buf = new byte[len];
      this.trans.ReadAll(buf, 0, len);
      return buf;
    }

    private byte[] ReadBinary(int length)
    {
      if (length == 0)
        return new byte[0];
      byte[] buf = new byte[length];
      this.trans.ReadAll(buf, 0, length);
      return buf;
    }

    public override void ReadMessageEnd()
    {
    }

    public override void ReadFieldEnd()
    {
    }

    public override void ReadMapEnd()
    {
    }

    public override void ReadListEnd()
    {
    }

    public override void ReadSetEnd()
    {
    }

    private uint ReadVarint32()
    {
      uint num1 = 0;
      int num2 = 0;
      while (true)
      {
        byte num3 = (byte) this.ReadByte();
        num1 |= (uint) (((int) num3 & (int) sbyte.MaxValue) << num2);
        if (((int) num3 & 128) == 128)
          num2 += 7;
        else
          break;
      }
      return num1;
    }

    private ulong ReadVarint64()
    {
      int num1 = 0;
      ulong num2 = 0;
      while (true)
      {
        byte num3 = (byte) this.ReadByte();
        num2 |= (ulong) ((int) num3 & (int) sbyte.MaxValue) << num1;
        if (((int) num3 & 128) == 128)
          num1 += 7;
        else
          break;
      }
      return num2;
    }

    private int zigzagToInt(uint n) => (int) (n >> 1) ^ -((int) n & 1);

    private long zigzagToLong(ulong n) => (long) (n >> 1) ^ -((long) n & 1L);

    private long bytesToLong(byte[] bytes) => ((long) bytes[7] & (long) byte.MaxValue) << 56 | ((long) bytes[6] & (long) byte.MaxValue) << 48 | ((long) bytes[5] & (long) byte.MaxValue) << 40 | ((long) bytes[4] & (long) byte.MaxValue) << 32 | ((long) bytes[3] & (long) byte.MaxValue) << 24 | ((long) bytes[2] & (long) byte.MaxValue) << 16 | ((long) bytes[1] & (long) byte.MaxValue) << 8 | (long) bytes[0] & (long) byte.MaxValue;

    private bool isBoolType(byte b)
    {
      int num = (int) b & 15;
      return num == 1 || num == 2;
    }

    private TType getTType(byte type)
    {
      switch ((byte) ((uint) type & 15U))
      {
        case 0:
          return TType.Stop;
        case 1:
        case 2:
          return TType.Bool;
        case 3:
          return TType.Byte;
        case 4:
          return TType.I16;
        case 5:
          return TType.I32;
        case 6:
          return TType.I64;
        case 7:
          return TType.Double;
        case 8:
          return TType.String;
        case 9:
          return TType.List;
        case 10:
          return TType.Set;
        case 11:
          return TType.Map;
        case 12:
          return TType.Struct;
        default:
          throw new TProtocolException("don't know what type: " + (object) (byte) ((uint) type & 15U));
      }
    }

    private byte getCompactType(TType ttype) => TCompactProtocol.ttypeToCompactType[(int) ttype];

    private static class Types
    {
      public const byte STOP = 0;
      public const byte BOOLEAN_TRUE = 1;
      public const byte BOOLEAN_FALSE = 2;
      public const byte BYTE = 3;
      public const byte I16 = 4;
      public const byte I32 = 5;
      public const byte I64 = 6;
      public const byte DOUBLE = 7;
      public const byte BINARY = 8;
      public const byte LIST = 9;
      public const byte SET = 10;
      public const byte MAP = 11;
      public const byte STRUCT = 12;
    }

    public class Factory : TProtocolFactory
    {
      public TProtocol GetProtocol(TTransport trans) => (TProtocol) new TCompactProtocol(trans);
    }
  }
}
