// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Tuple`2
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

#nullable disable
namespace AstralBattles.Core.Infrastructure
{
  public class Tuple<T1, T2>
  {
    public Tuple(T1 t1, T2 t2)
    {
      this.Item1 = t1;
      this.Item2 = t2;
    }

    public Tuple()
    {
    }

    public T1 Item1 { get; set; }

    public T2 Item2 { get; set; }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is Tuple<T1, T2>))
        return false;
      Tuple<T1, T2> tuple = (Tuple<T1, T2>) obj;
      return this.Item1.Equals((object) tuple.Item1) && this.Item2.Equals((object) tuple.Item2);
    }

    public override int GetHashCode() => this.Item1.GetHashCode() + this.Item2.GetHashCode();

    public override string ToString()
    {
      return string.Format("{0} - {1}", (object) this.Item1, (object) this.Item2);
    }
  }
}
