// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AuthenticatorDevice
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AuthenticatorDevice
  {
    [DataMember(Name = "device_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_id")]
    public long? DeviceId { get; set; }

    [DataMember(Name = "device_restore_code", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_restore_code")]
    public string DeviceRestoreCode { get; set; }

    [DataMember(Name = "device_uid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_uid")]
    public string DeviceUid { get; set; }

    [DataMember(Name = "device_type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_type")]
    public string DeviceType { get; set; }

    [DataMember(Name = "device_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_name")]
    public string DeviceName { get; set; }

    [DataMember(Name = "device_version", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_version")]
    public string DeviceVersion { get; set; }

    [DataMember(Name = "device_deleted_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_deleted_date")]
    public long? DeviceDeletedDate { get; set; }

    [DataMember(Name = "device_deleted_deviceid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device_deleted_deviceid")]
    public long? DeviceDeletedDeviceid { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AuthenticatorDevice {\n");
      stringBuilder.Append("  DeviceId: ").Append((object) this.DeviceId).Append("\n");
      stringBuilder.Append("  DeviceRestoreCode: ").Append(this.DeviceRestoreCode).Append("\n");
      stringBuilder.Append("  DeviceUid: ").Append(this.DeviceUid).Append("\n");
      stringBuilder.Append("  DeviceType: ").Append(this.DeviceType).Append("\n");
      stringBuilder.Append("  DeviceName: ").Append(this.DeviceName).Append("\n");
      stringBuilder.Append("  DeviceVersion: ").Append(this.DeviceVersion).Append("\n");
      stringBuilder.Append("  DeviceDeletedDate: ").Append((object) this.DeviceDeletedDate).Append("\n");
      stringBuilder.Append("  DeviceDeletedDeviceid: ").Append((object) this.DeviceDeletedDeviceid).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
