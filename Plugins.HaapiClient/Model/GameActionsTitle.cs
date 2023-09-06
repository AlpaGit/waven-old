// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameActionsTitle
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
  public class GameActionsTitle
  {
    [DataMember(Name = "lang", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "lang")]
    public List<string> Lang { get; set; }

    [DataMember(Name = "text", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "text")]
    public List<string> Text { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameActionsTitle {\n");
      stringBuilder.Append("  Lang: ").Append((object) this.Lang).Append("\n");
      stringBuilder.Append("  Text: ").Append((object) this.Text).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
