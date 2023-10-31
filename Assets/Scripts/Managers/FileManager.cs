using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public static class FileManager
    {
        private static Dictionary<string, string> _fileCache = new Dictionary<string, string>();
    
        private const string STATISTICS_JSON_PATH = "/statistics.json";
        private const string INFO_TXT_RESOURCES_PATH = "Text/info";


    
        private static string LoadTextFile(string fileName)
        {
            var path = Application.persistentDataPath + fileName;
        
            if (_fileCache.ContainsKey(path))
            {
                return _fileCache[path];
            }
            else
            {
                var textFileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read);
                string result = null;
            
                using(var streamReader = new StreamReader(textFileStream))
                    result = streamReader.ReadToEnd();
            
                if (!string.IsNullOrEmpty(result))
                    _fileCache.Add(path, result);
            
                return result;
            }
        }

        private static void SaveTextFile(string fileName, string text)
        {
            var path = Application.persistentDataPath + fileName;

            if (_fileCache.ContainsKey(path))
                _fileCache[path] = text;
            else
                _fileCache.Add(path, text);

            if (!File.Exists(path))
                File.Create(path);
            
            var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
            var textBytes = new UTF8Encoding(true).GetBytes(text);

            file.Write(textBytes, 0, textBytes.Length);
            file.Close();
        }
    
        public static string LoadStatistics() => LoadTextFile(STATISTICS_JSON_PATH);
        public static void SaveStatistics(string text) => SaveTextFile(STATISTICS_JSON_PATH, text);
        public static string LoadTargets() => ((TextAsset)Resources.Load(INFO_TXT_RESOURCES_PATH, typeof(TextAsset))).text;
    }
}
