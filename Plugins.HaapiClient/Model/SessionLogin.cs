// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.SessionLogin
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class SessionLogin
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "id_string", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id_string")]
    public string IdString { get; set; }

    [DataMember(Name = "account", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account")]
    public Account Account { get; set; }

    [DataMember(Name = "game", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "game")]
    public GameAccount Game { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class SessionLogin {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  IdString: ").Append(this.IdString).Append("\n");
      stringBuilder.Append("  Account: ").Append((object) this.Account).Append("\n");
      stringBuilder.Append("  Game: ").Append((object) this.Game).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
