// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellFilter
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
  public abstract class SpellFilter : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static SpellFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class SpellFilter");
        return (SpellFilter) null;
      }
      string str = jtoken.Value<string>();
      SpellFilter spellFilter;
      switch (str)
      {
        case "SpellGiveReservePointFilter":
          spellFilter = (SpellFilter) new SpellGiveReservePointFilter();
          break;
        case "SpellElementsFilter":
          spellFilter = (SpellFilter) new SpellElementsFilter();
          break;
        case "SpellTagsFilter":
          spellFilter = (SpellFilter) new SpellTagsFilter();
          break;
        case "SpecificSpellFilter":
          spellFilter = (SpellFilter) new SpecificSpellFilter();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (SpellFilter) null;
      }
      spellFilter.PopulateFromJson(jsonObject);
      return spellFilter;
    }

    public static SpellFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellFilter.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    public abstract bool Accept(int spellInstanceId, SpellDefinition spellDef);
  }
}
