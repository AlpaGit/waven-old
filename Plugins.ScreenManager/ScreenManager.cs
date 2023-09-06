// Decompiled with JetBrains decompiler
// Type: Ankama.ScreenManagement.ScreenManager
// Assembly: Plugins.ScreenManager, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E44D48D-5FB2-4F40-8C6D-7D760CDD308E
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.ScreenManager.dll

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Ankama.ScreenManagement
{
  [PublicAPI]
  public static class ScreenManager
  {
    private const string UnitySelectMonitor = "UnitySelectMonitor";
    private static readonly List<DisplayInfo> s_displayInfo = new List<DisplayInfo>(1);
    private static readonly List<Resolution> s_windowedResolutions = new List<Resolution>(32);
    private static ResolutionComparer s_resolutionComparer = new ResolutionComparer();
    private static readonly List<Resolution> s_resolutionBuffer = new List<Resolution>(32);
    private static bool s_hasDisplayInformation;
    private static bool s_isSwitchingMode;
    private static int s_primaryDisplayIndex;
    private static ScreenManagerError s_lastError = ScreenManagerError.None;
    private const int EnumCurrentSettings = -1;
    private const int MonitorDefaultToNull = 0;
    private const int MonitorDefaultToPrimary = 1;
    private const int MonitorDefaultToNearest = 2;
    private const string UnityWindowClassName = "UnityWndClass";
    private const FullScreenMode PlatformFullScreenMode = FullScreenMode.FullScreenWindow;
    private static IntPtr s_windowHandle;

    [PublicAPI]
    public static bool hasDisplayInformation => ScreenManager.s_hasDisplayInformation;

    [PublicAPI]
    public static bool isSwitchingMode => ScreenManager.s_isSwitchingMode;

    [PublicAPI]
    public static void UpdateDisplayInformation()
    {
      ScreenManager.RetrieveDisplayInformation();
      ScreenManager.ComputeWindowedResolutions();
      ScreenManager.s_hasDisplayInformation = true;
    }

    [PublicAPI]
    public static int GetDisplayCount() => ScreenManager.s_displayInfo.Count;

    [PublicAPI]
    public static DisplayInfo GetDisplayInfo(int index) => ScreenManager.s_displayInfo[index];

    [PublicAPI]
    public static DisplayInfo GetPrimaryDisplayInfo() => ScreenManager.s_displayInfo[ScreenManager.s_primaryDisplayIndex];

    [PublicAPI]
    public static int GetWindowedResolutionCount() => ScreenManager.s_windowedResolutions.Count;

    [PublicAPI]
    public static Resolution GetWindowedResolution(int index) => ScreenManager.s_windowedResolutions[index];

    [PublicAPI]
    public static bool TryGetCurrentDisplayIndex(out int displayIndex)
    {
      RectInt a;
      if (!ScreenManager.TryGetApplicationWindowFrame(out a))
      {
        displayIndex = 0;
        return false;
      }
      int num1 = 0;
      int num2 = int.MinValue;
      List<DisplayInfo> displayInfo1 = ScreenManager.s_displayInfo;
      int count = displayInfo1.Count;
      for (int index = 0; index < count; ++index)
      {
        DisplayInfo displayInfo2 = displayInfo1[index];
        int intersectionArea = ScreenManager.GetRectangleIntersectionArea(a, displayInfo2.frame);
        if (intersectionArea > num2)
        {
          num1 = index;
          num2 = intersectionArea;
        }
      }
      displayIndex = num1;
      return true;
    }

    [PublicAPI]
    public static IEnumerator SetWindowedMode(
      Resolution resolution,
      ScreenManagerWindowedPosition position = ScreenManagerWindowedPosition.Centered)
    {
      if (ScreenManager.s_isSwitchingMode)
      {
        ScreenManager.s_lastError = ScreenManagerError.Busy;
      }
      else
      {
        ScreenManager.s_isSwitchingMode = true;
        ScreenManager.s_lastError = ScreenManagerError.None;
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.Windowed, 0);
        yield return (object) null;
        switch (position)
        {
          case ScreenManagerWindowedPosition.Auto:
            ScreenManager.s_isSwitchingMode = false;
            break;
          case ScreenManagerWindowedPosition.Centered:
            if (!ScreenManager.CenterApplicationWindow())
            {
              Debug.LogWarning((object) "[ScreenManager] Failed to center window.");
              goto case ScreenManagerWindowedPosition.Auto;
            }
            else
              goto case ScreenManagerWindowedPosition.Auto;
          default:
            throw new ArgumentOutOfRangeException(nameof (position), (object) position, (string) null);
        }
      }
    }

    [PublicAPI]
    public static IEnumerator SetFullScreenMode(int displayIndex, Resolution resolution)
    {
      if (ScreenManager.s_isSwitchingMode)
        ScreenManager.s_lastError = ScreenManagerError.Busy;
      else if (displayIndex < 0 || displayIndex >= ScreenManager.s_displayInfo.Count)
      {
        ScreenManager.s_lastError = ScreenManagerError.InvalidArgument;
      }
      else
      {
        DisplayInfo displayInfo = ScreenManager.s_displayInfo[displayIndex];
        Resolution[] resolutions = displayInfo.resolutions;
        int length = resolutions.Length;
        for (int index = 0; index < length; ++index)
        {
          Resolution resolution1 = resolutions[index];
          if (resolution1.width == resolution.width && resolution1.height == resolution.height && resolution1.refreshRate == resolution.refreshRate)
          {
            int displayIndex1;
            if (!ScreenManager.TryGetCurrentDisplayIndex(out displayIndex1))
              Debug.LogWarning((object) "[ScreenManager] Could not get current display index.");
            else if (displayIndex1 != displayIndex)
            {
              PlayerPrefs.SetInt("UnitySelectMonitor", displayIndex);
              PlayerPrefs.Save();
              RectInt frame = displayInfo.frame;
              if (!ScreenManager.MoveApplicationWindowAtPosition(frame.x, frame.y))
              {
                ScreenManager.s_lastError = ScreenManagerError.SystemCallFailure;
                yield break;
              }
            }
            ScreenManager.s_isSwitchingMode = true;
            ScreenManager.s_lastError = ScreenManagerError.None;
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow, resolution.refreshRate);
            yield return (object) null;
            ScreenManager.s_isSwitchingMode = false;
            yield break;
          }
        }
        ScreenManager.s_lastError = ScreenManagerError.InvalidArgument;
      }
    }

    [PublicAPI]
    public static ScreenManagerError GetLastError() => ScreenManager.s_lastError;

    private static void ComputeWindowedResolutions()
    {
      List<DisplayInfo> displayInfo = ScreenManager.s_displayInfo;
      List<Resolution> resolutionBuffer = ScreenManager.s_resolutionBuffer;
      int count = displayInfo.Count;
      for (int index1 = 0; index1 < count; ++index1)
      {
        Resolution[] resolutions = displayInfo[index1].resolutions;
        int length = resolutions.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          Resolution resolution = resolutions[index2] with
          {
            refreshRate = 0
          };
          if (!ScreenManager.ContainsResolutionSize(resolutionBuffer, resolution))
            resolutionBuffer.Add(resolution);
        }
      }
      resolutionBuffer.Sort((IComparer<Resolution>) ScreenManager.s_resolutionComparer);
      ScreenManager.s_windowedResolutions.Clear();
      ScreenManager.s_windowedResolutions.AddRange((IEnumerable<Resolution>) resolutionBuffer);
      resolutionBuffer.Clear();
    }

    private static bool CenterApplicationWindow()
    {
      int displayIndex;
      if (!ScreenManager.TryGetCurrentDisplayIndex(out displayIndex))
      {
        Debug.LogWarning((object) "[ScreenManager] Could not get current display index.");
        return false;
      }
      RectInt frame = ScreenManager.s_displayInfo[displayIndex].frame;
      int width = Screen.width;
      int height = Screen.height;
      int num1 = frame.width - width >> 1;
      int num2 = frame.height - height >> 1;
      return ScreenManager.MoveApplicationWindowAtPosition(frame.x + num1, frame.y + num2);
    }

    public static bool ContainsResolutionSize(List<Resolution> list, Resolution resolution)
    {
      int width = resolution.width;
      int height = resolution.height;
      int count = list.Count;
      for (int index = 0; index < count; ++index)
      {
        Resolution resolution1 = list[index];
        if (resolution1.width == width && resolution1.height == height)
          return true;
      }
      return false;
    }

    public static bool ContainsResolutionSize(Resolution[] list, Resolution resolution)
    {
      int width = resolution.width;
      int height = resolution.height;
      int length = list.Length;
      for (int index = 0; index < length; ++index)
      {
        Resolution resolution1 = list[index];
        if (resolution1.width == width && resolution1.height == height)
          return true;
      }
      return false;
    }

    private static int GetRectangleIntersectionArea(RectInt a, RectInt b)
    {
      int num1 = Mathf.Max(a.xMin, b.xMin);
      int num2 = Mathf.Min(a.xMax, b.xMax);
      int num3 = Mathf.Max(a.yMin, b.yMin);
      int num4 = Mathf.Min(a.yMax, b.yMax);
      return num1 < num2 && num3 < num4 ? (num2 - num1) * (num4 - num3) : 0;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumDisplayDevices(
      string deviceName,
      uint deviceNumber,
      ref ScreenManager.DisplayDevice displayDevice,
      uint flags);

    [DllImport("User32.dll")]
    private static extern bool EnumDisplaySettings(
      string deviceName,
      int index,
      ref ScreenManager.DisplayEnvironmentMode displayEnvironmentMode,
      uint flags);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    [DllImport("user32.dll")]
    private static extern bool EnumThreadWindows(
      uint threadId,
      ScreenManager.EnumWindowsProc enumFunc,
      IntPtr param);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetClassName(
      IntPtr windowHandle,
      StringBuilder stringBuilder,
      int maxCount);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr windowHandle, out ScreenManager.Rect rect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool MoveWindow(
      IntPtr windowHandle,
      int x,
      int y,
      int width,
      int height,
      bool repaint);

    [RuntimeInitializeOnLoadMethod]
    private static void SystemInitialization()
    {
      uint currentThreadId;
      try
      {
        currentThreadId = ScreenManager.GetCurrentThreadId();
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[ScreenManager] CRITICAL - Could not retrieve current thread ID:");
        Debug.LogException(ex);
        return;
      }
      try
      {
        ScreenManager.EnumThreadWindows(currentThreadId, new ScreenManager.EnumWindowsProc(ScreenManager.EnumWindowProcCallback), IntPtr.Zero);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[ScreenManager] CRITICAL - Could not enumerate thread windows:");
        Debug.LogException(ex);
        return;
      }
      if (!(ScreenManager.s_windowHandle == IntPtr.Zero))
        return;
      Debug.LogError((object) "[ScreenManager] CRITICAL - Could not find Unity window.");
    }

    private static void RetrieveDisplayInformation()
    {
      ScreenManager.s_primaryDisplayIndex = 0;
      List<DisplayInfo> displayInfo1 = ScreenManager.s_displayInfo;
      displayInfo1.Clear();
      try
      {
        ScreenManager.DisplayDevice displayDevice1 = new ScreenManager.DisplayDevice();
        displayDevice1.size = Marshal.SizeOf<ScreenManager.DisplayDevice>(displayDevice1);
        ScreenManager.DisplayDevice displayDevice2 = new ScreenManager.DisplayDevice();
        displayDevice2.size = Marshal.SizeOf<ScreenManager.DisplayDevice>(displayDevice2);
        ScreenManager.DisplayEnvironmentMode displayEnvironmentMode = new ScreenManager.DisplayEnvironmentMode();
        displayEnvironmentMode.size = (ushort) Marshal.SizeOf<ScreenManager.DisplayEnvironmentMode>(displayEnvironmentMode);
        List<Resolution> resolutionBuffer = ScreenManager.s_resolutionBuffer;
        ResolutionComparer resolutionComparer = ScreenManager.s_resolutionComparer;
        uint deviceNumber = 0;
        while (ScreenManager.EnumDisplayDevices((string) null, deviceNumber, ref displayDevice1, 0U))
        {
          if (displayDevice1.stateFlags.HasFlag((Enum) ScreenManager.DisplayDeviceStateFlags.AttachedToDesktop))
          {
            string deviceName = displayDevice1.deviceName;
            string deviceString = displayDevice1.deviceString;
            bool isPrimaryDisplay = displayDevice1.stateFlags.HasFlag((Enum) ScreenManager.DisplayDeviceStateFlags.PrimaryDevice);
            string name;
            try
            {
              name = !ScreenManager.EnumDisplayDevices(deviceName, 0U, ref displayDevice2, 0U) ? string.Empty : displayDevice2.deviceString;
            }
            catch (Exception ex)
            {
              Debug.LogWarning((object) ("[ScreenManager] Failed to retrieve display friendly name for display named '" + deviceName + "':"));
              Debug.LogException(ex);
              name = string.Empty;
            }
            try
            {
              if (!ScreenManager.EnumDisplaySettings(deviceName, -1, ref displayEnvironmentMode, 0U))
              {
                Debug.LogError((object) ("[ScreenManager] Failed to retrieve current settings for display named '" + deviceName + "'."));
                continue;
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ("[ScreenManager] Failed to retrieve current display settings for display named '" + deviceName + "':"));
              Debug.LogException(ex);
              continue;
            }
            RectInt frame = new RectInt(displayEnvironmentMode.position.x, displayEnvironmentMode.position.y, (int) displayEnvironmentMode.pixelWidth, (int) displayEnvironmentMode.pixelHeight);
            Resolution systemResolution = new Resolution()
            {
              width = frame.width,
              height = frame.height,
              refreshRate = (int) displayEnvironmentMode.displayFrequency
            };
            int index = 0;
            try
            {
              for (; ScreenManager.EnumDisplaySettings(deviceName, index, ref displayEnvironmentMode, 0U); ++index)
              {
                if (displayEnvironmentMode.bitsPerPixel == 32U && displayEnvironmentMode.displayFixedOutput == 0U)
                {
                  Resolution resolution = new Resolution()
                  {
                    width = (int) displayEnvironmentMode.pixelWidth,
                    height = (int) displayEnvironmentMode.pixelHeight,
                    refreshRate = (int) displayEnvironmentMode.displayFrequency
                  };
                  resolutionBuffer.Add(resolution);
                }
              }
            }
            catch (Exception ex)
            {
              resolutionBuffer.Clear();
              Debug.LogError((object) string.Format("[ScreenManager] Failed to retrieve display settings for display named '{0}' at mode index {1}:", (object) deviceName, (object) index));
              Debug.LogException(ex);
              continue;
            }
            resolutionBuffer.Sort((IComparer<Resolution>) resolutionComparer);
            DisplayInfo displayInfo2 = new DisplayInfo(name, deviceString, isPrimaryDisplay, frame, systemResolution, resolutionBuffer);
            if (isPrimaryDisplay)
              ScreenManager.s_primaryDisplayIndex = displayInfo1.Count;
            displayInfo1.Add(displayInfo2);
            resolutionBuffer.Clear();
          }
          ++deviceNumber;
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[ScreenManager] Failed to enumerate displays:");
        Debug.LogException(ex);
      }
    }

    private static bool MoveApplicationWindowAtPosition(int x, int y)
    {
      IntPtr windowHandle = ScreenManager.s_windowHandle;
      if (windowHandle == IntPtr.Zero)
      {
        Debug.LogError((object) "[ScreenManager] No application window handle.");
        return false;
      }
      ScreenManager.Rect rect;
      try
      {
        if (!ScreenManager.GetWindowRect(windowHandle, out rect))
        {
          Debug.LogError((object) string.Format("[ScreenManager] Failed to get application window rect (WIN32ERROR: {0}).", (object) Marshal.GetLastWin32Error()));
          return false;
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[ScreenManager] Failed to get application window rect:");
        Debug.LogException(ex);
        return false;
      }
      int width = rect.right - rect.left;
      int height = rect.bottom - rect.top;
      try
      {
        if (!ScreenManager.MoveWindow(windowHandle, x, y, width, height, false))
        {
          Debug.LogError((object) string.Format("[ScreenManager] Failed to move application window (WIN32ERROR: {0}).", (object) Marshal.GetLastWin32Error()));
          return false;
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[ScreenManager] Failed to move application window:");
        Debug.LogException(ex);
        return false;
      }
      return true;
    }

    private static bool TryGetApplicationWindowFrame(out RectInt value)
    {
      IntPtr windowHandle = ScreenManager.s_windowHandle;
      if (windowHandle == IntPtr.Zero)
      {
        Debug.LogError((object) "[ScreenManager] No application window handle.");
        value = new RectInt();
        return false;
      }
      ScreenManager.Rect rect;
      try
      {
        if (!ScreenManager.GetWindowRect(windowHandle, out rect))
        {
          Debug.LogError((object) string.Format("[ScreenManager] Failed to get application window rect (WIN32ERROR: {0}).", (object) Marshal.GetLastWin32Error()));
          value = new RectInt();
          return false;
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[ScreenManager] Failed to get application window rect:");
        Debug.LogException(ex);
        value = new RectInt();
        return false;
      }
      value = new RectInt(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
      return true;
    }

    private static bool EnumWindowProcCallback(IntPtr windowHandle, IntPtr param)
    {
      int length = "UnityWndClass".Length;
      StringBuilder stringBuilder = new StringBuilder(length);
      int className;
      try
      {
        className = ScreenManager.GetClassName(windowHandle, stringBuilder, length + 1);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[ScreenManager] Failed to get window class name:");
        Debug.LogException(ex);
        return true;
      }
      if (length != className || !stringBuilder.ToString().Equals("UnityWndClass"))
        return true;
      ScreenManager.s_windowHandle = windowHandle;
      return false;
    }

    private struct DisplayDevice
    {
      public int size;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public readonly string deviceName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public readonly string deviceString;
      [MarshalAs(UnmanagedType.U4)]
      public readonly ScreenManager.DisplayDeviceStateFlags stateFlags;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public readonly string deviceID;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public readonly string deviceKey;
    }

    [Flags]
    private enum DisplayDeviceStateFlags
    {
      [UsedImplicitly] AttachedToDesktop = 1,
      [UsedImplicitly] MultiDriver = 2,
      [UsedImplicitly] PrimaryDevice = 4,
      [UsedImplicitly] MirroringDriver = 8,
      [UsedImplicitly] VgaCompatible = 16, // 0x00000010
      [UsedImplicitly] Removable = 32, // 0x00000020
      [UsedImplicitly] ModesPruned = 134217728, // 0x08000000
      [UsedImplicitly] Remote = 67108864, // 0x04000000
      [UsedImplicitly] Disconnect = 33554432, // 0x02000000
    }

    private struct Point
    {
      public readonly int x;
      public readonly int y;
    }

    private struct DisplayEnvironmentMode
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public readonly string deviceName;
      public readonly ushort specVersion;
      public readonly ushort driverVersion;
      public ushort size;
      public readonly ushort driverExtra;
      public readonly uint fields;
      public readonly ScreenManager.Point position;
      public readonly uint displayOrientation;
      public readonly uint displayFixedOutput;
      public readonly short color;
      public readonly short duplex;
      public readonly short yResolution;
      public readonly short trueTypeOption;
      public readonly short collate;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public readonly string formName;
      public readonly ushort logPixels;
      public readonly uint bitsPerPixel;
      public readonly uint pixelWidth;
      public readonly uint pixelHeight;
      public readonly uint displayFlags;
      public readonly uint displayFrequency;
    }

    public struct Rect
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    public delegate bool EnumWindowsProc(IntPtr windowHandle, IntPtr param);
  }
}
