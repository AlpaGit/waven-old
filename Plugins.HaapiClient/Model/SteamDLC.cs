// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.SteamDLC
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
  public class SteamDLC
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "game_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "game_id")]
    public long? GameId { get; set; }

    [DataMember(Name = "app_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "app_id")]
    public long? AppId { get; set; }

    [DataMember(Name = "definition_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "definition_id")]
    public long? DefinitionId { get; set; }

    [DataMember(Name = "deleted_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "deleted_date")]
    public DateTime? DeletedDate { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class SteamDLC {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  GameId: ").Append((object) this.GameId).Append("\n");
      stringBuilder.Append("  AppId: ").Append((object) this.AppId).Append("\n");
      stringBuilder.Append("  DefinitionId: ").Append((object) this.DefinitionId).Append("\n");
      stringBuilder.Append("  DeletedDate: ").Append((object) this.DeletedDate).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
