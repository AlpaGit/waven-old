// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.PopupMenu
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class PopupMenu : MonoBehaviour
  {
    [SerializeField]
    private ContainerDrawer m_drawer;
    [SerializeField]
    private Button m_closeButton;
    [SerializeField]
    private GameObject m_closeContainer;

    private void Awake()
    {
      this.m_drawer.open = false;
      this.m_closeContainer.SetActive(false);
      this.m_closeButton.onClick.AddListener(new UnityAction(this.OnClose));
    }

    private void OnClose() => this.Close();

    public void Open()
    {
      this.m_drawer.Open();
      this.m_closeContainer.SetActive(true);
    }

    public void Close()
    {
      this.m_drawer.Close();
      this.m_closeContainer.SetActive(false);
    }
  }
}
