// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopReference
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
  public class ShopReference
  {
    [DataMember(Name = "description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "free", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "free")]
    public bool? Free { get; set; }

    [DataMember(Name = "quantity", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "quantity")]
    public long? Quantity { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "reference_accountstatus", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_accountstatus")]
    public ShopReferenceTypeAccountStatus ReferenceAccountstatus { get; set; }

    [DataMember(Name = "reference_icegift", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_icegift")]
    public List<ShopReferenceTypeIceGift> ReferenceIcegift { get; set; }

    [DataMember(Name = "reference_virtualgift", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_virtualgift")]
    public List<ShopReferenceTypeVirtualGift> ReferenceVirtualgift { get; set; }

    [DataMember(Name = "reference_kard", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_kard")]
    public List<ShopReferenceTypeKard> ReferenceKard { get; set; }

    [DataMember(Name = "reference_krosmaster", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_krosmaster")]
    public List<ShopReferenceTypeKrosmaster> ReferenceKrosmaster { get; set; }

    [DataMember(Name = "reference_nothing", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_nothing")]
    public ShopReferenceTypeNothing ReferenceNothing { get; set; }

    [DataMember(Name = "reference_tactilewars", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_tactilewars")]
    public ShopReferenceTypeTactilewars ReferenceTactilewars { get; set; }

    [DataMember(Name = "reference_virtualsubscriptionlevel", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_virtualsubscriptionlevel")]
    public ShopReferenceTypeVirtualSubscriptionLevel ReferenceVirtualsubscriptionlevel { get; set; }

    [DataMember(Name = "reference_video", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_video")]
    public ShopReferenceTypeVideo ReferenceVideo { get; set; }

    [DataMember(Name = "reference_music", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_music")]
    public ShopReferenceTypeMusic ReferenceMusic { get; set; }

    [DataMember(Name = "reference_flag", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_flag")]
    public ShopReferenceTypeFlag ReferenceFlag { get; set; }

    [DataMember(Name = "reference_chartransfer", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_chartransfer")]
    public ShopReferenceTypeCharTransfer ReferenceChartransfer { get; set; }

    [DataMember(Name = "reference_digitalcomic", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_digitalcomic")]
    public ShopReferenceTypeDigitalComic ReferenceDigitalcomic { get; set; }

    [DataMember(Name = "reference_premium", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_premium")]
    public ShopReferenceTypePremium ReferencePremium { get; set; }

    [DataMember(Name = "reference_recurringvirtualsubscription", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_recurringvirtualsubscription")]
    public ShopReferenceTypeRecurringVirtualSubscription ReferenceRecurringvirtualsubscription { get; set; }

    [DataMember(Name = "reference_ogrine", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_ogrine")]
    public ShopReferenceTypeOgrine ReferenceOgrine { get; set; }

    [DataMember(Name = "reference_gameaction", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reference_gameaction")]
    public ShopReferenceTypeGameAction ReferenceGameaction { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopReference {\n");
      stringBuilder.Append("  Description: ").Append(this.Description).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Free: ").Append((object) this.Free).Append("\n");
      stringBuilder.Append("  Quantity: ").Append((object) this.Quantity).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  ReferenceAccountstatus: ").Append((object) this.ReferenceAccountstatus).Append("\n");
      stringBuilder.Append("  ReferenceIcegift: ").Append((object) this.ReferenceIcegift).Append("\n");
      stringBuilder.Append("  ReferenceVirtualgift: ").Append((object) this.ReferenceVirtualgift).Append("\n");
      stringBuilder.Append("  ReferenceKard: ").Append((object) this.ReferenceKard).Append("\n");
      stringBuilder.Append("  ReferenceKrosmaster: ").Append((object) this.ReferenceKrosmaster).Append("\n");
      stringBuilder.Append("  ReferenceNothing: ").Append((object) this.ReferenceNothing).Append("\n");
      stringBuilder.Append("  ReferenceTactilewars: ").Append((object) this.ReferenceTactilewars).Append("\n");
      stringBuilder.Append("  ReferenceVirtualsubscriptionlevel: ").Append((object) this.ReferenceVirtualsubscriptionlevel).Append("\n");
      stringBuilder.Append("  ReferenceVideo: ").Append((object) this.ReferenceVideo).Append("\n");
      stringBuilder.Append("  ReferenceMusic: ").Append((object) this.ReferenceMusic).Append("\n");
      stringBuilder.Append("  ReferenceFlag: ").Append((object) this.ReferenceFlag).Append("\n");
      stringBuilder.Append("  ReferenceChartransfer: ").Append((object) this.ReferenceChartransfer).Append("\n");
      stringBuilder.Append("  ReferenceDigitalcomic: ").Append((object) this.ReferenceDigitalcomic).Append("\n");
      stringBuilder.Append("  ReferencePremium: ").Append((object) this.ReferencePremium).Append("\n");
      stringBuilder.Append("  ReferenceRecurringvirtualsubscription: ").Append((object) this.ReferenceRecurringvirtualsubscription).Append("\n");
      stringBuilder.Append("  ReferenceOgrine: ").Append((object) this.ReferenceOgrine).Append("\n");
      stringBuilder.Append("  ReferenceGameaction: ").Append((object) this.ReferenceGameaction).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
