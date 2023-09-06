// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.LightweightShadowUtils
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class LightweightShadowUtils
  {
    public static bool ExtractSpotLightMatrix(
      ref CullResults cullResults,
      int shadowLightIndex,
      out Matrix4x4 shadowMatrix,
      out Matrix4x4 viewMatrix,
      out Matrix4x4 projMatrix)
    {
      int num = cullResults.ComputeSpotShadowMatricesAndCullingPrimitives(shadowLightIndex, out viewMatrix, out projMatrix, out ShadowSplitData _) ? 1 : 0;
      shadowMatrix = LightweightShadowUtils.GetShadowTransform(projMatrix, viewMatrix);
      return num != 0;
    }

    public static void RenderShadowSlice(
      CommandBuffer cmd,
      ref ScriptableRenderContext context,
      ref ShadowSliceData shadowSliceData,
      Matrix4x4 proj,
      Matrix4x4 view,
      DrawShadowsSettings settings)
    {
      cmd.SetViewport(new Rect((float) shadowSliceData.offsetX, (float) shadowSliceData.offsetY, (float) shadowSliceData.resolution, (float) shadowSliceData.resolution));
      cmd.EnableScissorRect(new Rect((float) (shadowSliceData.offsetX + 4), (float) (shadowSliceData.offsetY + 4), (float) (shadowSliceData.resolution - 8), (float) (shadowSliceData.resolution - 8)));
      cmd.SetViewProjectionMatrices(view, proj);
      context.ExecuteCommandBuffer(cmd);
      cmd.Clear();
      context.DrawShadows(ref settings);
      cmd.DisableScissorRect();
      context.ExecuteCommandBuffer(cmd);
      cmd.Clear();
    }

    public static int GetMaxTileResolutionInAtlas(int atlasWidth, int atlasHeight, int tileCount)
    {
      int f = Mathf.Min(atlasWidth, atlasHeight);
      if ((double) tileCount > (double) Mathf.Log((float) f))
      {
        Debug.LogError((object) string.Format("Cannot fit {0} tiles into current shadowmap atlas of size ({1}, {2}). ShadowMap Resolution set to zero.", (object) tileCount, (object) atlasWidth, (object) atlasHeight));
        return 0;
      }
      for (int index = atlasWidth / f * atlasHeight / f; index < tileCount; index = atlasWidth / f * atlasHeight / f)
        f >>= 1;
      return f;
    }

    public static void ApplySliceTransform(
      ref ShadowSliceData shadowSliceData,
      int atlasWidth,
      int atlasHeight)
    {
      Matrix4x4 identity = Matrix4x4.identity;
      float num1 = 1f / (float) atlasWidth;
      float num2 = 1f / (float) atlasHeight;
      identity.m00 = (float) shadowSliceData.resolution * num1;
      identity.m11 = (float) shadowSliceData.resolution * num2;
      identity.m03 = (float) shadowSliceData.offsetX * num1;
      identity.m13 = (float) shadowSliceData.offsetY * num2;
      shadowSliceData.shadowTransform = identity * shadowSliceData.shadowTransform;
    }

    public static void SetupShadowCasterConstants(
      CommandBuffer cmd,
      ref VisibleLight visibleLight,
      Matrix4x4 proj,
      float cascadeResolution)
    {
      Light light = visibleLight.light;
      float x = 0.0f;
      float y = 0.0f;
      if (visibleLight.lightType == LightType.Directional)
      {
        float num1 = SystemInfo.usesReversedZBuffer ? 1f : -1f;
        x = light.shadowBias * proj.m22 * num1;
        double num2 = 2.0 / (double) proj.m00;
        double num3 = 2.0 / (double) proj.m11;
        double num4 = (double) cascadeResolution;
        float num5 = Mathf.Max((float) (num2 / num4), (float) num3 / cascadeResolution);
        y = (float) (-(double) light.shadowNormalBias * (double) num5 * 3.6500000953674316);
      }
      else if (visibleLight.lightType == LightType.Spot)
      {
        float num = SystemInfo.usesReversedZBuffer ? -1f : 1f;
        x = light.shadowBias * num;
        y = 0.0f;
      }
      else
        Debug.LogWarning((object) "Only spot and directional shadow casters are supported in lightweight pipeline");
      Vector3 vector3 = (Vector3) -visibleLight.localToWorld.GetColumn(2);
      cmd.SetGlobalVector("_ShadowBias", new Vector4(x, y, 0.0f, 0.0f));
      cmd.SetGlobalVector("_LightDirection", new Vector4(vector3.x, vector3.y, vector3.z, 0.0f));
    }

    public static Matrix4x4 GetShadowTransform(Matrix4x4 proj, Matrix4x4 view)
    {
      if (SystemInfo.usesReversedZBuffer)
      {
        proj.m20 = -proj.m20;
        proj.m21 = -proj.m21;
        proj.m22 = -proj.m22;
        proj.m23 = -proj.m23;
      }
      return Matrix4x4.identity with
      {
        m00 = 0.5f,
        m11 = 0.5f,
        m22 = 0.5f,
        m03 = 0.5f,
        m23 = 0.5f,
        m13 = 0.5f
      } * (proj * view);
    }
  }
}
