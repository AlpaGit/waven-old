// Decompiled with JetBrains decompiler
// Type: GameObjectLoader
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using System.Collections;
using UnityEngine;

public class GameObjectLoader : AssetLoader<GameObject>
{
  private GameObject m_instantiatedObject;

  protected override IEnumerator Apply(GameObject asset, UIResourceDisplayMode displayMode)
  {
    GameObjectLoader gameObjectLoader = this;
    // ISSUE: reference to a compiler-generated method
    yield return (object) gameObjectLoader.\u003C\u003En__0(asset, displayMode);
    if ((Object) null != (Object) gameObjectLoader.m_instantiatedObject)
      Object.Destroy((Object) gameObjectLoader.m_instantiatedObject);
    gameObjectLoader.m_instantiatedObject = Object.Instantiate<GameObject>(asset, gameObjectLoader.transform);
    gameObjectLoader.m_instantiatedObject.transform.localPosition = Vector3.zero;
  }

  protected override IEnumerator Revert(UIResourceDisplayMode displayMode)
  {
    yield return (object) base.Revert(displayMode);
    if ((bool) (Object) this.m_instantiatedObject)
    {
      Object.Destroy((Object) this.m_instantiatedObject);
      this.m_instantiatedObject = (GameObject) null;
    }
  }
}
