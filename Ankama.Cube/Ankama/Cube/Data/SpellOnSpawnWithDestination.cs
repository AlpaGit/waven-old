// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellOnSpawnWithDestination
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
  public sealed class SpellOnSpawnWithDestination : IEditableContent
  {
    private Id<SpellDefinition> m_spell;
    private SpellDestination m_destination;

    public Id<SpellDefinition> spell => this.m_spell;

    public SpellDestination destination => this.m_destination;

    public override string ToString() => this.m_spell == (Id<SpellDefinition>) null ? "Draw null in {m_destination}" : string.Format("Draw [{0}] in {1}", (object) ObjectReference.GetSpell(this.m_spell.value)?.idAndName, (object) this.m_destination);

    public static SpellOnSpawnWithDestination FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellOnSpawnWithDestination) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellOnSpawnWithDestination spawnWithDestination = new SpellOnSpawnWithDestination();
      spawnWithDestination.PopulateFromJson(jsonObject);
      return spawnWithDestination;
    }

    public static SpellOnSpawnWithDestination FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellOnSpawnWithDestination defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellOnSpawnWithDestination.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_spell = Serialization.JsonTokenIdValue<SpellDefinition>(jsonObject, "spell");
      this.m_destination = (SpellDestination) Serialization.JsonTokenValue<int>(jsonObject, "destination");
    }
  }
}
