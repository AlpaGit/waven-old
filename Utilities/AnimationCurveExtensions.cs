// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.AnimationCurveExtensions
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Utilities
{
  [PublicAPI]
  public static class AnimationCurveExtensions
  {
    [PublicAPI]
    public static float GetMinimumValue(this AnimationCurve curve)
    {
      Keyframe[] keys = curve.keys;
      int length = keys.Length;
      switch (length)
      {
        case 0:
          return 0.0f;
        case 1:
          return keys[0].value;
        default:
          float b1 = float.MaxValue;
          for (int index = 0; index < length - 1; ++index)
          {
            Keyframe keyframe1 = keys[index];
            Keyframe keyframe2 = keys[index + 1];
            float b2 = float.MaxValue;
            float num1 = keyframe2.time - keyframe1.time;
            float a = keyframe1.value;
            float num2 = keyframe2.value;
            float num3 = keyframe1.outTangent * num1;
            float num4 = keyframe2.inTangent * num1;
            float num5 = (float) ((double) num3 + (double) num4 + 2.0 * (double) a - 2.0 * (double) num2);
            float num6 = (float) (-2.0 * (double) num3 - (double) num4 - 3.0 * (double) a + 3.0 * (double) num2);
            float num7 = num3;
            float num8 = a;
            if ((double) Math.Abs(num5) > 1.4012984643248171E-45)
            {
              float f = (float) ((double) num6 * (double) num6 - 3.0 * (double) num5 * (double) num7);
              if ((double) f > 0.0)
              {
                float num9 = Mathf.Sqrt(f);
                float num10 = 3f * num5;
                float num11 = (-num6 - num9) / num10;
                float num12 = (-num6 + num9) / num10;
                b2 = Mathf.Min((float) ((double) num5 * (double) num11 * (double) num11 * (double) num11 + (double) num6 * (double) num11 * (double) num11 + (double) num7 * (double) num11) + num8, (float) ((double) num5 * (double) num12 * (double) num12 * (double) num12 + (double) num6 * (double) num12 * (double) num12 + (double) num7 * (double) num12) + num8);
              }
            }
            float num13 = Mathf.Min(a, b2);
            if ((double) num13 < (double) b1)
              b1 = num13;
          }
          return Mathf.Min(keys[length - 1].value, b1);
      }
    }

    [PublicAPI]
    public static float GetMaximumValue(this AnimationCurve curve)
    {
      Keyframe[] keys = curve.keys;
      int length = keys.Length;
      switch (length)
      {
        case 0:
          return 0.0f;
        case 1:
          return keys[0].value;
        default:
          float b1 = float.MinValue;
          for (int index = 0; index < length - 1; ++index)
          {
            Keyframe keyframe1 = keys[index];
            Keyframe keyframe2 = keys[index + 1];
            float b2 = float.MinValue;
            float num1 = keyframe2.time - keyframe1.time;
            float a = keyframe1.value;
            float num2 = keyframe2.value;
            float num3 = keyframe1.outTangent * num1;
            float num4 = keyframe2.inTangent * num1;
            float num5 = (float) ((double) num3 + (double) num4 + 2.0 * (double) a - 2.0 * (double) num2);
            float num6 = (float) (-2.0 * (double) num3 - (double) num4 - 3.0 * (double) a + 3.0 * (double) num2);
            float num7 = num3;
            float num8 = a;
            if ((double) Math.Abs(num5) > 1.4012984643248171E-45)
            {
              float f = (float) ((double) num6 * (double) num6 - 3.0 * (double) num5 * (double) num7);
              if ((double) f > 0.0)
              {
                float num9 = Mathf.Sqrt(f);
                float num10 = 3f * num5;
                float num11 = (-num6 - num9) / num10;
                float num12 = (-num6 + num9) / num10;
                b2 = Mathf.Max((float) ((double) num5 * (double) num11 * (double) num11 * (double) num11 + (double) num6 * (double) num11 * (double) num11 + (double) num7 * (double) num11) + num8, (float) ((double) num5 * (double) num12 * (double) num12 * (double) num12 + (double) num6 * (double) num12 * (double) num12 + (double) num7 * (double) num12) + num8);
              }
            }
            float num13 = Mathf.Max(a, b2);
            if ((double) num13 > (double) b1)
              b1 = num13;
          }
          return Mathf.Max(keys[length - 1].value, b1);
      }
    }

    [PublicAPI]
    public static Vector2 GetValueRange(this AnimationCurve curve)
    {
      Keyframe[] keys = curve.keys;
      int length = keys.Length;
      switch (length)
      {
        case 0:
          return new Vector2(0.0f, 0.0f);
        case 1:
          double num1 = (double) keys[0].value;
          return new Vector2((float) num1, (float) num1);
        default:
          float b1 = float.MaxValue;
          float b2 = float.MinValue;
          for (int index = 0; index < length - 1; ++index)
          {
            Keyframe keyframe1 = keys[index];
            Keyframe keyframe2 = keys[index + 1];
            float b3 = float.MaxValue;
            float b4 = float.MinValue;
            float num2 = keyframe2.time - keyframe1.time;
            float a1 = keyframe1.value;
            float num3 = keyframe2.value;
            float num4 = keyframe1.outTangent * num2;
            float num5 = keyframe2.inTangent * num2;
            float num6 = (float) ((double) num4 + (double) num5 + 2.0 * (double) a1 - 2.0 * (double) num3);
            float num7 = (float) (-2.0 * (double) num4 - (double) num5 - 3.0 * (double) a1 + 3.0 * (double) num3);
            float num8 = num4;
            float num9 = a1;
            if ((double) Math.Abs(num6) > 1.4012984643248171E-45)
            {
              float f = (float) ((double) num7 * (double) num7 - 3.0 * (double) num6 * (double) num8);
              if ((double) f > 0.0)
              {
                float num10 = Mathf.Sqrt(f);
                float num11 = 3f * num6;
                float num12 = (-num7 - num10) / num11;
                float num13 = (-num7 + num10) / num11;
                double a2 = (double) num6 * (double) num12 * (double) num12 * (double) num12 + (double) num7 * (double) num12 * (double) num12 + (double) num8 * (double) num12 + (double) num9;
                float b5 = (float) ((double) num6 * (double) num13 * (double) num13 * (double) num13 + (double) num7 * (double) num13 * (double) num13 + (double) num8 * (double) num13) + num9;
                b3 = Mathf.Min((float) a2, b5);
                b4 = Mathf.Max((float) a2, b5);
              }
            }
            float num14 = Mathf.Min(a1, b3);
            float num15 = Mathf.Max(a1, b4);
            if ((double) num14 < (double) b1)
              b1 = num14;
            if ((double) num15 > (double) b2)
              b2 = num15;
          }
          double a = (double) keys[length - 1].value;
          return new Vector2(Mathf.Min((float) a, b1), Mathf.Max((float) a, b2));
      }
    }
  }
}
