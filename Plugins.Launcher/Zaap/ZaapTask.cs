// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.Zaap.ZaapTask
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

using System;
using Zaap_CSharp_Client;

namespace Ankama.Launcher.Zaap
{
  internal interface ZaapTask
  {
    Action Execute(ZaapClient zaapClient);
  }
}
