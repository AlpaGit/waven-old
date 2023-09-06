// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.CardNumberCounterRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public sealed class CardNumberCounterRework : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField]
    private UISpriteTextRenderer m_text;
    private int m_count;

    public IEnumerator Increment()
    {
      ++this.m_count;
      this.m_text.text = string.Format("x{0}", (object) this.m_count);
      yield break;
    }

    public IEnumerator Decrement()
    {
      --this.m_count;
      this.m_text.text = string.Format("x{0}", (object) this.m_count);
      yield break;
    }
  }
}
