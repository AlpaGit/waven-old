// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.Account
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
  public class Account
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "login", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "login")]
    public string Login { get; set; }

    [DataMember(Name = "nickname", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "nickname")]
    public string Nickname { get; set; }

    [DataMember(Name = "security", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "security")]
    public List<string> Security { get; set; }

    [DataMember(Name = "lang", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "lang")]
    public string Lang { get; set; }

    [DataMember(Name = "community", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "community")]
    public string Community { get; set; }

    [DataMember(Name = "added_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_date")]
    public DateTime? AddedDate { get; set; }

    [DataMember(Name = "added_ip", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_ip")]
    public string AddedIp { get; set; }

    [DataMember(Name = "login_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "login_date")]
    public DateTime? LoginDate { get; set; }

    [DataMember(Name = "login_ip", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "login_ip")]
    public string LoginIp { get; set; }

    [DataMember(Name = "locked", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "locked")]
    public long? Locked { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class Account {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Login: ").Append(this.Login).Append("\n");
      stringBuilder.Append("  Nickname: ").Append(this.Nickname).Append("\n");
      stringBuilder.Append("  Security: ").Append((object) this.Security).Append("\n");
      stringBuilder.Append("  Lang: ").Append(this.Lang).Append("\n");
      stringBuilder.Append("  Community: ").Append(this.Community).Append("\n");
      stringBuilder.Append("  AddedDate: ").Append((object) this.AddedDate).Append("\n");
      stringBuilder.Append("  AddedIp: ").Append(this.AddedIp).Append("\n");
      stringBuilder.Append("  LoginDate: ").Append((object) this.LoginDate).Append("\n");
      stringBuilder.Append("  LoginIp: ").Append(this.LoginIp).Append("\n");
      stringBuilder.Append("  Locked: ").Append((object) this.Locked).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
