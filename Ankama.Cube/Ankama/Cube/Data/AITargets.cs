// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AITargets
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
  public abstract class AITargets : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static AITargets FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AITargets) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class AITargets");
        return (AITargets) null;
      }
      string str = jtoken.Value<string>();
      AITargets aiTargets;
      switch (str)
      {
        case "Nothing":
          aiTargets = (AITargets) new AITargets.Nothing();
          break;
        case "All":
          aiTargets = (AITargets) new AITargets.All();
          break;
        case "Allies":
          aiTargets = (AITargets) new AITargets.Allies();
          break;
        case "AlliesWounded":
          aiTargets = (AITargets) new AITargets.AlliesWounded();
          break;
        case "Opponents":
          aiTargets = (AITargets) new AITargets.Opponents();
          break;
        case "SameAsActionTargets":
          aiTargets = (AITargets) new AITargets.SameAsActionTargets();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (AITargets) null;
      }
      aiTargets.PopulateFromJson(jsonObject);
      return aiTargets;
    }

    public static AITargets FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AITargets defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AITargets.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    [Serializable]
    public sealed class All : AITargets, IAIActionTargets, IEditableContent
    {
      public override string ToString() => this.GetType().Name;

      public static AITargets.All FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AITargets.All) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AITargets.All all = new AITargets.All();
        all.PopulateFromJson(jsonObject);
        return all;
      }

      public static AITargets.All FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AITargets.All defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AITargets.All.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class Allies : AITargets, IAIActionTargets, IEditableContent
    {
      public override string ToString() => this.GetType().Name;

      public static AITargets.Allies FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AITargets.Allies) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AITargets.Allies allies = new AITargets.Allies();
        allies.PopulateFromJson(jsonObject);
        return allies;
      }

      public static AITargets.Allies FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AITargets.Allies defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AITargets.Allies.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class AlliesWounded : AITargets, IAIActionTargets, IEditableContent
    {
      public override string ToString() => this.GetType().Name;

      public static AITargets.AlliesWounded FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AITargets.AlliesWounded) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AITargets.AlliesWounded alliesWounded = new AITargets.AlliesWounded();
        alliesWounded.PopulateFromJson(jsonObject);
        return alliesWounded;
      }

      public static AITargets.AlliesWounded FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AITargets.AlliesWounded defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AITargets.AlliesWounded.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class Nothing : AITargets, IAIActionTargets, IEditableContent
    {
      public override string ToString() => this.GetType().Name;

      public static AITargets.Nothing FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AITargets.Nothing) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AITargets.Nothing nothing = new AITargets.Nothing();
        nothing.PopulateFromJson(jsonObject);
        return nothing;
      }

      public static AITargets.Nothing FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AITargets.Nothing defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AITargets.Nothing.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class Opponents : AITargets, IAIActionTargets, IEditableContent
    {
      public override string ToString() => this.GetType().Name;

      public static AITargets.Opponents FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AITargets.Opponents) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AITargets.Opponents opponents = new AITargets.Opponents();
        opponents.PopulateFromJson(jsonObject);
        return opponents;
      }

      public static AITargets.Opponents FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AITargets.Opponents defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AITargets.Opponents.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }

    [Serializable]
    public sealed class SameAsActionTargets : AITargets
    {
      public override string ToString() => this.GetType().Name;

      public static AITargets.SameAsActionTargets FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (AITargets.SameAsActionTargets) null;
        }
        JObject jsonObject = token.Value<JObject>();
        AITargets.SameAsActionTargets sameAsActionTargets = new AITargets.SameAsActionTargets();
        sameAsActionTargets.PopulateFromJson(jsonObject);
        return sameAsActionTargets;
      }

      public static AITargets.SameAsActionTargets FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        AITargets.SameAsActionTargets defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AITargets.SameAsActionTargets.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
    }
  }
}
