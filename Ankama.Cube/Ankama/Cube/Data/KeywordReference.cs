// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.KeywordReference
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting;
using DataEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Ankama.Cube.Data
{
  [Serializable]
  public struct KeywordReference
  {
    public readonly ObjectReference.Type type;
    public readonly int id;
    public readonly string keyword;
    private readonly KeywordCondition conditionMask;

    public KeywordReference(ObjectReference.Type type, int id, KeywordCondition conditionMask = (KeywordCondition) 0)
    {
      this.type = type;
      this.id = id;
      this.keyword = (string) null;
      this.conditionMask = conditionMask;
    }

    public KeywordReference(string keyword, KeywordCondition conditionMask = (KeywordCondition) 0)
    {
      this.type = ObjectReference.Type.None;
      this.id = 0;
      this.keyword = keyword;
      this.conditionMask = conditionMask;
    }

    public bool IsValidFor(KeywordContext context) => (context & (KeywordContext) this.conditionMask) == (KeywordContext) this.conditionMask;

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.type == ObjectReference.Type.None)
        stringBuilder.Append(this.keyword);
      else
        stringBuilder.Append(this.type.ToString().ToLower()).Append(':').Append(this.id);
      if (this.conditionMask != (KeywordCondition) 0)
        stringBuilder.Append("<if ").Append(this.conditionMask.ToString()).Append("/>");
      return stringBuilder.ToString();
    }

    public bool Equals(KeywordReference other) => this.type == other.type && this.id == other.id && string.Equals(this.keyword, other.keyword) && this.conditionMask == other.conditionMask;

    public override bool Equals(object obj) => obj != null && obj is KeywordReference other && this.Equals(other);

    public override int GetHashCode()
    {
      int conditionMask = (int) this.conditionMask;
      return this.keyword != null ? conditionMask * 397 ^ this.keyword.GetHashCode() : (int) ((ObjectReference.Type) (conditionMask * 397) ^ this.type) * 397 ^ this.id;
    }

    public static bool operator ==(KeywordReference left, KeywordReference right) => left.type == right.type && left.id == right.id && left.keyword == right.keyword && left.conditionMask == right.conditionMask;

    public static bool operator !=(KeywordReference left, KeywordReference right) => left.type != right.type || left.id != right.id || left.keyword != right.keyword || left.conditionMask != right.conditionMask;

    public static void Write(KeywordReference keywordReference, JsonTextWriter writer)
    {
      KeywordCondition conditionMask = keywordReference.conditionMask;
      if (keywordReference.type == ObjectReference.Type.None)
      {
        if (conditionMask == (KeywordCondition) 0)
        {
          writer.WriteValue(keywordReference.keyword);
        }
        else
        {
          writer.WriteStartObject();
          writer.WritePropertyName("condition");
          writer.WriteValue((object) conditionMask);
          writer.WritePropertyName("keyword");
          writer.WriteValue(keywordReference.keyword);
          writer.WriteEndObject();
        }
      }
      else
      {
        writer.WriteStartObject();
        if (conditionMask != (KeywordCondition) 0)
        {
          writer.WritePropertyName("condition");
          writer.WriteValue((object) conditionMask);
        }
        writer.WritePropertyName("type");
        writer.WriteValue((object) keywordReference.type);
        writer.WritePropertyName("id");
        writer.WriteValue(keywordReference.id);
        writer.WriteEndObject();
      }
    }

    public static KeywordReference FromJson(JToken jToken)
    {
      if (jToken.Type == JTokenType.String)
        return new KeywordReference(jToken.Value<string>());
      JObject jsonObject = (JObject) jToken;
      KeywordCondition conditionMask = (KeywordCondition) Serialization.JsonTokenValue<int>(jsonObject, "condition");
      string keyword = Serialization.JsonTokenValue<string>(jsonObject, "keyword");
      return keyword != null ? new KeywordReference(keyword, conditionMask) : new KeywordReference((ObjectReference.Type) Serialization.JsonTokenValue<int>(jsonObject, "type"), Serialization.JsonTokenValue<int>(jsonObject, "id"), conditionMask);
    }
  }
}
