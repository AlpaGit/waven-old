// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.CmsNews
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class CmsNews
  {
    [DataMember(Name = "item_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "item_id")]
    public long? ItemId { get; set; }

    [DataMember(Name = "image_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image_url")]
    public string ImageUrl { get; set; }

    [DataMember(Name = "title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [DataMember(Name = "sub_title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "sub_title")]
    public string SubTitle { get; set; }

    [DataMember(Name = "url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "date")]
    public string Date { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class CmsNews {\n");
      stringBuilder.Append("  ItemId: ").Append((object) this.ItemId).Append("\n");
      stringBuilder.Append("  ImageUrl: ").Append(this.ImageUrl).Append("\n");
      stringBuilder.Append("  Title: ").Append(this.Title).Append("\n");
      stringBuilder.Append("  SubTitle: ").Append(this.SubTitle).Append("\n");
      stringBuilder.Append("  Url: ").Append(this.Url).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Date: ").Append(this.Date).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
