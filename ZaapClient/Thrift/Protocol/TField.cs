// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TField
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public struct TField
  {
    private string name;
    private TType type;
    private short id;

    public TField(string name, TType type, short id)
      : this()
    {
      this.name = name;
      this.type = type;
      this.id = id;
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public TType Type
    {
      get => this.type;
      set => this.type = value;
    }

    public short ID
    {
      get => this.id;
      set => this.id = value;
    }
  }
}
