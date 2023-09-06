// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.CompanionStatusCellRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.DeckMaker
{
  public class CompanionStatusCellRenderer : 
    CompanionCellRenderer<ReserveCompanionStatus, ICompanionStatusCellRendererConfigurator>
  {
    [SerializeField]
    private GameObject m_highlight;
    [SerializeField]
    private Image m_deadIcon;
    [SerializeField]
    private Image m_givenIcon;
    [SerializeField]
    private Image m_inFightIcon;
    [SerializeField]
    protected DragNDropElement m_castableDnd;
    [Header("Audio Events")]
    [SerializeField]
    private UnityEvent m_onBeginDrag;
    [SerializeField]
    private UnityEvent m_onCancelDrag;
    [SerializeField]
    private UnityEvent m_onEnterTarget;
    [SerializeField]
    private UnityEvent m_onExitTarget;
    [SerializeField]
    private UnityEvent m_onBecameAvailable;
    private CompanionStatusData? m_statusData;
    private IDragNDropValidator m_dragNDropValidator;

    protected override IReadOnlyList<Cost> GetCosts()
    {
      ref CompanionStatusData? local = ref this.m_statusData;
      return !local.HasValue ? (IReadOnlyList<Cost>) null : local.GetValueOrDefault().cost;
    }

    protected override bool IsAvailable()
    {
      ref CompanionStatusData? local1 = ref this.m_statusData;
      if ((local1.HasValue ? (local1.GetValueOrDefault().hasResources ? 1 : 0) : 1) != 0)
      {
        ref CompanionStatusData? local2 = ref this.m_statusData;
        if ((local2.HasValue ? (int) local2.GetValueOrDefault().state : 0) == 0)
        {
          ICompanionStatusCellRendererConfigurator configurator = this.m_configurator;
          return configurator == null || configurator.IsParentInteractable();
        }
      }
      return false;
    }

    private void Awake()
    {
      if (!(bool) (UnityEngine.Object) this.m_castableDnd)
        return;
      this.m_castableDnd.SkipEndDragEvent = true;
      this.m_castableDnd.OnDragBegin += new Action<bool>(this.OnDragBegin);
      this.m_castableDnd.OnDragEnd += new Action<bool>(this.OnDragEnd);
      this.m_castableDnd.OnDragBeginRequest += new Func<bool>(this.IsDragValid);
      this.m_castableDnd.castBehaviour = DndCastBehaviour.MoveBack;
    }

    private bool IsDragValid() => this.m_dragNDropValidator == null || this.m_dragNDropValidator.IsValidDrag((object) this.m_value);

    protected override void SetValue(ReserveCompanionStatus val)
    {
      // ISSUE: explicit non-virtual call
      this.SetValue(val?.definition, val != null ? __nonvirtual (val.level) : 0);
      this.SetStateIcon();
      bool flag = this.IsAvailable();
      this.m_highlight.SetActive(flag);
      this.m_castableDnd.enableDnd = flag;
      this.m_castableDnd.ResetContentPosition();
    }

    private void SetStateIcon()
    {
      ref CompanionStatusData? local = ref this.m_statusData;
      CompanionReserveState companionReserveState = local.HasValue ? local.GetValueOrDefault().state : CompanionReserveState.Idle;
      this.m_deadIcon.gameObject.SetActive(companionReserveState == CompanionReserveState.Dead);
      this.m_givenIcon.gameObject.SetActive(companionReserveState == CompanionReserveState.Given);
      this.m_inFightIcon.gameObject.SetActive(companionReserveState == CompanionReserveState.InFight);
    }

    public override void OnConfiguratorUpdate(bool instant)
    {
      if (this.m_configurator != null)
      {
        this.m_statusData = this.m_configurator.GetCompanionStatusData(this.m_value);
        this.m_dragNDropValidator = this.m_configurator.GetDragNDropValidator();
      }
      else
      {
        this.m_statusData = new CompanionStatusData?();
        this.m_dragNDropValidator = (IDragNDropValidator) null;
      }
      base.OnConfiguratorUpdate(instant);
      this.SetStateIcon();
      bool flag = this.IsAvailable();
      if (this.m_highlight.activeSelf != flag)
      {
        this.m_highlight.SetActive(flag);
        if (flag)
          this.m_onBecameAvailable.Invoke();
      }
      this.m_castableDnd.enableDnd = flag;
    }

    private void OnDragBegin(bool click)
    {
      this.m_configurator?.GetEventListener()?.CastCompanionDragBegin(this, click);
      this.m_onBeginDrag.Invoke();
    }

    private void OnDragEnd(bool onTarget) => this.m_configurator?.GetEventListener()?.CastCompanionDragEnd(this, onTarget);

    public void OnEnterTarget()
    {
      this.m_castableDnd.OnEnterTarget();
      this.m_onEnterTarget.Invoke();
    }

    public void OnExitTarget()
    {
      this.m_castableDnd.OnExitTarget();
      this.m_onExitTarget.Invoke();
    }

    public void StartCast() => this.m_castableDnd.StartCast();

    public void CancelCast()
    {
      this.m_castableDnd.CancelCast();
      this.m_onCancelDrag.Invoke();
      this.m_onExitTarget.Invoke();
    }

    public void DoneCasting()
    {
      this.m_castableDnd.DoneCasting();
      this.m_onExitTarget.Invoke();
    }
  }
}
