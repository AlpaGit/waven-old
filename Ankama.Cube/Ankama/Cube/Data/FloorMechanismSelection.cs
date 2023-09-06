// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FloorMechanismSelection
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
  public sealed class FloorMechanismSelection : AbstractInvocationSelection
  {
    private Id<FloorMechanismDefinition> m_floorMechanismId;

    public Id<FloorMechanismDefinition> floorMechanismId => this.m_floorMechanismId;

    public override string ToString() => this.CustomToString();

    public static FloorMechanismSelection FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FloorMechanismSelection) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FloorMechanismSelection mechanismSelection = new FloorMechanismSelection();
      mechanismSelection.PopulateFromJson(jsonObject);
      return mechanismSelection;
    }

    public static FloorMechanismSelection FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FloorMechanismSelection defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FloorMechanismSelection.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_floorMechanismId = Serialization.JsonTokenIdValue<FloorMechanismDefinition>(jsonObject, "floorMechanismId");
    }
  }
}
