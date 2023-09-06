// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Test.WeatherTest
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using UnityEngine;

namespace Ankama.Cube.Test
{
  public class WeatherTest : MonoBehaviour
  {
    [SerializeField]
    private Light m_mainLight;
    [Header("Rain")]
    [SerializeField]
    private ParticleSystem m_rainParticle;
    [SerializeField]
    private WorldRain m_worldRain;
    [SerializeField]
    [Min(0.0f)]
    private float m_maxLightColorLerpFactor = 0.8f;
    [SerializeField]
    private Color m_ambientColor = Color.grey;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_rainAmount;
    private Color m_baseLightColor;
    private Color m_baseAmbientColor;
    private float m_baseRateOverTime;

    private void OnEnable()
    {
      this.m_baseLightColor = (Object) this.m_mainLight != (Object) null ? this.m_mainLight.color : Color.black;
      this.m_baseAmbientColor = RenderSettings.ambientLight;
      this.m_baseRateOverTime = (Object) this.m_rainParticle != (Object) null ? this.m_rainParticle.emission.rateOverTime.constant : 0.0f;
    }

    private void OnDisable() => RenderSettings.ambientLight = this.m_baseAmbientColor;

    private void Update()
    {
      if ((Object) this.m_mainLight != (Object) null)
      {
        RenderSettings.ambientLight = Color.Lerp(this.m_baseAmbientColor, this.m_ambientColor, this.m_rainAmount);
        this.m_mainLight.color = Color.Lerp(this.m_baseLightColor, RenderSettings.ambientLight, Mathf.Lerp(0.0f, this.m_maxLightColorLerpFactor, this.m_rainAmount));
      }
      if ((bool) (Object) this.m_rainParticle)
        this.m_rainParticle.emission.rateOverTime = (ParticleSystem.MinMaxCurve) (this.m_baseRateOverTime * this.m_rainAmount);
      if (!((Object) this.m_worldRain != (Object) null))
        return;
      this.m_worldRain.m_amount = this.m_rainAmount;
    }
  }
}
