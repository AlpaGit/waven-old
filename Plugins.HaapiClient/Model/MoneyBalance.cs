// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.MoneyBalance
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class MoneyBalance
  {
    [DataMember(Name = "currency", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [DataMember(Name = "amount", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "amount")]
    public float? Amount { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class MoneyBalance {\n");
      stringBuilder.Append("  Currency: ").Append(this.Currency).Append("\n");
      stringBuilder.Append("  Amount: ").Append((object) this.Amount).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
