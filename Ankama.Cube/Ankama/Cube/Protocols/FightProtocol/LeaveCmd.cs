﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.LeaveCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class LeaveCmd : 
    IMessage<LeaveCmd>,
    IMessage,
    IEquatable<LeaveCmd>,
    IDeepCloneable<LeaveCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<LeaveCmd> _parser = new MessageParser<LeaveCmd>((Func<LeaveCmd>) (() => new LeaveCmd()));
    private UnknownFieldSet _unknownFields;

    [DebuggerNonUserCode]
    public static MessageParser<LeaveCmd> Parser => LeaveCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[3];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => LeaveCmd.Descriptor;

    [DebuggerNonUserCode]
    public LeaveCmd()
    {
    }

    [DebuggerNonUserCode]
    public LeaveCmd(LeaveCmd other)
      : this()
    {
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public LeaveCmd Clone() => new LeaveCmd(this);

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as LeaveCmd);

    [DebuggerNonUserCode]
    public bool Equals(LeaveCmd other)
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
    public void MergeFrom(LeaveCmd other)
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

    public string ToDiagnosticString() => nameof (LeaveCmd);
  }
}
