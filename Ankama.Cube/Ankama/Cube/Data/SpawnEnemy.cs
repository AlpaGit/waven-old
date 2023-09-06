// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpawnEnemy
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SpawnEnemy : IEditableContent
  {
    private List<SpawnPos> m_positions;

    public IReadOnlyList<SpawnPos> positions => (IReadOnlyList<SpawnPos>) this.m_positions;

    public override string ToString() => base.ToString();

    public static SpawnEnemy FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpawnEnemy) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpawnEnemy spawnEnemy = new SpawnEnemy();
      spawnEnemy.PopulateFromJson(jsonObject);
      return spawnEnemy;
    }

    public static SpawnEnemy FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpawnEnemy defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpawnEnemy.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      JArray jarray = Serialization.JsonArray(jsonObject, "positions");
      this.m_positions = new List<SpawnPos>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_positions.Add(SpawnPos.FromJsonToken(token));
    }
  }
}
