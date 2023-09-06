// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Device
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System;
using UnityEngine;

namespace Ankama.Cube
{
  public static class Device
  {
    public static readonly Device.Type currentType;
    private static float s_dpi;
    private static FullScreenMode s_fullScreenMode;
    private static Vector2Int s_screenSize;

    public static bool isStandalone => Device.currentType == Device.Type.PC;

    public static bool isMobile => Device.currentType == Device.Type.Mobile;

    public static bool isTablet => Device.currentType == Device.Type.Tablet;

    public static bool isMobileOrTablet
    {
      get
      {
        switch (Device.currentType)
        {
          case Device.Type.PC:
            return false;
          case Device.Type.Mobile:
          case Device.Type.Tablet:
            return true;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    public static bool fullScreen
    {
      get
      {
        switch (Device.s_fullScreenMode)
        {
          case FullScreenMode.ExclusiveFullScreen:
          case FullScreenMode.FullScreenWindow:
            return true;
          case FullScreenMode.MaximizedWindow:
          case FullScreenMode.Windowed:
            return false;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    public static event Device.ScreenSateChangedDelegate ScreenStateChanged;

    public static float dpi => Device.s_dpi;

    static Device()
    {
      if (Application.isMobilePlatform)
      {
        float dpi = Screen.dpi;
        if ((double) dpi <= 0.0)
        {
          Device.currentType = Device.Type.Mobile;
          Log.Warning(string.Format("Could not retrieve DPI of the device, defaulted to {0}.", (object) Device.currentType), 104, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Device.cs");
        }
        else
        {
          Resolution currentResolution = Screen.currentResolution;
          int width = currentResolution.width;
          int height = currentResolution.height;
          float num = Mathf.Sqrt((float) (width * width + height * height)) / dpi;
          Device.currentType = (double) num >= 7.0 ? Device.Type.Tablet : Device.Type.Mobile;
          Log.Info(string.Format("Auto-detected device type: {0}\n - physical diagonal: {1:n2} in\n - resolution: {2}x{3}\n - dpi: {4:n2}", (object) Device.currentType, (object) num, (object) width, (object) height, (object) dpi), 124, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Device.cs");
        }
      }
      else
        Device.currentType = Device.Type.PC;
      Device.s_dpi = Screen.dpi;
      Device.s_fullScreenMode = Screen.fullScreenMode;
      Device.s_screenSize = new Vector2Int(Screen.width, Screen.height);
    }

    public static void CheckScreenStateChanged()
    {
      bool flag = false;
      FullScreenMode fullScreenMode = Screen.fullScreenMode;
      if (fullScreenMode != Device.s_fullScreenMode)
      {
        Device.s_fullScreenMode = fullScreenMode;
        flag = true;
      }
      float dpi = Screen.dpi;
      if (!Mathf.Approximately(dpi, Device.s_dpi))
      {
        Device.s_dpi = dpi;
        flag = true;
      }
      Vector2Int vector2Int = new Vector2Int(Screen.width, Screen.height);
      if (vector2Int != Device.s_screenSize)
      {
        Device.s_screenSize = vector2Int;
        flag = true;
      }
      if (!flag)
        return;
      Log.Info(string.Format("Screen State Changed: fullscreen mode: {0}, size: {1}x{2}, DPI: {3}", (object) fullScreenMode, (object) vector2Int.x, (object) vector2Int.y, (object) dpi), 175, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Device.cs");
      Device.ScreenSateChangedDelegate screenStateChanged = Device.ScreenStateChanged;
      if (screenStateChanged == null)
        return;
      screenStateChanged();
    }

    public static void LogInfo()
    {
      string deviceModel = SystemInfo.deviceModel;
      string graphicsDeviceVendor = SystemInfo.graphicsDeviceVendor;
      string graphicsDeviceName = SystemInfo.graphicsDeviceName;
      string str1 = graphicsDeviceName.Contains(graphicsDeviceVendor, StringComparison.OrdinalIgnoreCase) ? graphicsDeviceName : graphicsDeviceVendor + " " + graphicsDeviceName;
      if (SystemInfo.deviceType == DeviceType.Desktop)
        str1 += string.Format(" ({0}/{1})", (object) SystemInfo.graphicsDeviceVendorID, (object) SystemInfo.graphicsDeviceID);
      string graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;
      string str2 = string.Format("{0} MB", (object) SystemInfo.graphicsMemorySize);
      Log.Info("[Device Information]\n - model: " + deviceModel + "\n - graphics device: " + str1 + "\n - graphics driver: " + graphicsDeviceVersion + "\n - graphics memory: " + str2, 204, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Device.cs");
      string operatingSystem = SystemInfo.operatingSystem;
      int num;
      switch (Application.platform)
      {
        case RuntimePlatform.IPhonePlayer:
        case RuntimePlatform.WebGLPlayer:
          num = 0;
          break;
        default:
          num = SystemInfo.processorFrequency;
          break;
      }
      string processorType = SystemInfo.processorType;
      string str3 = num == 0 || processorType.Contains("@", StringComparison.Ordinal) ? processorType : string.Format("{0} @ {1} MHz", (object) processorType, (object) num);
      int processorCount = SystemInfo.processorCount;
      if (processorCount > 0)
        str3 += string.Format(" ({0} logical cores)", (object) processorCount);
      string str4 = SystemInfo.systemMemorySize > 0 ? string.Format("{0} MB", (object) SystemInfo.systemMemorySize) : "n/a";
      Log.Info("[System Information]\n - os: " + operatingSystem + "\n - processor: " + str3 + "\n - memory: " + str4, 241, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Device.cs");
    }

    public enum Type
    {
      PC,
      Mobile,
      Tablet,
    }

    public delegate void ScreenSateChangedDelegate();
  }
}
