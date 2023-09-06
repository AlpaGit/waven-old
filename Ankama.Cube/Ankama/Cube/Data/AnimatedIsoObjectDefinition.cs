// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AnimatedIsoObjectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class AnimatedIsoObjectDefinition : IsoObjectDefinition
  {
    protected Id<CharacterSkinDefinition> m_defaultSkin;
    protected List<Id<CharacterSkinDefinition>> m_skins;

    public Id<CharacterSkinDefinition> defaultSkin => this.m_defaultSkin;

    public IReadOnlyList<Id<CharacterSkinDefinition>> skins => (IReadOnlyList<Id<CharacterSkinDefinition>>) this.m_skins;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_defaultSkin = Serialization.JsonTokenIdValue<CharacterSkinDefinition>(jsonObject, "defaultSkin");
      JArray jarray = Serialization.JsonArray(jsonObject, "skins");
      this.m_skins = new List<Id<CharacterSkinDefinition>>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_skins.Add(Serialization.JsonTokenIdValue<CharacterSkinDefinition>(token));
    }
  }
}
