// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.InvokeCompanionCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class InvokeCompanionCmd : 
    IMessage<InvokeCompanionCmd>,
    IMessage,
    IEquatable<InvokeCompanionCmd>,
    IDeepCloneable<InvokeCompanionCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<InvokeCompanionCmd> _parser = new MessageParser<InvokeCompanionCmd>((Func<InvokeCompanionCmd>) (() => new InvokeCompanionCmd()));
    private UnknownFieldSet _unknownFields;
    public const int CompanionDefIdFieldNumber = 1;
    private int companionDefId_;
    public const int CoordsFieldNumber = 2;
    private CellCoord coords_;

    [DebuggerNonUserCode]
    public static MessageParser<InvokeCompanionCmd> Parser => InvokeCompanionCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[14];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => InvokeCompanionCmd.Descriptor;

    [DebuggerNonUserCode]
    public InvokeCompanionCmd()
    {
    }

    [DebuggerNonUserCode]
    public InvokeCompanionCmd(InvokeCompanionCmd other)
      : this()
    {
      this.companionDefId_ = other.companionDefId_;
      this.coords_ = other.coords_ != null ? other.coords_.Clone() : (CellCoord) null;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public InvokeCompanionCmd Clone() => new InvokeCompanionCmd(this);

    [DebuggerNonUserCode]
    public int CompanionDefId
    {
      get => this.companionDefId_;
      set => this.companionDefId_ = value;
    }

    [DebuggerNonUserCode]
    public CellCoord Coords
    {
      get => this.coords_;
      set => this.coords_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as InvokeCompanionCmd);

    [DebuggerNonUserCode]
    public bool Equals(InvokeCompanionCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.CompanionDefId == other.CompanionDefId && object.Equals((object) this.Coords, (object) other.Coords) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.CompanionDefId != 0)
        hashCode ^= this.CompanionDefId.GetHashCode();
      if (this.coords_ != null)
        hashCode ^= this.Coords.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.CompanionDefId != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.CompanionDefId);
      }
      if (this.coords_ != null)
      {
        output.WriteRawTag((byte) 18);
        output.WriteMessage((IMessage) this.Coords);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.CompanionDefId != 0)
        size += 1 + CodedOutputStream.ComputeInt32Size(this.CompanionDefId);
      if (this.coords_ != null)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Coords);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(InvokeCompanionCmd other)
    {
      if (other == null)
        return;
      if (other.CompanionDefId != 0)
        this.CompanionDefId = other.CompanionDefId;
      if (other.coords_ != null)
      {
        if (this.coords_ == null)
          this.coords_ = new CellCoord();
        this.Coords.MergeFrom(other.Coords);
      }
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
            this.CompanionDefId = input.ReadInt32();
            continue;
          case 18:
            if (this.coords_ == null)
              this.coords_ = new CellCoord();
            input.ReadMessage((IMessage) this.coords_);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (InvokeCompanionCmd);
  }
}
