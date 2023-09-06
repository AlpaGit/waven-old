// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.CmsBookItem
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
  public class CmsBookItem
  {
    [DataMember(Name = "title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [DataMember(Name = "sub_title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "sub_title")]
    public string SubTitle { get; set; }

    [DataMember(Name = "summary", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "summary")]
    public string Summary { get; set; }

    [DataMember(Name = "wizcorp_data", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "wizcorp_data")]
    public List<string> WizcorpData { get; set; }

    [DataMember(Name = "wizcorp_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "wizcorp_id")]
    public string WizcorpId { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class CmsBookItem {\n");
      stringBuilder.Append("  Title: ").Append(this.Title).Append("\n");
      stringBuilder.Append("  SubTitle: ").Append(this.SubTitle).Append("\n");
      stringBuilder.Append("  Summary: ").Append(this.Summary).Append("\n");
      stringBuilder.Append("  WizcorpData: ").Append((object) this.WizcorpData).Append("\n");
      stringBuilder.Append("  WizcorpId: ").Append(this.WizcorpId).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
