// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AlmanaxZodiac
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AlmanaxZodiac
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "begin", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "begin")]
    public DateTime? Begin { get; set; }

    [DataMember(Name = "end", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "end")]
    public DateTime? End { get; set; }

    [DataMember(Name = "color", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "color")]
    public string Color { get; set; }

    [DataMember(Name = "background", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "background")]
    public bool? Background { get; set; }

    [DataMember(Name = "background_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "background_url")]
    public string BackgroundUrl { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "description", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [DataMember(Name = "image_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image_url")]
    public string ImageUrl { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AlmanaxZodiac {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Begin: ").Append((object) this.Begin).Append("\n");
      stringBuilder.Append("  End: ").Append((object) this.End).Append("\n");
      stringBuilder.Append("  Color: ").Append(this.Color).Append("\n");
      stringBuilder.Append("  Background: ").Append((object) this.Background).Append("\n");
      stringBuilder.Append("  BackgroundUrl: ").Append(this.BackgroundUrl).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  Description: ").Append(this.Description).Append("\n");
      stringBuilder.Append("  ImageUrl: ").Append(this.ImageUrl).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
