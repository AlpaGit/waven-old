// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.CommonProtocol.CastTarget
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using JetBrains.Annotations;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.CommonProtocol
{
  public sealed class CastTarget : 
    IMessage<CastTarget>,
    IMessage,
    IEquatable<CastTarget>,
    IDeepCloneable<CastTarget>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<CastTarget> _parser = new MessageParser<CastTarget>((Func<CastTarget>) (() => new CastTarget()));
    private UnknownFieldSet _unknownFields;
    public const int CellFieldNumber = 1;
    public const int EntityIdFieldNumber = 2;
    private object value_;
    private CastTarget.ValueOneofCase valueCase_;

    [DebuggerNonUserCode]
    public static MessageParser<CastTarget> Parser => CastTarget._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => CommonProtocolReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => CastTarget.Descriptor;

    [DebuggerNonUserCode]
    public CastTarget()
    {
    }

    [DebuggerNonUserCode]
    public CastTarget(CastTarget other)
      : this()
    {
      switch (other.ValueCase)
      {
        case CastTarget.ValueOneofCase.Cell:
          this.Cell = other.Cell.Clone();
          break;
        case CastTarget.ValueOneofCase.EntityId:
          this.EntityId = other.EntityId;
          break;
      }
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public CastTarget Clone() => new CastTarget(this);

    [DebuggerNonUserCode]
    public CellCoord Cell
    {
      get => this.valueCase_ != CastTarget.ValueOneofCase.Cell ? (CellCoord) null : (CellCoord) this.value_;
      set
      {
        this.value_ = (object) value;
        this.valueCase_ = value == null ? CastTarget.ValueOneofCase.None : CastTarget.ValueOneofCase.Cell;
      }
    }

    [DebuggerNonUserCode]
    public int EntityId
    {
      get => this.valueCase_ != CastTarget.ValueOneofCase.EntityId ? 0 : (int) this.value_;
      set
      {
        this.value_ = (object) value;
        this.valueCase_ = CastTarget.ValueOneofCase.EntityId;
      }
    }

    [DebuggerNonUserCode]
    public CastTarget.ValueOneofCase ValueCase => this.valueCase_;

    [DebuggerNonUserCode]
    public void ClearValue()
    {
      this.valueCase_ = CastTarget.ValueOneofCase.None;
      this.value_ = (object) null;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as CastTarget);

    [DebuggerNonUserCode]
    public bool Equals(CastTarget other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return object.Equals((object) this.Cell, (object) other.Cell) && this.EntityId == other.EntityId && this.ValueCase == other.ValueCase && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1;
      if (this.valueCase_ == CastTarget.ValueOneofCase.Cell)
        num ^= this.Cell.GetHashCode();
      if (this.valueCase_ == CastTarget.ValueOneofCase.EntityId)
        num ^= this.EntityId.GetHashCode();
      int hashCode = (int) ((CastTarget.ValueOneofCase) num ^ this.valueCase_);
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.valueCase_ == CastTarget.ValueOneofCase.Cell)
      {
        output.WriteRawTag((byte) 10);
        output.WriteMessage((IMessage) this.Cell);
      }
      if (this.valueCase_ == CastTarget.ValueOneofCase.EntityId)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.EntityId);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.valueCase_ == CastTarget.ValueOneofCase.Cell)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Cell);
      if (this.valueCase_ == CastTarget.ValueOneofCase.EntityId)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.EntityId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CastTarget other)
    {
      if (other == null)
        return;
      switch (other.ValueCase)
      {
        case CastTarget.ValueOneofCase.Cell:
          if (this.Cell == null)
            this.Cell = new CellCoord();
          this.Cell.MergeFrom(other.Cell);
          break;
        case CastTarget.ValueOneofCase.EntityId:
          this.EntityId = other.EntityId;
          break;
      }
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
          case 10:
            CellCoord builder = new CellCoord();
            if (this.valueCase_ == CastTarget.ValueOneofCase.Cell)
              builder.MergeFrom(this.Cell);
            input.ReadMessage((IMessage) builder);
            this.Cell = builder;
            continue;
          case 16:
            this.EntityId = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (CastTarget);

    [Pure]
    public Target ToTarget(FightStatus fightStatus)
    {
      switch (this.ValueCase)
      {
        case CastTarget.ValueOneofCase.Cell:
          return new Target((Coord) this.Cell);
        case CastTarget.ValueOneofCase.EntityId:
          return new Target((IEntity) fightStatus.GetEntity(this.EntityId));
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public enum ValueOneofCase
    {
      None,
      Cell,
      EntityId,
    }
  }
}
