// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterUIContainer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  [ExecuteInEditMode]
  public class CharacterUIContainer : MonoBehaviour
  {
    private const float MapCellIndicatorTweenDuration = 0.2f;
    private const float PixelPerUnit = 100f;
    [SerializeField]
    private CharacterUIResources m_resources;
    [SerializeField]
    private Transform m_adjustableHeightContainer;
    [Header("Map Cell Indicators")]
    [SerializeField]
    private SpriteRenderer m_mapCellIndicatorRenderer;
    [Header("Layout")]
    [SerializeField]
    private CharacterUILayoutElement[] m_layoutElements = new CharacterUILayoutElement[0];
    [SerializeField]
    private int m_layoutSpacing = 1;
    private Color m_color;
    private int m_sortingOrder;
    private bool m_layoutIsDirty;
    private CharacterHeight m_height = CharacterHeight.Normal;
    private MapCellIndicator m_mapCellIndicator;
    private CharacterUIContainer.TweenState m_tweenState;
    private Tweener m_mapCellIndicatorTween;
    private float m_mapCellIndicatorAlpha;

    public Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        Color color = value;
        color.a *= this.m_mapCellIndicatorAlpha;
        this.m_mapCellIndicatorRenderer.color = color;
      }
    }

    public int sortingOrder
    {
      get => this.m_sortingOrder;
      set
      {
        this.m_sortingOrder = value;
        this.m_mapCellIndicatorRenderer.sortingOrder = this.sortingOrder;
      }
    }

    public void Setup()
    {
      Color color = this.m_color with { a = 0.0f };
      this.m_mapCellIndicatorAlpha = 0.0f;
      this.m_mapCellIndicatorRenderer.color = color;
      this.m_mapCellIndicatorRenderer.enabled = false;
      this.m_mapCellIndicatorRenderer.sprite = (Sprite) null;
    }

    public void SetCharacterHeight(CharacterHeight value)
    {
      if (this.m_height == value)
        return;
      float height = value.GetHeight();
      this.m_adjustableHeightContainer.localPosition = this.m_adjustableHeightContainer.localPosition with
      {
        y = height / this.transform.localScale.y
      };
      this.m_height = value;
    }

    public void SetCellIndicator(MapCellIndicator cellIndicator)
    {
      if (cellIndicator == this.m_mapCellIndicator)
        return;
      switch (this.m_tweenState)
      {
        case CharacterUIContainer.TweenState.None:
          if (this.m_mapCellIndicator == MapCellIndicator.None)
          {
            this.m_mapCellIndicatorRenderer.sprite = this.GetMapIndicatorSprite(cellIndicator);
            this.m_mapCellIndicatorRenderer.enabled = true;
            this.m_tweenState = CharacterUIContainer.TweenState.Showing;
            this.m_mapCellIndicatorTween = (Tweener) DOTween.To(new DOGetter<float>(this.MapCellIndicatorTweenGetter), new DOSetter<float>(this.MapCellIndicatorTweenSetter), 1f, 0.2f).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnMapCellIndicatorShowingTweenComplete));
            this.m_mapCellIndicator = cellIndicator;
            return;
          }
          break;
        case CharacterUIContainer.TweenState.Showing:
          this.m_mapCellIndicatorTween.Kill();
          break;
        case CharacterUIContainer.TweenState.Hiding:
          this.m_mapCellIndicator = cellIndicator;
          return;
        default:
          throw new ArgumentOutOfRangeException();
      }
      float duration = Mathf.Lerp(0.0f, 0.2f, this.m_mapCellIndicatorAlpha);
      this.m_tweenState = CharacterUIContainer.TweenState.Hiding;
      this.m_mapCellIndicatorTween = (Tweener) DOTween.To(new DOGetter<float>(this.MapCellIndicatorTweenGetter), new DOSetter<float>(this.MapCellIndicatorTweenSetter), 0.0f, duration).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnMapCellIndicatorHidingTweenComplete));
      this.m_mapCellIndicator = cellIndicator;
    }

    public void SetLayoutDirty() => this.m_layoutIsDirty = true;

    private void OnEnable()
    {
      CameraHandler current = CameraHandler.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
      {
        current.onZoomChanged += new Action<CameraHandler>(this.OnZoomChanged);
        this.OnZoomChanged(current);
      }
      Device.ScreenStateChanged += new Device.ScreenSateChangedDelegate(this.OnScreenStateChange);
      CharacterUILayoutElement[] layoutElements = this.m_layoutElements;
      int length = layoutElements.Length;
      for (int index = 0; index < length; ++index)
        layoutElements[index].SetContainer(this);
      this.m_layoutIsDirty = true;
    }

    private void LateUpdate()
    {
      if (!this.m_layoutIsDirty)
        return;
      this.UpdateLayout();
      this.m_layoutIsDirty = false;
    }

    private void OnDisable()
    {
      CameraHandler current = CameraHandler.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        current.onZoomChanged -= new Action<CameraHandler>(this.OnZoomChanged);
      Device.ScreenStateChanged -= new Device.ScreenSateChangedDelegate(this.OnScreenStateChange);
    }

    private void OnZoomChanged(CameraHandler cameraHandler) => this.SetLocalScale(cameraHandler.camera.orthographicSize);

    private void OnScreenStateChange()
    {
      CameraHandler current = CameraHandler.current;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) current))
        return;
      this.SetLocalScale(current.camera.orthographicSize);
    }

    private void SetLocalScale(float orthographicSize)
    {
      int height1 = Screen.height;
      float num = (float) (100.0 / (double) ((float) height1 / (2f * orthographicSize)) * (double) height1 / 1080.0);
      this.transform.localScale = new Vector3(num, num, num);
      float height2 = this.m_height.GetHeight();
      this.m_adjustableHeightContainer.localPosition = this.m_adjustableHeightContainer.localPosition with
      {
        y = height2 / num
      };
    }

    private void UpdateLayout()
    {
      CharacterUILayoutElement[] layoutElements = this.m_layoutElements;
      int length = layoutElements.Length;
      if (length == 0)
        return;
      int layoutSpacing = this.m_layoutSpacing;
      int num1 = 0;
      for (int index = 0; index < length; ++index)
      {
        CharacterUILayoutElement characterUiLayoutElement = layoutElements[index];
        if (!((UnityEngine.Object) null == (UnityEngine.Object) characterUiLayoutElement) && characterUiLayoutElement.enabled)
          num1 += characterUiLayoutElement.layoutWidth + layoutSpacing;
      }
      int num2 = -(num1 - layoutSpacing >> 1);
      int num3 = 0;
      for (int index = 0; index < length; ++index)
      {
        CharacterUILayoutElement characterUiLayoutElement = layoutElements[index];
        if (!((UnityEngine.Object) null == (UnityEngine.Object) characterUiLayoutElement) && characterUiLayoutElement.enabled)
        {
          characterUiLayoutElement.SetLayoutPosition(num3 + num2);
          num3 += characterUiLayoutElement.layoutWidth + layoutSpacing;
        }
      }
    }

    private float MapCellIndicatorTweenGetter() => this.m_mapCellIndicatorAlpha;

    private void MapCellIndicatorTweenSetter(float value)
    {
      Color color = this.m_color;
      color.a *= value;
      this.m_mapCellIndicatorRenderer.color = color;
      this.m_mapCellIndicatorAlpha = value;
    }

    private void OnMapCellIndicatorHidingTweenComplete()
    {
      if (this.m_mapCellIndicator == MapCellIndicator.None)
      {
        this.m_mapCellIndicatorRenderer.enabled = false;
        this.m_tweenState = CharacterUIContainer.TweenState.None;
        this.m_mapCellIndicatorTween = (Tweener) null;
      }
      else
      {
        this.m_mapCellIndicatorRenderer.sprite = this.GetMapIndicatorSprite(this.m_mapCellIndicator);
        this.m_tweenState = CharacterUIContainer.TweenState.Showing;
        this.m_mapCellIndicatorTween = (Tweener) DOTween.To(new DOGetter<float>(this.MapCellIndicatorTweenGetter), new DOSetter<float>(this.MapCellIndicatorTweenSetter), 1f, 0.2f).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnMapCellIndicatorShowingTweenComplete));
      }
    }

    private void OnMapCellIndicatorShowingTweenComplete()
    {
      this.m_tweenState = CharacterUIContainer.TweenState.None;
      this.m_mapCellIndicatorTween = (Tweener) null;
    }

    private Sprite GetMapIndicatorSprite(MapCellIndicator cellIndicator)
    {
      if (cellIndicator == MapCellIndicator.None)
        return (Sprite) null;
      if (cellIndicator == MapCellIndicator.Death)
        return this.m_resources.mapCellIndicatorDeathIcon;
      throw new ArgumentOutOfRangeException(nameof (cellIndicator), (object) cellIndicator, (string) null);
    }

    private enum TweenState
    {
      None,
      Showing,
      Hiding,
    }
  }
}
