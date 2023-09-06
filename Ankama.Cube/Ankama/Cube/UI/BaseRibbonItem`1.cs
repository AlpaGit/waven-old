// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.BaseRibbonItem`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.UI.Components;
using DataEditor;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public abstract class BaseRibbonItem<T> : MonoBehaviour, IUIResourceConsumer, IUIResourceProvider where T : EditableData
  {
    [SerializeField]
    private Toggle m_toggle;
    [SerializeField]
    private ImageLoader m_visual;
    [SerializeField]
    private GameObject m_equippedSquare;
    [SerializeField]
    private CanvasGroup m_selectedTicks;
    private RectTransform m_tickRectTransform;
    private Vector2 m_defaultTickDelta;
    private bool m_selected;
    protected bool m_equipped;
    protected T m_definition;
    private MaterialLoader m_equippedMaterial;

    public event Action<T> OnSelected;

    public int definitionId => this.m_definition.id;

    private void Awake()
    {
      this.m_toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
      this.m_tickRectTransform = this.m_selectedTicks.GetComponent<RectTransform>();
      this.m_defaultTickDelta = this.m_tickRectTransform.sizeDelta;
      this.m_selectedTicks.gameObject.SetActive(false);
    }

    public virtual void Initialise(T definition) => this.m_definition = definition;

    private void OnToggleValueChanged(bool selected)
    {
      if (this.m_selected == selected)
        return;
      this.m_selected = selected;
      this.UpdateSelected();
    }

    public void SetSelected(T selectedValue, bool force = false) => this.SetSelected(selectedValue.id, force);

    public void SetSelected(int defId, bool force = false) => this.SetSelected(this.m_definition.id == defId, force);

    public void SetSelected(bool selected, bool force = false)
    {
      if (this.m_selected == selected && !force)
        return;
      this.m_selected = selected;
      this.UpdateSelected();
      this.m_toggle.isOn = selected;
    }

    protected virtual void UpdateSelected()
    {
      if (this.m_selected)
      {
        Action<T> onSelected = this.OnSelected;
        if (onSelected != null)
          onSelected(this.m_definition);
      }
      this.m_selectedTicks.gameObject.SetActive(this.m_selected);
      if (!this.m_selected)
        return;
      this.m_tickRectTransform.sizeDelta = this.m_defaultTickDelta + new Vector2(0.0f, 100f);
      this.m_selectedTicks.alpha = 0.0f;
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_selectedTicks.DOFade(1f, 0.1f));
      sequence.Insert(0.0f, (Tween) this.m_tickRectTransform.DOSizeDelta(this.m_defaultTickDelta, 0.1f));
      sequence.Play<Sequence>();
    }

    public void SetEquipped(T equippedValue) => this.SetEquipped((UnityEngine.Object) this.m_definition == (UnityEngine.Object) equippedValue);

    public void SetEquipped(bool equipped)
    {
      this.m_equipped = equipped;
      this.UpdateEquipped();
    }

    protected virtual void UpdateEquipped()
    {
      this.m_equippedSquare.SetActive(this.m_equipped);
      if (this.m_equipped && (UnityEngine.Object) this.m_equippedMaterial != (UnityEngine.Object) null && this.m_equippedMaterial.loadState == UIResourceLoadState.Loaded)
        this.m_visual.material = this.m_equippedMaterial.GetAsset();
      else
        this.m_visual.material = (Material) null;
    }

    public UIResourceDisplayMode Register(IUIResourceProvider provider) => UIResourceDisplayMode.Immediate;

    public void UnRegister(IUIResourceProvider provider)
    {
      if (this.loadState != UIResourceLoadState.Loaded)
        return;
      this.UpdateEquipped();
    }

    protected void SetupVisual(AssetReference assetReference, [NotNull] string bundleName) => this.m_visual.Setup(assetReference, bundleName, (IUIResourceConsumer) this);

    protected void SetupEquippedMaterial(AssetReference assetReference, [NotNull] string bundleName)
    {
      if ((UnityEngine.Object) this.m_equippedMaterial == (UnityEngine.Object) null)
        this.m_equippedMaterial = this.gameObject.AddComponent<MaterialLoader>();
      this.m_equippedMaterial.Setup(assetReference, bundleName, (IUIResourceConsumer) this);
    }

    public UIResourceLoadState loadState => (UnityEngine.Object) this.m_visual != (UnityEngine.Object) null && this.m_visual.loadState == UIResourceLoadState.Loading || (UnityEngine.Object) this.m_equippedMaterial != (UnityEngine.Object) null && this.m_equippedMaterial.loadState == UIResourceLoadState.Loading ? UIResourceLoadState.Loading : UIResourceLoadState.Loaded;
  }
}
