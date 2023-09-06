// Decompiled with JetBrains decompiler
// Type: Ankama.ScreenManagement.ResolutionComparer
// Assembly: Plugins.ScreenManager, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E44D48D-5FB2-4F40-8C6D-7D760CDD308E
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.ScreenManager.dll

using System.Collections.Generic;
using UnityEngine;

namespace Ankama.ScreenManagement
{
  internal class ResolutionComparer : IComparer<Resolution>
  {
    public int Compare(Resolution a, Resolution b)
    {
      int num1 = a.width.CompareTo(b.width);
      if (num1 != 0)
        return num1;
      int num2 = a.height.CompareTo(b.height);
      return num2 != 0 ? num2 : a.refreshRate.CompareTo(b.refreshRate);
    }
  }
}
