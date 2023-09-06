// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.GodAvailability
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
  public sealed class GodAvailability : IEditableContent
  {
    private God m_god;
    private DataAvailability m_availability;

    public God god => this.m_god;

    public DataAvailability availability => this.m_availability;

    public override string ToString() => string.Format("{0} {1}", (object) this.m_god, (object) this.m_availability);

    public static GodAvailability FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (GodAvailability) null;
      }
      JObject jsonObject = token.Value<JObject>();
      GodAvailability godAvailability = new GodAvailability();
      godAvailability.PopulateFromJson(jsonObject);
      return godAvailability;
    }

    public static GodAvailability FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      GodAvailability defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : GodAvailability.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_god = (God) Serialization.JsonTokenValue<int>(jsonObject, "god");
      this.m_availability = (DataAvailability) Serialization.JsonTokenValue<int>(jsonObject, "availability");
    }
  }
}
