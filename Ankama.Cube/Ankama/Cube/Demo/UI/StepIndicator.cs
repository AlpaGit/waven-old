// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StepIndicator
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class StepIndicator : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private StepIndicatorData m_data;
    private StepIndicator.State m_state;
    private float m_scale;

    protected void OnEnable()
    {
      if (this.m_state == StepIndicator.State.None)
        return;
      this.StartCoroutine(this.DelayUpdateVisual());
    }

    public void SetState(StepIndicator.State state, bool tween)
    {
      if (this.m_state == state)
        return;
      this.m_state = state;
      if (!this.isActiveAndEnabled)
        return;
      this.UpdateVisual(tween);
    }

    private IEnumerator DelayUpdateVisual()
    {
      yield return (object) null;
      this.UpdateVisual(false);
    }

    private void UpdateVisual(bool tween)
    {
      switch (this.m_state)
      {
        case StepIndicator.State.Disable:
          if (tween)
          {
            DOVirtual.Float(1f, 0.0f, this.m_data.transitionDuration, new TweenCallback<float>(this.LerpState));
            break;
          }
          this.LerpState(0.0f);
          break;
        case StepIndicator.State.Enable:
          if (tween)
          {
            DOVirtual.Float(0.0f, 1f, this.m_data.transitionDuration, new TweenCallback<float>(this.LerpState));
            break;
          }
          this.LerpState(1f);
          break;
      }
    }

    private void LerpState(float value)
    {
      StepIndicatorData.StateData disableState = this.m_data.disableState;
      StepIndicatorData.StateData enableState = this.m_data.enableState;
      float num1 = Mathf.Lerp(disableState.alpha, enableState.alpha, value);
      this.m_scale = Mathf.Lerp(disableState.scale, enableState.scale, value);
      this.m_canvasGroup.alpha = num1;
      this.m_background.transform.localScale = Vector3.one * this.m_scale;
      RectTransform transform1 = this.transform as RectTransform;
      RectTransform transform2 = this.m_background.transform as RectTransform;
      Rect rect = transform1.rect;
      double num2 = (double) rect.width / (double) this.m_scale;
      rect = transform1.rect;
      double width = (double) rect.width;
      float num3 = (float) (num2 - width);
      transform2.sizeDelta = Vector2.zero;
      transform2.offsetMax = new Vector2(num3 / 2f, transform2.offsetMax.y);
      transform2.offsetMin = new Vector2((float) (-(double) num3 / 2.0 + (28.0 - 28.0 * (double) this.m_scale)), transform2.offsetMin.y);
    }

    public enum State
    {
      None,
      Disable,
      Enable,
    }
  }
}
