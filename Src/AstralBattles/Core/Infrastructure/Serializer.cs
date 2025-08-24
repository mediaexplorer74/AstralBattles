// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.Serializer
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.Core.Infrastructure
{
  public class Serializer
  {
    private static readonly ILogger Logger = LogFactory.GetLogger<Serializer>();

    public static T Read<T>(string fileName, bool tryTwice = true)
    {
      try
      {
        T obj = default (T);
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(fileName, FileMode.Open, FileAccess.Read))
          {
            using (StreamReader streamReader = new StreamReader((Stream) storageFileStream))
              obj = (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) new StringReader(streamReader.ReadToEnd()));
          }
        }
        return obj;
      }
      catch (Exception ex1)
      {
        try
        {
          if (tryTwice)
            return Serializer.Read<T>(fileName, false);
        }
        catch (Exception ex2)
        {
          Serializer.Logger.LogError(ex2);
        }
        DebugHelper.Break();
        return default (T);
      }
    }

    public static void Write<T>(T obj, string fileName)
    {
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (storeForApplication.FileExists(fileName))
            storeForApplication.DeleteFile(fileName);
          using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(fileName, FileMode.Create))
          {
            new XmlSerializer(typeof (T)).Serialize((Stream) storageFileStream, (object) obj);
            storageFileStream.Flush(true);
            storageFileStream.Close();
          }
        }
      }
      catch (Exception ex)
      {
        Serializer.Logger.LogError(ex);
        DebugHelper.Break();
        throw;
      }
    }

    public static void Delete(string file)
    {
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          storeForApplication.DeleteFile(file);
      }
      catch (Exception ex)
      {
        Serializer.Logger.LogError(ex);
        DebugHelper.Break();
      }
    }

    public static void ClearStorage()
    {
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        foreach (string fileName in storeForApplication.GetFileNames())
          storeForApplication.DeleteFile(fileName);
      }
    }

    public static bool Exists(string file)
    {
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          return storeForApplication.FileExists(file);
      }
      catch (Exception ex)
      {
        Serializer.Logger.LogError(ex);
        return false;
      }
    }
  }
}
