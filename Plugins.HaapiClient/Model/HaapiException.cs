// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.HaapiException
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
  public class HaapiException
  {
    [DataMember(Name = "status", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status")]
    public int? Status { get; set; }

    [DataMember(Name = "message", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "stack_trace", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "stack_trace")]
    public List<string> StackTrace { get; set; }

    [DataMember(Name = "code", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "code")]
    public int? Code { get; set; }

    [DataMember(Name = "detail", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "detail")]
    public string Detail { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class HaapiException {\n");
      stringBuilder.Append("  Status: ").Append((object) this.Status).Append("\n");
      stringBuilder.Append("  Message: ").Append(this.Message).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  StackTrace: ").Append((object) this.StackTrace).Append("\n");
      stringBuilder.Append("  Code: ").Append((object) this.Code).Append("\n");
      stringBuilder.Append("  Detail: ").Append(this.Detail).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
