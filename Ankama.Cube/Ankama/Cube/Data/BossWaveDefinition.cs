// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.BossWaveDefinition
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
  public sealed class BossWaveDefinition : IEditableContent
  {
    private List<SpawnEnemy> m_spawn;

    public IReadOnlyList<SpawnEnemy> spawn => (IReadOnlyList<SpawnEnemy>) this.m_spawn;

    public override string ToString() => base.ToString();

    public static BossWaveDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (BossWaveDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      BossWaveDefinition bossWaveDefinition = new BossWaveDefinition();
      bossWaveDefinition.PopulateFromJson(jsonObject);
      return bossWaveDefinition;
    }

    public static BossWaveDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      BossWaveDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : BossWaveDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      JArray jarray = Serialization.JsonArray(jsonObject, "spawn");
      this.m_spawn = new List<SpawnEnemy>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_spawn.Add(SpawnEnemy.FromJsonToken(token));
    }
  }
}
