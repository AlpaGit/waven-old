// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.PmCount
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class PmCount
  {
    [DataMember(Name = "unread_messages", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "unread_messages")]
    public long? UnreadMessages { get; set; }

    [DataMember(Name = "all_messages", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "all_messages")]
    public long? AllMessages { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class PmCount {\n");
      stringBuilder.Append("  UnreadMessages: ").Append((object) this.UnreadMessages).Append("\n");
      stringBuilder.Append("  AllMessages: ").Append((object) this.AllMessages).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
