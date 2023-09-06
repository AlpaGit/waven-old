// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.Maps.BossFightMapResources
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Data.Maps
{
  [Serializable]
  public struct BossFightMapResources
  {
    [SerializeField]
    private MonsterSpawnCellDefinition m_monsterSpawnCellDefinition;
    [SerializeField]
    private GameObject[] m_heroLostFeedbacks;

    public MonsterSpawnCellDefinition monsterSpawnCellDefinition => this.m_monsterSpawnCellDefinition;

    public GameObject[] heroLostFeedbacks => this.m_heroLostFeedbacks;
  }
}
