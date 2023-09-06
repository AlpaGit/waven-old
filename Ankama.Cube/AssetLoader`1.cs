// Decompiled with JetBrains decompiler
// Type: AssetLoader`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using System.Collections;
using UnityEngine;

public class AssetLoader<T> : UIResourceLoader<T> where T : Object
{
  private T m_asset;

  public T GetAsset() => this.m_asset;

  protected override IEnumerator Apply(T asset, UIResourceDisplayMode displayMode)
  {
    this.m_asset = asset;
    yield break;
  }

  protected override IEnumerator Revert(UIResourceDisplayMode displayMode)
  {
    this.m_asset = default (T);
    yield break;
  }
}
