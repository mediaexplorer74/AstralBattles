
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace AstralBattles.Core.Infrastructure.Communications
{
  public static class GetMyIpAddressService
  {
    public const string ServiceUrl = "http://api.hostip.info/get_html.php";

    public static void GetIpAddressAsync(Action<string, Exception> callback)
    {
      // TODO: Replace WebClient with HttpClient for UWP
      // For MVP build, using placeholder implementation
      try
      {
        string placeholderIp = "127.0.0.1";
        callback(placeholderIp, null);
      }
      catch (Exception ex)
      {
        callback(string.Empty, ex);
      }
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
