// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.MatchMaking.MatchMakingButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.TEMPFastEnterMatch.MatchMaking
{
  public class MatchMakingButton : MonoBehaviour
  {
    [SerializeField]
    private Button m_btn;
    [SerializeField]
    private Button m_forceAiBtn;
    [SerializeField]
    private Text m_text;
    public int fightDefId;
    private string m_buttonText;
    private bool m_isSearching;

    public bool isSearching => this.m_isSearching;

    private string GetButtonText()
    {
      if (this.m_buttonText == null)
        this.m_buttonText = RuntimeData.fightDefinitions[this.fightDefId].displayName;
      return this.m_buttonText;
    }

    public Button button => this.m_btn;

    public Button forceAiBUtton => this.m_forceAiBtn;

    public void StartWait()
    {
      this.m_text.text = "Cancel " + this.GetButtonText();
      this.m_forceAiBtn.gameObject.SetActive(true);
      this.m_isSearching = true;
    }

    public void StopWait()
    {
      this.m_text.text = this.GetButtonText();
      this.m_forceAiBtn.gameObject.SetActive(false);
      this.m_isSearching = false;
    }
  }
}
