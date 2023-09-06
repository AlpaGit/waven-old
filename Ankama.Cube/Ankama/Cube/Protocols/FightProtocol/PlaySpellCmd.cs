// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.PlaySpellCmd
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
  public sealed class PlaySpellCmd : 
    IMessage<PlaySpellCmd>,
    IMessage,
    IEquatable<PlaySpellCmd>,
    IDeepCloneable<PlaySpellCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<PlaySpellCmd> _parser = new MessageParser<PlaySpellCmd>((Func<PlaySpellCmd>) (() => new PlaySpellCmd()));
    private UnknownFieldSet _unknownFields;
    public const int SpellIdFieldNumber = 1;
    private int spellId_;
    public const int CastTargetsFieldNumber = 2;
    private static readonly FieldCodec<CastTarget> _repeated_castTargets_codec = FieldCodec.ForMessage<CastTarget>(18U, CastTarget.Parser);
    private readonly RepeatedField<CastTarget> castTargets_ = new RepeatedField<CastTarget>();

    [DebuggerNonUserCode]
    public static MessageParser<PlaySpellCmd> Parser => PlaySpellCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[13];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => PlaySpellCmd.Descriptor;

    [DebuggerNonUserCode]
    public PlaySpellCmd()
    {
    }

    [DebuggerNonUserCode]
    public PlaySpellCmd(PlaySpellCmd other)
      : this()
    {
      this.spellId_ = other.spellId_;
      this.castTargets_ = other.castTargets_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public PlaySpellCmd Clone() => new PlaySpellCmd(this);

    [DebuggerNonUserCode]
    public int SpellId
    {
      get => this.spellId_;
      set => this.spellId_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<CastTarget> CastTargets => this.castTargets_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as PlaySpellCmd);

    [DebuggerNonUserCode]
    public bool Equals(PlaySpellCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.SpellId == other.SpellId && this.castTargets_.Equals(other.castTargets_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1;
      if (this.SpellId != 0)
        num ^= this.SpellId.GetHashCode();
      int hashCode = num ^ this.castTargets_.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.SpellId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.SpellId);
      }
      this.castTargets_.WriteTo(output, PlaySpellCmd._repeated_castTargets_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0;
      if (this.SpellId != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.SpellId);
      int size = num + this.castTargets_.CalculateSize(PlaySpellCmd._repeated_castTargets_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(PlaySpellCmd other)
    {
      if (other == null)
        return;
      if (other.SpellId != 0)
        this.SpellId = other.SpellId;
      this.castTargets_.Add((IEnumerable<CastTarget>) other.castTargets_);
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
            this.SpellId = input.ReadInt32();
            continue;
          case 18:
            this.castTargets_.AddEntriesFrom(input, PlaySpellCmd._repeated_castTargets_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (PlaySpellCmd);
  }
}
