// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.FightStartedEvent
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
  public sealed class FightStartedEvent : 
    IMessage<FightStartedEvent>,
    IMessage,
    IEquatable<FightStartedEvent>,
    IDeepCloneable<FightStartedEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightStartedEvent> _parser = new MessageParser<FightStartedEvent>((Func<FightStartedEvent>) (() => new FightStartedEvent()));
    private UnknownFieldSet _unknownFields;
    public const int FightInfoFieldNumber = 1;
    private FightInfo fightInfo_;
    public const int FightDefIdFieldNumber = 2;
    private int fightDefId_;
    public const int FightTypeFieldNumber = 3;
    private int fightType_;

    [DebuggerNonUserCode]
    public static MessageParser<FightStartedEvent> Parser => FightStartedEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightStartedEvent.Descriptor;

    [DebuggerNonUserCode]
    public FightStartedEvent()
    {
    }

    [DebuggerNonUserCode]
    public FightStartedEvent(FightStartedEvent other)
      : this()
    {
      this.fightInfo_ = other.fightInfo_ != null ? other.fightInfo_.Clone() : (FightInfo) null;
      this.fightDefId_ = other.fightDefId_;
      this.fightType_ = other.fightType_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightStartedEvent Clone() => new FightStartedEvent(this);

    [DebuggerNonUserCode]
    public FightInfo FightInfo
    {
      get => this.fightInfo_;
      set => this.fightInfo_ = value;
    }

    [DebuggerNonUserCode]
    public int FightDefId
    {
      get => this.fightDefId_;
      set => this.fightDefId_ = value;
    }

    [DebuggerNonUserCode]
    public int FightType
    {
      get => this.fightType_;
      set => this.fightType_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightStartedEvent);

    [DebuggerNonUserCode]
    public bool Equals(FightStartedEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return object.Equals((object) this.FightInfo, (object) other.FightInfo) && this.FightDefId == other.FightDefId && this.FightType == other.FightType && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      if (this.fightInfo_ != null)
        hashCode1 ^= this.FightInfo.GetHashCode();
      int num1;
      if (this.FightDefId != 0)
      {
        int num2 = hashCode1;
        num1 = this.FightDefId;
        int hashCode2 = num1.GetHashCode();
        hashCode1 = num2 ^ hashCode2;
      }
      if (this.FightType != 0)
      {
        int num3 = hashCode1;
        num1 = this.FightType;
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
      if (this.fightInfo_ != null)
      {
        output.WriteRawTag((byte) 10);
        output.WriteMessage((IMessage) this.FightInfo);
      }
      if (this.FightDefId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.FightDefId);
      }
      if (this.FightType != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.FightType);
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
      if (this.FightDefId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.FightDefId);
      if (this.FightType != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.FightType);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightStartedEvent other)
    {
      if (other == null)
        return;
      if (other.fightInfo_ != null)
      {
        if (this.fightInfo_ == null)
          this.fightInfo_ = new FightInfo();
        this.FightInfo.MergeFrom(other.FightInfo);
      }
      if (other.FightDefId != 0)
        this.FightDefId = other.FightDefId;
      if (other.FightType != 0)
        this.FightType = other.FightType;
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
            if (this.fightInfo_ == null)
              this.fightInfo_ = new FightInfo();
            input.ReadMessage((IMessage) this.fightInfo_);
            continue;
          case 16:
            this.FightDefId = input.ReadInt32();
            continue;
          case 24:
            this.FightType = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightStartedEvent);
  }
}
