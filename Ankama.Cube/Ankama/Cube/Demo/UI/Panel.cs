// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.Panel
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  [RequireComponent(typeof (RectTransform))]
  public class Panel : MonoBehaviour
  {
    [SerializeField]
    protected CanvasGroup m_transitionCanvasGroup;
    [SerializeField]
    protected Image m_illu;
    [SerializeField]
    private Image[] m_shadows;
    [SerializeField]
    private AbstractTextField[] m_texts;
    private float m_visibilityFactor = 1f;

    public float GetVisibilityFactor() => this.m_visibilityFactor;

    public CanvasGroup transitionCanvasGroup => this.m_transitionCanvasGroup;

    public void SetVisibilityFactor(float value, PanelListConfig config)
    {
      this.m_visibilityFactor = Mathf.Clamp01(value);
      float t1 = config.depthRepartition.Evaluate(this.m_visibilityFactor);
      float t2 = config.shadowDepthRepartition.Evaluate(this.m_visibilityFactor);
      this.m_illu.color = new Color(Mathf.Lerp(config.imageDepthDarken, 1f, t1), Mathf.Lerp(config.imageDepthDesaturation, 1f, t1), 1f, 1f);
      Color color = Color.Lerp(config.textDepthTint, Color.white, t1);
      for (int index = 0; index < this.m_texts.Length; ++index)
        this.m_texts[index].color = color;
      float a = Mathf.Lerp(config.shadowDepthAlpha, 1f, t2);
      for (int index = 0; index < this.m_shadows.Length; ++index)
      {
        Image shadow = this.m_shadows[index];
        shadow.WithAlpha<Image>(a);
        shadow.gameObject.SetActive((double) a > 0.0099999997764825821);
      }
    }
  }
}
