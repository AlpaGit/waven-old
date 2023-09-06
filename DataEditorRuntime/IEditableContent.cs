// Decompiled with JetBrains decompiler
// Type: DataEditor.IEditableContent
// Assembly: DataEditorRuntime, Version=1.0.6990.32389, Culture=neutral, PublicKeyToken=null
// MVID: 45C45C6B-0733-4518-B038-C58DEC652313
// Assembly location: E:\WAVEN\Waven_Data\Managed\DataEditorRuntime.dll

using Newtonsoft.Json.Linq;

namespace DataEditor
{
  public interface IEditableContent
  {
    void PopulateFromJson(JObject jsonObject);
  }
}
