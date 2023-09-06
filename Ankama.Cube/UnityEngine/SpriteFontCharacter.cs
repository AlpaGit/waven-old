// Decompiled with JetBrains decompiler
// Type: UnityEngine.SpriteFontCharacter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace UnityEngine
{
  [Serializable]
  public struct SpriteFontCharacter
  {
    [SerializeField]
    private char m_char;
    [SerializeField]
    private Sprite m_sprite;
    [SerializeField]
    private float m_leftShift;
    [SerializeField]
    private float m_rightShift;
    private Vector3[] m_vertices;
    private int[] m_triangles;
    private Vector2[] m_uvs;
    private Vector4 m_rect;

    public char character
    {
      [Pure] get => this.m_char;
    }

    public Texture2D texture
    {
      [Pure] get => this.m_sprite.texture;
    }

    public Texture2D associatedAlphaSplitTexture
    {
      [Pure] get => this.m_sprite.associatedAlphaSplitTexture;
    }

    public float pixelsPerUnit
    {
      [Pure] get => this.m_sprite.pixelsPerUnit;
    }

    public int vertexCount
    {
      [Pure] get => this.m_vertices.Length;
    }

    public int triangleCount
    {
      [Pure] get => this.m_triangles.Length;
    }

    public float left
    {
      [Pure] get => this.m_rect.x;
    }

    public float right
    {
      [Pure] get => this.m_rect.y;
    }

    public float width
    {
      [Pure] get => this.m_rect.z;
    }

    public float height
    {
      [Pure] get => this.m_rect.w;
    }

    public float leftShift
    {
      [Pure] get => this.m_leftShift;
    }

    public float rightShift
    {
      [Pure] get => this.m_rightShift;
    }

    public bool Load()
    {
      if ((Object) null == (Object) this.m_sprite)
        return false;
      float a1 = 0.0f;
      float a2 = 0.0f;
      Vector2[] vertices = this.m_sprite.vertices;
      ushort[] triangles = this.m_sprite.triangles;
      int length1 = vertices.Length;
      int length2 = triangles.Length;
      this.m_vertices = new Vector3[length1];
      this.m_triangles = new int[length2];
      for (int index = 0; index < length1; ++index)
      {
        Vector2 vector2 = vertices[index];
        a1 = Mathf.Min(a1, vector2.x);
        a2 = Mathf.Max(a2, vector2.x);
        this.m_vertices[index].Set(vector2.x, vector2.y, 0.0f);
      }
      for (int index = 0; index < length2; ++index)
        this.m_triangles[index] = (int) triangles[index];
      this.m_uvs = this.m_sprite.uv;
      float num1 = a1 + this.m_leftShift / this.pixelsPerUnit;
      float num2 = a2 + this.m_rightShift / this.pixelsPerUnit;
      this.m_rect.x = num1;
      this.m_rect.y = num2;
      this.m_rect.z = num2 - num1;
      this.m_rect.w = this.m_sprite.rect.height;
      return true;
    }

    public void Unload()
    {
      this.m_vertices = (Vector3[]) null;
      this.m_triangles = (int[]) null;
      this.m_uvs = (Vector2[]) null;
    }

    [Pure]
    public float Fill(
      List<Vector3> vertices,
      List<int> triangles,
      List<Vector2> uvs,
      float advance)
    {
      float x = this.m_rect.x;
      int count = vertices.Count;
      int length1 = this.m_vertices.Length;
      for (int index = 0; index < length1; ++index)
      {
        Vector3 vertex = this.m_vertices[index];
        vertex.x += advance - x;
        vertices.Add(vertex);
        Vector2 uv = this.m_uvs[index];
        uvs.Add(uv);
      }
      int length2 = this.m_triangles.Length;
      for (int index = 0; index < length2; ++index)
        triangles.Add(count + this.m_triangles[index]);
      return advance + this.m_rect.z;
    }

    [Pure]
    public float FillUI(
      List<Vector3> vertices,
      List<int> triangles,
      List<Vector2> uvs,
      float advance)
    {
      float pixelsPerUnit = this.pixelsPerUnit;
      float num = this.m_rect.x * pixelsPerUnit;
      int count = vertices.Count;
      int length1 = this.m_vertices.Length;
      for (int index = 0; index < length1; ++index)
      {
        Vector3 vector3 = this.m_vertices[index] * pixelsPerUnit;
        vector3.x += advance - num;
        vertices.Add(vector3);
        Vector2 uv = this.m_uvs[index];
        uvs.Add(uv);
      }
      int length2 = this.m_triangles.Length;
      for (int index = 0; index < length2; ++index)
        triangles.Add(count + this.m_triangles[index]);
      return advance + this.m_rect.z * pixelsPerUnit;
    }
  }
}
