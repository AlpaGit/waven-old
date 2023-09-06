// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.SelectDeckInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class SelectDeckInfo : 
    IMessage<SelectDeckInfo>,
    IMessage,
    IEquatable<SelectDeckInfo>,
    IDeepCloneable<SelectDeckInfo>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<SelectDeckInfo> _parser = new MessageParser<SelectDeckInfo>((Func<SelectDeckInfo>) (() => new SelectDeckInfo()));
    private UnknownFieldSet _unknownFields;
    public const int WeaponIdFieldNumber = 1;
    private int weaponId_;
    public const int DeckIdFieldNumber = 2;
    private static readonly FieldCodec<int?> _single_deckId_codec = FieldCodec.ForStructWrapper<int>(18U);
    private int? deckId_;

    [DebuggerNonUserCode]
    public static MessageParser<SelectDeckInfo> Parser => SelectDeckInfo._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[12];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SelectDeckInfo.Descriptor;

    [DebuggerNonUserCode]
    public SelectDeckInfo()
    {
    }

    [DebuggerNonUserCode]
    public SelectDeckInfo(SelectDeckInfo other)
      : this()
    {
      this.weaponId_ = other.weaponId_;
      this.DeckId = other.DeckId;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public SelectDeckInfo Clone() => new SelectDeckInfo(this);

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
    public override bool Equals(object other) => this.Equals(other as SelectDeckInfo);

    [DebuggerNonUserCode]
    public bool Equals(SelectDeckInfo other)
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
        SelectDeckInfo._single_deckId_codec.WriteTagAndValue(output, this.DeckId);
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
        size += SelectDeckInfo._single_deckId_codec.CalculateSizeWithTag(this.DeckId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(SelectDeckInfo other)
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
            int? nullable1 = SelectDeckInfo._single_deckId_codec.Read(input);
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

    public string ToDiagnosticString() => nameof (SelectDeckInfo);
  }
}
