// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.ProfileUIRoot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class ProfileUIRoot : AbstractUI
  {
    [SerializeField]
    private Transform SafeArea;
    [SerializeField]
    private Image m_greyBG;

    public void Initialise() => this.SafeArea.localPosition = new Vector3(this.SafeArea.localPosition.x, 1080f, 0.0f);

    public IEnumerator PlayEnterAnimation()
    {
      this.SafeArea.localPosition = new Vector3(0.0f, 1080f, 0.0f);
      Sequence sequence = DOTween.Sequence();
      sequence.Append((Tween) this.SafeArea.DOLocalMoveY(0.0f, 0.25f).SetEase<Tweener>(Ease.OutBounce));
      sequence.Play<Sequence>();
      yield return (object) sequence.WaitForKill();
    }

    public IEnumerator Unload()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ProfileUIRoot profileUiRoot = this;
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
      DOTweenModuleUI.DOFade(profileUiRoot.m_greyBG, 0.0f, 0.5f);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      DOTween.To(new DOGetter<float>(profileUiRoot.\u003CUnload\u003Eb__4_0), new DOSetter<float>(profileUiRoot.\u003CUnload\u003Eb__4_1), 0.0f, 0.25f);
      Sequence sequence = DOTween.Sequence();
      sequence.Append((Tween) profileUiRoot.SafeArea.DOLocalMove(new Vector3(profileUiRoot.SafeArea.localPosition.x, 1080f, 0.0f), 0.25f));
      sequence.Play<Sequence>();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) sequence.WaitForKill();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public void Close() => PlayerIconRoot.instance.ReducePanel();
  }
}
