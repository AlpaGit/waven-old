// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Test.UIManagerTestRuntime
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Configuration;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Test
{
  public class UIManagerTestRuntime : MonoBehaviour
  {
    public static UIManagerTestRuntime s_instance;
    public static int s_layerCount;
    [SerializeField]
    public UIManagerTestUI uiTestPrefab;

    private void Awake()
    {
      if ((Object) UIManagerTestRuntime.s_instance != (Object) null)
        return;
      UIManagerTestRuntime.s_instance = this;
      this.uiTestPrefab.gameObject.SetActive(false);
      ApplicationStarter.InitializeAssetManager();
    }

    private void OnDestroy()
    {
      if ((Object) UIManagerTestRuntime.s_instance != (Object) this)
        return;
      UIManagerTestRuntime.s_instance = (UIManagerTestRuntime) null;
    }

    private IEnumerator Start()
    {
      yield return (object) ApplicationStarter.ConfigureLocalAssetManager();
      StateManager.GetDefaultLayer().SetChildState((StateContext) new UIManagerTestStateContext(this.uiTestPrefab.useBlur));
    }
  }
}
