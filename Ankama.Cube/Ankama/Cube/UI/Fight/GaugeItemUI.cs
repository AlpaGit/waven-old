// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.GaugeItemUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public class GaugeItemUI : MonoBehaviour
  {
    [Header("Components")]
    [SerializeField]
    protected UISpriteTextRenderer m_text;
    [SerializeField]
    protected UISpriteTextRenderer m_modificationText;
    [SerializeField]
    protected Image m_image;
    [SerializeField]
    protected ParticleSystem m_upFX;
    [SerializeField]
    protected UISpriteTextRenderer m_upMFXModificationText;
    [Header("Modification")]
    [SerializeField]
    protected GaugePreviewResource m_previewResource;
    protected int m_value;
    private int m_tweenedValue;
    private int m_tweenedEndValue = int.MinValue;
    private int? m_modificationPreviewValue;
    private Tweener m_valueTweener;
    private bool m_highlighted;
    private Tween m_highlightTweener;

    public void SetSprite(Sprite sprite)
    {
      if (!((Object) this.m_image != (Object) null))
        return;
      this.m_image.sprite = sprite;
    }

    public void SetColor(Color color)
    {
      if ((Object) this.m_image != (Object) null)
        this.m_image.color = color;
      this.m_text.color = color;
    }

    public void SetAlpha(float alpha)
    {
      if ((Object) this.m_image != (Object) null)
        this.m_image.WithAlpha<Image>(alpha);
      this.m_text.WithAlpha<UISpriteTextRenderer>(alpha);
    }

    public void DoAlpha(float alpha, float duration)
    {
      if ((Object) this.m_image != (Object) null)
        DOTweenModuleUI.DOFade(this.m_image, alpha, duration);
      this.m_text.DOFade(alpha, duration);
    }

    public void Desaturate(float desaturationFactor)
    {
      if (!((Object) this.m_image != (Object) null))
        return;
      this.m_image.Desaturate<Image>(desaturationFactor);
    }

    public void Highlight(bool highlight)
    {
      if (this.m_highlighted == highlight || (Object) this.m_previewResource == (Object) null || !this.m_previewResource.highlightEnabled)
        return;
      Tween highlightTweener = this.m_highlightTweener;
      if ((highlightTweener != null ? (highlightTweener.IsPlaying() ? 1 : 0) : 0) != 0)
      {
        this.m_highlightTweener.Kill(true);
        this.m_highlightTweener = (Tween) null;
      }
      if (highlight && (Object) this.m_image != (Object) null)
      {
        float highlightPunch = this.m_previewResource.highlightPunch;
        this.m_highlightTweener = (Tween) this.m_image.transform.DOPunchScale(new Vector3(highlightPunch, highlightPunch, highlightPunch), this.m_previewResource.highlightDuration, this.m_previewResource.highlightVibrato, this.m_previewResource.highlightElasticity);
        int highlightLoopCount = this.m_previewResource.highlightLoopCount;
        if (highlightLoopCount != 1)
          this.m_highlightTweener.SetLoops<Tween>(highlightLoopCount);
      }
      this.m_highlighted = highlight;
    }

    protected int GetRealValue() => this.m_value;

    protected int GetTweenedValue() => this.m_tweenedValue;

    private void SetTweenedValue(int value)
    {
      this.m_tweenedValue = value;
      this.UpdateText(value);
    }

    protected virtual void UpdateText(int value) => this.m_text.text = this.m_tweenedValue.ToString();

    public virtual void UpdateMaxValue(int maxValueModification)
    {
    }

    public virtual void SetValue(int v)
    {
      if (v > this.m_value)
        this.PlayUpFX(v - this.m_value);
      this.m_value = v;
      this.UpdateTweenedValue();
    }

    private void PlayUpFX(int modificationValue)
    {
      if (!((Object) this.m_upFX != (Object) null))
        return;
      this.m_upMFXModificationText.text = "+" + (object) modificationValue;
      this.m_upFX.gameObject.SetActive(false);
      this.m_upFX.gameObject.SetActive(true);
      this.m_upFX.Play();
    }

    private void UpdateTweenedValue(bool tweening = true)
    {
      int endValue = this.GetRealValue() + (this.m_modificationPreviewValue ?? 0);
      if (endValue == this.m_tweenedEndValue)
        return;
      this.m_tweenedEndValue = endValue;
      if (this.m_valueTweener != null && this.m_valueTweener.IsPlaying())
      {
        this.m_valueTweener.Kill();
        this.m_valueTweener = (Tweener) null;
      }
      if (!tweening || (Object) null == (Object) this.m_previewResource)
        this.SetTweenedValue(endValue);
      else
        this.m_valueTweener = DOTween.To(new DOGetter<int>(this.GetTweenedValue), new DOSetter<int>(this.SetTweenedValue), endValue, this.m_previewResource.duration).SetEase<Tweener>(this.m_previewResource.ease);
    }

    public void AddModificationPreview(int modification)
    {
      this.m_modificationPreviewValue = new int?(modification);
      this.UpdateTweenedValue();
      this.Highlight(true);
      if (!((Object) this.m_modificationText != (Object) null) || !((Object) this.m_previewResource != (Object) null) || !this.m_previewResource.displayText)
        return;
      this.m_modificationText.text = modification.ToStringSigned();
      this.m_modificationText.gameObject.SetActive(true);
    }

    public void RemoveModificationPreview()
    {
      if (!this.m_modificationPreviewValue.HasValue)
        return;
      this.m_modificationPreviewValue = new int?();
      this.UpdateTweenedValue(false);
      this.Highlight(false);
      if (!((Object) this.m_modificationText != (Object) null))
        return;
      this.m_modificationText.gameObject.SetActive(false);
    }
  }
}
