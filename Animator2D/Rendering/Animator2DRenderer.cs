// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Rendering.Animator2DRenderer
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Ankama.Animations.Rendering
{
  internal sealed class Animator2DRenderer : IAnimator2DRenderer
  {
    private static readonly List<Vector3> s_vertexBuffer = new List<Vector3>();
    private static readonly List<Vector2> s_uvBuffer = new List<Vector2>();
    private static readonly List<Vector3> s_colorBuffer = new List<Vector3>();
    private static readonly List<int> s_triangleBuffer = new List<int>();
    private SpinLock m_spinLock = new SpinLock(false);
    private readonly MeshRenderer m_meshRenderer;
    private readonly MeshFilter m_meshFilter;
    private readonly Mesh m_mesh;
    private Material m_material;
    private readonly RenderState[] m_renderStates;
    private readonly RenderState[] m_bufferedRenderStates;
    private volatile int m_renderStateFrame = -1;
    private readonly List<Vector3> m_asyncVertexBuffer = new List<Vector3>();
    private readonly List<Vector2> m_asyncUvBuffer = new List<Vector2>();
    private readonly List<Vector3> m_asyncColorBuffer = new List<Vector3>();
    private readonly List<int> m_asyncTriangleBuffer = new List<int>();
    private volatile BufferState m_bufferState;
    private volatile int m_vertexBufferCapacityHint;
    private volatile int m_triangleBufferCapacityHint;

    public Animator2DRenderer(
      MeshRenderer meshRenderer,
      MeshFilter meshFilter,
      Material material,
      int maxNodeCount)
    {
      this.m_meshRenderer = meshRenderer;
      this.m_meshFilter = meshFilter;
      this.m_material = material;
      this.m_renderStates = new RenderState[maxNodeCount];
      this.m_bufferedRenderStates = new RenderState[maxNodeCount];
      Animator2DRendererUtility.CreateMesh(meshFilter, out this.m_mesh);
      meshRenderer.sharedMaterials = new Material[1]
      {
        material
      };
    }

    public void Start(AnimationInstance animation)
    {
      int nodeCount = (int) animation.nodeCount;
      if ((animation.combinedNodeState & (Ankama.Animations.Animation.NodeState.SpriteColorMultiply | Ankama.Animations.Animation.NodeState.SpriteColorAdditive)) == Ankama.Animations.Animation.NodeState.None)
      {
        this.m_material.DisableKeyword("USE_COLOR_EFFECTS");
        this.m_material.EnableKeyword("SKIP_COLOR_EFFECTS");
      }
      else
      {
        this.m_material.EnableKeyword("USE_COLOR_EFFECTS");
        this.m_material.DisableKeyword("SKIP_COLOR_EFFECTS");
      }
      bool lockTaken = false;
      try
      {
        this.m_spinLock.Enter(ref lockTaken);
        this.m_bufferState = BufferState.Ready;
        this.m_renderStateFrame = -1;
        RenderState[] renderStates = this.m_renderStates;
        RenderState[] bufferedRenderStates = this.m_bufferedRenderStates;
        for (int index = 0; index < nodeCount; ++index)
        {
          renderStates[index].Reset();
          bufferedRenderStates[index].Reset();
        }
      }
      finally
      {
        if (lockTaken)
          this.m_spinLock.Exit(false);
      }
    }

    public void Release() => Animator2DRendererUtility.DestroyMesh(this.m_meshFilter, this.m_mesh);

    public unsafe void Compute(Graphic[] graphics, AnimationInstance animation, int frame)
    {
      bool lockTaken = false;
      try
      {
        this.m_spinLock.Enter(ref lockTaken);
        int num1 = this.m_renderStateFrame;
        if (this.m_bufferState == BufferState.Pending && num1 == frame)
        {
          Mesh mesh = this.m_mesh;
          mesh.Clear();
          mesh.SetVertices(this.m_asyncVertexBuffer);
          mesh.SetUVs(0, this.m_asyncUvBuffer);
          mesh.SetUVs(1, this.m_asyncColorBuffer);
          mesh.SetTriangles(this.m_asyncTriangleBuffer, 0, false);
          mesh.UploadMeshData(false);
          this.m_bufferState = BufferState.Ready;
          return;
        }
        this.m_bufferState = BufferState.Ready;
        if (num1 == frame)
          return;
        RenderState[] renderStates = this.m_renderStates;
        int nodeCount = (int) animation.nodeCount;
        if (frame < num1)
        {
          num1 = -1;
          for (int index = 0; index < nodeCount; ++index)
            renderStates[index].Reset();
        }
        byte[] bytes = animation.bytes;
        int frameDataPosition;
        // ISSUE: variable of a reference type
        byte& local;
        if (num1 == frame - 1)
        {
          frameDataPosition = animation.frameDataPositions[frame];
        }
        else
        {
          frameDataPosition = animation.frameDataPositions[num1 + 1];
          for (; num1 < frame - 1; ++num1)
          {
            for (int index1 = nodeCount - 1; index1 >= 0; --index1)
            {
              short index2;
              try
              {
                local = ref bytes[frameDataPosition];
                index2 = *(short*) ref local;
              }
              finally
              {
                // ISSUE: cast to a reference type
                local = (byte&) IntPtr.Zero;
              }
              int dataPosition = frameDataPosition + 2;
              frameDataPosition = renderStates[(int) index2].Compute(bytes, dataPosition);
            }
          }
        }
        int num2 = 0;
        for (int index3 = nodeCount - 1; index3 >= 0; --index3)
        {
          short index4;
          try
          {
            local = ref bytes[frameDataPosition];
            index4 = *(short*) ref local;
          }
          finally
          {
            // ISSUE: cast to a reference type
            local = (byte&) IntPtr.Zero;
          }
          int dataPosition = frameDataPosition + 2;
          frameDataPosition = renderStates[(int) index4].Compute(bytes, dataPosition);
          RenderState renderState = renderStates[(int) index4];
          if (renderState.spriteIndex >= (short) 0)
          {
            Graphic graphic = graphics[(int) renderState.spriteIndex];
            if (graphic != null)
            {
              float m00 = renderState.m00;
              float m01 = renderState.m01;
              float m03 = renderState.m03;
              float m10 = renderState.m10;
              float m11 = renderState.m11;
              float m13 = renderState.m13;
              List<Vector3> vertexBuffer = Animator2DRenderer.s_vertexBuffer;
              List<Vector3> colorBuffer = Animator2DRenderer.s_colorBuffer;
              List<Vector2> uvBuffer = Animator2DRenderer.s_uvBuffer;
              List<int> triangleBuffer = Animator2DRenderer.s_triangleBuffer;
              Vector3 vector3_1 = new Vector3((float) renderState.alpha / (float) byte.MaxValue, renderState.multiplicativeColor, renderState.additiveColor);
              Vector2[] vertices = graphic.vertices;
              int length1 = vertices.Length;
              for (int index5 = 0; index5 < length1; ++index5)
              {
                Vector2 vector2 = vertices[index5];
                Vector3 vector3_2;
                vector3_2.x = (float) ((double) m00 * (double) vector2.x + (double) m01 * (double) vector2.y) + m03;
                vector3_2.y = (float) ((double) m10 * (double) vector2.x + (double) m11 * (double) vector2.y) + m13;
                vector3_2.z = 0.0f;
                vertexBuffer.Add(vector3_2);
                colorBuffer.Add(vector3_1);
              }
              Vector2[] uvs = graphic.uvs;
              int length2 = uvs.Length;
              for (int index6 = 0; index6 < length2; ++index6)
                uvBuffer.Add(uvs[index6]);
              int[] triangles = graphic.triangles;
              int length3 = triangles.Length;
              for (int index7 = 0; index7 < length3; ++index7)
              {
                int num3 = triangles[index7];
                triangleBuffer.Add(num3 + num2);
              }
              num2 += vertices.Length;
            }
          }
        }
        this.m_renderStateFrame = frame;
      }
      finally
      {
        this.m_spinLock.Exit(false);
      }
      Mesh mesh1 = this.m_mesh;
      this.m_vertexBufferCapacityHint = Animator2DRenderer.s_vertexBuffer.Capacity;
      this.m_triangleBufferCapacityHint = Animator2DRenderer.s_triangleBuffer.Capacity;
      mesh1.Clear();
      mesh1.SetVertices(Animator2DRenderer.s_vertexBuffer);
      Animator2DRenderer.s_vertexBuffer.Clear();
      mesh1.SetUVs(0, Animator2DRenderer.s_uvBuffer);
      Animator2DRenderer.s_uvBuffer.Clear();
      mesh1.SetUVs(1, Animator2DRenderer.s_colorBuffer);
      Animator2DRenderer.s_colorBuffer.Clear();
      mesh1.SetTriangles(Animator2DRenderer.s_triangleBuffer, 0, false);
      Animator2DRenderer.s_triangleBuffer.Clear();
      mesh1.UploadMeshData(false);
    }

    public unsafe void Buffer(Graphic[] graphics, AnimationInstance animation)
    {
      int nodeCount = (int) animation.nodeCount;
      bool lockTaken1 = false;
      int index1;
      int bufferCapacityHint1;
      int bufferCapacityHint2;
      try
      {
        this.m_spinLock.TryEnter(ref lockTaken1);
        if (!lockTaken1 || this.m_bufferState != BufferState.Ready)
          return;
        index1 = this.m_renderStateFrame + 1;
        if (index1 >= (int) animation.frameCount)
        {
          index1 -= (int) animation.frameCount;
          RenderState[] bufferedRenderStates = this.m_bufferedRenderStates;
          for (int index2 = 0; index2 < nodeCount; ++index2)
            bufferedRenderStates[index2].Reset();
        }
        else
          Array.Copy((Array) this.m_renderStates, (Array) this.m_bufferedRenderStates, nodeCount);
        bufferCapacityHint1 = this.m_vertexBufferCapacityHint;
        bufferCapacityHint2 = this.m_triangleBufferCapacityHint;
        this.m_bufferState = BufferState.Running;
      }
      finally
      {
        if (lockTaken1)
          this.m_spinLock.Exit(false);
      }
      List<Vector3> asyncVertexBuffer = this.m_asyncVertexBuffer;
      List<Vector3> asyncColorBuffer = this.m_asyncColorBuffer;
      List<Vector2> asyncUvBuffer = this.m_asyncUvBuffer;
      List<int> asyncTriangleBuffer = this.m_asyncTriangleBuffer;
      asyncVertexBuffer.Clear();
      asyncColorBuffer.Clear();
      asyncUvBuffer.Clear();
      asyncTriangleBuffer.Clear();
      if (bufferCapacityHint1 > asyncVertexBuffer.Capacity)
      {
        asyncVertexBuffer.Capacity = bufferCapacityHint1;
        asyncColorBuffer.Capacity = bufferCapacityHint1;
        asyncUvBuffer.Capacity = bufferCapacityHint1;
      }
      if (bufferCapacityHint2 > asyncTriangleBuffer.Capacity)
        asyncTriangleBuffer.Capacity = bufferCapacityHint2;
      RenderState[] bufferedRenderStates1 = this.m_bufferedRenderStates;
      byte[] bytes = animation.bytes;
      int frameDataPosition = animation.frameDataPositions[index1];
      int num1 = 0;
      for (int index3 = nodeCount - 1; index3 >= 0; --index3)
      {
        if (this.m_bufferState != BufferState.Running)
          return;
        short index4;
        fixed (byte* numPtr = &bytes[frameDataPosition])
          index4 = *(short*) numPtr;
        int dataPosition = frameDataPosition + 2;
        frameDataPosition = bufferedRenderStates1[(int) index4].Compute(bytes, dataPosition);
        RenderState renderState = bufferedRenderStates1[(int) index4];
        if (renderState.spriteIndex >= (short) 0)
        {
          Graphic graphic = graphics[(int) renderState.spriteIndex];
          if (graphic != null)
          {
            float m00 = renderState.m00;
            float m01 = renderState.m01;
            float m03 = renderState.m03;
            float m10 = renderState.m10;
            float m11 = renderState.m11;
            float m13 = renderState.m13;
            Vector3 vector3_1 = new Vector3((float) renderState.alpha / (float) byte.MaxValue, renderState.multiplicativeColor, renderState.additiveColor);
            Vector2[] vertices = graphic.vertices;
            int length1 = vertices.Length;
            for (int index5 = 0; index5 < length1; ++index5)
            {
              Vector2 vector2 = vertices[index5];
              Vector3 vector3_2;
              vector3_2.x = (float) ((double) m00 * (double) vector2.x + (double) m01 * (double) vector2.y) + m03;
              vector3_2.y = (float) ((double) m10 * (double) vector2.x + (double) m11 * (double) vector2.y) + m13;
              vector3_2.z = 0.0f;
              asyncVertexBuffer.Add(vector3_2);
              asyncColorBuffer.Add(vector3_1);
            }
            Vector2[] uvs = graphic.uvs;
            int length2 = uvs.Length;
            for (int index6 = 0; index6 < length2; ++index6)
              asyncUvBuffer.Add(uvs[index6]);
            int[] triangles = graphic.triangles;
            int length3 = triangles.Length;
            for (int index7 = 0; index7 < length3; ++index7)
            {
              int num2 = triangles[index7];
              asyncTriangleBuffer.Add(num2 + num1);
            }
            num1 += vertices.Length;
          }
        }
      }
      bool lockTaken2 = false;
      try
      {
        this.m_spinLock.Enter(ref lockTaken2);
        if (this.m_bufferState != BufferState.Running)
          return;
        Array.Copy((Array) this.m_bufferedRenderStates, (Array) this.m_renderStates, nodeCount);
        this.m_renderStateFrame = index1;
        this.m_bufferState = BufferState.Pending;
      }
      finally
      {
        if (lockTaken2)
          this.m_spinLock.Exit(false);
      }
    }

    public void SetMaterial(Material material, Color color)
    {
      Material material1 = new Material(material)
      {
        color = color,
        mainTexture = this.m_material.mainTexture
      };
      if (this.m_material.IsKeywordEnabled("USE_COLOR_EFFECTS"))
      {
        material1.EnableKeyword("USE_COLOR_EFFECTS");
        material1.DisableKeyword("SKIP_COLOR_EFFECTS");
      }
      else
      {
        material1.DisableKeyword("USE_COLOR_EFFECTS");
        material1.EnableKeyword("SKIP_COLOR_EFFECTS");
      }
      this.m_material = material1;
      this.m_meshRenderer.sharedMaterials = new Material[1]
      {
        material1
      };
    }

    public void SetColor(Color value)
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_material))
        return;
      this.m_material.color = value;
    }

    public void EnableKeyword(string keyword) => this.m_material.EnableKeyword(keyword);

    public void DisableKeyword(string keyword) => this.m_material.DisableKeyword(keyword);

    public bool IsKeywordEnabled(string keyword) => this.m_material.IsKeywordEnabled(keyword);
  }
}
