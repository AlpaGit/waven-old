// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameActionsGems
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class GameActionsGems
  {
    [DataMember(Name = "gem1", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gem1")]
    public long? Gem1 { get; set; }

    [DataMember(Name = "gem2", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gem2")]
    public long? Gem2 { get; set; }

    [DataMember(Name = "gem3", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gem3")]
    public long? Gem3 { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameActionsGems {\n");
      stringBuilder.Append("  Gem1: ").Append((object) this.Gem1).Append("\n");
      stringBuilder.Append("  Gem2: ").Append((object) this.Gem2).Append("\n");
      stringBuilder.Append("  Gem3: ").Append((object) this.Gem3).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
