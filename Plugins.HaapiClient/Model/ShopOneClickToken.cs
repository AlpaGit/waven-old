// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopOneClickToken
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ShopOneClickToken
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "pan", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "pan")]
    public string Pan { get; set; }

    [DataMember(Name = "brand", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "brand")]
    public string Brand { get; set; }

    [DataMember(Name = "expiry_year", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "expiry_year")]
    public long? ExpiryYear { get; set; }

    [DataMember(Name = "expiry_month", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "expiry_month")]
    public long? ExpiryMonth { get; set; }

    [DataMember(Name = "security_method", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "security_method")]
    public string SecurityMethod { get; set; }

    [DataMember(Name = "security_method_value", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "security_method_value")]
    public string SecurityMethodValue { get; set; }

    [DataMember(Name = "added_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_date")]
    public string AddedDate { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopOneClickToken {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Pan: ").Append(this.Pan).Append("\n");
      stringBuilder.Append("  Brand: ").Append(this.Brand).Append("\n");
      stringBuilder.Append("  ExpiryYear: ").Append((object) this.ExpiryYear).Append("\n");
      stringBuilder.Append("  ExpiryMonth: ").Append((object) this.ExpiryMonth).Append("\n");
      stringBuilder.Append("  SecurityMethod: ").Append(this.SecurityMethod).Append("\n");
      stringBuilder.Append("  SecurityMethodValue: ").Append(this.SecurityMethodValue).Append("\n");
      stringBuilder.Append("  AddedDate: ").Append(this.AddedDate).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
