// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Test.UIManagerTestUI
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.Test
{
  public class UIManagerTestUI : BaseOpenCloseUI
  {
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private ParticleSystem m_particles;
    [SerializeField]
    private RectTransform m_window;
    [SerializeField]
    private Toggle m_useBlurToggle;
    [SerializeField]
    private CustomDropdown m_creationOptionDropDown;
    [SerializeField]
    private Button m_createButton;
    [SerializeField]
    private Button m_removeButton;
    public Action<UIManagerTestUI, UIManagerTestCreationParams> onCreate;
    public Action<UIManagerTestUI> onRemove;
    private static int s_id;

    private void Start()
    {
      Color color = UnityEngine.Random.ColorHSV();
      this.m_particles.main.startColor = (ParticleSystem.MinMaxGradient) color;
      color.a = 0.4f;
      this.m_background.color = color;
      this.m_creationOptionDropDown.ClearOptions();
      this.m_creationOptionDropDown.AddOptions(((IEnumerable<string>) Enum.GetNames(typeof (UIManagerTestCreationOption))).ToList<string>());
      this.m_createButton.onClick.AddListener(new UnityAction(this.OnCreateClick));
      this.m_removeButton.onClick.AddListener((UnityAction) (() =>
      {
        Action<UIManagerTestUI> onRemove = this.onRemove;
        if (onRemove == null)
          return;
        onRemove(this);
      }));
      this.gameObject.name = string.Format("{0} {1}", (object) this.gameObject.name, (object) UIManagerTestUI.s_id);
      Vector2 vector2_1 = Vector2.zero;
      Vector2 vector2_2 = Vector2.one;
      switch (UIManagerTestUI.s_id % 4)
      {
        case 0:
          vector2_1 = new Vector2(0.0f, 0.5f);
          vector2_2 = new Vector2(0.5f, 1f);
          break;
        case 1:
          vector2_1 = new Vector2(0.5f, 0.5f);
          vector2_2 = new Vector2(1f, 1f);
          break;
        case 2:
          vector2_1 = new Vector2(0.0f, 0.0f);
          vector2_2 = new Vector2(0.5f, 0.5f);
          break;
        case 3:
          vector2_1 = new Vector2(0.5f, 0.0f);
          vector2_2 = new Vector2(1f, 0.5f);
          break;
      }
      this.m_window.anchorMin = vector2_1;
      this.m_window.anchorMax = vector2_2;
      ++UIManagerTestUI.s_id;
    }

    private void OnCreateClick()
    {
      UIManagerTestCreationParams testCreationParams = new UIManagerTestCreationParams()
      {
        useBlur = this.m_useBlurToggle.isOn,
        option = (UIManagerTestCreationOption) this.m_creationOptionDropDown.value
      };
      Action<UIManagerTestUI, UIManagerTestCreationParams> onCreate = this.onCreate;
      if (onCreate == null)
        return;
      onCreate(this, testCreationParams);
    }
  }
}
