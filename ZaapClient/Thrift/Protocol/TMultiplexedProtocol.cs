// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TMultiplexedProtocol
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public class TMultiplexedProtocol : TProtocolDecorator
  {
    public static string SEPARATOR = ":";
    private string ServiceName;

    public TMultiplexedProtocol(TProtocol protocol, string serviceName)
      : base(protocol)
    {
      this.ServiceName = serviceName;
    }

    public override void WriteMessageBegin(TMessage tMessage)
    {
      switch (tMessage.Type)
      {
        case TMessageType.Call:
        case TMessageType.Oneway:
          base.WriteMessageBegin(new TMessage(this.ServiceName + TMultiplexedProtocol.SEPARATOR + tMessage.Name, tMessage.Type, tMessage.SeqID));
          break;
        default:
          base.WriteMessageBegin(tMessage);
          break;
      }
    }
  }
}
