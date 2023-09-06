// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapRenderSettings
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.SRP;
using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  [Serializable]
  public struct MapRenderSettings
  {
    [SerializeField]
    public MapRenderSettings.LightSettings lightSettings;

    public static MapRenderSettings Create() => new MapRenderSettings()
    {
      lightSettings = MapRenderSettings.LightSettings.defaultSettings
    };

    public static MapRenderSettings CreateFromScene()
    {
      MapRenderSettings ambience = MapRenderSettings.Create();
      MapRenderSettings.FillWithScene(ref ambience);
      return ambience;
    }

    public static void FillWithScene(ref MapRenderSettings ambience, bool duplicateScriptables = false)
    {
      ambience.lightSettings.ambientColor = RenderSettings.ambientLight;
      Dictionary<Light, SRPLight> lights = SRPLight.s_lights;
      if (lights.Count > 1)
        Log.Warning("Multiple light in scene, select first one", 64, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\MapRenderProfile.cs");
      if (lights.Count <= 0)
        return;
      Dictionary<Light, SRPLight>.Enumerator enumerator = lights.GetEnumerator();
      enumerator.MoveNext();
      KeyValuePair<Light, SRPLight> current = enumerator.Current;
      Light key = current.Key;
      SRPLight srpLight = current.Value;
      ambience.lightSettings.lightRotation = key.transform.rotation;
      ambience.lightSettings.lightColor = key.color;
      ambience.lightSettings.lightIntensity = key.intensity;
      ambience.lightSettings.overrideLightRotation = srpLight.overrideDirRotation;
    }

    public static void ApplyToScene(MapRenderSettings ambience)
    {
      RenderSettings.ambientLight = ambience.lightSettings.ambientColor;
      foreach (KeyValuePair<Light, SRPLight> light in SRPLight.s_lights)
      {
        Light key = light.Key;
        SRPLight srpLight = light.Value;
        key.color = ambience.lightSettings.lightColor;
        key.intensity = ambience.lightSettings.lightIntensity;
        key.transform.rotation = ambience.lightSettings.lightRotation;
        srpLight.overrideDirRotation = ambience.lightSettings.overrideLightRotation;
      }
    }

    public static void TransitionTo(MapRenderSettings ambience, float duration)
    {
      DOTween.To((DOGetter<Color>) (() => RenderSettings.ambientLight), (DOSetter<Color>) (x => RenderSettings.ambientLight = x), ambience.lightSettings.ambientColor, duration);
      foreach (KeyValuePair<Light, SRPLight> light1 in SRPLight.s_lights)
      {
        Light light = light1.Key;
        SRPLight srpLight = light1.Value;
        DOTween.To((DOGetter<Color>) (() => light.color), (DOSetter<Color>) (x => light.color = x), ambience.lightSettings.lightColor, duration);
        DOTween.To((DOGetter<float>) (() => light.intensity), (DOSetter<float>) (x => light.intensity = x), ambience.lightSettings.lightIntensity, duration);
        DOTween.To((DOGetter<Quaternion>) (() => light.transform.rotation), (DOSetter<Quaternion>) (x => light.transform.rotation = x), ambience.lightSettings.lightRotation.eulerAngles, duration);
        DOTween.To((DOGetter<Quaternion>) (() => srpLight.overrideDirRotation), (DOSetter<Quaternion>) (x => srpLight.overrideDirRotation = x), ambience.lightSettings.overrideLightRotation.eulerAngles, duration);
      }
    }

    [Serializable]
    public struct LightSettings
    {
      [SerializeField]
      public Color ambientColor;
      [SerializeField]
      public Color lightColor;
      [SerializeField]
      public float lightIntensity;
      [SerializeField]
      [EulerAngles]
      public Quaternion lightRotation;
      [SerializeField]
      [EulerAngles]
      public Quaternion overrideLightRotation;

      public static MapRenderSettings.LightSettings defaultSettings => new MapRenderSettings.LightSettings()
      {
        ambientColor = Color.grey,
        lightColor = Color.white,
        lightIntensity = 1f,
        lightRotation = Quaternion.identity,
        overrideLightRotation = Quaternion.identity
      };
    }
  }
}
