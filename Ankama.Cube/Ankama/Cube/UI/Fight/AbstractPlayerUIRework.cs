// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.AbstractPlayerUIRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.CostModifiers;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Components;
using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public abstract class AbstractPlayerUIRework : MonoBehaviour
  {
    [SerializeField]
    private RawTextField m_playerName;
    [SerializeField]
    private Image m_rankIcon;
    [SerializeField]
    private ImageLoader m_heroIllustrationLoader;
    [SerializeField]
    private HeroLifeBarRework m_heroLifeBar;
    [SerializeField]
    private ActionPointCounterRework m_actionPointCounter;
    [SerializeField]
    private ElementaryPointsCounterRework m_elementaryPointsCounter;
    [SerializeField]
    protected ReservePointCounterRework m_reservePointCounter;
    private readonly Dictionary<int, SpellCostModification> m_spellCostModifiers = new Dictionary<int, SpellCostModification>();
    protected PlayerStatus m_playerStatus;

    public virtual void SetPlayerStatus(PlayerStatus playerStatus) => this.m_playerStatus = playerStatus;

    public void SetPlayerName([NotNull] string value) => this.m_playerName.SetText(value);

    public void SetRankIcon(int rank) => this.m_rankIcon.enabled = false;

    public void SetHeroIllustration(WeaponDefinition definition, Gender gender) => this.m_heroIllustrationLoader.Setup(definition.GetIllustrationReference(gender), "core/ui/characters/heroes");

    public void SetHeroStartLifePoints(int value, PlayerType playerType)
    {
      if (!((Object) null != (Object) this.m_heroLifeBar))
        return;
      this.m_heroLifeBar.SetStartLife(value, playerType);
    }

    public void ChangeHeroBaseLifePoints(int value)
    {
      if (!((Object) null != (Object) this.m_heroLifeBar))
        return;
      this.m_heroLifeBar.ChangeBaseLife(value);
    }

    public void ChangeHeroLifePoints(int value)
    {
      if (!((Object) null != (Object) this.m_heroLifeBar))
        return;
      this.m_heroLifeBar.ChangeLife(value);
    }

    public virtual void RefreshAvailableActions(bool recomputeSpellCosts)
    {
    }

    public virtual void UpdateAvailableActions(bool recomputeSpellCosts)
    {
    }

    public void SetActionPoints(int value)
    {
      if (!((Object) null != (Object) this.m_actionPointCounter))
        return;
      this.m_actionPointCounter.SetValue(value);
    }

    public void ChangeActionPoints(int value)
    {
      if (!((Object) null != (Object) this.m_actionPointCounter))
        return;
      this.m_actionPointCounter.ChangeValue(value);
    }

    public void PreviewActionPoints(int value, ValueModifier modifier)
    {
      if (!((Object) null != (Object) this.m_actionPointCounter))
        return;
      this.m_actionPointCounter.ShowPreview(value, modifier);
    }

    public void HidePreviewActionPoints(bool cancelled)
    {
      if (!((Object) null != (Object) this.m_actionPointCounter))
        return;
      this.m_actionPointCounter.HidePreview(cancelled);
    }

    public void SetupReserve(HeroStatus heroStatus, ReserveDefinition definition)
    {
      if (!((Object) null != (Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.Setup(heroStatus, definition);
    }

    public void SetReservePoints(int value)
    {
      if (!((Object) null != (Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.SetValue(value);
    }

    public virtual void ChangeReservePoints(int value)
    {
      if (!((Object) null != (Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.ChangeValue(value);
    }

    public void PreviewReservePoints(int value, ValueModifier modifier)
    {
      if (!((Object) null != (Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.ShowPreview(value, modifier);
    }

    public void HidePreviewReservePoints(bool cancelled)
    {
      if (!((Object) null != (Object) this.m_reservePointCounter))
        return;
      this.m_reservePointCounter.HidePreview(cancelled);
    }

    public void SetElementaryPoints(int air, int earth, int fire, int water)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.SetValues(air, earth, fire, water);
    }

    public void ChangeAirElementaryPoints(int value)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ChangeAirValue(value);
    }

    public void ShowPreviewAir(int value, ValueModifier modifier)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ShowPreviewAir(value, modifier);
    }

    public void HidePreviewAir(bool cancelled)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.HidePreviewAir(cancelled);
    }

    public void ChangeEarthElementaryPoints(int value)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ChangeEarthValue(value);
    }

    public void ShowPreviewEarth(int value, ValueModifier modifier)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ShowPreviewEarth(value, modifier);
    }

    public void HidePreviewEarth(bool cancelled)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.HidePreviewEarth(cancelled);
    }

    public void ChangeFireElementaryPoints(int value)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ChangeFireValue(value);
    }

    public void ShowPreviewFire(int value, ValueModifier modifier)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ShowPreviewFire(value, modifier);
    }

    public void HidePreviewFire(bool cancelled)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.HidePreviewFire(cancelled);
    }

    public void ChangeWaterElementaryPoints(int value)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ChangeWaterValue(value);
    }

    public void ShowPreviewWater(int value, ValueModifier modifier)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.ShowPreviewWater(value, modifier);
    }

    public void HidePreviewWater(bool cancelled)
    {
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.HidePreviewWater(cancelled);
    }

    public abstract void SetUIInteractable(bool interactable);

    public abstract void AddSpellStatus(SpellInfo spellInfo, int index);

    public abstract void RemoveSpellStatus(int spellInstanceId, int index);

    public abstract IEnumerator AddSpell(SpellInfo spellInfo, int index);

    public abstract IEnumerator RemoveSpell(int spellInstanceId, int index);

    public virtual IEnumerator AddSpellCostModifier(SpellCostModification spellCostModifier)
    {
      int id = spellCostModifier.id;
      if (this.m_spellCostModifiers.ContainsKey(id))
      {
        Log.Error(string.Format("Tried to add spell cost modifier with id {0} multiple times.", (object) id), 314, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\PlayerUI\\AbstractPlayerUIRework.cs");
      }
      else
      {
        this.m_spellCostModifiers.Add(spellCostModifier.id, spellCostModifier);
        yield break;
      }
    }

    public virtual IEnumerator RemoveSpellCostModifier(int spellCostModifierId)
    {
      if (!this.m_spellCostModifiers.Remove(spellCostModifierId))
      {
        Log.Error(string.Format("Tried to remove spell cost modifier with id {0} but it does not exist.", (object) spellCostModifierId), 325, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\PlayerUI\\AbstractPlayerUIRework.cs");
        yield break;
      }
    }

    public abstract void RefreshAvailableCompanions();

    public abstract IEnumerator UpdateAvailableCompanions();

    public abstract void AddCompanionStatus(int companionDefinitionId, int level, int index);

    public abstract void AddAdditionalCompanionStatus(
      PlayerStatus owner,
      int companionDefinitionId,
      int level);

    public abstract void RemoveAdditionalCompanionStatus(int companionDefinitionId);

    public abstract void ChangeCompanionStateStatus(
      int companionDefinitionId,
      CompanionReserveState state);

    public abstract IEnumerator AddCompanion(int companionDefinitionId, int level, int index);

    public abstract IEnumerator AddAdditionalCompanion(
      PlayerStatus owner,
      int companionDefinitionId,
      int level);

    public abstract IEnumerator RemoveAdditionalCompanion(int companionDefinitionId);

    public abstract IEnumerator ChangeCompanionState(
      int companionDefinitionId,
      CompanionReserveState state);

    public void HideAllPreviews(bool cancelled)
    {
      if ((Object) null != (Object) this.m_actionPointCounter)
        this.m_actionPointCounter.HidePreview(cancelled);
      if ((Object) null != (Object) this.m_reservePointCounter)
        this.m_reservePointCounter.HidePreview(cancelled);
      if (!((Object) null != (Object) this.m_elementaryPointsCounter))
        return;
      this.m_elementaryPointsCounter.HidePreviewAir(cancelled);
      this.m_elementaryPointsCounter.HidePreviewFire(cancelled);
      this.m_elementaryPointsCounter.HidePreviewEarth(cancelled);
      this.m_elementaryPointsCounter.HidePreviewWater(cancelled);
    }
  }
}
