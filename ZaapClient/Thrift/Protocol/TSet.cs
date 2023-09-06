// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TSet
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public struct TSet
  {
    private TType elementType;
    private int count;

    public TSet(TType elementType, int count)
      : this()
    {
      this.elementType = elementType;
      this.count = count;
    }

    public TSet(TList list)
      : this(list.ElementType, list.Count)
    {
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
