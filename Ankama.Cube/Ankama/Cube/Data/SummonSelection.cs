// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SummonSelection
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
  public sealed class SummonSelection : AbstractInvocationSelection
  {
    private Id<SummoningDefinition> m_summonId;

    public Id<SummoningDefinition> summonId => this.m_summonId;

    public override string ToString() => this.CustomToString();

    public static SummonSelection FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SummonSelection) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SummonSelection summonSelection = new SummonSelection();
      summonSelection.PopulateFromJson(jsonObject);
      return summonSelection;
    }

    public static SummonSelection FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SummonSelection defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SummonSelection.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_summonId = Serialization.JsonTokenIdValue<SummoningDefinition>(jsonObject, "summonId");
    }
  }
}
