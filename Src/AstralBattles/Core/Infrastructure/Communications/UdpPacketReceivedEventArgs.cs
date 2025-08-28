// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Communications.UdpPacketReceivedEventArgs
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.Net;
using System.Text;


namespace AstralBattles.Core.Infrastructure.Communications
{
  public class UdpPacketReceivedEventArgs : EventArgs
  {
    public string Message { get; set; }

    public IPEndPoint Source { get; set; }

    public UdpPacketReceivedEventArgs(byte[] data, IPEndPoint source)
    {
      this.Message = Encoding.UTF8.GetString(data, 0, data.Length);
      this.Source = source;
    }
  }
}
