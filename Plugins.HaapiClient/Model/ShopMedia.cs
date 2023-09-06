// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopMedia
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ShopMedia
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "key", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [DataMember(Name = "lang", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "lang")]
    public string Lang { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "param", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "param")]
    public string Param { get; set; }

    [DataMember(Name = "order", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "order")]
    public long? Order { get; set; }

    [DataMember(Name = "url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopMedia {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Key: ").Append(this.Key).Append("\n");
      stringBuilder.Append("  Lang: ").Append(this.Lang).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Param: ").Append(this.Param).Append("\n");
      stringBuilder.Append("  Order: ").Append((object) this.Order).Append("\n");
      stringBuilder.Append("  Url: ").Append(this.Url).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
