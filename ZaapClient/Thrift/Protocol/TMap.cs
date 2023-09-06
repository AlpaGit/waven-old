// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TMap
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public struct TMap
  {
    private TType keyType;
    private TType valueType;
    private int count;

    public TMap(TType keyType, TType valueType, int count)
      : this()
    {
      this.keyType = keyType;
      this.valueType = valueType;
      this.count = count;
    }

    public TType KeyType
    {
      get => this.keyType;
      set => this.keyType = value;
    }

    public TType ValueType
    {
      get => this.valueType;
      set => this.valueType = value;
    }

    public int Count
    {
      get => this.count;
      set => this.count = value;
    }
  }
}
