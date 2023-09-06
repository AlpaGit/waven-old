// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TServerTransport
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Transport
{
  public abstract class TServerTransport
  {
    public abstract void Listen();

    public abstract void Close();

    protected abstract TTransport AcceptImpl();

    public TTransport Accept() => this.AcceptImpl() ?? throw new TTransportException("accept() may not return NULL");
  }
}
