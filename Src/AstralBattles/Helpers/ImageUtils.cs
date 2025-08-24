// Decompiled with JetBrains decompiler
// Type: AstralBattles.Helpers.ImageUtils
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Windows.Media.Imaging;

#nullable disable
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
      int pixelWidth = source.PixelWidth;
      WriteableBitmap writeableBitmap = new WriteableBitmap(width, height);
      for (int index = 0; index <= height - 1; ++index)
      {
        int sourceIndex = xOffset + (yOffset + index) * pixelWidth;
        int destinationIndex = index * width;
        Array.Copy((Array) source.Pixels, sourceIndex, (Array) writeableBitmap.Pixels, destinationIndex, width);
      }
      return writeableBitmap;
    }
  }
}
