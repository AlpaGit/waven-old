// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ReferenceEffectDefinition
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
  public sealed class ReferenceEffectDefinition : AbstractEffectDefinition
  {
    private string m_name;

    public string name => this.m_name;

    public override string ToString() => "Reference: " + this.m_name;

    public static ReferenceEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ReferenceEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ReferenceEffectDefinition effectDefinition = new ReferenceEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ReferenceEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ReferenceEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ReferenceEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_name = Serialization.JsonTokenValue<string>(jsonObject, "name");
    }
  }
}
