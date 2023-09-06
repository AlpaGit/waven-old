// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameActionsActionsDeliveredKrosmasterFigure
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
  public class GameActionsActionsDeliveredKrosmasterFigure
  {
    [DataMember(Name = "generated_ids", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "generated_ids")]
    public List<long?> GeneratedIds { get; set; }

    [DataMember(Name = "figure_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "figure_id")]
    public long? FigureId { get; set; }

    [DataMember(Name = "pedestral_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "pedestral_id")]
    public long? PedestralId { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "quantity", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "quantity")]
    public long? Quantity { get; set; }

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

    [DataMember(Name = "uid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "uid")]
    public string Uid { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameActionsActionsDeliveredKrosmasterFigure {\n");
      stringBuilder.Append("  GeneratedIds: ").Append((object) this.GeneratedIds).Append("\n");
      stringBuilder.Append("  FigureId: ").Append((object) this.FigureId).Append("\n");
      stringBuilder.Append("  PedestralId: ").Append((object) this.PedestralId).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Quantity: ").Append((object) this.Quantity).Append("\n");
      stringBuilder.Append("  Game: ").Append((object) this.Game).Append("\n");
      stringBuilder.Append("  ServerId: ").Append((object) this.ServerId).Append("\n");
      stringBuilder.Append("  UserId: ").Append((object) this.UserId).Append("\n");
      stringBuilder.Append("  Date: ").Append((object) this.Date).Append("\n");
      stringBuilder.Append("  Uid: ").Append(this.Uid).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
