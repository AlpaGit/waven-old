// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.RemoveEntityEffectDefinition
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
  public sealed class RemoveEntityEffectDefinition : EffectExecutionDefinition
  {
    private bool m_kill;

    public bool kill => this.m_kill;

    public override string ToString() => this.GetType().Name;

    public static RemoveEntityEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (RemoveEntityEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      RemoveEntityEffectDefinition effectDefinition = new RemoveEntityEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static RemoveEntityEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      RemoveEntityEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : RemoveEntityEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_kill = Serialization.JsonTokenValue<bool>(jsonObject, "kill");
    }
  }
}
