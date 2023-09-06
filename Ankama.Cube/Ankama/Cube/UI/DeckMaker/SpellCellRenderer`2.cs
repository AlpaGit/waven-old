// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.SpellCellRenderer`2
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
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.DeckMaker
{
  public abstract class SpellCellRenderer<T, U> : 
    WithTooltipCellRenderer<T, U>,
    ISpellTooltipDataProvider,
    ITooltipDataProvider
    where U : ISpellCellRendererConfigurator
  {
    [SerializeField]
    protected FightUIFactory m_factory;
    [SerializeField]
    protected ImageLoader m_spellImageLoader;
    [SerializeField]
    protected Image m_elementaryStateImage;
    [SerializeField]
    protected GameObject m_costGameObject;
    [SerializeField]
    protected UISpriteTextRenderer m_costValue;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    protected Image m_highlighted;
    private SpellDefinition m_definition;
    private bool m_usable;
    protected SpellDefinitionContext m_spellContext;

    protected abstract bool IsAvailable();

    protected abstract int? GetAPCost();

    protected abstract int? GetBaseAPCost();

    private void Awake()
    {
      if (!(bool) (Object) this.m_highlighted)
        return;
      this.m_highlighted.gameObject.SetActive(this.m_usable);
    }

    protected void SetValue(SpellDefinition definition, int level)
    {
      SpellDefinition definition1 = this.m_definition;
      this.m_definition = definition;
      SpellDefinition spellDefinition = definition;
      if ((Object) definition1 != (Object) spellDefinition)
      {
        this.SetIllustration(definition);
        this.SetAPCost(this.GetAPCost(), this.GetBaseAPCost());
        this.SetUsable(this.IsAvailable(), true, true);
        if ((Object) this.m_highlighted != (Object) null)
          this.m_highlighted.material = this.m_factory.GetSpellSelectedMaterial(definition);
      }
      if (!((Object) definition != (Object) null))
        return;
      this.m_valueProvider = (IFightValueProvider) new FightValueProvider((IDefinitionWithPrecomputedData) definition, level);
      this.m_spellContext = new SpellDefinitionContext(definition, level);
    }

    protected override void Clear()
    {
      this.m_definition = (SpellDefinition) null;
      this.SetIllustration((SpellDefinition) null);
      this.SetAPCost(new int?(), new int?());
      this.SetUsable(false, true, true);
    }

    private void SetIllustration(SpellDefinition definition)
    {
      this.m_elementaryStateImage.enabled = false;
      if ((Object) definition != (Object) null && definition.illustrationReference.hasValue)
      {
        this.m_spellImageLoader.Setup(definition.illustrationReference, definition.illustrationBundleName);
        this.m_factory.Initialize(this.m_elementaryStateImage, definition.elementaryStates);
      }
      else
        this.m_spellImageLoader.Clear();
    }

    protected void SetAPCost(int? cost, int? baseCost)
    {
      this.m_costGameObject.SetActive(cost.HasValue);
      if (!cost.HasValue)
        return;
      int num1 = cost.Value;
      this.m_costValue.text = num1.ToString();
      int num2 = num1;
      int? nullable = baseCost;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      Color color;
      if (num2 == valueOrDefault1 & nullable.HasValue)
      {
        color = Color.white;
      }
      else
      {
        int num3 = num1;
        nullable = baseCost;
        int valueOrDefault2 = nullable.GetValueOrDefault();
        color = !(num3 < valueOrDefault2 & nullable.HasValue) ? new Color(1f, 0.25f, 0.18f) : new Color(0.23f, 1f, 0.16f);
      }
      this.m_costValue.tint = color;
    }

    private void SetUsable(bool usable, bool instant = false, bool force = false)
    {
      if (!(this.m_usable != usable | force))
        return;
      this.m_usable = usable;
      Color endValue = usable ? new Color(1f, 1f, 1f, 1f) : new Color(0.5f, 0.25f, 1f, 0.6f);
      if (instant)
        this.m_spellImageLoader.color = endValue;
      else
        DOTween.To((DOGetter<Color>) (() => this.m_spellImageLoader.color), (DOSetter<Color>) (v => this.m_spellImageLoader.color = v), endValue, 0.2f);
      if (!(bool) (Object) this.m_highlighted)
        return;
      this.m_highlighted.gameObject.SetActive(usable);
    }

    public override void OnConfiguratorUpdate(bool instant)
    {
      base.OnConfiguratorUpdate(instant);
      this.SetUsable(this.IsAvailable(), instant);
      this.SetAPCost(this.GetAPCost(), this.GetBaseAPCost());
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
      while (this.m_spellImageLoader.loadState == UIResourceLoadState.Loading)
        yield return (object) null;
    }

    public override TooltipDataType tooltipDataType => TooltipDataType.Spell;

    public override int GetTitleKey() => this.m_definition.i18nNameId;

    public override int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

    public override IFightValueProvider GetValueProvider() => this.m_valueProvider;

    public override KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;

    public TooltipElementValues GetGaugeModifications() => TooltipWindowUtility.GetTooltipElementValues(this.m_definition, (DynamicValueContext) this.m_spellContext);

    public override CellRenderer Clone()
    {
      SpellCellRenderer<T, U> spellCellRenderer = (SpellCellRenderer<T, U>) base.Clone();
      spellCellRenderer.m_definition = this.m_definition;
      return (CellRenderer) spellCellRenderer;
    }
  }
}
