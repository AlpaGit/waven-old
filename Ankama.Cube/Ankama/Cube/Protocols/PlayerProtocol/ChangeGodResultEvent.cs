﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.ChangeGodResultEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class ChangeGodResultEvent : 
    IMessage<ChangeGodResultEvent>,
    IMessage,
    IEquatable<ChangeGodResultEvent>,
    IDeepCloneable<ChangeGodResultEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<ChangeGodResultEvent> _parser = new MessageParser<ChangeGodResultEvent>((Func<ChangeGodResultEvent>) (() => new ChangeGodResultEvent()));
    private UnknownFieldSet _unknownFields;
    public const int ResultFieldNumber = 1;
    private CmdResult result_;

    [DebuggerNonUserCode]
    public static MessageParser<ChangeGodResultEvent> Parser => ChangeGodResultEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[6];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => ChangeGodResultEvent.Descriptor;

    [DebuggerNonUserCode]
    public ChangeGodResultEvent()
    {
    }

    [DebuggerNonUserCode]
    public ChangeGodResultEvent(ChangeGodResultEvent other)
      : this()
    {
      this.result_ = other.result_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public ChangeGodResultEvent Clone() => new ChangeGodResultEvent(this);

    [DebuggerNonUserCode]
    public CmdResult Result
    {
      get => this.result_;
      set => this.result_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as ChangeGodResultEvent);

    [DebuggerNonUserCode]
    public bool Equals(ChangeGodResultEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.Result == other.Result && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.Result != CmdResult.Failed)
        hashCode ^= this.Result.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.Result != CmdResult.Failed)
      {
        output.WriteRawTag((byte) 8);
        output.WriteEnum((int) this.Result);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.Result != CmdResult.Failed)
        size += 1 + CodedOutputStream.ComputeEnumSize((int) this.Result);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(ChangeGodResultEvent other)
    {
      if (other == null)
        return;
      if (other.Result != CmdResult.Failed)
        this.Result = other.Result;
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 8U)
          this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
        else
          this.result_ = (CmdResult) input.ReadEnum();
      }
    }

    public string ToDiagnosticString() => nameof (ChangeGodResultEvent);
  }
}
