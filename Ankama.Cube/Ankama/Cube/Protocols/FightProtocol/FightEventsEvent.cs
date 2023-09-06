// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.FightEventsEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Events;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class FightEventsEvent : 
    IMessage<FightEventsEvent>,
    IMessage,
    IEquatable<FightEventsEvent>,
    IDeepCloneable<FightEventsEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightEventsEvent> _parser = new MessageParser<FightEventsEvent>((Func<FightEventsEvent>) (() => new FightEventsEvent()));
    private UnknownFieldSet _unknownFields;
    public const int FightIdFieldNumber = 1;
    private int fightId_;
    public const int EventsFieldNumber = 2;
    private static readonly FieldCodec<FightEventData> _repeated_events_codec = FieldCodec.ForMessage<FightEventData>(18U, FightEventData.Parser);
    private readonly RepeatedField<FightEventData> events_ = new RepeatedField<FightEventData>();

    [DebuggerNonUserCode]
    public static MessageParser<FightEventsEvent> Parser => FightEventsEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[19];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightEventsEvent.Descriptor;

    [DebuggerNonUserCode]
    public FightEventsEvent()
    {
    }

    [DebuggerNonUserCode]
    public FightEventsEvent(FightEventsEvent other)
      : this()
    {
      this.fightId_ = other.fightId_;
      this.events_ = other.events_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightEventsEvent Clone() => new FightEventsEvent(this);

    [DebuggerNonUserCode]
    public int FightId
    {
      get => this.fightId_;
      set => this.fightId_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<FightEventData> Events => this.events_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightEventsEvent);

    [DebuggerNonUserCode]
    public bool Equals(FightEventsEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.FightId == other.FightId && this.events_.Equals(other.events_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1;
      if (this.FightId != 0)
        num ^= this.FightId.GetHashCode();
      int hashCode = num ^ this.events_.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.FightId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.FightId);
      }
      this.events_.WriteTo(output, FightEventsEvent._repeated_events_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0;
      if (this.FightId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.FightId);
      int size = num + this.events_.CalculateSize(FightEventsEvent._repeated_events_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightEventsEvent other)
    {
      if (other == null)
        return;
      if (other.FightId != 0)
        this.FightId = other.FightId;
      this.events_.Add((IEnumerable<FightEventData>) other.events_);
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
            this.FightId = input.ReadInt32();
            continue;
          case 18:
            this.events_.AddEntriesFrom(input, FightEventsEvent._repeated_events_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightEventsEvent);
  }
}
