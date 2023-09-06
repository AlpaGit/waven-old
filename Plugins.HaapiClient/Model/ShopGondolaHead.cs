// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopGondolaHead
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
  public class ShopGondolaHead
  {
    [DataMember(Name = "image", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image")]
    public List<ShopImage> Image { get; set; }

    [DataMember(Name = "home", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "home")]
    public bool? Home { get; set; }

    [DataMember(Name = "main", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "main")]
    public bool? Main { get; set; }

    [DataMember(Name = "link", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "link")]
    public string Link { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopGondolaHead {\n");
      stringBuilder.Append("  Image: ").Append((object) this.Image).Append("\n");
      stringBuilder.Append("  Home: ").Append((object) this.Home).Append("\n");
      stringBuilder.Append("  Main: ").Append((object) this.Main).Append("\n");
      stringBuilder.Append("  Link: ").Append(this.Link).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
