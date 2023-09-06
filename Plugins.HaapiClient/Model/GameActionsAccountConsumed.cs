// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameActionsAccountConsumed
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
  public class GameActionsAccountConsumed
  {
    [DataMember(Name = "actions", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "actions")]
    public List<GameActionsActionsDelivered> Actions { get; set; }

    [DataMember(Name = "updated_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "updated_date")]
    public DateTime? UpdatedDate { get; set; }

    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "account_id")]
    public long? AccountId { get; set; }

    [DataMember(Name = "game_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "game_id")]
    public long? GameId { get; set; }

    [DataMember(Name = "server_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "server_id")]
    public long? ServerId { get; set; }

    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "user_id")]
    public long? UserId { get; set; }

    [DataMember(Name = "definition_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "definition_id")]
    public long? DefinitionId { get; set; }

    [DataMember(Name = "description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "description")]
    public GameActionsDescription Description { get; set; }

    [DataMember(Name = "title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "title")]
    public GameActionsTitle Title { get; set; }

    [DataMember(Name = "external_type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "external_type")]
    public string ExternalType { get; set; }

    [DataMember(Name = "external_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "external_id")]
    public long? ExternalId { get; set; }

    [DataMember(Name = "added_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_date")]
    public DateTime? AddedDate { get; set; }

    [DataMember(Name = "available_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "available_date")]
    public DateTime? AvailableDate { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameActionsAccountConsumed {\n");
      stringBuilder.Append("  Actions: ").Append((object) this.Actions).Append("\n");
      stringBuilder.Append("  UpdatedDate: ").Append((object) this.UpdatedDate).Append("\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  AccountId: ").Append((object) this.AccountId).Append("\n");
      stringBuilder.Append("  GameId: ").Append((object) this.GameId).Append("\n");
      stringBuilder.Append("  ServerId: ").Append((object) this.ServerId).Append("\n");
      stringBuilder.Append("  UserId: ").Append((object) this.UserId).Append("\n");
      stringBuilder.Append("  DefinitionId: ").Append((object) this.DefinitionId).Append("\n");
      stringBuilder.Append("  Description: ").Append((object) this.Description).Append("\n");
      stringBuilder.Append("  Title: ").Append((object) this.Title).Append("\n");
      stringBuilder.Append("  ExternalType: ").Append(this.ExternalType).Append("\n");
      stringBuilder.Append("  ExternalId: ").Append((object) this.ExternalId).Append("\n");
      stringBuilder.Append("  AddedDate: ").Append((object) this.AddedDate).Append("\n");
      stringBuilder.Append("  AvailableDate: ").Append((object) this.AvailableDate).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
