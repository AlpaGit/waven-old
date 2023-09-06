// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameActionsActionsDelivered
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
  public class GameActionsActionsDelivered
  {
    [DataMember(Name = "game", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "game")]
    public long? Game { get; set; }

    [DataMember(Name = "server_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "server_id")]
    public long? ServerId { get; set; }

    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "user_id")]
    public long? UserId { get; set; }

    [DataMember(Name = "date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "date")]
    public DateTime? Date { get; set; }

    [DataMember(Name = "quantity", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "quantity")]
    public long? Quantity { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameActionsActionsDelivered {\n");
      stringBuilder.Append("  Game: ").Append((object) this.Game).Append("\n");
      stringBuilder.Append("  ServerId: ").Append((object) this.ServerId).Append("\n");
      stringBuilder.Append("  UserId: ").Append((object) this.UserId).Append("\n");
      stringBuilder.Append("  Date: ").Append((object) this.Date).Append("\n");
      stringBuilder.Append("  Quantity: ").Append((object) this.Quantity).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
