// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopReferenceTypeVideo
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ShopReferenceTypeVideo
  {
    [DataMember(Name = "duration", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "duration")]
    public long? Duration { get; set; }

    [DataMember(Name = "media_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "media_id")]
    public long? MediaId { get; set; }

    [DataMember(Name = "cid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "cid")]
    public string Cid { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopReferenceTypeVideo {\n");
      stringBuilder.Append("  Duration: ").Append((object) this.Duration).Append("\n");
      stringBuilder.Append("  MediaId: ").Append((object) this.MediaId).Append("\n");
      stringBuilder.Append("  Cid: ").Append(this.Cid).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
