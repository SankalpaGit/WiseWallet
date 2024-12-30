using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WiseWallet.Models;

namespace WiseWallet.Utils
{
    internal class FileCreation
    {
        public static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "DataStore.json");

        // Method to ensure file and directory existence
        public static void EnsureFileExists()
        {
            string directory = Path.GetDirectoryName(FilePath)!;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]"); 
            }
        }

    }
}
