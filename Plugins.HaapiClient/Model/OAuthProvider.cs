// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.OAuthProvider
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
  public class OAuthProvider
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "clientid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "clientid")]
    public string Clientid { get; set; }

    [DataMember(Name = "secret", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "secret")]
    public string Secret { get; set; }

    [DataMember(Name = "redirect", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "redirect")]
    public string Redirect { get; set; }

    [DataMember(Name = "access", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "access")]
    public List<string> Access { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class OAuthProvider {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Clientid: ").Append(this.Clientid).Append("\n");
      stringBuilder.Append("  Secret: ").Append(this.Secret).Append("\n");
      stringBuilder.Append("  Redirect: ").Append(this.Redirect).Append("\n");
      stringBuilder.Append("  Access: ").Append((object) this.Access).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
