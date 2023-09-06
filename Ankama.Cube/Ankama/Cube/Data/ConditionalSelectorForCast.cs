// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ConditionalSelectorForCast
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ConditionalSelectorForCast : 
    IEditableContent,
    ITargetSelector,
    ISelectorForCast
  {
    private EffectCondition m_condition;
    private ISelectorForCast m_selectorIfTrue;
    private ISelectorForCast m_selectorIfFalse;

    public EffectCondition condition => this.m_condition;

    public ISelectorForCast selectorIfTrue => this.m_selectorIfTrue;

    public ISelectorForCast selectorIfFalse => this.m_selectorIfFalse;

    public override string ToString() => this.GetType().Name;

    public static ConditionalSelectorForCast FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ConditionalSelectorForCast) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ConditionalSelectorForCast conditionalSelectorForCast = new ConditionalSelectorForCast();
      conditionalSelectorForCast.PopulateFromJson(jsonObject);
      return conditionalSelectorForCast;
    }

    public static ConditionalSelectorForCast FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ConditionalSelectorForCast defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ConditionalSelectorForCast.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_condition = EffectCondition.FromJsonProperty(jsonObject, "condition");
      this.m_selectorIfTrue = ISelectorForCastUtils.FromJsonProperty(jsonObject, "selectorIfTrue");
      this.m_selectorIfFalse = ISelectorForCastUtils.FromJsonProperty(jsonObject, "selectorIfFalse");
    }

    public IEnumerable<Target> EnumerateTargets(DynamicValueContext context)
    {
      ISelectorForCast selectorForCast = this.m_condition.IsValid(context) ? this.m_selectorIfTrue : this.m_selectorIfFalse;
      return selectorForCast == null ? ConditionalSelectorForCast.Empty() : selectorForCast.EnumerateTargets(context);
    }

    private static IEnumerable<Target> Empty()
    {
      yield break;
    }
  }
}
