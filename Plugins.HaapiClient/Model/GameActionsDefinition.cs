// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameActionsDefinition
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
  public class GameActionsDefinition
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "category_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "category_id")]
    public long? CategoryId { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "restriction_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "restriction_id")]
    public long? RestrictionId { get; set; }

    [DataMember(Name = "actions", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "actions")]
    public List<GameActionsActionsMeta> Actions { get; set; }

    [DataMember(Name = "online_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "online_date")]
    public DateTime? OnlineDate { get; set; }

    [DataMember(Name = "offline_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "offline_date")]
    public DateTime? OfflineDate { get; set; }

    [DataMember(Name = "added_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_date")]
    public DateTime? AddedDate { get; set; }

    [DataMember(Name = "added_account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "added_account_id")]
    public long? AddedAccountId { get; set; }

    [DataMember(Name = "modified_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "modified_date")]
    public DateTime? ModifiedDate { get; set; }

    [DataMember(Name = "modified_account_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "modified_account_id")]
    public long? ModifiedAccountId { get; set; }

    [DataMember(Name = "available_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "available_date")]
    public DateTime? AvailableDate { get; set; }

    [DataMember(Name = "definition_lang", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "definition_lang")]
    public List<string> DefinitionLang { get; set; }

    [DataMember(Name = "definition_title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "definition_title")]
    public List<string> DefinitionTitle { get; set; }

    [DataMember(Name = "definition_description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "definition_description")]
    public List<string> DefinitionDescription { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameActionsDefinition {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  CategoryId: ").Append((object) this.CategoryId).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  RestrictionId: ").Append((object) this.RestrictionId).Append("\n");
      stringBuilder.Append("  Actions: ").Append((object) this.Actions).Append("\n");
      stringBuilder.Append("  OnlineDate: ").Append((object) this.OnlineDate).Append("\n");
      stringBuilder.Append("  OfflineDate: ").Append((object) this.OfflineDate).Append("\n");
      stringBuilder.Append("  AddedDate: ").Append((object) this.AddedDate).Append("\n");
      stringBuilder.Append("  AddedAccountId: ").Append((object) this.AddedAccountId).Append("\n");
      stringBuilder.Append("  ModifiedDate: ").Append((object) this.ModifiedDate).Append("\n");
      stringBuilder.Append("  ModifiedAccountId: ").Append((object) this.ModifiedAccountId).Append("\n");
      stringBuilder.Append("  AvailableDate: ").Append((object) this.AvailableDate).Append("\n");
      stringBuilder.Append("  DefinitionLang: ").Append((object) this.DefinitionLang).Append("\n");
      stringBuilder.Append("  DefinitionTitle: ").Append((object) this.DefinitionTitle).Append("\n");
      stringBuilder.Append("  DefinitionDescription: ").Append((object) this.DefinitionDescription).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
