// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.PanelMeshEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  [RequireComponent(typeof (Image))]
  public class PanelMeshEffect : BaseMeshEffect
  {
    private static Vector3[] s_vertexs = new Vector3[4];
    [SerializeField]
    private Image m_image;
    [SerializeField]
    private float m_diagonal;

    public override void ModifyMesh(VertexHelper vh)
    {
      if (!this.IsActive() || (UnityEngine.Object) this.m_image == (UnityEngine.Object) null)
        return;
      Color color = this.m_image.color;
      Sprite sprite = this.m_image.sprite;
      vh.Clear();
      Rect pixelAdjustedRect = this.m_image.GetPixelAdjustedRect();
      float pixelsPerUnit = this.m_image.pixelsPerUnit;
      float num1 = sprite.rect.width / pixelsPerUnit;
      float num2 = sprite.rect.height / pixelsPerUnit;
      RectTransform transform = this.transform as RectTransform;
      float num3 = transform.pivot.x * num1;
      float num4 = transform.pivot.y * num2;
      Vector4 vector4_1 = new Vector4(0.0f - num3, 0.0f - num4, num1 - num3, num2 - num4);
      Vector4 vector4_2 = new Vector4(Math.Max(vector4_1.x, pixelAdjustedRect.xMin), vector4_1.y, Math.Min(vector4_1.z, pixelAdjustedRect.xMax), vector4_1.w);
      PanelMeshEffect.s_vertexs = new Vector3[4]
      {
        new Vector3(vector4_2.x, vector4_2.y),
        new Vector3(vector4_2.x + this.m_diagonal, vector4_2.w),
        new Vector3(vector4_2.z, vector4_2.w),
        new Vector3(vector4_2.z - this.m_diagonal, vector4_2.y)
      };
      for (int index = 0; index < 4; ++index)
      {
        Vector2 uv0 = new Vector2((PanelMeshEffect.s_vertexs[index].x - vector4_1.x) / num1, (PanelMeshEffect.s_vertexs[index].y - vector4_1.y) / num2);
        vh.AddVert(PanelMeshEffect.s_vertexs[index], (Color32) color, uv0);
      }
      vh.AddTriangle(0, 1, 2);
      vh.AddTriangle(2, 3, 0);
    }
  }
}
