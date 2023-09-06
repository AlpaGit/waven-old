// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EffectCondition
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
  public abstract class EffectCondition : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static EffectCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EffectCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class EffectCondition");
        return (EffectCondition) null;
      }
      string str = jtoken.Value<string>();
      EffectCondition effectCondition;
      switch (str)
      {
        case "AgonyCondition":
          effectCondition = (EffectCondition) new AgonyCondition();
          break;
        case "AndCondition":
          effectCondition = (EffectCondition) new AndCondition();
          break;
        case "CaracValueCondition":
          effectCondition = (EffectCondition) new CaracValueCondition();
          break;
        case "ClanCondition":
          effectCondition = (EffectCondition) new ClanCondition();
          break;
        case "DynamicValueEvaluatorCondition":
          effectCondition = (EffectCondition) new DynamicValueEvaluatorCondition();
          break;
        case "ElementaryStateCondition":
          effectCondition = (EffectCondition) new ElementaryStateCondition();
          break;
        case "HerdCondition":
          effectCondition = (EffectCondition) new HerdCondition();
          break;
        case "NotCondition":
          effectCondition = (EffectCondition) new NotCondition();
          break;
        case "NumberOfEntityCondition":
          effectCondition = (EffectCondition) new NumberOfEntityCondition();
          break;
        case "OrCondition":
          effectCondition = (EffectCondition) new OrCondition();
          break;
        case "PropertyCondition":
          effectCondition = (EffectCondition) new PropertyCondition();
          break;
        case "SpellCanBeRewindCondition":
          effectCondition = (EffectCondition) new SpellCanBeRewindCondition();
          break;
        case "UniqueCondition":
          effectCondition = (EffectCondition) new UniqueCondition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (EffectCondition) null;
      }
      effectCondition.PopulateFromJson(jsonObject);
      return effectCondition;
    }

    public static EffectCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EffectCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectCondition.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    public abstract bool IsValid(DynamicValueContext context);
  }
}
