// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Zipper
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;


namespace AstralBattles.Core.Infrastructure
{
  public static class Zipper
  {
    public static MemoryStream Decompress(byte[] data)
    {
      using (var input = new MemoryStream(data))
      using (var gzipStream = new GZipStream(input, CompressionMode.Decompress))
      {
        var output = new MemoryStream();
        gzipStream.CopyTo(output);
        output.Position = 0;
        return output;
      }
    }

    public static byte[] Compress(byte[] data)
    {
      using (var output = new MemoryStream())
      {
        using (var gzipStream = new GZipStream(output, CompressionLevel.Optimal))
        {
          gzipStream.Write(data, 0, data.Length);
        }
        return output.ToArray();
      }
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
