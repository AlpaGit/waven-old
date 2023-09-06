// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.StealCaracEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class StealCaracEffectDefinition : EffectExecutionDefinition
  {
    private DynamicValue m_quantity;
    private ISingleCaracIdSelector m_caracSelector;
    private IEntitySelector m_from;

    public DynamicValue quantity => this.m_quantity;

    public ISingleCaracIdSelector caracSelector => this.m_caracSelector;

    public IEntitySelector from => this.m_from;

    public override string ToString() => this.GetType().Name;

    public static StealCaracEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (StealCaracEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      StealCaracEffectDefinition effectDefinition = new StealCaracEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static StealCaracEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      StealCaracEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : StealCaracEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_quantity = DynamicValue.FromJsonProperty(jsonObject, "quantity");
      this.m_caracSelector = ISingleCaracIdSelectorUtils.FromJsonProperty(jsonObject, "caracSelector");
      this.m_from = IEntitySelectorUtils.FromJsonProperty(jsonObject, "from");
    }
  }
}
