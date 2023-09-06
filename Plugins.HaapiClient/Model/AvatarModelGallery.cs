// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AvatarModelGallery
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
  public class AvatarModelGallery
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public long? Id { get; set; }

    [DataMember(Name = "uid", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "uid")]
    public string Uid { get; set; }

    [DataMember(Name = "gameId", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "gameId")]
    public long? GameId { get; set; }

    [DataMember(Name = "title", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [DataMember(Name = "public", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "public")]
    public bool? _Public { get; set; }

    [DataMember(Name = "avatars", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "avatars")]
    public List<AvatarModelAvatar> Avatars { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AvatarModelGallery {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Uid: ").Append(this.Uid).Append("\n");
      stringBuilder.Append("  GameId: ").Append((object) this.GameId).Append("\n");
      stringBuilder.Append("  Title: ").Append(this.Title).Append("\n");
      stringBuilder.Append("  _Public: ").Append((object) this._Public).Append("\n");
      stringBuilder.Append("  Avatars: ").Append((object) this.Avatars).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
