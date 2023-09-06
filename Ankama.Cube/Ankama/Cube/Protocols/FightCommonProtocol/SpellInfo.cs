// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightCommonProtocol.SpellInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightCommonProtocol
{
  public sealed class SpellInfo : 
    IMessage<SpellInfo>,
    IMessage,
    IEquatable<SpellInfo>,
    IDeepCloneable<SpellInfo>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<SpellInfo> _parser = new MessageParser<SpellInfo>((Func<SpellInfo>) (() => new SpellInfo()));
    private UnknownFieldSet _unknownFields;
    public const int SpellInstanceIdFieldNumber = 1;
    private int spellInstanceId_;
    public const int SpellDefinitionIdFieldNumber = 2;
    private static readonly FieldCodec<int?> _single_spellDefinitionId_codec = FieldCodec.ForStructWrapper<int>(18U);
    private int? spellDefinitionId_;
    public const int SpellLevelFieldNumber = 3;
    private static readonly FieldCodec<int?> _single_spellLevel_codec = FieldCodec.ForStructWrapper<int>(26U);
    private int? spellLevel_;

    [DebuggerNonUserCode]
    public static MessageParser<SpellInfo> Parser => SpellInfo._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightCommonProtocolReflection.Descriptor.MessageTypes[1];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SpellInfo.Descriptor;

    [DebuggerNonUserCode]
    public SpellInfo()
    {
    }

    [DebuggerNonUserCode]
    public SpellInfo(SpellInfo other)
      : this()
    {
      this.spellInstanceId_ = other.spellInstanceId_;
      this.SpellDefinitionId = other.SpellDefinitionId;
      this.SpellLevel = other.SpellLevel;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public SpellInfo Clone() => new SpellInfo(this);

    [DebuggerNonUserCode]
    public int SpellInstanceId
    {
      get => this.spellInstanceId_;
      set => this.spellInstanceId_ = value;
    }

    [DebuggerNonUserCode]
    public int? SpellDefinitionId
    {
      get => this.spellDefinitionId_;
      set => this.spellDefinitionId_ = value;
    }

    [DebuggerNonUserCode]
    public int? SpellLevel
    {
      get => this.spellLevel_;
      set => this.spellLevel_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as SpellInfo);

    [DebuggerNonUserCode]
    public bool Equals(SpellInfo other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      if (this.SpellInstanceId != other.SpellInstanceId)
        return false;
      int? nullable1 = this.SpellDefinitionId;
      int? nullable2 = other.SpellDefinitionId;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return false;
      nullable2 = this.SpellLevel;
      nullable1 = other.SpellLevel;
      return nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      if (this.SpellInstanceId != 0)
        hashCode1 ^= this.SpellInstanceId.GetHashCode();
      int? nullable;
      if (this.spellDefinitionId_.HasValue)
      {
        int num = hashCode1;
        nullable = this.SpellDefinitionId;
        int hashCode2 = nullable.GetHashCode();
        hashCode1 = num ^ hashCode2;
      }
      if (this.spellLevel_.HasValue)
      {
        int num = hashCode1;
        nullable = this.SpellLevel;
        int hashCode3 = nullable.GetHashCode();
        hashCode1 = num ^ hashCode3;
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
      if (this.SpellInstanceId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.SpellInstanceId);
      }
      if (this.spellDefinitionId_.HasValue)
        SpellInfo._single_spellDefinitionId_codec.WriteTagAndValue(output, this.SpellDefinitionId);
      if (this.spellLevel_.HasValue)
        SpellInfo._single_spellLevel_codec.WriteTagAndValue(output, this.SpellLevel);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.SpellInstanceId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.SpellInstanceId);
      if (this.spellDefinitionId_.HasValue)
        size += SpellInfo._single_spellDefinitionId_codec.CalculateSizeWithTag(this.SpellDefinitionId);
      if (this.spellLevel_.HasValue)
        size += SpellInfo._single_spellLevel_codec.CalculateSizeWithTag(this.SpellLevel);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(SpellInfo other)
    {
      if (other == null)
        return;
      if (other.SpellInstanceId != 0)
        this.SpellInstanceId = other.SpellInstanceId;
      int? nullable;
      if (other.spellDefinitionId_.HasValue)
      {
        if (this.spellDefinitionId_.HasValue)
        {
          nullable = other.SpellDefinitionId;
          int num = 0;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            goto label_7;
        }
        this.SpellDefinitionId = other.SpellDefinitionId;
      }
label_7:
      if (other.spellLevel_.HasValue)
      {
        if (this.spellLevel_.HasValue)
        {
          nullable = other.SpellLevel;
          int num = 0;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            goto label_11;
        }
        this.SpellLevel = other.SpellLevel;
      }
label_11:
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
            this.SpellInstanceId = input.ReadInt32();
            continue;
          case 18:
            int? nullable1 = SpellInfo._single_spellDefinitionId_codec.Read(input);
            if (this.spellDefinitionId_.HasValue)
            {
              int? nullable2 = nullable1;
              int num2 = 0;
              if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                continue;
            }
            this.SpellDefinitionId = nullable1;
            continue;
          case 26:
            int? nullable3 = SpellInfo._single_spellLevel_codec.Read(input);
            if (this.spellLevel_.HasValue)
            {
              int? nullable4 = nullable3;
              int num3 = 0;
              if (nullable4.GetValueOrDefault() == num3 & nullable4.HasValue)
                continue;
            }
            this.SpellLevel = nullable3;
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (SpellInfo);
  }
}
