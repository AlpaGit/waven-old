// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TeleportEffectDefinition
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
  public sealed class TeleportEffectDefinition : EffectExecutionDefinition
  {
    private ISingleCoordSelector m_destination;
    private MovementType m_movementType;
    private bool m_allowTeleportOnTargetEntity;

    public ISingleCoordSelector destination => this.m_destination;

    public MovementType movementType => this.m_movementType;

    public bool allowTeleportOnTargetEntity => this.m_allowTeleportOnTargetEntity;

    public override string ToString() => string.Format("TP {0} to {1}{2}", (object) this.m_executionTargetSelector, (object) this.destination, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static TeleportEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (TeleportEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      TeleportEffectDefinition effectDefinition = new TeleportEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static TeleportEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      TeleportEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : TeleportEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_destination = ISingleCoordSelectorUtils.FromJsonProperty(jsonObject, "destination");
      this.m_movementType = (MovementType) Serialization.JsonTokenValue<int>(jsonObject, "movementType", 3);
      this.m_allowTeleportOnTargetEntity = Serialization.JsonTokenValue<bool>(jsonObject, "allowTeleportOnTargetEntity");
    }
  }
}
