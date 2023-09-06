// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ConstantsDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ConstantsDefinition : EditableData
  {
    private int m_agonyThreshold;

    public int agonyThreshold => this.m_agonyThreshold;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_agonyThreshold = Serialization.JsonTokenValue<int>(jsonObject, "agonyThreshold", 20);
    }
  }
}
