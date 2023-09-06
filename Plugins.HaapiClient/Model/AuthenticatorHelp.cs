// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AuthenticatorHelp
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AuthenticatorHelp
  {
    [DataMember(Name = "question", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "question")]
    public string Question { get; set; }

    [DataMember(Name = "answer", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "answer")]
    public string Answer { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AuthenticatorHelp {\n");
      stringBuilder.Append("  Question: ").Append(this.Question).Append("\n");
      stringBuilder.Append("  Answer: ").Append(this.Answer).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
