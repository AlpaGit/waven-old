// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopArticle
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
  public class ShopArticle
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "key", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "subtitle", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "subtitle")]
    public string Subtitle { get; set; }

    [DataMember(Name = "description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [DataMember(Name = "currency", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [DataMember(Name = "original_price", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "original_price")]
    public float? OriginalPrice { get; set; }

    [DataMember(Name = "price", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "price")]
    public float? Price { get; set; }

    [DataMember(Name = "startdate", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "startdate")]
    public DateTime? Startdate { get; set; }

    [DataMember(Name = "enddate", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "enddate")]
    public DateTime? Enddate { get; set; }

    [DataMember(Name = "showCountDown", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "showCountDown")]
    public bool? ShowCountDown { get; set; }

    [DataMember(Name = "stock", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "stock")]
    public long? Stock { get; set; }

    [DataMember(Name = "image", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image")]
    public List<ShopImage> Image { get; set; }

    [DataMember(Name = "references", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "references")]
    public List<ShopReference> References { get; set; }

    [DataMember(Name = "promo", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "promo")]
    public List<ShopPromo> Promo { get; set; }

    [DataMember(Name = "media", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "media")]
    public List<ShopMedia> Media { get; set; }

    [DataMember(Name = "metas", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "metas")]
    public List<ShopMetaGroup> Metas { get; set; }

    [DataMember(Name = "pricelist", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "pricelist")]
    public List<ShopPrice> Pricelist { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopArticle {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Key: ").Append(this.Key).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Subtitle: ").Append(this.Subtitle).Append("\n");
      stringBuilder.Append("  Description: ").Append(this.Description).Append("\n");
      stringBuilder.Append("  Currency: ").Append(this.Currency).Append("\n");
      stringBuilder.Append("  OriginalPrice: ").Append((object) this.OriginalPrice).Append("\n");
      stringBuilder.Append("  Price: ").Append((object) this.Price).Append("\n");
      stringBuilder.Append("  Startdate: ").Append((object) this.Startdate).Append("\n");
      stringBuilder.Append("  Enddate: ").Append((object) this.Enddate).Append("\n");
      stringBuilder.Append("  ShowCountDown: ").Append((object) this.ShowCountDown).Append("\n");
      stringBuilder.Append("  Stock: ").Append((object) this.Stock).Append("\n");
      stringBuilder.Append("  Image: ").Append((object) this.Image).Append("\n");
      stringBuilder.Append("  References: ").Append((object) this.References).Append("\n");
      stringBuilder.Append("  Promo: ").Append((object) this.Promo).Append("\n");
      stringBuilder.Append("  Media: ").Append((object) this.Media).Append("\n");
      stringBuilder.Append("  Metas: ").Append((object) this.Metas).Append("\n");
      stringBuilder.Append("  Pricelist: ").Append((object) this.Pricelist).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
