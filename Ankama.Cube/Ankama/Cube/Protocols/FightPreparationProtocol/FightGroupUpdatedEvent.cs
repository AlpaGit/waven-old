// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightPreparationProtocol.FightGroupUpdatedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.PlayerProtocol;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightPreparationProtocol
{
  public sealed class FightGroupUpdatedEvent : 
    IMessage<FightGroupUpdatedEvent>,
    IMessage,
    IEquatable<FightGroupUpdatedEvent>,
    IDeepCloneable<FightGroupUpdatedEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightGroupUpdatedEvent> _parser = new MessageParser<FightGroupUpdatedEvent>((Func<FightGroupUpdatedEvent>) (() => new FightGroupUpdatedEvent()));
    private UnknownFieldSet _unknownFields;
    public const int GroupRemovedFieldNumber = 1;
    private bool groupRemoved_;
    public const int MembersFieldNumber = 2;
    private static readonly FieldCodec<PlayerPublicInfo> _repeated_members_codec = FieldCodec.ForMessage<PlayerPublicInfo>(18U, PlayerPublicInfo.Parser);
    private readonly RepeatedField<PlayerPublicInfo> members_ = new RepeatedField<PlayerPublicInfo>();

    [DebuggerNonUserCode]
    public static MessageParser<FightGroupUpdatedEvent> Parser => FightGroupUpdatedEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightPreparationProtocolReflection.Descriptor.MessageTypes[2];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightGroupUpdatedEvent.Descriptor;

    [DebuggerNonUserCode]
    public FightGroupUpdatedEvent()
    {
    }

    [DebuggerNonUserCode]
    public FightGroupUpdatedEvent(FightGroupUpdatedEvent other)
      : this()
    {
      this.groupRemoved_ = other.groupRemoved_;
      this.members_ = other.members_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightGroupUpdatedEvent Clone() => new FightGroupUpdatedEvent(this);

    [DebuggerNonUserCode]
    public bool GroupRemoved
    {
      get => this.groupRemoved_;
      set => this.groupRemoved_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<PlayerPublicInfo> Members => this.members_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightGroupUpdatedEvent);

    [DebuggerNonUserCode]
    public bool Equals(FightGroupUpdatedEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.GroupRemoved == other.GroupRemoved && this.members_.Equals(other.members_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1;
      if (this.GroupRemoved)
        num ^= this.GroupRemoved.GetHashCode();
      int hashCode = num ^ this.members_.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.GroupRemoved)
      {
        output.WriteRawTag((byte) 8);
        output.WriteBool(this.GroupRemoved);
      }
      this.members_.WriteTo(output, FightGroupUpdatedEvent._repeated_members_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0;
      if (this.GroupRemoved)
        num += 2;
      int size = num + this.members_.CalculateSize(FightGroupUpdatedEvent._repeated_members_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightGroupUpdatedEvent other)
    {
      if (other == null)
        return;
      if (other.GroupRemoved)
        this.GroupRemoved = other.GroupRemoved;
      this.members_.Add((IEnumerable<PlayerPublicInfo>) other.members_);
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
            this.GroupRemoved = input.ReadBool();
            continue;
          case 18:
            this.members_.AddEntriesFrom(input, FightGroupUpdatedEvent._repeated_members_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightGroupUpdatedEvent);
  }
}
