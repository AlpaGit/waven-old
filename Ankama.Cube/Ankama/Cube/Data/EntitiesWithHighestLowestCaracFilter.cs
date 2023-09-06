// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntitiesWithHighestLowestCaracFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class EntitiesWithHighestLowestCaracFilter : 
    IEditableContent,
    IEntityFilter,
    ITargetFilter
  {
    private Superlative m_superlative;
    private CaracId m_carac;

    public Superlative superlative => this.m_superlative;

    public CaracId carac => this.m_carac;

    public override string ToString() => this.GetType().Name;

    public static EntitiesWithHighestLowestCaracFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EntitiesWithHighestLowestCaracFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EntitiesWithHighestLowestCaracFilter lowestCaracFilter = new EntitiesWithHighestLowestCaracFilter();
      lowestCaracFilter.PopulateFromJson(jsonObject);
      return lowestCaracFilter;
    }

    public static EntitiesWithHighestLowestCaracFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EntitiesWithHighestLowestCaracFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EntitiesWithHighestLowestCaracFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_superlative = (Superlative) Serialization.JsonTokenValue<int>(jsonObject, "superlative", 1);
      this.m_carac = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "carac", 1);
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      IEntity[] array = entities.ToArray<IEntity>();
      List<IEntity> entityList = new List<IEntity>();
      switch (this.m_superlative)
      {
        case Superlative.Smallest:
          int num1 = int.MaxValue;
          foreach (IEntity entity in array)
          {
            int carac = entity.GetCarac(this.carac);
            if (carac <= num1)
            {
              if (carac == num1)
              {
                entityList.Add(entity);
              }
              else
              {
                entityList.Clear();
                entityList.Add(entity);
                num1 = carac;
              }
            }
          }
          break;
        case Superlative.Biggest:
          int num2 = int.MinValue;
          foreach (IEntity entity in array)
          {
            int carac = entity.GetCarac(this.carac);
            if (carac >= num2)
            {
              if (carac == num2)
              {
                entityList.Add(entity);
              }
              else
              {
                entityList.Clear();
                entityList.Add(entity);
                num2 = carac;
              }
            }
          }
          break;
        default:
          Debug.LogError((object) "Unknown Superlative: {m_superlative");
          break;
      }
      return (IEnumerable<IEntity>) entityList;
    }
  }
}
