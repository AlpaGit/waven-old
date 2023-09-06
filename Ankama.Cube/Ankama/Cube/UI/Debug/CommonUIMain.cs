// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.CommonUIMain
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetReferences;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Configuration;
using Ankama.Cube.Data.UI;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.UI.Debug
{
  public class CommonUIMain : MonoBehaviour
  {
    private void Awake()
    {
      if (!CommonUIMain.InitializeAssetManager())
        Application.Quit();
      RuntimeData.InitializeFonts();
      RuntimeData.InitializeLanguage(CultureCode.FR_FR);
      this.StartCoroutine(this.LoadScene());
    }

    private static bool InitializeAssetManager()
    {
      AssetManager.Initialize();
      AssetReferenceMap assetReferenceMap = Resources.Load<AssetReferenceMap>("AssetReferenceMap");
      if ((Object) null == (Object) assetReferenceMap)
      {
        Log.Error("[CRITICAL] Could not load AssetReferenceMap.", 31, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Debug\\CommonUIMain.cs");
        return false;
      }
      AssetManager.SetAssetReferenceMap(assetReferenceMap);
      Resources.UnloadAsset((Object) assetReferenceMap);
      return true;
    }

    private IEnumerator LoadScene()
    {
      yield return (object) ApplicationStarter.ConfigureLocalAssetManager();
      StateManager.GetDefaultLayer().SetChildState((StateContext) new CommonUIState());
    }
  }
}
