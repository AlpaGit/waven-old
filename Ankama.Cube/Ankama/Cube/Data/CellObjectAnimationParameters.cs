// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CellObjectAnimationParameters
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using FMODUnity;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Animations/CellObject Animation Parameters", fileName = "New CellObject Animation Parameters")]
  public class CellObjectAnimationParameters : ScriptableObject
  {
    public const string AudioStrengthParameterName = "Strength";
    [SerializeField]
    private AnimationCurve m_curve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1f, 1f);
    [SerializeField]
    private float m_duration = 1f;
    [SerializeField]
    private CellObjectAnimationParameters.Shape m_shape;
    [SerializeField]
    private Vector2 m_size = Vector2.one;
    [SerializeField]
    private Quaternion m_direction = Quaternion.identity;
    [SerializeField]
    private float m_propagationSpeed;
    [SerializeField]
    private float m_propagationDistance = 2f;
    [SerializeField]
    private AudioReferenceWithParameters m_sound;

    public bool isValid => (double) this.m_duration > 0.0;

    public AudioReferenceWithParameters sound => this.m_sound;

    public float totalDuration => (double) this.m_propagationSpeed > 0.0 && (double) this.m_propagationDistance > 0.0 ? this.m_duration * (float) (1.0 + (double) this.m_propagationDistance / (double) this.m_propagationSpeed) : this.m_duration;

    public void GetBounds(
      Vector2Int origin,
      Quaternion rotation,
      float time,
      out Vector2Int min,
      out Vector2Int max)
    {
      float propagationSpeed = this.m_propagationSpeed;
      float propagationDistance = this.m_propagationDistance;
      bool flag = (double) propagationSpeed > 0.0 && (double) propagationDistance > 0.0;
      switch (this.m_shape)
      {
        case CellObjectAnimationParameters.Shape.Disc:
        case CellObjectAnimationParameters.Shape.Donut:
          int num = !flag ? Mathf.CeilToInt(this.m_size.y) : Mathf.CeilToInt(this.m_size.y * (1f + Mathf.Max(propagationDistance, propagationSpeed * time)));
          Vector2Int vector2Int1 = new Vector2Int(num, num);
          min = origin - vector2Int1;
          max = origin + vector2Int1;
          break;
        case CellObjectAnimationParameters.Shape.Line:
          Quaternion rotation1 = rotation * this.m_direction;
          Vector2Int vector2Int2 = new Vector2Int(!flag ? Mathf.CeilToInt(this.m_size.x) : Mathf.CeilToInt(this.m_size.x + Mathf.Max(propagationDistance, propagationSpeed * time)), 0);
          Vector2Int vector2Int3 = origin + vector2Int2.Rotate(rotation1);
          min = new Vector2Int(Mathf.Min(origin.x, vector2Int3.x), Mathf.Min(origin.y, vector2Int3.y));
          max = new Vector2Int(Mathf.Max(origin.x, vector2Int3.x), Mathf.Max(origin.y, vector2Int3.y));
          break;
        case CellObjectAnimationParameters.Shape.Rectangle:
          Quaternion rotation2 = rotation * this.m_direction;
          Vector2Int vector2Int4 = new Vector2Int(!flag ? Mathf.CeilToInt(this.m_size.x) : Mathf.CeilToInt(this.m_size.x + Mathf.Max(propagationDistance, propagationSpeed * time)), 0);
          Vector2Int vector2Int5 = new Vector2Int(0, Mathf.CeilToInt(0.5f * this.m_size.y));
          Vector2Int vector2Int6 = vector2Int4.Rotate(rotation2);
          Vector2Int vector2Int7 = vector2Int5.Rotate(rotation2);
          Vector2Int vector2Int8 = origin - vector2Int7;
          Vector2Int vector2Int9 = origin + vector2Int6 + vector2Int7;
          min = new Vector2Int(Mathf.Min(vector2Int8.x, vector2Int9.x), Mathf.Min(vector2Int8.y, vector2Int9.y));
          max = new Vector2Int(Mathf.Max(vector2Int8.x, vector2Int9.x), Mathf.Max(vector2Int8.y, vector2Int9.y));
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public float Compute(Vector2Int coords, Vector2Int origin, Quaternion rotation, float time)
    {
      float propagationSpeed = this.m_propagationSpeed;
      float propagationDistance = this.m_propagationDistance;
      bool flag = (double) propagationSpeed > 0.0 && (double) propagationDistance > 0.0;
      switch (this.m_shape)
      {
        case CellObjectAnimationParameters.Shape.Disc:
          float y1 = this.m_size.y;
          float num1 = Vector2Int.Distance(coords, origin);
          if (!flag)
            return (1f - Mathf.Clamp01(num1 - y1)) * this.m_curve.Evaluate(time / this.m_duration);
          float num2 = Mathf.Max(0.0f, num1 - y1);
          float num3 = num2 / propagationSpeed;
          float num4 = time * propagationSpeed;
          float num5 = Mathf.Min(1f, num2 / propagationDistance);
          return (float) ((1.0 - (double) Mathf.Clamp01(num1 - (y1 + num4))) * (1.0 - (double) num5)) * this.m_curve.Evaluate((time - num3) / this.m_duration);
        case CellObjectAnimationParameters.Shape.Donut:
          float x = this.m_size.x;
          float y2 = this.m_size.y;
          float num6 = Vector2Int.Distance(coords, origin);
          if (!flag)
            return (1f - Mathf.Max(Mathf.Clamp01(x - num6), Mathf.Clamp01(num6 - y2))) * this.m_curve.Evaluate(time / this.m_duration);
          float num7 = Mathf.Max(0.0f, num6 - y2);
          float num8 = num7 / propagationSpeed;
          float num9 = time * propagationSpeed;
          float num10 = Mathf.Min(1f, num7 / propagationDistance);
          return (float) ((1.0 - (double) Mathf.Max(Mathf.Clamp01(x - num6), Mathf.Clamp01(num6 - (y2 + num9)))) * (1.0 - (double) num10)) * this.m_curve.Evaluate((time - num8) / this.m_duration);
        case CellObjectAnimationParameters.Shape.Line:
          if (!flag)
            return this.m_curve.Evaluate(time / this.m_duration);
          Vector2Int vector2Int1 = coords - origin;
          float num11 = Mathf.Max(0.0f, (float) Math.Abs(vector2Int1.x + vector2Int1.y) - this.m_size.x);
          float num12 = num11 / propagationSpeed;
          return (1f - Mathf.Min(1f, num11 / propagationDistance)) * this.m_curve.Evaluate((time - num12) / this.m_duration);
        case CellObjectAnimationParameters.Shape.Rectangle:
          float num13 = 0.5f * this.m_size.y;
          Vector2Int scale1 = Vector2Int.right.Rotate(rotation * this.m_direction);
          Vector2Int scale2 = new Vector2Int(scale1.y, scale1.x);
          Vector2Int vector2Int2 = coords - origin;
          Vector2Int vector2Int3 = vector2Int2;
          vector2Int3.Scale(scale2);
          float num14 = Mathf.Clamp01((float) Mathf.Abs(vector2Int3.x + vector2Int3.y) - num13);
          if (!flag)
            return (1f - num14) * this.m_curve.Evaluate(time / this.m_duration);
          Vector2Int vector2Int4 = vector2Int2;
          vector2Int4.Scale(scale1);
          int num15 = Mathf.Abs(vector2Int4.x + vector2Int4.y);
          float num16 = (float) num15 / propagationSpeed;
          return (float) ((1.0 - (double) Mathf.Min(1f, (float) num15 / propagationDistance)) * (1.0 - (double) num14)) * this.m_curve.Evaluate((time - num16) / this.m_duration);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public enum Shape
    {
      Disc,
      Donut,
      Line,
      Rectangle,
    }
  }
}
