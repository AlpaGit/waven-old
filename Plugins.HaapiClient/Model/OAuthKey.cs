// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.OAuthKey
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class OAuthKey
  {
    [DataMember(Name = "access_token", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "access_token")]
    public string AccessToken { get; set; }

    [DataMember(Name = "refresh_token", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "refresh_token")]
    public string RefreshToken { get; set; }

    [DataMember(Name = "expires_in", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "expires_in")]
    public long? ExpiresIn { get; set; }

    [DataMember(Name = "token_type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "token_type")]
    public string TokenType { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class OAuthKey {\n");
      stringBuilder.Append("  AccessToken: ").Append(this.AccessToken).Append("\n");
      stringBuilder.Append("  RefreshToken: ").Append(this.RefreshToken).Append("\n");
      stringBuilder.Append("  ExpiresIn: ").Append((object) this.ExpiresIn).Append("\n");
      stringBuilder.Append("  TokenType: ").Append(this.TokenType).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
