// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.AdminCommandsProtocol.AdminCmdResultEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.AdminCommandsProtocol
{
  public sealed class AdminCmdResultEvent : 
    IMessage<AdminCmdResultEvent>,
    IMessage,
    IEquatable<AdminCmdResultEvent>,
    IDeepCloneable<AdminCmdResultEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<AdminCmdResultEvent> _parser = new MessageParser<AdminCmdResultEvent>((Func<AdminCmdResultEvent>) (() => new AdminCmdResultEvent()));
    private UnknownFieldSet _unknownFields;
    public const int IdFieldNumber = 1;
    private int id_;
    public const int SuccessFieldNumber = 2;
    private bool success_;
    public const int ResultFieldNumber = 3;
    private string result_ = "";

    [DebuggerNonUserCode]
    public static MessageParser<AdminCmdResultEvent> Parser => AdminCmdResultEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => AdminCommandsProtocolReflection.Descriptor.MessageTypes[1];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminCmdResultEvent.Descriptor;

    [DebuggerNonUserCode]
    public AdminCmdResultEvent()
    {
    }

    [DebuggerNonUserCode]
    public AdminCmdResultEvent(AdminCmdResultEvent other)
      : this()
    {
      this.id_ = other.id_;
      this.success_ = other.success_;
      this.result_ = other.result_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public AdminCmdResultEvent Clone() => new AdminCmdResultEvent(this);

    [DebuggerNonUserCode]
    public int Id
    {
      get => this.id_;
      set => this.id_ = value;
    }

    [DebuggerNonUserCode]
    public bool Success
    {
      get => this.success_;
      set => this.success_ = value;
    }

    [DebuggerNonUserCode]
    public string Result
    {
      get => this.result_;
      set => this.result_ = ProtoPreconditions.CheckNotNull<string>(value, nameof (value));
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as AdminCmdResultEvent);

    [DebuggerNonUserCode]
    public bool Equals(AdminCmdResultEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.Id == other.Id && this.Success == other.Success && !(this.Result != other.Result) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.Id != 0)
        hashCode ^= this.Id.GetHashCode();
      if (this.Success)
        hashCode ^= this.Success.GetHashCode();
      if (this.Result.Length != 0)
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
      if (this.Id != 0)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt32(this.Id);
      }
      if (this.Success)
      {
        output.WriteRawTag((byte) 16);
        output.WriteBool(this.Success);
      }
      if (this.Result.Length != 0)
      {
        output.WriteRawTag((byte) 26);
        output.WriteString(this.Result);
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
      if (this.Success)
        size += 2;
      if (this.Result.Length != 0)
        size += 1 + CodedOutputStream.ComputeStringSize(this.Result);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(AdminCmdResultEvent other)
    {
      if (other == null)
        return;
      if (other.Id != 0)
        this.Id = other.Id;
      if (other.Success)
        this.Success = other.Success;
      if (other.Result.Length != 0)
        this.Result = other.Result;
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
            this.Id = input.ReadInt32();
            continue;
          case 16:
            this.Success = input.ReadBool();
            continue;
          case 26:
            this.Result = input.ReadString();
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (AdminCmdResultEvent);
  }
}
