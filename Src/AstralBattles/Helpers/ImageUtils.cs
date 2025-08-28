
using System;
using Windows.UI.Xaml.Media.Imaging;


namespace AstralBattles.Helpers
{
  public class ImageUtils
  {
    private static WriteableBitmap CropImage(
      WriteableBitmap source,
      int xOffset,
      int yOffset,
      int width,
      int height)
    {
      // TODO: Implement WriteableBitmap cropping for UWP using PixelBuffer
      // For MVP build, return original bitmap
      return source;
    }
  }
}
