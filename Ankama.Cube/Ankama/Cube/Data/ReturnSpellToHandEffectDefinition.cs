// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ReturnSpellToHandEffectDefinition
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
  public sealed class ReturnSpellToHandEffectDefinition : EffectExecutionDefinition
  {
    private ReturnSpellToHandSpellParam m_spell;

    public ReturnSpellToHandSpellParam spell => this.m_spell;

    public override string ToString() => "Return '" + (this.m_spell == ReturnSpellToHandSpellParam.ThisSpell ? "this" : "triggering") + "' spell to hand" + (this.m_condition == null ? "" : string.Format(" if {0}", (object) this.m_condition));

    public static ReturnSpellToHandEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ReturnSpellToHandEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ReturnSpellToHandEffectDefinition effectDefinition = new ReturnSpellToHandEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ReturnSpellToHandEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ReturnSpellToHandEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ReturnSpellToHandEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_spell = (ReturnSpellToHandSpellParam) Serialization.JsonTokenValue<int>(jsonObject, "spell");
    }
  }
}
