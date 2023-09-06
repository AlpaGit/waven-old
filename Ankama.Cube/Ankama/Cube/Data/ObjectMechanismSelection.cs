// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ObjectMechanismSelection
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
  public sealed class ObjectMechanismSelection : AbstractInvocationSelection
  {
    private Id<ObjectMechanismDefinition> m_objectMechanismId;

    public Id<ObjectMechanismDefinition> objectMechanismId => this.m_objectMechanismId;

    public override string ToString() => this.CustomToString();

    public static ObjectMechanismSelection FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ObjectMechanismSelection) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ObjectMechanismSelection mechanismSelection = new ObjectMechanismSelection();
      mechanismSelection.PopulateFromJson(jsonObject);
      return mechanismSelection;
    }

    public static ObjectMechanismSelection FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ObjectMechanismSelection defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ObjectMechanismSelection.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_objectMechanismId = Serialization.JsonTokenIdValue<ObjectMechanismDefinition>(jsonObject, "objectMechanismId");
    }
  }
}
