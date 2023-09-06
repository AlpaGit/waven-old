// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ErrorCodeRestriction
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ErrorCodeRestriction
  {
    [DataMember(Name = "reason", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reason")]
    public string Reason { get; set; }

    [DataMember(Name = "message", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ErrorCodeRestriction {\n");
      stringBuilder.Append("  Reason: ").Append(this.Reason).Append("\n");
      stringBuilder.Append("  Message: ").Append(this.Message).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
