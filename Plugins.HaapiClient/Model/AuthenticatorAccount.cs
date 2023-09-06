// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AuthenticatorAccount
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AuthenticatorAccount
  {
    [DataMember(Name = "otp_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "otp_id")]
    public long? OtpId { get; set; }

    [DataMember(Name = "otp_login", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "otp_login")]
    public string OtpLogin { get; set; }

    [DataMember(Name = "otp_nickname", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "otp_nickname")]
    public string OtpNickname { get; set; }

    [DataMember(Name = "otp_public_key", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "otp_public_key")]
    public string OtpPublicKey { get; set; }

    [DataMember(Name = "otp_login_enabled", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "otp_login_enabled")]
    public long? OtpLoginEnabled { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AuthenticatorAccount {\n");
      stringBuilder.Append("  OtpId: ").Append((object) this.OtpId).Append("\n");
      stringBuilder.Append("  OtpLogin: ").Append(this.OtpLogin).Append("\n");
      stringBuilder.Append("  OtpNickname: ").Append(this.OtpNickname).Append("\n");
      stringBuilder.Append("  OtpPublicKey: ").Append(this.OtpPublicKey).Append("\n");
      stringBuilder.Append("  OtpLoginEnabled: ").Append((object) this.OtpLoginEnabled).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
