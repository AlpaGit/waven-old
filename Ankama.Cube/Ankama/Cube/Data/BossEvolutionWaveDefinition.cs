// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.BossEvolutionWaveDefinition
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
  public sealed class BossEvolutionWaveDefinition : IEditableContent
  {
    private int m_evolutionStep;
    private BossWaveDefinition m_wave;
    private bool m_resetEvolution;

    public int evolutionStep => this.m_evolutionStep;

    public BossWaveDefinition wave => this.m_wave;

    public bool resetEvolution => this.m_resetEvolution;

    public override string ToString() => this.GetType().Name;

    public static BossEvolutionWaveDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (BossEvolutionWaveDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      BossEvolutionWaveDefinition evolutionWaveDefinition = new BossEvolutionWaveDefinition();
      evolutionWaveDefinition.PopulateFromJson(jsonObject);
      return evolutionWaveDefinition;
    }

    public static BossEvolutionWaveDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      BossEvolutionWaveDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : BossEvolutionWaveDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_evolutionStep = Serialization.JsonTokenValue<int>(jsonObject, "evolutionStep", 1);
      this.m_wave = BossWaveDefinition.FromJsonProperty(jsonObject, "wave");
      this.m_resetEvolution = Serialization.JsonTokenValue<bool>(jsonObject, "resetEvolution", true);
    }
  }
}
