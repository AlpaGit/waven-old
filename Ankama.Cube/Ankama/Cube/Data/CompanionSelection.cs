// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CompanionSelection
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
  public sealed class CompanionSelection : AbstractInvocationSelection
  {
    private Id<CompanionDefinition> m_companionId;

    public Id<CompanionDefinition> companionId => this.m_companionId;

    public override string ToString() => this.CustomToString();

    public static CompanionSelection FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CompanionSelection) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CompanionSelection companionSelection = new CompanionSelection();
      companionSelection.PopulateFromJson(jsonObject);
      return companionSelection;
    }

    public static CompanionSelection FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CompanionSelection defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CompanionSelection.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_companionId = Serialization.JsonTokenIdValue<CompanionDefinition>(jsonObject, "companionId");
    }
  }
}
