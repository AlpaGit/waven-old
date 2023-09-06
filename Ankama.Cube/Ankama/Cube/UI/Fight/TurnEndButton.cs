// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.TurnEndButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public class TurnEndButton : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField]
    private Button m_button;
    [SerializeField]
    private Image m_timeFilling;
    [SerializeField]
    private TextField m_text;
    [SerializeField]
    private UISpriteTextRenderer m_chronoText;
    [Header("Colors")]
    [SerializeField]
    private Color m_myTurnColor;
    [SerializeField]
    private Color m_opponentTurnColor;
    private float m_turnStartTime;
    private float m_turnDuration;
    private bool m_isDirty;
    private bool m_running;
    private int m_previousSecond = -1;
    private Sequence m_sequence;

    public void SetTurnDuration(float turnDuration) => this.m_turnDuration = turnDuration;

    public void StartTurn(bool isLocalPlayerTurn, bool isFightEnded)
    {
      this.SetInteractable(isLocalPlayerTurn && !isFightEnded);
      this.m_chronoText.gameObject.SetActive(false);
      this.m_text.gameObject.SetActive(true);
      if (isLocalPlayerTurn)
      {
        this.m_timeFilling.color = this.m_myTurnColor;
        this.m_text.SetText(51179);
      }
      else
      {
        this.m_timeFilling.color = this.m_opponentTurnColor;
        this.m_text.SetText(85537);
      }
      this.m_previousSecond = -1;
      this.m_turnStartTime = Time.unscaledTime;
      this.m_running = true;
      this.m_isDirty = true;
    }

    public void Stop()
    {
      this.SetInteractable(false);
      this.m_running = false;
      this.m_isDirty = true;
      this.m_chronoText.gameObject.SetActive(false);
      this.m_text.gameObject.SetActive(true);
    }

    public void EndTurn()
    {
      this.SetInteractable(false);
      this.m_isDirty = true;
    }

    public void EndTeamTurn()
    {
      this.SetInteractable(false);
      this.m_running = false;
      this.m_isDirty = true;
    }

    public void AddListener(UnityAction call) => this.m_button.onClick.AddListener(call);

    public void SimulateClick() => InputUtility.SimulateClickOn((Selectable) this.m_button);

    private void Awake()
    {
      this.StartTurn(false, true);
      this.m_timeFilling.fillAmount = 0.0f;
    }

    private void Update()
    {
      if (!this.m_running && !this.m_isDirty)
        return;
      this.RefreshTimeFilling();
      this.m_isDirty = false;
    }

    private void SetInteractable(bool value)
    {
      this.m_button.interactable = value;
      this.m_text.color = this.m_text.color with
      {
        a = value ? 1f : 0.6f
      };
    }

    private void RefreshTimeFilling()
    {
      if (!this.m_running || Mathf.Approximately(this.m_turnDuration, 0.0f))
      {
        this.m_timeFilling.fillAmount = 0.0f;
      }
      else
      {
        float num1 = Time.unscaledTime - this.m_turnStartTime;
        this.m_timeFilling.fillAmount = Mathf.Clamp01(num1 / this.m_turnDuration);
        if ((double) this.m_turnDuration - (double) num1 <= 10.0)
        {
          this.m_chronoText.gameObject.SetActive(true);
          this.m_text.gameObject.SetActive(false);
          int num2 = (int) ((double) this.m_turnDuration - (double) num1);
          if (this.m_previousSecond != num2)
          {
            this.m_chronoText.text = num2.ToString();
            this.m_chronoText.transform.localScale = Vector3.one * 5f;
            if (this.m_sequence != null && this.m_sequence.IsActive())
              this.m_sequence.Kill();
            this.SetAlpha(0.0f);
            this.m_sequence = DOTween.Sequence();
            this.m_sequence.Insert(0.0f, (Tween) DOTween.To((DOGetter<float>) (() => this.m_chronoText.color.a), new DOSetter<float>(this.SetAlpha), 1f, 0.3f));
            this.m_sequence.Insert(0.0f, (Tween) DOTween.To((DOGetter<Vector3>) (() => Vector3.one * 5f), (DOSetter<Vector3>) (x => this.m_chronoText.transform.localScale = x), Vector3.one, 0.3f).SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(0.0f));
          }
          this.m_previousSecond = num2;
        }
        else
        {
          this.m_chronoText.gameObject.SetActive(false);
          this.m_text.gameObject.SetActive(true);
        }
      }
    }

    private void SetAlpha(float value) => this.m_chronoText.color = this.m_chronoText.color with
    {
      a = value
    };
  }
}
