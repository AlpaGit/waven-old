// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.NamedEffectDefinition
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
  public sealed class NamedEffectDefinition : IEditableContent
  {
    private string m_name;
    private List<EffectDefinition> m_effects;

    public string name => this.m_name;

    public IReadOnlyList<EffectDefinition> effects => (IReadOnlyList<EffectDefinition>) this.m_effects;

    public override string ToString() => string.Format("{0} ({1} effets)", (object) this.m_name, (object) this.m_effects.Count);

    public static NamedEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (NamedEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      NamedEffectDefinition effectDefinition = new NamedEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static NamedEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      NamedEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : NamedEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_name = Serialization.JsonTokenValue<string>(jsonObject, "name");
      JArray jarray = Serialization.JsonArray(jsonObject, "effects");
      this.m_effects = new List<EffectDefinition>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_effects.Add(EffectDefinition.FromJsonToken(token));
    }
  }
}
