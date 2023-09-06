// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.RemoveDeckCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class RemoveDeckCmd : 
    IMessage<RemoveDeckCmd>,
    IMessage,
    IEquatable<RemoveDeckCmd>,
    IDeepCloneable<RemoveDeckCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<RemoveDeckCmd> _parser = new MessageParser<RemoveDeckCmd>((Func<RemoveDeckCmd>) (() => new RemoveDeckCmd()));
    private UnknownFieldSet _unknownFields;
    public const int IdFieldNumber = 1;
    private int id_;

    [DebuggerNonUserCode]
    public static MessageParser<RemoveDeckCmd> Parser => RemoveDeckCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[9];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => RemoveDeckCmd.Descriptor;

    [DebuggerNonUserCode]
    public RemoveDeckCmd()
    {
    }

    [DebuggerNonUserCode]
    public RemoveDeckCmd(RemoveDeckCmd other)
      : this()
    {
      this.id_ = other.id_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public RemoveDeckCmd Clone() => new RemoveDeckCmd(this);

    [DebuggerNonUserCode]
    public int Id
    {
      get => this.id_;
      set => this.id_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as RemoveDeckCmd);

    [DebuggerNonUserCode]
    public bool Equals(RemoveDeckCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.Id == other.Id && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.Id != 0)
        hashCode ^= this.Id.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.Id != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.Id);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.Id != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.Id);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(RemoveDeckCmd other)
    {
      if (other == null)
        return;
      if (other.Id != 0)
        this.Id = other.Id;
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
          this.Id = input.ReadInt32();
      }
    }

    public string ToDiagnosticString() => nameof (RemoveDeckCmd);
  }
}
