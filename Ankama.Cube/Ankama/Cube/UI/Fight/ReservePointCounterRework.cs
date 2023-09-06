// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.ReservePointCounterRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public sealed class ReservePointCounterRework : 
    MonoBehaviour,
    IPointerEnterHandler,
    IEventSystemHandler,
    IPointerExitHandler,
    ITextTooltipDataProvider,
    ITooltipDataProvider
  {
    [SerializeField]
    private PointCounterRework m_counter;
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    [SerializeField]
    private Button m_button;
    [SerializeField]
    private ParticleSystem m_paUpFx;
    [SerializeField]
    private ParticleSystem m_paDownFx;
    private ReserveDefinition m_reserveDefinition;
    private DynamicFightValueProvider m_fightValueProvider;
    private bool m_uiIsInteractable;

    public event Action OnReserveActivation;

    public void Setup(HeroStatus heroStatus, ReserveDefinition reserveDefinition)
    {
      this.m_reserveDefinition = reserveDefinition;
      this.m_fightValueProvider = new DynamicFightValueProvider((IDynamicValueSource) heroStatus, heroStatus.level);
    }

    public void SetInteractable(bool interactable)
    {
      this.m_uiIsInteractable = interactable;
      this.RefreshUsability();
    }

    public void SetValue(int value)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_counter)
        this.m_counter.SetValue(value);
      this.RefreshUsability();
    }

    public void ChangeValue(int value)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_counter)
      {
        int targetValue = this.m_counter.targetValue;
        if (targetValue < value)
          this.PlayActionPointsUp();
        else if (targetValue > value)
          this.PlayActionPointsDown();
        this.m_counter.ChangeValue(value);
      }
      this.RefreshUsability();
    }

    private void Awake()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_button)
        this.m_button.onClick.AddListener(new UnityAction(this.OnClick));
      this.RefreshUsability();
    }

    private void OnClick()
    {
      if (DragNDropListener.instance.dragging)
        return;
      UIManager instance = UIManager.instance;
      if ((UnityEngine.Object) null != (UnityEngine.Object) instance && instance.userInteractionLocked)
        return;
      Action reserveActivation = this.OnReserveActivation;
      if (reserveActivation == null)
        return;
      reserveActivation();
    }

    private void PlayActionPointsUp()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_paUpFx))
        return;
      this.m_paUpFx.gameObject.SetActive(true);
      this.m_paUpFx.Stop();
      this.m_paUpFx.Play();
    }

    private void PlayActionPointsDown()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_paDownFx))
        return;
      this.m_paDownFx.gameObject.SetActive(true);
      this.m_paDownFx.Stop();
      this.m_paDownFx.Play();
    }

    private void RefreshUsability()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_button))
        return;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_counter)
        this.m_button.interactable = this.m_uiIsInteractable && this.m_counter.targetValue > 0;
      else
        this.m_button.interactable = this.m_uiIsInteractable;
    }

    public void OnPointerEnter(PointerEventData eventData) => FightUIRework.ShowTooltip((ITooltipDataProvider) this, this.m_tooltipPosition, this.GetComponent<RectTransform>());

    public void OnPointerExit(PointerEventData eventData) => FightUIRework.HideTooltip();

    public void ShowPreview(int value, ValueModifier modifier)
    {
    }

    public void HidePreview(bool cancelled)
    {
    }

    public TooltipDataType tooltipDataType => TooltipDataType.Text;

    public int GetTitleKey() => !((UnityEngine.Object) null != (UnityEngine.Object) this.m_reserveDefinition) ? 0 : this.m_reserveDefinition.i18nNameId;

    public int GetDescriptionKey() => !((UnityEngine.Object) null != (UnityEngine.Object) this.m_reserveDefinition) ? 0 : this.m_reserveDefinition.i18nDescriptionId;

    public IFightValueProvider GetValueProvider() => (IFightValueProvider) this.m_fightValueProvider;

    public KeywordReference[] keywordReferences => !((UnityEngine.Object) null != (UnityEngine.Object) this.m_reserveDefinition) ? new KeywordReference[0] : this.m_reserveDefinition.precomputedData.keywordReferences;
  }
}
