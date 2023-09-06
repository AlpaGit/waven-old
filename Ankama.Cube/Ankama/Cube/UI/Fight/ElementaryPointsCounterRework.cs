// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.ElementaryPointsCounterRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public sealed class ElementaryPointsCounterRework : MonoBehaviour
  {
    [SerializeField]
    private PointCounterRework m_air;
    [SerializeField]
    private PointCounterRework m_earth;
    [SerializeField]
    private PointCounterRework m_fire;
    [SerializeField]
    private PointCounterRework m_water;

    public void SetValues(int air, int earth, int fire, int water)
    {
      this.m_air.SetValue(air);
      this.m_earth.SetValue(earth);
      this.m_fire.SetValue(fire);
      this.m_water.SetValue(water);
    }

    public void ChangeAirValue(int value)
    {
      if (!((Object) null != (Object) this.m_air))
        return;
      this.m_air.ChangeValue(value);
    }

    public void ShowPreviewAir(int value, ValueModifier modifier)
    {
    }

    public void HidePreviewAir(bool cancelled)
    {
    }

    public void ChangeEarthValue(int value)
    {
      if (!((Object) null != (Object) this.m_earth))
        return;
      this.m_earth.ChangeValue(value);
    }

    public void ShowPreviewEarth(int value, ValueModifier modifier)
    {
    }

    public void HidePreviewEarth(bool cancelled)
    {
    }

    public void ChangeFireValue(int value)
    {
      if (!((Object) null != (Object) this.m_fire))
        return;
      this.m_fire.ChangeValue(value);
    }

    public void ShowPreviewFire(int value, ValueModifier modifier)
    {
    }

    public void HidePreviewFire(bool cancelled)
    {
    }

    public void ChangeWaterValue(int value)
    {
      if (!((Object) null != (Object) this.m_water))
        return;
      this.m_water.ChangeValue(value);
    }

    public void ShowPreviewWater(int value, ValueModifier modifier)
    {
    }

    public void HidePreviewWater(bool cancelled)
    {
    }
  }
}
