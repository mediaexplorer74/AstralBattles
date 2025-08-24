// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Communications.GetMyIpAddressService
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.Net;

#nullable disable
namespace AstralBattles.Core.Infrastructure.Communications
{
  public static class GetMyIpAddressService
  {
    public const string ServiceUrl = "http://api.hostip.info/get_html.php";

    public static void GetIpAddressAsync(Action<string, Exception> callback)
    {
      WebClient webClient = new WebClient();
      webClient.DownloadStringCompleted += (DownloadStringCompletedEventHandler) ((s, e) => callback(GetMyIpAddressService.ParseIp(e.Result), e.Error));
      webClient.DownloadStringAsync(new Uri("http://api.hostip.info/get_html.php"));
    }

    private static string ParseIp(string result)
    {
      if (string.IsNullOrWhiteSpace(result))
        return string.Empty;
      try
      {
        return result.Remove(0, result.IndexOf("\nIP: ") + "\nIP: ".Length).Trim('\n', '\t', '\r');
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }
  }
}
