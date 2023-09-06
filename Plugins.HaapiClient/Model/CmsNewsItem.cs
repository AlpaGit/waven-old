// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.CmsNewsItem
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class CmsNewsItem
  {
    [DataMember(Name = "title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [DataMember(Name = "sub_title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "sub_title")]
    public string SubTitle { get; set; }

    [DataMember(Name = "begin", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "begin")]
    public string Begin { get; set; }

    [DataMember(Name = "end", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "end")]
    public string End { get; set; }

    [DataMember(Name = "comments_count", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "comments_count")]
    public long? CommentsCount { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "date")]
    public string Date { get; set; }

    [DataMember(Name = "url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class CmsNewsItem {\n");
      stringBuilder.Append("  Title: ").Append(this.Title).Append("\n");
      stringBuilder.Append("  SubTitle: ").Append(this.SubTitle).Append("\n");
      stringBuilder.Append("  Begin: ").Append(this.Begin).Append("\n");
      stringBuilder.Append("  End: ").Append(this.End).Append("\n");
      stringBuilder.Append("  CommentsCount: ").Append((object) this.CommentsCount).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Date: ").Append(this.Date).Append("\n");
      stringBuilder.Append("  Url: ").Append(this.Url).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
