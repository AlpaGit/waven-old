// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.KardTicket
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class KardTicket
  {
    [DataMember(Name = "order_list", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "order_list")]
    public List<KardTypeOrder> OrderList { get; set; }

    [DataMember(Name = "kard_list", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "kard_list")]
    public List<KardKard> KardList { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class KardTicket {\n");
      stringBuilder.Append("  OrderList: ").Append((object) this.OrderList).Append("\n");
      stringBuilder.Append("  KardList: ").Append((object) this.KardList).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
