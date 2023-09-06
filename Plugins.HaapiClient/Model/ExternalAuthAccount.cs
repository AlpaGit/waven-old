// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ExternalAuthAccount
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
  public class ExternalAuthAccount
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account_id")]
    public long? AccountId { get; set; }

    [DataMember(Name = "uid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "uid")]
    public string Uid { get; set; }

    [DataMember(Name = "data", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "data")]
    public List<string> Data { get; set; }

    [DataMember(Name = "game_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "game_id")]
    public long? GameId { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ExternalAuthAccount {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  AccountId: ").Append((object) this.AccountId).Append("\n");
      stringBuilder.Append("  Uid: ").Append(this.Uid).Append("\n");
      stringBuilder.Append("  Data: ").Append((object) this.Data).Append("\n");
      stringBuilder.Append("  GameId: ").Append((object) this.GameId).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
