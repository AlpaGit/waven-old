// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.QualityManager
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using Ankama.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class QualityManager
  {
    private static QualityAsset s_instance;
    public static Action<QualityAsset> onChanged;
    private static bool s_loaded = false;
    private static List<QualityAsset> s_qualityPresets = new List<QualityAsset>();

    public static QualityAsset current
    {
      get
      {
        if ((UnityEngine.Object) QualityManager.s_instance == (UnityEngine.Object) null)
          QualityManager.Load();
        return QualityManager.s_instance;
      }
    }

    public static void Load()
    {
      if (QualityManager.s_loaded)
        return;
      StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
      foreach (QualityAsset qualityAsset in Resources.LoadAll<QualityAsset>(""))
      {
        if (Application.isMobilePlatform && qualityAsset.name.Contains("mobile", comparisonType))
          QualityManager.s_qualityPresets.Add(qualityAsset);
        else if (qualityAsset.name.Contains("standalone", comparisonType))
          QualityManager.s_qualityPresets.Add(qualityAsset);
      }
      if (QualityManager.s_qualityPresets.Count != 0)
      {
        int index = QualityManager.s_qualityPresets.Count - 1;
        if (index < 0)
          index = 0;
        QualityManager.s_instance = QualityManager.s_qualityPresets[index];
      }
      if ((UnityEngine.Object) QualityManager.s_instance == (UnityEngine.Object) null)
        Log.Error("Failed to load RenderQualitySettings", 97, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\QualityManager.cs");
      QualityManager.s_loaded = true;
    }

    public static List<QualityAsset> GetQualityPresets()
    {
      if (QualityManager.s_loaded)
        return QualityManager.s_qualityPresets;
      Log.Error("Lot Loaded", 107, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\QualityManager.cs");
      return (List<QualityAsset>) null;
    }

    public static int GetQualityPresetIndex()
    {
      if (QualityManager.s_loaded)
        return QualityManager.s_qualityPresets.IndexOf(QualityManager.s_instance);
      Log.Error("Lot Loaded", 118, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\QualityManager.cs");
      return -1;
    }

    public static void SetQualityPresetIndex(int index)
    {
      if (!QualityManager.s_loaded)
      {
        Log.Error("Lot Loaded", 129, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\QualityManager.cs");
      }
      else
      {
        if (index < 0 || index >= QualityManager.s_qualityPresets.Count)
        {
          index = Mathf.Clamp(index, 0, QualityManager.s_qualityPresets.Count - 1);
          Log.Error("Index out of range", 136, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\QualityManager.cs");
        }
        QualityManager.SetQualityPreset(QualityManager.s_qualityPresets[index]);
      }
    }

    public static void SetQualityPreset(QualityAsset settings)
    {
      QualityManager.s_instance = settings;
      Action<QualityAsset> onChanged = QualityManager.onChanged;
      if (onChanged == null)
        return;
      onChanged(QualityManager.s_instance);
    }

    public static void NotifyQualityChanged(QualityAsset settings)
    {
      if ((UnityEngine.Object) QualityManager.s_instance != (UnityEngine.Object) settings)
        return;
      Action<QualityAsset> onChanged = QualityManager.onChanged;
      if (onChanged == null)
        return;
      onChanged(QualityManager.s_instance);
    }

    public enum StandaloneQuality
    {
      Low,
      Medium,
      High,
      Ultra,
    }
  }
}
