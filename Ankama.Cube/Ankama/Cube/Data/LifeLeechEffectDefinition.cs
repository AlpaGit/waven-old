// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.LifeLeechEffectDefinition
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
  public sealed class LifeLeechEffectDefinition : DamageEffectDefinition
  {
    private bool m_physicalDamage;
    private ISingleEntitySelector m_source;
    private ISingleEntitySelector m_leecher;

    public bool physicalDamage => this.m_physicalDamage;

    public ISingleEntitySelector source => this.m_source;

    public ISingleEntitySelector leecher => this.m_leecher;

    public override string ToString() => string.Format("Leech life {0} on {1}{2}", (object) this.m_value, (object) this.m_executionTargetSelector, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static LifeLeechEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (LifeLeechEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      LifeLeechEffectDefinition effectDefinition = new LifeLeechEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static LifeLeechEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      LifeLeechEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : LifeLeechEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_physicalDamage = Serialization.JsonTokenValue<bool>(jsonObject, "physicalDamage");
      this.m_source = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "source");
      this.m_leecher = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "leecher");
    }
  }
}
