// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.Layouts.AlignLayout
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components.Layouts
{
  [ExecuteInEditMode]
  public class AlignLayout : MonoBehaviour
  {
    [SerializeField]
    [UsedImplicitly]
    protected float m_spacing;
    [SerializeField]
    [UsedImplicitly]
    protected RectTransform.Edge m_direction;
    [SerializeField]
    [UsedImplicitly]
    protected bool m_reverseOrder;
    private bool m_dirty;
    private RectTransform m_rect;

    protected RectTransform rectTransform
    {
      get
      {
        if ((UnityEngine.Object) this.m_rect == (UnityEngine.Object) null)
          this.m_rect = this.GetComponent<RectTransform>();
        return this.m_rect;
      }
    }

    private void Refresh()
    {
      int childCount = this.transform.childCount;
      float offset = 0.0f;
      if (this.m_reverseOrder)
      {
        for (int childIndex = childCount - 1; childIndex >= 0; --childIndex)
          this.SetChildPosition(childIndex, ref offset);
      }
      else
      {
        for (int childIndex = 0; childIndex < childCount; ++childIndex)
          this.SetChildPosition(childIndex, ref offset);
      }
    }

    private void SetChildPosition(int childIndex, ref float offset)
    {
      Transform child = this.transform.GetChild(childIndex);
      if (child.GetComponent<ILayoutIgnorer>() != null)
        return;
      RectTransform component = child.GetComponent<RectTransform>();
      Vector3 localPosition = component.localPosition;
      float num;
      switch (this.m_direction)
      {
        case RectTransform.Edge.Left:
          num = component.rect.width;
          localPosition.x = -offset;
          localPosition.y = 0.0f;
          break;
        case RectTransform.Edge.Right:
          num = component.rect.width;
          localPosition.x = offset;
          localPosition.y = 0.0f;
          break;
        case RectTransform.Edge.Top:
          num = component.rect.height;
          localPosition.x = 0.0f;
          localPosition.y = offset;
          break;
        case RectTransform.Edge.Bottom:
          num = component.rect.height;
          localPosition.x = 0.0f;
          localPosition.y = -offset;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      component.localPosition = localPosition;
      offset += num + this.m_spacing;
    }

    private void Update() => this.Refresh();
  }
}
