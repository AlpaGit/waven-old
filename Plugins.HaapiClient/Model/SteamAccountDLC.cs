// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.SteamAccountDLC
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class SteamAccountDLC
  {
    [DataMember(Name = "dlc_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "dlc_id")]
    public long? DlcId { get; set; }

    [DataMember(Name = "dlc", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "dlc")]
    public SteamDLC Dlc { get; set; }

    [DataMember(Name = "account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account_id")]
    public long? AccountId { get; set; }

    [DataMember(Name = "added_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_date")]
    public DateTime? AddedDate { get; set; }

    [DataMember(Name = "added_ip", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_ip")]
    public long? AddedIp { get; set; }

    [DataMember(Name = "deleted_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "deleted_date")]
    public DateTime? DeletedDate { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class SteamAccountDLC {\n");
      stringBuilder.Append("  DlcId: ").Append((object) this.DlcId).Append("\n");
      stringBuilder.Append("  Dlc: ").Append((object) this.Dlc).Append("\n");
      stringBuilder.Append("  AccountId: ").Append((object) this.AccountId).Append("\n");
      stringBuilder.Append("  AddedDate: ").Append((object) this.AddedDate).Append("\n");
      stringBuilder.Append("  AddedIp: ").Append((object) this.AddedIp).Append("\n");
      stringBuilder.Append("  DeletedDate: ").Append((object) this.DeletedDate).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
