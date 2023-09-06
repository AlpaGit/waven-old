// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.ResignCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class ResignCmd : 
    IMessage<ResignCmd>,
    IMessage,
    IEquatable<ResignCmd>,
    IDeepCloneable<ResignCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<ResignCmd> _parser = new MessageParser<ResignCmd>((Func<ResignCmd>) (() => new ResignCmd()));
    private UnknownFieldSet _unknownFields;

    [DebuggerNonUserCode]
    public static MessageParser<ResignCmd> Parser => ResignCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[2];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => ResignCmd.Descriptor;

    [DebuggerNonUserCode]
    public ResignCmd()
    {
    }

    [DebuggerNonUserCode]
    public ResignCmd(ResignCmd other)
      : this()
    {
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public ResignCmd Clone() => new ResignCmd(this);

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as ResignCmd);

    [DebuggerNonUserCode]
    public bool Equals(ResignCmd other)
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
    public void MergeFrom(ResignCmd other)
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

    public string ToDiagnosticString() => nameof (ResignCmd);
  }
}
