// Decompiled with JetBrains decompiler
// Type: FMODUnity.FMODSettings
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using FMOD;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
  public class FMODSettings : ScriptableObject
  {
    private const string SettingsAssetName = "FMODStudioSettings";
    private const string SettingsAssetFolder = "FMOD";
    [SerializeField]
    public string masterBankName;
    [SerializeField]
    public DEBUG_FLAGS loggingLevel = DEBUG_FLAGS.WARNING;
    [SerializeField]
    public List<FMODSettings.PlatformIntSetting> speakerModeSettings;
    [SerializeField]
    public List<FMODSettings.PlatformIntSetting> sampleRateSettings;
    [SerializeField]
    public List<FMODSettings.PlatformBoolSetting> liveUpdateSettings;
    [SerializeField]
    public List<FMODSettings.PlatformBoolSetting> overlaySettings;
    [SerializeField]
    public List<FMODSettings.PlatformBoolSetting> loggingSettings;
    [SerializeField]
    public List<FMODSettings.PlatformStringSetting> bankDirectorySettings;
    [SerializeField]
    public List<FMODSettings.PlatformIntSetting> virtualChannelSettings;
    [SerializeField]
    public List<FMODSettings.PlatformIntSetting> realChannelSettings;
    [SerializeField]
    public FMODRuntimeCache runtimeCache;

    public static bool HasSetting<T>(List<T> list, FMODPlatform platform) where T : FMODSettings.PlatformSettingBase
    {
      foreach (T obj in list)
      {
        if (obj.platform == platform)
          return true;
      }
      return false;
    }

    public static T1 GetSetting<T0, T1>(List<T0> list, FMODPlatform platform, T1 defaultValue) where T0 : FMODSettings.PlatformSetting<T1>
    {
      for (; platform != FMODPlatform.None; platform = platform.GetParent())
      {
        foreach (T0 obj in list)
        {
          if (obj.platform == platform)
            return obj.value;
        }
      }
      return defaultValue;
    }

    public static void SetSetting<T0, T1>(List<T0> list, FMODPlatform platform, T1 value) where T0 : FMODSettings.PlatformSetting<T1>, new()
    {
      foreach (T0 obj in list)
      {
        if (obj.platform == platform)
        {
          obj.value = value;
          return;
        }
      }
      T0 obj1 = new T0();
      obj1.platform = platform;
      obj1.value = value;
      T0 obj2 = obj1;
      list.Add(obj2);
    }

    public bool IsLiveUpdateEnabled(FMODPlatform platform) => FMODSettings.GetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.liveUpdateSettings, platform, FMODSettings.TriStateBool.Disabled) == FMODSettings.TriStateBool.Enabled;

    public bool IsOverlayEnabled(FMODPlatform platform) => FMODSettings.GetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.overlaySettings, platform, FMODSettings.TriStateBool.Disabled) == FMODSettings.TriStateBool.Enabled;

    public int GetRealChannels(FMODPlatform platform) => FMODSettings.GetSetting<FMODSettings.PlatformIntSetting, int>(this.realChannelSettings, platform, 64);

    public int GetVirtualChannels(FMODPlatform platform) => FMODSettings.GetSetting<FMODSettings.PlatformIntSetting, int>(this.virtualChannelSettings, platform, 128);

    public int GetSpeakerMode(FMODPlatform platform) => FMODSettings.GetSetting<FMODSettings.PlatformIntSetting, int>(this.speakerModeSettings, platform, 3);

    public int GetSampleRate(FMODPlatform platform) => FMODSettings.GetSetting<FMODSettings.PlatformIntSetting, int>(this.sampleRateSettings, platform, 48000);

    public string GetBankPlatform(FMODPlatform platform) => FMODSettings.GetSetting<FMODSettings.PlatformStringSetting, string>(this.bankDirectorySettings, platform, "Desktop");

    private FMODSettings()
    {
      this.realChannelSettings = new List<FMODSettings.PlatformIntSetting>();
      this.virtualChannelSettings = new List<FMODSettings.PlatformIntSetting>();
      this.loggingSettings = new List<FMODSettings.PlatformBoolSetting>();
      this.liveUpdateSettings = new List<FMODSettings.PlatformBoolSetting>();
      this.overlaySettings = new List<FMODSettings.PlatformBoolSetting>();
      this.sampleRateSettings = new List<FMODSettings.PlatformIntSetting>();
      this.speakerModeSettings = new List<FMODSettings.PlatformIntSetting>();
      this.bankDirectorySettings = new List<FMODSettings.PlatformStringSetting>();
      FMODSettings.SetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.loggingSettings, FMODPlatform.PlayInEditor, FMODSettings.TriStateBool.Enabled);
      FMODSettings.SetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.liveUpdateSettings, FMODPlatform.PlayInEditor, FMODSettings.TriStateBool.Enabled);
      FMODSettings.SetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.overlaySettings, FMODPlatform.PlayInEditor, FMODSettings.TriStateBool.Enabled);
      FMODSettings.SetSetting<FMODSettings.PlatformIntSetting, int>(this.sampleRateSettings, FMODPlatform.PlayInEditor, 48000);
      FMODSettings.SetSetting<FMODSettings.PlatformIntSetting, int>(this.realChannelSettings, FMODPlatform.PlayInEditor, 256);
      FMODSettings.SetSetting<FMODSettings.PlatformIntSetting, int>(this.virtualChannelSettings, FMODPlatform.PlayInEditor, 1024);
      FMODSettings.SetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.loggingSettings, FMODPlatform.Default, FMODSettings.TriStateBool.Disabled);
      FMODSettings.SetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.liveUpdateSettings, FMODPlatform.Default, FMODSettings.TriStateBool.Disabled);
      FMODSettings.SetSetting<FMODSettings.PlatformBoolSetting, FMODSettings.TriStateBool>(this.overlaySettings, FMODPlatform.Default, FMODSettings.TriStateBool.Disabled);
      FMODSettings.SetSetting<FMODSettings.PlatformIntSetting, int>(this.realChannelSettings, FMODPlatform.Default, 32);
      FMODSettings.SetSetting<FMODSettings.PlatformIntSetting, int>(this.virtualChannelSettings, FMODPlatform.Default, 128);
      FMODSettings.SetSetting<FMODSettings.PlatformIntSetting, int>(this.sampleRateSettings, FMODPlatform.Default, 0);
      FMODSettings.SetSetting<FMODSettings.PlatformIntSetting, int>(this.speakerModeSettings, FMODPlatform.Default, 3);
    }

    [PublicAPI]
    public enum TriStateBool
    {
      Disabled,
      Enabled,
      Development,
    }

    public abstract class PlatformSettingBase
    {
      public FMODPlatform platform;
    }

    public abstract class PlatformSetting<T> : FMODSettings.PlatformSettingBase
    {
      public T value;
    }

    [Serializable]
    public sealed class PlatformIntSetting : FMODSettings.PlatformSetting<int>
    {
    }

    [Serializable]
    public sealed class PlatformStringSetting : FMODSettings.PlatformSetting<string>
    {
    }

    [Serializable]
    public sealed class PlatformBoolSetting : FMODSettings.PlatformSetting<FMODSettings.TriStateBool>
    {
    }
  }
}
