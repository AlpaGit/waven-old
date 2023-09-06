// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightCommonProtocol.FightInfo
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
  public sealed class FightInfo : 
    IMessage<FightInfo>,
    IMessage,
    IEquatable<FightInfo>,
    IDeepCloneable<FightInfo>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightInfo> _parser = new MessageParser<FightInfo>((Func<FightInfo>) (() => new FightInfo()));
    private UnknownFieldSet _unknownFields;
    public const int FightDefIdFieldNumber = 1;
    private int fightDefId_;
    public const int FightMapIdFieldNumber = 2;
    private int fightMapId_;
    public const int FightTypeFieldNumber = 3;
    private int fightType_;
    public const int ConcurrentFightsCountFieldNumber = 4;
    private int concurrentFightsCount_;
    public const int OwnFightIdFieldNumber = 5;
    private int ownFightId_;
    public const int OwnTeamIndexFieldNumber = 6;
    private int ownTeamIndex_;
    public const int TeamsFieldNumber = 7;
    private static readonly FieldCodec<FightInfo.Types.Team> _repeated_teams_codec = FieldCodec.ForMessage<FightInfo.Types.Team>(58U, FightInfo.Types.Team.Parser);
    private readonly RepeatedField<FightInfo.Types.Team> teams_ = new RepeatedField<FightInfo.Types.Team>();

    [DebuggerNonUserCode]
    public static MessageParser<FightInfo> Parser => FightInfo._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightCommonProtocolReflection.Descriptor.MessageTypes[2];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightInfo.Descriptor;

    [DebuggerNonUserCode]
    public FightInfo()
    {
    }

    [DebuggerNonUserCode]
    public FightInfo(FightInfo other)
      : this()
    {
      this.fightDefId_ = other.fightDefId_;
      this.fightMapId_ = other.fightMapId_;
      this.fightType_ = other.fightType_;
      this.concurrentFightsCount_ = other.concurrentFightsCount_;
      this.ownFightId_ = other.ownFightId_;
      this.ownTeamIndex_ = other.ownTeamIndex_;
      this.teams_ = other.teams_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightInfo Clone() => new FightInfo(this);

    [DebuggerNonUserCode]
    public int FightDefId
    {
      get => this.fightDefId_;
      set => this.fightDefId_ = value;
    }

    [DebuggerNonUserCode]
    public int FightMapId
    {
      get => this.fightMapId_;
      set => this.fightMapId_ = value;
    }

    [DebuggerNonUserCode]
    public int FightType
    {
      get => this.fightType_;
      set => this.fightType_ = value;
    }

    [DebuggerNonUserCode]
    public int ConcurrentFightsCount
    {
      get => this.concurrentFightsCount_;
      set => this.concurrentFightsCount_ = value;
    }

    [DebuggerNonUserCode]
    public int OwnFightId
    {
      get => this.ownFightId_;
      set => this.ownFightId_ = value;
    }

    [DebuggerNonUserCode]
    public int OwnTeamIndex
    {
      get => this.ownTeamIndex_;
      set => this.ownTeamIndex_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<FightInfo.Types.Team> Teams => this.teams_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightInfo);

    [DebuggerNonUserCode]
    public bool Equals(FightInfo other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.FightDefId == other.FightDefId && this.FightMapId == other.FightMapId && this.FightType == other.FightType && this.ConcurrentFightsCount == other.ConcurrentFightsCount && this.OwnFightId == other.OwnFightId && this.OwnTeamIndex == other.OwnTeamIndex && this.teams_.Equals(other.teams_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num1 = 1;
      int num2;
      if (this.FightDefId != 0)
      {
        int num3 = num1;
        num2 = this.FightDefId;
        int hashCode = num2.GetHashCode();
        num1 = num3 ^ hashCode;
      }
      if (this.FightMapId != 0)
      {
        int num4 = num1;
        num2 = this.FightMapId;
        int hashCode = num2.GetHashCode();
        num1 = num4 ^ hashCode;
      }
      if (this.FightType != 0)
      {
        int num5 = num1;
        num2 = this.FightType;
        int hashCode = num2.GetHashCode();
        num1 = num5 ^ hashCode;
      }
      if (this.ConcurrentFightsCount != 0)
      {
        int num6 = num1;
        num2 = this.ConcurrentFightsCount;
        int hashCode = num2.GetHashCode();
        num1 = num6 ^ hashCode;
      }
      if (this.OwnFightId != 0)
      {
        int num7 = num1;
        num2 = this.OwnFightId;
        int hashCode = num2.GetHashCode();
        num1 = num7 ^ hashCode;
      }
      if (this.OwnTeamIndex != 0)
      {
        int num8 = num1;
        num2 = this.OwnTeamIndex;
        int hashCode = num2.GetHashCode();
        num1 = num8 ^ hashCode;
      }
      int hashCode1 = num1 ^ this.teams_.GetHashCode();
      if (this._unknownFields != null)
        hashCode1 ^= this._unknownFields.GetHashCode();
      return hashCode1;
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
      if (this.FightMapId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.FightMapId);
      }
      if (this.FightType != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.FightType);
      }
      if (this.ConcurrentFightsCount != 0)
      {
        output.WriteRawTag((byte) 32);
        output.WriteInt32(this.ConcurrentFightsCount);
      }
      if (this.OwnFightId != 0)
      {
        output.WriteRawTag((byte) 40);
        output.WriteInt32(this.OwnFightId);
      }
      if (this.OwnTeamIndex != 0)
      {
        output.WriteRawTag((byte) 48);
        output.WriteInt32(this.OwnTeamIndex);
      }
      this.teams_.WriteTo(output, FightInfo._repeated_teams_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0;
      if (this.FightDefId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.FightDefId);
      if (this.FightMapId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.FightMapId);
      if (this.FightType != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.FightType);
      if (this.ConcurrentFightsCount != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.ConcurrentFightsCount);
      if (this.OwnFightId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.OwnFightId);
      if (this.OwnTeamIndex != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.OwnTeamIndex);
      int size = num + this.teams_.CalculateSize(FightInfo._repeated_teams_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightInfo other)
    {
      if (other == null)
        return;
      if (other.FightDefId != 0)
        this.FightDefId = other.FightDefId;
      if (other.FightMapId != 0)
        this.FightMapId = other.FightMapId;
      if (other.FightType != 0)
        this.FightType = other.FightType;
      if (other.ConcurrentFightsCount != 0)
        this.ConcurrentFightsCount = other.ConcurrentFightsCount;
      if (other.OwnFightId != 0)
        this.OwnFightId = other.OwnFightId;
      if (other.OwnTeamIndex != 0)
        this.OwnTeamIndex = other.OwnTeamIndex;
      this.teams_.Add((IEnumerable<FightInfo.Types.Team>) other.teams_);
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
            this.FightMapId = input.ReadInt32();
            continue;
          case 24:
            this.FightType = input.ReadInt32();
            continue;
          case 32:
            this.ConcurrentFightsCount = input.ReadInt32();
            continue;
          case 40:
            this.OwnFightId = input.ReadInt32();
            continue;
          case 48:
            this.OwnTeamIndex = input.ReadInt32();
            continue;
          case 58:
            this.teams_.AddEntriesFrom(input, FightInfo._repeated_teams_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightInfo);

    [DebuggerNonUserCode]
    public static class Types
    {
      public sealed class Team : 
        IMessage<FightInfo.Types.Team>,
        IMessage,
        IEquatable<FightInfo.Types.Team>,
        IDeepCloneable<FightInfo.Types.Team>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<FightInfo.Types.Team> _parser = new MessageParser<FightInfo.Types.Team>((Func<FightInfo.Types.Team>) (() => new FightInfo.Types.Team()));
        private UnknownFieldSet _unknownFields;
        public const int PlayersFieldNumber = 1;
        private static readonly FieldCodec<FightInfo.Types.Player> _repeated_players_codec = FieldCodec.ForMessage<FightInfo.Types.Player>(10U, FightInfo.Types.Player.Parser);
        private readonly RepeatedField<FightInfo.Types.Player> players_ = new RepeatedField<FightInfo.Types.Player>();

        [DebuggerNonUserCode]
        public static MessageParser<FightInfo.Types.Team> Parser => FightInfo.Types.Team._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => FightInfo.Descriptor.NestedTypes[0];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightInfo.Types.Team.Descriptor;

        [DebuggerNonUserCode]
        public Team()
        {
        }

        [DebuggerNonUserCode]
        public Team(FightInfo.Types.Team other)
          : this()
        {
          this.players_ = other.players_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public FightInfo.Types.Team Clone() => new FightInfo.Types.Team(this);

        [DebuggerNonUserCode]
        public RepeatedField<FightInfo.Types.Player> Players => this.players_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as FightInfo.Types.Team);

        [DebuggerNonUserCode]
        public bool Equals(FightInfo.Types.Team other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.players_.Equals(other.players_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1 ^ this.players_.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          this.players_.WriteTo(output, FightInfo.Types.Team._repeated_players_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0 + this.players_.CalculateSize(FightInfo.Types.Team._repeated_players_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(FightInfo.Types.Team other)
        {
          if (other == null)
            return;
          this.players_.Add((IEnumerable<FightInfo.Types.Player>) other.players_);
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
              this.players_.AddEntriesFrom(input, FightInfo.Types.Team._repeated_players_codec);
          }
        }

        public string ToDiagnosticString() => nameof (Team);
      }

      public sealed class Player : 
        IMessage<FightInfo.Types.Player>,
        IMessage,
        IEquatable<FightInfo.Types.Player>,
        IDeepCloneable<FightInfo.Types.Player>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<FightInfo.Types.Player> _parser = new MessageParser<FightInfo.Types.Player>((Func<FightInfo.Types.Player>) (() => new FightInfo.Types.Player()));
        private UnknownFieldSet _unknownFields;
        public const int NameFieldNumber = 1;
        private string name_ = "";
        public const int LevelFieldNumber = 2;
        private int level_;
        public const int WeaponIdFieldNumber = 3;
        private static readonly FieldCodec<int?> _single_weaponId_codec = FieldCodec.ForStructWrapper<int>(26U);
        private int? weaponId_;

        [DebuggerNonUserCode]
        public static MessageParser<FightInfo.Types.Player> Parser => FightInfo.Types.Player._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => FightInfo.Descriptor.NestedTypes[1];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightInfo.Types.Player.Descriptor;

        [DebuggerNonUserCode]
        public Player()
        {
        }

        [DebuggerNonUserCode]
        public Player(FightInfo.Types.Player other)
          : this()
        {
          this.name_ = other.name_;
          this.level_ = other.level_;
          this.WeaponId = other.WeaponId;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public FightInfo.Types.Player Clone() => new FightInfo.Types.Player(this);

        [DebuggerNonUserCode]
        public string Name
        {
          get => this.name_;
          set => this.name_ = ProtoPreconditions.CheckNotNull<string>(value, nameof (value));
        }

        [DebuggerNonUserCode]
        public int Level
        {
          get => this.level_;
          set => this.level_ = value;
        }

        [DebuggerNonUserCode]
        public int? WeaponId
        {
          get => this.weaponId_;
          set => this.weaponId_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as FightInfo.Types.Player);

        [DebuggerNonUserCode]
        public bool Equals(FightInfo.Types.Player other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          if (this.Name != other.Name || this.Level != other.Level)
            return false;
          int? weaponId1 = this.WeaponId;
          int? weaponId2 = other.WeaponId;
          return weaponId1.GetValueOrDefault() == weaponId2.GetValueOrDefault() & weaponId1.HasValue == weaponId2.HasValue && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.Name.Length != 0)
            hashCode ^= this.Name.GetHashCode();
          if (this.Level != 0)
            hashCode ^= this.Level.GetHashCode();
          if (this.weaponId_.HasValue)
            hashCode ^= this.WeaponId.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.Name.Length != 0)
          {
            output.WriteRawTag((byte) 10);
            output.WriteString(this.Name);
          }
          if (this.Level != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Level);
          }
          if (this.weaponId_.HasValue)
            FightInfo.Types.Player._single_weaponId_codec.WriteTagAndValue(output, this.WeaponId);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.Name.Length != 0)
            size += 1 + CodedOutputStream.ComputeStringSize(this.Name);
          if (this.Level != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Level);
          if (this.weaponId_.HasValue)
            size += FightInfo.Types.Player._single_weaponId_codec.CalculateSizeWithTag(this.WeaponId);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(FightInfo.Types.Player other)
        {
          if (other == null)
            return;
          if (other.Name.Length != 0)
            this.Name = other.Name;
          if (other.Level != 0)
            this.Level = other.Level;
          if (other.weaponId_.HasValue)
          {
            if (this.weaponId_.HasValue)
            {
              int? weaponId = other.WeaponId;
              int num = 0;
              if (weaponId.GetValueOrDefault() == num & weaponId.HasValue)
                goto label_9;
            }
            this.WeaponId = other.WeaponId;
          }
label_9:
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num1;
          while ((num1 = input.ReadTag()) != 0U)
          {
            switch (num1)
            {
              case 10:
                this.Name = input.ReadString();
                continue;
              case 16:
                this.Level = input.ReadInt32();
                continue;
              case 26:
                int? nullable1 = FightInfo.Types.Player._single_weaponId_codec.Read(input);
                if (this.weaponId_.HasValue)
                {
                  int? nullable2 = nullable1;
                  int num2 = 0;
                  if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                    continue;
                }
                this.WeaponId = nullable1;
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (Player);
      }
    }
  }
}
