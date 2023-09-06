// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Render.SpriteRendererGroup
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Render
{
  [ExecuteInEditMode]
  public class SpriteRendererGroup : MonoBehaviour, IUIResourceConsumer
  {
    [SerializeField]
    private float m_apha;
    private SpriteRenderer[] m_renderers;
    private float m_currentAlpha;
    private Coroutine m_appearRoutine;

    private void OnEnable() => this.RebuildRenderersArray();

    public void RebuildRenderersArray()
    {
      this.m_apha = 0.0f;
      this.m_renderers = this.GetComponentsInChildren<SpriteRenderer>();
      this.SetAlpha();
    }

    private void Update()
    {
      if ((double) this.m_currentAlpha == (double) this.m_apha)
        return;
      this.SetAlpha();
    }

    private void SetAlpha()
    {
      this.m_currentAlpha = this.m_apha;
      for (int index = this.m_renderers.Length - 1; index >= 0; --index)
      {
        SpriteRenderer renderer = this.m_renderers[index];
        if (!((Object) renderer == (Object) null))
        {
          Color color = renderer.color with
          {
            a = this.m_currentAlpha
          };
          renderer.color = color;
        }
      }
    }

    public UIResourceDisplayMode Register(IUIResourceProvider provider) => UIResourceDisplayMode.Immediate;

    public void UnRegister(IUIResourceProvider provider)
    {
      if (this.m_appearRoutine != null)
        this.StopCoroutine(this.m_appearRoutine);
      this.RebuildRenderersArray();
    }

    public void PlayAppear() => this.m_appearRoutine = this.StartCoroutine(this.Alphatween());

    private IEnumerator Alphatween()
    {
      float step = 4f;
      while ((double) this.m_apha < 1.0)
      {
        this.m_apha += step * Time.deltaTime;
        yield return (object) null;
      }
    }
  }
}
