// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.SpriteAnimation
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Animations
{
  public class SpriteAnimation : MonoBehaviour
  {
    [SerializeField]
    private Sprite[] m_sprites;
    [SerializeField]
    private SpriteRenderer[] m_spriteRenderers;
    [SerializeField]
    private float m_animationSpeed;
    private float m_animation;
    private float m_animationLength;

    private void Awake() => this.m_animationLength = (float) this.m_sprites.Length;

    private void Update()
    {
      this.m_animation = Mathf.Repeat(this.m_animation + this.m_animationSpeed * Time.deltaTime, this.m_animationLength);
      Sprite sprite = this.m_sprites[Mathf.FloorToInt(this.m_animation)];
      for (int index = 0; index < this.m_spriteRenderers.Length; ++index)
        this.m_spriteRenderers[index].sprite = sprite;
    }
  }
}
