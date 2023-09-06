// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.ImageSlice
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public class ImageSlice : Image
  {
    private static readonly Vector3[] s_Xy = new Vector3[4];
    private static readonly Vector3[] s_Uv = new Vector3[4];
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_fillStart;

    private Sprite activeSprite => !((Object) this.overrideSprite == (Object) null) ? this.overrideSprite : this.sprite;

    public float fillStart
    {
      get => this.m_fillStart;
      set
      {
        float y = Mathf.Clamp01(value);
        if (EqualityComparer<float>.Default.Equals(this.m_fillStart, y))
          return;
        this.m_fillStart = y;
        this.SetVerticesDirty();
      }
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
      if ((Object) this.activeSprite == (Object) null)
        base.OnPopulateMesh(toFill);
      else
        this.GenerateFilledSprite(toFill, this.preserveAspect);
    }

    private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
    {
      Vector4 vector4 = !((Object) this.activeSprite == (Object) null) ? DataUtility.GetPadding(this.activeSprite) : Vector4.zero;
      Vector2 vector2_1;
      if ((Object) this.activeSprite == (Object) null)
      {
        vector2_1 = Vector2.zero;
      }
      else
      {
        Rect rect = this.activeSprite.rect;
        double width = (double) rect.width;
        rect = this.activeSprite.rect;
        double height = (double) rect.height;
        vector2_1 = new Vector2((float) width, (float) height);
      }
      Vector2 vector2_2 = vector2_1;
      Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
      int num1 = Mathf.RoundToInt(vector2_2.x);
      int num2 = Mathf.RoundToInt(vector2_2.y);
      Vector4 drawingDimensions = new Vector4(vector4.x / (float) num1, vector4.y / (float) num2, ((float) num1 - vector4.z) / (float) num1, ((float) num2 - vector4.w) / (float) num2);
      if (shouldPreserveAspect && (double) vector2_2.sqrMagnitude > 0.0)
      {
        float num3 = vector2_2.x / vector2_2.y;
        float num4 = pixelAdjustedRect.width / pixelAdjustedRect.height;
        if ((double) num3 > (double) num4)
        {
          float height = pixelAdjustedRect.height;
          pixelAdjustedRect.height = pixelAdjustedRect.width * (1f / num3);
          pixelAdjustedRect.y += (height - pixelAdjustedRect.height) * this.rectTransform.pivot.y;
        }
        else
        {
          float width = pixelAdjustedRect.width;
          pixelAdjustedRect.width = pixelAdjustedRect.height * num3;
          pixelAdjustedRect.x += (width - pixelAdjustedRect.width) * this.rectTransform.pivot.x;
        }
      }
      drawingDimensions = new Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * drawingDimensions.x, pixelAdjustedRect.y + pixelAdjustedRect.height * drawingDimensions.y, pixelAdjustedRect.x + pixelAdjustedRect.width * drawingDimensions.z, pixelAdjustedRect.y + pixelAdjustedRect.height * drawingDimensions.w);
      return drawingDimensions;
    }

    private void GenerateFilledSprite(VertexHelper toFill, bool preserveAspect)
    {
      toFill.Clear();
      if ((double) this.fillAmount < 0.001)
        return;
      Vector4 drawingDimensions = this.GetDrawingDimensions(preserveAspect);
      Vector4 vector4 = !((Object) this.activeSprite != (Object) null) ? Vector4.zero : DataUtility.GetOuterUV(this.activeSprite);
      UIVertex.simpleVert.color = (Color32) this.color;
      float x1 = vector4.x;
      float y1 = vector4.y;
      float x2 = vector4.z;
      float y2 = vector4.w;
      float num1 = Mathf.Min(this.fillAmount, 1f - this.m_fillStart);
      if (this.fillMethod == Image.FillMethod.Horizontal || this.fillMethod == Image.FillMethod.Vertical)
      {
        if (this.fillMethod == Image.FillMethod.Horizontal)
        {
          float num2 = x2 - x1;
          float num3 = drawingDimensions.z - drawingDimensions.x;
          if (this.fillOrigin == 0)
          {
            drawingDimensions.x += num3 * this.fillStart;
            drawingDimensions.z = drawingDimensions.x + num3 * num1;
            x1 += num2 * this.fillStart;
            x2 = x1 + num2 * num1;
          }
          else
          {
            drawingDimensions.z -= num3 * this.fillStart;
            drawingDimensions.x = drawingDimensions.z - num3 * num1;
            x2 -= num2 * this.fillStart;
            x1 = x2 - num2 * num1;
          }
        }
        else
        {
          float num4 = y2 - y1;
          float num5 = drawingDimensions.w - drawingDimensions.y;
          if (this.fillOrigin == 0)
          {
            drawingDimensions.y += num5 * this.fillStart;
            drawingDimensions.w = drawingDimensions.y + num5 * num1;
            y1 += num4 * this.fillStart;
            y2 = y1 + num4 * num1;
          }
          else
          {
            drawingDimensions.w -= num5 * this.fillStart;
            drawingDimensions.y = drawingDimensions.w - num5 * num1;
            y2 -= num4 * this.fillStart;
            y1 = y2 - num4 * num1;
          }
        }
      }
      ImageSlice.s_Xy[0] = (Vector3) new Vector2(drawingDimensions.x, drawingDimensions.y);
      ImageSlice.s_Xy[1] = (Vector3) new Vector2(drawingDimensions.x, drawingDimensions.w);
      ImageSlice.s_Xy[2] = (Vector3) new Vector2(drawingDimensions.z, drawingDimensions.w);
      ImageSlice.s_Xy[3] = (Vector3) new Vector2(drawingDimensions.z, drawingDimensions.y);
      ImageSlice.s_Uv[0] = (Vector3) new Vector2(x1, y1);
      ImageSlice.s_Uv[1] = (Vector3) new Vector2(x1, y2);
      ImageSlice.s_Uv[2] = (Vector3) new Vector2(x2, y2);
      ImageSlice.s_Uv[3] = (Vector3) new Vector2(x2, y1);
      ImageSlice.AddQuad(toFill, ImageSlice.s_Xy, (Color32) this.color, ImageSlice.s_Uv);
    }

    private static void AddQuad(
      VertexHelper vertexHelper,
      Vector3[] quadPositions,
      Color32 color,
      Vector3[] quadUVs)
    {
      int currentVertCount = vertexHelper.currentVertCount;
      for (int index = 0; index < 4; ++index)
        vertexHelper.AddVert(quadPositions[index], color, (Vector2) quadUVs[index]);
      vertexHelper.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
      vertexHelper.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
    }
  }
}
