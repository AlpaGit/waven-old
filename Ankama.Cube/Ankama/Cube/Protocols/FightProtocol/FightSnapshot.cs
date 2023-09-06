// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.FightSnapshot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class FightSnapshot : 
    IMessage<FightSnapshot>,
    IMessage,
    IEquatable<FightSnapshot>,
    IDeepCloneable<FightSnapshot>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightSnapshot> _parser = new MessageParser<FightSnapshot>((Func<FightSnapshot>) (() => new FightSnapshot()));
    private UnknownFieldSet _unknownFields;
    public const int FightIdFieldNumber = 1;
    private int fightId_;
    public const int EntitiesFieldNumber = 2;
    private static readonly FieldCodec<FightSnapshot.Types.EntitySnapshot> _repeated_entities_codec = FieldCodec.ForMessage<FightSnapshot.Types.EntitySnapshot>(18U, FightSnapshot.Types.EntitySnapshot.Parser);
    private readonly RepeatedField<FightSnapshot.Types.EntitySnapshot> entities_ = new RepeatedField<FightSnapshot.Types.EntitySnapshot>();
    public const int TurnIndexFieldNumber = 3;
    private int turnIndex_;
    public const int TurnRemainingTimeSecFieldNumber = 4;
    private int turnRemainingTimeSec_;
    public const int PlayersCompanionsFieldNumber = 5;
    private static readonly MapField<int, FightSnapshot.Types.Companions>.Codec _map_playersCompanions_codec = new MapField<int, FightSnapshot.Types.Companions>.Codec(FieldCodec.ForInt32(8U), FieldCodec.ForMessage<FightSnapshot.Types.Companions>(18U, FightSnapshot.Types.Companions.Parser), 42U);
    private readonly MapField<int, FightSnapshot.Types.Companions> playersCompanions_ = new MapField<int, FightSnapshot.Types.Companions>();
    public const int PlayersCardsCountFieldNumber = 6;
    private static readonly MapField<int, int>.Codec _map_playersCardsCount_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8U), FieldCodec.ForInt32(16U), 50U);
    private readonly MapField<int, int> playersCardsCount_ = new MapField<int, int>();

    [DebuggerNonUserCode]
    public static MessageParser<FightSnapshot> Parser => FightSnapshot._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[9];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightSnapshot.Descriptor;

    [DebuggerNonUserCode]
    public FightSnapshot()
    {
    }

    [DebuggerNonUserCode]
    public FightSnapshot(FightSnapshot other)
      : this()
    {
      this.fightId_ = other.fightId_;
      this.entities_ = other.entities_.Clone();
      this.turnIndex_ = other.turnIndex_;
      this.turnRemainingTimeSec_ = other.turnRemainingTimeSec_;
      this.playersCompanions_ = other.playersCompanions_.Clone();
      this.playersCardsCount_ = other.playersCardsCount_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightSnapshot Clone() => new FightSnapshot(this);

    [DebuggerNonUserCode]
    public int FightId
    {
      get => this.fightId_;
      set => this.fightId_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<FightSnapshot.Types.EntitySnapshot> Entities => this.entities_;

    [DebuggerNonUserCode]
    public int TurnIndex
    {
      get => this.turnIndex_;
      set => this.turnIndex_ = value;
    }

    [DebuggerNonUserCode]
    public int TurnRemainingTimeSec
    {
      get => this.turnRemainingTimeSec_;
      set => this.turnRemainingTimeSec_ = value;
    }

    [DebuggerNonUserCode]
    public MapField<int, FightSnapshot.Types.Companions> PlayersCompanions => this.playersCompanions_;

    [DebuggerNonUserCode]
    public MapField<int, int> PlayersCardsCount => this.playersCardsCount_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightSnapshot);

    [DebuggerNonUserCode]
    public bool Equals(FightSnapshot other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.FightId == other.FightId && this.entities_.Equals(other.entities_) && this.TurnIndex == other.TurnIndex && this.TurnRemainingTimeSec == other.TurnRemainingTimeSec && this.PlayersCompanions.Equals(other.PlayersCompanions) && this.PlayersCardsCount.Equals(other.PlayersCardsCount) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num1 = 1;
      int num2;
      if (this.FightId != 0)
      {
        int num3 = num1;
        num2 = this.FightId;
        int hashCode = num2.GetHashCode();
        num1 = num3 ^ hashCode;
      }
      int num4 = num1 ^ this.entities_.GetHashCode();
      if (this.TurnIndex != 0)
      {
        int num5 = num4;
        num2 = this.TurnIndex;
        int hashCode = num2.GetHashCode();
        num4 = num5 ^ hashCode;
      }
      if (this.TurnRemainingTimeSec != 0)
      {
        int num6 = num4;
        num2 = this.TurnRemainingTimeSec;
        int hashCode = num2.GetHashCode();
        num4 = num6 ^ hashCode;
      }
      int hashCode1 = num4 ^ this.PlayersCompanions.GetHashCode() ^ this.PlayersCardsCount.GetHashCode();
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
      this.entities_.WriteTo(output, FightSnapshot._repeated_entities_codec);
      if (this.TurnIndex != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.TurnIndex);
      }
      if (this.TurnRemainingTimeSec != 0)
      {
        output.WriteRawTag((byte) 32);
        output.WriteInt32(this.TurnRemainingTimeSec);
      }
      this.playersCompanions_.WriteTo(output, FightSnapshot._map_playersCompanions_codec);
      this.playersCardsCount_.WriteTo(output, FightSnapshot._map_playersCardsCount_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num1 = 0;
      if (this.FightId != 0)
        num1 += 1 + CodedOutputStream.ComputeInt32Size(this.FightId);
      int num2 = num1 + this.entities_.CalculateSize(FightSnapshot._repeated_entities_codec);
      if (this.TurnIndex != 0)
        num2 += 1 + CodedOutputStream.ComputeInt32Size(this.TurnIndex);
      if (this.TurnRemainingTimeSec != 0)
        num2 += 1 + CodedOutputStream.ComputeInt32Size(this.TurnRemainingTimeSec);
      int size = num2 + this.playersCompanions_.CalculateSize(FightSnapshot._map_playersCompanions_codec) + this.playersCardsCount_.CalculateSize(FightSnapshot._map_playersCardsCount_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightSnapshot other)
    {
      if (other == null)
        return;
      if (other.FightId != 0)
        this.FightId = other.FightId;
      this.entities_.Add((IEnumerable<FightSnapshot.Types.EntitySnapshot>) other.entities_);
      if (other.TurnIndex != 0)
        this.TurnIndex = other.TurnIndex;
      if (other.TurnRemainingTimeSec != 0)
        this.TurnRemainingTimeSec = other.TurnRemainingTimeSec;
      this.playersCompanions_.Add((IDictionary<int, FightSnapshot.Types.Companions>) other.playersCompanions_);
      this.playersCardsCount_.Add((IDictionary<int, int>) other.playersCardsCount_);
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
          case 18:
            this.entities_.AddEntriesFrom(input, FightSnapshot._repeated_entities_codec);
            continue;
          case 24:
            this.TurnIndex = input.ReadInt32();
            continue;
          case 32:
            this.TurnRemainingTimeSec = input.ReadInt32();
            continue;
          case 42:
            this.playersCompanions_.AddEntriesFrom(input, FightSnapshot._map_playersCompanions_codec);
            continue;
          case 50:
            this.playersCardsCount_.AddEntriesFrom(input, FightSnapshot._map_playersCardsCount_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightSnapshot);

    [DebuggerNonUserCode]
    public static class Types
    {
      public sealed class EntitySnapshot : 
        IMessage<FightSnapshot.Types.EntitySnapshot>,
        IMessage,
        IEquatable<FightSnapshot.Types.EntitySnapshot>,
        IDeepCloneable<FightSnapshot.Types.EntitySnapshot>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<FightSnapshot.Types.EntitySnapshot> _parser = new MessageParser<FightSnapshot.Types.EntitySnapshot>((Func<FightSnapshot.Types.EntitySnapshot>) (() => new FightSnapshot.Types.EntitySnapshot()));
        private UnknownFieldSet _unknownFields;
        public const int EntityIdFieldNumber = 1;
        private int entityId_;
        public const int EntityTypeFieldNumber = 2;
        private int entityType_;
        public const int NameFieldNumber = 3;
        private static readonly FieldCodec<string> _single_name_codec = FieldCodec.ForClassWrapper<string>(26U);
        private string name_;
        public const int DefIdFieldNumber = 4;
        private static readonly FieldCodec<int?> _single_defId_codec = FieldCodec.ForStructWrapper<int>(34U);
        private int? defId_;
        public const int WeaponIdFieldNumber = 5;
        private static readonly FieldCodec<int?> _single_weaponId_codec = FieldCodec.ForStructWrapper<int>(42U);
        private int? weaponId_;
        public const int GenderIdFieldNumber = 6;
        private static readonly FieldCodec<int?> _single_genderId_codec = FieldCodec.ForStructWrapper<int>(50U);
        private int? genderId_;
        public const int PlayerIndexInFightFieldNumber = 7;
        private static readonly FieldCodec<int?> _single_playerIndexInFight_codec = FieldCodec.ForStructWrapper<int>(58U);
        private int? playerIndexInFight_;
        public const int OwnerIdFieldNumber = 8;
        private static readonly FieldCodec<int?> _single_ownerId_codec = FieldCodec.ForStructWrapper<int>(66U);
        private int? ownerId_;
        public const int TeamIdFieldNumber = 9;
        private static readonly FieldCodec<int?> _single_teamId_codec = FieldCodec.ForStructWrapper<int>(74U);
        private int? teamId_;
        public const int LevelFieldNumber = 10;
        private static readonly FieldCodec<int?> _single_level_codec = FieldCodec.ForStructWrapper<int>(82U);
        private int? level_;
        public const int PropertiesFieldNumber = 11;
        private static readonly FieldCodec<int> _repeated_properties_codec = FieldCodec.ForInt32(90U);
        private readonly RepeatedField<int> properties_ = new RepeatedField<int>();
        public const int PositionFieldNumber = 12;
        private CellCoord position_;
        public const int DirectionFieldNumber = 13;
        private static readonly FieldCodec<int?> _single_direction_codec = FieldCodec.ForStructWrapper<int>(106U);
        private int? direction_;
        public const int CaracsFieldNumber = 14;
        private static readonly MapField<int, int>.Codec _map_caracs_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8U), FieldCodec.ForInt32(16U), 114U);
        private readonly MapField<int, int> caracs_ = new MapField<int, int>();
        public const int CustomSkinFieldNumber = 15;
        private static readonly FieldCodec<string> _single_customSkin_codec = FieldCodec.ForClassWrapper<string>(122U);
        private string customSkin_;
        public const int ActionDoneThisTurnFieldNumber = 16;
        private static readonly FieldCodec<bool?> _single_actionDoneThisTurn_codec = FieldCodec.ForStructWrapper<bool>(130U);
        private bool? actionDoneThisTurn_;

        [DebuggerNonUserCode]
        public static MessageParser<FightSnapshot.Types.EntitySnapshot> Parser => FightSnapshot.Types.EntitySnapshot._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => FightSnapshot.Descriptor.NestedTypes[2];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightSnapshot.Types.EntitySnapshot.Descriptor;

        [DebuggerNonUserCode]
        public EntitySnapshot()
        {
        }

        [DebuggerNonUserCode]
        public EntitySnapshot(FightSnapshot.Types.EntitySnapshot other)
          : this()
        {
          this.entityId_ = other.entityId_;
          this.entityType_ = other.entityType_;
          this.Name = other.Name;
          this.DefId = other.DefId;
          this.WeaponId = other.WeaponId;
          this.GenderId = other.GenderId;
          this.PlayerIndexInFight = other.PlayerIndexInFight;
          this.OwnerId = other.OwnerId;
          this.TeamId = other.TeamId;
          this.Level = other.Level;
          this.properties_ = other.properties_.Clone();
          this.position_ = other.position_ != null ? other.position_.Clone() : (CellCoord) null;
          this.Direction = other.Direction;
          this.caracs_ = other.caracs_.Clone();
          this.CustomSkin = other.CustomSkin;
          this.ActionDoneThisTurn = other.ActionDoneThisTurn;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public FightSnapshot.Types.EntitySnapshot Clone() => new FightSnapshot.Types.EntitySnapshot(this);

        [DebuggerNonUserCode]
        public int EntityId
        {
          get => this.entityId_;
          set => this.entityId_ = value;
        }

        [DebuggerNonUserCode]
        public int EntityType
        {
          get => this.entityType_;
          set => this.entityType_ = value;
        }

        [DebuggerNonUserCode]
        public string Name
        {
          get => this.name_;
          set => this.name_ = value;
        }

        [DebuggerNonUserCode]
        public int? DefId
        {
          get => this.defId_;
          set => this.defId_ = value;
        }

        [DebuggerNonUserCode]
        public int? WeaponId
        {
          get => this.weaponId_;
          set => this.weaponId_ = value;
        }

        [DebuggerNonUserCode]
        public int? GenderId
        {
          get => this.genderId_;
          set => this.genderId_ = value;
        }

        [DebuggerNonUserCode]
        public int? PlayerIndexInFight
        {
          get => this.playerIndexInFight_;
          set => this.playerIndexInFight_ = value;
        }

        [DebuggerNonUserCode]
        public int? OwnerId
        {
          get => this.ownerId_;
          set => this.ownerId_ = value;
        }

        [DebuggerNonUserCode]
        public int? TeamId
        {
          get => this.teamId_;
          set => this.teamId_ = value;
        }

        [DebuggerNonUserCode]
        public int? Level
        {
          get => this.level_;
          set => this.level_ = value;
        }

        [DebuggerNonUserCode]
        public RepeatedField<int> Properties => this.properties_;

        [DebuggerNonUserCode]
        public CellCoord Position
        {
          get => this.position_;
          set => this.position_ = value;
        }

        [DebuggerNonUserCode]
        public int? Direction
        {
          get => this.direction_;
          set => this.direction_ = value;
        }

        [DebuggerNonUserCode]
        public MapField<int, int> Caracs => this.caracs_;

        [DebuggerNonUserCode]
        public string CustomSkin
        {
          get => this.customSkin_;
          set => this.customSkin_ = value;
        }

        [DebuggerNonUserCode]
        public bool? ActionDoneThisTurn
        {
          get => this.actionDoneThisTurn_;
          set => this.actionDoneThisTurn_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as FightSnapshot.Types.EntitySnapshot);

        [DebuggerNonUserCode]
        public bool Equals(FightSnapshot.Types.EntitySnapshot other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          if (this.EntityId != other.EntityId || this.EntityType != other.EntityType || this.Name != other.Name)
            return false;
          int? nullable1 = this.DefId;
          int? nullable2 = other.DefId;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            return false;
          nullable2 = this.WeaponId;
          nullable1 = other.WeaponId;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            return false;
          nullable1 = this.GenderId;
          nullable2 = other.GenderId;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            return false;
          nullable2 = this.PlayerIndexInFight;
          nullable1 = other.PlayerIndexInFight;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            return false;
          nullable1 = this.OwnerId;
          nullable2 = other.OwnerId;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            return false;
          nullable2 = this.TeamId;
          nullable1 = other.TeamId;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            return false;
          nullable1 = this.Level;
          nullable2 = other.Level;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) || !this.properties_.Equals(other.properties_) || !object.Equals((object) this.Position, (object) other.Position))
            return false;
          nullable2 = this.Direction;
          nullable1 = other.Direction;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) || !this.Caracs.Equals(other.Caracs) || this.CustomSkin != other.CustomSkin)
            return false;
          bool? actionDoneThisTurn1 = this.ActionDoneThisTurn;
          bool? actionDoneThisTurn2 = other.ActionDoneThisTurn;
          return actionDoneThisTurn1.GetValueOrDefault() == actionDoneThisTurn2.GetValueOrDefault() & actionDoneThisTurn1.HasValue == actionDoneThisTurn2.HasValue && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int num1 = 1;
          int num2;
          if (this.EntityId != 0)
          {
            int num3 = num1;
            num2 = this.EntityId;
            int hashCode = num2.GetHashCode();
            num1 = num3 ^ hashCode;
          }
          if (this.EntityType != 0)
          {
            int num4 = num1;
            num2 = this.EntityType;
            int hashCode = num2.GetHashCode();
            num1 = num4 ^ hashCode;
          }
          if (this.name_ != null)
            num1 ^= this.Name.GetHashCode();
          int? nullable;
          if (this.defId_.HasValue)
          {
            int num5 = num1;
            nullable = this.DefId;
            int hashCode = nullable.GetHashCode();
            num1 = num5 ^ hashCode;
          }
          if (this.weaponId_.HasValue)
          {
            int num6 = num1;
            nullable = this.WeaponId;
            int hashCode = nullable.GetHashCode();
            num1 = num6 ^ hashCode;
          }
          if (this.genderId_.HasValue)
          {
            int num7 = num1;
            nullable = this.GenderId;
            int hashCode = nullable.GetHashCode();
            num1 = num7 ^ hashCode;
          }
          if (this.playerIndexInFight_.HasValue)
          {
            int num8 = num1;
            nullable = this.PlayerIndexInFight;
            int hashCode = nullable.GetHashCode();
            num1 = num8 ^ hashCode;
          }
          if (this.ownerId_.HasValue)
          {
            int num9 = num1;
            nullable = this.OwnerId;
            int hashCode = nullable.GetHashCode();
            num1 = num9 ^ hashCode;
          }
          if (this.teamId_.HasValue)
          {
            int num10 = num1;
            nullable = this.TeamId;
            int hashCode = nullable.GetHashCode();
            num1 = num10 ^ hashCode;
          }
          if (this.level_.HasValue)
          {
            int num11 = num1;
            nullable = this.Level;
            int hashCode = nullable.GetHashCode();
            num1 = num11 ^ hashCode;
          }
          int num12 = num1 ^ this.properties_.GetHashCode();
          if (this.position_ != null)
            num12 ^= this.Position.GetHashCode();
          if (this.direction_.HasValue)
          {
            int num13 = num12;
            nullable = this.Direction;
            int hashCode = nullable.GetHashCode();
            num12 = num13 ^ hashCode;
          }
          int hashCode1 = num12 ^ this.Caracs.GetHashCode();
          if (this.customSkin_ != null)
            hashCode1 ^= this.CustomSkin.GetHashCode();
          if (this.actionDoneThisTurn_.HasValue)
            hashCode1 ^= this.ActionDoneThisTurn.GetHashCode();
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.EntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.EntityId);
          }
          if (this.EntityType != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.EntityType);
          }
          if (this.name_ != null)
            FightSnapshot.Types.EntitySnapshot._single_name_codec.WriteTagAndValue(output, this.Name);
          if (this.defId_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_defId_codec.WriteTagAndValue(output, this.DefId);
          if (this.weaponId_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_weaponId_codec.WriteTagAndValue(output, this.WeaponId);
          if (this.genderId_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_genderId_codec.WriteTagAndValue(output, this.GenderId);
          if (this.playerIndexInFight_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_playerIndexInFight_codec.WriteTagAndValue(output, this.PlayerIndexInFight);
          if (this.ownerId_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_ownerId_codec.WriteTagAndValue(output, this.OwnerId);
          if (this.teamId_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_teamId_codec.WriteTagAndValue(output, this.TeamId);
          if (this.level_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_level_codec.WriteTagAndValue(output, this.Level);
          this.properties_.WriteTo(output, FightSnapshot.Types.EntitySnapshot._repeated_properties_codec);
          if (this.position_ != null)
          {
            output.WriteRawTag((byte) 98);
            output.WriteMessage((IMessage) this.Position);
          }
          if (this.direction_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_direction_codec.WriteTagAndValue(output, this.Direction);
          this.caracs_.WriteTo(output, FightSnapshot.Types.EntitySnapshot._map_caracs_codec);
          if (this.customSkin_ != null)
            FightSnapshot.Types.EntitySnapshot._single_customSkin_codec.WriteTagAndValue(output, this.CustomSkin);
          if (this.actionDoneThisTurn_.HasValue)
            FightSnapshot.Types.EntitySnapshot._single_actionDoneThisTurn_codec.WriteTagAndValue(output, this.ActionDoneThisTurn);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int num1 = 0;
          if (this.EntityId != 0)
            num1 += 1 + CodedOutputStream.ComputeInt32Size(this.EntityId);
          if (this.EntityType != 0)
            num1 += 1 + CodedOutputStream.ComputeInt32Size(this.EntityType);
          if (this.name_ != null)
            num1 += FightSnapshot.Types.EntitySnapshot._single_name_codec.CalculateSizeWithTag(this.Name);
          if (this.defId_.HasValue)
            num1 += FightSnapshot.Types.EntitySnapshot._single_defId_codec.CalculateSizeWithTag(this.DefId);
          if (this.weaponId_.HasValue)
            num1 += FightSnapshot.Types.EntitySnapshot._single_weaponId_codec.CalculateSizeWithTag(this.WeaponId);
          if (this.genderId_.HasValue)
            num1 += FightSnapshot.Types.EntitySnapshot._single_genderId_codec.CalculateSizeWithTag(this.GenderId);
          if (this.playerIndexInFight_.HasValue)
            num1 += FightSnapshot.Types.EntitySnapshot._single_playerIndexInFight_codec.CalculateSizeWithTag(this.PlayerIndexInFight);
          if (this.ownerId_.HasValue)
            num1 += FightSnapshot.Types.EntitySnapshot._single_ownerId_codec.CalculateSizeWithTag(this.OwnerId);
          if (this.teamId_.HasValue)
            num1 += FightSnapshot.Types.EntitySnapshot._single_teamId_codec.CalculateSizeWithTag(this.TeamId);
          if (this.level_.HasValue)
            num1 += FightSnapshot.Types.EntitySnapshot._single_level_codec.CalculateSizeWithTag(this.Level);
          int num2 = num1 + this.properties_.CalculateSize(FightSnapshot.Types.EntitySnapshot._repeated_properties_codec);
          if (this.position_ != null)
            num2 += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Position);
          if (this.direction_.HasValue)
            num2 += FightSnapshot.Types.EntitySnapshot._single_direction_codec.CalculateSizeWithTag(this.Direction);
          int size = num2 + this.caracs_.CalculateSize(FightSnapshot.Types.EntitySnapshot._map_caracs_codec);
          if (this.customSkin_ != null)
            size += FightSnapshot.Types.EntitySnapshot._single_customSkin_codec.CalculateSizeWithTag(this.CustomSkin);
          if (this.actionDoneThisTurn_.HasValue)
            size += FightSnapshot.Types.EntitySnapshot._single_actionDoneThisTurn_codec.CalculateSizeWithTag(this.ActionDoneThisTurn);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(FightSnapshot.Types.EntitySnapshot other)
        {
          if (other == null)
            return;
          if (other.EntityId != 0)
            this.EntityId = other.EntityId;
          if (other.EntityType != 0)
            this.EntityType = other.EntityType;
          if (other.name_ != null && (this.name_ == null || other.Name != ""))
            this.Name = other.Name;
          int? nullable;
          if (other.defId_.HasValue)
          {
            if (this.defId_.HasValue)
            {
              nullable = other.DefId;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_11;
            }
            this.DefId = other.DefId;
          }
label_11:
          if (other.weaponId_.HasValue)
          {
            if (this.weaponId_.HasValue)
            {
              nullable = other.WeaponId;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_15;
            }
            this.WeaponId = other.WeaponId;
          }
label_15:
          if (other.genderId_.HasValue)
          {
            if (this.genderId_.HasValue)
            {
              nullable = other.GenderId;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_19;
            }
            this.GenderId = other.GenderId;
          }
label_19:
          if (other.playerIndexInFight_.HasValue)
          {
            if (this.playerIndexInFight_.HasValue)
            {
              nullable = other.PlayerIndexInFight;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_23;
            }
            this.PlayerIndexInFight = other.PlayerIndexInFight;
          }
label_23:
          if (other.ownerId_.HasValue)
          {
            if (this.ownerId_.HasValue)
            {
              nullable = other.OwnerId;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_27;
            }
            this.OwnerId = other.OwnerId;
          }
label_27:
          if (other.teamId_.HasValue)
          {
            if (this.teamId_.HasValue)
            {
              nullable = other.TeamId;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_31;
            }
            this.TeamId = other.TeamId;
          }
label_31:
          if (other.level_.HasValue)
          {
            if (this.level_.HasValue)
            {
              nullable = other.Level;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_35;
            }
            this.Level = other.Level;
          }
label_35:
          this.properties_.Add((IEnumerable<int>) other.properties_);
          if (other.position_ != null)
          {
            if (this.position_ == null)
              this.position_ = new CellCoord();
            this.Position.MergeFrom(other.Position);
          }
          if (other.direction_.HasValue)
          {
            if (this.direction_.HasValue)
            {
              nullable = other.Direction;
              int num = 0;
              if (nullable.GetValueOrDefault() == num & nullable.HasValue)
                goto label_43;
            }
            this.Direction = other.Direction;
          }
label_43:
          this.caracs_.Add((IDictionary<int, int>) other.caracs_);
          if (other.customSkin_ != null && (this.customSkin_ == null || other.CustomSkin != ""))
            this.CustomSkin = other.CustomSkin;
          if (other.actionDoneThisTurn_.HasValue)
          {
            if (this.actionDoneThisTurn_.HasValue)
            {
              bool? actionDoneThisTurn = other.ActionDoneThisTurn;
              bool flag = false;
              if (actionDoneThisTurn.GetValueOrDefault() == flag & actionDoneThisTurn.HasValue)
                goto label_49;
            }
            this.ActionDoneThisTurn = other.ActionDoneThisTurn;
          }
label_49:
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
              case 8:
                this.EntityId = input.ReadInt32();
                continue;
              case 16:
                this.EntityType = input.ReadInt32();
                continue;
              case 26:
                string str1 = FightSnapshot.Types.EntitySnapshot._single_name_codec.Read(input);
                if (this.name_ == null || str1 != "")
                {
                  this.Name = str1;
                  continue;
                }
                continue;
              case 34:
                int? nullable1 = FightSnapshot.Types.EntitySnapshot._single_defId_codec.Read(input);
                if (this.defId_.HasValue)
                {
                  int? nullable2 = nullable1;
                  int num2 = 0;
                  if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                    continue;
                }
                this.DefId = nullable1;
                continue;
              case 42:
                int? nullable3 = FightSnapshot.Types.EntitySnapshot._single_weaponId_codec.Read(input);
                if (this.weaponId_.HasValue)
                {
                  int? nullable4 = nullable3;
                  int num3 = 0;
                  if (nullable4.GetValueOrDefault() == num3 & nullable4.HasValue)
                    continue;
                }
                this.WeaponId = nullable3;
                continue;
              case 50:
                int? nullable5 = FightSnapshot.Types.EntitySnapshot._single_genderId_codec.Read(input);
                if (this.genderId_.HasValue)
                {
                  int? nullable6 = nullable5;
                  int num4 = 0;
                  if (nullable6.GetValueOrDefault() == num4 & nullable6.HasValue)
                    continue;
                }
                this.GenderId = nullable5;
                continue;
              case 58:
                int? nullable7 = FightSnapshot.Types.EntitySnapshot._single_playerIndexInFight_codec.Read(input);
                if (this.playerIndexInFight_.HasValue)
                {
                  int? nullable8 = nullable7;
                  int num5 = 0;
                  if (nullable8.GetValueOrDefault() == num5 & nullable8.HasValue)
                    continue;
                }
                this.PlayerIndexInFight = nullable7;
                continue;
              case 66:
                int? nullable9 = FightSnapshot.Types.EntitySnapshot._single_ownerId_codec.Read(input);
                if (this.ownerId_.HasValue)
                {
                  int? nullable10 = nullable9;
                  int num6 = 0;
                  if (nullable10.GetValueOrDefault() == num6 & nullable10.HasValue)
                    continue;
                }
                this.OwnerId = nullable9;
                continue;
              case 74:
                int? nullable11 = FightSnapshot.Types.EntitySnapshot._single_teamId_codec.Read(input);
                if (this.teamId_.HasValue)
                {
                  int? nullable12 = nullable11;
                  int num7 = 0;
                  if (nullable12.GetValueOrDefault() == num7 & nullable12.HasValue)
                    continue;
                }
                this.TeamId = nullable11;
                continue;
              case 82:
                int? nullable13 = FightSnapshot.Types.EntitySnapshot._single_level_codec.Read(input);
                if (this.level_.HasValue)
                {
                  int? nullable14 = nullable13;
                  int num8 = 0;
                  if (nullable14.GetValueOrDefault() == num8 & nullable14.HasValue)
                    continue;
                }
                this.Level = nullable13;
                continue;
              case 88:
              case 90:
                this.properties_.AddEntriesFrom(input, FightSnapshot.Types.EntitySnapshot._repeated_properties_codec);
                continue;
              case 98:
                if (this.position_ == null)
                  this.position_ = new CellCoord();
                input.ReadMessage((IMessage) this.position_);
                continue;
              case 106:
                int? nullable15 = FightSnapshot.Types.EntitySnapshot._single_direction_codec.Read(input);
                if (this.direction_.HasValue)
                {
                  int? nullable16 = nullable15;
                  int num9 = 0;
                  if (nullable16.GetValueOrDefault() == num9 & nullable16.HasValue)
                    continue;
                }
                this.Direction = nullable15;
                continue;
              case 114:
                this.caracs_.AddEntriesFrom(input, FightSnapshot.Types.EntitySnapshot._map_caracs_codec);
                continue;
              case 122:
                string str2 = FightSnapshot.Types.EntitySnapshot._single_customSkin_codec.Read(input);
                if (this.customSkin_ == null || str2 != "")
                {
                  this.CustomSkin = str2;
                  continue;
                }
                continue;
              case 130:
                bool? nullable17 = FightSnapshot.Types.EntitySnapshot._single_actionDoneThisTurn_codec.Read(input);
                if (this.actionDoneThisTurn_.HasValue)
                {
                  bool? nullable18 = nullable17;
                  bool flag = false;
                  if (nullable18.GetValueOrDefault() == flag & nullable18.HasValue)
                    continue;
                }
                this.ActionDoneThisTurn = nullable17;
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (EntitySnapshot);
      }

      public sealed class Companions : 
        IMessage<FightSnapshot.Types.Companions>,
        IMessage,
        IEquatable<FightSnapshot.Types.Companions>,
        IDeepCloneable<FightSnapshot.Types.Companions>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<FightSnapshot.Types.Companions> _parser = new MessageParser<FightSnapshot.Types.Companions>((Func<FightSnapshot.Types.Companions>) (() => new FightSnapshot.Types.Companions()));
        private UnknownFieldSet _unknownFields;
        public const int AllDefIdsFieldNumber = 1;
        private static readonly FieldCodec<int> _repeated_allDefIds_codec = FieldCodec.ForInt32(10U);
        private readonly RepeatedField<int> allDefIds_ = new RepeatedField<int>();
        public const int AvailableIdsFieldNumber = 2;
        private static readonly FieldCodec<int> _repeated_availableIds_codec = FieldCodec.ForInt32(18U);
        private readonly RepeatedField<int> availableIds_ = new RepeatedField<int>();

        [DebuggerNonUserCode]
        public static MessageParser<FightSnapshot.Types.Companions> Parser => FightSnapshot.Types.Companions._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => FightSnapshot.Descriptor.NestedTypes[3];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightSnapshot.Types.Companions.Descriptor;

        [DebuggerNonUserCode]
        public Companions()
        {
        }

        [DebuggerNonUserCode]
        public Companions(FightSnapshot.Types.Companions other)
          : this()
        {
          this.allDefIds_ = other.allDefIds_.Clone();
          this.availableIds_ = other.availableIds_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public FightSnapshot.Types.Companions Clone() => new FightSnapshot.Types.Companions(this);

        [DebuggerNonUserCode]
        public RepeatedField<int> AllDefIds => this.allDefIds_;

        [DebuggerNonUserCode]
        public RepeatedField<int> AvailableIds => this.availableIds_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as FightSnapshot.Types.Companions);

        [DebuggerNonUserCode]
        public bool Equals(FightSnapshot.Types.Companions other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.allDefIds_.Equals(other.allDefIds_) && this.availableIds_.Equals(other.availableIds_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1 ^ this.allDefIds_.GetHashCode() ^ this.availableIds_.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          this.allDefIds_.WriteTo(output, FightSnapshot.Types.Companions._repeated_allDefIds_codec);
          this.availableIds_.WriteTo(output, FightSnapshot.Types.Companions._repeated_availableIds_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0 + this.allDefIds_.CalculateSize(FightSnapshot.Types.Companions._repeated_allDefIds_codec) + this.availableIds_.CalculateSize(FightSnapshot.Types.Companions._repeated_availableIds_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(FightSnapshot.Types.Companions other)
        {
          if (other == null)
            return;
          this.allDefIds_.Add((IEnumerable<int>) other.allDefIds_);
          this.availableIds_.Add((IEnumerable<int>) other.availableIds_);
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
              case 10:
                this.allDefIds_.AddEntriesFrom(input, FightSnapshot.Types.Companions._repeated_allDefIds_codec);
                continue;
              case 16:
              case 18:
                this.availableIds_.AddEntriesFrom(input, FightSnapshot.Types.Companions._repeated_availableIds_codec);
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (Companions);
      }
    }
  }
}
