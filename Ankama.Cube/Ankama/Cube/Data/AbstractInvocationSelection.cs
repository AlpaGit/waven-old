// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AbstractInvocationSelection
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
  public abstract class AbstractInvocationSelection : IEditableContent
  {
    protected string m_referenceId;

    public string referenceId => this.m_referenceId;

    public override string ToString() => this.GetType().Name;

    public static AbstractInvocationSelection FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AbstractInvocationSelection) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class AbstractInvocationSelection");
        return (AbstractInvocationSelection) null;
      }
      string str = jtoken.Value<string>();
      AbstractInvocationSelection invocationSelection;
      switch (str)
      {
        case "SummonSelection":
          invocationSelection = (AbstractInvocationSelection) new SummonSelection();
          break;
        case "CompanionSelection":
          invocationSelection = (AbstractInvocationSelection) new CompanionSelection();
          break;
        case "WeaponSelection":
          invocationSelection = (AbstractInvocationSelection) new WeaponSelection();
          break;
        case "FloorMechanismSelection":
          invocationSelection = (AbstractInvocationSelection) new FloorMechanismSelection();
          break;
        case "ObjectMechanismSelection":
          invocationSelection = (AbstractInvocationSelection) new ObjectMechanismSelection();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (AbstractInvocationSelection) null;
      }
      invocationSelection.PopulateFromJson(jsonObject);
      return invocationSelection;
    }

    public static AbstractInvocationSelection FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AbstractInvocationSelection defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AbstractInvocationSelection.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject) => this.m_referenceId = Serialization.JsonTokenValue<string>(jsonObject, "referenceId", "");

    public string CustomToString() => this.GetType().Name;
  }
}
