// Decompiled with JetBrains decompiler
// Type: DeckEditItemPointerListener
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckEditItemPointerListener : 
  MonoBehaviour,
  IPointerExitHandler,
  IEventSystemHandler,
  IPointerEnterHandler
{
  private string m_overSound = "event:/UI/Menu/UI_GEN_RollOver_Button";
  private bool m_destroying;
  private RectTransform m_effectTarget;

  private void Awake() => this.m_effectTarget = this.transform.GetChild(0).GetComponent<RectTransform>();

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (this.m_destroying)
      return;
    this.m_effectTarget.DOLocalMoveY(5f, 0.1f);
    AudioManager.PlayOneShot(this.m_overSound);
  }

  public void OnPointerExit(PointerEventData eventData) => this.m_effectTarget.DOLocalMoveY(0.0f, 0.1f);

  public void RemoveComponent()
  {
    this.m_destroying = true;
    this.m_effectTarget.localPosition = this.m_effectTarget.localPosition with
    {
      y = 0.0f
    };
    Object.Destroy((Object) this);
  }
}
