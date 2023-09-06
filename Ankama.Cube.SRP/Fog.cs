﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.Fog
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  [ExecuteInEditMode]
  public class Fog : MonoBehaviour
  {
    [SerializeField]
    public Color color = Color.white;
    [SerializeField]
    public float viewDistance = 50f;
    [SerializeField]
    [Min(0.01f)]
    public float viewFade = 35f;
    [SerializeField]
    public float worldHeight;
    [SerializeField]
    [Min(0.01f)]
    public float worldfade = 35f;

    public Vector4 shaderParams => new Vector4(this.worldHeight, this.worldHeight - this.worldfade, this.viewDistance, this.viewDistance + this.viewFade);

    private void OnEnable() => FogManager.instance.Register(this);

    private void OnDisable() => FogManager.instance.Unregister(this);
  }
}
