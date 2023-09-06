// Decompiled with JetBrains decompiler
// Type: SpellHighlight
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using Ankama.Utilities;
using System.Collections;
using UnityEngine;

public class SpellHighlight : MonoBehaviour
{
  [SerializeField]
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private Vector3 spriteScale;
  [SerializeField]
  private Vector2 spriteAnim;
  [SerializeField]
  private ParticleSystem fxAura;
  [SerializeField]
  private float killDelay1;
  [SerializeField]
  private float killDelay2;
  [SerializeField]
  private GameObject spell;
  private static Color startColor = new Color(1f, 1f, 1f, 0.0f);
  private static Color endColor = new Color(1f, 1f, 1f, 1f);
  private bool spawning;
  private bool dying;
  private bool spellLaunched;
  private SpellDraggerTest dragger;
  private Transform spriteTransform;

  private void Awake() => this.spriteTransform = this.spriteRenderer.transform;

  private void OnEnable()
  {
    this.spriteRenderer.color = SpellHighlight.startColor;
    this.StartCoroutine(this.SpawnCoroutine());
    this.dying = false;
    this.spellLaunched = false;
  }

  private void Update() => this.spriteTransform.localPosition = this.spriteTransform.localPosition with
  {
    y = Mathf.Sin(Time.time * this.spriteAnim.x) * this.spriteAnim.y
  };

  public void SetDragger(SpellDraggerTest SpellDraggerTest) => this.dragger = SpellDraggerTest;

  private IEnumerator SpawnCoroutine()
  {
    this.spawning = true;
    this.spriteTransform.localScale = Vector3.zero;
    this.fxAura.Play(true);
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime * 8f)
    {
      this.spriteRenderer.color = Color.Lerp(SpellHighlight.startColor, SpellHighlight.endColor, f);
      this.spriteTransform.localScale = Vector3.Lerp(Vector3.zero, this.spriteScale, (float) ((double) f * (double) f * (3.0 - 2.0 * (double) f)));
      yield return (object) wait;
    }
    this.spriteTransform.localScale = this.spriteScale;
    this.spriteRenderer.color = SpellHighlight.endColor;
    this.spawning = false;
  }

  public void Kill()
  {
    if (this.dying)
      return;
    this.dying = true;
    this.StartCoroutine(this.KillCoroutine());
  }

  public void LaunchSpell()
  {
    if (this.spellLaunched)
      return;
    this.spellLaunched = true;
    Object.Instantiate<GameObject>(this.spell, this.transform.position + new Vector3(0.0f, 0.5f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 1f));
    this.Kill();
  }

  private IEnumerator KillCoroutine()
  {
    SpellHighlight spellHighlight = this;
    spellHighlight.fxAura.Stop(true);
    yield return (object) new WaitForTime(spellHighlight.killDelay1);
    while (spellHighlight.spawning)
      yield return (object) null;
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime * 4f)
    {
      spellHighlight.spriteRenderer.color = Color.Lerp(SpellHighlight.endColor, SpellHighlight.startColor, f);
      yield return (object) wait;
    }
    spellHighlight.dragger.currentSpellHighlight = (SpellHighlight) null;
    spellHighlight.spriteRenderer.color = SpellHighlight.startColor;
    yield return (object) new WaitForTime(spellHighlight.killDelay2);
    Object.Destroy((Object) spellHighlight.gameObject);
  }
}
