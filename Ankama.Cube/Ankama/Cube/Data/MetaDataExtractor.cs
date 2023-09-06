// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MetaDataExtractor
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ankama.Cube.Data
{
  public abstract class MetaDataExtractor
  {
    protected abstract void TryAdd(object elt);

    [PublicAPI]
    public void GetMemberRecursively(IEditableContent data, params string[] fieldNames) => this.GetMemberRecursively(data, (Func<FieldInfo, bool>) (f => ((IEnumerable<string>) fieldNames).Contains<string>(f.Name)));

    [PublicAPI]
    public void GetMemberRecursively(IEditableContent data, Func<FieldInfo, bool> predicate = null)
    {
      FieldInfo[] source = data.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
      if (predicate != null)
        source = ((IEnumerable<FieldInfo>) source).Where<FieldInfo>(predicate).ToArray<FieldInfo>();
      this.GetRecursivelyInFields((object) data, (IEnumerable<FieldInfo>) source);
    }

    private void GetRecursivelyInFields(object src, IEnumerable<FieldInfo> fieldInfos)
    {
      if (src == null)
        return;
      foreach (FieldInfo fieldInfo in fieldInfos)
      {
        object elt1 = fieldInfo.GetValue(src);
        if (elt1 != null)
        {
          if (elt1 is IEnumerable enumerable)
          {
            foreach (object elt2 in enumerable)
            {
              if (elt2 != null)
                this.Add(elt2);
            }
          }
          else
            this.Add(elt1);
        }
      }
    }

    private void Add(object elt)
    {
      this.TryAdd(elt);
      if (!(elt is IEditableContent))
        return;
      FieldInfo[] fields = elt.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
      this.GetRecursivelyInFields(elt, (IEnumerable<FieldInfo>) fields);
    }
  }
}
