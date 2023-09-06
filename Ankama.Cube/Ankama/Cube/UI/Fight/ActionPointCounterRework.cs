// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.ActionPointCounterRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public sealed class ActionPointCounterRework : MonoBehaviour
  {
    [SerializeField]
    private PointCounterRework m_counter;

    public void SetValue(int value)
    {
      if (!((Object) null != (Object) this.m_counter))
        return;
      this.m_counter.SetValue(value);
    }

    public void ChangeValue(int value)
    {
      if (!((Object) null != (Object) this.m_counter))
        return;
      this.m_counter.ChangeValue(value);
    }

    public void ShowPreview(int value, ValueModifier modifier)
    {
    }

    public void HidePreview(bool cancelled)
    {
    }
  }
}
