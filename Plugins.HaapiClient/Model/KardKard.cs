// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.KardKard
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
  public class KardKard
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "kard_multiple", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "kard_multiple")]
    public List<KardKard> KardMultiple { get; set; }

    [DataMember(Name = "kard_krosmaster", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "kard_krosmaster")]
    public KardTypeKrosmaster KardKrosmaster { get; set; }

    [DataMember(Name = "kard_action", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "kard_action")]
    public KardTypeAction KardAction { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class KardKard {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Description: ").Append(this.Description).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  KardMultiple: ").Append((object) this.KardMultiple).Append("\n");
      stringBuilder.Append("  KardKrosmaster: ").Append((object) this.KardKrosmaster).Append("\n");
      stringBuilder.Append("  KardAction: ").Append((object) this.KardAction).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
