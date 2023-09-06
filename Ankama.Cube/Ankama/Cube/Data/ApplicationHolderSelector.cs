// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ApplicationHolderSelector
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
  public sealed class ApplicationHolderSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleEntitySelector,
    IEntitySelector,
    ISingleTargetSelector
  {
    public override string ToString() => this.GetType().Name;

    public static ApplicationHolderSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ApplicationHolderSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ApplicationHolderSelector applicationHolderSelector = new ApplicationHolderSelector();
      applicationHolderSelector.PopulateFromJson(jsonObject);
      return applicationHolderSelector;
    }

    public static ApplicationHolderSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ApplicationHolderSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ApplicationHolderSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      if (context.type == DynamicValueHolderType.CharacterAction)
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
          Log.Error("Cannot use ApplicationHolder when cast target context is " + context.GetType().Name + ".", 26, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\TargetSelectors\\ApplicationHolderSelector.cs");
      }
      else
        Log.Error(string.Format("Cannot use ApplicationHolder when cast object type is {0} ({1}).", (object) context.type, (object) context.GetType().Name), 31, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\TargetSelectors\\ApplicationHolderSelector.cs");
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
