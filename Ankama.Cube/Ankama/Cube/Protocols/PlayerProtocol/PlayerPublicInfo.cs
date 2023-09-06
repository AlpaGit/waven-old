// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.PlayerPublicInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class PlayerPublicInfo : 
    IMessage<PlayerPublicInfo>,
    IMessage,
    IEquatable<PlayerPublicInfo>,
    IDeepCloneable<PlayerPublicInfo>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<PlayerPublicInfo> _parser = new MessageParser<PlayerPublicInfo>((Func<PlayerPublicInfo>) (() => new PlayerPublicInfo()));
    private UnknownFieldSet _unknownFields;
    public const int NicknameFieldNumber = 1;
    private string nickname_ = "";
    public const int GodFieldNumber = 2;
    private int god_;
    public const int WeaponIdFieldNumber = 3;
    private int weaponId_;
    public const int SkinFieldNumber = 4;
    private int skin_;

    [DebuggerNonUserCode]
    public static MessageParser<PlayerPublicInfo> Parser => PlayerPublicInfo._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlayerPublicInfo.Descriptor;

    [DebuggerNonUserCode]
    public PlayerPublicInfo()
    {
    }

    [DebuggerNonUserCode]
    public PlayerPublicInfo(PlayerPublicInfo other)
      : this()
    {
      this.nickname_ = other.nickname_;
      this.god_ = other.god_;
      this.weaponId_ = other.weaponId_;
      this.skin_ = other.skin_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public PlayerPublicInfo Clone() => new PlayerPublicInfo(this);

    [DebuggerNonUserCode]
    public string Nickname
    {
      get => this.nickname_;
      set => this.nickname_ = ProtoPreconditions.CheckNotNull<string>(value, nameof (value));
    }

    [DebuggerNonUserCode]
    public int God
    {
      get => this.god_;
      set => this.god_ = value;
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
    public override bool Equals(object other) => this.Equals(other as PlayerPublicInfo);

    [DebuggerNonUserCode]
    public bool Equals(PlayerPublicInfo other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return !(this.Nickname != other.Nickname) && this.God == other.God && this.WeaponId == other.WeaponId && this.Skin == other.Skin && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.Nickname.Length != 0)
        hashCode ^= this.Nickname.GetHashCode();
      if (this.God != 0)
        hashCode ^= this.God.GetHashCode();
      if (this.WeaponId != 0)
        hashCode ^= this.WeaponId.GetHashCode();
      if (this.Skin != 0)
        hashCode ^= this.Skin.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.Nickname.Length != 0)
      {
        output.WriteRawTag((byte) 10);
        output.WriteString(this.Nickname);
      }
      if (this.God != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.God);
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
      if (this.Nickname.Length != 0)
        size += 1 + CodedOutputStream.ComputeStringSize(this.Nickname);
      if (this.God != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.God);
      if (this.WeaponId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.WeaponId);
      if (this.Skin != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.Skin);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(PlayerPublicInfo other)
    {
      if (other == null)
        return;
      if (other.Nickname.Length != 0)
        this.Nickname = other.Nickname;
      if (other.God != 0)
        this.God = other.God;
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
          case 10:
            this.Nickname = input.ReadString();
            continue;
          case 16:
            this.God = input.ReadInt32();
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

    public string ToDiagnosticString() => nameof (PlayerPublicInfo);
  }
}
