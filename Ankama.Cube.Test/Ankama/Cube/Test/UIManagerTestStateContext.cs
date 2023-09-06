// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Test.UIManagerTestStateContext
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.Test
{
  public class UIManagerTestStateContext : StateContext
  {
    private bool m_useBlur;
    private List<UIManagerTestUI> m_uis = new List<UIManagerTestUI>();

    public UIManagerTestStateContext(bool useBlur) => this.m_useBlur = useBlur;

    protected override IEnumerator Load()
    {
      yield return (object) this.AddUI(this.m_useBlur).OpenCoroutine();
    }

    public override bool AllowsTransition([CanBeNull] StateContext nextState) => true;

    protected override IEnumerator Transition([CanBeNull] StateTransitionInfo transitionInfo)
    {
      for (int i = 0; i < this.m_uis.Count; ++i)
        yield return (object) this.m_uis[i].CloseCoroutine();
    }

    protected override IEnumerator Unload()
    {
      for (int index = 0; index < this.m_uis.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_uis[index].gameObject);
      this.m_uis.Clear();
      yield return (object) base.Unload();
    }

    private UIManagerTestUI AddUI(bool blur)
    {
      UIManagerTestUI uiManagerTestUi = UnityEngine.Object.Instantiate<UIManagerTestUI>(UIManagerTestRuntime.s_instance.uiTestPrefab);
      uiManagerTestUi.gameObject.SetActive(true);
      uiManagerTestUi.useBlur = blur;
      uiManagerTestUi.onCreate = new Action<UIManagerTestUI, UIManagerTestCreationParams>(this.OnCreate);
      uiManagerTestUi.onRemove = new Action<UIManagerTestUI>(this.OnRemove);
      uiManagerTestUi.SetDepth((StateContext) this);
      this.m_uis.Add(uiManagerTestUi);
      return uiManagerTestUi;
    }

    private void OnRemove(UIManagerTestUI ui)
    {
      if (this.m_uis.Count > 1)
      {
        ui.Close((Action) (() =>
        {
          this.m_uis.Remove(ui);
          UnityEngine.Object.Destroy((UnityEngine.Object) ui.gameObject);
        }));
      }
      else
      {
        if (this.parent == null)
          return;
        this.parent.ClearChildState();
      }
    }

    private void OnCreate(UIManagerTestUI ui, UIManagerTestCreationParams parameters)
    {
      if (parameters.option == UIManagerTestCreationOption.SameState)
      {
        this.AddUI(parameters.useBlur).Open();
      }
      else
      {
        UIManagerTestStateContext childState = new UIManagerTestStateContext(parameters.useBlur);
        switch (parameters.option)
        {
          case UIManagerTestCreationOption.ChildState:
            this.SetChildState((StateContext) childState);
            break;
          case UIManagerTestCreationOption.NewState:
            if (this.parent == null)
              break;
            this.parent.SetChildState((StateContext) childState);
            break;
          case UIManagerTestCreationOption.NewLayer:
            StateManager.AddLayer(string.Format("NewLayer {0}", (object) UIManagerTestRuntime.s_layerCount++)).SetChildState((StateContext) childState);
            break;
        }
      }
    }
  }
}
