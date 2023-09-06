// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.GodSelection.GodSelectionRoot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Data;
using Ankama.Cube.Demo.UI;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Components.Tooltip;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.GodSelection
{
  public class GodSelectionRoot : AbstractUI
  {
    public Action<God> onGodSelected;
    public Action onCloseClick;
    [Header("Button")]
    [SerializeField]
    private Button m_closeButton;
    [SerializeField]
    private Button m_validateButton;
    [Header("Visual")]
    [SerializeField]
    private CanvasGroup m_bgCanvas;
    [SerializeField]
    private ImageLoader m_illuLoader;
    [SerializeField]
    private ImageLoader m_statueLoader;
    [SerializeField]
    private ImagePositionToShader m_backgroundShader;
    [Header("Canvas")]
    [SerializeField]
    private CanvasGroup m_safePanelCanvas;
    [SerializeField]
    private CanvasGroup m_godListCanvas;
    [Header("Tooltips")]
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    [SerializeField]
    private GenericTooltipWindow m_genericTooltipWindow;
    [Header("Info")]
    [SerializeField]
    private TextField m_godName;
    [SerializeField]
    private TextField m_godDescription;
    [Header("BGColor")]
    [SerializeField]
    private Color m_BGColor;
    [SerializeField]
    private Color m_BGColorBorder;
    [Header("Visual")]
    [SerializeField]
    private ParticleSystem m_changeGodFX;
    [SerializeField]
    private ParticleSystem m_equipeGodFX;
    [Header("God")]
    [SerializeField]
    private GodSelectionRibbonItem m_godUiPrefab;
    [SerializeField]
    private RectTransform m_godRibbon;
    [SerializeField]
    private RectTransform m_godContent;
    [SerializeField]
    private ScrollRect m_godScrollRect;
    [SerializeField]
    private GameObject m_godLeftArrow;
    [SerializeField]
    private GameObject m_godRightArrow;
    [Header("Sounds")]
    [SerializeField]
    private UnityEvent m_onGodChange;
    private readonly List<GodDefinition> m_playableGods = new List<GodDefinition>();
    private List<GodSelectionRibbonItem> m_ribbonItems;
    private GodDefinition m_currentGod;
    private bool m_building;

    protected override void Awake()
    {
      this.m_bgCanvas.alpha = 0.0f;
      base.Awake();
      this.m_genericTooltipWindow.gameObject.SetActive(false);
      this.m_godUiPrefab.gameObject.SetActive(false);
    }

    public void Initialise()
    {
      this.m_safePanelCanvas.alpha = 0.0f;
      this.m_closeButton.onClick.AddListener(new UnityAction(this.OnCloseClick));
      this.m_validateButton.onClick.AddListener(new UnityAction(this.OnEquipGod));
      this.m_backgroundShader.SetColor(this.m_BGColor, this.m_BGColorBorder);
    }

    private void OnCloseClick()
    {
      Action onCloseClick = this.onCloseClick;
      if (onCloseClick == null)
        return;
      onCloseClick();
    }

    private IEnumerator BuildGodList()
    {
      GodSelectionRoot godSelectionRoot = this;
      godSelectionRoot.m_building = true;
      godSelectionRoot.m_playableGods.Clear();
      God god = PlayerData.instance != null ? PlayerData.instance.god : God.Iop;
      foreach (GodDefinition godDefinition in RuntimeData.godDefinitions.Values)
      {
        if (godDefinition.playable)
          godSelectionRoot.m_playableGods.Add(godDefinition);
      }
      godSelectionRoot.m_playableGods.Sort((IComparer<GodDefinition>) new GodSelectionRoot.GodComparer());
      int num = 0;
      GodDefinition godDefinition1 = (GodDefinition) null;
      foreach (GodDefinition playableGod in godSelectionRoot.m_playableGods)
      {
        if (playableGod.god == god)
        {
          godDefinition1 = playableGod;
          break;
        }
        ++num;
      }
      if (godSelectionRoot.m_ribbonItems == null)
        godSelectionRoot.m_ribbonItems = new List<GodSelectionRibbonItem>();
      for (int index = 0; index < godSelectionRoot.m_playableGods.Count; ++index)
      {
        GodDefinition playableGod = godSelectionRoot.m_playableGods[index];
        GodSelectionRibbonItem selectionRibbonItem;
        if (godSelectionRoot.m_ribbonItems.Count > index)
        {
          selectionRibbonItem = godSelectionRoot.m_ribbonItems[index];
        }
        else
        {
          selectionRibbonItem = UnityEngine.Object.Instantiate<GodSelectionRibbonItem>(godSelectionRoot.m_godUiPrefab, godSelectionRoot.m_godUiPrefab.transform.parent);
          godSelectionRoot.m_ribbonItems.Add(selectionRibbonItem);
        }
        selectionRibbonItem.gameObject.SetActive(true);
        selectionRibbonItem.Initialise(playableGod);
        selectionRibbonItem.OnSelected += new Action<GodDefinition>(godSelectionRoot.OnGodSelected);
        selectionRibbonItem.SetSelected(godDefinition1, true);
        selectionRibbonItem.SetEquipped(godDefinition1);
      }
      godSelectionRoot.m_building = false;
      godSelectionRoot.DisplayGod(godDefinition1);
      while (godSelectionRoot.IsAnyGodLoading())
        yield return (object) null;
    }

    private void OnGodSelected(GodDefinition godDefinition) => this.DisplayGod(godDefinition);

    private bool IsAnyGodLoading()
    {
      foreach (BaseRibbonItem<GodDefinition> ribbonItem in this.m_ribbonItems)
      {
        if (ribbonItem.loadState == UIResourceLoadState.Loading)
          return true;
      }
      return false;
    }

    public IEnumerator PlayEnterAnimation()
    {
      GodSelectionRoot godSelectionRoot = this;
      godSelectionRoot.m_bgCanvas.DOFade(1f, 0.3f);
      yield return (object) godSelectionRoot.BuildGodList();
      yield return (object) new WaitForEndOfFrame();
      if ((double) godSelectionRoot.m_godContent.sizeDelta.x < (double) godSelectionRoot.m_godRibbon.sizeDelta.x)
      {
        Vector2 sizeDelta = godSelectionRoot.m_godContent.sizeDelta;
        sizeDelta.x += 10f;
        sizeDelta.y = godSelectionRoot.m_godRibbon.sizeDelta.y;
        godSelectionRoot.m_godRibbon.sizeDelta = sizeDelta;
        Vector3 localPosition = godSelectionRoot.m_godContent.transform.localPosition with
        {
          x = 0.0f
        };
        godSelectionRoot.m_godContent.transform.localPosition = localPosition;
        godSelectionRoot.m_godScrollRect.enabled = false;
        godSelectionRoot.m_godLeftArrow.SetActive(false);
        godSelectionRoot.m_godRightArrow.SetActive(false);
      }
      yield return (object) godSelectionRoot.PlayAnimation(godSelectionRoot.m_animationDirector.GetAnimation("Open"));
    }

    public void DisplayGod(GodDefinition definition)
    {
      if (this.m_building || (UnityEngine.Object) this.m_currentGod == (UnityEngine.Object) definition)
        return;
      this.m_currentGod = definition;
      foreach (BaseRibbonItem<GodDefinition> ribbonItem in this.m_ribbonItems)
        ribbonItem.SetSelected(this.m_currentGod);
      this.StartCoroutine(this.AppearRoutine(definition));
      this.m_godName.SetText(definition.i18nNameId);
      this.m_godDescription.SetText(definition.i18nDescriptionId);
      this.m_validateButton.interactable = PlayerData.instance.god != definition.god;
    }

    private IEnumerator AppearRoutine(GodDefinition definition)
    {
      GodSelectionRoot godSelectionRoot = this;
      godSelectionRoot.m_statueLoader.color = new Color(1f, 1f, 1f, 0.0f);
      godSelectionRoot.m_illuLoader.color = new Color(1f, 1f, 1f, 0.0f);
      AssetReference uiStatueReference = definition.GetUIStatueReference();
      godSelectionRoot.m_statueLoader.Setup(uiStatueReference, AssetBundlesUtility.GetUIGodsResourcesBundleName());
      AssetReference uibgReference = definition.GetUIBGReference();
      godSelectionRoot.m_illuLoader.Setup(uibgReference, AssetBundlesUtility.GetUIGodsResourcesBundleName());
      while (godSelectionRoot.m_statueLoader.loadState == UIResourceLoadState.Loading && godSelectionRoot.m_illuLoader.loadState == UIResourceLoadState.Loading)
        yield return (object) null;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      DOTween.To(new DOGetter<Color>(godSelectionRoot.\u003CAppearRoutine\u003Eb__37_0), new DOSetter<Color>(godSelectionRoot.\u003CAppearRoutine\u003Eb__37_1), Color.white, 0.25f);
      godSelectionRoot.m_onGodChange.Invoke();
      yield return (object) new WaitForTime(0.1f);
      godSelectionRoot.m_changeGodFX.Play();
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      DOTween.To(new DOGetter<Color>(godSelectionRoot.\u003CAppearRoutine\u003Eb__37_2), new DOSetter<Color>(godSelectionRoot.\u003CAppearRoutine\u003Eb__37_3), Color.white, 0.5f);
    }

    private void OnEquipGod()
    {
      this.onGodSelected(this.m_currentGod.god);
      this.m_validateButton.interactable = false;
      foreach (BaseRibbonItem<GodDefinition> ribbonItem in this.m_ribbonItems)
        ribbonItem.SetEquipped(this.m_currentGod);
      this.m_equipeGodFX.Play();
      this.StartCoroutine(this.AutoCloseRoutine(0.3f));
    }

    private IEnumerator AutoCloseRoutine(float delay)
    {
      yield return (object) new WaitForTime(delay);
      this.SimulateCloseClick();
    }

    public IEnumerator CloseUI()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      GodSelectionRoot godSelectionRoot = this;
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
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      DOTween.To(new DOGetter<float>(godSelectionRoot.\u003CCloseUI\u003Eb__40_0), new DOSetter<float>(godSelectionRoot.\u003CCloseUI\u003Eb__40_1), 0.0f, 0.25f);
      godSelectionRoot.m_godListCanvas.interactable = false;
      godSelectionRoot.m_godListCanvas.blocksRaycasts = false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) godSelectionRoot.PlayAnimation(godSelectionRoot.m_animationDirector.GetAnimation("Close"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public void SimulateCloseClick() => InputUtility.SimulateClickOn((Selectable) this.m_closeButton);

    private class GodComparer : Comparer<GodDefinition>
    {
      public override int Compare(GodDefinition x, GodDefinition y)
      {
        if ((UnityEngine.Object) x == (UnityEngine.Object) null && (UnityEngine.Object) y == (UnityEngine.Object) null)
          return 0;
        if ((UnityEngine.Object) x == (UnityEngine.Object) null)
          return 1;
        return (UnityEngine.Object) y == (UnityEngine.Object) null ? -1 : x.Order - y.Order;
      }
    }
  }
}
