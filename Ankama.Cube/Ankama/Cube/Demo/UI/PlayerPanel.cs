// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.PlayerPanel
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class PlayerPanel : MonoBehaviour
  {
    [SerializeField]
    protected CanvasGroup m_canvasGroup;
    [SerializeField]
    protected Image m_illu;
    [SerializeField]
    private RawTextField m_name;
    [SerializeField]
    private RawTextField m_level;
    [SerializeField]
    private CanvasGroup[] m_texts;
    [SerializeField]
    private float m_openTransitionDuration = 1f;
    [SerializeField]
    private float m_closeTransitionDuration = 0.8f;
    private Tween m_tween;

    private float stateValue { get; set; }

    public long playerId { get; private set; }

    public bool isEmpty => this.playerId < 0L;

    public void Set(FightPlayerInfo player, int level, SquadFakeData fakeData, bool tween = false)
    {
      this.playerId = player.Uid;
      this.Set(player.Info.Nickname, level, fakeData, tween);
    }

    public void Set(string nickname, int level, SquadFakeData fakeData, bool tween = false)
    {
      level = 6;
      this.m_illu.sprite = fakeData.illu;
      this.m_name.SetText(nickname);
      this.m_level.SetText(string.Format("Niveau {0}", (object) level));
      if (this.m_tween != null && this.m_tween.IsActive())
        this.m_tween.Kill();
      if (tween)
        this.m_tween = (Tween) DOVirtual.Float(this.stateValue, 1f, this.m_openTransitionDuration, new TweenCallback<float>(this.UpdateState));
      else
        this.UpdateState(1f);
    }

    public void SetEmpty(bool tween = false)
    {
      this.playerId = -1L;
      if (this.m_tween != null && this.m_tween.IsActive())
        this.m_tween.Kill();
      if (tween)
        this.m_tween = (Tween) DOVirtual.Float(this.stateValue, 0.0f, this.m_closeTransitionDuration, new TweenCallback<float>(this.UpdateState));
      else
        this.UpdateState(0.0f);
    }

    private void UpdateState(float value)
    {
      this.stateValue = value;
      for (int index = 0; index < this.m_texts.Length; ++index)
      {
        CanvasGroup text = this.m_texts[index];
        text.alpha = value;
        text.gameObject.SetActive((double) value > 9.9999997473787516E-05);
      }
      this.m_illu.color = new Color(value, 1f, 1f, 1f);
    }

    public enum State
    {
      Empy,
      Player,
    }
  }
}
