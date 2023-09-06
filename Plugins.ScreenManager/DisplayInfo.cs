// Decompiled with JetBrains decompiler
// Type: Ankama.ScreenManagement.DisplayInfo
// Assembly: Plugins.ScreenManager, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E44D48D-5FB2-4F40-8C6D-7D760CDD308E
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.ScreenManager.dll

using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.ScreenManagement
{
  [PublicAPI]
  public struct DisplayInfo
  {
    [PublicAPI]
    public readonly string name;
    [PublicAPI]
    public readonly string adapterName;
    [PublicAPI]
    public readonly bool isPrimaryDisplay;
    [PublicAPI]
    internal readonly RectInt frame;
    [PublicAPI]
    public readonly Resolution systemResolution;
    [PublicAPI]
    public readonly Resolution[] resolutions;

    internal DisplayInfo(
      string name,
      string adapterName,
      bool isPrimaryDisplay,
      RectInt frame,
      Resolution systemResolution,
      List<Resolution> resolutionList)
    {
      this.name = name;
      this.adapterName = adapterName;
      this.isPrimaryDisplay = isPrimaryDisplay;
      this.frame = frame;
      this.systemResolution = systemResolution;
      this.resolutions = resolutionList.ToArray();
    }
  }
}
