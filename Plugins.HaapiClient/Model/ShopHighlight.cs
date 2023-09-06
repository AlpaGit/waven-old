// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopHighlight
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
  public class ShopHighlight
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "image", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image")]
    public List<ShopImage> Image { get; set; }

    [DataMember(Name = "mode", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "mode")]
    public string Mode { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "link", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "link")]
    public string Link { get; set; }

    [DataMember(Name = "description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "external_category", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "external_category")]
    public ShopCategory ExternalCategory { get; set; }

    [DataMember(Name = "external_article", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "external_article")]
    public ShopArticle ExternalArticle { get; set; }

    [DataMember(Name = "external_gondolahead", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "external_gondolahead")]
    public ShopGondolaHead ExternalGondolahead { get; set; }

    [DataMember(Name = "external_direct", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "external_direct")]
    public string ExternalDirect { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopHighlight {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Image: ").Append((object) this.Image).Append("\n");
      stringBuilder.Append("  Mode: ").Append(this.Mode).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Link: ").Append(this.Link).Append("\n");
      stringBuilder.Append("  Description: ").Append(this.Description).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  ExternalCategory: ").Append((object) this.ExternalCategory).Append("\n");
      stringBuilder.Append("  ExternalArticle: ").Append((object) this.ExternalArticle).Append("\n");
      stringBuilder.Append("  ExternalGondolahead: ").Append((object) this.ExternalGondolahead).Append("\n");
      stringBuilder.Append("  ExternalDirect: ").Append(this.ExternalDirect).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
