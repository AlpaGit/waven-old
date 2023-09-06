// Decompiled with JetBrains decompiler
// Type: DataEditor.EditableData
// Assembly: DataEditorRuntime, Version=1.0.6990.32389, Culture=neutral, PublicKeyToken=null
// MVID: 45C45C6B-0733-4518-B038-C58DEC652313
// Assembly location: E:\WAVEN\Waven_Data\Managed\DataEditorRuntime.dll

using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace DataEditor
{
  public abstract class EditableData : ScriptableObject
  {
    public const int IdNull = 0;
    private int m_id;
    [UsedImplicitly]
    [SerializeField]
    protected string m_jsonRepresentation;
    [UsedImplicitly]
    [SerializeField]
    private string m_displayName;

    public int id
    {
      get => this.m_id;
      protected internal set => this.m_id = value;
    }

    [PublicAPI]
    public string displayName
    {
      get => this.m_displayName;
      protected set => this.m_displayName = value;
    }

    [PublicAPI]
    public string idAndName => string.Format("{0} - {1}", (object) this.id, (object) this.displayName);

    [PublicAPI]
    public void LoadFromJson()
    {
      try
      {
        this.LoadFromJson(this.m_jsonRepresentation);
      }
      catch (Exception ex)
      {
        throw new SerializationException(string.Format("Error while loading {0} {1}-{2}.", (object) this.GetType().Name, (object) this.m_id, (object) this.m_displayName), ex);
      }
    }

    private void LoadFromJson(string json)
    {
      if (string.IsNullOrEmpty(json))
        return;
      this.PopulateFromJson(JObject.Parse(json));
    }

    public virtual void PopulateFromJson(JObject jsonObject) => this.m_id = DataEditor.Serialization.JsonTokenValue<int>(jsonObject, "id");

    public override string ToString() => string.Format("[{0}: id={1}, displayName={2}]", (object) this.GetType().Name, (object) this.id, (object) this.displayName);
  }
}
