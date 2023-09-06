// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.PlayerLayerNavButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class PlayerLayerNavButton : MonoBehaviour
  {
    private Action m_method;
    private PlayerLayerNavRoot m_root;
    private AnimatedToggleButton m_toggle;
    [SerializeField]
    private Transform m_parent;

    public void Initialise(PlayerLayerNavRoot root)
    {
      this.m_root = root;
      this.m_toggle = this.GetComponent<AnimatedToggleButton>();
      this.m_toggle.onValueChanged.AddListener((UnityAction<bool>) (b => this.OnClic(b)));
    }

    public void OnClic(bool ison) => this.m_root.OnClic(this);

    public void OnDeselect() => this.m_parent.DOScale(Vector3.one, 0.15f);

    public void OnValidate() => this.m_parent.DOScale(Vector3.one * 1.2f, 0.15f);

    public Action GetMethod() => this.m_method;

    public void SetMethode(Action action) => this.m_method = action;

    public void ForceClic() => this.m_toggle.isOn = true;
  }
}
