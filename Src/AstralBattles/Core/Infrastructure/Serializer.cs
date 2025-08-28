﻿
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;


namespace AstralBattles.Core.Infrastructure
{
  public class Serializer
  {
    private static readonly ILogger Logger = LogFactory.GetLogger<Serializer>();

    public static async Task<T> Read<T>(string fileName, bool tryTwice = true)
    {
        try
        {
            return await ReadAsync<T>(fileName, tryTwice);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Serializer - Read T error: " + ex.Message);
            Serializer.Logger.LogError(ex);
            //DebugHelper.Break();
            return default(T);
        }
    }

    private static async Task<T> ReadAsync<T>(string fileName, bool tryTwice = true)
    {
      try
      {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        StorageFile file = await localFolder.GetFileAsync(fileName);
        string content = await FileIO.ReadTextAsync(file);
        return (T)new XmlSerializer(typeof(T)).Deserialize(new StringReader(content));
      }
      catch (Exception ex1)
      {
        try
        {
            Debug.WriteLine("[ex1] Serializer - ReadAsync T error: " + ex1.Message);

            //if (tryTwice)
            //  return await ReadAsync<T>(fileName, false);
        }
        catch (Exception ex2)
        {
            Debug.WriteLine("[ex2] Serializer - ReadAsync error: " + ex2.Message);
            Serializer.Logger.LogError(ex2);
        }
        return default(T);
      }
    }

    public static async void Write<T>(T obj, string fileName)
    {
      try
      {
         await WriteAsync<T>(obj, fileName);//.GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] Serializer - Write error: " + ex.Message);
        Serializer.Logger.LogError(ex);
        //DebugHelper.Break();
        //throw;
      }
    }

    private static async Task WriteAsync<T>(T obj, string fileName)
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      StorageFile file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
      
      using (var stream = new StringWriter())
      {
        new XmlSerializer(typeof(T)).Serialize(stream, obj);
        await FileIO.WriteTextAsync(file, stream.ToString());
      }
    }

    public async static void Delete(string file)
    {
      try
      {
           await DeleteAsync(file);//.GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] Serializer - Delete error: " + ex.Message);
        Serializer.Logger.LogError(ex);
        //DebugHelper.Break();
      }
    }

    private static async Task DeleteAsync(string file)
    {
      try
      {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        StorageFile fileToDelete = await localFolder.GetFileAsync(file);
        await fileToDelete.DeleteAsync();
      }
      catch (Exception ex)//(FileNotFoundException)
      {
        Debug.WriteLine("[ex] Serializer - DeleteAsync error: " + ex.Message);
        // File doesn't exist, nothing to delete
      }
    }

    public static async void ClearStorage()
    {
      try
      {
          await ClearStorageAsync();//.GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] Serializer - ClearStorage error: " + ex.Message);
        Serializer.Logger.LogError(ex);
        //DebugHelper.Break();
      }
    }

    private static async Task ClearStorageAsync()
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      var files = await localFolder.GetFilesAsync();
      foreach (var file in files)
      {
        await file.DeleteAsync();
      }
    }

    public static async Task<bool> Exists(string filename)
    {
      try
      {
          return await ExistsAsync(filename);//.GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] Serializer - file  "+ filename + " error: " + ex.Message);
        Serializer.Logger.LogError(ex);
        return false;
      }
    }

    private static async Task<bool> ExistsAsync(string filename)
    {
      try
      {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        await localFolder.GetFileAsync(filename);
        return true;
      }
      catch (Exception ex)//(FileNotFoundException)
      {
        Debug.WriteLine("[ex] Serializer - "+ filename + " file not found: " + ex.Message);
        return false;
      }
    }
  }
}
