// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.BundleNameAttribute
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [AttributeUsage(AttributeTargets.Field)]
  public class BundleNameAttribute : Attribute
  {
    public readonly string bundleName;

    public BundleNameAttribute(string bundleName) => this.bundleName = !string.IsNullOrEmpty(bundleName) ? bundleName.Trim().ToLowerInvariant() : throw new ArgumentException(nameof (bundleName));
  }
}
