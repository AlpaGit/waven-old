// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightCommonProtocol.GameStatistics
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightCommonProtocol
{
  public sealed class GameStatistics : 
    IMessage<GameStatistics>,
    IMessage,
    IEquatable<GameStatistics>,
    IDeepCloneable<GameStatistics>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<GameStatistics> _parser = new MessageParser<GameStatistics>((Func<GameStatistics>) (() => new GameStatistics()));
    private UnknownFieldSet _unknownFields;
    public const int PlayerStatsFieldNumber = 1;
    private static readonly FieldCodec<GameStatistics.Types.PlayerStats> _repeated_playerStats_codec = FieldCodec.ForMessage<GameStatistics.Types.PlayerStats>(10U, GameStatistics.Types.PlayerStats.Parser);
    private readonly RepeatedField<GameStatistics.Types.PlayerStats> playerStats_ = new RepeatedField<GameStatistics.Types.PlayerStats>();

    [DebuggerNonUserCode]
    public static MessageParser<GameStatistics> Parser => GameStatistics._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightCommonProtocolReflection.Descriptor.MessageTypes[3];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => GameStatistics.Descriptor;

    [DebuggerNonUserCode]
    public GameStatistics()
    {
    }

    [DebuggerNonUserCode]
    public GameStatistics(GameStatistics other)
      : this()
    {
      this.playerStats_ = other.playerStats_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public GameStatistics Clone() => new GameStatistics(this);

    [DebuggerNonUserCode]
    public RepeatedField<GameStatistics.Types.PlayerStats> PlayerStats => this.playerStats_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as GameStatistics);

    [DebuggerNonUserCode]
    public bool Equals(GameStatistics other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.playerStats_.Equals(other.playerStats_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1 ^ this.playerStats_.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      this.playerStats_.WriteTo(output, GameStatistics._repeated_playerStats_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0 + this.playerStats_.CalculateSize(GameStatistics._repeated_playerStats_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(GameStatistics other)
    {
      if (other == null)
        return;
      this.playerStats_.Add((IEnumerable<GameStatistics.Types.PlayerStats>) other.playerStats_);
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
        else
          this.playerStats_.AddEntriesFrom(input, GameStatistics._repeated_playerStats_codec);
      }
    }

    public string ToDiagnosticString() => nameof (GameStatistics);

    [DebuggerNonUserCode]
    public static class Types
    {
      public sealed class PlayerStats : 
        IMessage<GameStatistics.Types.PlayerStats>,
        IMessage,
        IEquatable<GameStatistics.Types.PlayerStats>,
        IDeepCloneable<GameStatistics.Types.PlayerStats>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<GameStatistics.Types.PlayerStats> _parser = new MessageParser<GameStatistics.Types.PlayerStats>((Func<GameStatistics.Types.PlayerStats>) (() => new GameStatistics.Types.PlayerStats()));
        private UnknownFieldSet _unknownFields;
        public const int PlayerIdFieldNumber = 1;
        private int playerId_;
        public const int FightIdFieldNumber = 2;
        private int fightId_;
        public const int StatsFieldNumber = 3;
        private static readonly MapField<int, int>.Codec _map_stats_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8U), FieldCodec.ForInt32(16U), 26U);
        private readonly MapField<int, int> stats_ = new MapField<int, int>();
        public const int TitlesFieldNumber = 4;
        private static readonly FieldCodec<int> _repeated_titles_codec = FieldCodec.ForInt32(34U);
        private readonly RepeatedField<int> titles_ = new RepeatedField<int>();

        [DebuggerNonUserCode]
        public static MessageParser<GameStatistics.Types.PlayerStats> Parser => GameStatistics.Types.PlayerStats._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => GameStatistics.Descriptor.NestedTypes[0];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => GameStatistics.Types.PlayerStats.Descriptor;

        [DebuggerNonUserCode]
        public PlayerStats()
        {
        }

        [DebuggerNonUserCode]
        public PlayerStats(GameStatistics.Types.PlayerStats other)
          : this()
        {
          this.playerId_ = other.playerId_;
          this.fightId_ = other.fightId_;
          this.stats_ = other.stats_.Clone();
          this.titles_ = other.titles_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public GameStatistics.Types.PlayerStats Clone() => new GameStatistics.Types.PlayerStats(this);

        [DebuggerNonUserCode]
        public int PlayerId
        {
          get => this.playerId_;
          set => this.playerId_ = value;
        }

        [DebuggerNonUserCode]
        public int FightId
        {
          get => this.fightId_;
          set => this.fightId_ = value;
        }

        [DebuggerNonUserCode]
        public MapField<int, int> Stats => this.stats_;

        [DebuggerNonUserCode]
        public RepeatedField<int> Titles => this.titles_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as GameStatistics.Types.PlayerStats);

        [DebuggerNonUserCode]
        public bool Equals(GameStatistics.Types.PlayerStats other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.PlayerId == other.PlayerId && this.FightId == other.FightId && this.Stats.Equals(other.Stats) && this.titles_.Equals(other.titles_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int num1 = 1;
          int num2;
          if (this.PlayerId != 0)
          {
            int num3 = num1;
            num2 = this.PlayerId;
            int hashCode = num2.GetHashCode();
            num1 = num3 ^ hashCode;
          }
          if (this.FightId != 0)
          {
            int num4 = num1;
            num2 = this.FightId;
            int hashCode = num2.GetHashCode();
            num1 = num4 ^ hashCode;
          }
          int hashCode1 = num1 ^ this.Stats.GetHashCode() ^ this.titles_.GetHashCode();
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.PlayerId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.PlayerId);
          }
          if (this.FightId != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.FightId);
          }
          this.stats_.WriteTo(output, GameStatistics.Types.PlayerStats._map_stats_codec);
          this.titles_.WriteTo(output, GameStatistics.Types.PlayerStats._repeated_titles_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int num = 0;
          if (this.PlayerId != 0)
            num += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerId);
          if (this.FightId != 0)
            num += 1 + CodedOutputStream.ComputeInt32Size(this.FightId);
          int size = num + this.stats_.CalculateSize(GameStatistics.Types.PlayerStats._map_stats_codec) + this.titles_.CalculateSize(GameStatistics.Types.PlayerStats._repeated_titles_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(GameStatistics.Types.PlayerStats other)
        {
          if (other == null)
            return;
          if (other.PlayerId != 0)
            this.PlayerId = other.PlayerId;
          if (other.FightId != 0)
            this.FightId = other.FightId;
          this.stats_.Add((IDictionary<int, int>) other.stats_);
          this.titles_.Add((IEnumerable<int>) other.titles_);
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
                this.PlayerId = input.ReadInt32();
                continue;
              case 16:
                this.FightId = input.ReadInt32();
                continue;
              case 26:
                this.stats_.AddEntriesFrom(input, GameStatistics.Types.PlayerStats._map_stats_codec);
                continue;
              case 32:
              case 34:
                this.titles_.AddEntriesFrom(input, GameStatistics.Types.PlayerStats._repeated_titles_codec);
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (PlayerStats);
      }
    }
  }
}
