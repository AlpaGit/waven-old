// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.SelectDeckAndWeaponCmd
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
  public sealed class SelectDeckAndWeaponCmd : 
    IMessage<SelectDeckAndWeaponCmd>,
    IMessage,
    IEquatable<SelectDeckAndWeaponCmd>,
    IDeepCloneable<SelectDeckAndWeaponCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<SelectDeckAndWeaponCmd> _parser = new MessageParser<SelectDeckAndWeaponCmd>((Func<SelectDeckAndWeaponCmd>) (() => new SelectDeckAndWeaponCmd()));
    private UnknownFieldSet _unknownFields;
    public const int SelectedDecksFieldNumber = 1;
    private static readonly FieldCodec<SelectDeckInfo> _repeated_selectedDecks_codec = FieldCodec.ForMessage<SelectDeckInfo>(10U, SelectDeckInfo.Parser);
    private readonly RepeatedField<SelectDeckInfo> selectedDecks_ = new RepeatedField<SelectDeckInfo>();
    public const int SelectedWeaponFieldNumber = 2;
    private static readonly FieldCodec<int?> _single_selectedWeapon_codec = FieldCodec.ForStructWrapper<int>(18U);
    private int? selectedWeapon_;

    [DebuggerNonUserCode]
    public static MessageParser<SelectDeckAndWeaponCmd> Parser => SelectDeckAndWeaponCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[11];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SelectDeckAndWeaponCmd.Descriptor;

    [DebuggerNonUserCode]
    public SelectDeckAndWeaponCmd()
    {
    }

    [DebuggerNonUserCode]
    public SelectDeckAndWeaponCmd(SelectDeckAndWeaponCmd other)
      : this()
    {
      this.selectedDecks_ = other.selectedDecks_.Clone();
      this.SelectedWeapon = other.SelectedWeapon;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public SelectDeckAndWeaponCmd Clone() => new SelectDeckAndWeaponCmd(this);

    [DebuggerNonUserCode]
    public RepeatedField<SelectDeckInfo> SelectedDecks => this.selectedDecks_;

    [DebuggerNonUserCode]
    public int? SelectedWeapon
    {
      get => this.selectedWeapon_;
      set => this.selectedWeapon_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as SelectDeckAndWeaponCmd);

    [DebuggerNonUserCode]
    public bool Equals(SelectDeckAndWeaponCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      if (!this.selectedDecks_.Equals(other.selectedDecks_))
        return false;
      int? selectedWeapon1 = this.SelectedWeapon;
      int? selectedWeapon2 = other.SelectedWeapon;
      return selectedWeapon1.GetValueOrDefault() == selectedWeapon2.GetValueOrDefault() & selectedWeapon1.HasValue == selectedWeapon2.HasValue && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1 ^ this.selectedDecks_.GetHashCode();
      if (this.selectedWeapon_.HasValue)
        hashCode ^= this.SelectedWeapon.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      this.selectedDecks_.WriteTo(output, SelectDeckAndWeaponCmd._repeated_selectedDecks_codec);
      if (this.selectedWeapon_.HasValue)
        SelectDeckAndWeaponCmd._single_selectedWeapon_codec.WriteTagAndValue(output, this.SelectedWeapon);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0 + this.selectedDecks_.CalculateSize(SelectDeckAndWeaponCmd._repeated_selectedDecks_codec);
      if (this.selectedWeapon_.HasValue)
        size += SelectDeckAndWeaponCmd._single_selectedWeapon_codec.CalculateSizeWithTag(this.SelectedWeapon);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(SelectDeckAndWeaponCmd other)
    {
      if (other == null)
        return;
      this.selectedDecks_.Add((IEnumerable<SelectDeckInfo>) other.selectedDecks_);
      if (other.selectedWeapon_.HasValue)
      {
        if (this.selectedWeapon_.HasValue)
        {
          int? selectedWeapon = other.SelectedWeapon;
          int num = 0;
          if (selectedWeapon.GetValueOrDefault() == num & selectedWeapon.HasValue)
            goto label_5;
        }
        this.SelectedWeapon = other.SelectedWeapon;
      }
label_5:
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
            this.selectedDecks_.AddEntriesFrom(input, SelectDeckAndWeaponCmd._repeated_selectedDecks_codec);
            continue;
          case 18:
            int? nullable1 = SelectDeckAndWeaponCmd._single_selectedWeapon_codec.Read(input);
            if (this.selectedWeapon_.HasValue)
            {
              int? nullable2 = nullable1;
              int num2 = 0;
              if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                continue;
            }
            this.SelectedWeapon = nullable1;
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (SelectDeckAndWeaponCmd);
  }
}
