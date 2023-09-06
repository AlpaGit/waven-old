// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TotalActivationValueOfThisAssemblage
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class TotalActivationValueOfThisAssemblage : DynamicValue
  {
    public override string ToString() => this.GetType().Name;

    public static TotalActivationValueOfThisAssemblage FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (TotalActivationValueOfThisAssemblage) null;
      }
      JObject jsonObject = token.Value<JObject>();
      TotalActivationValueOfThisAssemblage ofThisAssemblage = new TotalActivationValueOfThisAssemblage();
      ofThisAssemblage.PopulateFromJson(jsonObject);
      return ofThisAssemblage;
    }

    public static TotalActivationValueOfThisAssemblage FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      TotalActivationValueOfThisAssemblage defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : TotalActivationValueOfThisAssemblage.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      if (!(context is AssembledEntityContext assembledEntityContext))
      {
        value = 0;
        return false;
      }
      int num1 = 0;
      IReadOnlyList<int> assemblingIds = assembledEntityContext.assembling.assemblingIds;
      FightStatus fightStatus = assembledEntityContext.fightStatus;
      int num2 = 0;
      for (int count = ((IReadOnlyCollection<int>) assemblingIds).Count; num2 < count; ++num2)
      {
        FloorMechanismStatus entityStatus;
        if (fightStatus.TryGetEntity<FloorMechanismStatus>(assemblingIds[num2], out entityStatus))
        {
          int? activationValue = entityStatus.activationValue;
          if (activationValue.HasValue)
            num1 += activationValue.Value;
        }
      }
      value = num1;
      return true;
    }
  }
}
