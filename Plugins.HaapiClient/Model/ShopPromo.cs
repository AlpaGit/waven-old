// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopPromo
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
  public class ShopPromo
  {
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [DataMember(Name = "image", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image")]
    public string Image { get; set; }

    [DataMember(Name = "start_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "start_date")]
    public DateTime? StartDate { get; set; }

    [DataMember(Name = "end_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "end_date")]
    public DateTime? EndDate { get; set; }

    [DataMember(Name = "gifts", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gifts")]
    public List<ShopArticle> Gifts { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopPromo {\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Description: ").Append(this.Description).Append("\n");
      stringBuilder.Append("  Image: ").Append(this.Image).Append("\n");
      stringBuilder.Append("  StartDate: ").Append((object) this.StartDate).Append("\n");
      stringBuilder.Append("  EndDate: ").Append((object) this.EndDate).Append("\n");
      stringBuilder.Append("  Gifts: ").Append((object) this.Gifts).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
