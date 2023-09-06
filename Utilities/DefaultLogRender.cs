// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.DefaultLogRender
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ankama.Utilities
{
  internal class DefaultLogRender : ILogRender
  {
    public string Render(
      string tag,
      string message,
      string filename,
      int line,
      LogType logType,
      DateTime date)
    {
      string str1 = message ?? "Null";
      string str2 = date.ToString("HH:mm:ss.fff", (IFormatProvider) DateTimeFormatInfo.InvariantInfo);
      string fileName = Path.GetFileName(filename);
      return tag != null ? string.Format("{0} {1} [{2}] ({3}:{4}) - {5}", (object) str2, (object) DefaultLogRender.GetLevel(logType), (object) tag, (object) fileName, (object) line, (object) str1) : string.Format("{0} {1} ({2}:{3}) - {4}", (object) str2, (object) DefaultLogRender.GetLevel(logType), (object) fileName, (object) line, (object) str1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetLevel(LogType logType)
    {
      switch (logType)
      {
        case LogType.Error:
          return " ERROR";
        case LogType.Assert:
          return "ASSERT";
        case LogType.Warning:
          return "  WARN";
        case LogType.Log:
          return "  INFO";
        case LogType.Exception:
          return "EXCEPTION";
        default:
          return "";
      }
    }
  }
}
