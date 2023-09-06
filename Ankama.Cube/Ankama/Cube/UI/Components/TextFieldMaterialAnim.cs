// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.TextFieldMaterialAnim
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [RequireComponent(typeof (AbstractTextField))]
  [ExecuteInEditMode]
  public class TextFieldMaterialAnim : MonoBehaviour
  {
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_softness;
    [SerializeField]
    [Range(-1f, 1f)]
    private float m_dilate;
    private AbstractTextField m_textField;
    internal static readonly int OutlineSoftnessId = Shader.PropertyToID("_OutlineSoftness");
    internal static readonly int FaceDilateId = Shader.PropertyToID("_FaceDilate");

    private void OnEnable() => this.UpdateVisual();

    protected void OnDidApplyAnimationProperties() => this.UpdateVisual();

    private void UpdateVisual()
    {
      if ((Object) this.m_textField == (Object) null)
        this.m_textField = this.GetComponent<AbstractTextField>();
      if ((Object) this.m_textField == (Object) null)
        return;
      Material material = this.m_textField.material;
      if (!((Object) material != (Object) null))
        return;
      material.SetFloat(TextFieldMaterialAnim.OutlineSoftnessId, this.m_softness);
      material.SetFloat(TextFieldMaterialAnim.FaceDilateId, this.m_dilate);
    }
  }
}
