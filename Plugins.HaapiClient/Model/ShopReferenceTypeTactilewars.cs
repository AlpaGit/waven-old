// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopReferenceTypeTactilewars
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ShopReferenceTypeTactilewars
  {
    [DataMember(Name = "item", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "item")]
    public string Item { get; set; }

    [DataMember(Name = "rarity", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "rarity")]
    public long? Rarity { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopReferenceTypeTactilewars {\n");
      stringBuilder.Append("  Item: ").Append(this.Item).Append("\n");
      stringBuilder.Append("  Rarity: ").Append((object) this.Rarity).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
