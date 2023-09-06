// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.GetFightInfoCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class GetFightInfoCmd : 
    IMessage<GetFightInfoCmd>,
    IMessage,
    IEquatable<GetFightInfoCmd>,
    IDeepCloneable<GetFightInfoCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<GetFightInfoCmd> _parser = new MessageParser<GetFightInfoCmd>((Func<GetFightInfoCmd>) (() => new GetFightInfoCmd()));
    private UnknownFieldSet _unknownFields;

    [DebuggerNonUserCode]
    public static MessageParser<GetFightInfoCmd> Parser => GetFightInfoCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[4];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => GetFightInfoCmd.Descriptor;

    [DebuggerNonUserCode]
    public GetFightInfoCmd()
    {
    }

    [DebuggerNonUserCode]
    public GetFightInfoCmd(GetFightInfoCmd other)
      : this()
    {
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public GetFightInfoCmd Clone() => new GetFightInfoCmd(this);

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as GetFightInfoCmd);

    [DebuggerNonUserCode]
    public bool Equals(GetFightInfoCmd other)
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
    public void MergeFrom(GetFightInfoCmd other)
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

    public string ToDiagnosticString() => nameof (GetFightInfoCmd);
  }
}
