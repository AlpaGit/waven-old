// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.CommonProtocol.CellCoord
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using JetBrains.Annotations;
using System;
using System.Diagnostics;
using UnityEngine;

namespace Ankama.Cube.Protocols.CommonProtocol
{
  public sealed class CellCoord : 
    IMessage<CellCoord>,
    IMessage,
    IEquatable<CellCoord>,
    IDeepCloneable<CellCoord>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<CellCoord> _parser = new MessageParser<CellCoord>((Func<CellCoord>) (() => new CellCoord()));
    private UnknownFieldSet _unknownFields;
    public const int XFieldNumber = 1;
    private int x_;
    public const int YFieldNumber = 2;
    private int y_;

    [DebuggerNonUserCode]
    public static MessageParser<CellCoord> Parser => CellCoord._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => CommonProtocolReflection.Descriptor.MessageTypes[1];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => CellCoord.Descriptor;

    [DebuggerNonUserCode]
    public CellCoord()
    {
    }

    [DebuggerNonUserCode]
    public CellCoord(CellCoord other)
      : this()
    {
      this.x_ = other.x_;
      this.y_ = other.y_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public CellCoord Clone() => new CellCoord(this);

    [DebuggerNonUserCode]
    public int X
    {
      get => this.x_;
      set => this.x_ = value;
    }

    [DebuggerNonUserCode]
    public int Y
    {
      get => this.y_;
      set => this.y_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as CellCoord);

    [DebuggerNonUserCode]
    public bool Equals(CellCoord other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.X == other.X && this.Y == other.Y && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      int num1;
      if (this.X != 0)
      {
        int num2 = hashCode1;
        num1 = this.X;
        int hashCode2 = num1.GetHashCode();
        hashCode1 = num2 ^ hashCode2;
      }
      if (this.Y != 0)
      {
        int num3 = hashCode1;
        num1 = this.Y;
        int hashCode3 = num1.GetHashCode();
        hashCode1 = num3 ^ hashCode3;
      }
      if (this._unknownFields != null)
        hashCode1 ^= this._unknownFields.GetHashCode();
      return hashCode1;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.X != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.X);
      }
      if (this.Y != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.Y);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.X != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.X);
      if (this.Y != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.Y);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CellCoord other)
    {
      if (other == null)
        return;
      if (other.X != 0)
        this.X = other.X;
      if (other.Y != 0)
        this.Y = other.Y;
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 8:
            this.X = input.ReadInt32();
            continue;
          case 16:
            this.Y = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (CellCoord);

    [Pure]
    public static explicit operator Coord(CellCoord value) => new Coord(value.x_, value.y_);

    [Pure]
    public static explicit operator Vector2Int(CellCoord value) => new Vector2Int(value.x_, value.y_);
  }
}
