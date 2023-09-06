// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TransformationEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class TransformationEffectDefinition : EffectExecutionDefinition
  {
    private AbstractInvocationSelection m_transformInto;
    private AttributesToCopyOnTransform m_attributesToCopy;

    public AbstractInvocationSelection transformInto => this.m_transformInto;

    public AttributesToCopyOnTransform attributesToCopy => this.m_attributesToCopy;

    public override string ToString() => string.Format("Transform into {0}{1}", (object) this.m_transformInto, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static TransformationEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (TransformationEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      TransformationEffectDefinition effectDefinition = new TransformationEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static TransformationEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      TransformationEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : TransformationEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_transformInto = AbstractInvocationSelection.FromJsonProperty(jsonObject, "transformInto");
      this.m_attributesToCopy = AttributesToCopyOnTransform.FromJsonProperty(jsonObject, "attributesToCopy");
    }
  }
}
