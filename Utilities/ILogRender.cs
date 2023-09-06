// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.ILogRender
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Utilities
{
  [PublicAPI]
  public interface ILogRender
  {
    string Render(
      string tag,
      string message,
      string filename,
      int line,
      LogType logType,
      DateTime date);
  }
}
