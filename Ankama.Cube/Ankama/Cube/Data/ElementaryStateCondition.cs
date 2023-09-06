// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementaryStateCondition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ElementaryStateCondition : EffectCondition
  {
    private ISingleEntitySelector m_selector;
    private ElementaryStates m_elementaryState;

    public ISingleEntitySelector selector => this.m_selector;

    public ElementaryStates elementaryState => this.m_elementaryState;

    public override string ToString() => string.Format("{0} is {1}", (object) this.m_selector, (object) this.m_elementaryState);

    public static ElementaryStateCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ElementaryStateCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ElementaryStateCondition elementaryStateCondition = new ElementaryStateCondition();
      elementaryStateCondition.PopulateFromJson(jsonObject);
      return elementaryStateCondition;
    }

    public static ElementaryStateCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ElementaryStateCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ElementaryStateCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_selector = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "selector");
      this.m_elementaryState = (ElementaryStates) Serialization.JsonTokenValue<int>(jsonObject, "elementaryState");
    }

    public override bool IsValid(DynamicValueContext context) => this.selector.EnumerateEntities(context).All<IEntity>((Func<IEntity, bool>) (e => e is IEntityWithElementaryState withElementaryState && withElementaryState.HasElementaryState(this.elementaryState)));
  }
}
