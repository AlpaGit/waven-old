// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.TournamentRound
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
  public class TournamentRound
  {
    [DataMember(Name = "round_number", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "round_number")]
    public long? RoundNumber { get; set; }

    [DataMember(Name = "fights", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "fights")]
    public List<TournamentMatch> Fights { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class TournamentRound {\n");
      stringBuilder.Append("  RoundNumber: ").Append((object) this.RoundNumber).Append("\n");
      stringBuilder.Append("  Fights: ").Append((object) this.Fights).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
