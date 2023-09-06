// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.GameSelectionUIDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.Demo.UI
{
  public class GameSelectionUIDemo : BaseFightSelectionUI
  {
    [SerializeField]
    private GameSelectionButton[] m_buttons;
    [SerializeField]
    private SlidingAnimUI m_slidingAnim;
    private GameSelectionButton m_hightlightedButton;
    public Action<int> onSelect;

    private void Start()
    {
      for (int index1 = 0; index1 < this.m_buttons.Length; ++index1)
      {
        int index = index1;
        GameSelectionButton button = this.m_buttons[index];
        button.onClick.AddListener((UnityAction) (() => this.OnButtonClick(index)));
        button.onPointerEnter += new Action<GameSelectionButton>(this.OnPointerEnter);
        button.onPointerExit += new Action<GameSelectionButton>(this.OnPointerExit);
      }
    }

    private void OnPointerEnter(GameSelectionButton button)
    {
      this.m_hightlightedButton = button;
      for (int index = 0; index < this.m_buttons.Length; ++index)
      {
        GameSelectionButton button1 = this.m_buttons[index];
        button1.anotherButtonIsHightlighted = (UnityEngine.Object) button1 != (UnityEngine.Object) this.m_hightlightedButton;
      }
    }

    private void OnPointerExit(GameSelectionButton button)
    {
      if ((UnityEngine.Object) this.m_hightlightedButton != (UnityEngine.Object) button)
        return;
      for (int index = 0; index < this.m_buttons.Length; ++index)
        this.m_buttons[index].anotherButtonIsHightlighted = false;
      this.m_hightlightedButton = (GameSelectionButton) null;
    }

    private void OnButtonClick(int index)
    {
      Action<int> onSelect = this.onSelect;
      if (onSelect == null)
        return;
      onSelect(index);
    }

    public override IEnumerator OpenFrom(SlidingSide side)
    {
      GameSelectionUIDemo gameSelectionUiDemo = this;
      Sequence elemSequence = gameSelectionUiDemo.m_slidingAnim.PlayAnim(true, side, side == SlidingSide.Left);
      gameSelectionUiDemo.m_openDirector.time = 0.0;
      gameSelectionUiDemo.m_openDirector.Play();
      while (elemSequence.IsActive() || gameSelectionUiDemo.m_openDirector.playableGraph.IsValid() && !gameSelectionUiDemo.m_openDirector.playableGraph.IsDone())
        yield return (object) null;
    }

    public override IEnumerator CloseTo(SlidingSide side)
    {
      GameSelectionUIDemo gameSelectionUiDemo = this;
      Sequence elemSequence = gameSelectionUiDemo.m_slidingAnim.PlayAnim(false, side, side == SlidingSide.Right);
      gameSelectionUiDemo.m_closeDirector.time = 0.0;
      gameSelectionUiDemo.m_closeDirector.Play();
      while (elemSequence.IsActive() || gameSelectionUiDemo.m_closeDirector.playableGraph.IsValid() && !gameSelectionUiDemo.m_closeDirector.playableGraph.IsDone())
        yield return (object) null;
    }
  }
}
