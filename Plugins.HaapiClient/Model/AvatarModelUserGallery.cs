// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AvatarModelUserGallery
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AvatarModelUserGallery
  {
    [DataMember(Name = "userId", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "userId")]
    public long? UserId { get; set; }

    [DataMember(Name = "gallery", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gallery")]
    public AvatarModelGallery Gallery { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AvatarModelUserGallery {\n");
      stringBuilder.Append("  UserId: ").Append((object) this.UserId).Append("\n");
      stringBuilder.Append("  Gallery: ").Append((object) this.Gallery).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
