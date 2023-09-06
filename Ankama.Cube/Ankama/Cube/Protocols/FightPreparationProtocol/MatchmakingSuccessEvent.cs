// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightPreparationProtocol.MatchmakingSuccessEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightPreparationProtocol
{
  public sealed class MatchmakingSuccessEvent : 
    IMessage<MatchmakingSuccessEvent>,
    IMessage,
    IEquatable<MatchmakingSuccessEvent>,
    IDeepCloneable<MatchmakingSuccessEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<MatchmakingSuccessEvent> _parser = new MessageParser<MatchmakingSuccessEvent>((Func<MatchmakingSuccessEvent>) (() => new MatchmakingSuccessEvent()));
    private UnknownFieldSet _unknownFields;
    public const int FightDefIdFieldNumber = 1;
    private int fightDefId_;

    [DebuggerNonUserCode]
    public static MessageParser<MatchmakingSuccessEvent> Parser => MatchmakingSuccessEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightPreparationProtocolReflection.Descriptor.MessageTypes[7];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => MatchmakingSuccessEvent.Descriptor;

    [DebuggerNonUserCode]
    public MatchmakingSuccessEvent()
    {
    }

    [DebuggerNonUserCode]
    public MatchmakingSuccessEvent(MatchmakingSuccessEvent other)
      : this()
    {
      this.fightDefId_ = other.fightDefId_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public MatchmakingSuccessEvent Clone() => new MatchmakingSuccessEvent(this);

    [DebuggerNonUserCode]
    public int FightDefId
    {
      get => this.fightDefId_;
      set => this.fightDefId_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as MatchmakingSuccessEvent);

    [DebuggerNonUserCode]
    public bool Equals(MatchmakingSuccessEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.FightDefId == other.FightDefId && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.FightDefId != 0)
        hashCode ^= this.FightDefId.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.FightDefId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.FightDefId);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.FightDefId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.FightDefId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(MatchmakingSuccessEvent other)
    {
      if (other == null)
        return;
      if (other.FightDefId != 0)
        this.FightDefId = other.FightDefId;
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 8U)
          this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
        else
          this.FightDefId = input.ReadInt32();
      }
    }

    public string ToDiagnosticString() => nameof (MatchmakingSuccessEvent);
  }
}
