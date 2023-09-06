// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.WeaponCellRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using Ankama.Cube.Utility;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace Ankama.Cube.UI.DeckMaker
{
  public class WeaponCellRenderer : 
    WithTooltipCellRenderer<WeaponData, IWeaponDataCellRendererConfigurator>,
    ICharacterTooltipDataProvider,
    ITooltipDataProvider
  {
    [SerializeField]
    private ImageLoader m_weaponImage;
    [SerializeField]
    private TextField m_levelTextField;
    private Tween m_animationTween;

    protected override void SetValue(WeaponData val)
    {
      if (val != null)
        this.m_valueProvider = (IFightValueProvider) new FightValueProvider((IDefinitionWithPrecomputedData) val.definition, val.level);
      this.SetLevel(val?.level);
      this.SetIllustration(val);
    }

    private void SetIllustration(WeaponData data)
    {
      if ((Object) this.m_weaponImage == (Object) null)
        return;
      WeaponDefinition definition = data?.definition;
      if ((Object) definition != (Object) null)
        this.m_weaponImage.Setup(definition.GetWeaponIllustrationReference(), AssetBundlesUtility.GetUICharacterResourcesBundleName());
      else
        this.m_weaponImage.Clear();
    }

    private void SetLevel(int? level)
    {
      if (!(bool) (Object) this.m_levelTextField)
        return;
      this.m_levelTextField.gameObject.SetActive(level.HasValue);
      this.m_levelTextField.SetText(68066, (IValueProvider) new IndexedValueProvider(new string[1]
      {
        level.ToString()
      }));
    }

    protected override void Clear()
    {
      this.SetIllustration((WeaponData) null);
      this.SetLevel(new int?());
      if (this.m_animationTween == null)
        return;
      this.m_animationTween.Kill();
      this.m_animationTween = (Tween) null;
    }

    public override void OnConfiguratorUpdate(bool instant)
    {
      base.OnConfiguratorUpdate(instant);
      Tween animationTween = this.m_animationTween;
      if (animationTween != null)
        animationTween.Kill();
      this.m_animationTween = (Tween) null;
      if (!(bool) (Object) this.m_weaponImage)
        return;
      IWeaponDataCellRendererConfigurator configurator = this.m_configurator;
      Color endValue = (configurator != null ? (configurator.IsWeaponDataAvailable(this.m_value) ? 1 : 0) : 1) != 0 ? new Color(1f, 1f, 1f) : new Color(1f, 0.0f, 1f);
      if (instant)
        this.m_weaponImage.color = endValue;
      else
        this.m_animationTween = (Tween) DOTween.To((DOGetter<Color>) (() => this.m_weaponImage.color), (DOSetter<Color>) (col => this.m_weaponImage.color = col), endValue, 0.2f);
    }

    public override TooltipDataType tooltipDataType => TooltipDataType.Character;

    public override int GetTitleKey() => this.m_value.definition.i18nNameId;

    public override int GetDescriptionKey() => this.m_value.definition.i18nDescriptionId;

    public override IFightValueProvider GetValueProvider() => this.m_valueProvider;

    public override KeywordReference[] keywordReferences => this.m_value.definition.precomputedData.keywordReferences;

    public ActionType GetActionType() => this.m_value.definition.actionType;

    public TooltipActionIcon GetActionIcon() => TooltipWindowUtility.GetActionIcon((CharacterDefinition) this.m_value.definition);

    public bool TryGetActionValue(out int actionValue)
    {
      actionValue = 0;
      if (this.m_value == null)
        return true;
      ILevelOnlyDependant actionValue1 = this.m_value.definition.actionValue;
      if (actionValue1 == null)
        return false;
      actionValue = actionValue1.GetValueWithLevel(this.m_value.level);
      return true;
    }

    public int GetLifeValue()
    {
      if (this.m_value == null)
        return 0;
      ILevelOnlyDependant life = this.m_value.definition.life;
      return life == null ? 0 : life.GetValueWithLevel(this.m_value.level);
    }

    public int GetMovementValue()
    {
      if (this.m_value == null)
        return 0;
      ILevelOnlyDependant movementPoints = this.m_value.definition.movementPoints;
      return movementPoints == null ? 0 : movementPoints.GetValueWithLevel(this.m_value.level);
    }
  }
}
