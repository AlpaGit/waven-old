// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StatPlayer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Components;
using Ankama.Cube.Utility;
using Google.Protobuf.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class StatPlayer : AbstractStat
  {
    [SerializeField]
    private Transform m_illuContainer;
    [SerializeField]
    private ImageLoader m_illuImageLoader;
    [SerializeField]
    private RawTextField m_nameText;
    [SerializeField]
    private Image m_crownImage;
    [SerializeField]
    private RawTextField m_bestPlayerText;
    [SerializeField]
    private List<Image> m_titleIcons;
    [SerializeField]
    private Image m_deadIcon;
    [SerializeField]
    private StatData m_statData;

    public IEnumerator Init(StatBoard.PlayerStatData statData)
    {
      this.m_nameText.SetText(statData.name);
      Color color = statData.ally ? this.m_statData.allyColor : this.m_statData.opponentColor;
      this.m_nameText.color = color;
      this.m_bestPlayerText.color = color;
      this.m_crownImage.color = color;
      GameStatistics.Types.PlayerStats playerStats = statData.playerStats;
      bool flag = statData.playerStats.Titles.Contains(0);
      this.m_bestPlayerText.gameObject.SetActive(flag);
      this.m_illuContainer.localScale = Vector3.one * (flag ? this.m_statData.illuMvpScale : this.m_statData.illuNeutralScale);
      int num;
      if (playerStats.Stats.TryGetValue(12, out num))
        this.m_deadIcon.gameObject.SetActive(num > 0);
      else
        this.m_deadIcon.gameObject.SetActive(true);
      yield return (object) this.LoadIllu(statData.weaponDefinition);
    }

    private void SetTitles(RepeatedField<int> titles)
    {
      int num = 0;
      for (int index = 0; index < titles.Count; ++index)
      {
        FightStatType title = (FightStatType) titles[index];
        if (title != FightStatType.Mvp)
        {
          if (index >= this.m_titleIcons.Count)
          {
            Image image = Object.Instantiate<Image>(this.m_titleIcons[0]);
            image.transform.SetParent(this.m_titleIcons[0].transform.parent);
            this.m_titleIcons.Add(image);
          }
          Image titleIcon = this.m_titleIcons[index];
          titleIcon.gameObject.SetActive(true);
          titleIcon.sprite = this.m_statData.GetIcon(title);
          ++num;
        }
      }
      for (int index = num; index < this.m_titleIcons.Count; ++index)
        this.m_titleIcons[index].gameObject.SetActive(false);
    }

    private IEnumerator LoadIllu(WeaponDefinition weaponDefinition)
    {
      this.m_illuImageLoader.Setup(weaponDefinition.GetIllustrationReference(), AssetBundlesUtility.GetUICharacterResourcesBundleName());
      yield break;
    }
  }
}
