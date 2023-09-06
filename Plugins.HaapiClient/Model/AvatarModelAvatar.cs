// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.AvatarModelAvatar
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
  public class AvatarModelAvatar
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

    [DataMember(Name = "character", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "character")]
    public string Character { get; set; }

    [DataMember(Name = "galleries", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "galleries")]
    public List<AvatarModelGallery> Galleries { get; set; }

    [DataMember(Name = "url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class AvatarModelAvatar {\n");
      stringBuilder.Append("  Id: ").Append((object) this.Id).Append("\n");
      stringBuilder.Append("  Uid: ").Append(this.Uid).Append("\n");
      stringBuilder.Append("  GameId: ").Append((object) this.GameId).Append("\n");
      stringBuilder.Append("  Character: ").Append(this.Character).Append("\n");
      stringBuilder.Append("  Galleries: ").Append((object) this.Galleries).Append("\n");
      stringBuilder.Append("  Url: ").Append(this.Url).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
