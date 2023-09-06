// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.ImagePositionToShader
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  [RequireComponent(typeof (Image))]
  public class ImagePositionToShader : MonoBehaviour
  {
    [SerializeField]
    private Transform m_transform;
    private int m_positionID;
    private Material m_material;

    protected void Awake()
    {
      this.m_positionID = Shader.PropertyToID("_Position");
      Image component = this.GetComponent<Image>();
      this.m_material = Object.Instantiate<Material>(component.material);
      component.material = this.m_material;
    }

    public void Update() => this.m_material.SetVector(this.m_positionID, (Vector4) this.m_transform.localPosition);

    public void SetColor(Color c1, Color c2)
    {
      this.m_material.SetColor("_Color1", c1);
      this.m_material.SetColor("_Color2", c2);
    }

    public void TweenColor(Color c1, Color c2, float duration)
    {
      this.m_material.DOColor(c1, "_Color1", duration);
      this.m_material.DOColor(c2, "_Color2", duration);
    }

    private void OnDestroy() => Object.Destroy((Object) this.m_material);
  }
}
