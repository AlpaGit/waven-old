// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopBuyResult
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
  public class ShopBuyResult
  {
    [DataMember(Name = "balance", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "balance")]
    public List<MoneyBalance> Balance { get; set; }

    [DataMember(Name = "order_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "order_id")]
    public long? OrderId { get; set; }

    [DataMember(Name = "order_status", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "order_status")]
    public string OrderStatus { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopBuyResult {\n");
      stringBuilder.Append("  Balance: ").Append((object) this.Balance).Append("\n");
      stringBuilder.Append("  OrderId: ").Append((object) this.OrderId).Append("\n");
      stringBuilder.Append("  OrderStatus: ").Append(this.OrderStatus).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
