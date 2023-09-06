// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FightMapRegionDefinition
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
  public sealed class FightMapRegionDefinition : IEditableContent
  {
    [SerializeField]
    private Vector2Int m_sizeMin;
    [SerializeField]
    private Vector2Int m_sizeMax;
    [SerializeField]
    private SpawnCell[] m_spawnCells;

    public Vector2Int sizeMin => this.m_sizeMin;

    public Vector2Int sizeMax => this.m_sizeMax;

    public SpawnCell[] spawnCells => this.m_spawnCells;

    public override string ToString() => this.GetType().Name;

    public static FightMapRegionDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FightMapRegionDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FightMapRegionDefinition regionDefinition = new FightMapRegionDefinition();
      regionDefinition.PopulateFromJson(jsonObject);
      return regionDefinition;
    }

    public static FightMapRegionDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FightMapRegionDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FightMapRegionDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }
  }
}
