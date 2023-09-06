// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MechanismDefinition
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
  public abstract class MechanismDefinition : 
    AnimatedIsoObjectDefinition,
    IEffectList,
    IEditableContent,
    IFamilyList
  {
    protected PrecomputedData m_precomputedData;
    protected List<Family> m_families;
    protected IAreaDefinition m_effectAreaDefinition;

    public PrecomputedData precomputedData => this.m_precomputedData;

    public IReadOnlyList<Family> families => (IReadOnlyList<Family>) this.m_families;

    public IAreaDefinition effectAreaDefinition => this.m_effectAreaDefinition;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_precomputedData = PrecomputedData.FromJsonProperty(jsonObject, "precomputedData");
      this.m_families = Serialization.JsonArrayAsList<Family>(jsonObject, "families");
      this.m_effectAreaDefinition = IAreaDefinitionUtils.FromJsonProperty(jsonObject, "effectAreaDefinition");
    }
  }
}
