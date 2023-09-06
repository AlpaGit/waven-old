// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightPreparationProtocol.LaunchMatchmakingResultEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightPreparationProtocol
{
  public sealed class LaunchMatchmakingResultEvent : 
    IMessage<LaunchMatchmakingResultEvent>,
    IMessage,
    IEquatable<LaunchMatchmakingResultEvent>,
    IDeepCloneable<LaunchMatchmakingResultEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<LaunchMatchmakingResultEvent> _parser = new MessageParser<LaunchMatchmakingResultEvent>((Func<LaunchMatchmakingResultEvent>) (() => new LaunchMatchmakingResultEvent()));
    private UnknownFieldSet _unknownFields;
    public const int FightDefIdFieldNumber = 1;
    private int fightDefId_;
    public const int ResultFieldNumber = 2;
    private LaunchMatchmakingResultEvent.Types.Result result_;

    [DebuggerNonUserCode]
    public static MessageParser<LaunchMatchmakingResultEvent> Parser => LaunchMatchmakingResultEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightPreparationProtocolReflection.Descriptor.MessageTypes[5];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => LaunchMatchmakingResultEvent.Descriptor;

    [DebuggerNonUserCode]
    public LaunchMatchmakingResultEvent()
    {
    }

    [DebuggerNonUserCode]
    public LaunchMatchmakingResultEvent(LaunchMatchmakingResultEvent other)
      : this()
    {
      this.fightDefId_ = other.fightDefId_;
      this.result_ = other.result_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public LaunchMatchmakingResultEvent Clone() => new LaunchMatchmakingResultEvent(this);

    [DebuggerNonUserCode]
    public int FightDefId
    {
      get => this.fightDefId_;
      set => this.fightDefId_ = value;
    }

    [DebuggerNonUserCode]
    public LaunchMatchmakingResultEvent.Types.Result Result
    {
      get => this.result_;
      set => this.result_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as LaunchMatchmakingResultEvent);

    [DebuggerNonUserCode]
    public bool Equals(LaunchMatchmakingResultEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.FightDefId == other.FightDefId && this.Result == other.Result && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.FightDefId != 0)
        hashCode ^= this.FightDefId.GetHashCode();
      if (this.Result != LaunchMatchmakingResultEvent.Types.Result.Ok)
        hashCode ^= this.Result.GetHashCode();
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
      if (this.Result != LaunchMatchmakingResultEvent.Types.Result.Ok)
      {
        output.WriteRawTag((byte) 16);
        output.WriteEnum((int) this.Result);
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
      if (this.Result != LaunchMatchmakingResultEvent.Types.Result.Ok)
        size += 1 + CodedOutputStream.ComputeEnumSize((int) this.Result);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(LaunchMatchmakingResultEvent other)
    {
      if (other == null)
        return;
      if (other.FightDefId != 0)
        this.FightDefId = other.FightDefId;
      if (other.Result != LaunchMatchmakingResultEvent.Types.Result.Ok)
        this.Result = other.Result;
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
            this.FightDefId = input.ReadInt32();
            continue;
          case 16:
            this.result_ = (LaunchMatchmakingResultEvent.Types.Result) input.ReadEnum();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (LaunchMatchmakingResultEvent);

    [DebuggerNonUserCode]
    public static class Types
    {
      public enum Result
      {
        [OriginalName("OK")] Ok,
        [OriginalName("INTERNAL_ERROR")] InternalError,
        [OriginalName("VALID_DECK_NOT_FOUND")] ValidDeckNotFound,
        [OriginalName("ONLY_OWNER_CAN_LAUNCH")] OnlyOwnerCanLaunch,
        [OriginalName("GROUP_NOT_CREATED")] GroupNotCreated,
        [OriginalName("SOME_PLAYER_NOT_READY")] SomePlayerNotReady,
        [OriginalName("TOO_MANY_PLAYERS_FOR_FIGHT_DEFINITION")] TooManyPlayersForFightDefinition,
        [OriginalName("PLAYER_LEFT")] PlayerLeft,
      }
    }
  }
}
