// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.SessionAccountsPaged
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
  public class SessionAccountsPaged
  {
    [DataMember(Name = "total", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "total")]
    public long? Total { get; set; }

    [DataMember(Name = "page_size", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "page_size")]
    public long? PageSize { get; set; }

    [DataMember(Name = "page_previous", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "page_previous")]
    public long? PagePrevious { get; set; }

    [DataMember(Name = "page_current", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "page_current")]
    public long? PageCurrent { get; set; }

    [DataMember(Name = "page_next", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "page_next")]
    public long? PageNext { get; set; }

    [DataMember(Name = "page_last", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "page_last")]
    public long? PageLast { get; set; }

    [DataMember(Name = "accounts", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "accounts")]
    public List<Account> Accounts { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class SessionAccountsPaged {\n");
      stringBuilder.Append("  Total: ").Append((object) this.Total).Append("\n");
      stringBuilder.Append("  PageSize: ").Append((object) this.PageSize).Append("\n");
      stringBuilder.Append("  PagePrevious: ").Append((object) this.PagePrevious).Append("\n");
      stringBuilder.Append("  PageCurrent: ").Append((object) this.PageCurrent).Append("\n");
      stringBuilder.Append("  PageNext: ").Append((object) this.PageNext).Append("\n");
      stringBuilder.Append("  PageLast: ").Append((object) this.PageLast).Append("\n");
      stringBuilder.Append("  Accounts: ").Append((object) this.Accounts).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
