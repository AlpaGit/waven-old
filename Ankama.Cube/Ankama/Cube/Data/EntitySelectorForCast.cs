// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntitySelectorForCast
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Utilities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class EntitySelectorForCast : 
    IEditableContent,
    ITargetSelector,
    ISelectorForCast,
    IEntitySelector
  {
    public override string ToString() => this.GetType().Name;

    public static EntitySelectorForCast FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EntitySelectorForCast) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class EntitySelectorForCast");
        return (EntitySelectorForCast) null;
      }
      string str = jtoken.Value<string>();
      EntitySelectorForCast entitySelectorForCast;
      switch (str)
      {
        case "AllEntitiesSelector":
          entitySelectorForCast = (EntitySelectorForCast) new AllEntitiesSelector();
          break;
        case "CasterHeroSelector":
          entitySelectorForCast = (EntitySelectorForCast) new CasterHeroSelector();
          break;
        case "CasterSelector":
          entitySelectorForCast = (EntitySelectorForCast) new CasterSelector();
          break;
        case "FilteredEntitySelector":
          entitySelectorForCast = (EntitySelectorForCast) new FilteredEntitySelector();
          break;
        case "OwnerOfEffectHolderSelector":
          entitySelectorForCast = (EntitySelectorForCast) new OwnerOfEffectHolderSelector();
          break;
        case "OwnerOfSelector":
          entitySelectorForCast = (EntitySelectorForCast) new OwnerOfSelector();
          break;
        case "UnionOfEntitiesSelector":
          entitySelectorForCast = (EntitySelectorForCast) new UnionOfEntitiesSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (EntitySelectorForCast) null;
      }
      entitySelectorForCast.PopulateFromJson(jsonObject);
      return entitySelectorForCast;
    }

    public static EntitySelectorForCast FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EntitySelectorForCast defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EntitySelectorForCast.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    public abstract IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context);

    public IEnumerable<Target> EnumerateTargets(DynamicValueContext context)
    {
      EntitySelectorForCast entitySelectorForCast = this;
      int casterId = -1;
      foreach (IEntity enumerateEntity in entitySelectorForCast.EnumerateEntities(context))
      {
        if (!enumerateEntity.HasProperty(PropertyId.Untargetable))
        {
          yield return new Target(enumerateEntity);
        }
        else
        {
          if (casterId == -1)
          {
            casterId = 0;
            if (context is DynamicValueFightContext valueFightContext)
            {
              PlayerStatus entityStatus;
              if (valueFightContext.fightStatus.TryGetEntity<PlayerStatus>(valueFightContext.playerId, out entityStatus))
              {
                casterId = entityStatus.id;
              }
              else
              {
                Log.Warning("Selector " + entitySelectorForCast.GetType().Name + " tried to retrieve caster but could not find it.", 41, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\TargetSelectors\\ISelectorForCast.cs");
                continue;
              }
            }
            else
            {
              Log.Warning("Selector " + entitySelectorForCast.GetType().Name + " tried to retrieve caster but wasn't given a compatible context.", 47, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\TargetSelectors\\ISelectorForCast.cs");
              continue;
            }
          }
          if (enumerateEntity is IEntityWithOwner entityWithOwner && entityWithOwner.ownerId == casterId)
            yield return new Target(enumerateEntity);
        }
      }
    }
  }
}
