// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.FightSnapshotEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class FightSnapshotEvent : 
    IMessage<FightSnapshotEvent>,
    IMessage,
    IEquatable<FightSnapshotEvent>,
    IDeepCloneable<FightSnapshotEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightSnapshotEvent> _parser = new MessageParser<FightSnapshotEvent>((Func<FightSnapshotEvent>) (() => new FightSnapshotEvent()));
    private UnknownFieldSet _unknownFields;
    public const int FightsSnapshotsFieldNumber = 1;
    private static readonly FieldCodec<FightSnapshot> _repeated_fightsSnapshots_codec = FieldCodec.ForMessage<FightSnapshot>(10U, FightSnapshot.Parser);
    private readonly RepeatedField<FightSnapshot> fightsSnapshots_ = new RepeatedField<FightSnapshot>();
    public const int OwnFightIdFieldNumber = 2;
    private int ownFightId_;
    public const int OwnPlayerIdFieldNumber = 3;
    private int ownPlayerId_;
    public const int OwnSpellsIdsFieldNumber = 4;
    private static readonly FieldCodec<FightSnapshotSpell> _repeated_ownSpellsIds_codec = FieldCodec.ForMessage<FightSnapshotSpell>(34U, FightSnapshotSpell.Parser);
    private readonly RepeatedField<FightSnapshotSpell> ownSpellsIds_ = new RepeatedField<FightSnapshotSpell>();

    [DebuggerNonUserCode]
    public static MessageParser<FightSnapshotEvent> Parser => FightSnapshotEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[7];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightSnapshotEvent.Descriptor;

    [DebuggerNonUserCode]
    public FightSnapshotEvent()
    {
    }

    [DebuggerNonUserCode]
    public FightSnapshotEvent(FightSnapshotEvent other)
      : this()
    {
      this.fightsSnapshots_ = other.fightsSnapshots_.Clone();
      this.ownFightId_ = other.ownFightId_;
      this.ownPlayerId_ = other.ownPlayerId_;
      this.ownSpellsIds_ = other.ownSpellsIds_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightSnapshotEvent Clone() => new FightSnapshotEvent(this);

    [DebuggerNonUserCode]
    public RepeatedField<FightSnapshot> FightsSnapshots => this.fightsSnapshots_;

    [DebuggerNonUserCode]
    public int OwnFightId
    {
      get => this.ownFightId_;
      set => this.ownFightId_ = value;
    }

    [DebuggerNonUserCode]
    public int OwnPlayerId
    {
      get => this.ownPlayerId_;
      set => this.ownPlayerId_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<FightSnapshotSpell> OwnSpellsIds => this.ownSpellsIds_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightSnapshotEvent);

    [DebuggerNonUserCode]
    public bool Equals(FightSnapshotEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.fightsSnapshots_.Equals(other.fightsSnapshots_) && this.OwnFightId == other.OwnFightId && this.OwnPlayerId == other.OwnPlayerId && this.ownSpellsIds_.Equals(other.ownSpellsIds_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1 ^ this.fightsSnapshots_.GetHashCode();
      if (this.OwnFightId != 0)
        num ^= this.OwnFightId.GetHashCode();
      if (this.OwnPlayerId != 0)
        num ^= this.OwnPlayerId.GetHashCode();
      int hashCode = num ^ this.ownSpellsIds_.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      this.fightsSnapshots_.WriteTo(output, FightSnapshotEvent._repeated_fightsSnapshots_codec);
      if (this.OwnFightId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.OwnFightId);
      }
      if (this.OwnPlayerId != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.OwnPlayerId);
      }
      this.ownSpellsIds_.WriteTo(output, FightSnapshotEvent._repeated_ownSpellsIds_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0 + this.fightsSnapshots_.CalculateSize(FightSnapshotEvent._repeated_fightsSnapshots_codec);
      if (this.OwnFightId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.OwnFightId);
      if (this.OwnPlayerId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.OwnPlayerId);
      int size = num + this.ownSpellsIds_.CalculateSize(FightSnapshotEvent._repeated_ownSpellsIds_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightSnapshotEvent other)
    {
      if (other == null)
        return;
      this.fightsSnapshots_.Add((IEnumerable<FightSnapshot>) other.fightsSnapshots_);
      if (other.OwnFightId != 0)
        this.OwnFightId = other.OwnFightId;
      if (other.OwnPlayerId != 0)
        this.OwnPlayerId = other.OwnPlayerId;
      this.ownSpellsIds_.Add((IEnumerable<FightSnapshotSpell>) other.ownSpellsIds_);
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
            this.fightsSnapshots_.AddEntriesFrom(input, FightSnapshotEvent._repeated_fightsSnapshots_codec);
            continue;
          case 16:
            this.OwnFightId = input.ReadInt32();
            continue;
          case 24:
            this.OwnPlayerId = input.ReadInt32();
            continue;
          case 34:
            this.ownSpellsIds_.AddEntriesFrom(input, FightSnapshotEvent._repeated_ownSpellsIds_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightSnapshotEvent);
  }
}
