// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.FightSnapshotSpell
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class FightSnapshotSpell : 
    IMessage<FightSnapshotSpell>,
    IMessage,
    IEquatable<FightSnapshotSpell>,
    IDeepCloneable<FightSnapshotSpell>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightSnapshotSpell> _parser = new MessageParser<FightSnapshotSpell>((Func<FightSnapshotSpell>) (() => new FightSnapshotSpell()));
    private UnknownFieldSet _unknownFields;
    public const int SpellDefIdFieldNumber = 1;
    private int spellDefId_;
    public const int SpellInstanceIdFieldNumber = 2;
    private int spellInstanceId_;

    [DebuggerNonUserCode]
    public static MessageParser<FightSnapshotSpell> Parser => FightSnapshotSpell._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[8];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightSnapshotSpell.Descriptor;

    [DebuggerNonUserCode]
    public FightSnapshotSpell()
    {
    }

    [DebuggerNonUserCode]
    public FightSnapshotSpell(FightSnapshotSpell other)
      : this()
    {
      this.spellDefId_ = other.spellDefId_;
      this.spellInstanceId_ = other.spellInstanceId_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightSnapshotSpell Clone() => new FightSnapshotSpell(this);

    [DebuggerNonUserCode]
    public int SpellDefId
    {
      get => this.spellDefId_;
      set => this.spellDefId_ = value;
    }

    [DebuggerNonUserCode]
    public int SpellInstanceId
    {
      get => this.spellInstanceId_;
      set => this.spellInstanceId_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightSnapshotSpell);

    [DebuggerNonUserCode]
    public bool Equals(FightSnapshotSpell other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.SpellDefId == other.SpellDefId && this.SpellInstanceId == other.SpellInstanceId && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode1 = 1;
      int num1;
      if (this.SpellDefId != 0)
      {
        int num2 = hashCode1;
        num1 = this.SpellDefId;
        int hashCode2 = num1.GetHashCode();
        hashCode1 = num2 ^ hashCode2;
      }
      if (this.SpellInstanceId != 0)
      {
        int num3 = hashCode1;
        num1 = this.SpellInstanceId;
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
      if (this.SpellDefId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.SpellDefId);
      }
      if (this.SpellInstanceId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteInt32(this.SpellInstanceId);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.SpellDefId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.SpellDefId);
      if (this.SpellInstanceId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.SpellInstanceId);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightSnapshotSpell other)
    {
      if (other == null)
        return;
      if (other.SpellDefId != 0)
        this.SpellDefId = other.SpellDefId;
      if (other.SpellInstanceId != 0)
        this.SpellInstanceId = other.SpellInstanceId;
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
            this.SpellDefId = input.ReadInt32();
            continue;
          case 16:
            this.SpellInstanceId = input.ReadInt32();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightSnapshotSpell);
  }
}
