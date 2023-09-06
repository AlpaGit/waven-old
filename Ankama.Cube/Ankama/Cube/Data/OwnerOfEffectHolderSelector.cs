// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.OwnerOfEffectHolderSelector
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
  public sealed class OwnerOfEffectHolderSelector : 
    EntitySelectorForCast,
    ISingleEntitySelector,
    IEntitySelector,
    ITargetSelector,
    IEditableContent,
    ISingleTargetSelector
  {
    public override string ToString() => this.GetType().Name;

    public static OwnerOfEffectHolderSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (OwnerOfEffectHolderSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      OwnerOfEffectHolderSelector effectHolderSelector = new OwnerOfEffectHolderSelector();
      effectHolderSelector.PopulateFromJson(jsonObject);
      return effectHolderSelector;
    }

    public static OwnerOfEffectHolderSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      OwnerOfEffectHolderSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : OwnerOfEffectHolderSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public override IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      IEntity entity;
      if (this.TryGetEntity<IEntity>(context, out entity))
        yield return entity;
    }

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      if (context is CharacterActionValueContext actionValueContext)
      {
        int ownerId = actionValueContext.relatedCharacterStatus.ownerId;
        return actionValueContext.fightStatus.TryGetEntity<T>(ownerId, out entity);
      }
      Log.Error(string.Format("Cannot use Owner Of Effect Holder in {0}", (object) context), 29, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\TargetSelectors\\OwnerOfEffectHolderSelector.cs");
      entity = default (T);
      return false;
    }
  }
}
