// Decompiled with JetBrains decompiler
// Type: UnityEngine.SpriteFont
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;

namespace UnityEngine
{
  [CreateAssetMenu]
  public class SpriteFont : ScriptableObject
  {
    [SerializeField]
    protected float m_shiftScale = 1f;
    [SerializeField]
    protected SpriteFontCharacter[] m_characters;
    private int m_referenceCount;
    protected List<Vector3> m_vertexPool = new List<Vector3>(64);
    protected List<int> m_trianglePool = new List<int>(64);
    protected List<Vector2> m_uvPool = new List<Vector2>(64);
    protected List<Color32> m_colorPool = new List<Color32>(64);
    protected List<Vector4> m_tintPool = new List<Vector4>(64);
    protected List<SpriteFontCharacter> m_textCharacters = new List<SpriteFontCharacter>(1);

    public bool isValid { get; protected set; }

    public bool isLoaded { get; private set; }

    public Texture2D texture { get; private set; }

    public Texture2D associatedAlphaSplitTexture { get; private set; }

    public float height { get; private set; }

    public float pixelsPerUnit { get; private set; } = 100f;

    public float lastComputedWidth { get; private set; }

    public bool Load()
    {
      if (!this.isLoaded)
      {
        if (this.InternalLoad())
          ++this.m_referenceCount;
      }
      else if (this.isValid)
        ++this.m_referenceCount;
      return this.isValid;
    }

    private bool InternalLoad()
    {
      int length = this.m_characters.Length;
      if (length == 0)
      {
        this.isValid = false;
        return false;
      }
      for (int index = 0; index < length; ++index)
      {
        if (!this.m_characters[index].Load())
        {
          Debug.LogWarning((object) string.Format("No sprite for character '{0}' (index={1})", (object) this.m_characters[index].character, (object) index));
          this.isValid = false;
          return false;
        }
      }
      SpriteFontCharacter character = this.m_characters[0];
      this.texture = character.texture;
      this.associatedAlphaSplitTexture = character.associatedAlphaSplitTexture;
      this.height = character.height;
      this.pixelsPerUnit = character.pixelsPerUnit;
      this.isValid = (Object) null != (Object) this.texture;
      this.isLoaded = true;
      return true;
    }

    public void Unload()
    {
      if (!this.isLoaded || !this.isValid)
        return;
      --this.m_referenceCount;
      if (this.m_referenceCount != 0)
        return;
      int length = this.m_characters.Length;
      for (int index = 0; index < length; ++index)
        this.m_characters[index].Unload();
      this.texture = (Texture2D) null;
      this.associatedAlphaSplitTexture = (Texture2D) null;
      this.height = 0.0f;
      this.pixelsPerUnit = 100f;
      this.isLoaded = false;
    }

