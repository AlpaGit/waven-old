// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TJSONProtocol
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
  public class TJSONProtocol : TProtocol
  {
    private static byte[] COMMA = new byte[1]{ (byte) 44 };
    private static byte[] COLON = new byte[1]{ (byte) 58 };
    private static byte[] LBRACE = new byte[1]{ (byte) 123 };
    private static byte[] RBRACE = new byte[1]{ (byte) 125 };
    private static byte[] LBRACKET = new byte[1]
    {
      (byte) 91
    };
    private static byte[] RBRACKET = new byte[1]
    {
      (byte) 93
    };
    private static byte[] QUOTE = new byte[1]{ (byte) 34 };
    private static byte[] BACKSLASH = new byte[1]
    {
      (byte) 92
    };
    private byte[] ESCSEQ = new byte[4]
    {
      (byte) 92,
      (byte) 117,
      (byte) 48,
      (byte) 48
    };
    private const long VERSION = 1;
    private byte[] JSON_CHAR_TABLE = new byte[48]
    {
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 98,
      (byte) 116,
      (byte) 110,
      (byte) 0,
      (byte) 102,
      (byte) 114,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 34,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1
    };
    private char[] ESCAPE_CHARS = "\"\\/bfnrt".ToCharArray();
    private byte[] ESCAPE_CHAR_VALS = new byte[8]
    {
      (byte) 34,
      (byte) 92,
      (byte) 47,
      (byte) 8,
      (byte) 12,
      (byte) 10,
      (byte) 13,
      (byte) 9
    };
    private const int DEF_STRING_SIZE = 16;
    private static byte[] NAME_BOOL = new byte[2]
    {
      (byte) 116,
      (byte) 102
    };
    private static byte[] NAME_BYTE = new byte[2]
    {
      (byte) 105,
      (byte) 56
    };
    private static byte[] NAME_I16 = new byte[3]
    {
      (byte) 105,
      (byte) 49,
      (byte) 54
    };
    private static byte[] NAME_I32 = new byte[3]
    {
      (byte) 105,
      (byte) 51,
      (byte) 50
    };
    private static byte[] NAME_I64 = new byte[3]
    {
      (byte) 105,
      (byte) 54,
      (byte) 52
    };
    private static byte[] NAME_DOUBLE = new byte[3]
    {
      (byte) 100,
      (byte) 98,
      (byte) 108
    };
    private static byte[] NAME_STRUCT = new byte[3]
    {
      (byte) 114,
      (byte) 101,
      (byte) 99
    };
    private static byte[] NAME_STRING = new byte[3]
    {
      (byte) 115,
      (byte) 116,
      (byte) 114
    };
    private static byte[] NAME_MAP = new byte[3]
    {
      (byte) 109,
      (byte) 97,
      (byte) 112
    };
    private static byte[] NAME_LIST = new byte[3]
    {
      (byte) 108,
      (byte) 115,
      (byte) 116
    };
    private static byte[] NAME_SET = new byte[3]
    {
      (byte) 115,
      (byte) 101,
      (byte) 116
    };
    protected Encoding utf8Encoding = Encoding.UTF8;
    protected Stack<TJSONProtocol.JSONBaseContext> contextStack = new Stack<TJSONProtocol.JSONBaseContext>();
    protected TJSONProtocol.JSONBaseContext context;
    protected TJSONProtocol.LookaheadReader reader;
    private byte[] tempBuffer = new byte[4];

    public TJSONProtocol(TTransport trans)
      : base(trans)
    {
      this.context = new TJSONProtocol.JSONBaseContext(this);
      this.reader = new TJSONProtocol.LookaheadReader(this);
    }

    private static byte[] GetTypeNameForTypeID(TType typeID)
    {
      switch (typeID)
      {
        case TType.Bool:
          return TJSONProtocol.NAME_BOOL;
        case TType.Byte:
          return TJSONProtocol.NAME_BYTE;
        case TType.Double:
          return TJSONProtocol.NAME_DOUBLE;
        case TType.I16:
          return TJSONProtocol.NAME_I16;
        case TType.I32:
          return TJSONProtocol.NAME_I32;
        case TType.I64:
          return TJSONProtocol.NAME_I64;
        case TType.String:
          return TJSONProtocol.NAME_STRING;
        case TType.Struct:
          return TJSONProtocol.NAME_STRUCT;
        case TType.Map:
          return TJSONProtocol.NAME_MAP;
        case TType.Set:
          return TJSONProtocol.NAME_SET;
        case TType.List:
          return TJSONProtocol.NAME_LIST;
        default:
          throw new TProtocolException(5, "Unrecognized type");
      }
    }

    private static TType GetTypeIDForTypeName(byte[] name)
    {
      TType ttype = TType.Stop;
      if (name.Length > 1)
      {
        byte num = name[0];
        switch (num)
        {
          case 105:
            switch (name[1])
            {
              case 49:
                ttype = TType.I16;
                break;
              case 51:
                ttype = TType.I32;
                break;
              case 54:
                ttype = TType.I64;
                break;
              case 56:
                ttype = TType.Byte;
                break;
            }
            break;
          case 108:
            ttype = TType.List;
            break;
          case 109:
            ttype = TType.Map;
            break;
          default:
            switch (num)
            {
              case 100:
                ttype = TType.Double;
                break;
              case 114:
                ttype = TType.Struct;
                break;
              case 115:
                if (name[1] == (byte) 116)
                {
                  ttype = TType.String;
                  break;
                }
                if (name[1] == (byte) 101)
                {
                  ttype = TType.Set;
                  break;
                }
                break;
              case 116:
                ttype = TType.Bool;
                break;
            }
            break;
        }
      }
      return ttype != TType.Stop ? ttype : throw new TProtocolException(5, "Unrecognized type");
    }

    protected void PushContext(TJSONProtocol.JSONBaseContext c)
    {
      this.contextStack.Push(this.context);
      this.context = c;
    }

    protected void PopContext() => this.context = this.contextStack.Pop();

    protected void ReadJSONSyntaxChar(byte[] b)
    {
      byte num = this.reader.Read();
      if ((int) num != (int) b[0])
        throw new TProtocolException(1, "Unexpected character:" + (object) (char) num);
    }

    private static byte HexVal(byte ch)
    {
      if (ch >= (byte) 48 && ch <= (byte) 57)
        return (byte) ((uint) (ushort) ch - 48U);
      if (ch < (byte) 97 || ch > (byte) 102)
        throw new TProtocolException(1, "Expected hex character");
      ch += (byte) 10;
      return (byte) ((uint) (ushort) ch - 97U);
    }

    private static byte HexChar(byte val)
    {
      val &= (byte) 15;
      if (val < (byte) 10)
        return (byte) ((uint) (ushort) val + 48U);
      val -= (byte) 10;
      return (byte) ((uint) (ushort) val + 97U);
    }

    private void WriteJSONString(byte[] b)
    {
      this.context.Write();
      this.trans.Write(TJSONProtocol.QUOTE);
      int length = b.Length;
      for (int off = 0; off < length; ++off)
      {
        if (((int) b[off] & (int) byte.MaxValue) >= 48)
        {
          if ((int) b[off] == (int) TJSONProtocol.BACKSLASH[0])
          {
            this.trans.Write(TJSONProtocol.BACKSLASH);
            this.trans.Write(TJSONProtocol.BACKSLASH);
          }
          else
            this.trans.Write(b, off, 1);
        }
        else
        {
          this.tempBuffer[0] = this.JSON_CHAR_TABLE[(int) b[off]];
          if (this.tempBuffer[0] == (byte) 1)
            this.trans.Write(b, off, 1);
          else if (this.tempBuffer[0] > (byte) 1)
          {
            this.trans.Write(TJSONProtocol.BACKSLASH);
            this.trans.Write(this.tempBuffer, 0, 1);
          }
          else
          {
            this.trans.Write(this.ESCSEQ);
            this.tempBuffer[0] = TJSONProtocol.HexChar((byte) ((uint) b[off] >> 4));
            this.tempBuffer[1] = TJSONProtocol.HexChar(b[off]);
            this.trans.Write(this.tempBuffer, 0, 2);
          }
        }
      }
      this.trans.Write(TJSONProtocol.QUOTE);
    }

    private void WriteJSONInteger(long num)
    {
      this.context.Write();
      string s = num.ToString();
      bool flag = this.context.EscapeNumbers();
      if (flag)
        this.trans.Write(TJSONProtocol.QUOTE);
      this.trans.Write(this.utf8Encoding.GetBytes(s));
      if (!flag)
        return;
      this.trans.Write(TJSONProtocol.QUOTE);
    }

    private void WriteJSONDouble(double num)
    {
      this.context.Write();
      string s = num.ToString("G17", (IFormatProvider) CultureInfo.InvariantCulture);
      bool flag1 = false;
      switch (s[0])
      {
        case '-':
          if (s[1] == 'I')
          {
            flag1 = true;
            break;
          }
          break;
        case 'I':
        case 'N':
          flag1 = true;
          break;
      }
      bool flag2 = flag1 || this.context.EscapeNumbers();
      if (flag2)
        this.trans.Write(TJSONProtocol.QUOTE);
      this.trans.Write(this.utf8Encoding.GetBytes(s));
      if (!flag2)
        return;
      this.trans.Write(TJSONProtocol.QUOTE);
    }

    private void WriteJSONBase64(byte[] b)
    {
      this.context.Write();
      this.trans.Write(TJSONProtocol.QUOTE);
      int length = b.Length;
      int srcOff = 0;
      int num = length < 2 ? 0 : length - 2;
      for (int index = length - 1; index >= num && b[index] == (byte) 61; --index)
        --length;
      for (; length >= 3; length -= 3)
      {
        TBase64Utils.encode(b, srcOff, 3, this.tempBuffer, 0);
        this.trans.Write(this.tempBuffer, 0, 4);
        srcOff += 3;
      }
      if (length > 0)
      {
        TBase64Utils.encode(b, srcOff, length, this.tempBuffer, 0);
        this.trans.Write(this.tempBuffer, 0, length + 1);
      }
      this.trans.Write(TJSONProtocol.QUOTE);
    }

    private void WriteJSONObjectStart()
    {
      this.context.Write();
      this.trans.Write(TJSONProtocol.LBRACE);
      this.PushContext((TJSONProtocol.JSONBaseContext) new TJSONProtocol.JSONPairContext(this));
    }

    private void WriteJSONObjectEnd()
    {
      this.PopContext();
      this.trans.Write(TJSONProtocol.RBRACE);
    }

    private void WriteJSONArrayStart()
    {
      this.context.Write();
      this.trans.Write(TJSONProtocol.LBRACKET);
      this.PushContext((TJSONProtocol.JSONBaseContext) new TJSONProtocol.JSONListContext(this));
    }

    private void WriteJSONArrayEnd()
    {
      this.PopContext();
      this.trans.Write(TJSONProtocol.RBRACKET);
    }

    public override void WriteMessageBegin(TMessage message)
    {
      this.WriteJSONArrayStart();
      this.WriteJSONInteger(1L);
      this.WriteJSONString(this.utf8Encoding.GetBytes(message.Name));
      this.WriteJSONInteger((long) message.Type);
      this.WriteJSONInteger((long) message.SeqID);
    }

    public override void WriteMessageEnd() => this.WriteJSONArrayEnd();

    public override void WriteStructBegin(TStruct str) => this.WriteJSONObjectStart();

    public override void WriteStructEnd() => this.WriteJSONObjectEnd();

    public override void WriteFieldBegin(TField field)
    {
      this.WriteJSONInteger((long) field.ID);
      this.WriteJSONObjectStart();
      this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(field.Type));
    }

    public override void WriteFieldEnd() => this.WriteJSONObjectEnd();

    public override void WriteFieldStop()
    {
    }

    public override void WriteMapBegin(TMap map)
    {
      this.WriteJSONArrayStart();
      this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(map.KeyType));
      this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(map.ValueType));
      this.WriteJSONInteger((long) map.Count);
      this.WriteJSONObjectStart();
    }

    public override void WriteMapEnd()
    {
      this.WriteJSONObjectEnd();
      this.WriteJSONArrayEnd();
    }

    public override void WriteListBegin(TList list)
    {
      this.WriteJSONArrayStart();
      this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(list.ElementType));
      this.WriteJSONInteger((long) list.Count);
    }

    public override void WriteListEnd() => this.WriteJSONArrayEnd();

    public override void WriteSetBegin(TSet set)
    {
      this.WriteJSONArrayStart();
      this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(set.ElementType));
      this.WriteJSONInteger((long) set.Count);
    }

    public override void WriteSetEnd() => this.WriteJSONArrayEnd();

    public override void WriteBool(bool b) => this.WriteJSONInteger(!b ? 0L : 1L);

    public override void WriteByte(sbyte b) => this.WriteJSONInteger((long) b);

    public override void WriteI16(short i16) => this.WriteJSONInteger((long) i16);

    public override void WriteI32(int i32) => this.WriteJSONInteger((long) i32);

    public override void WriteI64(long i64) => this.WriteJSONInteger(i64);

    public override void WriteDouble(double dub) => this.WriteJSONDouble(dub);

    public override void WriteString(string str) => this.WriteJSONString(this.utf8Encoding.GetBytes(str));

    public override void WriteBinary(byte[] bin) => this.WriteJSONBase64(bin);

    private byte[] ReadJSONString(bool skipContext)
    {
      MemoryStream memoryStream = new MemoryStream();
      List<char> charList = new List<char>();
      if (!skipContext)
        this.context.Read();
      this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
      while (true)
      {
        byte num1 = this.reader.Read();
        if ((int) num1 != (int) TJSONProtocol.QUOTE[0])
        {
          if ((int) num1 != (int) this.ESCSEQ[0])
          {
            memoryStream.Write(new byte[1]{ num1 }, 0, 1);
          }
          else
          {
            byte num2 = this.reader.Read();
            if ((int) num2 != (int) this.ESCSEQ[1])
            {
              int index = Array.IndexOf<char>(this.ESCAPE_CHARS, (char) num2);
              if (index != -1)
              {
                byte num3 = this.ESCAPE_CHAR_VALS[index];
                memoryStream.Write(new byte[1]{ num3 }, 0, 1);
              }
              else
                break;
            }
            else
            {
              this.trans.ReadAll(this.tempBuffer, 0, 4);
              short c = (short) (((int) TJSONProtocol.HexVal(this.tempBuffer[0]) << 12) + ((int) TJSONProtocol.HexVal(this.tempBuffer[1]) << 8) + ((int) TJSONProtocol.HexVal(this.tempBuffer[2]) << 4) + (int) TJSONProtocol.HexVal(this.tempBuffer[3]));
              if (char.IsHighSurrogate((char) c))
              {
                if (charList.Count <= 0)
                  charList.Add((char) c);
                else
                  goto label_12;
              }
              else if (char.IsLowSurrogate((char) c))
              {
                if (charList.Count != 0)
                {
                  charList.Add((char) c);
                  byte[] bytes = this.utf8Encoding.GetBytes(charList.ToArray());
                  memoryStream.Write(bytes, 0, bytes.Length);
                  charList.Clear();
                }
                else
                  goto label_16;
              }
              else
              {
                byte[] bytes = this.utf8Encoding.GetBytes(new char[1]
                {
                  (char) c
                });
                memoryStream.Write(bytes, 0, bytes.Length);
              }
            }
          }
        }
        else
          goto label_19;
      }
      throw new TProtocolException(1, "Expected control char");
label_12:
      throw new TProtocolException(1, "Expected low surrogate char");
label_16:
      throw new TProtocolException(1, "Expected high surrogate char");
label_19:
      if (charList.Count > 0)
        throw new TProtocolException(1, "Expected low surrogate char");
      return memoryStream.ToArray();
    }

    private bool IsJSONNumeric(byte b)
    {
      switch (b)
      {
        case 43:
        case 45:
        case 46:
        case 48:
        case 49:
        case 50:
        case 51:
        case 52:
        case 53:
        case 54:
        case 55:
        case 56:
        case 57:
        case 69:
          return true;
        default:
          if (b != (byte) 101)
            return false;
          goto case 43;
      }
    }

    private string ReadJSONNumericChars()
    {
      StringBuilder stringBuilder = new StringBuilder();
      while (this.IsJSONNumeric(this.reader.Peek()))
        stringBuilder.Append((char) this.reader.Read());
      return stringBuilder.ToString();
    }

    private long ReadJSONInteger()
    {
      this.context.Read();
      if (this.context.EscapeNumbers())
        this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
      string s = this.ReadJSONNumericChars();
      if (this.context.EscapeNumbers())
        this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
      try
      {
        return long.Parse(s);
      }
      catch (FormatException ex)
      {
        throw new TProtocolException(1, "Bad data encounted in numeric data");
      }
    }

    private double ReadJSONDouble()
    {
      this.context.Read();
      if ((int) this.reader.Peek() == (int) TJSONProtocol.QUOTE[0])
      {
        byte[] bytes = this.ReadJSONString(true);
        double d = double.Parse(this.utf8Encoding.GetString(bytes, 0, bytes.Length), (IFormatProvider) CultureInfo.InvariantCulture);
        return this.context.EscapeNumbers() || double.IsNaN(d) || double.IsInfinity(d) ? d : throw new TProtocolException(1, "Numeric data unexpectedly quoted");
      }
      if (this.context.EscapeNumbers())
        this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
      try
      {
        return double.Parse(this.ReadJSONNumericChars(), (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new TProtocolException(1, "Bad data encounted in numeric data");
      }
    }

    private byte[] ReadJSONBase64()
    {
      byte[] numArray = this.ReadJSONString(false);
      int length1 = numArray.Length;
      int srcOff = 0;
      int length2 = 0;
      while (length1 > 0 && numArray[length1 - 1] == (byte) 61)
        --length1;
      while (length1 > 4)
      {
        TBase64Utils.decode(numArray, srcOff, 4, numArray, length2);
        srcOff += 4;
        length1 -= 4;
        length2 += 3;
      }
      if (length1 > 1)
      {
        TBase64Utils.decode(numArray, srcOff, length1, numArray, length2);
        length2 += length1 - 1;
      }
      byte[] destinationArray = new byte[length2];
      Array.Copy((Array) numArray, 0, (Array) destinationArray, 0, length2);
      return destinationArray;
    }

    private void ReadJSONObjectStart()
    {
      this.context.Read();
      this.ReadJSONSyntaxChar(TJSONProtocol.LBRACE);
      this.PushContext((TJSONProtocol.JSONBaseContext) new TJSONProtocol.JSONPairContext(this));
    }

    private void ReadJSONObjectEnd()
    {
      this.ReadJSONSyntaxChar(TJSONProtocol.RBRACE);
      this.PopContext();
    }

    private void ReadJSONArrayStart()
    {
      this.context.Read();
      this.ReadJSONSyntaxChar(TJSONProtocol.LBRACKET);
      this.PushContext((TJSONProtocol.JSONBaseContext) new TJSONProtocol.JSONListContext(this));
    }

    private void ReadJSONArrayEnd()
    {
      this.ReadJSONSyntaxChar(TJSONProtocol.RBRACKET);
      this.PopContext();
    }

    public override TMessage ReadMessageBegin()
    {
      TMessage tmessage = new TMessage();
      this.ReadJSONArrayStart();
      if (this.ReadJSONInteger() != 1L)
        throw new TProtocolException(4, "Message contained bad version.");
      byte[] bytes = this.ReadJSONString(false);
      tmessage.Name = this.utf8Encoding.GetString(bytes, 0, bytes.Length);
      tmessage.Type = (TMessageType) this.ReadJSONInteger();
      tmessage.SeqID = (int) this.ReadJSONInteger();
      return tmessage;
    }

    public override void ReadMessageEnd() => this.ReadJSONArrayEnd();

    public override TStruct ReadStructBegin()
    {
      this.ReadJSONObjectStart();
      return new TStruct();
    }

    public override void ReadStructEnd() => this.ReadJSONObjectEnd();

    public override TField ReadFieldBegin()
    {
      TField tfield = new TField();
      if ((int) this.reader.Peek() == (int) TJSONProtocol.RBRACE[0])
      {
        tfield.Type = TType.Stop;
      }
      else
      {
        tfield.ID = (short) this.ReadJSONInteger();
        this.ReadJSONObjectStart();
        tfield.Type = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
      }
      return tfield;
    }

    public override void ReadFieldEnd() => this.ReadJSONObjectEnd();

    public override TMap ReadMapBegin()
    {
      TMap tmap = new TMap();
      this.ReadJSONArrayStart();
      tmap.KeyType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
      tmap.ValueType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
      tmap.Count = (int) this.ReadJSONInteger();
      this.ReadJSONObjectStart();
      return tmap;
    }

    public override void ReadMapEnd()
    {
      this.ReadJSONObjectEnd();
      this.ReadJSONArrayEnd();
    }

    public override TList ReadListBegin()
    {
      TList tlist = new TList();
      this.ReadJSONArrayStart();
      tlist.ElementType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
      tlist.Count = (int) this.ReadJSONInteger();
      return tlist;
    }

    public override void ReadListEnd() => this.ReadJSONArrayEnd();

    public override TSet ReadSetBegin()
    {
      TSet tset = new TSet();
      this.ReadJSONArrayStart();
      tset.ElementType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
      tset.Count = (int) this.ReadJSONInteger();
      return tset;
    }

    public override void ReadSetEnd() => this.ReadJSONArrayEnd();

    public override bool ReadBool() => this.ReadJSONInteger() != 0L;

    public override sbyte ReadByte() => (sbyte) this.ReadJSONInteger();

    public override short ReadI16() => (short) this.ReadJSONInteger();

    public override int ReadI32() => (int) this.ReadJSONInteger();

    public override long ReadI64() => this.ReadJSONInteger();

    public override double ReadDouble() => this.ReadJSONDouble();

    public override string ReadString()
    {
      byte[] bytes = this.ReadJSONString(false);
      return this.utf8Encoding.GetString(bytes, 0, bytes.Length);
    }

    public override byte[] ReadBinary() => this.ReadJSONBase64();

    public class Factory : TProtocolFactory
    {
      public TProtocol GetProtocol(TTransport trans) => (TProtocol) new TJSONProtocol(trans);
    }

    protected class JSONBaseContext
    {
      protected TJSONProtocol proto;

      public JSONBaseContext(TJSONProtocol proto) => this.proto = proto;

      public virtual void Write()
      {
      }

      public virtual void Read()
      {
      }

      public virtual bool EscapeNumbers() => false;
    }

    protected class JSONListContext : TJSONProtocol.JSONBaseContext
    {
      private bool first = true;

      public JSONListContext(TJSONProtocol protocol)
        : base(protocol)
      {
      }

      public override void Write()
      {
        if (this.first)
          this.first = false;
        else
          this.proto.trans.Write(TJSONProtocol.COMMA);
      }

      public override void Read()
      {
        if (this.first)
          this.first = false;
        else
          this.proto.ReadJSONSyntaxChar(TJSONProtocol.COMMA);
      }
    }

    protected class JSONPairContext : TJSONProtocol.JSONBaseContext
    {
      private bool first = true;
      private bool colon = true;

      public JSONPairContext(TJSONProtocol proto)
        : base(proto)
      {
      }

      public override void Write()
      {
        if (this.first)
        {
          this.first = false;
          this.colon = true;
        }
        else
        {
          this.proto.trans.Write(!this.colon ? TJSONProtocol.COMMA : TJSONProtocol.COLON);
          this.colon = !this.colon;
        }
      }

      public override void Read()
      {
        if (this.first)
        {
          this.first = false;
          this.colon = true;
        }
        else
        {
          this.proto.ReadJSONSyntaxChar(!this.colon ? TJSONProtocol.COMMA : TJSONProtocol.COLON);
          this.colon = !this.colon;
        }
      }

      public override bool EscapeNumbers() => this.colon;
    }

    protected class LookaheadReader
    {
      protected TJSONProtocol proto;
      private bool hasData;
      private byte[] data = new byte[1];

      public LookaheadReader(TJSONProtocol proto) => this.proto = proto;

      public byte Read()
      {
        if (this.hasData)
          this.hasData = false;
        else
          this.proto.trans.ReadAll(this.data, 0, 1);
        return this.data[0];
      }

      public byte Peek()
      {
        if (!this.hasData)
          this.proto.trans.ReadAll(this.data, 0, 1);
        this.hasData = true;
        return this.data[0];
      }
    }
  }
}
