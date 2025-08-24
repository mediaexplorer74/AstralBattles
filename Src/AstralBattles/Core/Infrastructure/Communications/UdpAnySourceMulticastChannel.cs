// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Communications.UdpAnySourceMulticastChannel
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Threading;

#nullable disable
namespace AstralBattles.Core.Infrastructure.Communications
{
  public class UdpAnySourceMulticastChannel : IDisposable
  {
    public static bool IsJoined;

    public event EventHandler<UdpPacketReceivedEventArgs> PacketReceived;

    public event EventHandler AfterOpen;

    public event EventHandler BeforeClose;

    public bool IsDisposed { get; private set; }

    private byte[] ReceiveBuffer { get; set; }

    private UdpAnySourceMulticastClient Client { get; set; }

    public Dispatcher Dispatcher { get; set; }

    public UdpAnySourceMulticastChannel(IPAddress address, int port)
      : this(address, port, 1024)
    {
    }

    public UdpAnySourceMulticastChannel(IPAddress address, int port, int maxMessageSize)
    {
      this.ReceiveBuffer = new byte[maxMessageSize];
      this.Client = new UdpAnySourceMulticastClient(address, port);
    }

    public void Dispose()
    {
      if (this.IsDisposed)
        return;
      this.IsDisposed = true;
      if (this.Client == null)
        return;
      this.Client.Dispose();
    }

    public void Open()
    {
      if (UdpAnySourceMulticastChannel.IsJoined)
        return;
      this.Client.BeginJoinGroup((AsyncCallback) (result => this.Dispatcher.BeginInvoke((Action) (() =>
      {
        try
        {
          this.Client.EndJoinGroup(result);
          UdpAnySourceMulticastChannel.IsJoined = true;
          this.OnAfterOpen();
          this.Receive();
        }
        catch
        {
        }
      }))), (object) null);
    }

    public void Close()
    {
      this.OnBeforeClose();
      UdpAnySourceMulticastChannel.IsJoined = false;
      this.Dispose();
    }

    public void Send(string format, params object[] args)
    {
      if (!UdpAnySourceMulticastChannel.IsJoined)
        return;
      byte[] bytes = Encoding.UTF8.GetBytes(string.Format(format, args));
      this.Client.BeginSendToGroup(bytes, 0, bytes.Length, (AsyncCallback) (result => this.Client.EndSendToGroup(result)), (object) null);
    }

    public void SendTo(IPEndPoint endPoint, string format, params object[] args)
    {
      if (!UdpAnySourceMulticastChannel.IsJoined)
        return;
      byte[] bytes = Encoding.UTF8.GetBytes(string.Format(format, args));
      this.Client.BeginSendTo(bytes, 0, bytes.Length, endPoint, (AsyncCallback) (result => this.Client.EndSendToGroup(result)), (object) null);
    }

    private void Receive()
    {
      if (!UdpAnySourceMulticastChannel.IsJoined)
        return;
      Array.Clear((Array) this.ReceiveBuffer, 0, this.ReceiveBuffer.Length);
      this.Client.BeginReceiveFromGroup(this.ReceiveBuffer, 0, this.ReceiveBuffer.Length, (AsyncCallback) (result =>
      {
        if (this.IsDisposed)
          return;
        IPEndPoint source;
        this.Dispatcher.BeginInvoke((Action) (() =>
        {
          try
          {
            this.Client.EndReceiveFromGroup(result, out source);
            this.OnReceive(source, this.ReceiveBuffer);
            this.Receive();
          }
          catch (Exception ex)
          {
            UdpAnySourceMulticastChannel.IsJoined = false;
            this.Open();
          }
        }));
      }), (object) null);
    }

    private void OnReceive(IPEndPoint source, byte[] data)
    {
      EventHandler<UdpPacketReceivedEventArgs> packetReceived = this.PacketReceived;
      if (packetReceived == null)
        return;
      packetReceived((object) this, new UdpPacketReceivedEventArgs(data, source));
    }

    private void OnAfterOpen()
    {
      EventHandler afterOpen = this.AfterOpen;
      if (afterOpen == null)
        return;
      afterOpen((object) this, EventArgs.Empty);
    }

    private void OnBeforeClose()
    {
      EventHandler beforeClose = this.BeforeClose;
      if (beforeClose == null)
        return;
      beforeClose((object) this, EventArgs.Empty);
    }
  }
}
