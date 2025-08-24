// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Communications.ServerController
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.Net;

#nullable disable
namespace AstralBattles.Core.Infrastructure.Communications
{
  public class ServerController : NotifyPropertyChangedBase
  {
    private static readonly ServerController instance = new ServerController();
    private bool isEnabled;
    private UdpAnySourceMulticastChannel channel;

    public static ServerController Instance => ServerController.instance;

    public bool IsEnabled
    {
      get => this.isEnabled;
      set
      {
        if (this.isEnabled == value)
          return;
        this.isEnabled = value;
        this.RaisePropertyChanged(nameof (IsEnabled));
        if (!value)
          return;
        this.StartListening();
      }
    }

    private void StartListening()
    {
      this.channel = new UdpAnySourceMulticastChannel(IPAddress.Parse("224.0.0.11"), 23523);
      this.channel.PacketReceived += new EventHandler<UdpPacketReceivedEventArgs>(this.OnPacketReceived);
      this.channel.Open();
    }

    private void OnPacketReceived(object sender, UdpPacketReceivedEventArgs e)
    {
    }

    public event EventHandler<PlayerCreateInfoEventArgs> OnOpponentJoin = delegate { };

    public event EventHandler<PlayerCreateInfoEventArgs> OnOpponentChangedInfo = delegate { };

    public event EventHandler OnOpponentDisconnect = delegate { };
  }
}
