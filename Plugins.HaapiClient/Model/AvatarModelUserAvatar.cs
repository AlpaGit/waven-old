// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AvatarModelUserAvatar
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class AvatarModelUserAvatar
  {
    [DataMember(Name = "userId", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "userId")]
    public long? UserId { get; set; }

    [DataMember(Name = "avatar", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "avatar")]
    public AvatarModelAvatar Avatar { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AvatarModelUserAvatar {\n");
      stringBuilder.Append("  UserId: ").Append((object) this.UserId).Append("\n");
      stringBuilder.Append("  Avatar: ").Append((object) this.Avatar).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
