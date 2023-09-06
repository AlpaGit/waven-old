// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PropertyChangeEffectDefinition
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
  public sealed class PropertyChangeEffectDefinition : EffectExecutionWithDurationDefinition
  {
    private PropertyId m_propertyId;

    public PropertyId propertyId => this.m_propertyId;

    public override string ToString() => string.Format("set Property {0}{1}", (object) this.m_propertyId, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static PropertyChangeEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PropertyChangeEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PropertyChangeEffectDefinition effectDefinition = new PropertyChangeEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static PropertyChangeEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PropertyChangeEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PropertyChangeEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_propertyId = (PropertyId) Serialization.JsonTokenValue<int>(jsonObject, "propertyId");
    }
  }
}
