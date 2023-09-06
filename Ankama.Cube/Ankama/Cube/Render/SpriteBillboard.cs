// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Render.SpriteBillboard
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Render
{
  [ExecuteInEditMode]
  public class SpriteBillboard : MonoBehaviour
  {
    [SerializeField]
    private Transform[] m_transforms;
    [SerializeField]
    private bool m_lookTowardDirection;
    private Vector3[] m_previousWorldSpacePositions;

    private void OnEnable()
    {
      Camera.onPreCull += new Camera.CameraCallback(this.OnPrecullCamera);
      int length = this.m_transforms.Length;
      this.m_previousWorldSpacePositions = new Vector3[length];
      for (int index = 0; index < length; ++index)
        this.m_previousWorldSpacePositions[index] = this.m_transforms[index].position;
    }

    private void OnDisable() => Camera.onPreCull -= new Camera.CameraCallback(this.OnPrecullCamera);

    private void OnPrecullCamera(Camera currentCamera)
    {
      if (!currentCamera.isActiveAndEnabled)
        return;
      Transform[] transforms = this.m_transforms;
      int length = transforms.Length;
      Transform transform1 = currentCamera.transform;
      Vector3 forward = transform1.forward;
      if (this.m_lookTowardDirection)
      {
        Vector3[] worldSpacePositions = this.m_previousWorldSpacePositions;
        Vector3 right = transform1.right;
        for (int index = 0; index < length; ++index)
        {
          Transform transform2 = transforms[index];
          Vector3 position = transform2.position;
          Vector3 rhs = position - worldSpacePositions[index];
          worldSpacePositions[index] = position;
          transform2.forward = -Mathf.Sign(Vector3.Dot(right, rhs)) * forward;
        }
      }
      else
      {
        for (int index = 0; index < length; ++index)
          transforms[index].forward = forward;
      }
    }
  }
}
