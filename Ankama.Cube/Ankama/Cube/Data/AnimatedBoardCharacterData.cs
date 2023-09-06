// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AnimatedBoardCharacterData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public abstract class AnimatedBoardCharacterData : AnimatedCharacterData
  {
    [SerializeField]
    private AnimatedObjectDefinition m_animatedObjectDefinition;
    [SerializeField]
    private CharacterHeight m_height = CharacterHeight.Normal;

    public AnimatedObjectDefinition animatedObjectDefinition => this.m_animatedObjectDefinition;

    public CharacterHeight height => this.m_height;
  }
}
