// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.AdminCommandsProtocol.AdminCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.AdminCommandsProtocol
{
  public sealed class AdminCmd : 
    IMessage<AdminCmd>,
    IMessage,
    IEquatable<AdminCmd>,
    IDeepCloneable<AdminCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<AdminCmd> _parser = new MessageParser<AdminCmd>((Func<AdminCmd>) (() => new AdminCmd()));
    private UnknownFieldSet _unknownFields;
    public const int IdFieldNumber = 1;
    private int id_;
    public const int GiveAllCompanionsFieldNumber = 2;
    public const int GiveAllWeaponsFieldNumber = 3;
    public const int SetWeaponLevelFieldNumber = 4;
    public const int SetAllWeaponLevelsFieldNumber = 5;
    public const int SetGenderFieldNumber = 6;
    public const int GetClusterStatisticsFieldNumber = 7;
    private object cmd_;
    private AdminCmd.CmdOneofCase cmdCase_;

    [DebuggerNonUserCode]
    public static MessageParser<AdminCmd> Parser => AdminCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => AdminCommandsProtocolReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminCmd.Descriptor;

    [DebuggerNonUserCode]
    public AdminCmd()
    {
    }

    [DebuggerNonUserCode]
    public AdminCmd(AdminCmd other)
      : this()
    {
      this.id_ = other.id_;
      switch (other.CmdCase)
      {
        case AdminCmd.CmdOneofCase.GiveAllCompanions:
          this.GiveAllCompanions = other.GiveAllCompanions;
          break;
        case AdminCmd.CmdOneofCase.GiveAllWeapons:
          this.GiveAllWeapons = other.GiveAllWeapons;
          break;
        case AdminCmd.CmdOneofCase.SetWeaponLevel:
          this.SetWeaponLevel = other.SetWeaponLevel.Clone();
          break;
        case AdminCmd.CmdOneofCase.SetAllWeaponLevels:
          this.SetAllWeaponLevels = other.SetAllWeaponLevels.Clone();
          break;
        case AdminCmd.CmdOneofCase.SetGender:
          this.SetGender = other.SetGender.Clone();
          break;
        case AdminCmd.CmdOneofCase.GetClusterStatistics:
          this.GetClusterStatistics = other.GetClusterStatistics.Clone();
          break;
      }
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public AdminCmd Clone() => new AdminCmd(this);

    [DebuggerNonUserCode]
    public int Id
    {
      get => this.id_;
      set => this.id_ = value;
    }

    [DebuggerNonUserCode]
    public bool GiveAllCompanions
    {
      get => this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllCompanions && (bool) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = AdminCmd.CmdOneofCase.GiveAllCompanions;
      }
    }

    [DebuggerNonUserCode]
    public bool GiveAllWeapons
    {
      get => this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllWeapons && (bool) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = AdminCmd.CmdOneofCase.GiveAllWeapons;
      }
    }

    [DebuggerNonUserCode]
    public AdminCmd.Types.SetLevel SetWeaponLevel
    {
      get => this.cmdCase_ != AdminCmd.CmdOneofCase.SetWeaponLevel ? (AdminCmd.Types.SetLevel) null : (AdminCmd.Types.SetLevel) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminCmd.CmdOneofCase.None : AdminCmd.CmdOneofCase.SetWeaponLevel;
      }
    }

    [DebuggerNonUserCode]
    public AdminCmd.Types.SetAllLevels SetAllWeaponLevels
    {
      get => this.cmdCase_ != AdminCmd.CmdOneofCase.SetAllWeaponLevels ? (AdminCmd.Types.SetAllLevels) null : (AdminCmd.Types.SetAllLevels) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminCmd.CmdOneofCase.None : AdminCmd.CmdOneofCase.SetAllWeaponLevels;
      }
    }

    [DebuggerNonUserCode]
    public AdminCmd.Types.SetGender SetGender
    {
      get => this.cmdCase_ != AdminCmd.CmdOneofCase.SetGender ? (AdminCmd.Types.SetGender) null : (AdminCmd.Types.SetGender) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminCmd.CmdOneofCase.None : AdminCmd.CmdOneofCase.SetGender;
      }
    }

    [DebuggerNonUserCode]
    public AdminCmd.Types.GetClusterStatistics GetClusterStatistics
    {
      get => this.cmdCase_ != AdminCmd.CmdOneofCase.GetClusterStatistics ? (AdminCmd.Types.GetClusterStatistics) null : (AdminCmd.Types.GetClusterStatistics) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminCmd.CmdOneofCase.None : AdminCmd.CmdOneofCase.GetClusterStatistics;
      }
    }

    [DebuggerNonUserCode]
    public AdminCmd.CmdOneofCase CmdCase => this.cmdCase_;

    [DebuggerNonUserCode]
    public void ClearCmd()
    {
      this.cmdCase_ = AdminCmd.CmdOneofCase.None;
      this.cmd_ = (object) null;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as AdminCmd);

    [DebuggerNonUserCode]
    public bool Equals(AdminCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.Id == other.Id && this.GiveAllCompanions == other.GiveAllCompanions && this.GiveAllWeapons == other.GiveAllWeapons && object.Equals((object) this.SetWeaponLevel, (object) other.SetWeaponLevel) && object.Equals((object) this.SetAllWeaponLevels, (object) other.SetAllWeaponLevels) && object.Equals((object) this.SetGender, (object) other.SetGender) && object.Equals((object) this.GetClusterStatistics, (object) other.GetClusterStatistics) && this.CmdCase == other.CmdCase && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.Id != 0)
        num1 ^= this.Id.GetHashCode();
      bool flag;
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllCompanions)
      {
        int num2 = num1;
        flag = this.GiveAllCompanions;
        int hashCode = flag.GetHashCode();
        num1 = num2 ^ hashCode;
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllWeapons)
      {
        int num3 = num1;
        flag = this.GiveAllWeapons;
        int hashCode = flag.GetHashCode();
        num1 = num3 ^ hashCode;
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetWeaponLevel)
        num1 ^= this.SetWeaponLevel.GetHashCode();
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetAllWeaponLevels)
        num1 ^= this.SetAllWeaponLevels.GetHashCode();
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetGender)
        num1 ^= this.SetGender.GetHashCode();
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GetClusterStatistics)
        num1 ^= this.GetClusterStatistics.GetHashCode();
      int hashCode1 = (int) ((AdminCmd.CmdOneofCase) num1 ^ this.cmdCase_);
      if (this._unknownFields != null)
        hashCode1 ^= this._unknownFields.GetHashCode();
      return hashCode1;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.Id != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.Id);
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllCompanions)
      {
        output.WriteRawTag((byte) 16);
        output.WriteBool(this.GiveAllCompanions);
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllWeapons)
      {
        output.WriteRawTag((byte) 24);
        output.WriteBool(this.GiveAllWeapons);
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetWeaponLevel)
      {
        output.WriteRawTag((byte) 34);
        output.WriteMessage((IMessage) this.SetWeaponLevel);
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetAllWeaponLevels)
      {
        output.WriteRawTag((byte) 42);
        output.WriteMessage((IMessage) this.SetAllWeaponLevels);
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetGender)
      {
        output.WriteRawTag((byte) 50);
        output.WriteMessage((IMessage) this.SetGender);
      }
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GetClusterStatistics)
      {
        output.WriteRawTag((byte) 58);
        output.WriteMessage((IMessage) this.GetClusterStatistics);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.Id != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.Id);
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllCompanions)
        size += 2;
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GiveAllWeapons)
        size += 2;
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetWeaponLevel)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SetWeaponLevel);
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetAllWeaponLevels)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SetAllWeaponLevels);
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetGender)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SetGender);
      if (this.cmdCase_ == AdminCmd.CmdOneofCase.GetClusterStatistics)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.GetClusterStatistics);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(AdminCmd other)
    {
      if (other == null)
        return;
      if (other.Id != 0)
        this.Id = other.Id;
      switch (other.CmdCase)
      {
        case AdminCmd.CmdOneofCase.GiveAllCompanions:
          this.GiveAllCompanions = other.GiveAllCompanions;
          break;
        case AdminCmd.CmdOneofCase.GiveAllWeapons:
          this.GiveAllWeapons = other.GiveAllWeapons;
          break;
        case AdminCmd.CmdOneofCase.SetWeaponLevel:
          if (this.SetWeaponLevel == null)
            this.SetWeaponLevel = new AdminCmd.Types.SetLevel();
          this.SetWeaponLevel.MergeFrom(other.SetWeaponLevel);
          break;
        case AdminCmd.CmdOneofCase.SetAllWeaponLevels:
          if (this.SetAllWeaponLevels == null)
            this.SetAllWeaponLevels = new AdminCmd.Types.SetAllLevels();
          this.SetAllWeaponLevels.MergeFrom(other.SetAllWeaponLevels);
          break;
        case AdminCmd.CmdOneofCase.SetGender:
          if (this.SetGender == null)
            this.SetGender = new AdminCmd.Types.SetGender();
          this.SetGender.MergeFrom(other.SetGender);
          break;
        case AdminCmd.CmdOneofCase.GetClusterStatistics:
          if (this.GetClusterStatistics == null)
            this.GetClusterStatistics = new AdminCmd.Types.GetClusterStatistics();
          this.GetClusterStatistics.MergeFrom(other.GetClusterStatistics);
          break;
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
          case 8:
            this.Id = input.ReadInt32();
            continue;
          case 16:
            this.GiveAllCompanions = input.ReadBool();
            continue;
          case 24:
            this.GiveAllWeapons = input.ReadBool();
            continue;
          case 34:
            AdminCmd.Types.SetLevel builder1 = new AdminCmd.Types.SetLevel();
            if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetWeaponLevel)
              builder1.MergeFrom(this.SetWeaponLevel);
            input.ReadMessage((IMessage) builder1);
            this.SetWeaponLevel = builder1;
            continue;
          case 42:
            AdminCmd.Types.SetAllLevels builder2 = new AdminCmd.Types.SetAllLevels();
            if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetAllWeaponLevels)
              builder2.MergeFrom(this.SetAllWeaponLevels);
            input.ReadMessage((IMessage) builder2);
            this.SetAllWeaponLevels = builder2;
            continue;
          case 50:
            AdminCmd.Types.SetGender builder3 = new AdminCmd.Types.SetGender();
            if (this.cmdCase_ == AdminCmd.CmdOneofCase.SetGender)
              builder3.MergeFrom(this.SetGender);
            input.ReadMessage((IMessage) builder3);
            this.SetGender = builder3;
            continue;
          case 58:
            AdminCmd.Types.GetClusterStatistics builder4 = new AdminCmd.Types.GetClusterStatistics();
            if (this.cmdCase_ == AdminCmd.CmdOneofCase.GetClusterStatistics)
              builder4.MergeFrom(this.GetClusterStatistics);
            input.ReadMessage((IMessage) builder4);
            this.GetClusterStatistics = builder4;
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (AdminCmd);

    public enum CmdOneofCase
    {
      None = 0,
      GiveAllCompanions = 2,
      GiveAllWeapons = 3,
      SetWeaponLevel = 4,
      SetAllWeaponLevels = 5,
      SetGender = 6,
      GetClusterStatistics = 7,
    }

    [DebuggerNonUserCode]
    public static class Types
    {
      public sealed class SetLevel : 
        IMessage<AdminCmd.Types.SetLevel>,
        IMessage,
        IEquatable<AdminCmd.Types.SetLevel>,
        IDeepCloneable<AdminCmd.Types.SetLevel>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminCmd.Types.SetLevel> _parser = new MessageParser<AdminCmd.Types.SetLevel>((Func<AdminCmd.Types.SetLevel>) (() => new AdminCmd.Types.SetLevel()));
        private UnknownFieldSet _unknownFields;
        public const int IdFieldNumber = 1;
        private int id_;
        public const int LevelFieldNumber = 2;
        private int level_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminCmd.Types.SetLevel> Parser => AdminCmd.Types.SetLevel._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminCmd.Descriptor.NestedTypes[0];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminCmd.Types.SetLevel.Descriptor;

        [DebuggerNonUserCode]
        public SetLevel()
        {
        }

        [DebuggerNonUserCode]
        public SetLevel(AdminCmd.Types.SetLevel other)
          : this()
        {
          this.id_ = other.id_;
          this.level_ = other.level_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminCmd.Types.SetLevel Clone() => new AdminCmd.Types.SetLevel(this);

        [DebuggerNonUserCode]
        public int Id
        {
          get => this.id_;
          set => this.id_ = value;
        }

        [DebuggerNonUserCode]
        public int Level
        {
          get => this.level_;
          set => this.level_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminCmd.Types.SetLevel);

        [DebuggerNonUserCode]
        public bool Equals(AdminCmd.Types.SetLevel other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.Id == other.Id && this.Level == other.Level && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.Id != 0)
          {
            int num2 = hashCode1;
            num1 = this.Id;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Level != 0)
          {
            int num3 = hashCode1;
            num1 = this.Level;
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
          if (this.Id != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.Id);
          }
          if (this.Level != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Level);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.Id != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Id);
          if (this.Level != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Level);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminCmd.Types.SetLevel other)
        {
          if (other == null)
            return;
          if (other.Id != 0)
            this.Id = other.Id;
          if (other.Level != 0)
            this.Level = other.Level;
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
                this.Id = input.ReadInt32();
                continue;
              case 16:
                this.Level = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (SetLevel);
      }

      public sealed class SetAllLevels : 
        IMessage<AdminCmd.Types.SetAllLevels>,
        IMessage,
        IEquatable<AdminCmd.Types.SetAllLevels>,
        IDeepCloneable<AdminCmd.Types.SetAllLevels>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminCmd.Types.SetAllLevels> _parser = new MessageParser<AdminCmd.Types.SetAllLevels>((Func<AdminCmd.Types.SetAllLevels>) (() => new AdminCmd.Types.SetAllLevels()));
        private UnknownFieldSet _unknownFields;
        public const int LevelFieldNumber = 1;
        private int level_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminCmd.Types.SetAllLevels> Parser => AdminCmd.Types.SetAllLevels._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminCmd.Descriptor.NestedTypes[1];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminCmd.Types.SetAllLevels.Descriptor;

        [DebuggerNonUserCode]
        public SetAllLevels()
        {
        }

        [DebuggerNonUserCode]
        public SetAllLevels(AdminCmd.Types.SetAllLevels other)
          : this()
        {
          this.level_ = other.level_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminCmd.Types.SetAllLevels Clone() => new AdminCmd.Types.SetAllLevels(this);

        [DebuggerNonUserCode]
        public int Level
        {
          get => this.level_;
          set => this.level_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminCmd.Types.SetAllLevels);

        [DebuggerNonUserCode]
        public bool Equals(AdminCmd.Types.SetAllLevels other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.Level == other.Level && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.Level != 0)
            hashCode ^= this.Level.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.Level != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.Level);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.Level != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Level);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminCmd.Types.SetAllLevels other)
        {
          if (other == null)
            return;
          if (other.Level != 0)
            this.Level = other.Level;
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
              this.Level = input.ReadInt32();
          }
        }

        public string ToDiagnosticString() => nameof (SetAllLevels);
      }

      public sealed class SetGender : 
        IMessage<AdminCmd.Types.SetGender>,
        IMessage,
        IEquatable<AdminCmd.Types.SetGender>,
        IDeepCloneable<AdminCmd.Types.SetGender>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminCmd.Types.SetGender> _parser = new MessageParser<AdminCmd.Types.SetGender>((Func<AdminCmd.Types.SetGender>) (() => new AdminCmd.Types.SetGender()));
        private UnknownFieldSet _unknownFields;
        public const int GenderFieldNumber = 1;
        private int gender_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminCmd.Types.SetGender> Parser => AdminCmd.Types.SetGender._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminCmd.Descriptor.NestedTypes[2];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminCmd.Types.SetGender.Descriptor;

        [DebuggerNonUserCode]
        public SetGender()
        {
        }

        [DebuggerNonUserCode]
        public SetGender(AdminCmd.Types.SetGender other)
          : this()
        {
          this.gender_ = other.gender_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminCmd.Types.SetGender Clone() => new AdminCmd.Types.SetGender(this);

        [DebuggerNonUserCode]
        public int Gender
        {
          get => this.gender_;
          set => this.gender_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminCmd.Types.SetGender);

        [DebuggerNonUserCode]
        public bool Equals(AdminCmd.Types.SetGender other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.Gender == other.Gender && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.Gender != 0)
            hashCode ^= this.Gender.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.Gender != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.Gender);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.Gender != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Gender);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminCmd.Types.SetGender other)
        {
          if (other == null)
            return;
          if (other.Gender != 0)
            this.Gender = other.Gender;
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
              this.Gender = input.ReadInt32();
          }
        }

        public string ToDiagnosticString() => nameof (SetGender);
      }

      public sealed class GetClusterStatistics : 
        IMessage<AdminCmd.Types.GetClusterStatistics>,
        IMessage,
        IEquatable<AdminCmd.Types.GetClusterStatistics>,
        IDeepCloneable<AdminCmd.Types.GetClusterStatistics>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminCmd.Types.GetClusterStatistics> _parser = new MessageParser<AdminCmd.Types.GetClusterStatistics>((Func<AdminCmd.Types.GetClusterStatistics>) (() => new AdminCmd.Types.GetClusterStatistics()));
        private UnknownFieldSet _unknownFields;
        public const int FightsCountFieldNumber = 1;
        private bool fightsCount_;
        public const int ConnectionsCountsFieldNumber = 2;
        private bool connectionsCounts_;
        public const int PlayersEntitiesCountFieldNumber = 3;
        private bool playersEntitiesCount_;
        public const int DetailedFieldNumber = 4;
        private bool detailed_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminCmd.Types.GetClusterStatistics> Parser => AdminCmd.Types.GetClusterStatistics._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminCmd.Descriptor.NestedTypes[3];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminCmd.Types.GetClusterStatistics.Descriptor;

        [DebuggerNonUserCode]
        public GetClusterStatistics()
        {
        }

        [DebuggerNonUserCode]
        public GetClusterStatistics(AdminCmd.Types.GetClusterStatistics other)
          : this()
        {
          this.fightsCount_ = other.fightsCount_;
          this.connectionsCounts_ = other.connectionsCounts_;
          this.playersEntitiesCount_ = other.playersEntitiesCount_;
          this.detailed_ = other.detailed_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminCmd.Types.GetClusterStatistics Clone() => new AdminCmd.Types.GetClusterStatistics(this);

        [DebuggerNonUserCode]
        public bool FightsCount
        {
          get => this.fightsCount_;
          set => this.fightsCount_ = value;
        }

        [DebuggerNonUserCode]
        public bool ConnectionsCounts
        {
          get => this.connectionsCounts_;
          set => this.connectionsCounts_ = value;
        }

        [DebuggerNonUserCode]
        public bool PlayersEntitiesCount
        {
          get => this.playersEntitiesCount_;
          set => this.playersEntitiesCount_ = value;
        }

        [DebuggerNonUserCode]
        public bool Detailed
        {
          get => this.detailed_;
          set => this.detailed_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminCmd.Types.GetClusterStatistics);

        [DebuggerNonUserCode]
        public bool Equals(AdminCmd.Types.GetClusterStatistics other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.FightsCount == other.FightsCount && this.ConnectionsCounts == other.ConnectionsCounts && this.PlayersEntitiesCount == other.PlayersEntitiesCount && this.Detailed == other.Detailed && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          bool flag;
          if (this.FightsCount)
          {
            int num = hashCode1;
            flag = this.FightsCount;
            int hashCode2 = flag.GetHashCode();
            hashCode1 = num ^ hashCode2;
          }
          if (this.ConnectionsCounts)
          {
            int num = hashCode1;
            flag = this.ConnectionsCounts;
            int hashCode3 = flag.GetHashCode();
            hashCode1 = num ^ hashCode3;
          }
          if (this.PlayersEntitiesCount)
          {
            int num = hashCode1;
            flag = this.PlayersEntitiesCount;
            int hashCode4 = flag.GetHashCode();
            hashCode1 = num ^ hashCode4;
          }
          if (this.Detailed)
          {
            int num = hashCode1;
            flag = this.Detailed;
            int hashCode5 = flag.GetHashCode();
            hashCode1 = num ^ hashCode5;
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
          if (this.FightsCount)
          {
            output.WriteRawTag((byte) 8);
            output.WriteBool(this.FightsCount);
          }
          if (this.ConnectionsCounts)
          {
            output.WriteRawTag((byte) 16);
            output.WriteBool(this.ConnectionsCounts);
          }
          if (this.PlayersEntitiesCount)
          {
            output.WriteRawTag((byte) 24);
            output.WriteBool(this.PlayersEntitiesCount);
          }
          if (this.Detailed)
          {
            output.WriteRawTag((byte) 32);
            output.WriteBool(this.Detailed);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.FightsCount)
            size += 2;
          if (this.ConnectionsCounts)
            size += 2;
          if (this.PlayersEntitiesCount)
            size += 2;
          if (this.Detailed)
            size += 2;
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminCmd.Types.GetClusterStatistics other)
        {
          if (other == null)
            return;
          if (other.FightsCount)
            this.FightsCount = other.FightsCount;
          if (other.ConnectionsCounts)
            this.ConnectionsCounts = other.ConnectionsCounts;
          if (other.PlayersEntitiesCount)
            this.PlayersEntitiesCount = other.PlayersEntitiesCount;
          if (other.Detailed)
            this.Detailed = other.Detailed;
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
                this.FightsCount = input.ReadBool();
                continue;
              case 16:
                this.ConnectionsCounts = input.ReadBool();
                continue;
              case 24:
                this.PlayersEntitiesCount = input.ReadBool();
                continue;
              case 32:
                this.Detailed = input.ReadBool();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (GetClusterStatistics);
      }
    }
  }
}
