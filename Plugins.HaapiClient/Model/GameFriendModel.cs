// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Model.GameFriendModel
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Ankama.Haapi.Swagger.Model
{
  [DataContract]
  public class GameFriendModel
  {
    [DataMember(Name = "game", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "game")]
    public long? Game { get; set; }

    [DataMember(Name = "friend_from_account", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "friend_from_account")]
    public long? FriendFromAccount { get; set; }

    [DataMember(Name = "friend_to_account", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "friend_to_account")]
    public long? FriendToAccount { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("class GameFriendModel {\n");
      stringBuilder.Append("  Game: ").Append((object) this.Game).Append("\n");
      stringBuilder.Append("  FriendFromAccount: ").Append((object) this.FriendFromAccount).Append("\n");
      stringBuilder.Append("  FriendToAccount: ").Append((object) this.FriendToAccount).Append("\n");
      stringBuilder.Append("}\n");
      return stringBuilder.ToString();
    }

    public string ToJson() => JsonConvert.SerializeObject((object) this, Formatting.Indented);
  }
}
