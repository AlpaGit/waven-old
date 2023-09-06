// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TMultiplexedProcessor
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System.Collections.Generic;
using System.IO;

namespace Thrift.Protocol
{
  public class TMultiplexedProcessor : TProcessor
  {
    private Dictionary<string, TProcessor> ServiceProcessorMap = new Dictionary<string, TProcessor>();

    public void RegisterProcessor(string serviceName, TProcessor processor) => this.ServiceProcessorMap.Add(serviceName, processor);

    private void Fail(
      TProtocol oprot,
      TMessage message,
      TApplicationException.ExceptionType extype,
      string etxt)
    {
      TApplicationException tapplicationException = new TApplicationException(extype, etxt);
      TMessage message1 = new TMessage(message.Name, TMessageType.Exception, message.SeqID);
      oprot.WriteMessageBegin(message1);
      tapplicationException.Write(oprot);
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

    public bool Process(TProtocol iprot, TProtocol oprot)
    {
      try
      {
        TMessage message = iprot.ReadMessageBegin();
        if (message.Type != TMessageType.Call && message.Type != TMessageType.Oneway)
        {
          this.Fail(oprot, message, TApplicationException.ExceptionType.InvalidMessageType, "Message type CALL or ONEWAY expected");
          return false;
        }
        int length = message.Name.IndexOf(TMultiplexedProtocol.SEPARATOR);
        if (length < 0)
        {
          this.Fail(oprot, message, TApplicationException.ExceptionType.InvalidProtocol, "Service name not found in message name: " + message.Name + ". Did you forget to use a TMultiplexProtocol in your client?");
          return false;
        }
        string key = message.Name.Substring(0, length);
        TProcessor tprocessor;
        if (!this.ServiceProcessorMap.TryGetValue(key, out tprocessor))
        {
          this.Fail(oprot, message, TApplicationException.ExceptionType.InternalError, "Service name not found: " + key + ". Did you forget to call RegisterProcessor()?");
          return false;
        }
        TMessage messageBegin = new TMessage(message.Name.Substring(key.Length + TMultiplexedProtocol.SEPARATOR.Length), message.Type, message.SeqID);
        return tprocessor.Process((TProtocol) new TMultiplexedProcessor.StoredMessageProtocol(iprot, messageBegin), oprot);
      }
      catch (IOException ex)
      {
        return false;
      }
    }

    private class StoredMessageProtocol : TProtocolDecorator
    {
      private TMessage MsgBegin;

      public StoredMessageProtocol(TProtocol protocol, TMessage messageBegin)
        : base(protocol)
      {
        this.MsgBegin = messageBegin;
      }

      public override TMessage ReadMessageBegin() => this.MsgBegin;
    }
  }
}
