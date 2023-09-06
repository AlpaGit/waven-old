// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Rendering.Animator2DRendererUtility
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using UnityEngine;
using UnityEngine.Rendering;

namespace Ankama.Animations.Rendering
{
  internal static class Animator2DRendererUtility
  {
    public static void CreateMeshComponents(
      GameObject gameObject,
      out MeshRenderer meshRenderer,
      out MeshFilter meshFilter)
    {
      meshRenderer = gameObject.GetComponent<MeshRenderer>();
      if ((Object) null == (Object) meshRenderer)
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
      meshFilter = gameObject.GetComponent<MeshFilter>();
      if ((Object) null == (Object) meshFilter)
        meshFilter = gameObject.AddComponent<MeshFilter>();
      meshRenderer.lightProbeUsage = LightProbeUsage.Off;
      meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
      meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
      meshRenderer.receiveShadows = false;
      meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
    }

    public static void DestroyMeshComponents(MeshRenderer meshRenderer, MeshFilter meshFilter)
    {
      if ((Object) null != (Object) meshRenderer)
        Object.Destroy((Object) meshRenderer);
      if (!((Object) null != (Object) meshFilter))
        return;
      Object.Destroy((Object) meshFilter);
    }

    public static void CreateMesh(MeshFilter meshFilter, out Mesh mesh)
    {
      if ((Object) null == (Object) meshFilter)
      {
        Debug.LogError((object) "Tried to create a mesh without a valid mesh filter.");
        mesh = (Mesh) null;
      }
      else
      {
        mesh = new Mesh();
        mesh.MarkDynamic();
        meshFilter.sharedMesh = mesh;
      }
    }

    public static void DestroyMesh(MeshFilter meshFilter, Mesh mesh)
    {
      if ((Object) null != (Object) meshFilter)
        meshFilter.sharedMesh = (Mesh) null;
      if ((Object) null == (Object) mesh)
        return;
      mesh.Clear();
      Object.Destroy((Object) mesh);
    }
  }
}
