// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PropDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class PropDefinition : IsoObjectDefinition
  {
    [SerializeField]
    private FightCellState m_impliedCellState;

    public FightCellState impliedCellState => this.m_impliedCellState;

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
  }
}
