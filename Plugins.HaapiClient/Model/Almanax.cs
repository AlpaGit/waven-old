// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.Almanax
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class Almanax
  {
    [DataMember(Name = "event", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "event")]
    public AlmanaxEvent _Event { get; set; }

    [DataMember(Name = "zodiac", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "zodiac")]
    public AlmanaxZodiac Zodiac { get; set; }

    [DataMember(Name = "month", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "month")]
    public AlmanaxMonth Month { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class Almanax {\n");
      stringBuilder.Append("  _Event: ").Append((object) this._Event).Append("\n");
      stringBuilder.Append("  Zodiac: ").Append((object) this.Zodiac).Append("\n");
      stringBuilder.Append("  Month: ").Append((object) this.Month).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
