// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetReferences.AssetReferenceType
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System.Diagnostics;
using UnityEngine;

namespace Ankama.AssetManagement.AssetReferences
{
  [PublicAPI]
  [Conditional("UNITY_EDITOR")]
  public class AssetReferenceType : PropertyAttribute
  {
    [PublicAPI]
    public AssetReferenceType([NotNull] System.Type type)
    {
    }
  }
}
