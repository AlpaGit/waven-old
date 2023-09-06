// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.FightUIFactory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Debug;
using Ankama.Cube.UI.Fight.Info;
using Ankama.Cube.UI.Fight.TeamCounter;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public class FightUIFactory : ScriptableObject
  {
    [Header("Prefabs")]
    [SerializeField]
    private PlayerUIRework m_playerUIPrefab;
    [SerializeField]
    private DebugFightUI m_debugUIPrefab;
    [SerializeField]
    private FightInfoMessageRoot m_fightInfoMessageRootPrefab;
    [SerializeField]
    private TeamPointCounter m_teamPointCounterPrefab;
    [Space(10f)]
    [SerializeField]
    private FightUIFactory.ElementCaracIdSpriteDictionary m_gaugeSprites;
    [Space(10f)]
    [SerializeField]
    private FightUIFactory.ElementaryStatesSpriteDictionary m_elementaryStatesSprites;
    [Space(10f)]
    [SerializeField]
    private FightUIFactory.SpellDataDictionary m_uiSpellCastData;
    [SerializeField]
    private FightUIFactory.CastItemData m_uiCompanionCastData;
    private static readonly Dictionary<CastHighlight, GameObjectPool> s_castHighlightPools = new Dictionary<CastHighlight, GameObjectPool>();
    private static readonly List<KeyValuePair<CastHighlight, GameObjectPool>> s_castHighlightInstances = new List<KeyValuePair<CastHighlight, GameObjectPool>>();

    public PlayerUIRework CreatePlayerUI(PlayerStatus playerStatus, Transform parent) => UnityEngine.Object.Instantiate<PlayerUIRework>(this.m_playerUIPrefab, parent);

    public DebugFightUI CreateDebugUI(Transform parent) => UnityEngine.Object.Instantiate<DebugFightUI>(this.m_debugUIPrefab, parent);

    public FightInfoMessageRoot CreateMessageRibbonRoot(Transform parent) => UnityEngine.Object.Instantiate<FightInfoMessageRoot>(this.m_fightInfoMessageRootPrefab, parent);

    public TeamPointCounter CreateTeamPointCounter(Transform parent) => UnityEngine.Object.Instantiate<TeamPointCounter>(this.m_teamPointCounterPrefab, parent);

    public static bool isReady { get; private set; }

    public static IEnumerator Load()
    {
      if (FightUIFactory.isReady)
      {
        Log.Error("Load called while the FightUIFactory is already ready.", 198, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
      }
      else
      {
        FightUIFactory.isReady = true;
        yield break;
      }
    }

    public static IEnumerator Unload()
    {
      if (FightUIFactory.isReady)
      {
        foreach (GameObjectPool gameObjectPool in FightUIFactory.s_castHighlightPools.Values)
          gameObjectPool.Dispose();
        FightUIFactory.s_castHighlightPools.Clear();
        FightUIFactory.s_castHighlightInstances.Clear();
        FightUIFactory.isReady = false;
        yield break;
      }
    }

    public Material GetSpellSelectedMaterial(SpellDefinition definition)
    {
      FightUIFactory.CastItemData castItemData;
      if (this.m_uiSpellCastData.TryGetValue(definition.element, out castItemData))
        return castItemData.selectedMaterial;
      Log.Error(string.Format("No spellData assigned to element {0}", (object) definition.element), 228, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
      return (Material) null;
    }

    public void Initialize(GaugeItemUI ui, CaracId element)
    {
      Sprite sprite;
      this.m_gaugeSprites.TryGetValue(element, out sprite);
      if ((UnityEngine.Object) null == (UnityEngine.Object) sprite)
        Log.Error(string.Format("No sprite assigned to element {0}", (object) element), 241, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
      ui.SetSprite(sprite);
    }

    public void Initialize(Image ui, ElementaryStates elementaryStates)
    {
      if (elementaryStates == ElementaryStates.None)
      {
        ui.sprite = (Sprite) null;
      }
      else
      {
        Sprite sprite;
        this.m_elementaryStatesSprites.TryGetValue(elementaryStates, out sprite);
        ui.sprite = sprite;
      }
      ui.enabled = (UnityEngine.Object) ui.sprite != (UnityEngine.Object) null;
    }

    private CastHighlight GetCastHighlight<T>(T definition) where T : ICastableDefinition
    {
      SpellDefinition spellDefinition = (object) definition as SpellDefinition;
      if ((UnityEngine.Object) spellDefinition != (UnityEngine.Object) null)
      {
        FightUIFactory.CastItemData castItemData;
        if (this.m_uiSpellCastData.TryGetValue(spellDefinition.element, out castItemData))
          return castItemData.castHighlight;
        Log.Error(string.Format("No spellData assigned to element {0}", (object) spellDefinition.element), 271, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
        return (CastHighlight) null;
      }
      if ((UnityEngine.Object) ((object) definition as CompanionDefinition) != (UnityEngine.Object) null)
        return this.m_uiCompanionCastData.castHighlight;
      Log.Error("Definition type not handled: " + definition.GetType().Name, 284, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
      return (CastHighlight) null;
    }

    public CastHighlight CreateCastHighlight<T>(T item, Transform parent) where T : ICastableStatus
    {
      ICastableDefinition definition = item.GetDefinition();
      CastHighlight castHighlight = this.GetCastHighlight<ICastableDefinition>(definition);
      if ((UnityEngine.Object) castHighlight == (UnityEngine.Object) null)
      {
        Log.Error(string.Format("No {0} prefab defined for {1}: {2}", (object) "CastHighlight", (object) item.GetType().Name, (object) definition), 297, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
        return (CastHighlight) null;
      }
      GameObjectPool gameObjectPool;
      if (!FightUIFactory.s_castHighlightPools.TryGetValue(castHighlight, out gameObjectPool))
      {
        gameObjectPool = new GameObjectPool(castHighlight.gameObject);
        FightUIFactory.s_castHighlightPools.Add(castHighlight, gameObjectPool);
      }
      CastHighlight component = gameObjectPool.Instantiate(parent).GetComponent<CastHighlight>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        FightUIFactory.s_castHighlightInstances.Add(new KeyValuePair<CastHighlight, GameObjectPool>(component, gameObjectPool));
        component.transform.localPosition = Vector3.zero;
        component.Play();
      }
      return component;
    }

    public void DestroyCellHighlight(CastHighlight cellCastHighlight)
    {
      if ((UnityEngine.Object) cellCastHighlight == (UnityEngine.Object) null)
        return;
      cellCastHighlight.Stop();
      for (int index = FightUIFactory.s_castHighlightInstances.Count - 1; index >= 0; --index)
      {
        KeyValuePair<CastHighlight, GameObjectPool> highlightInstance = FightUIFactory.s_castHighlightInstances[index];
        if ((UnityEngine.Object) highlightInstance.Key == (UnityEngine.Object) cellCastHighlight)
        {
          FightUIFactory.s_castHighlightInstances.RemoveAt(index);
          highlightInstance.Value.Release(cellCastHighlight.gameObject);
          return;
        }
      }
      Log.Error("no pool found", (UnityEngine.Object) cellCastHighlight, 337, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
      UnityEngine.Object.Destroy((UnityEngine.Object) cellCastHighlight.gameObject);
    }

    private GameObject GetCastFX<T>(T definition) where T : ICastableDefinition
    {
      SpellDefinition spellDefinition = (object) definition as SpellDefinition;
      if ((UnityEngine.Object) spellDefinition != (UnityEngine.Object) null)
      {
        FightUIFactory.CastItemData castItemData;
        if (this.m_uiSpellCastData.TryGetValue(spellDefinition.element, out castItemData))
          return castItemData.castingFX;
        Log.Error(string.Format("No spellData assigned to element {0}", (object) spellDefinition.element), 353, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
        return (GameObject) null;
      }
      if ((UnityEngine.Object) ((object) definition as CompanionDefinition) != (UnityEngine.Object) null)
        return this.m_uiCompanionCastData.castingFX;
      Log.Error("Definition type not handled: " + definition.GetType().Name, 366, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
      return (GameObject) null;
    }

    public GameObject CreateCastFX<T>(T item, Vector3 position, Quaternion rotation) where T : ICastableStatus
    {
      ICastableDefinition definition = item.GetDefinition();
      GameObject castFx1 = this.GetCastFX<ICastableDefinition>(definition);
      if ((UnityEngine.Object) castFx1 == (UnityEngine.Object) null)
      {
        Log.Error(string.Format("No castFX prefab defined for {0}: {1}", (object) item.GetType().Name, (object) definition), 376, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Gauges\\FightUIFactory.cs");
        return (GameObject) null;
      }
      GameObject castFx2 = UnityEngine.Object.Instantiate<GameObject>(castFx1);
      if ((UnityEngine.Object) castFx2 != (UnityEngine.Object) null)
      {
        castFx2.transform.position = position;
        castFx2.transform.rotation = rotation;
      }
      return castFx2;
    }

    public void DestroyCastFX(GameObject castFx)
    {
      if (!((UnityEngine.Object) castFx != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) castFx.gameObject);
    }

    [Serializable]
    private struct CastItemData
    {
      [SerializeField]
      private Material m_selectedMaterial;
      [SerializeField]
      private CastHighlight m_castHighlight;
      [SerializeField]
      private GameObject m_castingFX;

      public Material selectedMaterial => this.m_selectedMaterial;

      public CastHighlight castHighlight => this.m_castHighlight;

      public GameObject castingFX => this.m_castingFX;
    }

    [Serializable]
    private class ElementaryStatesSpriteDictionary : StatesDictionary<Sprite>
    {
    }

    [Serializable]
    private class ElementCaracIdSpriteDictionary : ElementCaracsDictionary<Sprite>
    {
    }

    [Serializable]
    private class SpellDataDictionary : ElementsDictionary<FightUIFactory.CastItemData>
    {
    }
  }
}
