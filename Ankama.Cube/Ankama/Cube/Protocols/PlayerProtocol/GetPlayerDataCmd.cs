// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.GetPlayerDataCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class GetPlayerDataCmd : 
    IMessage<GetPlayerDataCmd>,
    IMessage,
    IEquatable<GetPlayerDataCmd>,
    IDeepCloneable<GetPlayerDataCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<GetPlayerDataCmd> _parser = new MessageParser<GetPlayerDataCmd>((Func<GetPlayerDataCmd>) (() => new GetPlayerDataCmd()));
    private UnknownFieldSet _unknownFields;
    public const int AccountDataFieldNumber = 1;
    private bool accountData_;
    public const int OccupationFieldNumber = 2;
    private bool occupation_;
    public const int HeroDataFieldNumber = 3;
    private bool heroData_;
    public const int DecksFieldNumber = 4;
    private bool decks_;
    public const int CompanionsFieldNumber = 5;
    private bool companions_;
    public const int WeaponsFieldNumber = 6;
    private bool weapons_;

    [DebuggerNonUserCode]
    public static MessageParser<GetPlayerDataCmd> Parser => GetPlayerDataCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[3];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => GetPlayerDataCmd.Descriptor;

    [DebuggerNonUserCode]
    public GetPlayerDataCmd()
    {
    }

    [DebuggerNonUserCode]
    public GetPlayerDataCmd(GetPlayerDataCmd other)
      : this()
    {
      this.accountData_ = other.accountData_;
      this.occupation_ = other.occupation_;
      this.heroData_ = other.heroData_;
      this.decks_ = other.decks_;
      this.companions_ = other.companions_;
      this.weapons_ = other.weapons_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public GetPlayerDataCmd Clone() => new GetPlayerDataCmd(this);

    [DebuggerNonUserCode]
    public bool AccountData
    {
      get => this.accountData_;
      set => this.accountData_ = value;
    }

    [DebuggerNonUserCode]
    public bool Occupation
    {
      get => this.occupation_;
      set => this.occupation_ = value;
    }

    [DebuggerNonUserCode]
    public bool HeroData
    {
      get => this.heroData_;
      set => this.heroData_ = value;
    }

    [DebuggerNonUserCode]
    public bool Decks
    {
      get => this.decks_;
      set => this.decks_ = value;
    }

    [DebuggerNonUserCode]
    public bool Companions
    {
      get => this.companions_;
      set => this.companions_ = value;
    }

    [DebuggerNonUserCode]
    public bool Weapons
    {
      get => this.weapons_;
      set => this.weapons_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as GetPlayerDataCmd);

    [DebuggerNonUserCode]
    public bool Equals(GetPlayerDataCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.AccountData == other.AccountData && this.Occupation == other.Occupation && this.HeroData == other.HeroData && this.Decks == other.Decks && this.Companions == other.Companions && this.Weapons == other.Weapons && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      bool flag;
      if (this.AccountData)
      {
        int num = hashCode1;
        flag = this.AccountData;
        int hashCode2 = flag.GetHashCode();
        hashCode1 = num ^ hashCode2;
      }
      if (this.Occupation)
      {
        int num = hashCode1;
        flag = this.Occupation;
        int hashCode3 = flag.GetHashCode();
        hashCode1 = num ^ hashCode3;
      }
      if (this.HeroData)
      {
        int num = hashCode1;
        flag = this.HeroData;
        int hashCode4 = flag.GetHashCode();
        hashCode1 = num ^ hashCode4;
      }
      if (this.Decks)
      {
        int num = hashCode1;
        flag = this.Decks;
        int hashCode5 = flag.GetHashCode();
        hashCode1 = num ^ hashCode5;
      }
      if (this.Companions)
      {
        int num = hashCode1;
        flag = this.Companions;
        int hashCode6 = flag.GetHashCode();
        hashCode1 = num ^ hashCode6;
      }
      if (this.Weapons)
      {
        int num = hashCode1;
        flag = this.Weapons;
        int hashCode7 = flag.GetHashCode();
        hashCode1 = num ^ hashCode7;
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
      if (this.AccountData)
      {
        output.WriteRawTag((byte) 8);
        output.WriteBool(this.AccountData);
      }
      if (this.Occupation)
      {
        output.WriteRawTag((byte) 16);
        output.WriteBool(this.Occupation);
      }
      if (this.HeroData)
      {
        output.WriteRawTag((byte) 24);
        output.WriteBool(this.HeroData);
      }
      if (this.Decks)
      {
        output.WriteRawTag((byte) 32);
        output.WriteBool(this.Decks);
      }
      if (this.Companions)
      {
        output.WriteRawTag((byte) 40);
        output.WriteBool(this.Companions);
      }
      if (this.Weapons)
      {
        output.WriteRawTag((byte) 48);
        output.WriteBool(this.Weapons);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.AccountData)
        size += 2;
      if (this.Occupation)
        size += 2;
      if (this.HeroData)
        size += 2;
      if (this.Decks)
        size += 2;
      if (this.Companions)
        size += 2;
      if (this.Weapons)
        size += 2;
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(GetPlayerDataCmd other)
    {
      if (other == null)
        return;
      if (other.AccountData)
        this.AccountData = other.AccountData;
      if (other.Occupation)
        this.Occupation = other.Occupation;
      if (other.HeroData)
        this.HeroData = other.HeroData;
      if (other.Decks)
        this.Decks = other.Decks;
      if (other.Companions)
        this.Companions = other.Companions;
      if (other.Weapons)
        this.Weapons = other.Weapons;
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
            this.AccountData = input.ReadBool();
            continue;
          case 16:
            this.Occupation = input.ReadBool();
            continue;
          case 24:
            this.HeroData = input.ReadBool();
            continue;
          case 32:
            this.Decks = input.ReadBool();
            continue;
          case 40:
            this.Companions = input.ReadBool();
            continue;
          case 48:
            this.Weapons = input.ReadBool();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (GetPlayerDataCmd);
  }
}
