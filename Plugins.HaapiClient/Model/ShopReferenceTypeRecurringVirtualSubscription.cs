// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.ShopReferenceTypeRecurringVirtualSubscription
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class ShopReferenceTypeRecurringVirtualSubscription
  {
    [DataMember(Name = "recurring_article", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "recurring_article")]
    public long? RecurringArticle { get; set; }

    [DataMember(Name = "recurring_duration", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "recurring_duration")]
    public string RecurringDuration { get; set; }

    [DataMember(Name = "recurring_missingday_article", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "recurring_missingday_article")]
    public long? RecurringMissingdayArticle { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class ShopReferenceTypeRecurringVirtualSubscription {\n");
      stringBuilder.Append("  RecurringArticle: ").Append((object) this.RecurringArticle).Append("\n");
      stringBuilder.Append("  RecurringDuration: ").Append(this.RecurringDuration).Append("\n");
      stringBuilder.Append("  RecurringMissingdayArticle: ").Append((object) this.RecurringMissingdayArticle).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
