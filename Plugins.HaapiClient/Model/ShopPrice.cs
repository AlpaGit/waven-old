// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopPrice
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ShopPrice
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "original_price", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "original_price")]
    public float? OriginalPrice { get; set; }

    [DataMember(Name = "price", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "price")]
    public float? Price { get; set; }

    [DataMember(Name = "currency", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [DataMember(Name = "country", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "country")]
    public string Country { get; set; }

    [DataMember(Name = "paymentmode", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "paymentmode")]
    public string Paymentmode { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopPrice {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  OriginalPrice: ").Append((object) this.OriginalPrice).Append("\n");
      stringBuilder.Append("  Price: ").Append((object) this.Price).Append("\n");
      stringBuilder.Append("  Currency: ").Append(this.Currency).Append("\n");
      stringBuilder.Append("  Country: ").Append(this.Country).Append("\n");
      stringBuilder.Append("  Paymentmode: ").Append(this.Paymentmode).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
