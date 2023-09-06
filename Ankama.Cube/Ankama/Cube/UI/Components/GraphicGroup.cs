// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.GraphicGroup
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  [ExecuteInEditMode]
  public class GraphicGroup : MonoBehaviour
  {
    [SerializeField]
    private Graphic[] m_children;
    [SerializeField]
    private Color m_color;

    public Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        for (int index = 0; index < this.m_children.Length; ++index)
        {
          Graphic child = this.m_children[index];
          if ((Object) child != (Object) this && (Object) child != (Object) null)
            child.color = value;
        }
      }
    }

    public float alpha
    {
      get => this.m_color.a;
      set => this.color = this.m_color with { a = value };
    }

    private void Start() => this.m_children = this.GetComponentsInChildren<Graphic>(true);

    private void OnTransformChildrenChanged() => this.m_children = this.GetComponentsInChildren<Graphic>(true);
  }
}
