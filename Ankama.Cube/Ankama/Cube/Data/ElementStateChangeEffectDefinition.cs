// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementStateChangeEffectDefinition
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
  public sealed class ElementStateChangeEffectDefinition : EffectExecutionDefinition
  {
    private ElementaryStates m_elementaryState;

    public ElementaryStates elementaryState => this.m_elementaryState;

    public override string ToString() => "Change ElementaryState" + (this.m_condition == null ? "" : string.Format(" if {0}", (object) this.m_condition));

    public static ElementStateChangeEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ElementStateChangeEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ElementStateChangeEffectDefinition effectDefinition = new ElementStateChangeEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ElementStateChangeEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ElementStateChangeEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ElementStateChangeEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_elementaryState = (ElementaryStates) Serialization.JsonTokenValue<int>(jsonObject, "elementaryState");
    }
  }
}
