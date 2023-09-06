// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ApiKey
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ApiKey
  {
    [DataMember(Name = "key", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [DataMember(Name = "account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account_id")]
    public long? AccountId { get; set; }

    [DataMember(Name = "ip", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "ip")]
    public string Ip { get; set; }

    [DataMember(Name = "added_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_date")]
    public DateTime? AddedDate { get; set; }

    [DataMember(Name = "meta", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "meta")]
    public List<string> Meta { get; set; }

    [DataMember(Name = "data", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, string> Data { get; set; }

    [DataMember(Name = "access", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "access")]
    public List<string> Access { get; set; }

    [DataMember(Name = "refresh_token", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "refresh_token")]
    public string RefreshToken { get; set; }

    [DataMember(Name = "expiration_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "expiration_date")]
    public DateTime? ExpirationDate { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ApiKey {\n");
      stringBuilder.Append("  Key: ").Append(this.Key).Append("\n");
      stringBuilder.Append("  AccountId: ").Append((object) this.AccountId).Append("\n");
      stringBuilder.Append("  Ip: ").Append(this.Ip).Append("\n");
      stringBuilder.Append("  AddedDate: ").Append((object) this.AddedDate).Append("\n");
      stringBuilder.Append("  Meta: ").Append((object) this.Meta).Append("\n");
      stringBuilder.Append("  Data: ").Append((object) this.Data).Append("\n");
      stringBuilder.Append("  Access: ").Append((object) this.Access).Append("\n");
      stringBuilder.Append("  RefreshToken: ").Append(this.RefreshToken).Append("\n");
      stringBuilder.Append("  ExpirationDate: ").Append((object) this.ExpirationDate).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
