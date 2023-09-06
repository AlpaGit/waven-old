// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.MoveEntityCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class MoveEntityCmd : 
    IMessage<MoveEntityCmd>,
    IMessage,
    IEquatable<MoveEntityCmd>,
    IDeepCloneable<MoveEntityCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<MoveEntityCmd> _parser = new MessageParser<MoveEntityCmd>((Func<MoveEntityCmd>) (() => new MoveEntityCmd()));
    private UnknownFieldSet _unknownFields;
    public const int EntityIdFieldNumber = 1;
    private int entityId_;
    public const int PathFieldNumber = 2;
    private static readonly FieldCodec<CellCoord> _repeated_path_codec = FieldCodec.ForMessage<CellCoord>(18U, CellCoord.Parser);
    private readonly RepeatedField<CellCoord> path_ = new RepeatedField<CellCoord>();
    public const int EntityToAttackIdFieldNumber = 3;
    private static readonly FieldCodec<int?> _single_entityToAttackId_codec = FieldCodec.ForStructWrapper<int>(26U);
    private int? entityToAttackId_;

    [DebuggerNonUserCode]
    public static MessageParser<MoveEntityCmd> Parser => MoveEntityCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[12];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => MoveEntityCmd.Descriptor;

    [DebuggerNonUserCode]
    public MoveEntityCmd()
    {
    }

    [DebuggerNonUserCode]
    public MoveEntityCmd(MoveEntityCmd other)
      : this()
    {
      this.entityId_ = other.entityId_;
      this.path_ = other.path_.Clone();
      this.EntityToAttackId = other.EntityToAttackId;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public MoveEntityCmd Clone() => new MoveEntityCmd(this);

    [DebuggerNonUserCode]
    public int EntityId
    {
      get => this.entityId_;
      set => this.entityId_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<CellCoord> Path => this.path_;

    [DebuggerNonUserCode]
    public int? EntityToAttackId
    {
      get => this.entityToAttackId_;
      set => this.entityToAttackId_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as MoveEntityCmd);

    [DebuggerNonUserCode]
    public bool Equals(MoveEntityCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      if (this.EntityId != other.EntityId || !this.path_.Equals(other.path_))
        return false;
      int? entityToAttackId1 = this.EntityToAttackId;
      int? entityToAttackId2 = other.EntityToAttackId;
      return entityToAttackId1.GetValueOrDefault() == entityToAttackId2.GetValueOrDefault() & entityToAttackId1.HasValue == entityToAttackId2.HasValue && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1;
      if (this.EntityId != 0)
        num ^= this.EntityId.GetHashCode();
      int hashCode = num ^ this.path_.GetHashCode();
      if (this.entityToAttackId_.HasValue)
        hashCode ^= this.EntityToAttackId.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.EntityId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.EntityId);
      }
      this.path_.WriteTo(output, MoveEntityCmd._repeated_path_codec);
      if (this.entityToAttackId_.HasValue)
        MoveEntityCmd._single_entityToAttackId_codec.WriteTagAndValue(output, this.EntityToAttackId);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0;
      if (this.EntityId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.EntityId);
      int size = num + this.path_.CalculateSize(MoveEntityCmd._repeated_path_codec);
      if (this.entityToAttackId_.HasValue)
        size += MoveEntityCmd._single_entityToAttackId_codec.CalculateSizeWithTag(this.EntityToAttackId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(MoveEntityCmd other)
    {
      if (other == null)
        return;
      if (other.EntityId != 0)
        this.EntityId = other.EntityId;
      this.path_.Add((IEnumerable<CellCoord>) other.path_);
      if (other.entityToAttackId_.HasValue)
      {
        if (this.entityToAttackId_.HasValue)
        {
          int? entityToAttackId = other.EntityToAttackId;
          int num = 0;
          if (entityToAttackId.GetValueOrDefault() == num & entityToAttackId.HasValue)
            goto label_7;
        }
        this.EntityToAttackId = other.EntityToAttackId;
      }
label_7:
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num1;
      while ((num1 = input.ReadTag()) != 0U)
      {
        switch (num1)
        {
          case 8:
            this.EntityId = input.ReadInt32();
            continue;
          case 18:
            this.path_.AddEntriesFrom(input, MoveEntityCmd._repeated_path_codec);
            continue;
          case 26:
            int? nullable1 = MoveEntityCmd._single_entityToAttackId_codec.Read(input);
            if (this.entityToAttackId_.HasValue)
            {
              int? nullable2 = nullable1;
              int num2 = 0;
              if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                continue;
            }
            this.EntityToAttackId = nullable1;
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (MoveEntityCmd);
  }
}
