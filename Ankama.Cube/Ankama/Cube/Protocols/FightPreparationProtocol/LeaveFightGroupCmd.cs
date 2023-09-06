// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightPreparationProtocol.LeaveFightGroupCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightPreparationProtocol
{
  public sealed class LeaveFightGroupCmd : 
    IMessage<LeaveFightGroupCmd>,
    IMessage,
    IEquatable<LeaveFightGroupCmd>,
    IDeepCloneable<LeaveFightGroupCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<LeaveFightGroupCmd> _parser = new MessageParser<LeaveFightGroupCmd>((Func<LeaveFightGroupCmd>) (() => new LeaveFightGroupCmd()));
    private UnknownFieldSet _unknownFields;

    [DebuggerNonUserCode]
    public static MessageParser<LeaveFightGroupCmd> Parser => LeaveFightGroupCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightPreparationProtocolReflection.Descriptor.MessageTypes[1];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => LeaveFightGroupCmd.Descriptor;

    [DebuggerNonUserCode]
    public LeaveFightGroupCmd()
    {
    }

    [DebuggerNonUserCode]
    public LeaveFightGroupCmd(LeaveFightGroupCmd other)
      : this()
    {
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public LeaveFightGroupCmd Clone() => new LeaveFightGroupCmd(this);

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as LeaveFightGroupCmd);

    [DebuggerNonUserCode]
    public bool Equals(LeaveFightGroupCmd other)
    {
      if (other == null)
        return false;
      return other == this || object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(LeaveFightGroupCmd other)
    {
      if (other == null)
        return;
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      while (input.ReadTag() != 0U)
        this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
    }

    public string ToDiagnosticString() => nameof (LeaveFightGroupCmd);
  }
}
