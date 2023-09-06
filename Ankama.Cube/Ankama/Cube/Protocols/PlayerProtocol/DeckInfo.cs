// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.DeckInfo
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
  public sealed class DeckInfo : 
    IMessage<DeckInfo>,
    IMessage,
    IEquatable<DeckInfo>,
    IDeepCloneable<DeckInfo>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<DeckInfo> _parser = new MessageParser<DeckInfo>((Func<DeckInfo>) (() => new DeckInfo()));
    private UnknownFieldSet _unknownFields;
    public const int IdFieldNumber = 1;
    private static readonly FieldCodec<int?> _single_id_codec = FieldCodec.ForStructWrapper<int>(10U);
    private int? id_;
    public const int NameFieldNumber = 2;
    private string name_ = "";
    public const int GodFieldNumber = 3;
    private int god_;
    public const int WeaponFieldNumber = 4;
    private int weapon_;
    public const int CompanionsFieldNumber = 5;
    private static readonly FieldCodec<int> _repeated_companions_codec = FieldCodec.ForInt32(42U);
    private readonly RepeatedField<int> companions_ = new RepeatedField<int>();
    public const int SpellsFieldNumber = 6;
    private static readonly FieldCodec<int> _repeated_spells_codec = FieldCodec.ForInt32(50U);
    private readonly RepeatedField<int> spells_ = new RepeatedField<int>();
    public const int SummoningsFieldNumber = 7;
    private static readonly FieldCodec<int> _repeated_summonings_codec = FieldCodec.ForInt32(58U);
    private readonly RepeatedField<int> summonings_ = new RepeatedField<int>();

    [DebuggerNonUserCode]
    public static MessageParser<DeckInfo> Parser => DeckInfo._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[1];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => DeckInfo.Descriptor;

    [DebuggerNonUserCode]
    public DeckInfo()
    {
    }

    [DebuggerNonUserCode]
    public DeckInfo(DeckInfo other)
      : this()
    {
      this.Id = other.Id;
      this.name_ = other.name_;
      this.god_ = other.god_;
      this.weapon_ = other.weapon_;
      this.companions_ = other.companions_.Clone();
      this.spells_ = other.spells_.Clone();
      this.summonings_ = other.summonings_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public DeckInfo Clone() => new DeckInfo(this);

    [DebuggerNonUserCode]
    public int? Id
    {
      get => this.id_;
      set => this.id_ = value;
    }

    [DebuggerNonUserCode]
    public string Name
    {
      get => this.name_;
      set => this.name_ = ProtoPreconditions.CheckNotNull<string>(value, nameof (value));
    }

    [DebuggerNonUserCode]
    public int God
    {
      get => this.god_;
      set => this.god_ = value;
    }

    [DebuggerNonUserCode]
    public int Weapon
    {
      get => this.weapon_;
      set => this.weapon_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<int> Companions => this.companions_;

    [DebuggerNonUserCode]
    public RepeatedField<int> Spells => this.spells_;

    [DebuggerNonUserCode]
    public RepeatedField<int> Summonings => this.summonings_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as DeckInfo);

    [DebuggerNonUserCode]
    public bool Equals(DeckInfo other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      int? id1 = this.Id;
      int? id2 = other.Id;
      return id1.GetValueOrDefault() == id2.GetValueOrDefault() & id1.HasValue == id2.HasValue && !(this.Name != other.Name) && this.God == other.God && this.Weapon == other.Weapon && this.companions_.Equals(other.companions_) && this.spells_.Equals(other.spells_) && this.summonings_.Equals(other.summonings_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1;
      if (this.id_.HasValue)
        num ^= this.Id.GetHashCode();
      if (this.Name.Length != 0)
        num ^= this.Name.GetHashCode();
      if (this.God != 0)
        num ^= this.God.GetHashCode();
      if (this.Weapon != 0)
        num ^= this.Weapon.GetHashCode();
      int hashCode = num ^ this.companions_.GetHashCode() ^ this.spells_.GetHashCode() ^ this.summonings_.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.id_.HasValue)
        DeckInfo._single_id_codec.WriteTagAndValue(output, this.Id);
      if (this.Name.Length != 0)
      {
        output.WriteRawTag((byte) 18);
        output.WriteString(this.Name);
      }
      if (this.God != 0)
      {
        output.WriteRawTag((byte) 24);
        output.WriteInt32(this.God);
      }
      if (this.Weapon != 0)
      {
        output.WriteRawTag((byte) 32);
        output.WriteInt32(this.Weapon);
      }
      this.companions_.WriteTo(output, DeckInfo._repeated_companions_codec);
      this.spells_.WriteTo(output, DeckInfo._repeated_spells_codec);
      this.summonings_.WriteTo(output, DeckInfo._repeated_summonings_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0;
      if (this.id_.HasValue)
        num += DeckInfo._single_id_codec.CalculateSizeWithTag(this.Id);
      if (this.Name.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.Name);
      if (this.God != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.God);
      if (this.Weapon != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Weapon);
      int size = num + this.companions_.CalculateSize(DeckInfo._repeated_companions_codec) + this.spells_.CalculateSize(DeckInfo._repeated_spells_codec) + this.summonings_.CalculateSize(DeckInfo._repeated_summonings_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(DeckInfo other)
    {
      if (other == null)
        return;
      if (other.id_.HasValue)
      {
        if (this.id_.HasValue)
        {
          int? id = other.Id;
          int num = 0;
          if (id.GetValueOrDefault() == num & id.HasValue)
            goto label_5;
        }
        this.Id = other.Id;
      }
label_5:
      if (other.Name.Length != 0)
        this.Name = other.Name;
      if (other.God != 0)
        this.God = other.God;
      if (other.Weapon != 0)
        this.Weapon = other.Weapon;
      this.companions_.Add((IEnumerable<int>) other.companions_);
      this.spells_.Add((IEnumerable<int>) other.spells_);
      this.summonings_.Add((IEnumerable<int>) other.summonings_);
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
            int? nullable1 = DeckInfo._single_id_codec.Read(input);
            if (this.id_.HasValue)
            {
              int? nullable2 = nullable1;
              int num2 = 0;
              if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                continue;
            }
            this.Id = nullable1;
            continue;
          case 18:
            this.Name = input.ReadString();
            continue;
          case 24:
            this.God = input.ReadInt32();
            continue;
          case 32:
            this.Weapon = input.ReadInt32();
            continue;
          case 40:
          case 42:
            this.companions_.AddEntriesFrom(input, DeckInfo._repeated_companions_codec);
            continue;
          case 48:
          case 50:
            this.spells_.AddEntriesFrom(input, DeckInfo._repeated_spells_codec);
            continue;
          case 56:
          case 58:
            this.summonings_.AddEntriesFrom(input, DeckInfo._repeated_summonings_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (DeckInfo);
  }
}
