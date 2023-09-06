// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Info.FightInfoMessageRibbon
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.UI.Components;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.Info
{
  public class FightInfoMessageRibbon : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup m_fieldView;
    [SerializeField]
    private GameObject m_visualRoot;
    [SerializeField]
    private TextField m_playerOriginalTextField;
    [Header("Ribbon")]
    [SerializeField]
    private float m_verticalSpacing = 45f;
    [Header("IconRoot")]
    [SerializeField]
    private Image m_iconImg;
    [SerializeField]
    private GameObject m_countRoot;
    [SerializeField]
    private UISpriteTextRenderer m_countText;
    [SerializeField]
    private FightInfoMessageRibbonData m_datas;
    private List<string> m_messageParameter;
    private FightInfoValueProvider m_valueProvider;

    public void Awake() => this.m_messageParameter = new List<string>();

    public void PlayAnimation(int ribbonMessageID, Action<FightInfoMessageRibbon> callback)
    {
      this.gameObject.SetActive(true);
      this.m_playerOriginalTextField.SetText(ribbonMessageID, (IValueProvider) this.GetProvider());
      this.m_fieldView.alpha = 0.0f;
      this.m_visualRoot.transform.localPosition = new Vector3(0.0f, -100f, 0.0f);
      this.m_visualRoot.transform.localScale = new Vector3(3f, 3f, 3f);
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) DOTween.To(new DOGetter<float>(this.FieldViewAlphaGetter), new DOSetter<float>(this.FieldViewAlphaSetter), 1f, 0.5f));
      sequence.Insert(0.0f, (Tween) DOTween.To(new DOGetter<Vector3>(this.VisualRootLocalPositionGetter), new DOSetter<Vector3>(this.VisualRootLocalPositionSetter), Vector3.zero, 0.5f));
      sequence.Insert(0.0f, (Tween) DOTween.To(new DOGetter<Vector3>(this.VisualRootLocalScaleGetter), new DOSetter<Vector3>(this.VisualRootLocalScaleSetter), Vector3.one, 0.3f));
      sequence.Insert(2f, (Tween) DOTween.To(new DOGetter<float>(this.FieldViewAlphaGetter), new DOSetter<float>(this.FieldViewAlphaSetter), 0.0f, 0.3f).SetDelay<TweenerCore<float, float, FloatOptions>>(2f));
      sequence.OnComplete<Sequence>((TweenCallback) (() => callback(this)));
      sequence.Play<Sequence>();
      this.transform.SetAsLastSibling();
    }

    public void Initialise(MessageInfoType msgType, int iconID, Color bgColor, int countValue = 0)
    {
      this.m_countText.color = bgColor;
      MessageInfoIconData[] icons = this.m_datas.icons;
      if (iconID < icons.Length)
      {
        MessageInfoIconData messageInfoIconData = icons[iconID];
        this.m_iconImg.sprite = messageInfoIconData.visual;
        this.m_iconImg.color = messageInfoIconData.useColor ? bgColor : Color.white;
      }
      else
        this.m_iconImg.gameObject.SetActive(false);
      if (msgType != MessageInfoType.Default)
      {
        if (msgType != MessageInfoType.Score)
          throw new ArgumentOutOfRangeException(nameof (msgType), (object) msgType, (string) null);
        this.m_countRoot.SetActive(true);
        this.m_countText.text = countValue.ToString();
      }
      else
        this.m_countRoot.SetActive(false);
    }

    public void SetExpectedIndex(int expected, bool tween)
    {
      Vector3 endValue = new Vector3(0.0f, -this.m_verticalSpacing * (float) expected, 0.0f);
      if (tween)
      {
        this.transform.localPosition = new Vector3(0.0f, -this.m_verticalSpacing * (float) (expected + 1), 0.0f);
        this.transform.DOLocalMove(endValue, 0.5f);
      }
      else
        this.transform.localPosition = endValue;
    }

    private Vector3 VisualRootLocalPositionGetter() => this.m_visualRoot.transform.localPosition;

    private void VisualRootLocalPositionSetter(Vector3 value) => this.m_visualRoot.transform.localPosition = value;

    private Vector3 VisualRootLocalScaleGetter() => this.m_visualRoot.transform.localScale;

    private void VisualRootLocalScaleSetter(Vector3 value) => this.m_visualRoot.transform.localScale = value;

    private float FieldViewAlphaGetter() => this.m_fieldView.alpha;

    private void FieldViewAlphaSetter(float value) => this.m_fieldView.alpha = value;

    public void ClearParameters() => this.m_messageParameter?.Clear();

    public void AddParameter(string parameter)
    {
      if (this.m_valueProvider == null)
        this.m_valueProvider = new FightInfoValueProvider(this);
      if (this.m_messageParameter == null)
        this.m_messageParameter = new List<string>();
      this.m_messageParameter.Add(parameter);
    }

    public FightInfoValueProvider GetProvider()
    {
      if (this.m_valueProvider == null)
        this.m_valueProvider = new FightInfoValueProvider(this);
      return this.m_valueProvider;
    }

    public IReadOnlyList<string> GetParameter() => (IReadOnlyList<string>) this.m_messageParameter;
  }
}
