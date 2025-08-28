// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.IBattlefield
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace AstralBattles.Core.Model
{
  public interface IBattlefield
  {
    Player FirstPlayer { get; set; }

    Player SecondPlayer { get; set; }

    Player ActivePlayer { get; set; }

    Player InactivePlayer { get; set; }

    bool WaitingNextPlayersTurn { get; set; }

    bool IsWaitingForUserInput { get; set; }

    int Turn { get; set; }

    GameLogItem LastLogItem { get; set; }

    bool IsFirstPlayerTurn { get; set; }

    Field SelectedField { get; set; }

    Card LastUsedCard { get; set; }

    int RoundIndex { get; set; }

    Element FirstPlayerSelectedElement { get; set; }

    Card SelectedCardOrCardType { get; set; }

    ObservableCollection<GameLogItem> GameLog { get; set; }

    Player SecondPlayerInitial { get; set; }

    Player FirstPlayerInitial { get; set; }

    Player GetPlayerByName(string name);

    void UseCard(Card card);

    void EndTurn();

    void StartNewTurn();

    void GameOver(Player winner);

    void SerializeCurrentState();

    IEnumerable<Field> GetAllNonEmptyFields();

    void OnSpell(SpellCard spell);

    void OnSummon(CreatureCard creature, bool bySecondPlayer);

    void OnSkip();

    void Refresh();

    void UpdateIsPlayerInitialTurn();

    void OnShowComputersChoose(Card card);
  }
}
