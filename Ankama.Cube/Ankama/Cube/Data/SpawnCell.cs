// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpawnCell
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
  public sealed class SpawnCell : IEditableContent
  {
    [SerializeField]
    private Vector2Int m_coords;
    [SerializeField]
    private Direction m_orientation;

    public Vector2Int coords => this.m_coords;

    public Direction orientation => this.m_orientation;

    public override string ToString() => this.GetType().Name;

    public static SpawnCell FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpawnCell) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpawnCell spawnCell = new SpawnCell();
      spawnCell.PopulateFromJson(jsonObject);
      return spawnCell;
    }

    public static SpawnCell FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpawnCell defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpawnCell.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }
  }
}
