// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.CellRenderer`2
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [RequireComponent(typeof (RectTransform))]
  public abstract class CellRenderer<T, U> : CellRenderer where U : ICellRendererConfigurator
  {
    protected T m_value;
    protected U m_configurator;

    public override object value => (object) this.m_value;

    protected abstract void SetValue(T value);

    protected abstract void Clear();

    public override void SetValue(object v)
    {
      if (v is T obj)
      {
        this.m_value = obj;
        this.SetValue(this.m_value);
      }
      else
      {
        this.m_value = default (T);
        this.Clear();
      }
      this.OnConfiguratorUpdate(true);
    }

    public override System.Type GetValueType() => typeof (T);

    public override void SetConfigurator(ICellRendererConfigurator configurator, bool andUpdate = true) => this.SetConfigurator((U) configurator, andUpdate);

    private void SetConfigurator(U configurator, bool andUpdate)
    {
      this.m_configurator = configurator;
      if (!andUpdate)
        return;
      this.OnConfiguratorUpdate(true);
    }

    public override CellRenderer Clone()
    {
      CellRenderer<T, U> cellRenderer = UnityEngine.Object.Instantiate<CellRenderer<T, U>>(this, this.rectTransform.parent);
      cellRenderer.m_value = this.m_value;
      return (CellRenderer) cellRenderer;
    }
  }
}
