// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.TournamentMatch
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
  public class TournamentMatch
  {
    [DataMember(Name = "fight_uid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "fight_uid")]
    public long? FightUid { get; set; }

    [DataMember(Name = "room_code", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "room_code")]
    public string RoomCode { get; set; }

    [DataMember(Name = "millis", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "millis")]
    public long? Millis { get; set; }

    [DataMember(Name = "teams", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "teams")]
    public List<TournamentTeam> Teams { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class TournamentMatch {\n");
      stringBuilder.Append("  FightUid: ").Append((object) this.FightUid).Append("\n");
      stringBuilder.Append("  RoomCode: ").Append(this.RoomCode).Append("\n");
      stringBuilder.Append("  Millis: ").Append((object) this.Millis).Append("\n");
      stringBuilder.Append("  Teams: ").Append((object) this.Teams).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
