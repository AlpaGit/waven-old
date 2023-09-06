// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightProtocol.CommandHandledEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightProtocol
{
  public sealed class CommandHandledEvent : 
    IMessage<CommandHandledEvent>,
    IMessage,
    IEquatable<CommandHandledEvent>,
    IDeepCloneable<CommandHandledEvent>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<CommandHandledEvent> _parser = new MessageParser<CommandHandledEvent>((Func<CommandHandledEvent>) (() => new CommandHandledEvent()));
    private UnknownFieldSet _unknownFields;

    [DebuggerNonUserCode]
    public static MessageParser<CommandHandledEvent> Parser => CommandHandledEvent._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightProtocolReflection.Descriptor.MessageTypes[18];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => CommandHandledEvent.Descriptor;

    [DebuggerNonUserCode]
    public CommandHandledEvent()
    {
    }

    [DebuggerNonUserCode]
    public CommandHandledEvent(CommandHandledEvent other)
      : this()
    {
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public CommandHandledEvent Clone() => new CommandHandledEvent(this);

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as CommandHandledEvent);

    [DebuggerNonUserCode]
    public bool Equals(CommandHandledEvent other)
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
    public void MergeFrom(CommandHandledEvent other)
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

    public string ToDiagnosticString() => nameof (CommandHandledEvent);
  }
}
