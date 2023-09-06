// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.ScenesUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ankama.Cube.Utility
{
  public static class ScenesUtility
  {
    public const string FightMapWrapperSceneName = "FightMapWrapper";
    public const string HavreMapWrapperSceneName = "HavreMapWrapper";
    public const string FightUISceneName = "FightUI";
    public const string FightUIReworkSceneName = "FightUIRework";
    public const string WorldUISceneName = "WorldUI";
    public const string FightEndedWinUISceneName = "FightEndedWinUI";
    public const string FightEndedLoseUISceneName = "FightEndedLoseUI";
    public const string FightEndedDrawUISceneName = "FightEndedDrawUI";
    public const string PopupInfoUISceneName = "PopupInfoUI";
    public const string PlayerUIMainState = "PlayerLayerUI";
    public const string PlayerUINavState = "PlayerLayer_NavRibbonCanvas";
    public const string ParametersUISceneName = "ParametersUI";
    public const string OptionUISceneName = "OptionUI";
    public const string BugReportUISceneName = "BugReportUI";
    public const string DeckUISceneName = "DeckUI";
    public const string DeckMainUISceneName = "PlayerLayer_DeckCanvas";
    public const string ProfileUISceneName = "PlayerLayer_ProfilCanvas";
    public const string GodSelectionUISceneName = "GodSelectionUI";
    public const string UIZaap_Pvp = "UIZaap_PVP";
    public const string UIZaap_1v1Loaing = "MatchmakingUI_1v1";

    public static string GetFightMapSceneName(int id) => "FightMap_" + (object) id;

    public static string GetHavreMapSceneName(int id)
    {
      switch (id)
      {
        case 0:
          return "HavreDimension_Amakna";
        case 1:
          return "HavreDimension_Bonta";
        case 2:
          return "HavreDimension_Brakmar";
        case 3:
          return "HavreDimension_Sufokia";
        case 4:
          return "HavreDimension_Astrub";
        default:
          return "HavreDimension_Amakna";
      }
    }

    public static T GetSceneRoot<T>(Scene scene) where T : Component
    {
      GameObject sceneRoot = ScenesUtility.GetSceneRoot(scene);
      if ((Object) null == (Object) sceneRoot)
        return default (T);
      T componentInChildren = sceneRoot.GetComponentInChildren<T>();
      if ((Object) null == (Object) componentInChildren)
        Log.Error("Cannot find component of type '" + typeof (T).Name + "' in root object of scene named '" + scene.name + "'.", 61, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Utility\\ScenesUtility.cs");
      return componentInChildren;
    }

    [CanBeNull]
    public static T GetComponentInRootGameObjects<T>(Scene scene) where T : Component
    {
      int rootCount = scene.rootCount;
      if (rootCount == 0)
        return default (T);
      List<GameObject> gameObjectList = ListPool<GameObject>.Get(scene.rootCount);
      scene.GetRootGameObjects(gameObjectList);
      for (int index = 0; index < rootCount; ++index)
      {
        T component = gameObjectList[index].GetComponent<T>();
        if ((Object) null != (Object) component)
        {
          ListPool<GameObject>.Release(gameObjectList);
          return component;
        }
      }
      ListPool<GameObject>.Release(gameObjectList);
      return default (T);
    }

    [CanBeNull]
    private static GameObject GetSceneRoot(Scene scene)
    {
      int rootCount = scene.rootCount;
      if (rootCount == 0)
      {
        Log.Error("Scene named '" + scene.name + "' is empty.", 96, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Utility\\ScenesUtility.cs");
        return (GameObject) null;
      }
      List<GameObject> gameObjectList = ListPool<GameObject>.Get(rootCount);
      scene.GetRootGameObjects(gameObjectList);
      if (rootCount > 1)
        Log.Warning("Scene named '" + scene.name + "' has more than one root GameObject.", 105, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Utility\\ScenesUtility.cs");
      for (int index = 0; index < rootCount; ++index)
      {
        GameObject sceneRoot = gameObjectList[index];
        if (sceneRoot.name.Equals("root"))
        {
          ListPool<GameObject>.Release(gameObjectList);
          return sceneRoot;
        }
      }
      Log.Error("Scene named '" + scene.name + "' does not have a root GameObject named 'root'.", 117, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Utility\\ScenesUtility.cs");
      ListPool<GameObject>.Release(gameObjectList);
      return (GameObject) null;
    }
  }
}
