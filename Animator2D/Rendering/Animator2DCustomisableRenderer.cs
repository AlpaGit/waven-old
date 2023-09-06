﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Rendering.Animator2DCustomisableRenderer
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Ankama.Animations.Rendering
{
  internal sealed class Animator2DCustomisableRenderer : IAnimator2DRenderer
  {
    private static readonly List<Vector3> s_vertexBuffer = new List<Vector3>();
    private static readonly List<Vector2> s_uvBuffer = new List<Vector2>();
    private static readonly List<Vector3> s_colorBuffer = new List<Vector3>();
    private static readonly List<List<int>> s_triangleBuffers = new List<List<int>>();
    private static readonly List<int> s_rendererMaterials = new List<int>();
    private SpinLock m_spinLock = new SpinLock(false);
    private readonly Mesh m_mesh;
    private readonly MeshRenderer m_meshRenderer;
    private readonly MeshFilter m_meshFilter;
    private readonly DynamicMaterialArray m_materialArray;
    private readonly Graphic[] m_customisationGraphics;
    private bool m_customisationDirty;
    private readonly CustomisableRenderState[] m_renderStates;
    private readonly CustomisableRenderState[] m_bufferedRenderStates;
    private volatile int m_renderStateFrame = -1;
    private readonly List<Vector3> m_asyncVertexBuffer = new List<Vector3>();
    private readonly List<Vector2> m_asyncUvBuffer = new List<Vector2>();
    private readonly List<Vector3> m_asyncColorBuffer = new List<Vector3>();
    private readonly List<List<int>> m_asyncTriangleBuffers = new List<List<int>>();
    private readonly List<int> m_asyncRendererMaterials = new List<int>();
    private volatile BufferState m_bufferState;
    private volatile int m_vertexBufferCapacityHint;

    public Animator2DCustomisableRenderer(
      MeshRenderer meshRenderer,
      MeshFilter meshFilter,
      Material material,
      int maxNodeCount,
      Graphic[] graphics,
      string[] exposedNodeNames)
    {
      DynamicMaterialArray dynamicMaterialArray = new DynamicMaterialArray(material);
      int length = graphics.Length;
      for (int index = 0; index < length; ++index)
      {
        Graphic graphic = graphics[index];
        Texture2D texture = graphic.atlas;
        if ((UnityEngine.Object) null == (UnityEngine.Object) texture)
          texture = Texture2D.whiteTexture;
        int instanceId = texture.GetInstanceID();
        graphic.textureId = instanceId;
        dynamicMaterialArray.AddTexture(instanceId, texture);
      }
      this.m_customisationGraphics = new Graphic[exposedNodeNames.Length];
      this.m_materialArray = dynamicMaterialArray;
      this.m_meshRenderer = meshRenderer;
      this.m_meshFilter = meshFilter;
      this.m_renderStates = new CustomisableRenderState[maxNodeCount];
      this.m_bufferedRenderStates = new CustomisableRenderState[maxNodeCount];
      Animator2DRendererUtility.CreateMesh(this.m_meshFilter, out this.m_mesh);
    }

    public void SetCustomisation(int index, GraphicAsset graphicAsset)
    {
      this.m_bufferState = BufferState.Ready;
      bool lockTaken = false;
      try
      {
        this.m_spinLock.Enter(ref lockTaken);
        Graphic customisationGraphic = this.m_customisationGraphics[index];
        if (customisationGraphic != null)
        {
          this.m_customisationGraphics[index] = (Graphic) null;
          if ((UnityEngine.Object) null != (UnityEngine.Object) customisationGraphic.atlas)
          {
            bool flag = true;
            int textureId = customisationGraphic.textureId;
            Graphic[] customisationGraphics = this.m_customisationGraphics;
            int length = customisationGraphics.Length;
            for (int index1 = 0; index1 < length; ++index1)
            {
              Graphic graphic = customisationGraphics[index1];
              if (graphic != null && textureId == graphic.textureId)
              {
                flag = false;
                break;
              }
            }
            if (flag)
              this.m_materialArray.RemoveTexture(textureId);
          }
        }
        if ((UnityEngine.Object) null != (UnityEngine.Object) graphicAsset)
        {
          Texture2D texture2D = graphicAsset.atlas;
          if ((UnityEngine.Object) null == (UnityEngine.Object) texture2D)
            texture2D = Texture2D.whiteTexture;
          Graphic graphic = new Graphic(texture2D, graphicAsset.vertices, graphicAsset.uvs, graphicAsset.triangles);
          this.m_materialArray.AddTexture(graphic.textureId, texture2D);
          this.m_customisationGraphics[index] = graphic;
        }
        this.m_customisationDirty = true;
      }
      finally
      {
        if (lockTaken)
          this.m_spinLock.Exit(false);
      }
    }

    public void Start(AnimationInstance animation)
    {
      int nodeCount = (int) animation.nodeCount;
      if ((animation.combinedNodeState & (Ankama.Animations.Animation.NodeState.SpriteColorMultiply | Ankama.Animations.Animation.NodeState.SpriteColorAdditive)) == Ankama.Animations.Animation.NodeState.None)
        this.m_materialArray.UseColorEffects();
      else
        this.m_materialArray.SkipColorEffects();
      this.m_bufferState = BufferState.Ready;
      bool lockTaken = false;
      try
      {
        this.m_spinLock.Enter(ref lockTaken);
        this.m_renderStateFrame = -1;
        CustomisableRenderState[] renderStates = this.m_renderStates;
        CustomisableRenderState[] bufferedRenderStates = this.m_bufferedRenderStates;
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
          int count = this.m_asyncRendererMaterials.Count;
          Mesh mesh = this.m_mesh;
          mesh.Clear();
          mesh.subMeshCount = count;
          mesh.SetVertices(this.m_asyncVertexBuffer);
          mesh.SetUVs(0, this.m_asyncUvBuffer);
          mesh.SetUVs(1, this.m_asyncColorBuffer);
          for (int index = 0; index < count; ++index)
            this.m_mesh.SetTriangles(this.m_asyncTriangleBuffers[index], index, false);
          mesh.UploadMeshData(false);
          this.m_meshRenderer.sharedMaterials = this.m_materialArray.Get(this.m_asyncRendererMaterials);
          this.m_bufferState = BufferState.Ready;
          return;
        }
        this.m_bufferState = BufferState.Ready;
        if (num1 == frame)
        {
          if (!this.m_customisationDirty)
            return;
          this.m_customisationDirty = false;
          this.ReApplyRenderStates(graphics, animation);
        }
        else
        {
          CustomisableRenderState[] renderStates = this.m_renderStates;
          byte[] bytes = animation.bytes;
          int nodeCount = (int) animation.nodeCount;
          if (frame < num1)
          {
            num1 = -1;
            for (int index = 0; index < nodeCount; ++index)
              renderStates[index].Reset();
          }
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
          int num3 = 0;
          int index3 = -1;
          List<int> intList = (List<int>) null;
          int count = Animator2DCustomisableRenderer.s_triangleBuffers.Count;
          for (int index4 = nodeCount - 1; index4 >= 0; --index4)
          {
            short index5;
            try
            {
              local = ref bytes[frameDataPosition];
              index5 = *(short*) ref local;
            }
            finally
            {
              // ISSUE: cast to a reference type
              local = (byte&) IntPtr.Zero;
            }
            int dataPosition = frameDataPosition + 2;
            frameDataPosition = this.m_renderStates[(int) index5].Compute(bytes, dataPosition);
            CustomisableRenderState renderState = this.m_renderStates[(int) index5];
            if (renderState.spriteIndex >= (short) 0)
            {
              Graphic graphic = renderState.customisationIndex < (short) 0 ? graphics[(int) renderState.spriteIndex] : this.m_customisationGraphics[(int) renderState.customisationIndex] ?? graphics[(int) renderState.spriteIndex];
              if (graphic != null)
              {
                int textureId = graphic.textureId;
                if (textureId != num3)
                {
                  ++index3;
                  num3 = textureId;
                  Animator2DCustomisableRenderer.s_rendererMaterials.Add(textureId);
                  if (count == index3)
                  {
                    intList = new List<int>();
                    Animator2DCustomisableRenderer.s_triangleBuffers.Add(intList);
                    ++count;
                  }
                  else
                    intList = Animator2DCustomisableRenderer.s_triangleBuffers[index3];
                }
                float m00 = renderState.m00;
                float m01 = renderState.m01;
                float m03 = renderState.m03;
                float m10 = renderState.m10;
                float m11 = renderState.m11;
                float m13 = renderState.m13;
                Vector3 vector3_1 = new Vector3((float) renderState.alpha / (float) byte.MaxValue, renderState.multiplicativeColor, renderState.additiveColor);
                List<Vector3> vertexBuffer = Animator2DCustomisableRenderer.s_vertexBuffer;
                List<Vector3> colorBuffer = Animator2DCustomisableRenderer.s_colorBuffer;
                List<Vector2> uvBuffer = Animator2DCustomisableRenderer.s_uvBuffer;
                Vector2[] vertices = graphic.vertices;
                int length1 = vertices.Length;
                for (int index6 = 0; index6 < length1; ++index6)
                {
                  Vector2 vector2 = vertices[index6];
                  Vector3 vector3_2;
                  vector3_2.x = (float) ((double) m00 * (double) vector2.x + (double) m01 * (double) vector2.y) + m03;
                  vector3_2.y = (float) ((double) m10 * (double) vector2.x + (double) m11 * (double) vector2.y) + m13;
                  vector3_2.z = 0.0f;
                  vertexBuffer.Add(vector3_2);
                  colorBuffer.Add(vector3_1);
                }
                Vector2[] uvs = graphic.uvs;
                int length2 = uvs.Length;
                for (int index7 = 0; index7 < length2; ++index7)
                  uvBuffer.Add(uvs[index7]);
                int[] triangles = graphic.triangles;
                int length3 = triangles.Length;
                for (int index8 = 0; index8 < length3; ++index8)
                {
                  int num4 = triangles[index8];
                  intList.Add(num4 + num2);
                }
                num2 += vertices.Length;
              }
            }
          }
          this.m_renderStateFrame = frame;
          this.m_customisationDirty = false;
        }
      }
      finally
      {
        this.m_spinLock.Exit(false);
      }
      Mesh mesh1 = this.m_mesh;
      int count1 = Animator2DCustomisableRenderer.s_rendererMaterials.Count;
      this.m_vertexBufferCapacityHint = Animator2DCustomisableRenderer.s_vertexBuffer.Capacity;
      mesh1.Clear();
      mesh1.subMeshCount = count1;
      mesh1.SetVertices(Animator2DCustomisableRenderer.s_vertexBuffer);
      Animator2DCustomisableRenderer.s_vertexBuffer.Clear();
      mesh1.SetUVs(0, Animator2DCustomisableRenderer.s_uvBuffer);
      Animator2DCustomisableRenderer.s_uvBuffer.Clear();
      mesh1.SetUVs(1, Animator2DCustomisableRenderer.s_colorBuffer);
      Animator2DCustomisableRenderer.s_colorBuffer.Clear();
      for (int index = 0; index < count1; ++index)
      {
        List<int> triangleBuffer = Animator2DCustomisableRenderer.s_triangleBuffers[index];
        mesh1.SetTriangles(triangleBuffer, index, false);
        triangleBuffer.Clear();
      }
      mesh1.UploadMeshData(false);
      this.m_meshRenderer.sharedMaterials = this.m_materialArray.Get(Animator2DCustomisableRenderer.s_rendererMaterials);
      Animator2DCustomisableRenderer.s_rendererMaterials.Clear();
    }

    public unsafe void Buffer(Graphic[] graphics, AnimationInstance animation)
    {
      int nodeCount = (int) animation.nodeCount;
      bool lockTaken1 = false;
      int index1;
      int bufferCapacityHint;
      try
      {
        this.m_spinLock.TryEnter(ref lockTaken1);
        if (!lockTaken1 || this.m_bufferState != BufferState.Ready)
          return;
        index1 = this.m_renderStateFrame + 1;
        if (index1 >= (int) animation.frameCount)
        {
          CustomisableRenderState[] bufferedRenderStates = this.m_bufferedRenderStates;
          for (int index2 = 0; index2 < nodeCount; ++index2)
            bufferedRenderStates[index2].Reset();
        }
        else
          Array.Copy((Array) this.m_renderStates, (Array) this.m_bufferedRenderStates, nodeCount);
        bufferCapacityHint = this.m_vertexBufferCapacityHint;
        this.m_bufferState = BufferState.Running;
      }
      finally
      {
        if (lockTaken1)
          this.m_spinLock.Exit(false);
      }
      List<int> rendererMaterials = this.m_asyncRendererMaterials;
      List<Vector3> asyncVertexBuffer = this.m_asyncVertexBuffer;
      List<Vector3> asyncColorBuffer = this.m_asyncColorBuffer;
      List<Vector2> asyncUvBuffer = this.m_asyncUvBuffer;
      List<List<int>> asyncTriangleBuffers = this.m_asyncTriangleBuffers;
      rendererMaterials.Clear();
      asyncVertexBuffer.Clear();
      asyncColorBuffer.Clear();
      asyncUvBuffer.Clear();
      int count = asyncTriangleBuffers.Count;
      for (int index3 = 0; index3 < count; ++index3)
        asyncTriangleBuffers[index3].Clear();
      if (bufferCapacityHint > asyncVertexBuffer.Capacity)
      {
        asyncVertexBuffer.Capacity = bufferCapacityHint;
        asyncColorBuffer.Capacity = bufferCapacityHint;
        asyncUvBuffer.Capacity = bufferCapacityHint;
      }
      CustomisableRenderState[] bufferedRenderStates1 = this.m_bufferedRenderStates;
      byte[] bytes = animation.bytes;
      int frameDataPosition = animation.frameDataPositions[index1];
      int num1 = 0;
      int num2 = 0;
      int index4 = -1;
      List<int> intList = (List<int>) null;
      for (int index5 = nodeCount - 1; index5 >= 0; --index5)
      {
        if (this.m_bufferState != BufferState.Running)
          return;
        short index6;
        fixed (byte* numPtr = &bytes[frameDataPosition])
          index6 = *(short*) numPtr;
        int dataPosition = frameDataPosition + 2;
        frameDataPosition = bufferedRenderStates1[(int) index6].Compute(bytes, dataPosition);
        CustomisableRenderState customisableRenderState = bufferedRenderStates1[(int) index6];
        if (customisableRenderState.spriteIndex >= (short) 0)
        {
          Graphic graphic = customisableRenderState.customisationIndex < (short) 0 ? graphics[(int) customisableRenderState.spriteIndex] : this.m_customisationGraphics[(int) customisableRenderState.customisationIndex] ?? graphics[(int) customisableRenderState.spriteIndex];
          if (graphic != null)
          {
            int textureId = graphic.textureId;
            if (textureId != num2)
            {
              ++index4;
              num2 = textureId;
              this.m_asyncRendererMaterials.Add(textureId);
              if (count == index4)
              {
                intList = new List<int>();
                this.m_asyncTriangleBuffers.Add(intList);
                ++count;
              }
              else
                intList = this.m_asyncTriangleBuffers[index4];
            }
            float m00 = customisableRenderState.m00;
            float m01 = customisableRenderState.m01;
            float m03 = customisableRenderState.m03;
            float m10 = customisableRenderState.m10;
            float m11 = customisableRenderState.m11;
            float m13 = customisableRenderState.m13;
            Vector3 vector3_1 = new Vector3((float) customisableRenderState.alpha / (float) byte.MaxValue, customisableRenderState.multiplicativeColor, customisableRenderState.additiveColor);
            Vector2[] vertices = graphic.vertices;
            int length1 = vertices.Length;
            for (int index7 = 0; index7 < length1; ++index7)
            {
              Vector2 vector2 = vertices[index7];
              Vector3 vector3_2;
              vector3_2.x = (float) ((double) m00 * (double) vector2.x + (double) m01 * (double) vector2.y) + m03;
              vector3_2.y = (float) ((double) m10 * (double) vector2.x + (double) m11 * (double) vector2.y) + m13;
              vector3_2.z = 0.0f;
              asyncVertexBuffer.Add(vector3_2);
              asyncColorBuffer.Add(vector3_1);
            }
            Vector2[] uvs = graphic.uvs;
            int length2 = uvs.Length;
            for (int index8 = 0; index8 < length2; ++index8)
              asyncUvBuffer.Add(uvs[index8]);
            int[] triangles = graphic.triangles;
            int length3 = triangles.Length;
            for (int index9 = 0; index9 < length3; ++index9)
            {
              int num3 = triangles[index9];
              intList.Add(num3 + num1);
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
      this.m_materialArray.SetMaterial(material, color);
      Material[] sharedMaterials = this.m_meshRenderer.sharedMaterials;
      List<int> rendererMaterials = Animator2DCustomisableRenderer.s_rendererMaterials;
      int length = sharedMaterials.Length;
      for (int index = 0; index < length; ++index)
      {
        int instanceId = sharedMaterials[index].mainTexture.GetInstanceID();
        rendererMaterials.Add(instanceId);
      }
      this.m_meshRenderer.sharedMaterials = this.m_materialArray.Get(rendererMaterials);
      rendererMaterials.Clear();
    }

    public void SetColor(Color value) => this.m_materialArray.SetColor(value);

    private unsafe void ReApplyRenderStates(Graphic[] graphics, AnimationInstance animation)
    {
      Graphic[] customisationGraphics = this.m_customisationGraphics;
      CustomisableRenderState[] renderStates = this.m_renderStates;
      byte[] bytes = animation.bytes;
      int nodeCount = (int) animation.nodeCount;
      int index1 = animation.frameDataPositions[this.m_renderStateFrame];
      int num1 = 0;
      int num2 = 0;
      int index2 = -1;
      List<int> intList = (List<int>) null;
      int count = Animator2DCustomisableRenderer.s_triangleBuffers.Count;
      for (int index3 = nodeCount - 1; index3 >= 0; --index3)
      {
        short index4;
        fixed (byte* numPtr = &bytes[index1])
          index4 = *(short*) numPtr;
        int dataPosition = index1 + 2;
        index1 = CustomisableRenderState.Advance(bytes, dataPosition);
        CustomisableRenderState customisableRenderState = renderStates[(int) index4];
        if (customisableRenderState.spriteIndex >= (short) 0)
        {
          Graphic graphic = customisableRenderState.customisationIndex < (short) 0 ? graphics[(int) customisableRenderState.spriteIndex] : customisationGraphics[(int) customisableRenderState.customisationIndex] ?? graphics[(int) customisableRenderState.spriteIndex];
          if (graphic != null)
          {
            int textureId = graphic.textureId;
            if (textureId != num2)
            {
              ++index2;
              num2 = textureId;
              Animator2DCustomisableRenderer.s_rendererMaterials.Add(textureId);
              if (index2 == count)
              {
                intList = new List<int>();
                Animator2DCustomisableRenderer.s_triangleBuffers.Add(intList);
                ++count;
              }
              else
                intList = Animator2DCustomisableRenderer.s_triangleBuffers[index2];
            }
            List<Vector3> vertexBuffer = Animator2DCustomisableRenderer.s_vertexBuffer;
            List<Vector3> colorBuffer = Animator2DCustomisableRenderer.s_colorBuffer;
            List<Vector2> uvBuffer = Animator2DCustomisableRenderer.s_uvBuffer;
            float m00 = customisableRenderState.m00;
            float m01 = customisableRenderState.m01;
            float m03 = customisableRenderState.m03;
            float m10 = customisableRenderState.m10;
            float m11 = customisableRenderState.m11;
            float m13 = customisableRenderState.m13;
            Vector3 vector3_1 = new Vector3((float) customisableRenderState.alpha / (float) byte.MaxValue, customisableRenderState.multiplicativeColor, customisableRenderState.additiveColor);
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
              intList.Add(num3 + num1);
            }
            num1 += vertices.Length;
          }
        }
      }
    }

    public void EnableKeyword(string keyword) => this.m_materialArray.EnableKeyword(keyword);

    public void DisableKeyword(string keyword) => this.m_materialArray.DisableKeyword(keyword);

    public bool IsKeywordEnabled(string keyword) => this.m_materialArray.IsKeywordEnabled(keyword);
  }
}
