// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.ImageLoader
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public sealed class ImageLoader : UIResourceLoader<Sprite>
  {
    [Header("Target")]
    [SerializeField]
    private Image m_image;
    [Header("Tween Settings")]
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_fadeInTweenDuration = 0.25f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_fadeOutTweenDuration = 0.15f;
    private TweenerCore<float, float, FloatOptions> m_tween;
    private Material m_material;
    private Color m_color = Color.white;
    private float m_alpha = 1f;

    public Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        if (!((Object) null != (Object) this.m_image))
          return;
        Color color = value;
        color.a *= this.m_alpha;
        this.m_image.color = color;
      }
    }

    public Material material
    {
      get => this.m_material;
      set
      {
        this.m_material = value;
        if (!((Object) null != (Object) this.m_image))
          return;
        this.m_image.material = value;
      }
    }

    private void Awake()
    {
      if (!((Object) null != (Object) this.m_image))
        return;
      this.m_color = this.m_image.color;
      this.m_material = this.m_image.material;
    }

    protected override IEnumerator Apply(Sprite sprite, UIResourceDisplayMode displayMode)
    {
      ImageLoader imageLoader = this;
      if ((Object) null == (Object) imageLoader.m_image)
      {
        Log.Warning("No image component has been linked.", 75, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\ResourceLoader\\ImageLoader.cs");
      }
      else
      {
        if (imageLoader.m_tween != null)
        {
          imageLoader.m_tween.Kill();
          imageLoader.m_tween = (TweenerCore<float, float, FloatOptions>) null;
        }
        if (displayMode == UIResourceDisplayMode.Immediate || !imageLoader.m_image.gameObject.activeInHierarchy)
        {
          imageLoader.m_alpha = 1f;
          imageLoader.m_image.color = imageLoader.m_color;
          imageLoader.m_image.sprite = sprite;
          imageLoader.m_image.enabled = true;
        }
        else
        {
          if ((Object) null != (Object) imageLoader.m_image.sprite)
          {
            float duration = imageLoader.m_fadeOutTweenDuration * imageLoader.m_alpha;
            if ((double) duration > 0.0)
            {
              imageLoader.m_tween = DOTween.To(new DOGetter<float>(imageLoader.TweenGetter), new DOSetter<float>(imageLoader.TweenSetter), 0.0f, duration).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(imageLoader.OnTweenComplete));
              while (imageLoader.m_tween != null && imageLoader.m_tween.IsPlaying())
                yield return (object) null;
            }
          }
          else
          {
            Color color = imageLoader.m_color with
            {
              a = 0.0f
            };
            imageLoader.m_alpha = 0.0f;
            imageLoader.m_image.color = color;
          }
          imageLoader.m_image.enabled = true;
          imageLoader.m_image.sprite = sprite;
          imageLoader.m_tween = DOTween.To(new DOGetter<float>(imageLoader.TweenGetter), new DOSetter<float>(imageLoader.TweenSetter), 1f, imageLoader.m_fadeInTweenDuration).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(imageLoader.OnTweenComplete));
          while (imageLoader.m_tween != null && imageLoader.m_tween.IsPlaying())
            yield return (object) null;
        }
      }
    }

    protected override IEnumerator Revert(UIResourceDisplayMode displayMode)
    {
      ImageLoader imageLoader = this;
      if ((Object) null == (Object) imageLoader.m_image)
      {
        Log.Warning("No image component has been linked.", 134, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\ResourceLoader\\ImageLoader.cs");
      }
      else
      {
        if (imageLoader.m_tween != null)
        {
          imageLoader.m_tween.Kill();
          imageLoader.m_tween = (TweenerCore<float, float, FloatOptions>) null;
        }
        if (displayMode == UIResourceDisplayMode.Immediate || !imageLoader.m_image.isActiveAndEnabled)
        {
          Color color = imageLoader.m_color with
          {
            a = 0.0f
          };
          imageLoader.m_alpha = 0.0f;
          imageLoader.m_image.color = color;
          imageLoader.m_image.sprite = (Sprite) null;
          imageLoader.m_image.enabled = false;
        }
        else
        {
          if ((Object) null != (Object) imageLoader.m_image.sprite)
          {
            float duration = imageLoader.m_fadeOutTweenDuration * imageLoader.m_alpha;
            if ((double) duration > 0.0)
            {
              imageLoader.m_tween = DOTween.To(new DOGetter<float>(imageLoader.TweenGetter), new DOSetter<float>(imageLoader.TweenSetter), 0.0f, duration).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(imageLoader.OnTweenComplete));
              while (imageLoader.m_tween != null && !imageLoader.m_tween.IsPlaying())
                yield return (object) null;
            }
          }
          else
          {
            Color color = imageLoader.m_color with
            {
              a = 0.0f
            };
            imageLoader.m_alpha = 0.0f;
            imageLoader.m_image.color = color;
          }
          imageLoader.m_image.sprite = (Sprite) null;
          imageLoader.m_image.enabled = false;
        }
      }
    }

    private void TweenSetter(float value)
    {
      this.m_alpha = value;
      if (!((Object) null != (Object) this.m_image))
        return;
      Color color = this.m_color;
      color.a *= value;
      this.m_image.color = color;
    }

    private float TweenGetter() => this.m_alpha;

    private void OnTweenComplete() => this.m_tween = (TweenerCore<float, float, FloatOptions>) null;
  }
}
