// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.ChangeGodCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class ChangeGodCmd : 
    IMessage<ChangeGodCmd>,
    IMessage,
    IEquatable<ChangeGodCmd>,
    IDeepCloneable<ChangeGodCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<ChangeGodCmd> _parser = new MessageParser<ChangeGodCmd>((Func<ChangeGodCmd>) (() => new ChangeGodCmd()));
    private UnknownFieldSet _unknownFields;
    public const int GodFieldNumber = 1;
    private int god_;

    [DebuggerNonUserCode]
    public static MessageParser<ChangeGodCmd> Parser => ChangeGodCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[5];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => ChangeGodCmd.Descriptor;

    [DebuggerNonUserCode]
    public ChangeGodCmd()
    {
    }

    [DebuggerNonUserCode]
    public ChangeGodCmd(ChangeGodCmd other)
      : this()
    {
      this.god_ = other.god_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public ChangeGodCmd Clone() => new ChangeGodCmd(this);

    [DebuggerNonUserCode]
    public int God
    {
      get => this.god_;
      set => this.god_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as ChangeGodCmd);

    [DebuggerNonUserCode]
    public bool Equals(ChangeGodCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.God == other.God && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.God != 0)
        hashCode ^= this.God.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.God != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.God);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.God != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.God);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(ChangeGodCmd other)
    {
      if (other == null)
        return;
      if (other.God != 0)
        this.God = other.God;
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
          this.God = input.ReadInt32();
      }
    }

    public string ToDiagnosticString() => nameof (ChangeGodCmd);
  }
}
