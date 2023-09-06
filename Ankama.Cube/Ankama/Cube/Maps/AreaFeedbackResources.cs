// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.AreaFeedbackResources
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps
{
  public sealed class AreaFeedbackResources : ScriptableObject
  {
    [SerializeField]
    private Color m_localColor = Color.white;
    [SerializeField]
    private Color[] m_colors = new Color[0];

    public Color localColor => this.m_localColor;

    public Color[] colors => this.m_colors;
  }
}
