// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.NotCondition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class NotCondition : EffectCondition
  {
    private EffectCondition m_condition;

    public EffectCondition condition => this.m_condition;

    public override string ToString() => string.Format("not ({0})", (object) this.condition);

    public static NotCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (NotCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      NotCondition notCondition = new NotCondition();
      notCondition.PopulateFromJson(jsonObject);
      return notCondition;
    }

    public static NotCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      NotCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : NotCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_condition = EffectCondition.FromJsonProperty(jsonObject, "condition");
    }

    public override bool IsValid(DynamicValueContext context) => !this.m_condition.IsValid(context);
  }
}
