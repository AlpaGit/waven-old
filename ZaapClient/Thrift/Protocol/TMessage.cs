// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TMessage
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public struct TMessage
  {
    private string name;
    private TMessageType type;
    private int seqID;

    public TMessage(string name, TMessageType type, int seqid)
      : this()
    {
      this.name = name;
      this.type = type;
      this.seqID = seqid;
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public TMessageType Type
    {
      get => this.type;
      set => this.type = value;
    }

    public int SeqID
    {
      get => this.seqID;
      set => this.seqID = value;
    }
  }
}
