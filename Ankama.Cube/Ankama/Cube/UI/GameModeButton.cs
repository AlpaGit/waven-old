// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.GameModeButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class GameModeButton : Button
  {
    [SerializeField]
    private Transform m_scaleDummy;
    [SerializeField]
    private Image m_image;
    [SerializeField]
    private GameModeButtonStyle m_style;
    private Sequence m_tweenSequence;

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (!this.gameObject.activeInHierarchy)
        return;
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("AnimatedTextButton " + this.name + " doesn't have a style defined !", 27, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\ZaapGameMode\\GameModeButton.cs");
      }
      else
      {
        GameModeButtonState gameModeButtonState = this.m_style.disable;
        switch (state)
        {
          case Selectable.SelectionState.Normal:
            gameModeButtonState = this.m_style.normal;
            break;
          case Selectable.SelectionState.Highlighted:
            gameModeButtonState = this.m_style.highlight;
            break;
          case Selectable.SelectionState.Pressed:
            gameModeButtonState = this.m_style.pressed;
            break;
          case Selectable.SelectionState.Disabled:
            gameModeButtonState = this.m_style.disable;
            break;
        }
        Sequence tweenSequence = this.m_tweenSequence;
        if (tweenSequence != null)
          tweenSequence.Kill();
        if (instant)
        {
          this.m_scaleDummy.localScale = Vector3.one * gameModeButtonState.scale;
          this.m_image.color = gameModeButtonState.imageColor;
        }
        else
        {
          this.m_tweenSequence = DOTween.Sequence();
          this.m_tweenSequence.Insert(0.0f, (Tween) this.m_scaleDummy.DOScale(Vector3.one * gameModeButtonState.scale, this.m_style.transitionDuration).SetEase<Tweener>(this.m_style.ease));
          this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOColor(this.m_image, gameModeButtonState.imageColor, this.m_style.transitionDuration).SetEase<Tweener>(this.m_style.ease));
        }
      }
    }
  }
}
