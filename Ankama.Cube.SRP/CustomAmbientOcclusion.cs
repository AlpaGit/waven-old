// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CustomAmbientOcclusion
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  [PostProcess(typeof (CustomAmbientOcclusionRenderer), PostProcessEvent.BeforeTransparent, "DofusCube/AmbientOcclusion", true)]
  [Serializable]
  public sealed class CustomAmbientOcclusion : PostProcessEffectSettings
  {
    [Range(0.0f, 4f)]
    [Tooltip("The degree of darkness added by ambient occlusion. Higher values produce darker areas.")]
    public FloatParameter intensity;
    [Tooltip("The radius of sample points. This affects the size of darkened areas.")]
    public FloatParameter radius;
    [ColorUsage(false)]
    [Tooltip("The custom color to use for the ambient occlusion. The default is black.")]
    public ColorParameter color;

    public override bool IsEnabledAndSupported(PostProcessRenderContext context)
    {
      int num1 = !this.enabled.value ? 0 : ((double) this.intensity.value > 0.0 ? 1 : 0);
      Shader aoShader = CubeSRP.resources.aoShader;
      int num2 = !(bool) (UnityEngine.Object) aoShader ? 0 : (aoShader.isSupported ? 1 : 0);
      return (num1 & num2) != 0;
    }

    public CustomAmbientOcclusion()
    {
      FloatParameter floatParameter1 = new FloatParameter();
      floatParameter1.value = 1f;
      this.intensity = floatParameter1;
      FloatParameter floatParameter2 = new FloatParameter();
      floatParameter2.value = 0.2f;
      this.radius = floatParameter2;
      ColorParameter colorParameter = new ColorParameter();
      colorParameter.value = Color.black;
      this.color = colorParameter;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }
  }
}
