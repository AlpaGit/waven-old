// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.SaveDeckResultEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class SaveDeckResultEvent : 
    IMessage<SaveDeckResultEvent>,
    IMessage,
    IEquatable<SaveDeckResultEvent>,
    IDeepCloneable<SaveDeckResultEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<SaveDeckResultEvent> _parser = new MessageParser<SaveDeckResultEvent>((Func<SaveDeckResultEvent>) (() => new SaveDeckResultEvent()));
    private UnknownFieldSet _unknownFields;
    public const int ResultFieldNumber = 1;
    private CmdResult result_;
    public const int DeckIdFieldNumber = 2;
    private int deckId_;

    [DebuggerNonUserCode]
    public static MessageParser<SaveDeckResultEvent> Parser => SaveDeckResultEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[8];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SaveDeckResultEvent.Descriptor;

    [DebuggerNonUserCode]
    public SaveDeckResultEvent()
    {
    }

    [DebuggerNonUserCode]
    public SaveDeckResultEvent(SaveDeckResultEvent other)
      : this()
    {
      this.result_ = other.result_;
      this.deckId_ = other.deckId_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public SaveDeckResultEvent Clone() => new SaveDeckResultEvent(this);

    [DebuggerNonUserCode]
    public CmdResult Result
    {
      get => this.result_;
      set => this.result_ = value;
    }

    [DebuggerNonUserCode]
    public int DeckId
    {
      get => this.deckId_;
      set => this.deckId_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as SaveDeckResultEvent);

    [DebuggerNonUserCode]
    public bool Equals(SaveDeckResultEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.Result == other.Result && this.DeckId == other.DeckId && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.Result != CmdResult.Failed)
        hashCode ^= this.Result.GetHashCode();
      if (this.DeckId != 0)
        hashCode ^= this.DeckId.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.Result != CmdResult.Failed)
      {
        output.WriteRawTag((byte) 8);
        output.WriteEnum((int) this.Result);
      }
      if (this.DeckId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.DeckId);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.Result != CmdResult.Failed)
        size += 1 + CodedOutputStream.ComputeEnumSize((int) this.Result);
      if (this.DeckId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.DeckId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(SaveDeckResultEvent other)
    {
      if (other == null)
        return;
      if (other.Result != CmdResult.Failed)
        this.Result = other.Result;
      if (other.DeckId != 0)
        this.DeckId = other.DeckId;
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
            this.result_ = (CmdResult) input.ReadEnum();
            continue;
          case 16:
            this.DeckId = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (SaveDeckResultEvent);
  }
}
