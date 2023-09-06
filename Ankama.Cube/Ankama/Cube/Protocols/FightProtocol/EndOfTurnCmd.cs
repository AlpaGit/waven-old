// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.EndOfTurnCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class EndOfTurnCmd : 
    IMessage<EndOfTurnCmd>,
    IMessage,
    IEquatable<EndOfTurnCmd>,
    IDeepCloneable<EndOfTurnCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<EndOfTurnCmd> _parser = new MessageParser<EndOfTurnCmd>((Func<EndOfTurnCmd>) (() => new EndOfTurnCmd()));
    private UnknownFieldSet _unknownFields;
    public const int TurnIndexFieldNumber = 1;
    private int turnIndex_;

    [DebuggerNonUserCode]
    public static MessageParser<EndOfTurnCmd> Parser => EndOfTurnCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[17];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => EndOfTurnCmd.Descriptor;

    [DebuggerNonUserCode]
    public EndOfTurnCmd()
    {
    }

    [DebuggerNonUserCode]
    public EndOfTurnCmd(EndOfTurnCmd other)
      : this()
    {
      this.turnIndex_ = other.turnIndex_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public EndOfTurnCmd Clone() => new EndOfTurnCmd(this);

    [DebuggerNonUserCode]
    public int TurnIndex
    {
      get => this.turnIndex_;
      set => this.turnIndex_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as EndOfTurnCmd);

    [DebuggerNonUserCode]
    public bool Equals(EndOfTurnCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.TurnIndex == other.TurnIndex && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.TurnIndex != 0)
        hashCode ^= this.TurnIndex.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.TurnIndex != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.TurnIndex);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.TurnIndex != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.TurnIndex);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(EndOfTurnCmd other)
    {
      if (other == null)
        return;
      if (other.TurnIndex != 0)
        this.TurnIndex = other.TurnIndex;
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
          this.TurnIndex = input.ReadInt32();
      }
    }

    public string ToDiagnosticString() => nameof (EndOfTurnCmd);
  }
}
