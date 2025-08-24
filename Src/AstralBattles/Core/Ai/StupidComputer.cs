// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Ai.StupidComputer
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace AstralBattles.Core.Ai
{
  public class StupidComputer : ComputerIntellect
  {
    public override Card GetCard(out Field field)
    {
      field = (Field) null;
      Card card = (Card) null;
      ObservableCollection<Field> fields = this.Battlefield.ActivePlayer.Fields;
      if (fields.Any<Field>((Func<Field, bool>) (i => i.IsEmpty)))
        card = (Card) this.Battlefield.ActivePlayer.Elements.SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).Where<Card>((Func<Card, bool>) (i => i.IsActive)).OfType<CreatureCard>().OrderByDescending<CreatureCard, int>((Func<CreatureCard, int>) (i => i.Level)).ThenByDescending<CreatureCard, int>((Func<CreatureCard, int>) (i => i.Damage)).FirstOrDefault<CreatureCard>();
      if (card == null)
        return card;
      field = fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).GetRandomElement<Field>();
      return card;
    }
  }
}
