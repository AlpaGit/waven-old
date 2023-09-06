// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TList
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public struct TList
  {
    private TType elementType;
    private int count;

    public TList(TType elementType, int count)
      : this()
    {
      this.elementType = elementType;
      this.count = count;
    }

    public TType ElementType
    {
      get => this.elementType;
      set => this.elementType = value;
    }

    public int Count
    {
      get => this.count;
      set => this.count = value;
    }
  }
}
