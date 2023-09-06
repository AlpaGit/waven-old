// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.TournamentTeam
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
  public class TournamentTeam
  {
    [DataMember(Name = "team_uid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "team_uid")]
    public long? TeamUid { get; set; }

    [DataMember(Name = "team_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "team_name")]
    public string TeamName { get; set; }

    [DataMember(Name = "team_characters", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "team_characters")]
    public List<long?> TeamCharacters { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class TournamentTeam {\n");
      stringBuilder.Append("  TeamUid: ").Append((object) this.TeamUid).Append("\n");
      stringBuilder.Append("  TeamName: ").Append(this.TeamName).Append("\n");
      stringBuilder.Append("  TeamCharacters: ").Append((object) this.TeamCharacters).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
