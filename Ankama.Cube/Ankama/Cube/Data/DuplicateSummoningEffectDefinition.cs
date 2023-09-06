// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DuplicateSummoningEffectDefinition
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
  public sealed class DuplicateSummoningEffectDefinition : EffectExecutionDefinition
  {
    private ISingleEntitySelector m_invocationOwner;
    private ISingleCoordSelector m_destination;
    private bool? m_copyNonHealableLifeValue;

    public ISingleEntitySelector invocationOwner => this.m_invocationOwner;

    public ISingleCoordSelector destination => this.m_destination;

    public bool? copyNonHealableLifeValue => this.m_copyNonHealableLifeValue;

    public override string ToString() => this.GetType().Name;

    public static DuplicateSummoningEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DuplicateSummoningEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DuplicateSummoningEffectDefinition effectDefinition = new DuplicateSummoningEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static DuplicateSummoningEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DuplicateSummoningEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DuplicateSummoningEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_invocationOwner = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "invocationOwner");
      this.m_destination = ISingleCoordSelectorUtils.FromJsonProperty(jsonObject, "destination");
      this.m_copyNonHealableLifeValue = Serialization.JsonTokenValue<bool?>(jsonObject, "copyNonHealableLifeValue");
    }
  }
}
