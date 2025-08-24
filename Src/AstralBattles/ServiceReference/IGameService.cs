// Decompiled with JetBrains decompiler
// Type: AstralBattles.ServiceReference.IGameService
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ServiceModel;

#nullable disable
namespace AstralBattles.ServiceReference
{
  [ServiceContract(ConfigurationName = "ServiceReference.IGameService")]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public interface IGameService
  {
    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/GetAvailableGames", ReplyAction = "http://tempuri.org/IGameService/GetAvailableGamesResponse")]
    IAsyncResult BeginGetAvailableGames(AsyncCallback callback, object asyncState);

    ObservableCollection<GameInfo> EndGetAvailableGames(IAsyncResult result);

    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/CreateItem", ReplyAction = "http://tempuri.org/IGameService/CreateItemResponse")]
    IAsyncResult BeginCreateItem(GameInfo item, AsyncCallback callback, object asyncState);

    void EndCreateItem(IAsyncResult result);

    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/RegisterNewPlayer", ReplyAction = "http://tempuri.org/IGameService/RegisterNewPlayerResponse")]
    IAsyncResult BeginRegisterNewPlayer(
      PlayerInfo player,
      AsyncCallback callback,
      object asyncState);

    PlayerInfo EndRegisterNewPlayer(IAsyncResult result);

    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/GetPlayers", ReplyAction = "http://tempuri.org/IGameService/GetPlayersResponse")]
    IAsyncResult BeginGetPlayers(AsyncCallback callback, object asyncState);

    ObservableCollection<PlayerInfo> EndGetPlayers(IAsyncResult result);

    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/MarkItemAsInactive", ReplyAction = "http://tempuri.org/IGameService/MarkItemAsInactiveResponse")]
    IAsyncResult BeginMarkItemAsInactive(int id, AsyncCallback callback, object asyncState);

    void EndMarkItemAsInactive(IAsyncResult result);

    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/GetTop25Players", ReplyAction = "http://tempuri.org/IGameService/GetTop25PlayersResponse")]
    IAsyncResult BeginGetTop25Players(AsyncCallback callback, object asyncState);

    ObservableCollection<PlayerInfo> EndGetTop25Players(IAsyncResult result);

    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/HandleRoundEnd", ReplyAction = "http://tempuri.org/IGameService/HandleRoundEndResponse")]
    IAsyncResult BeginHandleRoundEnd(
      int winnerId,
      int loserId,
      AsyncCallback callback,
      object asyncState);

    void EndHandleRoundEnd(IAsyncResult result);

    [OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IGameService/HandleLeave", ReplyAction = "http://tempuri.org/IGameService/HandleLeaveResponse")]
    IAsyncResult BeginHandleLeave(
      int leaverId,
      int opponentId,
      AsyncCallback callback,
      object asyncState);

    void EndHandleLeave(IAsyncResult result);
  }
}
