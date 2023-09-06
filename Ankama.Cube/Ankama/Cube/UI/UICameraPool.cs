// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.UICameraPool
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI
{
  public class UICameraPool
  {
    private readonly List<UICamera> m_freeCameras = new List<UICamera>();
    private readonly List<UICamera> m_busyCameras = new List<UICamera>();
    private readonly Camera m_prefab;

    public List<UICamera> busyCameras => this.m_busyCameras;

    public UICameraPool(Camera prefab) => this.m_prefab = prefab;

    public void ReleaseAll()
    {
      int count = this.m_busyCameras.Count;
      for (int index = 0; index < count; ++index)
      {
        UICamera busyCamera = this.m_busyCameras[index];
        busyCamera.camera.gameObject.SetActive(false);
        busyCamera.Clean();
        this.m_freeCameras.Add(busyCamera);
      }
      this.m_busyCameras.Clear();
    }

    public UICamera Get()
    {
      UICamera uiCamera;
      if (this.m_freeCameras.Count == 0)
      {
        uiCamera = new UICamera(Object.Instantiate<Camera>(this.m_prefab, this.m_prefab.transform.parent));
      }
      else
      {
        uiCamera = this.m_freeCameras[0];
        this.m_freeCameras.RemoveAt(0);
      }
      this.m_busyCameras.Add(uiCamera);
      uiCamera.camera.gameObject.SetActive(true);
      return uiCamera;
    }
  }
}
