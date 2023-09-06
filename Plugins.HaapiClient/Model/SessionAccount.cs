// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.SessionAccount
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class SessionAccount
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account_id")]
    public long? AccountId { get; set; }

    [DataMember(Name = "game_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "game_id")]
    public long? GameId { get; set; }

    [DataMember(Name = "date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "date")]
    public DateTime? Date { get; set; }

    [DataMember(Name = "ip", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "ip")]
    public string Ip { get; set; }

    [DataMember(Name = "country", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "country")]
    public string Country { get; set; }

    [DataMember(Name = "isp_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "isp_id")]
    public long? IspId { get; set; }

    [DataMember(Name = "duration", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "duration")]
    public long? Duration { get; set; }

    [DataMember(Name = "type_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type_id")]
    public long? TypeId { get; set; }

    [DataMember(Name = "server_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "server_id")]
    public long? ServerId { get; set; }

    [DataMember(Name = "character_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "character_id")]
    public long? CharacterId { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class SessionAccount {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  AccountId: ").Append((object) this.AccountId).Append("\n");
      stringBuilder.Append("  GameId: ").Append((object) this.GameId).Append("\n");
      stringBuilder.Append("  Date: ").Append((object) this.Date).Append("\n");
      stringBuilder.Append("  Ip: ").Append(this.Ip).Append("\n");
      stringBuilder.Append("  Country: ").Append(this.Country).Append("\n");
      stringBuilder.Append("  IspId: ").Append((object) this.IspId).Append("\n");
      stringBuilder.Append("  Duration: ").Append((object) this.Duration).Append("\n");
      stringBuilder.Append("  TypeId: ").Append((object) this.TypeId).Append("\n");
      stringBuilder.Append("  ServerId: ").Append((object) this.ServerId).Append("\n");
      stringBuilder.Append("  CharacterId: ").Append((object) this.CharacterId).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
