// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IsoObjectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class IsoObjectDefinition : EditableData
  {
    protected IObjectAreaDefinition m_areaDefinition;

    public IObjectAreaDefinition areaDefinition => this.m_areaDefinition;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_areaDefinition = IObjectAreaDefinitionUtils.FromJsonProperty(jsonObject, "areaDefinition");
    }
  }
}
