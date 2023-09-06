// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioListenerPosition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Maps;
using System;
using UnityEngine;

namespace Ankama.Cube.Audio
{
  public sealed class AudioListenerPosition : MonoBehaviour
  {
    [SerializeField]
    private float m_minZoomDistance = -8f;
    [SerializeField]
    private float m_maxZoomDistance = 16f;

    private void Start()
    {
      AudioManager.RegisterListenerPosition(this);
      CameraHandler current = CameraHandler.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current && current.hasZoomRange)
      {
        current.onZoomChanged += new Action<CameraHandler>(this.OnZoomChanged);
        this.OnZoomChanged(current);
      }
      else
        this.transform.localPosition = new Vector3(0.0f, 0.0f, this.m_minZoomDistance);
    }

    private void Update()
    {
      if (!this.transform.hasChanged)
        return;
      AudioManager.UpdateListenerPosition(this);
      this.transform.hasChanged = false;
    }

    private void OnZoomChanged(CameraHandler cameraHandler) => this.UpdatePosition(cameraHandler.zoomLevel);

    public void UpdatePosition(float zoom) => this.transform.localPosition = new Vector3(0.0f, 0.0f, Mathf.Lerp(this.m_minZoomDistance, this.m_maxZoomDistance, zoom));

    private void OnDestroy()
    {
      AudioManager.UnRegisterListenerPosition(this);
      CameraHandler current = CameraHandler.current;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) current))
        return;
      current.onZoomChanged -= new Action<CameraHandler>(this.OnZoomChanged);
    }
  }
}
