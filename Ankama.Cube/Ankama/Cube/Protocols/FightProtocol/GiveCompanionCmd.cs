// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.GiveCompanionCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class GiveCompanionCmd : 
    IMessage<GiveCompanionCmd>,
    IMessage,
    IEquatable<GiveCompanionCmd>,
    IDeepCloneable<GiveCompanionCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<GiveCompanionCmd> _parser = new MessageParser<GiveCompanionCmd>((Func<GiveCompanionCmd>) (() => new GiveCompanionCmd()));
    private UnknownFieldSet _unknownFields;
    public const int CompanionDefIdFieldNumber = 1;
    private int companionDefId_;
    public const int TargetFightIdFieldNumber = 2;
    private int targetFightId_;
    public const int TargetPlayerIdFieldNumber = 3;
    private int targetPlayerId_;

    [DebuggerNonUserCode]
    public static MessageParser<GiveCompanionCmd> Parser => GiveCompanionCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[15];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => GiveCompanionCmd.Descriptor;

    [DebuggerNonUserCode]
    public GiveCompanionCmd()
    {
    }

    [DebuggerNonUserCode]
    public GiveCompanionCmd(GiveCompanionCmd other)
      : this()
    {
      this.companionDefId_ = other.companionDefId_;
      this.targetFightId_ = other.targetFightId_;
      this.targetPlayerId_ = other.targetPlayerId_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public GiveCompanionCmd Clone() => new GiveCompanionCmd(this);

    [DebuggerNonUserCode]
    public int CompanionDefId
    {
      get => this.companionDefId_;
      set => this.companionDefId_ = value;
    }

    [DebuggerNonUserCode]
    public int TargetFightId
    {
      get => this.targetFightId_;
      set => this.targetFightId_ = value;
    }

    [DebuggerNonUserCode]
    public int TargetPlayerId
    {
      get => this.targetPlayerId_;
      set => this.targetPlayerId_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as GiveCompanionCmd);

    [DebuggerNonUserCode]
    public bool Equals(GiveCompanionCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.CompanionDefId == other.CompanionDefId && this.TargetFightId == other.TargetFightId && this.TargetPlayerId == other.TargetPlayerId && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      int num1;
      if (this.CompanionDefId != 0)
      {
        int num2 = hashCode1;
        num1 = this.CompanionDefId;
        int hashCode2 = num1.GetHashCode();
        hashCode1 = num2 ^ hashCode2;
      }
      if (this.TargetFightId != 0)
      {
        int num3 = hashCode1;
        num1 = this.TargetFightId;
        int hashCode3 = num1.GetHashCode();
        hashCode1 = num3 ^ hashCode3;
      }
      if (this.TargetPlayerId != 0)
      {
        int num4 = hashCode1;
        num1 = this.TargetPlayerId;
        int hashCode4 = num1.GetHashCode();
        hashCode1 = num4 ^ hashCode4;
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
      if (this.CompanionDefId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.CompanionDefId);
      }
      if (this.TargetFightId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.TargetFightId);
      }
      if (this.TargetPlayerId != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.TargetPlayerId);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.CompanionDefId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.CompanionDefId);
      if (this.TargetFightId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetFightId);
      if (this.TargetPlayerId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetPlayerId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(GiveCompanionCmd other)
    {
      if (other == null)
        return;
      if (other.CompanionDefId != 0)
        this.CompanionDefId = other.CompanionDefId;
      if (other.TargetFightId != 0)
        this.TargetFightId = other.TargetFightId;
      if (other.TargetPlayerId != 0)
        this.TargetPlayerId = other.TargetPlayerId;
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
            this.CompanionDefId = input.ReadInt32();
            continue;
          case 16:
            this.TargetFightId = input.ReadInt32();
            continue;
          case 24:
            this.TargetPlayerId = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (GiveCompanionCmd);
  }
}
