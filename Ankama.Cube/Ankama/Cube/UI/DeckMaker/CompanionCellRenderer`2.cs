// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.CompanionCellRenderer`2
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight;
using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI.DeckMaker
{
  public abstract class CompanionCellRenderer<T, U> : 
    WithTooltipCellRenderer<T, U>,
    ICharacterTooltipDataProvider,
    ITooltipDataProvider
    where U : ICompanionCellRendererConfigurator
  {
    [SerializeField]
    protected FightUIFactory m_factory;
    [SerializeField]
    protected ImageLoader m_companionImage;
    [SerializeField]
    protected GaugeItemUI[] m_elements;
    [SerializeField]
    protected CanvasGroup m_canvasGroup;
    private CompanionDefinitionContext m_context;
    private CompanionDefinition m_definition;
    private int m_level;
    private static readonly IReadOnlyList<Cost> s_noCost = (IReadOnlyList<Cost>) new List<Cost>();
    private bool m_usable;

    protected abstract IReadOnlyList<Cost> GetCosts();

    protected abstract bool IsAvailable();

    protected void SetValue(CompanionDefinition definition, int level)
    {
      CompanionDefinition definition1 = this.m_definition;
      this.m_definition = definition;
      this.m_level = level;
      if ((Object) definition != (Object) definition1)
      {
        this.SetIllustration(definition);
        this.SetCost();
        this.SetUsable(this.IsAvailable(), true, true);
      }
      if ((Object) definition != (Object) null)
      {
        this.m_valueProvider = (IFightValueProvider) new FightValueProvider((IDefinitionWithPrecomputedData) definition, level);
        this.m_context = new CompanionDefinitionContext(definition, level);
      }
      else
      {
        this.m_valueProvider = (IFightValueProvider) null;
        this.m_context = (CompanionDefinitionContext) null;
      }
    }

    protected override void Clear()
    {
      this.m_definition = (CompanionDefinition) null;
      this.SetIllustration((CompanionDefinition) null);
      this.SetCost();
      this.SetUsable(this.IsAvailable(), true, true);
    }

    private void SetIllustration(CompanionDefinition definition)
    {
      if ((Object) definition == (Object) null || !definition.illustrationReference.hasValue)
        this.m_companionImage.Clear();
      else
        this.m_companionImage.Setup(definition.illustrationReference, definition.illustrationBundleName);
    }

    private void SetCost() => this.SetElements(this.GetCosts() ?? CompanionCellRenderer<T, U>.s_noCost);

    private void SetElements(IReadOnlyList<Cost> costs)
    {
      int index1 = 0;
      int count = ((IReadOnlyCollection<Cost>) costs).Count;
      for (int index2 = 0; index2 < count; ++index2)
      {
        if (costs[index2] is ElementPointsCost cost)
        {
          int v;
          cost.value.GetValue((DynamicValueContext) this.m_context, out v);
          GaugeItemUI element = this.m_elements[index1];
          element.SetValue(v);
          this.m_factory.Initialize(element, cost.element);
          element.gameObject.SetActive(true);
          ++index1;
        }
      }
      int length = this.m_elements.Length;
      for (int index3 = index1; index3 < length; ++index3)
        this.m_elements[index3].gameObject.SetActive(false);
    }

    private void SetUsable(bool usable, bool instant = false, bool force = false)
    {
      if (!(this.m_usable != usable | force))
        return;
      this.m_usable = usable;
      Color endValue = usable ? new Color(1f, 1f, 1f, 1f) : new Color(0.5f, 0.25f, 1f, 0.6f);
      if (instant)
        this.m_companionImage.color = endValue;
      else
        DOTween.To((DOGetter<Color>) (() => this.m_companionImage.color), (DOSetter<Color>) (v => this.m_companionImage.color = v), endValue, 0.2f);
    }

    public override void OnConfiguratorUpdate(bool instant)
    {
      base.OnConfiguratorUpdate(instant);
      this.SetCost();
      this.SetUsable(this.IsAvailable(), instant);
    }

    public override Sequence DestroySequence()
    {
      Sequence s = DOTween.Sequence();
      s.Insert(0.0f, (Tween) this.transform.DOLocalMoveY(this.transform.localPosition.y + 40f, 0.25f).SetEase<Tweener>(Ease.OutSine));
      s.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(0.0f, 0.25f));
      return s;
    }

    public IEnumerator WaitForImage()
    {
      while (this.m_companionImage.loadState == UIResourceLoadState.Loading)
        yield return (object) null;
    }

    public override TooltipDataType tooltipDataType => TooltipDataType.Character;

    public override int GetTitleKey() => this.m_definition.i18nNameId;

    public override int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

    public override IFightValueProvider GetValueProvider() => this.m_valueProvider;

    public override KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;

    public ActionType GetActionType() => this.m_definition.actionType;

    public TooltipActionIcon GetActionIcon() => TooltipWindowUtility.GetActionIcon((CharacterDefinition) this.m_definition);

    public bool TryGetActionValue(out int val)
    {
      ILevelOnlyDependant actionValue = this.m_definition.actionValue;
      if (actionValue != null)
      {
        val = actionValue.GetValueWithLevel(this.m_level);
        return true;
      }
      val = 0;
      return false;
    }

    public int GetLifeValue() => this.m_definition.life.GetValueWithLevel(this.m_level);

    public int GetMovementValue() => this.m_definition.movementPoints.GetValueWithLevel(this.m_level);

    public override CellRenderer Clone()
    {
      CompanionCellRenderer<T, U> companionCellRenderer = (CompanionCellRenderer<T, U>) base.Clone();
      companionCellRenderer.m_definition = this.m_definition;
      companionCellRenderer.m_level = this.m_level;
      return (CellRenderer) companionCellRenderer;
    }
  }
}
