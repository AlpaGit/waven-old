// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DynamicValue
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
  public abstract class DynamicValue : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static DynamicValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DynamicValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class DynamicValue");
        return (DynamicValue) null;
      }
      string str = jtoken.Value<string>();
      DynamicValue dynamicValue;
      switch (str)
      {
        case "BoardEntityCaracValue":
          dynamicValue = (DynamicValue) new BoardEntityCaracValue();
          break;
        case "CharacterActionValue":
          dynamicValue = (DynamicValue) new CharacterActionValue();
          break;
        case "ClampValue":
          dynamicValue = (DynamicValue) new ClampValue();
          break;
        case "ConditionalValue":
          dynamicValue = (DynamicValue) new ConditionalValue();
          break;
        case "ConstIntLevelBasedDynamicValue":
          dynamicValue = (DynamicValue) new ConstIntLevelBasedDynamicValue();
          break;
        case "ConstIntegerValue":
          dynamicValue = (DynamicValue) new ConstIntegerValue();
          break;
        case "DistanceValue":
          dynamicValue = (DynamicValue) new DistanceValue();
          break;
        case "DynamicValueLevelBasedDynamicValue":
          dynamicValue = (DynamicValue) new DynamicValueLevelBasedDynamicValue();
          break;
        case "ElementCaracValue":
          dynamicValue = (DynamicValue) new ElementCaracValue();
          break;
        case "EntityCountValue":
          dynamicValue = (DynamicValue) new EntityCountValue();
          break;
        case "FloatingCounterValue":
          dynamicValue = (DynamicValue) new FloatingCounterValue();
          break;
        case "LinearLevelBasedDynamicValue":
          dynamicValue = (DynamicValue) new LinearLevelBasedDynamicValue();
          break;
        case "MultValue":
          dynamicValue = (DynamicValue) new MultValue();
          break;
        case "NegativeValue":
          dynamicValue = (DynamicValue) new NegativeValue();
          break;
        case "ReserveCaracValue":
          dynamicValue = (DynamicValue) new ReserveCaracValue();
          break;
        case "SimpleDiceValue":
          dynamicValue = (DynamicValue) new SimpleDiceValue();
          break;
        case "SpellsInHandValue":
          dynamicValue = (DynamicValue) new SpellsInHandValue();
          break;
        case "SpellsPlayedThisTurnValue":
          dynamicValue = (DynamicValue) new SpellsPlayedThisTurnValue();
          break;
        case "SumValue":
          dynamicValue = (DynamicValue) new SumValue();
          break;
        case "ThisSpellActionPointsCostValue":
          dynamicValue = (DynamicValue) new ThisSpellActionPointsCostValue();
          break;
        case "ThisSpellElementsPointsCostValue":
          dynamicValue = (DynamicValue) new ThisSpellElementsPointsCostValue();
          break;
        case "ThisSpellReservePointsCostValue":
          dynamicValue = (DynamicValue) new ThisSpellReservePointsCostValue();
          break;
        case "TotalActivationValueOfThisAssemblage":
          dynamicValue = (DynamicValue) new TotalActivationValueOfThisAssemblage();
          break;
        case "TriggeringEventValue":
          dynamicValue = (DynamicValue) new TriggeringEventValue();
          break;
        case "TriggeringMovementLengthValue":
          dynamicValue = (DynamicValue) new TriggeringMovementLengthValue();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (DynamicValue) null;
      }
      dynamicValue.PopulateFromJson(jsonObject);
      return dynamicValue;
    }

    public static DynamicValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DynamicValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DynamicValue.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    public abstract bool GetValue(DynamicValueContext context, out int value);

    public virtual bool ToString(DynamicValueContext context, out string value)
    {
      int num1;
      int num2 = this.GetValue(context, out num1) ? 1 : 0;
      value = num1.ToString();
      return num2 != 0;
    }
  }
}
