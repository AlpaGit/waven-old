// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.ServerProtocol.DisconnectedByServerEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.ServerProtocol
{
  public sealed class DisconnectedByServerEvent : 
    IMessage<DisconnectedByServerEvent>,
    IMessage,
    IEquatable<DisconnectedByServerEvent>,
    IDeepCloneable<DisconnectedByServerEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<DisconnectedByServerEvent> _parser = new MessageParser<DisconnectedByServerEvent>((Func<DisconnectedByServerEvent>) (() => new DisconnectedByServerEvent()));
    private UnknownFieldSet _unknownFields;
    public const int ReasonFieldNumber = 1;
    private DisconnectedByServerEvent.Types.Reason reason_;

    [DebuggerNonUserCode]
    public static MessageParser<DisconnectedByServerEvent> Parser => DisconnectedByServerEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => ServerProtocolReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => DisconnectedByServerEvent.Descriptor;

    [DebuggerNonUserCode]
    public DisconnectedByServerEvent()
    {
    }

    [DebuggerNonUserCode]
    public DisconnectedByServerEvent(DisconnectedByServerEvent other)
      : this()
    {
      this.reason_ = other.reason_;
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public DisconnectedByServerEvent Clone() => new DisconnectedByServerEvent(this);

    [DebuggerNonUserCode]
    public DisconnectedByServerEvent.Types.Reason Reason
    {
      get => this.reason_;
      set => this.reason_ = value;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as DisconnectedByServerEvent);

    [DebuggerNonUserCode]
    public bool Equals(DisconnectedByServerEvent other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return this.Reason == other.Reason && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int hashCode = 1;
      if (this.Reason != DisconnectedByServerEvent.Types.Reason.Unknown)
        hashCode ^= this.Reason.GetHashCode();
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.Reason != DisconnectedByServerEvent.Types.Reason.Unknown)
      {
        output.WriteRawTag((byte) 8);
        output.WriteEnum((int) this.Reason);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.Reason != DisconnectedByServerEvent.Types.Reason.Unknown)
        size += 1 + CodedOutputStream.ComputeEnumSize((int) this.Reason);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(DisconnectedByServerEvent other)
    {
      if (other == null)
        return;
      if (other.Reason != DisconnectedByServerEvent.Types.Reason.Unknown)
        this.Reason = other.Reason;
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
          this.reason_ = (DisconnectedByServerEvent.Types.Reason) input.ReadEnum();
      }
    }

    public string ToDiagnosticString() => nameof (DisconnectedByServerEvent);

    [DebuggerNonUserCode]
    public static class Types
    {
      public enum Reason
      {
        [OriginalName("Unknown")] Unknown,
        [OriginalName("Error")] Error,
        [OriginalName("ServerIsStopping")] ServerIsStopping,
        [OriginalName("UnableToLoadAccount")] UnableToLoadAccount,
        [OriginalName("LoggedInAgainWithSameAccount")] LoggedInAgainWithSameAccount,
      }
    }
  }
}
