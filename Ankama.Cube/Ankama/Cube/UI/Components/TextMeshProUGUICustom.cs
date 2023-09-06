// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.TextMeshProUGUICustom
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using TMPro;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [AddComponentMenu("")]
  public class TextMeshProUGUICustom : TextMeshProUGUI
  {
    protected override void Awake()
    {
      this.text = "";
      base.Awake();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      TMP_SubMeshUI[] subTextObjects = this.m_subTextObjects;
      int length = subTextObjects.Length;
      for (int index = 1; index < length; ++index)
      {
        TMP_SubMeshUI tmpSubMeshUi = subTextObjects[index];
        if ((Object) null == (Object) tmpSubMeshUi)
          break;
        Object.Destroy((Object) tmpSubMeshUi.gameObject);
        subTextObjects[index] = (TMP_SubMeshUI) null;
      }
    }
  }
}
