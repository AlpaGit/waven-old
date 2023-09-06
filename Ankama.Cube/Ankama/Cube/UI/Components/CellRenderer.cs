// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.CellRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  public abstract class CellRenderer : MonoBehaviour
  {
    private RectTransform m_rectTransform;
    private DragNDropClient m_dragNDropClient;

    public RectTransform rectTransform
    {
      get
      {
        if ((UnityEngine.Object) this.m_rectTransform != (UnityEngine.Object) null)
          return this.m_rectTransform;
        this.m_rectTransform = this.GetComponent<RectTransform>();
        if ((UnityEngine.Object) this.m_rectTransform == (UnityEngine.Object) null)
          Log.Warning("CellRenderer without RectTransform !!! Name: " + this.name, 76, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\List\\CellRenderer.cs");
        return this.m_rectTransform;
      }
    }

    public abstract System.Type GetValueType();

    public abstract void SetValue(object value);

    public abstract void SetConfigurator(ICellRendererConfigurator configurator, bool andUpdate = true);

    public abstract void OnConfiguratorUpdate(bool instant);

    public virtual Sequence DestroySequence() => (Sequence) null;

    public abstract object value { get; }

    public DragNDropClient dragNDropClient
    {
      get => this.m_dragNDropClient;
      set => this.m_dragNDropClient = value;
    }

    public abstract CellRenderer Clone();
  }
}
