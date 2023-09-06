// Decompiled with JetBrains decompiler
// Type: InspectorButtonAttribute
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
  public readonly string methodName;
  public readonly object[] args;
  private float? m_buttonWidth;

  public float? buttonWidth
  {
    get => this.m_buttonWidth;
    set => this.m_buttonWidth = value;
  }

  public InspectorButtonAttribute(string methodName, params object[] args)
  {
    this.methodName = methodName;
    this.args = args;
  }
}
