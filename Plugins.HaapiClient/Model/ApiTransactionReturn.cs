// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ApiTransactionReturn
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
  public class ApiTransactionReturn
  {
    [DataMember(Name = "code", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "code")]
    public long? Code { get; set; }

    [DataMember(Name = "header", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "header")]
    public Dictionary<string, string> Header { get; set; }

    [DataMember(Name = "data", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "data")]
    public string Data { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ApiTransactionReturn {\n");
      stringBuilder.Append("  Code: ").Append((object) this.Code).Append("\n");
      stringBuilder.Append("  Header: ").Append((object) this.Header).Append("\n");
      stringBuilder.Append("  Data: ").Append(this.Data).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
