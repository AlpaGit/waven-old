// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.RelationRelation
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class RelationRelation
  {
    [DataMember(Name = "account", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account")]
    public Account Account { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "group", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "group")]
    public RelationGroup Group { get; set; }

    [DataMember(Name = "alias", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "alias")]
    public string Alias { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class RelationRelation {\n");
      stringBuilder.Append("  Account: ").Append((object) this.Account).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Group: ").Append((object) this.Group).Append("\n");
      stringBuilder.Append("  Alias: ").Append(this.Alias).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
