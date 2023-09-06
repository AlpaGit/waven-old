// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.FightInfoEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.FightCommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class FightInfoEvent : 
    IMessage<FightInfoEvent>,
    IMessage,
    IEquatable<FightInfoEvent>,
    IDeepCloneable<FightInfoEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightInfoEvent> _parser = new MessageParser<FightInfoEvent>((Func<FightInfoEvent>) (() => new FightInfoEvent()));
    private UnknownFieldSet _unknownFields;
    public const int FightInfoFieldNumber = 1;
    private FightInfo fightInfo_;

    [DebuggerNonUserCode]
    public static MessageParser<FightInfoEvent> Parser => FightInfoEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[5];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightInfoEvent.Descriptor;

    [DebuggerNonUserCode]
    public FightInfoEvent()
    {
    }

    [DebuggerNonUserCode]
    public FightInfoEvent(FightInfoEvent other)
      : this()
    {
      this.fightInfo_ = other.fightInfo_ != null ? other.fightInfo_.Clone() : (FightInfo) null;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightInfoEvent Clone() => new FightInfoEvent(this);

    [DebuggerNonUserCode]
    public FightInfo FightInfo
    {
      get => this.fightInfo_;
      set => this.fightInfo_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightInfoEvent);

    [DebuggerNonUserCode]
    public bool Equals(FightInfoEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return object.Equals((object) this.FightInfo, (object) other.FightInfo) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.fightInfo_ != null)
        hashCode ^= this.FightInfo.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.fightInfo_ != null)
      {
        output.WriteRawTag((byte) 10);
        output.WriteMessage((IMessage) this.FightInfo);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.fightInfo_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.FightInfo);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightInfoEvent other)
    {
      if (other == null)
        return;
      if (other.fightInfo_ != null)
      {
        if (this.fightInfo_ == null)
          this.fightInfo_ = new FightInfo();
        this.FightInfo.MergeFrom(other.FightInfo);
      }
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
        {
          this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
        }
        else
        {
          if (this.fightInfo_ == null)
            this.fightInfo_ = new FightInfo();
          input.ReadMessage((IMessage) this.fightInfo_);
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightInfoEvent);
  }
}
