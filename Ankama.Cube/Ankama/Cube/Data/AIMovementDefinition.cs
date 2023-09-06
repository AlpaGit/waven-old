// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AIMovementDefinition
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
  public abstract class AIMovementDefinition : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static AIMovementDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AIMovementDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class AIMovementDefinition");
        return (AIMovementDefinition) null;
      }
      string str = jtoken.Value<string>();
      AIMovementDefinition movementDefinition;
      switch (str)
      {
        case "DoNothing":
          movementDefinition = (AIMovementDefinition) new AIMovementDefinition.DoNothing();
          break;
        case "GetCloserTo":
          movementDefinition = (AIMovementDefinition) new AIMovementDefinition.GetCloserTo();
          break;
        case "StayOutOfAttack":
          movementDefinition = (AIMovementDefinition) new AIMovementDefinition.StayOutOfAttack();
          break;
        case "RunawayFrom":
          movementDefinition = (AIMovementDefinition) new AIMovementDefinition.RunawayFrom();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (AIMovementDefinition) null;
      }
      movementDefinition.PopulateFromJson(jsonObject);
      return movementDefinition;
    }

    public static AIMovementDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AIMovementDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AIMovementDefinition.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    [Serializable]
    public sealed class DoNothing : AIMovementDefinition
    {
      public override string ToString() => this.GetType().Name;

      public static AIMovementDefinition.DoNothing FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AIMovementDefinition.DoNothing) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AIMovementDefinition.DoNothing doNothing = new AIMovementDefinition.DoNothing();
        doNothing.PopulateFromJson(jsonObject);
        return doNothing;
      }

      public static AIMovementDefinition.DoNothing FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AIMovementDefinition.DoNothing defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AIMovementDefinition.DoNothing.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class GetCloserTo : AIMovementDefinition
    {
      private AITargets m_targets;

      public AITargets targets => this.m_targets;

      public override string ToString() => string.Format("GetCloserTo {0}", (object) this.m_targets);

      public static AIMovementDefinition.GetCloserTo FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AIMovementDefinition.GetCloserTo) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AIMovementDefinition.GetCloserTo getCloserTo = new AIMovementDefinition.GetCloserTo();
        getCloserTo.PopulateFromJson(jsonObject);
        return getCloserTo;
      }

      public static AIMovementDefinition.GetCloserTo FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AIMovementDefinition.GetCloserTo defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AIMovementDefinition.GetCloserTo.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targets = AITargets.FromJsonProperty(jsonObject, "targets");
      }
    }

    [Serializable]
    public sealed class RunawayFrom : AIMovementDefinition
    {
      private AITargets m_targets;

      public AITargets targets => this.m_targets;

      public override string ToString() => string.Format("RunawayFrom {0}", (object) this.m_targets);

      public static AIMovementDefinition.RunawayFrom FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AIMovementDefinition.RunawayFrom) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AIMovementDefinition.RunawayFrom runawayFrom = new AIMovementDefinition.RunawayFrom();
        runawayFrom.PopulateFromJson(jsonObject);
        return runawayFrom;
      }

      public static AIMovementDefinition.RunawayFrom FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AIMovementDefinition.RunawayFrom defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AIMovementDefinition.RunawayFrom.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targets = AITargets.FromJsonProperty(jsonObject, "targets");
      }
    }

    [Serializable]
    public sealed class StayOutOfAttack : AIMovementDefinition
    {
      private AITargets m_targets;

      public AITargets targets => this.m_targets;

      public override string ToString() => string.Format("StayOutOfAttack {0}", (object) this.m_targets);

      public static AIMovementDefinition.StayOutOfAttack FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AIMovementDefinition.StayOutOfAttack) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AIMovementDefinition.StayOutOfAttack stayOutOfAttack = new AIMovementDefinition.StayOutOfAttack();
        stayOutOfAttack.PopulateFromJson(jsonObject);
        return stayOutOfAttack;
      }

      public static AIMovementDefinition.StayOutOfAttack FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AIMovementDefinition.StayOutOfAttack defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AIMovementDefinition.StayOutOfAttack.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_targets = AITargets.FromJsonProperty(jsonObject, "targets");
      }
    }
  }
}
