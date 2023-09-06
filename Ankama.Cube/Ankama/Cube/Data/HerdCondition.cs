// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.HerdCondition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class HerdCondition : EffectCondition
  {
    private DynamicValue m_count;
    private static readonly FilteredEntitySelector selector = FilteredEntitySelector.From((IEntityFilter) OwnerFilter.sameAsCaster, (IEntityFilter) new OsamodasAnimalsEntityFilter());

    public DynamicValue count => this.m_count;

    public override string ToString() => string.Format("Herd({0})", (object) this.m_count);

    public static HerdCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (HerdCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      HerdCondition herdCondition = new HerdCondition();
      herdCondition.PopulateFromJson(jsonObject);
      return herdCondition;
    }

    public static HerdCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      HerdCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : HerdCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_count = DynamicValue.FromJsonProperty(jsonObject, "count");
    }

    public override bool IsValid(DynamicValueContext context)
    {
      int num1;
      this.count.GetValue(context, out num1);
      int num2 = 0;
      foreach (IEntity enumerateEntity in HerdCondition.selector.EnumerateEntities(context))
      {
        if (num2 >= num1)
          return true;
      }
      return false;
    }
  }
}
