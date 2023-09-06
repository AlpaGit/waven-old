// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AgonyCondition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.LifeArmorChanged})]
  [Serializable]
  public sealed class AgonyCondition : EffectCondition
  {
    private static readonly CasterHeroSelector selector = new CasterHeroSelector();

    public override string ToString() => this.GetType().Name;

    public static AgonyCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AgonyCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      AgonyCondition agonyCondition = new AgonyCondition();
      agonyCondition.PopulateFromJson(jsonObject);
      return agonyCondition;
    }

    public static AgonyCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AgonyCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AgonyCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public override bool IsValid(DynamicValueContext context)
    {
      IEntityWithLife entity;
      return AgonyCondition.selector.TryGetEntity<IEntityWithLife>(context, out entity) && entity.life * 100 / entity.baseLife < RuntimeData.constantsDefinition.agonyThreshold;
    }
  }
}
