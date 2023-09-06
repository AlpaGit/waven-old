// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TTransportFactory
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Transport
{
  public class TTransportFactory
  {
    public virtual TTransport GetTransport(TTransport trans) => trans;
  }
}
