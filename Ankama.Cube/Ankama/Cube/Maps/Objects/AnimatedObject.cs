// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.AnimatedObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Animations.Events;
using Ankama.Cube.Render;
using Ankama.Cube.SRP;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  [SelectionBase]
  [ExecuteInEditMode]
  public class AnimatedObject : MonoBehaviour
  {
    public const float AnimatedObjectScale = 0.01f;
    public const int MinFrameCountDelay = 15;
    [SerializeField]
    private AnimatedObjectDefinition m_animatedObjectDefinition;
    [SerializeField]
    private string m_defaultAnimName;
    [SerializeField]
    private bool m_invertAxis;
    private Animator2D m_animator2D;

    private void OnEnable()
    {
      this.m_animator2D = this.CreateAnimatorComponent();
      if (!((Object) this.m_animatedObjectDefinition != (Object) null) || !((Object) this.m_animatedObjectDefinition != (Object) null))
        return;
      this.m_animator2D.Initialised += new Animator2DInitialisedEventHandler(this.InitCallback);
      this.m_animator2D.SetDefinition(this.m_animatedObjectDefinition);
    }

    private void OnDisable()
    {
      if (!((Object) null != (Object) this.m_animator2D))
        return;
      DestroyUtility.Destroy((Object) this.m_animator2D.gameObject);
    }

    public void PlayAnim(string animName, bool loop, bool randomStartFrame = false)
    {
      if ((Object) this.m_animator2D == (Object) null || string.IsNullOrEmpty(animName))
        return;
      this.m_animator2D.SetAnimation(animName, loop, false, true);
      if (!randomStartFrame)
        return;
      this.m_animator2D.currentFrame = Random.Range(0, Mathf.Min(15, this.m_animator2D.animationFrameCount));
    }

    private void InitCallback(object sender, Animator2DInitialisedEventArgs e)
    {
      this.m_animator2D.Initialised -= new Animator2DInitialisedEventHandler(this.InitCallback);
      this.PlayAnim(this.GetDefaultAnim(), true, true);
    }

    private string GetDefaultAnim()
    {
      if (!string.IsNullOrEmpty(this.m_defaultAnimName))
        return this.m_defaultAnimName;
      if ((Object) this.m_animatedObjectDefinition == (Object) null || (Object) this.m_animatedObjectDefinition == (Object) null)
        return (string) null;
      if (!string.IsNullOrEmpty(this.m_animatedObjectDefinition.defaultAnimationName))
        return this.m_animatedObjectDefinition.defaultAnimationName;
      return this.m_animatedObjectDefinition.animationCount > 0 ? this.m_animatedObjectDefinition.GetAnimationName(0) : (string) null;
    }

    private Animator2D CreateAnimatorComponent()
    {
      GameObject gameObject = new GameObject("Animator2D Hidden");
      gameObject.transform.SetParent(this.transform, false);
      gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
      gameObject.transform.localRotation = Quaternion.Euler(0.0f, this.m_invertAxis ? 225f : 45f, 0.0f);
      Animator2D animatorComponent = gameObject.AddComponent<Animator2D>();
      gameObject.AddComponent<CharacterMeshShaderProperties>();
      return animatorComponent;
    }
  }
}
