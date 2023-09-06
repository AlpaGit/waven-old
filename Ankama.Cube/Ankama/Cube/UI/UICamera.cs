// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.UICamera
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.SRP;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI
{
  public class UICamera : IBlurCamera
  {
    public Camera camera;
    public List<AbstractUI> uis;
    public UICamera child;
    private bool m_hasBlur;
    private bool m_isBlurEnabled;
    private AbstractUI m_linkedBlurUI;

    public float factor => !((UnityEngine.Object) this.m_linkedBlurUI != (UnityEngine.Object) null) ? 0.0f : this.m_linkedBlurUI.blurAmount;

    public bool hasBlur => this.m_hasBlur;

    public bool isBlurEnabled => this.m_isBlurEnabled;

    public bool isFullBlur => (UnityEngine.Object) this.m_linkedBlurUI != (UnityEngine.Object) null && (double) this.m_linkedBlurUI.blurAmount >= 1.0;

    public UICamera(Camera cam)
    {
      this.camera = cam;
      this.m_isBlurEnabled = false;
      this.uis = new List<AbstractUI>();
    }

    private void EnableBlur(bool value)
    {
      if (this.m_isBlurEnabled == value)
        return;
      this.m_isBlurEnabled = value;
      if (value)
        CubeSRP.s_blurCamera[this.camera] = (IBlurCamera) this;
      else
        CubeSRP.s_blurCamera.Remove(this.camera);
    }

    public void ActiveBlurFor(AbstractUI linkedUI)
    {
      this.m_hasBlur = true;
      this.EnableBlur(true);
      this.m_linkedBlurUI = linkedUI;
      this.m_linkedBlurUI.onBlurFactorIsFull = new Action(this.OnBlurFactorIsFull);
      this.m_linkedBlurUI.onBlurFactorStartDecrease = new Action(this.OnBlurFactorStartDecrease);
    }

    public void Clean()
    {
      this.m_hasBlur = false;
      this.EnableBlur(false);
      if ((UnityEngine.Object) this.m_linkedBlurUI != (UnityEngine.Object) null)
      {
        this.m_linkedBlurUI.onBlurFactorIsFull = (Action) null;
        this.m_linkedBlurUI.onBlurFactorStartDecrease = (Action) null;
        this.m_linkedBlurUI = (AbstractUI) null;
      }
      this.uis.Clear();
      this.child = (UICamera) null;
    }

    public void NeedToDisplayBlur(bool value)
    {
      if (!this.m_hasBlur)
        return;
      this.EnableBlur(value);
    }

    private void OnBlurFactorIsFull()
    {
      if (!this.m_isBlurEnabled || this.child == null)
        return;
      this.child.NeedToDisplayBlurRecursive(false);
    }

    private void OnBlurFactorStartDecrease()
    {
      if (!this.m_isBlurEnabled || this.child == null)
        return;
      this.child.NeedToDisplayBlurRecursive(true);
    }

    private void NeedToDisplayBlurRecursive(bool value)
    {
      if (this.m_hasBlur)
        this.EnableBlur(value);
      if (this.child == null)
        return;
      if (!this.m_hasBlur)
        this.child.NeedToDisplayBlur(value);
      else if (!value)
        this.child.NeedToDisplayBlur(false);
      else if (this.isFullBlur)
        this.child.NeedToDisplayBlur(false);
      else
        this.child.NeedToDisplayBlur(true);
    }
  }
}
