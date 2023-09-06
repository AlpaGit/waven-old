// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ErrorTranslated
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ErrorTranslated
  {
    [DataMember(Name = "key", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [DataMember(Name = "text", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "text")]
    public string Text { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ErrorTranslated {\n");
      stringBuilder.Append("  Key: ").Append(this.Key).Append("\n");
      stringBuilder.Append("  Text: ").Append(this.Text).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
