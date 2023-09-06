// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CompanionAvailability
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
  public sealed class CompanionAvailability : IEditableContent
  {
    private Id<CompanionDefinition> m_companion;
    private DataAvailability m_availability;

    public Id<CompanionDefinition> companion => this.m_companion;

    public DataAvailability availability => this.m_availability;

    public override string ToString() => string.Format("{0} {1}", (object) this.m_companion, (object) this.m_availability);

    public static CompanionAvailability FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CompanionAvailability) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CompanionAvailability companionAvailability = new CompanionAvailability();
      companionAvailability.PopulateFromJson(jsonObject);
      return companionAvailability;
    }

    public static CompanionAvailability FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CompanionAvailability defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CompanionAvailability.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_companion = Serialization.JsonTokenIdValue<CompanionDefinition>(jsonObject, "companion");
      this.m_availability = (DataAvailability) Serialization.JsonTokenValue<int>(jsonObject, "availability");
    }
  }
}
