// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UniqueCondition
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
  public sealed class UniqueCondition : EffectCondition
  {
    private ISpecificEntityFilter m_specificEntity;
    private static readonly FilteredEntitySelector selector = FilteredEntitySelector.From((IEntityFilter) OwnerFilter.sameAsCaster);

    public ISpecificEntityFilter specificEntity => this.m_specificEntity;

    public override string ToString() => this.GetType().Name;

    public static UniqueCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (UniqueCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      UniqueCondition uniqueCondition = new UniqueCondition();
      uniqueCondition.PopulateFromJson(jsonObject);
      return uniqueCondition;
    }

    public static UniqueCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      UniqueCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : UniqueCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_specificEntity = ISpecificEntityFilterUtils.FromJsonProperty(jsonObject, "specificEntity");
    }

    public override bool IsValid(DynamicValueContext context)
    {
      foreach (IEntity enumerateEntity in UniqueCondition.selector.EnumerateEntities(context))
      {
        if (this.m_specificEntity.ValidFor(enumerateEntity))
          return false;
      }
      return true;
    }
  }
}
