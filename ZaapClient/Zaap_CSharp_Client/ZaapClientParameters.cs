// Decompiled with JetBrains decompiler
// Type: Zaap_CSharp_Client.ZaapClientParameters
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Zaap_CSharp_Client
{
  public class ZaapClientParameters
  {
    public int port;
    public string name;
    public string release;
    public int instanceId;
    public string hash;

    public bool Valid() => this.port != 0 && !string.IsNullOrEmpty(this.name) && !string.IsNullOrEmpty(this.release) && !string.IsNullOrEmpty(this.hash);
  }
}
