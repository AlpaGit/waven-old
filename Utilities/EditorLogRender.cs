// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.EditorLogRender
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace Ankama.Utilities
{
  internal class EditorLogRender : ILogRender
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
      if (tag == null)
        tag = Path.GetFileNameWithoutExtension(filename);
      return str2 + " [" + tag + "] - " + str1;
    }
  }
}
