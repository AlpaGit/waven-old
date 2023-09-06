// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightCommonProtocol.SpellMovement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightCommonProtocol
{
  public sealed class SpellMovement : 
    IMessage<SpellMovement>,
    IMessage,
    IEquatable<SpellMovement>,
    IDeepCloneable<SpellMovement>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<SpellMovement> _parser = new MessageParser<SpellMovement>((Func<SpellMovement>) (() => new SpellMovement()));
    private UnknownFieldSet _unknownFields;
    public const int SpellFieldNumber = 1;
    private SpellInfo spell_;
    public const int FromFieldNumber = 2;
    private SpellMovementZone from_;
    public const int ToFieldNumber = 3;
    private SpellMovementZone to_;
    public const int DiscardedBecauseHandWasFullFieldNumber = 4;
    private static readonly FieldCodec<bool?> _single_discardedBecauseHandWasFull_codec = FieldCodec.ForStructWrapper<bool>(34U);
    private bool? discardedBecauseHandWasFull_;

    [DebuggerNonUserCode]
    public static MessageParser<SpellMovement> Parser => SpellMovement._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightCommonProtocolReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SpellMovement.Descriptor;

    [DebuggerNonUserCode]
    public SpellMovement()
    {
    }

    [DebuggerNonUserCode]
    public SpellMovement(SpellMovement other)
      : this()
    {
      this.spell_ = other.spell_ != null ? other.spell_.Clone() : (SpellInfo) null;
      this.from_ = other.from_;
      this.to_ = other.to_;
      this.DiscardedBecauseHandWasFull = other.DiscardedBecauseHandWasFull;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public SpellMovement Clone() => new SpellMovement(this);

    [DebuggerNonUserCode]
    public SpellInfo Spell
    {
      get => this.spell_;
      set => this.spell_ = value;
    }

    [DebuggerNonUserCode]
    public SpellMovementZone From
    {
      get => this.from_;
      set => this.from_ = value;
    }

    [DebuggerNonUserCode]
    public SpellMovementZone To
    {
      get => this.to_;
      set => this.to_ = value;
    }

    [DebuggerNonUserCode]
    public bool? DiscardedBecauseHandWasFull
    {
      get => this.discardedBecauseHandWasFull_;
      set => this.discardedBecauseHandWasFull_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as SpellMovement);

    [DebuggerNonUserCode]
    public bool Equals(SpellMovement other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      if (!object.Equals((object) this.Spell, (object) other.Spell) || this.From != other.From || this.To != other.To)
        return false;
      bool? becauseHandWasFull1 = this.DiscardedBecauseHandWasFull;
      bool? becauseHandWasFull2 = other.DiscardedBecauseHandWasFull;
      return becauseHandWasFull1.GetValueOrDefault() == becauseHandWasFull2.GetValueOrDefault() & becauseHandWasFull1.HasValue == becauseHandWasFull2.HasValue && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      if (this.spell_ != null)
        hashCode1 ^= this.Spell.GetHashCode();
      SpellMovementZone spellMovementZone;
      if (this.From != SpellMovementZone.Nowhere)
      {
        int num = hashCode1;
        spellMovementZone = this.From;
        int hashCode2 = spellMovementZone.GetHashCode();
        hashCode1 = num ^ hashCode2;
      }
      if (this.To != SpellMovementZone.Nowhere)
      {
        int num = hashCode1;
        spellMovementZone = this.To;
        int hashCode3 = spellMovementZone.GetHashCode();
        hashCode1 = num ^ hashCode3;
      }
      if (this.discardedBecauseHandWasFull_.HasValue)
        hashCode1 ^= this.DiscardedBecauseHandWasFull.GetHashCode();
      if (this._unknownFields != null)
        hashCode1 ^= this._unknownFields.GetHashCode();
      return hashCode1;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.spell_ != null)
      {
        output.WriteRawTag((byte) 10);
        output.WriteMessage((IMessage) this.Spell);
      }
      if (this.From != SpellMovementZone.Nowhere)
      {
        output.WriteRawTag((byte) 16);
        output.WriteEnum((int) this.From);
      }
      if (this.To != SpellMovementZone.Nowhere)
      {
        output.WriteRawTag((byte) 24);
        output.WriteEnum((int) this.To);
      }
      if (this.discardedBecauseHandWasFull_.HasValue)
        SpellMovement._single_discardedBecauseHandWasFull_codec.WriteTagAndValue(output, this.DiscardedBecauseHandWasFull);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.spell_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Spell);
      if (this.From != SpellMovementZone.Nowhere)
        size += 1 + CodedOutputStream.ComputeEnumSize((int) this.From);
      if (this.To != SpellMovementZone.Nowhere)
        size += 1 + CodedOutputStream.ComputeEnumSize((int) this.To);
      if (this.discardedBecauseHandWasFull_.HasValue)
        size += SpellMovement._single_discardedBecauseHandWasFull_codec.CalculateSizeWithTag(this.DiscardedBecauseHandWasFull);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(SpellMovement other)
    {
      if (other == null)
        return;
      if (other.spell_ != null)
      {
        if (this.spell_ == null)
          this.spell_ = new SpellInfo();
        this.Spell.MergeFrom(other.Spell);
      }
      if (other.From != SpellMovementZone.Nowhere)
        this.From = other.From;
      if (other.To != SpellMovementZone.Nowhere)
        this.To = other.To;
      if (other.discardedBecauseHandWasFull_.HasValue)
      {
        if (this.discardedBecauseHandWasFull_.HasValue)
        {
          bool? becauseHandWasFull = other.DiscardedBecauseHandWasFull;
          bool flag = false;
          if (becauseHandWasFull.GetValueOrDefault() == flag & becauseHandWasFull.HasValue)
            goto label_13;
        }
        this.DiscardedBecauseHandWasFull = other.DiscardedBecauseHandWasFull;
      }
label_13:
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
            if (this.spell_ == null)
              this.spell_ = new SpellInfo();
            input.ReadMessage((IMessage) this.spell_);
            continue;
          case 16:
            this.from_ = (SpellMovementZone) input.ReadEnum();
            continue;
          case 24:
            this.to_ = (SpellMovementZone) input.ReadEnum();
            continue;
          case 34:
            bool? nullable1 = SpellMovement._single_discardedBecauseHandWasFull_codec.Read(input);
            if (this.discardedBecauseHandWasFull_.HasValue)
            {
              bool? nullable2 = nullable1;
              bool flag = false;
              if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
                continue;
            }
            this.DiscardedBecauseHandWasFull = nullable1;
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (SpellMovement);
  }
}
