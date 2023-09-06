// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpawnPos
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SpawnPos : IEditableContent
  {
    private Vector2Int m_coords;
    private Direction m_direction;

    public Vector2Int coords => this.m_coords;

    public Direction direction => this.m_direction;

    public override string ToString() => this.GetType().Name;

    public static SpawnPos FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpawnPos) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpawnPos spawnPos = new SpawnPos();
      spawnPos.PopulateFromJson(jsonObject);
      return spawnPos;
    }

    public static SpawnPos FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpawnPos defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpawnPos.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_coords = Serialization.JsonTokenUnityValue(jsonObject, "coords", this.m_coords);
      this.m_direction = (Direction) Serialization.JsonTokenValue<int>(jsonObject, "direction");
    }
  }
}
