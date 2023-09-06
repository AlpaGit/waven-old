// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MoveInLineEffectDefinition
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
  public sealed class MoveInLineEffectDefinition : EffectExecutionDefinition
  {
    private DynamicValue m_cellCount;
    private MoveVector m_direction;
    private MovementType m_movementType;
    private bool m_canPassThroughEntities;

    public DynamicValue cellCount => this.m_cellCount;

    public MoveVector direction => this.m_direction;

    public MovementType movementType => this.m_movementType;

    public bool canPassThroughEntities => this.m_canPassThroughEntities;

    public override string ToString() => string.Format("Move {0} {1} of {2} cells in line {3}", (object) this.m_executionTargetSelector, (object) this.direction, (object) this.cellCount, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static MoveInLineEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (MoveInLineEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      MoveInLineEffectDefinition effectDefinition = new MoveInLineEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static MoveInLineEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      MoveInLineEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : MoveInLineEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_cellCount = DynamicValue.FromJsonProperty(jsonObject, "cellCount");
      this.m_direction = MoveVector.FromJsonProperty(jsonObject, "direction");
      this.m_movementType = (MovementType) Serialization.JsonTokenValue<int>(jsonObject, "movementType", 6);
      this.m_canPassThroughEntities = Serialization.JsonTokenValue<bool>(jsonObject, "canPassThroughEntities");
    }
  }
}
