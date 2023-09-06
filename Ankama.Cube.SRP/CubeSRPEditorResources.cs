// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CubeSRPEditorResources
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Serialization;

namespace Ankama.Cube.SRP
{
  public class CubeSRPEditorResources : ScriptableObject
  {
    [FormerlySerializedAs("DefaultMaterial")]
    [SerializeField]
    private Material m_defaultMaterial;
    [FormerlySerializedAs("DefaultParticleMaterial")]
    [SerializeField]
    private Material m_particleMaterial;

    public Material defaultMaterial => this.m_defaultMaterial;

    public Material particleMaterial => this.m_particleMaterial;
  }
}
