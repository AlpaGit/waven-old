// Decompiled with JetBrains decompiler
// Type: DataEditor.EditableDataUtility
// Assembly: DataEditorRuntime, Version=1.0.6990.32389, Culture=neutral, PublicKeyToken=null
// MVID: 45C45C6B-0733-4518-B038-C58DEC652313
// Assembly location: E:\WAVEN\Waven_Data\Managed\DataEditorRuntime.dll

using System.Collections.Generic;
using UnityEngine;

namespace DataEditor
{
  public static class EditableDataUtility
  {
    public static int GenerateNewId(ICollection<int> existingIds, int randomizationRange = 1000)
    {
      int min = int.MinValue;
      foreach (int existingId in (IEnumerable<int>) existingIds)
      {
        if (existingId > min)
          min = existingId;
      }
      int max1 = Random.Range(1, randomizationRange);
      if (max1 > min)
        return max1;
      foreach (int existingId1 in (IEnumerable<int>) existingIds)
      {
        if (existingId1 == max1)
        {
          int num = 0;
          int max2 = min;
          foreach (int existingId2 in (IEnumerable<int>) existingIds)
          {
            if (existingId2 > max1 && existingId2 < max2)
              max2 = existingId2;
            if (existingId2 < max1 && existingId2 > num)
              num = existingId2;
          }
          if (max2 - max1 > 1)
            return Random.Range(max1 + 1, max2);
          return max1 - num > 1 ? Random.Range(num + 1, max1) : Random.Range(min, min + max1) + 1;
        }
      }
      return max1;
    }
  }
}
