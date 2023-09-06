// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.HeroInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class HeroInfo : 
    IMessage<HeroInfo>,
    IMessage,
    IEquatable<HeroInfo>,
    IDeepCloneable<HeroInfo>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<HeroInfo> _parser = new MessageParser<HeroInfo>((Func<HeroInfo>) (() => new HeroInfo()));
    private UnknownFieldSet _unknownFields;
    public const int GodFieldNumber = 1;
    private int god_;
    public const int GenderFieldNumber = 2;
    private int gender_;
    public const int WeaponIdFieldNumber = 3;
    private int weaponId_;
    public const int SkinFieldNumber = 4;
    private int skin_;

    [DebuggerNonUserCode]
    public static MessageParser<HeroInfo> Parser => HeroInfo._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[2];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => HeroInfo.Descriptor;

    [DebuggerNonUserCode]
    public HeroInfo()
    {
    }

    [DebuggerNonUserCode]
    public HeroInfo(HeroInfo other)
      : this()
    {
      this.god_ = other.god_;
      this.gender_ = other.gender_;
      this.weaponId_ = other.weaponId_;
      this.skin_ = other.skin_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public HeroInfo Clone() => new HeroInfo(this);

    [DebuggerNonUserCode]
    public int God
    {
      get => this.god_;
      set => this.god_ = value;
    }

    [DebuggerNonUserCode]
    public int Gender
    {
      get => this.gender_;
      set => this.gender_ = value;
    }

    [DebuggerNonUserCode]
    public int WeaponId
    {
      get => this.weaponId_;
      set => this.weaponId_ = value;
    }

    [DebuggerNonUserCode]
    public int Skin
    {
      get => this.skin_;
      set => this.skin_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as HeroInfo);

    [DebuggerNonUserCode]
    public bool Equals(HeroInfo other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.God == other.God && this.Gender == other.Gender && this.WeaponId == other.WeaponId && this.Skin == other.Skin && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      int num1;
      if (this.God != 0)
      {
        int num2 = hashCode1;
        num1 = this.God;
        int hashCode2 = num1.GetHashCode();
        hashCode1 = num2 ^ hashCode2;
      }
      if (this.Gender != 0)
      {
        int num3 = hashCode1;
        num1 = this.Gender;
        int hashCode3 = num1.GetHashCode();
        hashCode1 = num3 ^ hashCode3;
      }
      if (this.WeaponId != 0)
      {
        int num4 = hashCode1;
        num1 = this.WeaponId;
        int hashCode4 = num1.GetHashCode();
        hashCode1 = num4 ^ hashCode4;
      }
      if (this.Skin != 0)
      {
        int num5 = hashCode1;
        num1 = this.Skin;
        int hashCode5 = num1.GetHashCode();
        hashCode1 = num5 ^ hashCode5;
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
      if (this.God != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.God);
      }
      if (this.Gender != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.Gender);
      }
      if (this.WeaponId != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.WeaponId);
      }
      if (this.Skin != 0)
      {
        output.WriteRawTag((byte) 32);
        output.WriteInt32(this.Skin);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.God != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.God);
      if (this.Gender != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.Gender);
      if (this.WeaponId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.WeaponId);
      if (this.Skin != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.Skin);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(HeroInfo other)
    {
      if (other == null)
        return;
      if (other.God != 0)
        this.God = other.God;
      if (other.Gender != 0)
        this.Gender = other.Gender;
      if (other.WeaponId != 0)
        this.WeaponId = other.WeaponId;
      if (other.Skin != 0)
        this.Skin = other.Skin;
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
            this.God = input.ReadInt32();
            continue;
          case 16:
            this.Gender = input.ReadInt32();
            continue;
          case 24:
            this.WeaponId = input.ReadInt32();
            continue;
          case 32:
            this.Skin = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (HeroInfo);
  }
}
