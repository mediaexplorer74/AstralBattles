using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AstralBattles.Core.Infrastructure
{
    public static class UwpConverter
    {
        public static async Task ConvertProjectToUwp(string projectPath)
        {
            // Create backup folder
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupPath = Path.Combine(projectPath, $"Backup_{timestamp}");
            Directory.CreateDirectory(backupPath);

            // Get all XAML files
            var xamlFiles = Directory.GetFiles(projectPath, "*.xaml", SearchOption.AllDirectories);
            
            foreach (var filePath in xamlFiles)
            {
                try
                {
                    // Skip files in obj and bin folders
                    if (filePath.Contains("\\obj\\") || filePath.Contains("\\bin\\"))
                        continue;
                        
                    string relativePath = filePath.Substring(projectPath.Length).TrimStart(Path.DirectorySeparatorChar);
                    string backupFilePath = Path.Combine(backupPath, relativePath);
                    
                    // Ensure backup directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(backupFilePath));
                    
                    // Read the file content
                    string content = File.ReadAllText(filePath);
                    
                    // Convert XAML
                    string newContent = PageConverter.ConvertXamlToUwp(content);
                    
                    // Save backup
                    File.Copy(filePath, backupFilePath, true);
                    
                    // Save converted file
                    File.WriteAllText(filePath, newContent);
                    
                    // Process code-behind file if exists
                    string codeBehindPath = filePath + ".cs";
                    if (File.Exists(codeBehindPath))
                    {
                        string cbContent = File.ReadAllText(codeBehindPath);
                        string newCbContent = PageConverter.ConvertCodeBehindToUwp(cbContent);
                        
                        // Save backup
                        string backupCbPath = backupFilePath + ".cs";
                        File.Copy(codeBehindPath, backupCbPath, true);
                        
                        // Save converted file
                        File.WriteAllText(codeBehindPath, newCbContent);
                    }
                }
                catch (Exception ex)
                {
                    // Log error but continue with other files
                    Console.WriteLine($"Error processing {filePath}: {ex.Message}");
                }
            }
            
            // Update App.xaml.cs
            UpdateAppXamlCs(projectPath, backupPath);
        }
        
        private static void UpdateAppXamlCs(string projectPath, string backupPath)
        {
            string appXamlCsPath = Path.Combine(projectPath, "App.xaml.cs");
            if (File.Exists(appXamlCsPath))
            {
                // Backup original
                string backupFilePath = Path.Combine(backupPath, "App.xaml.cs");
                File.Copy(appXamlCsPath, backupFilePath, true);
                
                // Read and update content
                string content = File.ReadAllText(appXamlCsPath);
                string newContent = content
                    .Replace("using Microsoft.Phone.Shell;", "using Windows.UI.Xaml;")
                    .Replace("PhoneApplicationService", "Application")
                    .Replace("RootVisual = RootFrame;", "Window.Current.Content = RootFrame;")
                    .Replace("InitializePhoneApplication();", "// InitializePhoneApplication();");
                
                // Save updated file
                File.WriteAllText(appXamlCsPath, newContent);
            }
        }
    }
}
