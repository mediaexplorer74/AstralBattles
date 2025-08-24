// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Zipper
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using ICSharpCode.SharpZipLib.GZip;
using System;
using System.IO;
using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.Core.Infrastructure
{
  public static class Zipper
  {
    public static MemoryStream Decompress(byte[] data)
    {
      MemoryStream baseInputStream = new MemoryStream(data);
      MemoryStream memoryStream = new MemoryStream();
      GZipInputStream gzipInputStream = new GZipInputStream((Stream) baseInputStream);
      byte[] buffer = new byte[8192];
      int count;
      while ((count = gzipInputStream.Read(buffer, 0, 8192)) > 0)
        memoryStream.Write(buffer, 0, count);
      memoryStream.Position = 0L;
      return memoryStream;
    }

    public static byte[] Compress(byte[] data)
    {
      MemoryStream baseOutputStream = new MemoryStream();
      GZipOutputStream gzipOutputStream = new GZipOutputStream((Stream) baseOutputStream);
      gzipOutputStream.SetLevel(7);
      gzipOutputStream.Write(data, 0, data.Length);
      gzipOutputStream.Finish();
      gzipOutputStream.Close();
      return baseOutputStream.ToArray();
    }

    public static byte[] SerializeToCompressedData<T>(T obj)
    {
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
        MemoryStream memoryStream = new MemoryStream();
        xmlSerializer.Serialize((Stream) memoryStream, (object) obj);
        return memoryStream.ToArray();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public static T DeserializeFromCompressedData<T>(byte[] data)
    {
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
        MemoryStream memoryStream = new MemoryStream();
        return (T) xmlSerializer.Deserialize((Stream) Zipper.Decompress(data));
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
