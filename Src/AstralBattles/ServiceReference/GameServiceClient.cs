// Decompiled with JetBrains decompiler
// Type: AstralBattles.ServiceReference.GameServiceClient
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;


namespace AstralBattles.ServiceReference
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class GameServiceClient : ClientBase<IGameService>, IGameService
  {
    private ClientBase<IGameService>.BeginOperationDelegate onBeginGetAvailableGamesDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndGetAvailableGamesDelegate;
    private SendOrPostCallback onGetAvailableGamesCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginCreateItemDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndCreateItemDelegate;
    private SendOrPostCallback onCreateItemCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginRegisterNewPlayerDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndRegisterNewPlayerDelegate;
    private SendOrPostCallback onRegisterNewPlayerCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginGetPlayersDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndGetPlayersDelegate;
    private SendOrPostCallback onGetPlayersCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginMarkItemAsInactiveDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndMarkItemAsInactiveDelegate;
    private SendOrPostCallback onMarkItemAsInactiveCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginGetTop25PlayersDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndGetTop25PlayersDelegate;
    private SendOrPostCallback onGetTop25PlayersCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginHandleRoundEndDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndHandleRoundEndDelegate;
    private SendOrPostCallback onHandleRoundEndCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginHandleLeaveDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndHandleLeaveDelegate;
    private SendOrPostCallback onHandleLeaveCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginOpenDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndOpenDelegate;
    private SendOrPostCallback onOpenCompletedDelegate;
    private ClientBase<IGameService>.BeginOperationDelegate onBeginCloseDelegate;
    private ClientBase<IGameService>.EndOperationDelegate onEndCloseDelegate;
    private SendOrPostCallback onCloseCompletedDelegate;

    public GameServiceClient()
    {
    }

    public GameServiceClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public GameServiceClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public GameServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public GameServiceClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public CookieContainer CookieContainer
    {
      get => this.InnerChannel.GetProperty<IHttpCookieContainerManager>()?.CookieContainer;
      set
      {
        IHttpCookieContainerManager property = this.InnerChannel.GetProperty<IHttpCookieContainerManager>();
        if (property == null)
          throw new InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpCookieContainerBindingElement.");
        property.CookieContainer = value;
      }
    }

    public event EventHandler<GetAvailableGamesCompletedEventArgs> GetAvailableGamesCompleted;

    public event EventHandler<AsyncCompletedEventArgs> CreateItemCompleted;

    public event EventHandler<RegisterNewPlayerCompletedEventArgs> RegisterNewPlayerCompleted;

    public event EventHandler<GetPlayersCompletedEventArgs> GetPlayersCompleted;

    public event EventHandler<AsyncCompletedEventArgs> MarkItemAsInactiveCompleted;

    public event EventHandler<GetTop25PlayersCompletedEventArgs> GetTop25PlayersCompleted;

    public event EventHandler<AsyncCompletedEventArgs> HandleRoundEndCompleted;

    public event EventHandler<AsyncCompletedEventArgs> HandleLeaveCompleted;

    public event EventHandler<AsyncCompletedEventArgs> OpenCompleted;

    public event EventHandler<AsyncCompletedEventArgs> CloseCompleted;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginGetAvailableGames(AsyncCallback callback, object asyncState)
    {
      return this.Channel.BeginGetAvailableGames(callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    ObservableCollection<GameInfo> IGameService.EndGetAvailableGames(IAsyncResult result)
    {
      return this.Channel.EndGetAvailableGames(result);
    }

    private IAsyncResult OnBeginGetAvailableGames(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginGetAvailableGames(callback, asyncState);
    }

    private object[] OnEndGetAvailableGames(IAsyncResult result)
    {
      return new object[1]
      {
        (object) ((IGameService) this).EndGetAvailableGames(result)
      };
    }

    private void OnGetAvailableGamesCompleted(object state)
    {
      if (this.GetAvailableGamesCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.GetAvailableGamesCompleted((object) this, new GetAvailableGamesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void GetAvailableGamesAsync() => this.GetAvailableGamesAsync((object) null);

    public void GetAvailableGamesAsync(object userState)
    {
      if (this.onBeginGetAvailableGamesDelegate == null)
        this.onBeginGetAvailableGamesDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginGetAvailableGames);
      if (this.onEndGetAvailableGamesDelegate == null)
        this.onEndGetAvailableGamesDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndGetAvailableGames);
      if (this.onGetAvailableGamesCompletedDelegate == null)
        this.onGetAvailableGamesCompletedDelegate = new SendOrPostCallback(this.OnGetAvailableGamesCompleted);
      this.InvokeAsync(this.onBeginGetAvailableGamesDelegate, (object[]) null, this.onEndGetAvailableGamesDelegate, this.onGetAvailableGamesCompletedDelegate, userState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginCreateItem(
      GameInfo item,
      AsyncCallback callback,
      object asyncState)
    {
      return this.Channel.BeginCreateItem(item, callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    void IGameService.EndCreateItem(IAsyncResult result) => this.Channel.EndCreateItem(result);

    private IAsyncResult OnBeginCreateItem(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginCreateItem((GameInfo) inValues[0], callback, asyncState);
    }

    private object[] OnEndCreateItem(IAsyncResult result)
    {
      ((IGameService) this).EndCreateItem(result);
      return (object[]) null;
    }

    private void OnCreateItemCompleted(object state)
    {
      if (this.CreateItemCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.CreateItemCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void CreateItemAsync(GameInfo item) => this.CreateItemAsync(item, (object) null);

    public void CreateItemAsync(GameInfo item, object userState)
    {
      if (this.onBeginCreateItemDelegate == null)
        this.onBeginCreateItemDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginCreateItem);
      if (this.onEndCreateItemDelegate == null)
        this.onEndCreateItemDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndCreateItem);
      if (this.onCreateItemCompletedDelegate == null)
        this.onCreateItemCompletedDelegate = new SendOrPostCallback(this.OnCreateItemCompleted);
      this.InvokeAsync(this.onBeginCreateItemDelegate, new object[1]
      {
        (object) item
      }, this.onEndCreateItemDelegate, this.onCreateItemCompletedDelegate, userState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginRegisterNewPlayer(
      PlayerInfo player,
      AsyncCallback callback,
      object asyncState)
    {
      return this.Channel.BeginRegisterNewPlayer(player, callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    PlayerInfo IGameService.EndRegisterNewPlayer(IAsyncResult result)
    {
      return this.Channel.EndRegisterNewPlayer(result);
    }

    private IAsyncResult OnBeginRegisterNewPlayer(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginRegisterNewPlayer((PlayerInfo) inValues[0], callback, asyncState);
    }

    private object[] OnEndRegisterNewPlayer(IAsyncResult result)
    {
      return new object[1]
      {
        (object) ((IGameService) this).EndRegisterNewPlayer(result)
      };
    }

    private void OnRegisterNewPlayerCompleted(object state)
    {
      if (this.RegisterNewPlayerCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.RegisterNewPlayerCompleted((object) this, new RegisterNewPlayerCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void RegisterNewPlayerAsync(PlayerInfo player)
    {
      this.RegisterNewPlayerAsync(player, (object) null);
    }

    public void RegisterNewPlayerAsync(PlayerInfo player, object userState)
    {
      if (this.onBeginRegisterNewPlayerDelegate == null)
        this.onBeginRegisterNewPlayerDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginRegisterNewPlayer);
      if (this.onEndRegisterNewPlayerDelegate == null)
        this.onEndRegisterNewPlayerDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndRegisterNewPlayer);
      if (this.onRegisterNewPlayerCompletedDelegate == null)
        this.onRegisterNewPlayerCompletedDelegate = new SendOrPostCallback(this.OnRegisterNewPlayerCompleted);
      this.InvokeAsync(this.onBeginRegisterNewPlayerDelegate, new object[1]
      {
        (object) player
      }, this.onEndRegisterNewPlayerDelegate, this.onRegisterNewPlayerCompletedDelegate, userState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginGetPlayers(AsyncCallback callback, object asyncState)
    {
      return this.Channel.BeginGetPlayers(callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    ObservableCollection<PlayerInfo> IGameService.EndGetPlayers(IAsyncResult result)
    {
      return this.Channel.EndGetPlayers(result);
    }

    private IAsyncResult OnBeginGetPlayers(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginGetPlayers(callback, asyncState);
    }

    private object[] OnEndGetPlayers(IAsyncResult result)
    {
      return new object[1]
      {
        (object) ((IGameService) this).EndGetPlayers(result)
      };
    }

    private void OnGetPlayersCompleted(object state)
    {
      if (this.GetPlayersCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.GetPlayersCompleted((object) this, new GetPlayersCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void GetPlayersAsync() => this.GetPlayersAsync((object) null);

    public void GetPlayersAsync(object userState)
    {
      if (this.onBeginGetPlayersDelegate == null)
        this.onBeginGetPlayersDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginGetPlayers);
      if (this.onEndGetPlayersDelegate == null)
        this.onEndGetPlayersDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndGetPlayers);
      if (this.onGetPlayersCompletedDelegate == null)
        this.onGetPlayersCompletedDelegate = new SendOrPostCallback(this.OnGetPlayersCompleted);
      this.InvokeAsync(this.onBeginGetPlayersDelegate, (object[]) null, this.onEndGetPlayersDelegate, this.onGetPlayersCompletedDelegate, userState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginMarkItemAsInactive(
      int id,
      AsyncCallback callback,
      object asyncState)
    {
      return this.Channel.BeginMarkItemAsInactive(id, callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    void IGameService.EndMarkItemAsInactive(IAsyncResult result)
    {
      this.Channel.EndMarkItemAsInactive(result);
    }

    private IAsyncResult OnBeginMarkItemAsInactive(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginMarkItemAsInactive((int) inValues[0], callback, asyncState);
    }

    private object[] OnEndMarkItemAsInactive(IAsyncResult result)
    {
      ((IGameService) this).EndMarkItemAsInactive(result);
      return (object[]) null;
    }

    private void OnMarkItemAsInactiveCompleted(object state)
    {
      if (this.MarkItemAsInactiveCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.MarkItemAsInactiveCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void MarkItemAsInactiveAsync(int id) => this.MarkItemAsInactiveAsync(id, (object) null);

    public void MarkItemAsInactiveAsync(int id, object userState)
    {
      if (this.onBeginMarkItemAsInactiveDelegate == null)
        this.onBeginMarkItemAsInactiveDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginMarkItemAsInactive);
      if (this.onEndMarkItemAsInactiveDelegate == null)
        this.onEndMarkItemAsInactiveDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndMarkItemAsInactive);
      if (this.onMarkItemAsInactiveCompletedDelegate == null)
        this.onMarkItemAsInactiveCompletedDelegate = new SendOrPostCallback(this.OnMarkItemAsInactiveCompleted);
      this.InvokeAsync(this.onBeginMarkItemAsInactiveDelegate, new object[1]
      {
        (object) id
      }, this.onEndMarkItemAsInactiveDelegate, this.onMarkItemAsInactiveCompletedDelegate, userState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginGetTop25Players(AsyncCallback callback, object asyncState)
    {
      return this.Channel.BeginGetTop25Players(callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    ObservableCollection<PlayerInfo> IGameService.EndGetTop25Players(IAsyncResult result)
    {
      return this.Channel.EndGetTop25Players(result);
    }

    private IAsyncResult OnBeginGetTop25Players(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginGetTop25Players(callback, asyncState);
    }

    private object[] OnEndGetTop25Players(IAsyncResult result)
    {
      return new object[1]
      {
        (object) ((IGameService) this).EndGetTop25Players(result)
      };
    }

    private void OnGetTop25PlayersCompleted(object state)
    {
      if (this.GetTop25PlayersCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.GetTop25PlayersCompleted((object) this, new GetTop25PlayersCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void GetTop25PlayersAsync() => this.GetTop25PlayersAsync((object) null);

    public void GetTop25PlayersAsync(object userState)
    {
      if (this.onBeginGetTop25PlayersDelegate == null)
        this.onBeginGetTop25PlayersDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginGetTop25Players);
      if (this.onEndGetTop25PlayersDelegate == null)
        this.onEndGetTop25PlayersDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndGetTop25Players);
      if (this.onGetTop25PlayersCompletedDelegate == null)
        this.onGetTop25PlayersCompletedDelegate = new SendOrPostCallback(this.OnGetTop25PlayersCompleted);
      this.InvokeAsync(this.onBeginGetTop25PlayersDelegate, (object[]) null, this.onEndGetTop25PlayersDelegate, this.onGetTop25PlayersCompletedDelegate, userState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginHandleRoundEnd(
      int winnerId,
      int loserId,
      AsyncCallback callback,
      object asyncState)
    {
      return this.Channel.BeginHandleRoundEnd(winnerId, loserId, callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    void IGameService.EndHandleRoundEnd(IAsyncResult result)
    {
      this.Channel.EndHandleRoundEnd(result);
    }

    private IAsyncResult OnBeginHandleRoundEnd(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginHandleRoundEnd((int) inValues[0], (int) inValues[1], callback, asyncState);
    }

    private object[] OnEndHandleRoundEnd(IAsyncResult result)
    {
      ((IGameService) this).EndHandleRoundEnd(result);
      return (object[]) null;
    }

    private void OnHandleRoundEndCompleted(object state)
    {
      if (this.HandleRoundEndCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.HandleRoundEndCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void HandleRoundEndAsync(int winnerId, int loserId)
    {
      this.HandleRoundEndAsync(winnerId, loserId, (object) null);
    }

    public void HandleRoundEndAsync(int winnerId, int loserId, object userState)
    {
      if (this.onBeginHandleRoundEndDelegate == null)
        this.onBeginHandleRoundEndDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginHandleRoundEnd);
      if (this.onEndHandleRoundEndDelegate == null)
        this.onEndHandleRoundEndDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndHandleRoundEnd);
      if (this.onHandleRoundEndCompletedDelegate == null)
        this.onHandleRoundEndCompletedDelegate = new SendOrPostCallback(this.OnHandleRoundEndCompleted);
      this.InvokeAsync(this.onBeginHandleRoundEndDelegate, new object[2]
      {
        (object) winnerId,
        (object) loserId
      }, this.onEndHandleRoundEndDelegate, this.onHandleRoundEndCompletedDelegate, userState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IAsyncResult IGameService.BeginHandleLeave(
      int leaverId,
      int opponentId,
      AsyncCallback callback,
      object asyncState)
    {
      return this.Channel.BeginHandleLeave(leaverId, opponentId, callback, asyncState);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    void IGameService.EndHandleLeave(IAsyncResult result) => this.Channel.EndHandleLeave(result);

    private IAsyncResult OnBeginHandleLeave(
      object[] inValues,
      AsyncCallback callback,
      object asyncState)
    {
      return ((IGameService) this).BeginHandleLeave((int) inValues[0], (int) inValues[1], callback, asyncState);
    }

    private object[] OnEndHandleLeave(IAsyncResult result)
    {
      ((IGameService) this).EndHandleLeave(result);
      return (object[]) null;
    }

    private void OnHandleLeaveCompleted(object state)
    {
      if (this.HandleLeaveCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.HandleLeaveCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void HandleLeaveAsync(int leaverId, int opponentId)
    {
      this.HandleLeaveAsync(leaverId, opponentId, (object) null);
    }

    public void HandleLeaveAsync(int leaverId, int opponentId, object userState)
    {
      if (this.onBeginHandleLeaveDelegate == null)
        this.onBeginHandleLeaveDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginHandleLeave);
      if (this.onEndHandleLeaveDelegate == null)
        this.onEndHandleLeaveDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndHandleLeave);
      if (this.onHandleLeaveCompletedDelegate == null)
        this.onHandleLeaveCompletedDelegate = new SendOrPostCallback(this.OnHandleLeaveCompleted);
      this.InvokeAsync(this.onBeginHandleLeaveDelegate, new object[2]
      {
        (object) leaverId,
        (object) opponentId
      }, this.onEndHandleLeaveDelegate, this.onHandleLeaveCompletedDelegate, userState);
    }

    private IAsyncResult OnBeginOpen(object[] inValues, AsyncCallback callback, object asyncState)
    {
      return ((ICommunicationObject) this).BeginOpen(callback, asyncState);
    }

    private object[] OnEndOpen(IAsyncResult result)
    {
      ((ICommunicationObject) this).EndOpen(result);
      return (object[]) null;
    }

    private void OnOpenCompleted(object state)
    {
      if (this.OpenCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.OpenCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void OpenAsync() => this.OpenAsync((object) null);

    public void OpenAsync(object userState)
    {
      if (this.onBeginOpenDelegate == null)
        this.onBeginOpenDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginOpen);
      if (this.onEndOpenDelegate == null)
        this.onEndOpenDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndOpen);
      if (this.onOpenCompletedDelegate == null)
        this.onOpenCompletedDelegate = new SendOrPostCallback(this.OnOpenCompleted);
      this.InvokeAsync(this.onBeginOpenDelegate, (object[]) null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
    }

    private IAsyncResult OnBeginClose(object[] inValues, AsyncCallback callback, object asyncState)
    {
      return ((ICommunicationObject) this).BeginClose(callback, asyncState);
    }

    private object[] OnEndClose(IAsyncResult result)
    {
      ((ICommunicationObject) this).EndClose(result);
      return (object[]) null;
    }

    private void OnCloseCompleted(object state)
    {
      if (this.CloseCompleted == null)
        return;
      ClientBase<IGameService>.InvokeAsyncCompletedEventArgs completedEventArgs = (ClientBase<IGameService>.InvokeAsyncCompletedEventArgs) state;
      this.CloseCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public void CloseAsync() => this.CloseAsync((object) null);

    public void CloseAsync(object userState)
    {
      if (this.onBeginCloseDelegate == null)
        this.onBeginCloseDelegate = new ClientBase<IGameService>.BeginOperationDelegate(this.OnBeginClose);
      if (this.onEndCloseDelegate == null)
        this.onEndCloseDelegate = new ClientBase<IGameService>.EndOperationDelegate(this.OnEndClose);
      if (this.onCloseCompletedDelegate == null)
        this.onCloseCompletedDelegate = new SendOrPostCallback(this.OnCloseCompleted);
      this.InvokeAsync(this.onBeginCloseDelegate, (object[]) null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
    }

    protected override IGameService CreateChannel()
    {
      return (IGameService) new GameServiceClient.GameServiceClientChannel((ClientBase<IGameService>) this);
    }

    private class GameServiceClientChannel(ClientBase<IGameService> client) : 
      ClientBase<IGameService>.ChannelBase<IGameService>(client),
      IGameService
    {
      public IAsyncResult BeginGetAvailableGames(AsyncCallback callback, object asyncState)
      {
        return this.BeginInvoke("GetAvailableGames", new object[0], callback, asyncState);
      }

      public ObservableCollection<GameInfo> EndGetAvailableGames(IAsyncResult result)
      {
        return (ObservableCollection<GameInfo>) this.EndInvoke("GetAvailableGames", new object[0], result);
      }

      public IAsyncResult BeginCreateItem(GameInfo item, AsyncCallback callback, object asyncState)
      {
        return this.BeginInvoke("CreateItem", new object[1]
        {
          (object) item
        }, callback, asyncState);
      }

      public void EndCreateItem(IAsyncResult result)
      {
        this.EndInvoke("CreateItem", new object[0], result);
      }

      public IAsyncResult BeginRegisterNewPlayer(
        PlayerInfo player,
        AsyncCallback callback,
        object asyncState)
      {
        return this.BeginInvoke("RegisterNewPlayer", new object[1]
        {
          (object) player
        }, callback, asyncState);
      }

      public PlayerInfo EndRegisterNewPlayer(IAsyncResult result)
      {
        return (PlayerInfo) this.EndInvoke("RegisterNewPlayer", new object[0], result);
      }

      public IAsyncResult BeginGetPlayers(AsyncCallback callback, object asyncState)
      {
        return this.BeginInvoke("GetPlayers", new object[0], callback, asyncState);
      }

      public ObservableCollection<PlayerInfo> EndGetPlayers(IAsyncResult result)
      {
        return (ObservableCollection<PlayerInfo>) this.EndInvoke("GetPlayers", new object[0], result);
      }

      public IAsyncResult BeginMarkItemAsInactive(
        int id,
        AsyncCallback callback,
        object asyncState)
      {
        return this.BeginInvoke("MarkItemAsInactive", new object[1]
        {
          (object) id
        }, callback, asyncState);
      }

      public void EndMarkItemAsInactive(IAsyncResult result)
      {
        this.EndInvoke("MarkItemAsInactive", new object[0], result);
      }

      public IAsyncResult BeginGetTop25Players(AsyncCallback callback, object asyncState)
      {
        return this.BeginInvoke("GetTop25Players", new object[0], callback, asyncState);
      }

      public ObservableCollection<PlayerInfo> EndGetTop25Players(IAsyncResult result)
      {
        return (ObservableCollection<PlayerInfo>) this.EndInvoke("GetTop25Players", new object[0], result);
      }

      public IAsyncResult BeginHandleRoundEnd(
        int winnerId,
        int loserId,
        AsyncCallback callback,
        object asyncState)
      {
        return this.BeginInvoke("HandleRoundEnd", new object[2]
        {
          (object) winnerId,
          (object) loserId
        }, callback, asyncState);
      }

      public void EndHandleRoundEnd(IAsyncResult result)
      {
        this.EndInvoke("HandleRoundEnd", new object[0], result);
      }

      public IAsyncResult BeginHandleLeave(
        int leaverId,
        int opponentId,
        AsyncCallback callback,
        object asyncState)
      {
        return this.BeginInvoke("HandleLeave", new object[2]
        {
          (object) leaverId,
          (object) opponentId
        }, callback, asyncState);
      }

      public void EndHandleLeave(IAsyncResult result)
      {
        this.EndInvoke("HandleLeave", new object[0], result);
      }
    }
  }
}
