// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TStruct
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public struct TStruct
  {
    private string name;

    public TStruct(string name)
      : this()
    {
      this.name = name;
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }
  }
}
