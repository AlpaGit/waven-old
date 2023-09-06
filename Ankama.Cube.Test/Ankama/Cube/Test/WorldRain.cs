// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Test.WorldRain
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Ankama.Cube.Test
{
  [ExecuteInEditMode]
  public class WorldRain : MonoBehaviour
  {
    [SerializeField]
    [Range(0.0f, 1f)]
    public float m_amount;
    [SerializeField]
    [Min(0.0f)]
    public float m_timeFactor = 1f;
    [SerializeField]
    [Range(0.0f, 1f)]
    public float m_wetColorFactor = 0.8f;
    [SerializeField]
    [Range(0.0f, 2f)]
    private float m_dropColorFactor = 1.2f;
    [SerializeField]
    private WorldRain.Droplet m_droplet;
    [SerializeField]
    private WorldRain.SlidingDroplet m_slidingDroplet;
    [SerializeField]
    private WorldRain.AnimatedTillingTexture m_waterDropTexture;
    private float m_time;

    protected void OnEnable() => Shader.EnableKeyword(WorldRain.Uniforms._RainKeyword);

    protected void OnDisable() => Shader.DisableKeyword(WorldRain.Uniforms._RainKeyword);

    protected void Update()
    {
      float deltaTime = Time.deltaTime * this.m_timeFactor;
      this.m_time += deltaTime;
      this.m_droplet.Update(deltaTime);
      this.m_slidingDroplet.Update(deltaTime);
      Shader.SetGlobalVector(WorldRain.Uniforms._RainParams1, new Vector4(this.m_amount, this.m_wetColorFactor, this.m_dropColorFactor, this.m_time));
      Shader.SetGlobalTexture(WorldRain.Uniforms._DropletTex, (Texture) this.m_droplet.texture);
      Shader.SetGlobalTexture(WorldRain.Uniforms._DropletTex2, (Texture) this.m_droplet.texture2);
      Shader.SetGlobalVector(WorldRain.Uniforms._DropletTex_ST, this.m_droplet.TilingOffset);
      Shader.SetGlobalVector(WorldRain.Uniforms._Droplet_Param, this.m_droplet.Params);
      Shader.SetGlobalTexture(WorldRain.Uniforms._SlidingDropTex, (Texture) this.m_slidingDroplet.texture);
      Shader.SetGlobalTexture(WorldRain.Uniforms._SlidingDropTex2, (Texture) this.m_slidingDroplet.texture2);
      Shader.SetGlobalVector(WorldRain.Uniforms._SlidingDropTex_ST, this.m_slidingDroplet.TilingOffset);
      Shader.SetGlobalVector(WorldRain.Uniforms._SlidingDropTex_Param1, this.m_slidingDroplet.Params1);
      Shader.SetGlobalVector(WorldRain.Uniforms._SlidingDropTex_Param2, this.m_slidingDroplet.Params2);
    }

    [Serializable]
    public class Droplet
    {
      [SerializeField]
      public Texture2D texture;
      [SerializeField]
      public Texture2D texture2;
      [SerializeField]
      public Vector2 tiling = Vector2.one;
      [SerializeField]
      public Vector2 offset = Vector2.zero;
      [SerializeField]
      [Min(0.0f)]
      public float timeSpeed = 1f;
      [SerializeField]
      [Min(0.0f)]
      public float normalScale = 5f;
      private float time;

      public void Update(float deltaTime) => this.time += this.timeSpeed * deltaTime;

      public Vector4 TilingOffset => new Vector4(this.tiling.x, this.tiling.y, this.offset.x, this.offset.y);

      public Vector4 Params => new Vector4(this.time, this.normalScale, 0.0f, 0.0f);
    }

    [Serializable]
    public class SlidingDroplet
    {
      [SerializeField]
      public Texture2D texture;
      [SerializeField]
      public Texture2D texture2;
      [SerializeField]
      public Vector2 tiling = Vector2.one;
      [SerializeField]
      public Vector2 offset = Vector2.zero;
      [SerializeField]
      [Min(0.0f)]
      public float timeSpeed = 1f;
      [SerializeField]
      public Vector2 maskNoiseTiling = Vector2.one;
      [SerializeField]
      public Vector2 maskNoiseOffset = Vector2.zero;
      [SerializeField]
      [Min(0.0f)]
      private float maskNoiseTimeSpeed = 0.05f;
      [SerializeField]
      [Min(0.0f)]
      private float noiseStrength = 0.05f;
      [SerializeField]
      [Min(0.0f)]
      public float normalScale = 5f;
      private float time;
      private float maskNoisetime;

      public void Update(float deltaTime)
      {
        this.time += this.timeSpeed * deltaTime;
        this.maskNoisetime += this.maskNoiseTimeSpeed * deltaTime;
      }

      public Vector4 TilingOffset => new Vector4(this.tiling.x, this.tiling.y, this.offset.x, this.time + this.offset.y);

      public Vector4 Params1 => new Vector4(this.noiseStrength, this.normalScale, 0.0f, 0.0f);

      public Vector4 Params2 => new Vector4(this.maskNoiseTiling.x, this.maskNoiseTiling.y, this.maskNoiseOffset.x, this.maskNoisetime + this.maskNoiseOffset.y);
    }

    [Serializable]
    public struct AnimatedTillingTexture
    {
      [SerializeField]
      public Texture2D texture;
      [SerializeField]
      public Vector2 tiling;
      [SerializeField]
      public Vector2 offset;
      [SerializeField]
      public Vector2 atlasSize;
      [SerializeField]
      public float speed;

      public Vector4 TilingOffset => new Vector4(this.tiling.x, this.tiling.y, this.offset.x, this.offset.y);

      public Vector4 AnimParams => new Vector4(this.atlasSize.x, this.atlasSize.y, this.speed, 0.0f);
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Uniforms
    {
      internal static readonly string _RainKeyword = "_RAIN";
      internal static readonly int _RainParams1 = Shader.PropertyToID(nameof (_RainParams1));
      internal static readonly int _DropletTex = Shader.PropertyToID("_RainDropletTex");
      internal static readonly int _DropletTex2 = Shader.PropertyToID("_RainDropletTex2");
      internal static readonly int _DropletTex_ST = Shader.PropertyToID("_RainDropletTex_ST");
      internal static readonly int _Droplet_Param = Shader.PropertyToID("_RainDroplet_Param");
      internal static readonly int _SlidingDropTex = Shader.PropertyToID("_RainSlidingDropletTex");
      internal static readonly int _SlidingDropTex2 = Shader.PropertyToID("_RainSlidingDropletTex2");
      internal static readonly int _SlidingDropTex_ST = Shader.PropertyToID("_RainSlidingDropletTex_ST");
      internal static readonly int _SlidingDropTex_Param1 = Shader.PropertyToID("_RainSlidingDropletTex_Param1");
      internal static readonly int _SlidingDropTex_Param2 = Shader.PropertyToID("_RainSlidingDropletTex_Param2");
    }
  }
}
