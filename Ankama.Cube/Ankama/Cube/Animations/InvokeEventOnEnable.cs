// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.InvokeEventOnEnable
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.Animations
{
  public sealed class InvokeEventOnEnable : MonoBehaviour
  {
    [SerializeField]
    private UnityEvent m_event;

    private void OnEnable()
    {
      if (this.m_event != null)
        this.m_event.Invoke();
      this.gameObject.SetActive(false);
    }
  }
}
