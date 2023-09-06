// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameActionsActionsDeliveredMeta
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
  public class GameActionsActionsDeliveredMeta
  {
    [DataMember(Name = "destination_account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "destination_account_id")]
    public long? DestinationAccountId { get; set; }

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

    [DataMember(Name = "theme_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "theme_id")]
    public long? ThemeId { get; set; }

    [DataMember(Name = "item_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "item_id")]
    public long? ItemId { get; set; }

    [DataMember(Name = "effect", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "effect")]
    public string Effect { get; set; }

    [DataMember(Name = "generated_ids", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "generated_ids")]
    public List<long?> GeneratedIds { get; set; }

    [DataMember(Name = "kard_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "kard_id")]
    public long? KardId { get; set; }

    [DataMember(Name = "figure_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "figure_id")]
    public long? FigureId { get; set; }

    [DataMember(Name = "pedestral_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "pedestral_id")]
    public long? PedestralId { get; set; }

    [DataMember(Name = "bind_type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "bind_type")]
    public string BindType { get; set; }

    [DataMember(Name = "consume_type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "consume_type")]
    public string ConsumeType { get; set; }

    [DataMember(Name = "companion_xp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "companion_xp")]
    public long? CompanionXp { get; set; }

    [DataMember(Name = "pet_xp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "pet_xp")]
    public long? PetXp { get; set; }

    [DataMember(Name = "gems", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gems")]
    public GameActionsGems Gems { get; set; }

    [DataMember(Name = "booster_guid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "booster_guid")]
    public string BoosterGuid { get; set; }

    [DataMember(Name = "tradingcard_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "tradingcard_id")]
    public string TradingcardId { get; set; }

    [DataMember(Name = "god_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "god_id")]
    public long? GodId { get; set; }

    [DataMember(Name = "account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account_id")]
    public long? AccountId { get; set; }

    [DataMember(Name = "customisation_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "customisation_id")]
    public long? CustomisationId { get; set; }

    [DataMember(Name = "character_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "character_id")]
    public long? CharacterId { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameActionsActionsDeliveredMeta {\n");
      stringBuilder.Append("  DestinationAccountId: ").Append((object) this.DestinationAccountId).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  Quantity: ").Append((object) this.Quantity).Append("\n");
      stringBuilder.Append("  Game: ").Append((object) this.Game).Append("\n");
      stringBuilder.Append("  ServerId: ").Append((object) this.ServerId).Append("\n");
      stringBuilder.Append("  UserId: ").Append((object) this.UserId).Append("\n");
      stringBuilder.Append("  Date: ").Append((object) this.Date).Append("\n");
      stringBuilder.Append("  Uid: ").Append(this.Uid).Append("\n");
      stringBuilder.Append("  ThemeId: ").Append((object) this.ThemeId).Append("\n");
      stringBuilder.Append("  ItemId: ").Append((object) this.ItemId).Append("\n");
      stringBuilder.Append("  Effect: ").Append(this.Effect).Append("\n");
      stringBuilder.Append("  GeneratedIds: ").Append((object) this.GeneratedIds).Append("\n");
      stringBuilder.Append("  KardId: ").Append((object) this.KardId).Append("\n");
      stringBuilder.Append("  FigureId: ").Append((object) this.FigureId).Append("\n");
      stringBuilder.Append("  PedestralId: ").Append((object) this.PedestralId).Append("\n");
      stringBuilder.Append("  BindType: ").Append(this.BindType).Append("\n");
      stringBuilder.Append("  ConsumeType: ").Append(this.ConsumeType).Append("\n");
      stringBuilder.Append("  CompanionXp: ").Append((object) this.CompanionXp).Append("\n");
      stringBuilder.Append("  PetXp: ").Append((object) this.PetXp).Append("\n");
      stringBuilder.Append("  Gems: ").Append((object) this.Gems).Append("\n");
      stringBuilder.Append("  BoosterGuid: ").Append(this.BoosterGuid).Append("\n");
      stringBuilder.Append("  TradingcardId: ").Append(this.TradingcardId).Append("\n");
      stringBuilder.Append("  GodId: ").Append((object) this.GodId).Append("\n");
      stringBuilder.Append("  AccountId: ").Append((object) this.AccountId).Append("\n");
      stringBuilder.Append("  CustomisationId: ").Append((object) this.CustomisationId).Append("\n");
      stringBuilder.Append("  CharacterId: ").Append((object) this.CharacterId).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
