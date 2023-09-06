// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameAccount
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
  public class GameAccount
  {
    [DataMember(Name = "total_time_elapsed", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "total_time_elapsed")]
    public long? TotalTimeElapsed { get; set; }

    [DataMember(Name = "subscribed", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "subscribed")]
    public bool? Subscribed { get; set; }

    [DataMember(Name = "first_subscription_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "first_subscription_date")]
    public DateTime? FirstSubscriptionDate { get; set; }

    [DataMember(Name = "expiration_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "expiration_date")]
    public DateTime? ExpirationDate { get; set; }

    [DataMember(Name = "ban_end_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "ban_end_date")]
    public DateTime? BanEndDate { get; set; }

    [DataMember(Name = "added_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_date")]
    public DateTime? AddedDate { get; set; }

    [DataMember(Name = "login_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "login_date")]
    public DateTime? LoginDate { get; set; }

    [DataMember(Name = "login_ip", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "login_ip")]
    public string LoginIp { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameAccount {\n");
      stringBuilder.Append("  TotalTimeElapsed: ").Append((object) this.TotalTimeElapsed).Append("\n");
      stringBuilder.Append("  Subscribed: ").Append((object) this.Subscribed).Append("\n");
      stringBuilder.Append("  FirstSubscriptionDate: ").Append((object) this.FirstSubscriptionDate).Append("\n");
      stringBuilder.Append("  ExpirationDate: ").Append((object) this.ExpirationDate).Append("\n");
      stringBuilder.Append("  BanEndDate: ").Append((object) this.BanEndDate).Append("\n");
      stringBuilder.Append("  AddedDate: ").Append((object) this.AddedDate).Append("\n");
      stringBuilder.Append("  LoginDate: ").Append((object) this.LoginDate).Append("\n");
      stringBuilder.Append("  LoginIp: ").Append(this.LoginIp).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
