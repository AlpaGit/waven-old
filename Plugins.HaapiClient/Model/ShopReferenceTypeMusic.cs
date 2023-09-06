// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopReferenceTypeMusic
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ShopReferenceTypeMusic
  {
    [DataMember(Name = "preview_path", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "preview_path")]
    public string PreviewPath { get; set; }

    [DataMember(Name = "complete_path", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "complete_path")]
    public string CompletePath { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopReferenceTypeMusic {\n");
      stringBuilder.Append("  PreviewPath: ").Append(this.PreviewPath).Append("\n");
      stringBuilder.Append("  CompletePath: ").Append(this.CompletePath).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
