// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AlmanaxMonth
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AlmanaxMonth
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "month", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "month")]
    public long? Month { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "color", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "color")]
    public string Color { get; set; }

    [DataMember(Name = "background", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "background")]
    public bool? Background { get; set; }

    [DataMember(Name = "background_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "background_url")]
    public string BackgroundUrl { get; set; }

    [DataMember(Name = "protector_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "protector_name")]
    public string ProtectorName { get; set; }

    [DataMember(Name = "protector_description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "protector_description")]
    public string ProtectorDescription { get; set; }

    [DataMember(Name = "protector_image_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "protector_image_url")]
    public string ProtectorImageUrl { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AlmanaxMonth {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Month: ").Append((object) this.Month).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Color: ").Append(this.Color).Append("\n");
      stringBuilder.Append("  Background: ").Append((object) this.Background).Append("\n");
      stringBuilder.Append("  BackgroundUrl: ").Append(this.BackgroundUrl).Append("\n");
      stringBuilder.Append("  ProtectorName: ").Append(this.ProtectorName).Append("\n");
      stringBuilder.Append("  ProtectorDescription: ").Append(this.ProtectorDescription).Append("\n");
      stringBuilder.Append("  ProtectorImageUrl: ").Append(this.ProtectorImageUrl).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
