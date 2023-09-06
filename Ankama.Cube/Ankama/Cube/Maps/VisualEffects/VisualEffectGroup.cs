// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.VisualEffects.VisualEffectGroup
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.VisualEffects
{
  [SelectionBase]
  [ExecuteInEditMode]
  public sealed class VisualEffectGroup : VisualEffect
  {
    [SerializeField]
    private List<VisualEffect> m_children = new List<VisualEffect>();

    public override bool IsAlive()
    {
      List<VisualEffect> children = this.m_children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
      {
        if (children[index].IsAlive())
          return true;
      }
      return false;
    }

    protected override void PlayInternal()
    {
      List<VisualEffect> children = this.m_children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
        children[index].GroupPlayedInternal();
    }

    protected override void PauseInternal()
    {
      List<VisualEffect> children = this.m_children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
        children[index].GroupPausedInternal();
    }

    protected override void StopInternal(VisualEffectStopMethod stopMethod)
    {
      List<VisualEffect> children = this.m_children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
        children[index].GroupStoppedInternal(stopMethod);
    }

    protected override void ClearInternal()
    {
      List<VisualEffect> children = this.m_children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
        children[index].GroupClearedInternal();
    }

    public override void SetSortingOrder(int value)
    {
      List<VisualEffect> children = this.m_children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
        children[index].SetSortingOrder(value);
    }

    public override void SetColorModifier(Color color)
    {
      List<VisualEffect> children = this.m_children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
        children[index].SetColorModifier(color);
    }
  }
}
