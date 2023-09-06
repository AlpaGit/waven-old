// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AuthenticatorRestoreDevice
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
  public class AuthenticatorRestoreDevice
  {
    [DataMember(Name = "device_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_id")]
    public long? DeviceId { get; set; }

    [DataMember(Name = "accounts", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "accounts")]
    public List<AuthenticatorAccount> Accounts { get; set; }

    [DataMember(Name = "device_restore_code", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_restore_code")]
    public string DeviceRestoreCode { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AuthenticatorRestoreDevice {\n");
      stringBuilder.Append("  DeviceId: ").Append((object) this.DeviceId).Append("\n");
      stringBuilder.Append("  Accounts: ").Append((object) this.Accounts).Append("\n");
      stringBuilder.Append("  DeviceRestoreCode: ").Append(this.DeviceRestoreCode).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
