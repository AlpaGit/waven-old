// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.DeckUIRoot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Demo.UI;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Components.Tooltip;
using Ankama.Cube.UI.DeckMaker;
using Ankama.Cube.UI.Fight.Windows;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using DataEditor;
using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class DeckUIRoot : 
    AbstractUI,
    ISpellDataCellRendererConfigurator,
    ISpellCellRendererConfigurator,
    IWithTooltipCellRendererConfigurator,
    ICellRendererConfigurator,
    IWeaponDataCellRendererConfigurator
  {
    private const int m_equipWeapondTextID = 40340;
    private const int m_equippedWeapondTextID = 38149;
    private const int m_spellNameID = 33557;
    [Header("DeckPanel")]
    [SerializeField]
    private DeckPresetPanel m_presetPanel;
    [Header("SafePanel")]
    [SerializeField]
    private Transform m_safeArea;
    [Header("Buttons")]
    [SerializeField]
    private Button m_validateButton;
    [SerializeField]
    private TextField m_validateButtonText;
    [SerializeField]
    private Button m_editButton;
    [SerializeField]
    private Button m_createButton;
    [Header("Pedestal")]
    [SerializeField]
    private GameObject m_animatedCharacterRoot;
    [SerializeField]
    private EquippedFXControler m_equippedFX;
    [SerializeField]
    private ParticleSystem m_equipFXBtn;
    [SerializeField]
    private GameObjectLoader m_characterloader;
    [SerializeField]
    private WeaponCellRenderer m_pedestalWeaponCellRenderer;
    [SerializeField]
    private SpellDataCellRenderer m_pedestalSpellCellRenderer;
    [SerializeField]
    private RawTextField m_strengthField;
    [SerializeField]
    private RawTextField m_lifeField;
    [SerializeField]
    private RawTextField m_moveField;
    [Header("Weapons")]
    [SerializeField]
    private WeaponRibbonItem m_weaponUiPrefab;
    [SerializeField]
    private RectTransform m_weaponRibbon;
    [SerializeField]
    private RectTransform m_weaponContent;
    [SerializeField]
    private ScrollRect m_weaponScrollRect;
    [SerializeField]
    private GameObject m_weaponLeftArrow;
    [SerializeField]
    private GameObject m_weaponRightArrow;
    [Header("Weapon Ability and Spell")]
    [SerializeField]
    private TextField m_weaponTextField;
    [SerializeField]
    private TextField m_weaponLevelField;
    [SerializeField]
    private WeaponCellRenderer m_weaponVisual;
    [SerializeField]
    private TextField m_weaponPassiveText;
    [SerializeField]
    private SpellDataCellRenderer m_spellRenderer;
    [SerializeField]
    private Image m_spellVisual;
    [SerializeField]
    private TextField m_spellDescription;
    [SerializeField]
    private TextField m_spellName;
    [Header("Panel")]
    [SerializeField]
    private CanvasGroup m_safePanelCanvas;
    [SerializeField]
    private CanvasGroup m_weaponCanvas;
    [SerializeField]
    private CanvasGroup m_presetCanvas;
    [SerializeField]
    private CanvasGroup m_weaponListCanvas;
    [SerializeField]
    private CanvasGroup m_validateCanvas;
    [SerializeField]
    private CanvasGroup m_pedestalSpellAbilityBGCanvasGroup;
    [SerializeField]
    private CanvasGroup m_pedestalSpellAbilityMainCanvasGroup;
    [Header("Tooltips")]
    [SerializeField]
    private FightTooltip m_fightTooltip;
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    [SerializeField]
    private GenericTooltipWindow m_genericTooltipWindow;
    private int m_level = 1;
    private ImagePositionToShader m_backgroundShader;
    private WeaponDefinition m_currentWeapon;
    private List<WeaponRibbonItem> m_ribbonItems;
    private RectTransform m_pedestalSpellAbilityBgRectTransform;
    private RectTransform m_pedestalSpellAbilityMainRectTransform;
    private WeaponAndDeckModifications m_modifications;
    private DeckSlot m_emptySlot;
    private bool m_enterAnimationFinished;
    private Sequence m_pedestalTweenSequence;

    public DeckSlot selectedSlot => this.m_presetPanel.GetSelectedDeck();

    public event Action<DeckSlot> OnEditRequest;

    public event Action OnGotoEditAnimComplete;

    public event Action<int> OnEquipWeaponRequest;

    public event Action<DeckSlot> OnSelectDeckForWeaponRequest;

    protected override void Awake()
    {
      base.Awake();
      this.m_equippedFX.gameObject.SetActive(false);
      this.m_pedestalWeaponCellRenderer.SetConfigurator((ICellRendererConfigurator) this, true);
      this.m_pedestalSpellCellRenderer.SetConfigurator((ICellRendererConfigurator) this, true);
      this.m_spellRenderer.SetConfigurator((ICellRendererConfigurator) this, true);
      this.m_weaponVisual.SetConfigurator((ICellRendererConfigurator) this, true);
      this.m_pedestalSpellAbilityMainCanvasGroup.alpha = 0.0f;
      this.m_pedestalSpellAbilityMainCanvasGroup.gameObject.SetActive(false);
      this.m_pedestalSpellAbilityBGCanvasGroup.alpha = 0.0f;
      this.m_pedestalSpellAbilityBGCanvasGroup.gameObject.SetActive(false);
      this.m_pedestalSpellAbilityBgRectTransform = this.m_pedestalSpellAbilityBGCanvasGroup.GetComponent<RectTransform>();
      this.m_pedestalSpellAbilityMainRectTransform = this.m_pedestalSpellAbilityMainCanvasGroup.GetComponent<RectTransform>();
      this.m_pedestalSpellAbilityBgRectTransform.anchoredPosition = new Vector2(0.0f, 30f);
      this.m_pedestalSpellAbilityMainRectTransform.anchoredPosition = new Vector2(0.0f, 30f);
      this.m_presetPanel.OnSelectionChange += new Action<DeckSlot>(this.SelectDeckForWeapon);
      this.m_validateButton.onClick.AddListener(new UnityAction(this.ValidateSelection));
      this.m_createButton.onClick.AddListener(new UnityAction(this.CreateDeckForWeapon));
      this.m_editButton.onClick.AddListener(new UnityAction(this.EditDeck));
    }

    public void Initialise(WeaponAndDeckModifications modifications)
    {
      this.m_modifications = modifications;
      Vector2 sizeDelta = this.canvas.GetComponent<RectTransform>().sizeDelta;
      sizeDelta.x *= 0.7f;
      sizeDelta.y = this.m_weaponRibbon.sizeDelta.y;
      this.m_weaponRibbon.sizeDelta = sizeDelta;
      this.m_ribbonItems = new List<WeaponRibbonItem>();
      this.m_backgroundShader = GameObject.FindGameObjectWithTag("DeckSelection_BG").GetComponent<ImagePositionToShader>();
      this.m_presetPanel.InitialiseUI(this.m_modifications, new Action<DeckSlot>(this.OpenDeckEditState));
      this.m_safePanelCanvas.alpha = 0.0f;
      this.m_weaponCanvas.alpha = 0.0f;
      this.m_presetCanvas.alpha = 0.0f;
    }

    public IEnumerator BuildWeaponList(List<WeaponDefinition> weapons)
    {
      DeckUIRoot deckUiRoot = this;
      if (deckUiRoot.m_ribbonItems == null)
        deckUiRoot.m_ribbonItems = new List<WeaponRibbonItem>();
      yield return (object) null;
      int selectedWeapon = deckUiRoot.m_modifications.GetSelectedWeapon();
      for (int index = 0; index < weapons.Count; ++index)
      {
        WeaponDefinition weapon = weapons[index];
        WeaponRibbonItem weaponRibbonItem;
        if (deckUiRoot.m_ribbonItems.Count > index)
        {
          weaponRibbonItem = deckUiRoot.m_ribbonItems[index];
        }
        else
        {
          weaponRibbonItem = UnityEngine.Object.Instantiate<WeaponRibbonItem>(deckUiRoot.m_weaponUiPrefab, deckUiRoot.m_weaponUiPrefab.transform.parent);
          deckUiRoot.m_ribbonItems.Add(weaponRibbonItem);
        }
        weaponRibbonItem.gameObject.SetActive(true);
        weaponRibbonItem.Initialise(weapon);
        weaponRibbonItem.OnSelected += new Action<WeaponDefinition>(deckUiRoot.OnWeaponSelected);
        weaponRibbonItem.SetSelected(selectedWeapon, true);
        weaponRibbonItem.SetEquipped(deckUiRoot.m_currentWeapon);
      }
      deckUiRoot.m_weaponUiPrefab.gameObject.SetActive(false);
    }

    private bool IsCurrentWeapon(int weaponId) => this.m_modifications.GetSelectedWeapon() == weaponId;

    private void OnWeaponSelected(WeaponDefinition definition) => this.DisplayWeapon(definition);

    public void DisplayWeapon(WeaponDefinition definition)
    {
      foreach (BaseRibbonItem<WeaponDefinition> ribbonItem in this.m_ribbonItems)
        ribbonItem.SetSelected(definition);
      this.StartCoroutine(this.DisplayWeaponEnumerator(definition));
    }

    private IEnumerator DisplayWeaponEnumerator(WeaponDefinition definition)
    {
      if (!((UnityEngine.Object) this.m_currentWeapon == (UnityEngine.Object) definition))
      {
        this.m_currentWeapon = definition;
        bool flag1 = PlayerData.instance.weaponInventory.Contains<int>(definition.id);
        bool flag2 = !this.IsCurrentWeapon(definition.id) & flag1;
        this.m_validateButton.interactable = flag2;
        this.m_validateButtonText.SetText(flag2 ? 40340 : 38149);
        PlayerData.instance.weaponInventory.TryGetLevel(this.m_currentWeapon.id, out this.m_level);
        if (this.m_enterAnimationFinished)
          yield return (object) this.PlayFadeSequence(true);
        yield return (object) this.LoadWeaponInfos(definition);
        this.BuildDeckList();
        this.m_backgroundShader.TweenColor(definition.deckBuildingBackgroundColor, definition.deckBuildingBackgroundColor2, 0.2f);
        AssetReference characterReference = definition.GetUIAnimatedCharacterReference();
        CanvasGroup characterGroup = this.m_characterloader.GetComponent<CanvasGroup>();
        characterGroup.alpha = 0.0f;
        this.m_characterloader.Setup(characterReference, AssetBundlesUtility.GetUIAnimatedCharacterResourcesBundleName());
        while (this.m_characterloader.loadState == UIResourceLoadState.Loading)
          yield return (object) null;
        characterGroup.DOFade(1f, 0.3f);
        this.m_equippedFX.SetEquipped(this.IsCurrentWeapon(definition.id));
        if (this.m_enterAnimationFinished)
          yield return (object) this.PlayFadeSequence(false);
      }
    }

    private IEnumerator PlayFadeSequence(bool toOut)
    {
      DeckUIRoot deckUiRoot = this;
      if (toOut)
        yield return (object) deckUiRoot.PlayAnimation(deckUiRoot.m_animationDirector.GetAnimation("FadeOut"));
      else
        yield return (object) deckUiRoot.PlayAnimation(deckUiRoot.m_animationDirector.GetAnimation("FadeIn"));
    }

    public void BuildDeckList()
    {
      this.m_emptySlot = (DeckSlot) null;
      List<DeckSlot> deckSlots = this.CreateDeckSlots();
      int index = 0;
      for (int count = deckSlots.Count; index < count; ++index)
      {
        if (deckSlots[index].isAvailableEmptyDeckSlot)
        {
          this.m_emptySlot = deckSlots[index];
          break;
        }
      }
      this.m_createButton.interactable = this.m_emptySlot != null;
      this.m_presetPanel.Populate(deckSlots, this.m_currentWeapon);
    }

    private IEnumerator LoadWeaponInfos(WeaponDefinition definition)
    {
      AssetReference illustrationReference = definition.GetWeaponIllustrationReference();
      this.m_weaponTextField.SetText(definition.i18nNameId);
      this.m_weaponLevelField.SetText(68066, (IValueProvider) new IndexedValueProvider(new string[1]
      {
        this.m_level.ToString()
      }));
      AssetLoadRequest<Sprite> assetReferenceRequest = illustrationReference.LoadFromAssetBundleAsync<Sprite>(AssetBundlesUtility.GetUICharacterResourcesBundleName());
      while (!assetReferenceRequest.isDone)
        yield return (object) null;
      if ((int) assetReferenceRequest.error != 0)
      {
        Log.Error(string.Format("Error while loading illustration for {0} {1} error={2}", (object) definition.GetType().Name, (object) definition.name, (object) assetReferenceRequest.error), 307, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Player\\DeckRoot\\DeckUIRoot.cs");
      }
      else
      {
        WeaponData weaponData = new WeaponData(definition, this.m_level);
        this.m_weaponVisual.SetValue((object) weaponData);
        this.m_weaponPassiveText.SetText(definition.i18nDescriptionId, (IValueProvider) new FightValueProvider((IDefinitionWithPrecomputedData) definition, this.m_level));
        this.m_pedestalWeaponCellRenderer.SetValue((object) weaponData);
        if ((UnityEngine.Object) definition != (UnityEngine.Object) null)
        {
          List<Id<SpellDefinition>> list = ((IEnumerable<Id<SpellDefinition>>) definition.spells).ToList<Id<SpellDefinition>>();
          SpellDefinition definition1;
          if (list.Count != 0 && RuntimeData.spellDefinitions.TryGetValue(list[0].value, out definition1))
          {
            SpellData spellData = new SpellData(definition1, this.m_level);
            this.m_pedestalSpellCellRenderer.SetValue((object) spellData);
            this.m_equippedFX.SetElement(definition1.element);
            this.m_spellRenderer.SetValue((object) new SpellData(definition1, this.m_level));
            this.m_spellName.SetText(33557, (IValueProvider) new IndexedValueProvider(new string[1]
            {
              RuntimeData.FormattedText(spellData.definition.i18nNameId, (IValueProvider) null)
            }));
            this.m_spellDescription.SetText(spellData.definition.i18nDescriptionId, (IValueProvider) new FightValueProvider((IDefinitionWithPrecomputedData) spellData.definition, this.m_level));
            int valueWithLevel1 = definition.movementPoints.GetValueWithLevel(this.m_level);
            int valueWithLevel2 = definition.life.GetValueWithLevel(this.m_level);
            this.m_strengthField.SetText(definition.actionValue.GetValueWithLevel(this.m_level).ToString());
            this.m_lifeField.SetText(valueWithLevel2.ToString());
            this.m_moveField.SetText(valueWithLevel1.ToString());
          }
        }
      }
    }

    private void OnSpellDataReceived(Sprite sprite, string loadedBundleName) => this.m_spellVisual.sprite = sprite;

    public IEnumerator PlayEnterAnimation(List<WeaponDefinition> weapons)
    {
      DeckUIRoot deckUiRoot = this;
      yield return (object) deckUiRoot.BuildWeaponList(weapons);
      yield return (object) new WaitForEndOfFrame();
      if ((double) deckUiRoot.m_weaponContent.sizeDelta.x < (double) deckUiRoot.m_weaponRibbon.sizeDelta.x)
      {
        Vector2 sizeDelta = deckUiRoot.m_weaponContent.sizeDelta;
        sizeDelta.x += 10f;
        sizeDelta.y = deckUiRoot.m_weaponRibbon.sizeDelta.y;
        deckUiRoot.m_weaponRibbon.sizeDelta = sizeDelta;
        Vector3 localPosition = deckUiRoot.m_weaponContent.transform.localPosition with
        {
          x = 0.0f
        };
        deckUiRoot.m_weaponContent.transform.localPosition = localPosition;
        deckUiRoot.m_weaponScrollRect.enabled = false;
        deckUiRoot.m_weaponLeftArrow.SetActive(false);
        deckUiRoot.m_weaponRightArrow.SetActive(false);
      }
      yield return (object) deckUiRoot.PlayAnimation(deckUiRoot.m_animationDirector.GetAnimation("Open"));
      deckUiRoot.m_enterAnimationFinished = true;
    }

    public IEnumerator CloseUI()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      DeckUIRoot deckUiRoot = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      deckUiRoot.m_presetPanel.Unload();
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      DOTween.To(new DOGetter<float>(deckUiRoot.\u003CCloseUI\u003Eb__77_0), new DOSetter<float>(deckUiRoot.\u003CCloseUI\u003Eb__77_1), 0.0f, 0.25f);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) deckUiRoot.PlayAnimation(deckUiRoot.m_animationDirector.GetAnimation("Close"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public void Close() => PlayerIconRoot.instance.ReducePanel();

    private void OpenDeckEditState(DeckSlot slot)
    {
      Action<DeckSlot> onEditRequest = this.OnEditRequest;
      if (onEditRequest == null)
        return;
      onEditRequest(slot);
    }

    public void GotoEditAnim()
    {
      this.StartCoroutine(this.PlayFadeSequence(true));
      this.DisplayPedestalInfo(true);
      Sequence sequence = DOTween.Sequence();
      sequence.Append((Tween) this.m_weaponListCanvas.DOFade(0.0f, 0.3f));
      sequence.Insert(0.0f, (Tween) this.m_validateCanvas.DOFade(0.0f, 0.3f));
      sequence.OnKill<Sequence>((TweenCallback) (() =>
      {
        Action editAnimComplete = this.OnGotoEditAnimComplete;
        if (editAnimComplete != null)
          editAnimComplete();
        this.interactable = false;
      }));
    }

    public void BackFromEditAnim()
    {
      this.interactable = true;
      this.StartCoroutine(this.PlayAnimation(this.m_animationDirector.GetAnimation("BackFromEdition")));
      this.DisplayPedestalInfo(false);
      this.BuildDeckList();
    }

    public void EditDeck() => this.OpenDeckEditState(this.selectedSlot);

    private void CreateDeckForWeapon()
    {
      if (this.m_emptySlot == null)
        return;
      this.OpenDeckEditState(this.m_emptySlot);
    }

    private List<DeckSlot> CreateDeckSlots()
    {
      List<DeckSlot> deckSlots = new List<DeckSlot>();
      God god = PlayerData.instance.god;
      int id = this.m_currentWeapon.id;
      SquadDefinition definition;
      if (RuntimeData.squadDefinitions.TryGetValue(this.m_currentWeapon.defaultDeck.value, out definition))
      {
        DeckInfo deckInfo = definition.ToDeckInfo();
        deckInfo.Id = new int?(-definition.id);
        deckSlots.Add(new DeckSlot(deckInfo.FillEmptySlotsCopy(), true));
      }
      foreach (DeckInfo deck in PlayerData.instance.GetDecks())
      {
        if ((God) deck.God == god && deck.Weapon == id)
        {
          deckSlots.Add(new DeckSlot(deck.Clone().FillEmptySlotsCopy()));
          if (deckSlots.Count >= 4)
            break;
        }
      }
      int count = deckSlots.Count;
      for (int index = 4; count < index; ++count)
      {
        DeckInfo deckInfo = new DeckInfo().FillEmptySlotsCopy();
        deckInfo.Name = RuntimeData.FormattedText(92537, (IValueProvider) null);
        deckInfo.God = (int) god;
        deckInfo.Weapon = id;
        deckSlots.Add(new DeckSlot(deckInfo));
      }
      return deckSlots;
    }

    public void ValidateSelection()
    {
      Action<int> equipWeaponRequest = this.OnEquipWeaponRequest;
      if (equipWeaponRequest != null)
        equipWeaponRequest(this.m_currentWeapon.id);
      this.m_equipFXBtn.Play();
    }

    public void SelectDeckForWeapon(DeckSlot slot)
    {
      Action<DeckSlot> forWeaponRequest = this.OnSelectDeckForWeaponRequest;
      if (forWeaponRequest == null)
        return;
      forWeaponRequest(slot);
    }

    public void OnEquippedDeckUpdate() => this.m_presetPanel.OnEquippedDeckUpdate();

    public void OnSelectedWeaponUpdate()
    {
      foreach (BaseRibbonItem<WeaponDefinition> ribbonItem in this.m_ribbonItems)
        ribbonItem.SetEquipped(this.m_currentWeapon);
      bool flag = !this.IsCurrentWeapon(this.m_currentWeapon.id);
      this.m_validateButton.interactable = flag;
      this.m_validateButtonText.SetText(flag ? 40340 : 38149);
      this.m_equippedFX.SetEquipped(this.IsCurrentWeapon(this.m_currentWeapon.id));
    }

    public FightTooltip tooltip => this.m_fightTooltip;

    public TooltipPosition tooltipPosition => this.m_tooltipPosition;

    public bool IsWeaponDataAvailable(WeaponData data) => true;

    public bool IsSpellDataAvailable(SpellData data) => true;

    private void DisplayPedestalInfo(bool display)
    {
      Sequence pedestalTweenSequence = this.m_pedestalTweenSequence;
      if (pedestalTweenSequence != null)
        pedestalTweenSequence.Kill();
      Sequence sequence = DOTween.Sequence();
      if (display)
      {
        this.m_pedestalSpellAbilityBGCanvasGroup.gameObject.SetActive(true);
        this.m_pedestalSpellAbilityMainCanvasGroup.gameObject.SetActive(true);
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityMainCanvasGroup.DOFade(1f, 0.2f));
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityBGCanvasGroup.DOFade(1f, 0.2f));
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityBgRectTransform.DOAnchorPos(Vector2.zero, 0.2f).SetEase<Tweener>(Ease.InOutQuad));
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityMainRectTransform.DOAnchorPos(Vector2.zero, 0.2f).SetEase<Tweener>(Ease.InOutQuad));
      }
      else
      {
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityMainCanvasGroup.DOFade(0.0f, 0.2f));
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityBGCanvasGroup.DOFade(0.0f, 0.2f));
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityBgRectTransform.DOAnchorPos(new Vector2(0.0f, 30f), 0.2f).SetEase<Tweener>(Ease.InOutQuad));
        sequence.Insert(0.0f, (Tween) this.m_pedestalSpellAbilityMainRectTransform.DOAnchorPos(new Vector2(0.0f, 30f), 0.2f).SetEase<Tweener>(Ease.InOutQuad));
        sequence.OnKill<Sequence>(new TweenCallback(this.HidePedestalInfos));
      }
      this.m_pedestalTweenSequence = sequence;
    }

    private void HidePedestalInfos()
    {
      this.m_pedestalSpellAbilityBGCanvasGroup.gameObject.SetActive(false);
      this.m_pedestalSpellAbilityMainCanvasGroup.gameObject.SetActive(false);
      this.m_fightTooltip.Hide();
    }

    public int GetCurrentWeaponID() => this.m_currentWeapon.id;
  }
}
