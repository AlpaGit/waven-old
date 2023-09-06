// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.SpriteFilling
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  [ExecuteInEditMode]
  [RequireComponent(typeof (Sprite))]
  public sealed class SpriteFilling : MonoBehaviour
  {
    [SerializeField]
    [UsedImplicitly]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    [UsedImplicitly]
    private SpriteRenderer m_backgroundReference;
    [SerializeField]
    [UsedImplicitly]
    private Vector2 m_offset;
    [SerializeField]
    [UsedImplicitly]
    private Vector2 m_padding;
    [SerializeField]
    [UsedImplicitly]
    private TextAnchor m_anchor = TextAnchor.MiddleLeft;
    [SerializeField]
    [UsedImplicitly]
    [Range(0.0f, 1f)]
    private float m_value;

    [PublicAPI]
    public Sprite sprite
    {
      get => this.m_spriteRenderer.sprite;
      set => this.m_spriteRenderer.sprite = value;
    }

    [PublicAPI]
    public SpriteRenderer backgroundReference
    {
      get => this.m_backgroundReference;
      set
      {
        if (!((UnityEngine.Object) this.m_backgroundReference != (UnityEngine.Object) value))
          return;
        this.m_backgroundReference = value;
        this.Refresh();
      }
    }

    [PublicAPI]
    public TextAnchor anchor
    {
      get => this.m_anchor;
      set
      {
        if (this.m_anchor == value)
          return;
        this.m_anchor = value;
        this.Refresh();
      }
    }

    [PublicAPI]
    public Vector2 offset
    {
      get => this.m_offset;
      set
      {
        if (!(this.m_offset != value))
          return;
        this.m_offset = value;
        this.Refresh();
      }
    }

    [PublicAPI]
    public Vector2 padding
    {
      get => this.m_padding;
      set
      {
        if (!(this.m_padding != value))
          return;
        this.m_padding = value;
        this.Refresh();
      }
    }

    [PublicAPI]
    public float value
    {
      get => this.m_value;
      set
      {
        value = Mathf.Clamp01(value);
        if ((double) this.m_value == (double) value)
          return;
        this.m_value = value;
        this.Refresh();
      }
    }

    private void OnEnable()
    {
      if (this.m_spriteRenderer.drawMode != SpriteDrawMode.Sliced)
        this.m_spriteRenderer.drawMode = SpriteDrawMode.Sliced;
      this.Refresh();
    }

    private void Refresh()
    {
      SpriteRenderer backgroundReference = this.m_backgroundReference;
      if ((UnityEngine.Object) null == (UnityEngine.Object) backgroundReference)
        Log.Warning("Missing background reference on SpriteFilling named '" + this.name + "'.", 136, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\UI\\SpriteFilling.cs");
      else if (backgroundReference.drawMode != SpriteDrawMode.Sliced)
      {
        Log.Warning(string.Format("Draw mode of background reference assigned for {0} named '{1}' is not {2}.", (object) nameof (SpriteFilling), (object) this.name, (object) SpriteDrawMode.Sliced), 142, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\UI\\SpriteFilling.cs");
      }
      else
      {
        Transform transform = this.m_spriteRenderer.transform;
        Vector2 vector2_1 = backgroundReference.size - this.m_padding;
        Vector2 vector2_2;
        Vector2 vector2_3;
        switch (this.m_anchor)
        {
          case TextAnchor.UpperLeft:
            vector2_2 = vector2_1 * this.m_value;
            vector2_3 = (vector2_1 - vector2_2) * new Vector2(-0.5f, 0.5f);
            break;
          case TextAnchor.UpperCenter:
            vector2_2 = new Vector2(vector2_1.x, vector2_1.y * this.m_value);
            vector2_3 = new Vector2(0.0f, (float) (((double) vector2_1.y - (double) vector2_2.y) * 0.5));
            break;
          case TextAnchor.UpperRight:
            vector2_2 = vector2_1 * this.m_value;
            vector2_3 = (vector2_1 - vector2_2) * 0.5f;
            break;
          case TextAnchor.MiddleLeft:
            vector2_2 = new Vector2(vector2_1.x * this.m_value, vector2_1.y);
            vector2_3 = new Vector2((float) (((double) vector2_1.x - (double) vector2_2.x) * -0.5), 0.0f);
            break;
          case TextAnchor.MiddleCenter:
            vector2_2 = vector2_1 * this.m_value;
            vector2_3 = Vector2.zero;
            break;
          case TextAnchor.MiddleRight:
            vector2_2 = new Vector2(vector2_1.x * this.m_value, vector2_1.y);
            vector2_3 = new Vector2((float) (((double) vector2_1.x - (double) vector2_2.x) * 0.5), 0.0f);
            break;
          case TextAnchor.LowerLeft:
            vector2_2 = vector2_1 * this.m_value;
            vector2_3 = (vector2_1 - vector2_2) * new Vector2(-0.5f, -0.5f);
            break;
          case TextAnchor.LowerCenter:
            vector2_2 = vector2_1 * this.m_value;
            vector2_3 = -0.5f * vector2_1 + 0.5f * vector2_2;
            break;
          case TextAnchor.LowerRight:
            vector2_2 = vector2_1 * this.m_value;
            vector2_3 = (vector2_1 - vector2_2) * new Vector2(0.5f, -0.5f);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        Vector2 vector2_4 = vector2_3 + this.offset * vector2_1;
        this.m_spriteRenderer.size = vector2_2;
        transform.localPosition = new Vector3(vector2_4.x, vector2_4.y, transform.localPosition.z);
      }
    }
  }
}
