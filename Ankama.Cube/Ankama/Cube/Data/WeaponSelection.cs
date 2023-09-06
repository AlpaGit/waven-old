// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.WeaponSelection
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
  public sealed class WeaponSelection : AbstractInvocationSelection
  {
    private Id<WeaponDefinition> m_weaponId;

    public Id<WeaponDefinition> weaponId => this.m_weaponId;

    public override string ToString() => this.CustomToString();

    public static WeaponSelection FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (WeaponSelection) null;
      }
      JObject jsonObject = token.Value<JObject>();
      WeaponSelection weaponSelection = new WeaponSelection();
      weaponSelection.PopulateFromJson(jsonObject);
      return weaponSelection;
    }

    public static WeaponSelection FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      WeaponSelection defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : WeaponSelection.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_weaponId = Serialization.JsonTokenIdValue<WeaponDefinition>(jsonObject, "weaponId");
    }
  }
}
