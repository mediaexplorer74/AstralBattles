// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Services.TournamentService
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AstralBattles.Core.Services
{
  public class TournamentService
  {
    private readonly IPlayersRegistry playerRegistry;
    private static readonly TournamentService instance = new TournamentService();
    private static readonly Random Random = new Random(Environment.TickCount);

    public static TournamentService Instance => TournamentService.instance;

    private TournamentService()
    {
      this.playerRegistry = (IPlayersRegistry) new PlayersRegistry();
      this.Tournament = new TournamentInfo();
    }

    public IEnumerable<AstralBattles.Core.Infrastructure.Tuple<string, string>> GetCurrentRoundResults()
    {
      return (IEnumerable<AstralBattles.Core.Infrastructure.Tuple<string, string>>) this.Tournament.CurrentRound;
    }

    public TournamentInfo Tournament { get; set; }

    public void StartTournament(Player player)
    {
      this.Tournament = new TournamentInfo();
      this.Tournament.CurrentPlayer = player;
      List<string> list = ((IEnumerable<string>) new string[1]
      {
        player.Name
      }).Concat<string>((IEnumerable<string>) this.playerRegistry.GetNames()).ToList<string>();
      for (int index = 0; index < list.Count; ++index)
        this.Tournament.Stat.Add(new PlayerPoint(list[index], 0, 0, list[index] == player.Name));
      this.Tournament.Rounds = TournamentService.ListMatches(list);
      this.SaveTournamentInfo();
    }

    private void AddPoints(string name, int points)
    {
      PlayerPoint playerPoint = this.Tournament.Stat.First<PlayerPoint>((Func<PlayerPoint, bool>) (i => i.Name == name));
      playerPoint.Points += points;
      ++playerPoint.Wins;
    }

    public string GetCurrentOpponent()
    {
      this.ReloadTournamentInfo();
      AstralBattles.Core.Infrastructure.Tuple<string, string> tuple = this.Tournament.CurrentRound.First<AstralBattles.Core.Infrastructure.Tuple<string, string>>((Func<AstralBattles.Core.Infrastructure.Tuple<string, string>, bool>) (i => i.Item1 == this.Tournament.CurrentPlayer.Name || i.Item2 == this.Tournament.CurrentPlayer.Name));
      string name = this.Tournament.CurrentPlayer.Name;
      return tuple.Item1 == name ? tuple.Item2 : tuple.Item1;
    }

    public void EndRound(bool playerWon, int winnerPoints)
    {
      this.ReloadTournamentInfo();
      List<AstralBattles.Core.Infrastructure.Tuple<string, string>> currentRound = this.Tournament.CurrentRound;
      AstralBattles.Core.Infrastructure.Tuple<string, string> tuple1 = currentRound.First<AstralBattles.Core.Infrastructure.Tuple<string, string>>((Func<AstralBattles.Core.Infrastructure.Tuple<string, string>, bool>) (i => i.Item1 == this.Tournament.CurrentPlayer.Name || i.Item2 == this.Tournament.CurrentPlayer.Name));
      for (int index = 0; index < currentRound.Count; ++index)
      {
        AstralBattles.Core.Infrastructure.Tuple<string, string> tuple2 = currentRound[index];
        if (tuple2 == tuple1)
        {
          string name = this.Tournament.CurrentPlayer.Name;
          string currentOpponent = this.GetCurrentOpponent();
          tuple2.Item1 = playerWon ? name : currentOpponent;
          tuple2.Item2 = playerWon ? currentOpponent : name;
          this.AddPoints(playerWon ? name : currentOpponent, winnerPoints);
        }
        else
        {
          string player1 = tuple2.Item1;
          string player2 = tuple2.Item2;
          int points;
          string name = this.PlayGameBetweenComputers(player1, player2, out points);
          tuple2.Item1 = name;
          tuple2.Item2 = player1 == name ? player2 : player1;
          this.AddPoints(name, points);
        }
        currentRound[index] = tuple2;
      }
      this.Tournament.CurrentRound = currentRound;
      ++this.Tournament.CurrentRoundIndex;
      this.SaveTournamentInfo();
    }

    /*private void ReloadTournamentInfo()
    {
      try
      {
        this.Tournament = Serializer.Read<TournamentInfo>("TournamentInfo__1_452.xml");
      }
      catch (Exception ex)
      {
        this.Tournament = new TournamentInfo();
      }
    }*/

    private async void ReloadTournamentInfo()
    {
        try
        {
            this.Tournament = await Serializer.Read<TournamentInfo>("TournamentInfo__1_452.xml");
        }
        catch (Exception ex)
        {
            this.Tournament = new TournamentInfo();
        }
    }

    private void SaveTournamentInfo()
    {
      try
      {
        Serializer.Write<TournamentInfo>(this.Tournament, "TournamentInfo__1_452.xml");
      }
      catch (Exception ex)
      {
      }
    }

    private string PlayGameBetweenComputers(string player1, string player2, out int points)
    {
      int aiDifficulty1 = PlayersFactory.GetAiDifficulty(player1);
      int aiDifficulty2 = PlayersFactory.GetAiDifficulty(player2);
      points = TournamentService.Random.Next(31, 83);
      return aiDifficulty1 + TournamentService.Random.Next(0, 5) >= aiDifficulty2 + TournamentService.Random.Next(0, 5) ? player1 : player2;
    }

    public static List<List<AstralBattles.Core.Infrastructure.Tuple<string, string>>> ListMatches(
      List<string> listTeam)
    {
      List<List<AstralBattles.Core.Infrastructure.Tuple<string, string>>> tupleListList = new List<List<AstralBattles.Core.Infrastructure.Tuple<string, string>>>();
      int num = listTeam.Count - 1;
      int count1 = listTeam.Count / 2;
      List<string> stringList = new List<string>();
      stringList.AddRange(listTeam.Skip<string>(count1).Take<string>(count1));
      stringList.AddRange(((IEnumerable<string>) listTeam.Skip<string>(1).Take<string>(count1 - 1).ToArray<string>()).Reverse<string>());
      int count2 = stringList.Count;
      for (int index1 = 0; index1 < num; ++index1)
      {
        List<AstralBattles.Core.Infrastructure.Tuple<string, string>> tupleList = new List<AstralBattles.Core.Infrastructure.Tuple<string, string>>();
        int index2 = index1 % count2;
        tupleList.Add(new AstralBattles.Core.Infrastructure.Tuple<string, string>(stringList[index2], listTeam[0]));
        for (int index3 = 1; index3 < count1; ++index3)
        {
          int index4 = (index1 + index3) % count2;
          int index5 = (index1 + count2 - index3) % count2;
          tupleList.Add(new AstralBattles.Core.Infrastructure.Tuple<string, string>(stringList[index4], stringList[index5]));
        }
        tupleListList.Add(tupleList);
      }
      return tupleListList;
    }
  }
}
