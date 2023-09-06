// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.InvokeCreatureEffectDefinition
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
  public sealed class InvokeCreatureEffectDefinition : EffectExecutionDefinition
  {
    private AbstractInvocationSelection m_selection;
    private ISingleEntitySelector m_invocationOwner;
    private bool m_canBeInvokedOnOtherEntity;

    public AbstractInvocationSelection selection => this.m_selection;

    public ISingleEntitySelector invocationOwner => this.m_invocationOwner;

    public bool canBeInvokedOnOtherEntity => this.m_canBeInvokedOnOtherEntity;

    public override string ToString() => string.Format("Invoke {0}{1}", (object) this.m_selection, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static InvokeCreatureEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (InvokeCreatureEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      InvokeCreatureEffectDefinition effectDefinition = new InvokeCreatureEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static InvokeCreatureEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      InvokeCreatureEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : InvokeCreatureEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_selection = AbstractInvocationSelection.FromJsonProperty(jsonObject, "selection");
      this.m_invocationOwner = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "invocationOwner");
      this.m_canBeInvokedOnOtherEntity = Serialization.JsonTokenValue<bool>(jsonObject, "canBeInvokedOnOtherEntity");
    }
  }
}
