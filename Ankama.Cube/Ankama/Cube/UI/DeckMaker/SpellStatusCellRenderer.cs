// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.SpellStatusCellRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI.DeckMaker
{
  public class SpellStatusCellRenderer : 
    SpellCellRenderer<SpellStatus, ISpellStatusCellRendererConfigurator>
  {
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
    private IDragNDropValidator m_dragNDropValidator;
    private SpellStatusData? m_spellStatusData;

    protected override bool IsAvailable()
    {
      ref SpellStatusData? local = ref this.m_spellStatusData;
      if ((local.HasValue ? (local.GetValueOrDefault().hasEnoughAp ? 1 : 0) : 1) == 0)
        return false;
      ISpellStatusCellRendererConfigurator configurator = this.m_configurator;
      return configurator == null || configurator.IsParentInteractable();
    }

    protected override int? GetAPCost()
    {
      ref SpellStatusData? local = ref this.m_spellStatusData;
      return !local.HasValue ? new int?() : local.GetValueOrDefault().apCost;
    }

    protected override int? GetBaseAPCost()
    {
      ref SpellStatusData? local = ref this.m_spellStatusData;
      return !local.HasValue ? new int?() : local.GetValueOrDefault().baseCost;
    }

    private void Awake()
    {
      if (!(bool) (UnityEngine.Object) this.m_castableDnd)
        return;
      this.m_castableDnd.SkipEndDragEvent = true;
      this.m_castableDnd.OnDragBeginRequest += new Func<bool>(this.IsDragValid);
      this.m_castableDnd.OnDragBegin += new Action<bool>(this.OnDragBegin);
      this.m_castableDnd.OnDragEnd += new Action<bool>(this.OnDragEnd);
      this.m_castableDnd.castBehaviour = DndCastBehaviour.Stay;
    }

    private bool IsDragValid() => this.m_dragNDropValidator == null || this.m_dragNDropValidator.IsValidDrag((object) this.m_value);

    protected override void SetValue(SpellStatus val)
    {
      // ISSUE: explicit non-virtual call
      this.SetValue(val?.definition, val != null ? __nonvirtual (val.level) : 0);
      this.m_castableDnd.enableDnd = this.IsAvailable();
      this.m_castableDnd.ResetContentPosition();
    }

    public override void OnConfiguratorUpdate(bool instant)
    {
      if (this.m_configurator != null)
      {
        this.m_spellStatusData = this.m_configurator.GetSpellStatusData(this.m_value);
        this.m_dragNDropValidator = this.m_configurator.GetDragNDropValidator();
      }
      else
      {
        this.m_spellStatusData = new SpellStatusData?();
        this.m_dragNDropValidator = (IDragNDropValidator) null;
      }
      base.OnConfiguratorUpdate(instant);
      this.m_castableDnd.enableDnd = this.IsAvailable();
    }

    private void OnDragBegin(bool click)
    {
      this.m_configurator?.GetEventListener()?.CastSpellDragBegin(this, click);
      this.m_onBeginDrag.Invoke();
    }

    private void OnDragEnd(bool onTarget) => this.m_configurator?.GetEventListener()?.CastSpellDragEnd(this, onTarget);

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
