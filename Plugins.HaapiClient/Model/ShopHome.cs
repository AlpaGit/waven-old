// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopHome
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
  public class ShopHome
  {
    [DataMember(Name = "categories", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "categories")]
    public List<ShopCategory> Categories { get; set; }

    [DataMember(Name = "gondolahead_main", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gondolahead_main")]
    public List<ShopGondolaHead> GondolaheadMain { get; set; }

    [DataMember(Name = "gondolahead_article", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gondolahead_article")]
    public List<ShopArticle> GondolaheadArticle { get; set; }

    [DataMember(Name = "hightlight_carousel", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "hightlight_carousel")]
    public List<ShopHighlight> HightlightCarousel { get; set; }

    [DataMember(Name = "hightlight_image", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "hightlight_image")]
    public List<ShopHighlight> HightlightImage { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopHome {\n");
      stringBuilder.Append("  Categories: ").Append((object) this.Categories).Append("\n");
      stringBuilder.Append("  GondolaheadMain: ").Append((object) this.GondolaheadMain).Append("\n");
      stringBuilder.Append("  GondolaheadArticle: ").Append((object) this.GondolaheadArticle).Append("\n");
      stringBuilder.Append("  HightlightCarousel: ").Append((object) this.HightlightCarousel).Append("\n");
      stringBuilder.Append("  HightlightImage: ").Append((object) this.HightlightImage).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
