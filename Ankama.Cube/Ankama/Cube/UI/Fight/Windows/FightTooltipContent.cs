// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Windows.FightTooltipContent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.Windows
{
  [Serializable]
  public struct FightTooltipContent
  {
    [SerializeField]
    private GameObject m_iconsContainer;
    [Header("Texts")]
    [SerializeField]
    private TextField m_title;
    [SerializeField]
    private TextField m_description;
    [Header("Spell Elements")]
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_air;
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_earth;
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_fire;
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_water;
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_reserve;
    [Header("Character Properties")]
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_action;
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_life;
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_movement;
    [Header("Mechanism Properties")]
    [SerializeField]
    private FightTooltipContent.PropertyIcon m_armor;
    [Header("Action Icons")]
    [SerializeField]
    private GameObject m_closeCombatAttackIcon;
    [SerializeField]
    private GameObject m_rangedAttackIcon;
    [SerializeField]
    private GameObject m_healIcon;

    public void Setup()
    {
      this.m_title.SetText(0);
      this.m_description.SetText(0);
      this.SetCharacterPropertiesVisibility(false);
      this.SetSpellElementsVisibility(false);
    }

    public void Initialize([NotNull] ITooltipDataProvider dataProvider)
    {
      IValueProvider valueProvider = (IValueProvider) dataProvider.GetValueProvider();
      this.m_title.SetText(dataProvider.GetTitleKey(), valueProvider);
      this.m_description.SetText(dataProvider.GetDescriptionKey(), valueProvider);
      switch (dataProvider.tooltipDataType)
      {
        case TooltipDataType.Character:
          this.SetCharacterPropertiesVisibility(true);
          this.SetMechanismPropertiesVisibility(false);
          this.SetSpellElementsVisibility(false);
          this.SetIconsVisibility(true);
          this.InitializeProperties((ICharacterTooltipDataProvider) dataProvider);
          break;
        case TooltipDataType.ObjectMechanism:
          this.SetCharacterPropertiesVisibility(false);
          this.SetMechanismPropertiesVisibility(true);
          this.SetSpellElementsVisibility(false);
          this.SetIconsVisibility(true);
          this.InitializeProperties((IObjectMechanismTooltipDataProvider) dataProvider);
          break;
        case TooltipDataType.FloorMechanism:
          this.SetCharacterPropertiesVisibility(false);
          this.SetMechanismPropertiesVisibility(false);
          this.SetSpellElementsVisibility(false);
          this.SetIconsVisibility(false);
          break;
        case TooltipDataType.Spell:
          this.SetCharacterPropertiesVisibility(false);
          this.SetMechanismPropertiesVisibility(false);
          this.SetSpellElementsVisibility(true);
          this.SetIconsVisibility(true);
          this.InitializeProperties((ISpellTooltipDataProvider) dataProvider);
          break;
        case TooltipDataType.Text:
          this.SetCharacterPropertiesVisibility(false);
          this.SetMechanismPropertiesVisibility(false);
          this.SetSpellElementsVisibility(false);
          this.SetIconsVisibility(false);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void InitializeProperties(ICharacterTooltipDataProvider dataProvider)
    {
      TooltipActionIcon actionIcon = dataProvider.GetActionIcon();
      this.m_closeCombatAttackIcon.SetActive(actionIcon == TooltipActionIcon.AttackCloseCombat);
      this.m_rangedAttackIcon.SetActive(actionIcon == TooltipActionIcon.AttackRanged);
      this.m_healIcon.SetActive(actionIcon == TooltipActionIcon.Heal);
      switch (dataProvider.GetActionType())
      {
        case ActionType.None:
          this.m_action.SetActive(false);
          break;
        case ActionType.Attack:
          int num1;
          if (dataProvider.TryGetActionValue(out num1))
          {
            this.m_action.SetValue(num1);
            break;
          }
          break;
        case ActionType.Heal:
          int num2;
          if (dataProvider.TryGetActionValue(out num2))
          {
            this.m_action.SetValue(num2);
            break;
          }
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.m_life.SetValue(dataProvider.GetLifeValue());
      this.m_movement.SetValue(dataProvider.GetMovementValue());
    }

    private void InitializeProperties(IObjectMechanismTooltipDataProvider dataProvider) => this.m_armor.SetValue(dataProvider.GetArmorValue());

    private void InitializeProperties(ISpellTooltipDataProvider dataProvider)
    {
      TooltipElementValues gaugeModifications = dataProvider.GetGaugeModifications();
      this.m_air.SetModificationValue(gaugeModifications.air);
      this.m_earth.SetModificationValue(gaugeModifications.earth);
      this.m_fire.SetModificationValue(gaugeModifications.fire);
      this.m_water.SetModificationValue(gaugeModifications.water);
      this.m_reserve.SetModificationValue(gaugeModifications.reserve);
    }

    private void SetSpellElementsVisibility(bool value)
    {
      this.m_air.SetActive(value);
      this.m_earth.SetActive(value);
      this.m_fire.SetActive(value);
      this.m_water.SetActive(value);
      this.m_reserve.SetActive(value);
    }

    private void SetCharacterPropertiesVisibility(bool value)
    {
      this.m_action.SetActive(value);
      this.m_life.SetActive(value);
      this.m_movement.SetActive(value);
    }

    private void SetMechanismPropertiesVisibility(bool value) => this.m_armor.SetActive(value);

    private void SetIconsVisibility(bool value) => this.m_iconsContainer.SetActive(value);

    [Serializable]
    private struct PropertyIcon
    {
      [SerializeField]
      private GameObject m_imageContainer;
      [SerializeField]
      private GameObject m_textContainer;
      [SerializeField]
      private Image m_image;
      [SerializeField]
      private UISpriteTextRenderer m_spriteTextRenderer;

      public void SetActive(bool value)
      {
        this.m_imageContainer.SetActive(value);
        this.m_textContainer.SetActive(value);
      }

      public void SetValue(int value) => this.m_spriteTextRenderer.text = value.ToString();

      public void SetModificationValue(int value)
      {
        if (value == 0)
        {
          this.m_image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
          this.m_spriteTextRenderer.enabled = false;
        }
        else
        {
          this.m_image.color = new Color(1f, 1f, 1f, 1f);
          this.m_spriteTextRenderer.text = value.ToStringSigned();
          this.m_spriteTextRenderer.enabled = true;
        }
      }
    }
  }
}
