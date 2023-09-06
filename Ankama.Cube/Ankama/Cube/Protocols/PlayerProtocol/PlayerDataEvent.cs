// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.PlayerDataEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class PlayerDataEvent : 
    IMessage<PlayerDataEvent>,
    IMessage,
    IEquatable<PlayerDataEvent>,
    IDeepCloneable<PlayerDataEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<PlayerDataEvent> _parser = new MessageParser<PlayerDataEvent>((Func<PlayerDataEvent>) (() => new PlayerDataEvent()));
    private UnknownFieldSet _unknownFields;
    public const int AccountFieldNumber = 1;
    private PlayerDataEvent.Types.AccountData account_;
    public const int CompanionDataFieldNumber = 2;
    private PlayerDataEvent.Types.CompanionData companionData_;
    public const int WeaponLevelsDataFieldNumber = 3;
    private PlayerDataEvent.Types.WeaponLevelsData weaponLevelsData_;
    public const int SelectedWeaponsDataFieldNumber = 4;
    private PlayerDataEvent.Types.SelectedWeaponsData selectedWeaponsData_;
    public const int DecksFieldNumber = 5;
    private PlayerDataEvent.Types.DecksData decks_;
    public const int OccupationFieldNumber = 6;
    private PlayerDataEvent.Types.OccupationData occupation_;
    public const int HeroFieldNumber = 7;
    private PlayerDataEvent.Types.HeroData hero_;
    public const int DecksUpdatesFieldNumber = 8;
    private PlayerDataEvent.Types.DeckIncrementalUpdateData decksUpdates_;

    [DebuggerNonUserCode]
    public static MessageParser<PlayerDataEvent> Parser => PlayerDataEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[4];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Descriptor;

    [DebuggerNonUserCode]
    public PlayerDataEvent()
    {
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent(PlayerDataEvent other)
      : this()
    {
      this.account_ = other.account_ != null ? other.account_.Clone() : (PlayerDataEvent.Types.AccountData) null;
      this.companionData_ = other.companionData_ != null ? other.companionData_.Clone() : (PlayerDataEvent.Types.CompanionData) null;
      this.weaponLevelsData_ = other.weaponLevelsData_ != null ? other.weaponLevelsData_.Clone() : (PlayerDataEvent.Types.WeaponLevelsData) null;
      this.selectedWeaponsData_ = other.selectedWeaponsData_ != null ? other.selectedWeaponsData_.Clone() : (PlayerDataEvent.Types.SelectedWeaponsData) null;
      this.decks_ = other.decks_ != null ? other.decks_.Clone() : (PlayerDataEvent.Types.DecksData) null;
      this.occupation_ = other.occupation_ != null ? other.occupation_.Clone() : (PlayerDataEvent.Types.OccupationData) null;
      this.hero_ = other.hero_ != null ? other.hero_.Clone() : (PlayerDataEvent.Types.HeroData) null;
      this.decksUpdates_ = other.decksUpdates_ != null ? other.decksUpdates_.Clone() : (PlayerDataEvent.Types.DeckIncrementalUpdateData) null;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent Clone() => new PlayerDataEvent(this);

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.AccountData Account
    {
      get => this.account_;
      set => this.account_ = value;
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.CompanionData CompanionData
    {
      get => this.companionData_;
      set => this.companionData_ = value;
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.WeaponLevelsData WeaponLevelsData
    {
      get => this.weaponLevelsData_;
      set => this.weaponLevelsData_ = value;
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.SelectedWeaponsData SelectedWeaponsData
    {
      get => this.selectedWeaponsData_;
      set => this.selectedWeaponsData_ = value;
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.DecksData Decks
    {
      get => this.decks_;
      set => this.decks_ = value;
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.OccupationData Occupation
    {
      get => this.occupation_;
      set => this.occupation_ = value;
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.HeroData Hero
    {
      get => this.hero_;
      set => this.hero_ = value;
    }

    [DebuggerNonUserCode]
    public PlayerDataEvent.Types.DeckIncrementalUpdateData DecksUpdates
    {
      get => this.decksUpdates_;
      set => this.decksUpdates_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as PlayerDataEvent);

    [DebuggerNonUserCode]
    public bool Equals(PlayerDataEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return object.Equals((object) this.Account, (object) other.Account) && object.Equals((object) this.CompanionData, (object) other.CompanionData) && object.Equals((object) this.WeaponLevelsData, (object) other.WeaponLevelsData) && object.Equals((object) this.SelectedWeaponsData, (object) other.SelectedWeaponsData) && object.Equals((object) this.Decks, (object) other.Decks) && object.Equals((object) this.Occupation, (object) other.Occupation) && object.Equals((object) this.Hero, (object) other.Hero) && object.Equals((object) this.DecksUpdates, (object) other.DecksUpdates) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.account_ != null)
        hashCode ^= this.Account.GetHashCode();
      if (this.companionData_ != null)
        hashCode ^= this.CompanionData.GetHashCode();
      if (this.weaponLevelsData_ != null)
        hashCode ^= this.WeaponLevelsData.GetHashCode();
      if (this.selectedWeaponsData_ != null)
        hashCode ^= this.SelectedWeaponsData.GetHashCode();
      if (this.decks_ != null)
        hashCode ^= this.Decks.GetHashCode();
      if (this.occupation_ != null)
        hashCode ^= this.Occupation.GetHashCode();
      if (this.hero_ != null)
        hashCode ^= this.Hero.GetHashCode();
      if (this.decksUpdates_ != null)
        hashCode ^= this.DecksUpdates.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.account_ != null)
      {
        output.WriteRawTag((byte) 10);
        output.WriteMessage((IMessage) this.Account);
      }
      if (this.companionData_ != null)
      {
        output.WriteRawTag((byte) 18);
        output.WriteMessage((IMessage) this.CompanionData);
      }
      if (this.weaponLevelsData_ != null)
      {
        output.WriteRawTag((byte) 26);
        output.WriteMessage((IMessage) this.WeaponLevelsData);
      }
      if (this.selectedWeaponsData_ != null)
      {
        output.WriteRawTag((byte) 34);
        output.WriteMessage((IMessage) this.SelectedWeaponsData);
      }
      if (this.decks_ != null)
      {
        output.WriteRawTag((byte) 42);
        output.WriteMessage((IMessage) this.Decks);
      }
      if (this.occupation_ != null)
      {
        output.WriteRawTag((byte) 50);
        output.WriteMessage((IMessage) this.Occupation);
      }
      if (this.hero_ != null)
      {
        output.WriteRawTag((byte) 58);
        output.WriteMessage((IMessage) this.Hero);
      }
      if (this.decksUpdates_ != null)
      {
        output.WriteRawTag((byte) 66);
        output.WriteMessage((IMessage) this.DecksUpdates);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.account_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Account);
      if (this.companionData_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.CompanionData);
      if (this.weaponLevelsData_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.WeaponLevelsData);
      if (this.selectedWeaponsData_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SelectedWeaponsData);
      if (this.decks_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Decks);
      if (this.occupation_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Occupation);
      if (this.hero_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Hero);
      if (this.decksUpdates_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.DecksUpdates);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(PlayerDataEvent other)
    {
      if (other == null)
        return;
      if (other.account_ != null)
      {
        if (this.account_ == null)
          this.account_ = new PlayerDataEvent.Types.AccountData();
        this.Account.MergeFrom(other.Account);
      }
      if (other.companionData_ != null)
      {
        if (this.companionData_ == null)
          this.companionData_ = new PlayerDataEvent.Types.CompanionData();
        this.CompanionData.MergeFrom(other.CompanionData);
      }
      if (other.weaponLevelsData_ != null)
      {
        if (this.weaponLevelsData_ == null)
          this.weaponLevelsData_ = new PlayerDataEvent.Types.WeaponLevelsData();
        this.WeaponLevelsData.MergeFrom(other.WeaponLevelsData);
      }
      if (other.selectedWeaponsData_ != null)
      {
        if (this.selectedWeaponsData_ == null)
          this.selectedWeaponsData_ = new PlayerDataEvent.Types.SelectedWeaponsData();
        this.SelectedWeaponsData.MergeFrom(other.SelectedWeaponsData);
      }
      if (other.decks_ != null)
      {
        if (this.decks_ == null)
          this.decks_ = new PlayerDataEvent.Types.DecksData();
        this.Decks.MergeFrom(other.Decks);
      }
      if (other.occupation_ != null)
      {
        if (this.occupation_ == null)
          this.occupation_ = new PlayerDataEvent.Types.OccupationData();
        this.Occupation.MergeFrom(other.Occupation);
      }
      if (other.hero_ != null)
      {
        if (this.hero_ == null)
          this.hero_ = new PlayerDataEvent.Types.HeroData();
        this.Hero.MergeFrom(other.Hero);
      }
      if (other.decksUpdates_ != null)
      {
        if (this.decksUpdates_ == null)
          this.decksUpdates_ = new PlayerDataEvent.Types.DeckIncrementalUpdateData();
        this.DecksUpdates.MergeFrom(other.DecksUpdates);
      }
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
            if (this.account_ == null)
              this.account_ = new PlayerDataEvent.Types.AccountData();
            input.ReadMessage((IMessage) this.account_);
            continue;
          case 18:
            if (this.companionData_ == null)
              this.companionData_ = new PlayerDataEvent.Types.CompanionData();
            input.ReadMessage((IMessage) this.companionData_);
            continue;
          case 26:
            if (this.weaponLevelsData_ == null)
              this.weaponLevelsData_ = new PlayerDataEvent.Types.WeaponLevelsData();
            input.ReadMessage((IMessage) this.weaponLevelsData_);
            continue;
          case 34:
            if (this.selectedWeaponsData_ == null)
              this.selectedWeaponsData_ = new PlayerDataEvent.Types.SelectedWeaponsData();
            input.ReadMessage((IMessage) this.selectedWeaponsData_);
            continue;
          case 42:
            if (this.decks_ == null)
              this.decks_ = new PlayerDataEvent.Types.DecksData();
            input.ReadMessage((IMessage) this.decks_);
            continue;
          case 50:
            if (this.occupation_ == null)
              this.occupation_ = new PlayerDataEvent.Types.OccupationData();
            input.ReadMessage((IMessage) this.occupation_);
            continue;
          case 58:
            if (this.hero_ == null)
              this.hero_ = new PlayerDataEvent.Types.HeroData();
            input.ReadMessage((IMessage) this.hero_);
            continue;
          case 66:
            if (this.decksUpdates_ == null)
              this.decksUpdates_ = new PlayerDataEvent.Types.DeckIncrementalUpdateData();
            input.ReadMessage((IMessage) this.decksUpdates_);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (PlayerDataEvent);

    [DebuggerNonUserCode]
    public static class Types
    {
      public sealed class AccountData : 
        IMessage<PlayerDataEvent.Types.AccountData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.AccountData>,
        IDeepCloneable<PlayerDataEvent.Types.AccountData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.AccountData> _parser = new MessageParser<PlayerDataEvent.Types.AccountData>((Func<PlayerDataEvent.Types.AccountData>) (() => new PlayerDataEvent.Types.AccountData()));
        private UnknownFieldSet _unknownFields;
        public const int HashFieldNumber = 1;
        private int hash_;
        public const int NickNameFieldNumber = 2;
        private static readonly FieldCodec<string> _single_nickName_codec = FieldCodec.ForClassWrapper<string>(18U);
        private string nickName_;
        public const int AdminFieldNumber = 3;
        private bool admin_;
        public const int AccountTypeFieldNumber = 4;
        private string accountType_ = "";

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.AccountData> Parser => PlayerDataEvent.Types.AccountData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[0];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.AccountData.Descriptor;

        [DebuggerNonUserCode]
        public AccountData()
        {
        }

        [DebuggerNonUserCode]
        public AccountData(PlayerDataEvent.Types.AccountData other)
          : this()
        {
          this.hash_ = other.hash_;
          this.NickName = other.NickName;
          this.admin_ = other.admin_;
          this.accountType_ = other.accountType_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.AccountData Clone() => new PlayerDataEvent.Types.AccountData(this);

        [DebuggerNonUserCode]
        public int Hash
        {
          get => this.hash_;
          set => this.hash_ = value;
        }

        [DebuggerNonUserCode]
        public string NickName
        {
          get => this.nickName_;
          set => this.nickName_ = value;
        }

        [DebuggerNonUserCode]
        public bool Admin
        {
          get => this.admin_;
          set => this.admin_ = value;
        }

        [DebuggerNonUserCode]
        public string AccountType
        {
          get => this.accountType_;
          set => this.accountType_ = ProtoPreconditions.CheckNotNull<string>(value, nameof (value));
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.AccountData);

        [DebuggerNonUserCode]
        public bool Equals(PlayerDataEvent.Types.AccountData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.Hash == other.Hash && !(this.NickName != other.NickName) && this.Admin == other.Admin && !(this.AccountType != other.AccountType) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.Hash != 0)
            hashCode ^= this.Hash.GetHashCode();
          if (this.nickName_ != null)
            hashCode ^= this.NickName.GetHashCode();
          if (this.Admin)
            hashCode ^= this.Admin.GetHashCode();
          if (this.AccountType.Length != 0)
            hashCode ^= this.AccountType.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.Hash != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.Hash);
          }
          if (this.nickName_ != null)
            PlayerDataEvent.Types.AccountData._single_nickName_codec.WriteTagAndValue(output, this.NickName);
          if (this.Admin)
          {
            output.WriteRawTag((byte) 24);
            output.WriteBool(this.Admin);
          }
          if (this.AccountType.Length != 0)
          {
            output.WriteRawTag((byte) 34);
            output.WriteString(this.AccountType);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.Hash != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Hash);
          if (this.nickName_ != null)
            size += PlayerDataEvent.Types.AccountData._single_nickName_codec.CalculateSizeWithTag(this.NickName);
          if (this.Admin)
            size += 2;
          if (this.AccountType.Length != 0)
            size += 1 + CodedOutputStream.ComputeStringSize(this.AccountType);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(PlayerDataEvent.Types.AccountData other)
        {
          if (other == null)
            return;
          if (other.Hash != 0)
            this.Hash = other.Hash;
          if (other.nickName_ != null && (this.nickName_ == null || other.NickName != ""))
            this.NickName = other.NickName;
          if (other.Admin)
            this.Admin = other.Admin;
          if (other.AccountType.Length != 0)
            this.AccountType = other.AccountType;
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
                this.Hash = input.ReadInt32();
                continue;
              case 18:
                string str = PlayerDataEvent.Types.AccountData._single_nickName_codec.Read(input);
                if (this.nickName_ == null || str != "")
                {
                  this.NickName = str;
                  continue;
                }
                continue;
              case 24:
                this.Admin = input.ReadBool();
                continue;
              case 34:
                this.AccountType = input.ReadString();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (AccountData);
      }

      public sealed class DecksData : 
        IMessage<PlayerDataEvent.Types.DecksData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.DecksData>,
        IDeepCloneable<PlayerDataEvent.Types.DecksData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.DecksData> _parser = new MessageParser<PlayerDataEvent.Types.DecksData>((Func<PlayerDataEvent.Types.DecksData>) (() => new PlayerDataEvent.Types.DecksData()));
        private UnknownFieldSet _unknownFields;
        public const int CustomDecksFieldNumber = 1;
        private static readonly FieldCodec<DeckInfo> _repeated_customDecks_codec = FieldCodec.ForMessage<DeckInfo>(10U, DeckInfo.Parser);
        private readonly RepeatedField<DeckInfo> customDecks_ = new RepeatedField<DeckInfo>();
        public const int SelectedDecksFieldNumber = 2;
        private static readonly MapField<int, int>.Codec _map_selectedDecks_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8U), FieldCodec.ForInt32(16U), 18U);
        private readonly MapField<int, int> selectedDecks_ = new MapField<int, int>();

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.DecksData> Parser => PlayerDataEvent.Types.DecksData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[1];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.DecksData.Descriptor;

        [DebuggerNonUserCode]
        public DecksData()
        {
        }

        [DebuggerNonUserCode]
        public DecksData(PlayerDataEvent.Types.DecksData other)
          : this()
        {
          this.customDecks_ = other.customDecks_.Clone();
          this.selectedDecks_ = other.selectedDecks_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.DecksData Clone() => new PlayerDataEvent.Types.DecksData(this);

        [DebuggerNonUserCode]
        public RepeatedField<DeckInfo> CustomDecks => this.customDecks_;

        [DebuggerNonUserCode]
        public MapField<int, int> SelectedDecks => this.selectedDecks_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.DecksData);

        [DebuggerNonUserCode]
        public bool Equals(PlayerDataEvent.Types.DecksData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.customDecks_.Equals(other.customDecks_) && this.SelectedDecks.Equals(other.SelectedDecks) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1 ^ this.customDecks_.GetHashCode() ^ this.SelectedDecks.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          this.customDecks_.WriteTo(output, PlayerDataEvent.Types.DecksData._repeated_customDecks_codec);
          this.selectedDecks_.WriteTo(output, PlayerDataEvent.Types.DecksData._map_selectedDecks_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0 + this.customDecks_.CalculateSize(PlayerDataEvent.Types.DecksData._repeated_customDecks_codec) + this.selectedDecks_.CalculateSize(PlayerDataEvent.Types.DecksData._map_selectedDecks_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(PlayerDataEvent.Types.DecksData other)
        {
          if (other == null)
            return;
          this.customDecks_.Add((IEnumerable<DeckInfo>) other.customDecks_);
          this.selectedDecks_.Add((IDictionary<int, int>) other.selectedDecks_);
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
                this.customDecks_.AddEntriesFrom(input, PlayerDataEvent.Types.DecksData._repeated_customDecks_codec);
                continue;
              case 18:
                this.selectedDecks_.AddEntriesFrom(input, PlayerDataEvent.Types.DecksData._map_selectedDecks_codec);
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (DecksData);
      }

      public sealed class OccupationData : 
        IMessage<PlayerDataEvent.Types.OccupationData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.OccupationData>,
        IDeepCloneable<PlayerDataEvent.Types.OccupationData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.OccupationData> _parser = new MessageParser<PlayerDataEvent.Types.OccupationData>((Func<PlayerDataEvent.Types.OccupationData>) (() => new PlayerDataEvent.Types.OccupationData()));
        private UnknownFieldSet _unknownFields;
        public const int InFightFieldNumber = 1;
        private bool inFight_;

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.OccupationData> Parser => PlayerDataEvent.Types.OccupationData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[2];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.OccupationData.Descriptor;

        [DebuggerNonUserCode]
        public OccupationData()
        {
        }

        [DebuggerNonUserCode]
        public OccupationData(PlayerDataEvent.Types.OccupationData other)
          : this()
        {
          this.inFight_ = other.inFight_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.OccupationData Clone() => new PlayerDataEvent.Types.OccupationData(this);

        [DebuggerNonUserCode]
        public bool InFight
        {
          get => this.inFight_;
          set => this.inFight_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.OccupationData);

        [DebuggerNonUserCode]
        public bool Equals(PlayerDataEvent.Types.OccupationData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.InFight == other.InFight && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.InFight)
            hashCode ^= this.InFight.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.InFight)
          {
            output.WriteRawTag((byte) 8);
            output.WriteBool(this.InFight);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.InFight)
            size += 2;
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(PlayerDataEvent.Types.OccupationData other)
        {
          if (other == null)
            return;
          if (other.InFight)
            this.InFight = other.InFight;
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
              this.InFight = input.ReadBool();
          }
        }

        public string ToDiagnosticString() => nameof (OccupationData);
      }

      public sealed class HeroData : 
        IMessage<PlayerDataEvent.Types.HeroData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.HeroData>,
        IDeepCloneable<PlayerDataEvent.Types.HeroData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.HeroData> _parser = new MessageParser<PlayerDataEvent.Types.HeroData>((Func<PlayerDataEvent.Types.HeroData>) (() => new PlayerDataEvent.Types.HeroData()));
        private UnknownFieldSet _unknownFields;
        public const int InfoFieldNumber = 1;
        private HeroInfo info_;

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.HeroData> Parser => PlayerDataEvent.Types.HeroData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[3];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.HeroData.Descriptor;

        [DebuggerNonUserCode]
        public HeroData()
        {
        }

        [DebuggerNonUserCode]
        public HeroData(PlayerDataEvent.Types.HeroData other)
          : this()
        {
          this.info_ = other.info_ != null ? other.info_.Clone() : (HeroInfo) null;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.HeroData Clone() => new PlayerDataEvent.Types.HeroData(this);

        [DebuggerNonUserCode]
        public HeroInfo Info
        {
          get => this.info_;
          set => this.info_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.HeroData);

        [DebuggerNonUserCode]
        public bool Equals(PlayerDataEvent.Types.HeroData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return object.Equals((object) this.Info, (object) other.Info) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.info_ != null)
            hashCode ^= this.Info.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.info_ != null)
          {
            output.WriteRawTag((byte) 10);
            output.WriteMessage((IMessage) this.Info);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.info_ != null)
            size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Info);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(PlayerDataEvent.Types.HeroData other)
        {
          if (other == null)
            return;
          if (other.info_ != null)
          {
            if (this.info_ == null)
              this.info_ = new HeroInfo();
            this.Info.MergeFrom(other.Info);
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
              if (this.info_ == null)
                this.info_ = new HeroInfo();
              input.ReadMessage((IMessage) this.info_);
            }
          }
        }

        public string ToDiagnosticString() => nameof (HeroData);
      }

      public sealed class CompanionData : 
        IMessage<PlayerDataEvent.Types.CompanionData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.CompanionData>,
        IDeepCloneable<PlayerDataEvent.Types.CompanionData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.CompanionData> _parser = new MessageParser<PlayerDataEvent.Types.CompanionData>((Func<PlayerDataEvent.Types.CompanionData>) (() => new PlayerDataEvent.Types.CompanionData()));
        private UnknownFieldSet _unknownFields;
        public const int CompanionsFieldNumber = 1;
        private static readonly FieldCodec<int> _repeated_companions_codec = FieldCodec.ForInt32(10U);
        private readonly RepeatedField<int> companions_ = new RepeatedField<int>();

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.CompanionData> Parser => PlayerDataEvent.Types.CompanionData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[4];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.CompanionData.Descriptor;

        [DebuggerNonUserCode]
        public CompanionData()
        {
        }

        [DebuggerNonUserCode]
        public CompanionData(PlayerDataEvent.Types.CompanionData other)
          : this()
        {
          this.companions_ = other.companions_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.CompanionData Clone() => new PlayerDataEvent.Types.CompanionData(this);

        [DebuggerNonUserCode]
        public RepeatedField<int> Companions => this.companions_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.CompanionData);

        [DebuggerNonUserCode]
        public bool Equals(PlayerDataEvent.Types.CompanionData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.companions_.Equals(other.companions_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1 ^ this.companions_.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          this.companions_.WriteTo(output, PlayerDataEvent.Types.CompanionData._repeated_companions_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0 + this.companions_.CalculateSize(PlayerDataEvent.Types.CompanionData._repeated_companions_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(PlayerDataEvent.Types.CompanionData other)
        {
          if (other == null)
            return;
          this.companions_.Add((IEnumerable<int>) other.companions_);
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            if (num != 8U && num != 10U)
              this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            else
              this.companions_.AddEntriesFrom(input, PlayerDataEvent.Types.CompanionData._repeated_companions_codec);
          }
        }

        public string ToDiagnosticString() => nameof (CompanionData);
      }

      public sealed class WeaponLevelsData : 
        IMessage<PlayerDataEvent.Types.WeaponLevelsData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.WeaponLevelsData>,
        IDeepCloneable<PlayerDataEvent.Types.WeaponLevelsData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.WeaponLevelsData> _parser = new MessageParser<PlayerDataEvent.Types.WeaponLevelsData>((Func<PlayerDataEvent.Types.WeaponLevelsData>) (() => new PlayerDataEvent.Types.WeaponLevelsData()));
        private UnknownFieldSet _unknownFields;
        public const int WeaponLevelsFieldNumber = 1;
        private static readonly MapField<int, int>.Codec _map_weaponLevels_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8U), FieldCodec.ForInt32(16U), 10U);
        private readonly MapField<int, int> weaponLevels_ = new MapField<int, int>();

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.WeaponLevelsData> Parser => PlayerDataEvent.Types.WeaponLevelsData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[5];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.WeaponLevelsData.Descriptor;

        [DebuggerNonUserCode]
        public WeaponLevelsData()
        {
        }

        [DebuggerNonUserCode]
        public WeaponLevelsData(PlayerDataEvent.Types.WeaponLevelsData other)
          : this()
        {
          this.weaponLevels_ = other.weaponLevels_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.WeaponLevelsData Clone() => new PlayerDataEvent.Types.WeaponLevelsData(this);

        [DebuggerNonUserCode]
        public MapField<int, int> WeaponLevels => this.weaponLevels_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.WeaponLevelsData);

        [DebuggerNonUserCode]
        public bool Equals(PlayerDataEvent.Types.WeaponLevelsData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.WeaponLevels.Equals(other.WeaponLevels) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1 ^ this.WeaponLevels.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          this.weaponLevels_.WriteTo(output, PlayerDataEvent.Types.WeaponLevelsData._map_weaponLevels_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0 + this.weaponLevels_.CalculateSize(PlayerDataEvent.Types.WeaponLevelsData._map_weaponLevels_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(PlayerDataEvent.Types.WeaponLevelsData other)
        {
          if (other == null)
            return;
          this.weaponLevels_.Add((IDictionary<int, int>) other.weaponLevels_);
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
              this.weaponLevels_.AddEntriesFrom(input, PlayerDataEvent.Types.WeaponLevelsData._map_weaponLevels_codec);
          }
        }

        public string ToDiagnosticString() => nameof (WeaponLevelsData);
      }

      public sealed class SelectedWeaponsData : 
        IMessage<PlayerDataEvent.Types.SelectedWeaponsData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.SelectedWeaponsData>,
        IDeepCloneable<PlayerDataEvent.Types.SelectedWeaponsData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.SelectedWeaponsData> _parser = new MessageParser<PlayerDataEvent.Types.SelectedWeaponsData>((Func<PlayerDataEvent.Types.SelectedWeaponsData>) (() => new PlayerDataEvent.Types.SelectedWeaponsData()));
        private UnknownFieldSet _unknownFields;
        public const int SelectedWeaponsFieldNumber = 1;
        private static readonly MapField<int, int>.Codec _map_selectedWeapons_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8U), FieldCodec.ForInt32(16U), 10U);
        private readonly MapField<int, int> selectedWeapons_ = new MapField<int, int>();

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.SelectedWeaponsData> Parser => PlayerDataEvent.Types.SelectedWeaponsData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[6];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.SelectedWeaponsData.Descriptor;

        [DebuggerNonUserCode]
        public SelectedWeaponsData()
        {
        }

        [DebuggerNonUserCode]
        public SelectedWeaponsData(PlayerDataEvent.Types.SelectedWeaponsData other)
          : this()
        {
          this.selectedWeapons_ = other.selectedWeapons_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.SelectedWeaponsData Clone() => new PlayerDataEvent.Types.SelectedWeaponsData(this);

        [DebuggerNonUserCode]
        public MapField<int, int> SelectedWeapons => this.selectedWeapons_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.SelectedWeaponsData);

        [DebuggerNonUserCode]
        public bool Equals(PlayerDataEvent.Types.SelectedWeaponsData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.SelectedWeapons.Equals(other.SelectedWeapons) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1 ^ this.SelectedWeapons.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          this.selectedWeapons_.WriteTo(output, PlayerDataEvent.Types.SelectedWeaponsData._map_selectedWeapons_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0 + this.selectedWeapons_.CalculateSize(PlayerDataEvent.Types.SelectedWeaponsData._map_selectedWeapons_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(PlayerDataEvent.Types.SelectedWeaponsData other)
        {
          if (other == null)
            return;
          this.selectedWeapons_.Add((IDictionary<int, int>) other.selectedWeapons_);
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
              this.selectedWeapons_.AddEntriesFrom(input, PlayerDataEvent.Types.SelectedWeaponsData._map_selectedWeapons_codec);
          }
        }

        public string ToDiagnosticString() => nameof (SelectedWeaponsData);
      }

      public sealed class DeckIncrementalUpdateData : 
        IMessage<PlayerDataEvent.Types.DeckIncrementalUpdateData>,
        IMessage,
        IEquatable<PlayerDataEvent.Types.DeckIncrementalUpdateData>,
        IDeepCloneable<PlayerDataEvent.Types.DeckIncrementalUpdateData>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<PlayerDataEvent.Types.DeckIncrementalUpdateData> _parser = new MessageParser<PlayerDataEvent.Types.DeckIncrementalUpdateData>((Func<PlayerDataEvent.Types.DeckIncrementalUpdateData>) (() => new PlayerDataEvent.Types.DeckIncrementalUpdateData()));
        private UnknownFieldSet _unknownFields;
        public const int DeckUpdatedFieldNumber = 1;
        private static readonly FieldCodec<DeckInfo> _repeated_deckUpdated_codec = FieldCodec.ForMessage<DeckInfo>(10U, DeckInfo.Parser);
        private readonly RepeatedField<DeckInfo> deckUpdated_ = new RepeatedField<DeckInfo>();
        public const int DeckRemovedIdFieldNumber = 2;
        private static readonly FieldCodec<int> _repeated_deckRemovedId_codec = FieldCodec.ForInt32(18U);
        private readonly RepeatedField<int> deckRemovedId_ = new RepeatedField<int>();
        public const int DeckSelectionsUpdatedFieldNumber = 3;
        private static readonly FieldCodec<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon> _repeated_deckSelectionsUpdated_codec = FieldCodec.ForMessage<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>(26U, PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon.Parser);
        private readonly RepeatedField<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon> deckSelectionsUpdated_ = new RepeatedField<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>();

        [DebuggerNonUserCode]
        public static MessageParser<PlayerDataEvent.Types.DeckIncrementalUpdateData> Parser => PlayerDataEvent.Types.DeckIncrementalUpdateData._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => PlayerDataEvent.Descriptor.NestedTypes[7];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.DeckIncrementalUpdateData.Descriptor;

        [DebuggerNonUserCode]
        public DeckIncrementalUpdateData()
        {
        }

        [DebuggerNonUserCode]
        public DeckIncrementalUpdateData(
          PlayerDataEvent.Types.DeckIncrementalUpdateData other)
          : this()
        {
          this.deckUpdated_ = other.deckUpdated_.Clone();
          this.deckRemovedId_ = other.deckRemovedId_.Clone();
          this.deckSelectionsUpdated_ = other.deckSelectionsUpdated_.Clone();
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public PlayerDataEvent.Types.DeckIncrementalUpdateData Clone() => new PlayerDataEvent.Types.DeckIncrementalUpdateData(this);

        [DebuggerNonUserCode]
        public RepeatedField<DeckInfo> DeckUpdated => this.deckUpdated_;

        [DebuggerNonUserCode]
        public RepeatedField<int> DeckRemovedId => this.deckRemovedId_;

        [DebuggerNonUserCode]
        public RepeatedField<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon> DeckSelectionsUpdated => this.deckSelectionsUpdated_;

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.DeckIncrementalUpdateData);

        [DebuggerNonUserCode]
        public bool Equals(
          PlayerDataEvent.Types.DeckIncrementalUpdateData other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.deckUpdated_.Equals(other.deckUpdated_) && this.deckRemovedId_.Equals(other.deckRemovedId_) && this.deckSelectionsUpdated_.Equals(other.deckSelectionsUpdated_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1 ^ this.deckUpdated_.GetHashCode() ^ this.deckRemovedId_.GetHashCode() ^ this.deckSelectionsUpdated_.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          this.deckUpdated_.WriteTo(output, PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckUpdated_codec);
          this.deckRemovedId_.WriteTo(output, PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckRemovedId_codec);
          this.deckSelectionsUpdated_.WriteTo(output, PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckSelectionsUpdated_codec);
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0 + this.deckUpdated_.CalculateSize(PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckUpdated_codec) + this.deckRemovedId_.CalculateSize(PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckRemovedId_codec) + this.deckSelectionsUpdated_.CalculateSize(PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckSelectionsUpdated_codec);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(
          PlayerDataEvent.Types.DeckIncrementalUpdateData other)
        {
          if (other == null)
            return;
          this.deckUpdated_.Add((IEnumerable<DeckInfo>) other.deckUpdated_);
          this.deckRemovedId_.Add((IEnumerable<int>) other.deckRemovedId_);
          this.deckSelectionsUpdated_.Add((IEnumerable<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>) other.deckSelectionsUpdated_);
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
                this.deckUpdated_.AddEntriesFrom(input, PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckUpdated_codec);
                continue;
              case 16:
              case 18:
                this.deckRemovedId_.AddEntriesFrom(input, PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckRemovedId_codec);
                continue;
              case 26:
                this.deckSelectionsUpdated_.AddEntriesFrom(input, PlayerDataEvent.Types.DeckIncrementalUpdateData._repeated_deckSelectionsUpdated_codec);
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (DeckIncrementalUpdateData);

        [DebuggerNonUserCode]
        public static class Types
        {
          public sealed class SelectedDeckPerWeapon : 
            IMessage<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>,
            IMessage,
            IEquatable<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>,
            IDeepCloneable<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>,
            ICustomDiagnosticMessage
          {
            private static readonly MessageParser<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon> _parser = new MessageParser<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>((Func<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon>) (() => new PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon()));
            private UnknownFieldSet _unknownFields;
            public const int WeaponIdFieldNumber = 1;
            private int weaponId_;
            public const int DeckIdFieldNumber = 2;
            private static readonly FieldCodec<int?> _single_deckId_codec = FieldCodec.ForStructWrapper<int>(18U);
            private int? deckId_;

            [DebuggerNonUserCode]
            public static MessageParser<PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon> Parser => PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon._parser;

            [DebuggerNonUserCode]
            public static MessageDescriptor Descriptor => PlayerDataEvent.Types.DeckIncrementalUpdateData.Descriptor.NestedTypes[0];

            [DebuggerNonUserCode]
            MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon.Descriptor;

            [DebuggerNonUserCode]
            public SelectedDeckPerWeapon()
            {
            }

            [DebuggerNonUserCode]
            public SelectedDeckPerWeapon(
              PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon other)
              : this()
            {
              this.weaponId_ = other.weaponId_;
              this.DeckId = other.DeckId;
              this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
            }

            [DebuggerNonUserCode]
            public PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon Clone() => new PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon(this);

            [DebuggerNonUserCode]
            public int WeaponId
            {
              get => this.weaponId_;
              set => this.weaponId_ = value;
            }

            [DebuggerNonUserCode]
            public int? DeckId
            {
              get => this.deckId_;
              set => this.deckId_ = value;
            }

            [DebuggerNonUserCode]
            public override bool Equals(object other) => this.Equals(other as PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon);

            [DebuggerNonUserCode]
            public bool Equals(
              PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon other)
            {
              if (other == null)
                return false;
              if (other == this)
                return true;
              if (this.WeaponId != other.WeaponId)
                return false;
              int? deckId1 = this.DeckId;
              int? deckId2 = other.DeckId;
              return deckId1.GetValueOrDefault() == deckId2.GetValueOrDefault() & deckId1.HasValue == deckId2.HasValue && object.Equals((object) this._unknownFields, (object) other._unknownFields);
            }

            [DebuggerNonUserCode]
            public override int GetHashCode()
            {
              int hashCode = 1;
              if (this.WeaponId != 0)
                hashCode ^= this.WeaponId.GetHashCode();
              if (this.deckId_.HasValue)
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
              if (this.WeaponId != 0)
              {
                output.WriteRawTag((byte) 8);
                output.WriteInt32(this.WeaponId);
              }
              if (this.deckId_.HasValue)
                PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon._single_deckId_codec.WriteTagAndValue(output, this.DeckId);
              if (this._unknownFields == null)
                return;
              this._unknownFields.WriteTo(output);
            }

            [DebuggerNonUserCode]
            public int CalculateSize()
            {
              int size = 0;
              if (this.WeaponId != 0)
                size += 1 + CodedOutputStream.ComputeInt32Size(this.WeaponId);
              if (this.deckId_.HasValue)
                size += PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon._single_deckId_codec.CalculateSizeWithTag(this.DeckId);
              if (this._unknownFields != null)
                size += this._unknownFields.CalculateSize();
              return size;
            }

            [DebuggerNonUserCode]
            public void MergeFrom(
              PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon other)
            {
              if (other == null)
                return;
              if (other.WeaponId != 0)
                this.WeaponId = other.WeaponId;
              if (other.deckId_.HasValue)
              {
                if (this.deckId_.HasValue)
                {
                  int? deckId = other.DeckId;
                  int num = 0;
                  if (deckId.GetValueOrDefault() == num & deckId.HasValue)
                    goto label_7;
                }
                this.DeckId = other.DeckId;
              }
label_7:
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
                    this.WeaponId = input.ReadInt32();
                    continue;
                  case 18:
                    int? nullable1 = PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon._single_deckId_codec.Read(input);
                    if (this.deckId_.HasValue)
                    {
                      int? nullable2 = nullable1;
                      int num2 = 0;
                      if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                        continue;
                    }
                    this.DeckId = nullable1;
                    continue;
                  default:
                    this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                    continue;
                }
              }
            }

            public string ToDiagnosticString() => nameof (SelectedDeckPerWeapon);
          }
        }
      }
    }
  }
}
