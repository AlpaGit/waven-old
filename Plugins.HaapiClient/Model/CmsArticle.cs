// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.CmsArticle
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
  public class CmsArticle
  {
    [DataMember(Name = "image_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image_url")]
    public string ImageUrl { get; set; }

    [DataMember(Name = "subtitle", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "subtitle")]
    public string Subtitle { get; set; }

    [DataMember(Name = "baseline", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "baseline")]
    public string Baseline { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "category", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; }

    [DataMember(Name = "template_key", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "template_key")]
    public string TemplateKey { get; set; }

    [DataMember(Name = "sites", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "sites")]
    public List<string> Sites { get; set; }

    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "lang", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "lang")]
    public string Lang { get; set; }

    [DataMember(Name = "date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "date")]
    public string Date { get; set; }

    [DataMember(Name = "url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    [DataMember(Name = "canonical_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "canonical_url")]
    public string CanonicalUrl { get; set; }

    [DataMember(Name = "url_topic", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "url_topic")]
    public string UrlTopic { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class CmsArticle {\n");
      stringBuilder.Append("  ImageUrl: ").Append(this.ImageUrl).Append("\n");
      stringBuilder.Append("  Subtitle: ").Append(this.Subtitle).Append("\n");
      stringBuilder.Append("  Baseline: ").Append(this.Baseline).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Category: ").Append(this.Category).Append("\n");
      stringBuilder.Append("  TemplateKey: ").Append(this.TemplateKey).Append("\n");
      stringBuilder.Append("  Sites: ").Append((object) this.Sites).Append("\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Lang: ").Append(this.Lang).Append("\n");
      stringBuilder.Append("  Date: ").Append(this.Date).Append("\n");
      stringBuilder.Append("  Url: ").Append(this.Url).Append("\n");
      stringBuilder.Append("  CanonicalUrl: ").Append(this.CanonicalUrl).Append("\n");
      stringBuilder.Append("  UrlTopic: ").Append(this.UrlTopic).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
