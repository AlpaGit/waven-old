// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.LegalContent
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
  public class LegalContent
  {
    [DataMember(Name = "current_version", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "current_version")]
    public string CurrentVersion { get; set; }

    [DataMember(Name = "texts", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "texts")]
    public List<string> Texts { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class LegalContent {\n");
      stringBuilder.Append("  CurrentVersion: ").Append(this.CurrentVersion).Append("\n");
      stringBuilder.Append("  Texts: ").Append((object) this.Texts).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
