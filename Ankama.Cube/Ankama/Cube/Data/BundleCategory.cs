// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.BundleCategory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public enum BundleCategory
  {
    [BundleName("others")] None = 0,
    [BundleName("others")] Others = 1,
    [BundleName("unknowns")] Unknown = 2,
    [BundleName("cras")] Cra = 10, // 0x0000000A
    [BundleName("ecaflips")] Ecaflip = 11, // 0x0000000B
    [BundleName("eniripsas")] Eniripsa = 12, // 0x0000000C
    [BundleName("enutrofs")] Enutrof = 13, // 0x0000000D
    [BundleName("fecas")] Feca = 14, // 0x0000000E
    [BundleName("iops")] Iop = 15, // 0x0000000F
    [BundleName("osamodas")] Osamodas = 16, // 0x00000010
    [BundleName("pandawas")] Pandawa = 17, // 0x00000011
    [BundleName("sacrieurs")] Sacrieur = 18, // 0x00000012
    [BundleName("sadidas")] Sadida = 19, // 0x00000013
    [BundleName("srams")] Sram = 20, // 0x00000014
    [BundleName("xelors")] Xelor = 21, // 0x00000015
    [BundleName("eliotropes")] Eliotrope = 512, // 0x00000200
    [BundleName("huppermages")] Huppermage = 821, // 0x00000335
    [BundleName("ouginaks")] Ouginak = 1521, // 0x000005F1
    [BundleName("srams")] Roublard = 1815, // 0x00000717
    [BundleName("osamodas")] Steamer = 1920, // 0x00000780
    [BundleName("sadidas")] Zobal = 2615, // 0x00000A37
    [BundleName("bouftous")] Bouftous = 20000, // 0x00004E20
    [BundleName("chachas")] Chachas = 30800, // 0x00007850
    [BundleName("chafers")] Chafers = 30801, // 0x00007851
    [BundleName("corbacs")] Corbacs = 31500, // 0x00007B0C
    [BundleName("craqueleurs")] Craqueleurs = 31800, // 0x00007C38
    [BundleName("flaqueux")] Flaqueux = 61200, // 0x0000EF10
    [BundleName("prespics")] Prespics = 161800, // 0x00027808
    [BundleName("tofus")] Tofus = 201500, // 0x0003131C
  }
}
