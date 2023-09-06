// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Render.CharacterMeshShaderProperties
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Maps;
using UnityEngine;

namespace Ankama.Cube.Render
{
  [ExecuteInEditMode]
  [RequireComponent(typeof (Animator2D))]
  public class CharacterMeshShaderProperties : MonoBehaviour
  {
    private const string PropertyName = "_CharacterParams";
    private static readonly int s_propertyIndex = Shader.PropertyToID("_CharacterParams");
    private static Camera s_currentCamera;
    private static Vector3 s_vector;
    private Animator2D m_animator2D;

    private void OnEnable()
    {
      this.m_animator2D = this.GetComponent<Animator2D>();
      if ((Object) null == (Object) this.m_animator2D)
      {
        this.enabled = false;
      }
      else
      {
        if ((Object) null == (Object) CharacterMeshShaderProperties.s_currentCamera)
        {
          CharacterMeshShaderProperties.s_currentCamera = Camera.main;
          if ((Object) null == (Object) CharacterMeshShaderProperties.s_currentCamera)
            return;
        }
        CameraHandler.AddMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));
      }
    }

    private void OnDisable() => CameraHandler.RemoveMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));

    private void OnMapRotationChanged(
      DirectionAngle previousMapRotation,
      DirectionAngle newMapRotation)
    {
      CharacterMeshShaderProperties.s_vector = Quaternion.Euler(30f, CharacterMeshShaderProperties.s_currentCamera.transform.eulerAngles.y, 0.0f) * Vector3.forward / 0.5f;
      this.Refresh();
    }

    private void OnWillRenderObject()
    {
      if (!this.transform.hasChanged)
        return;
      this.Refresh();
      this.transform.hasChanged = false;
    }

    private void Refresh()
    {
      Vector3 vector = CharacterMeshShaderProperties.s_vector;
      Vector4 vector4 = new Vector4(vector.x, vector.y, vector.z, this.transform.position.y);
      MaterialPropertyBlock propertyBlock = this.m_animator2D.GetPropertyBlock();
      propertyBlock.SetVector(CharacterMeshShaderProperties.s_propertyIndex, vector4);
      this.m_animator2D.SetPropertyBlock(propertyBlock);
    }
  }
}
