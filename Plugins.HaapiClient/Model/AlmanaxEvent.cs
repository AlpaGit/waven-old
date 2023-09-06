// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AlmanaxEvent
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
  public class AlmanaxEvent
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [DataMember(Name = "mobile_info", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "mobile_info")]
    public string MobileInfo { get; set; }

    [DataMember(Name = "color", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "color")]
    public string Color { get; set; }

    [DataMember(Name = "date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "date")]
    public DateTime? Date { get; set; }

    [DataMember(Name = "color_date", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "color_date")]
    public string ColorDate { get; set; }

    [DataMember(Name = "recurring", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "recurring")]
    public bool? Recurring { get; set; }

    [DataMember(Name = "background", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "background")]
    public bool? Background { get; set; }

    [DataMember(Name = "background_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "background_url")]
    public string BackgroundUrl { get; set; }

    [DataMember(Name = "day_background_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "day_background_url")]
    public string DayBackgroundUrl { get; set; }

    [DataMember(Name = "border_background_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "border_background_url")]
    public string BorderBackgroundUrl { get; set; }

    [DataMember(Name = "image_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "image_url")]
    public string ImageUrl { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [DataMember(Name = "boss_image_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "boss_image_url")]
    public string BossImageUrl { get; set; }

    [DataMember(Name = "boss_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "boss_name")]
    public string BossName { get; set; }

    [DataMember(Name = "boss_text", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "boss_text")]
    public string BossText { get; set; }

    [DataMember(Name = "ephemeris", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "ephemeris")]
    public string Ephemeris { get; set; }

    [DataMember(Name = "rubrikabrax", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "rubrikabrax")]
    public string Rubrikabrax { get; set; }

    [DataMember(Name = "show_fest", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "show_fest")]
    public bool? ShowFest { get; set; }

    [DataMember(Name = "fest_text", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "fest_text")]
    public string FestText { get; set; }

    [DataMember(Name = "weight", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "weight")]
    public long? Weight { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AlmanaxEvent {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Type: ").Append(this.Type).Append("\n");
      stringBuilder.Append("  MobileInfo: ").Append(this.MobileInfo).Append("\n");
      stringBuilder.Append("  Color: ").Append(this.Color).Append("\n");
      stringBuilder.Append("  Date: ").Append((object) this.Date).Append("\n");
      stringBuilder.Append("  ColorDate: ").Append(this.ColorDate).Append("\n");
      stringBuilder.Append("  Recurring: ").Append((object) this.Recurring).Append("\n");
      stringBuilder.Append("  Background: ").Append((object) this.Background).Append("\n");
      stringBuilder.Append("  BackgroundUrl: ").Append(this.BackgroundUrl).Append("\n");
      stringBuilder.Append("  DayBackgroundUrl: ").Append(this.DayBackgroundUrl).Append("\n");
      stringBuilder.Append("  BorderBackgroundUrl: ").Append(this.BorderBackgroundUrl).Append("\n");
      stringBuilder.Append("  ImageUrl: ").Append(this.ImageUrl).Append("\n");
      stringBuilder.Append("  Name: ").Append(this.Name).Append("\n");
      stringBuilder.Append("  BossImageUrl: ").Append(this.BossImageUrl).Append("\n");
      stringBuilder.Append("  BossName: ").Append(this.BossName).Append("\n");
      stringBuilder.Append("  BossText: ").Append(this.BossText).Append("\n");
      stringBuilder.Append("  Ephemeris: ").Append(this.Ephemeris).Append("\n");
      stringBuilder.Append("  Rubrikabrax: ").Append(this.Rubrikabrax).Append("\n");
      stringBuilder.Append("  ShowFest: ").Append((object) this.ShowFest).Append("\n");
      stringBuilder.Append("  FestText: ").Append(this.FestText).Append("\n");
      stringBuilder.Append("  Weight: ").Append((object) this.Weight).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
