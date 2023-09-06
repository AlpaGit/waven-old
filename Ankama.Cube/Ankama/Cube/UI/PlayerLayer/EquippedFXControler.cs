// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.EquippedFXControler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class EquippedFXControler : MonoBehaviour
  {
    [SerializeField]
    private List<EquippedFXControler.ColorByElement> m_elementToColor;
    [SerializeField]
    private List<Image> m_ImageToColor;

    public void SetEquipped(bool b)
    {
      this.gameObject.SetActive(b);
      if (!b)
        return;
      foreach (Image target in this.m_ImageToColor)
        DOTweenModuleUI.DOFade(target, 1f, 0.1f);
    }

    public void SetElement(Element element)
    {
      Color color = Color.white;
      foreach (EquippedFXControler.ColorByElement colorByElement in this.m_elementToColor)
      {
        if (colorByElement.Element == element)
          color = colorByElement.Color;
      }
      foreach (Graphic graphic in this.m_ImageToColor)
        graphic.color = color;
    }

    [Serializable]
    public struct ColorByElement
    {
      [SerializeField]
      public Element Element;
      public Color Color;
    }
  }
}
