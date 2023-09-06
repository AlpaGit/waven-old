// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ThemisModel
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ThemisModel
  {
    [DataMember(Name = "model_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "model_id")]
    public long? ModelId { get; set; }

    [DataMember(Name = "model_success", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "model_success")]
    public bool? ModelSuccess { get; set; }

    [DataMember(Name = "model_return", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "model_return")]
    public string ModelReturn { get; set; }

    [DataMember(Name = "model_description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "model_description")]
    public string ModelDescription { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ThemisModel {\n");
      stringBuilder.Append("  ModelId: ").Append((object) this.ModelId).Append("\n");
      stringBuilder.Append("  ModelSuccess: ").Append((object) this.ModelSuccess).Append("\n");
      stringBuilder.Append("  ModelReturn: ").Append(this.ModelReturn).Append("\n");
      stringBuilder.Append("  ModelDescription: ").Append(this.ModelDescription).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
