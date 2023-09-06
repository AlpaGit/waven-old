// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterUILayoutElement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  public abstract class CharacterUILayoutElement : MonoBehaviour, ICharacterUI
  {
    private CharacterUIContainer m_container;
    protected Color m_color = Color.white;
    protected int m_sortingOrder;
    protected int m_layoutPosition;

    public abstract Color color { get; set; }

    public abstract int sortingOrder { get; set; }

    public int layoutWidth { get; protected set; }

    public abstract void SetLayoutPosition(int left);

    public void SetContainer(CharacterUIContainer container) => this.m_container = container;

    protected virtual void Layout() => this.m_container.SetLayoutDirty();

    protected static float LayoutSetTransform([NotNull] SpriteRenderer spriteRenderer, float position)
    {
      Sprite sprite = spriteRenderer.sprite;
      float num1;
      float num2;
      if ((Object) null == (Object) sprite)
      {
        num1 = 0.0f;
        num2 = 0.0f;
      }
      else
      {
        float pixelsPerUnit = sprite.pixelsPerUnit;
        num1 = sprite.rect.width / pixelsPerUnit;
        num2 = sprite.pivot.x / pixelsPerUnit;
      }
      Transform transform = spriteRenderer.transform;
      transform.localPosition = transform.localPosition with
      {
        x = position + num2
      };
      return num1;
    }

    protected static float LayoutSetTransform(SpriteTextRenderer spriteRenderer, float position)
    {
      Transform transform = spriteRenderer.transform;
      transform.localPosition = transform.localPosition with
      {
        x = position
      };
      return Mathf.Ceil(100f * spriteRenderer.textBounds.max.x) / 100f;
    }

    protected static void LayoutMoveTransform(Transform t, float delta)
    {
      Vector3 localPosition = t.localPosition;
      localPosition.x += delta;
      t.localPosition = localPosition;
    }
  }
}
