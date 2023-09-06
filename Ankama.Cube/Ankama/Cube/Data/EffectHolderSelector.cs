// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EffectHolderSelector
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
  public sealed class EffectHolderSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleEntitySelector,
    IEntitySelector,
    ISingleTargetSelector
  {
    public override string ToString() => this.GetType().Name;

    public static EffectHolderSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EffectHolderSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EffectHolderSelector effectHolderSelector = new EffectHolderSelector();
      effectHolderSelector.PopulateFromJson(jsonObject);
      return effectHolderSelector;
    }

    public static EffectHolderSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EffectHolderSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectHolderSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      if (context is CharacterActionValueContext actionValueContext)
      {
        if (actionValueContext.relatedCharacterStatus is T relatedCharacterStatus)
        {
          entity = relatedCharacterStatus;
          return true;
        }
      }
      else
        Log.Error(string.Format("Cannot use Effect Holder in {0}", (object) context), 23, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\TargetSelectors\\EffectHolderSelector.cs");
      entity = default (T);
      return false;
    }

    public IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      IEntity entity;
      if (this.TryGetEntity<IEntity>(context, out entity))
        yield return entity;
    }
  }
}
