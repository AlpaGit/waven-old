// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Info.FightInfoMessageRoot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.Info
{
  public class FightInfoMessageRoot : MonoBehaviour
  {
    private const int MaxRibbonCount = 5;
    [Header("TextFeedback")]
    [SerializeField]
    private FightInfoMessageRibbon m_ribbonReference;
    [SerializeField]
    private Color[] m_colors;
    private List<FightInfoMessageRibbon> m_ribbons;
    private List<FightInfoMessageRibbon> m_activeRibbons;
    private string[] m_colorsString;

    public void Start()
    {
      this.m_activeRibbons = new List<FightInfoMessageRibbon>();
      Color[] colors = this.m_colors;
      int length = this.m_colors.Length;
      string[] strArray = new string[length];
      for (int index = 0; index < length; ++index)
        strArray[index] = "#" + ColorUtility.ToHtmlStringRGBA(colors[index]);
      this.m_colorsString = strArray;
      List<FightInfoMessageRibbon> infoMessageRibbonList = new List<FightInfoMessageRibbon>();
      for (int index = 0; index < 5; ++index)
      {
        FightInfoMessageRibbon infoMessageRibbon = UnityEngine.Object.Instantiate<FightInfoMessageRibbon>(this.m_ribbonReference, this.m_ribbonReference.transform.parent);
        infoMessageRibbon.gameObject.SetActive(false);
        infoMessageRibbonList.Add(infoMessageRibbon);
      }
      this.m_ribbons = infoMessageRibbonList;
      this.m_ribbonReference.gameObject.SetActive(false);
    }

    public void BuildAndDrawScoreMessage(FightInfoMessage message, string playerOrigin)
    {
      FightInfoMessageRibbon ribbon = this.GetRibbon();
      if (!((UnityEngine.Object) ribbon != (UnityEngine.Object) null))
        return;
      ribbon.AddParameter(playerOrigin);
      ribbon.AddParameter(this.GetHTMLStringColor(message.ribbonGroup));
      this.DrawInfoMessage(ribbon, message, MessageInfoType.Score);
    }

    public void BuildAndDrawInfoMessage(FightInfoMessage message, params string[] parameters)
    {
      FightInfoMessageRibbon ribbon = this.GetRibbon();
      if (!((UnityEngine.Object) ribbon != (UnityEngine.Object) null))
        return;
      for (int index = 0; index < parameters.Length; ++index)
        ribbon.AddParameter(parameters[index]);
      ribbon.AddParameter(this.GetHTMLStringColor(message.ribbonGroup));
      this.DrawInfoMessage(ribbon, message, MessageInfoType.Default);
    }

    private void DrawInfoMessage(
      FightInfoMessageRibbon ribbon,
      FightInfoMessage message,
      MessageInfoType messageType)
    {
      ribbon.Initialise(messageType, (int) message.iconType, this.m_colors[(int) message.ribbonGroup], message.countValue);
      ribbon.PlayAnimation(message.id, new Action<FightInfoMessageRibbon>(this.ReleaseRibbon));
    }

    private FightInfoMessageRibbon GetRibbon()
    {
      List<FightInfoMessageRibbon> ribbons = this.m_ribbons;
      int count = ribbons.Count;
      if (count > 0)
      {
        FightInfoMessageRibbon infoMessageRibbon = ribbons[count - 1];
        ribbons.RemoveAt(count - 1);
        infoMessageRibbon.ClearParameters();
        infoMessageRibbon.SetExpectedIndex(this.m_activeRibbons.Count, false);
        this.m_activeRibbons.Add(infoMessageRibbon);
      }
      return (FightInfoMessageRibbon) null;
    }

    private void ReleaseRibbon(FightInfoMessageRibbon ribbon)
    {
      ribbon.gameObject.SetActive(false);
      List<FightInfoMessageRibbon> activeRibbons = this.m_activeRibbons;
      activeRibbons.Remove(ribbon);
      int count = activeRibbons.Count;
      for (int index = 0; index < count; ++index)
        activeRibbons[index].SetExpectedIndex(index, true);
      this.m_ribbons.Add(ribbon);
    }

    public Color GetColor(MessageInfoRibbonGroup group) => this.m_colors[(int) group];

    private string GetHTMLStringColor(MessageInfoRibbonGroup group) => this.m_colorsString[(int) group];
  }
}
