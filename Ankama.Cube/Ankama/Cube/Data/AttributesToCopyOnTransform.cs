// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AttributesToCopyOnTransform
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
  public sealed class AttributesToCopyOnTransform : IEditableContent
  {
    private bool m_life;
    private bool m_armor;
    private bool m_elementaryState;
    private bool m_alreadyActioned;

    public bool life => this.m_life;

    public bool armor => this.m_armor;

    public bool elementaryState => this.m_elementaryState;

    public bool alreadyActioned => this.m_alreadyActioned;

    public override string ToString() => this.GetType().Name;

    public static AttributesToCopyOnTransform FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AttributesToCopyOnTransform) null;
      }
      JObject jsonObject = token.Value<JObject>();
      AttributesToCopyOnTransform toCopyOnTransform = new AttributesToCopyOnTransform();
      toCopyOnTransform.PopulateFromJson(jsonObject);
      return toCopyOnTransform;
    }

    public static AttributesToCopyOnTransform FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AttributesToCopyOnTransform defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AttributesToCopyOnTransform.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_life = Serialization.JsonTokenValue<bool>(jsonObject, "life");
      this.m_armor = Serialization.JsonTokenValue<bool>(jsonObject, "armor");
      this.m_elementaryState = Serialization.JsonTokenValue<bool>(jsonObject, "elementaryState");
      this.m_alreadyActioned = Serialization.JsonTokenValue<bool>(jsonObject, "alreadyActioned");
    }
  }
}
