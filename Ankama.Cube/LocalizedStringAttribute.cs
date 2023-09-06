// Decompiled with JetBrains decompiler
// Type: LocalizedStringAttribute
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class LocalizedStringAttribute : PropertyAttribute
{
  public readonly string keyFormat;
  public readonly string collection;
  public readonly int lines;

  public LocalizedStringAttribute(string keyFormat, string collection, int lines = 1)
  {
    this.keyFormat = keyFormat;
    this.collection = collection;
    this.lines = lines;
  }
}