    public Mesh BuildMesh(string text, Color32 color, Vector4 tint, SpriteFontAlignment alignment)
    {
      int num1 = 0;
      int num2 = 0;
      float num3 = 0.0f;
      int length1 = text.Length;
      int length2 = this.m_characters.Length;
      this.m_textCharacters.Clear();
      if (this.m_textCharacters.Capacity < length1)
        this.m_textCharacters.Capacity = length1;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        char ch = text[index1];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          SpriteFontCharacter character = this.m_characters[index2];
          if ((int) ch == (int) character.character)
          {
            this.m_textCharacters.Add(character);
            num1 += character.vertexCount;
            num2 += character.triangleCount;
            num3 += character.width;
            break;
          }
        }
      }
      this.lastComputedWidth = num3;
      int count = this.m_textCharacters.Count;
      if (count <= 0)
        return (Mesh) null;
      this.m_vertexPool.Clear();
      if (this.m_vertexPool.Capacity < num1)
        this.m_vertexPool.Capacity = num1;
      this.m_trianglePool.Clear();
      if (this.m_trianglePool.Capacity < num2)
        this.m_trianglePool.Capacity = num2;
      this.m_uvPool.Clear();
      if (this.m_uvPool.Capacity < num1)
        this.m_uvPool.Capacity = num1;
      this.m_colorPool.Clear();
      if (this.m_colorPool.Capacity < num1)
        this.m_colorPool.Capacity = num1;
      this.m_tintPool.Clear();
      if (this.m_tintPool.Capacity < num1)
        this.m_tintPool.Capacity = num1;
      SpriteFontCharacter textCharacter;
      float advance;
      switch (alignment)
      {
        case SpriteFontAlignment.Left:
          textCharacter = this.m_textCharacters[0];
          advance = textCharacter.leftShift / this.pixelsPerUnit;
          break;
        case SpriteFontAlignment.Center:
          textCharacter = this.m_textCharacters[0];
          float left = textCharacter.left;
          textCharacter = this.m_textCharacters[count - 1];
          float right = textCharacter.right;
          advance = (float) (-0.5 * ((double) num3 - (double) left - (double) right));
          break;
        case SpriteFontAlignment.Right:
          advance = (float) (-(double) num3 + (double) this.m_textCharacters[count - 1].rightShift / (double) this.pixelsPerUnit);
          break;
        case SpriteFontAlignment.LeftCharacter:
          textCharacter = this.m_textCharacters[0];
          advance = textCharacter.left;
          break;
        case SpriteFontAlignment.RightCharacter:
          advance = -num3 + this.m_textCharacters[count - 1].right;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (alignment), (object) alignment, (string) null);
      }
      for (int index = 0; index < count; ++index)
      {
        textCharacter = this.m_textCharacters[index];
        advance = textCharacter.Fill(this.m_vertexPool, this.m_trianglePool, this.m_uvPool, advance);
      }
      for (int index = 0; index < num1; ++index)
      {
        this.m_colorPool.Add(color);
        this.m_tintPool.Add(tint);
      }
      Mesh mesh = new Mesh();
      mesh.MarkDynamic();
      mesh.SetVertices(this.m_vertexPool);
      mesh.SetTriangles(this.m_trianglePool, 0);
      mesh.SetUVs(0, this.m_uvPool);
      mesh.SetColors(this.m_colorPool);
      mesh.SetTangents(this.m_tintPool);
      mesh.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
      return mesh;
    }

    public void BuildUIMesh(Mesh mesh, string text, Color32 color, SpriteFontAlignment alignment)
    {
      int num1 = 0;
      int num2 = 0;
      float num3 = 0.0f;
      int length1 = text.Length;
      int length2 = this.m_characters.Length;
      this.m_textCharacters.Clear();
      if (this.m_textCharacters.Capacity < length1)
        this.m_textCharacters.Capacity = length1;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        char ch = text[index1];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          SpriteFontCharacter character = this.m_characters[index2];
          if ((int) ch == (int) character.character)
          {
            this.m_textCharacters.Add(character);
            num1 += character.vertexCount;
            num2 += character.triangleCount;
            num3 += character.width;
            break;
          }
        }
      }
      float num4 = num3 * this.pixelsPerUnit;
      this.lastComputedWidth = num4;
      int count = this.m_textCharacters.Count;
      if (count > 0)
      {
        this.m_vertexPool.Clear();
        if (this.m_vertexPool.Capacity < num1)
          this.m_vertexPool.Capacity = num1;
        this.m_trianglePool.Clear();
        if (this.m_trianglePool.Capacity < num2)
          this.m_trianglePool.Capacity = num2;
        this.m_uvPool.Clear();
        if (this.m_uvPool.Capacity < num1)
          this.m_uvPool.Capacity = num1;
        this.m_colorPool.Clear();
        if (this.m_colorPool.Capacity < num1)
          this.m_colorPool.Capacity = num1;
        SpriteFontCharacter textCharacter;
        float advance;
        switch (alignment)
        {
          case SpriteFontAlignment.Left:
            textCharacter = this.m_textCharacters[0];
            advance = textCharacter.leftShift;
            break;
          case SpriteFontAlignment.Center:
            textCharacter = this.m_textCharacters[0];
            float num5 = textCharacter.left * this.pixelsPerUnit;
            textCharacter = this.m_textCharacters[count - 1];
            float num6 = textCharacter.right * this.pixelsPerUnit;
            advance = (float) (-0.5 * ((double) num4 - (double) num5 - (double) num6));
            break;
          case SpriteFontAlignment.Right:
            advance = -num4 + this.m_textCharacters[count - 1].rightShift;
            break;
          case SpriteFontAlignment.LeftCharacter:
            textCharacter = this.m_textCharacters[0];
            advance = textCharacter.left * this.pixelsPerUnit;
            break;
          case SpriteFontAlignment.RightCharacter:
            advance = (float) (-(double) num4 + (double) this.m_textCharacters[count - 1].right * (double) this.pixelsPerUnit);
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (alignment), (object) alignment, (string) null);
        }
        for (int index = 0; index < count; ++index)
        {
          textCharacter = this.m_textCharacters[index];
          advance = textCharacter.FillUI(this.m_vertexPool, this.m_trianglePool, this.m_uvPool, advance);
        }
        for (int index = 0; index < num1; ++index)
          this.m_colorPool.Add(color);
        mesh.Clear();
        mesh.SetVertices(this.m_vertexPool);
        mesh.SetTriangles(this.m_trianglePool, 0);
        mesh.SetUVs(0, this.m_uvPool);
        mesh.SetColors(this.m_colorPool);
      }
      else
        mesh.Clear();
    }

    public void ChangeMeshColor(Mesh mesh, Color color, Vector4 tint)
    {
      if ((Object) null == (Object) mesh)
        return;
      this.m_colorPool.Clear();
      this.m_tintPool.Clear();
      int vertexCount = mesh.vertexCount;
      for (int index = 0; index < vertexCount; ++index)
      {
        this.m_colorPool.Add((Color32) color);
        this.m_tintPool.Add(tint);
      }
      mesh.SetColors(this.m_colorPool);
      mesh.SetTangents(this.m_tintPool);
    }
  }
}
