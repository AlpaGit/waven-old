// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.PlayerLeftFightEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class PlayerLeftFightEvent : 
    IMessage<PlayerLeftFightEvent>,
    IMessage,
    IEquatable<PlayerLeftFightEvent>,
    IDeepCloneable<PlayerLeftFightEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<PlayerLeftFightEvent> _parser = new MessageParser<PlayerLeftFightEvent>((Func<PlayerLeftFightEvent>) (() => new PlayerLeftFightEvent()));
    private UnknownFieldSet _unknownFields;
    public const int FightIdFieldNumber = 1;
    private int fightId_;
    public const int PlayerIdFieldNumber = 2;
    private int playerId_;

    [DebuggerNonUserCode]
    public static MessageParser<PlayerLeftFightEvent> Parser => PlayerLeftFightEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[10];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerLeftFightEvent.Descriptor;

    [DebuggerNonUserCode]
    public PlayerLeftFightEvent()
    {
    }

    [DebuggerNonUserCode]
    public PlayerLeftFightEvent(PlayerLeftFightEvent other)
      : this()
    {
      this.fightId_ = other.fightId_;
      this.playerId_ = other.playerId_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public PlayerLeftFightEvent Clone() => new PlayerLeftFightEvent(this);

    [DebuggerNonUserCode]
    public int FightId
    {
      get => this.fightId_;
      set => this.fightId_ = value;
    }

    [DebuggerNonUserCode]
    public int PlayerId
    {
      get => this.playerId_;
      set => this.playerId_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as PlayerLeftFightEvent);

    [DebuggerNonUserCode]
    public bool Equals(PlayerLeftFightEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.FightId == other.FightId && this.PlayerId == other.PlayerId && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      int num1;
      if (this.FightId != 0)
      {
        int num2 = hashCode1;
        num1 = this.FightId;
        int hashCode2 = num1.GetHashCode();
        hashCode1 = num2 ^ hashCode2;
      }
      if (this.PlayerId != 0)
      {
        int num3 = hashCode1;
        num1 = this.PlayerId;
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
      if (this.FightId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.FightId);
      }
      if (this.PlayerId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.PlayerId);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.FightId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.FightId);
      if (this.PlayerId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(PlayerLeftFightEvent other)
    {
      if (other == null)
        return;
      if (other.FightId != 0)
        this.FightId = other.FightId;
      if (other.PlayerId != 0)
        this.PlayerId = other.PlayerId;
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
          case 16:
            this.PlayerId = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (PlayerLeftFightEvent);
  }
}
