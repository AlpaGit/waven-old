// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class EffectDefinition : AbstractEffectDefinition
  {
    private AbstractEffectApplicationDefinition m_application;
    private AbstractEffectExecutionDefinition m_execution;

    public AbstractEffectApplicationDefinition application => this.m_application;

    public AbstractEffectExecutionDefinition execution => this.m_execution;

    public override string ToString() => this.m_execution.ToString().Length != 0 ? this.m_execution.ToString() : "[[" + this.m_execution.GetType().Name.Replace(nameof (EffectDefinition), "") + "]]";

    public static EffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EffectDefinition effectDefinition = new EffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static EffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_application = AbstractEffectApplicationDefinition.FromJsonProperty(jsonObject, "application");
      this.m_execution = AbstractEffectExecutionDefinition.FromJsonProperty(jsonObject, "execution");
    }
  }
}
