// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.PlaySpellCompanionUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.UI.DeckMaker;
using Ankama.Utilities;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public sealed class PlaySpellCompanionUI : MonoBehaviour
  {
    [SerializeField]
    private Canvas m_fightUICanvas;
    [SerializeField]
    private CastingModeParameters m_castingModeParameters;
    [SerializeField]
    private FightUIFactory m_factory;
    [SerializeField]
    private SpellStatusCellRenderer m_spellDummy;
    [SerializeField]
    private CompanionStatusCellRenderer m_companionDummy;
    [SerializeField]
    private float m_zoomScale = 0.75f;
    private GameObjectPool m_spellDummyPool;
    private GameObjectPool m_companionDummyPool;
    private CastableDragNDropElement m_currentDnd;
    private CellObject m_currentCell;

    private void Awake()
    {
      GameObject gameObject1 = this.m_spellDummy.gameObject;
      GameObject gameObject2 = this.m_companionDummy.gameObject;
      gameObject1.SetActive(false);
      gameObject2.SetActive(false);
      this.m_spellDummyPool = new GameObjectPool(gameObject1);
      this.m_companionDummyPool = new GameObjectPool(gameObject2);
    }

    public IEnumerator ShowPlaying(SpellStatus spell, CellObject cell)
    {
      PlaySpellCompanionUI spellCompanionUi = this;
      if (spellCompanionUi.m_spellDummyPool == null)
      {
        Log.Warning("PlaySpellCompanionUI is inactive.", 46, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\PlaySpellCompanionUI.cs");
      }
      else
      {
        GameObject dummy = spellCompanionUi.m_spellDummyPool.Instantiate(spellCompanionUi.transform, false);
        dummy.transform.localScale = spellCompanionUi.m_spellDummy.transform.localScale;
        SpellStatusCellRenderer itemUI = dummy.GetComponent<SpellStatusCellRenderer>();
        CastableDragNDropElement dnd = dummy.GetComponent<CastableDragNDropElement>();
        itemUI.SetValue((object) spell);
        yield return (object) itemUI.WaitForImage();
        yield return (object) spellCompanionUi.ShowPlaying((ICastableStatus) spell, dnd, cell);
        itemUI.SetValue((object) null);
        spellCompanionUi.m_spellDummyPool.Release(dummy);
      }
    }

    public IEnumerator ShowPlaying(ReserveCompanionStatus companion, CellObject cell)
    {
      PlaySpellCompanionUI spellCompanionUi = this;
      if (spellCompanionUi.m_companionDummyPool == null)
      {
        Log.Warning("PlaySpellCompanionUI is inactive.", 67, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\PlaySpellCompanionUI.cs");
      }
      else
      {
        GameObject dummy = spellCompanionUi.m_companionDummyPool.Instantiate(spellCompanionUi.transform, false);
        dummy.transform.localScale = spellCompanionUi.m_companionDummy.transform.localScale;
        CompanionStatusCellRenderer itemUI = dummy.GetComponent<CompanionStatusCellRenderer>();
        CastableDragNDropElement dnd = dummy.GetComponent<CastableDragNDropElement>();
        itemUI.SetValue((object) companion);
        yield return (object) itemUI.WaitForImage();
        yield return (object) spellCompanionUi.ShowPlaying((ICastableStatus) companion, dnd, cell);
        itemUI.SetValue((object) null);
        spellCompanionUi.m_companionDummyPool.Release(dummy);
      }
    }

    private IEnumerator ShowPlaying(
      ICastableStatus item,
      CastableDragNDropElement dnd,
      CellObject cell)
    {
      this.m_currentDnd = dnd;
      this.m_currentCell = cell;
      dnd.gameObject.SetActive(true);
      CastHighlight cellHighlight = this.m_factory.CreateCastHighlight<ICastableStatus>(item, cell.highlight.transform);
      Tween tween = dnd.PlayCastImmediate(FightUIRework.WorldToUIWorld(cell.highlight.transform.parent.position), CameraHandler.current.zoomScale * this.m_zoomScale, this.m_fightUICanvas.transform);
      while (tween.active && !tween.IsComplete())
        yield return (object) null;
      yield return (object) new WaitForTime(this.m_castingModeParameters.opponentPlayingDuration);
      this.m_factory.DestroyCellHighlight(cellHighlight);
      yield return (object) new WaitForTime(this.m_castingModeParameters.opponentCastPlayingDuration);
      Tween endCastImmediate = dnd.EndCastImmediate();
      while (endCastImmediate.active && !endCastImmediate.IsComplete())
        yield return (object) null;
      dnd.gameObject.SetActive(false);
      this.m_currentDnd = (CastableDragNDropElement) null;
      this.m_currentCell = (CellObject) null;
    }

    private void Update()
    {
      if ((Object) this.m_currentDnd == (Object) null || (Object) this.m_currentCell == (Object) null)
        return;
      float zoomFactor = CameraHandler.current.zoomScale * this.m_zoomScale;
      this.m_currentDnd.UpdateCastAnimationPosition(FightUIRework.WorldToUIWorld(this.m_currentCell.highlight.transform.parent.position), zoomFactor);
    }
  }
}
