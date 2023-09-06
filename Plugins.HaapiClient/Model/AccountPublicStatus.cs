// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AccountPublicStatus
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AccountPublicStatus
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [DataMember(Name = "value", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AccountPublicStatus {\n");
      stringBuilder.Append("  Id: ").Append(this.Id).Append("\n");
      stringBuilder.Append("  Value: ").Append(this.Value).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
