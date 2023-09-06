// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.InspectorCommentAttribute
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Utility
{
  [AttributeUsage(AttributeTargets.Field)]
  public class InspectorCommentAttribute : PropertyAttribute
  {
    public readonly string text;
    public readonly float height;

    public InspectorCommentAttribute(string text, float height = 18f)
    {
      this.height = height;
      this.text = text;
    }
  }
}
