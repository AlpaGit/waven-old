// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SwapPositionsEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SwapPositionsEffectDefinition : EffectExecutionDefinition
  {
    private ISingleEntitySelector m_entityToSwapWith;

    public ISingleEntitySelector entityToSwapWith => this.m_entityToSwapWith;

    public override string ToString() => string.Format("Swap position with {0}{1}", (object) this.entityToSwapWith, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static SwapPositionsEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SwapPositionsEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SwapPositionsEffectDefinition effectDefinition = new SwapPositionsEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static SwapPositionsEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SwapPositionsEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SwapPositionsEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_entityToSwapWith = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "entityToSwapWith");
    }
  }
}
