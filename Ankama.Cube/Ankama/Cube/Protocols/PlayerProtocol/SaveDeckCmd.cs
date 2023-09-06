// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.PlayerProtocol.SaveDeckCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.PlayerProtocol
{
  public sealed class SaveDeckCmd : 
    IMessage<SaveDeckCmd>,
    IMessage,
    IEquatable<SaveDeckCmd>,
    IDeepCloneable<SaveDeckCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<SaveDeckCmd> _parser = new MessageParser<SaveDeckCmd>((Func<SaveDeckCmd>) (() => new SaveDeckCmd()));
    private UnknownFieldSet _unknownFields;
    public const int InfoFieldNumber = 1;
    private DeckInfo info_;

    [DebuggerNonUserCode]
    public static MessageParser<SaveDeckCmd> Parser => SaveDeckCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => PlayerProtocolReflection.Descriptor.MessageTypes[7];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => SaveDeckCmd.Descriptor;

    [DebuggerNonUserCode]
    public SaveDeckCmd()
    {
    }

    [DebuggerNonUserCode]
    public SaveDeckCmd(SaveDeckCmd other)
      : this()
    {
      this.info_ = other.info_ != null ? other.info_.Clone() : (DeckInfo) null;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public SaveDeckCmd Clone() => new SaveDeckCmd(this);

    [DebuggerNonUserCode]
    public DeckInfo Info
    {
      get => this.info_;
      set => this.info_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as SaveDeckCmd);

    [DebuggerNonUserCode]
    public bool Equals(SaveDeckCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return object.Equals((object) this.Info, (object) other.Info) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.info_ != null)
        hashCode ^= this.Info.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.info_ != null)
      {
        output.WriteRawTag((byte) 10);
        output.WriteMessage((IMessage) this.Info);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.info_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Info);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(SaveDeckCmd other)
    {
      if (other == null)
        return;
      if (other.info_ != null)
      {
        if (this.info_ == null)
          this.info_ = new DeckInfo();
        this.Info.MergeFrom(other.Info);
      }
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
        {
          this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
        }
        else
        {
          if (this.info_ == null)
            this.info_ = new DeckInfo();
          input.ReadMessage((IMessage) this.info_);
        }
      }
    }

    public string ToDiagnosticString() => nameof (SaveDeckCmd);
  }
}
