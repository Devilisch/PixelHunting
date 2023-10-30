using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public static class FileManager
{
    private static Dictionary<string, string> _fileCache = new Dictionary<string, string>();
    
    private const string JSON_PATH = "/Resources/JSON/";
    private const string TEXT_PATH = "/Resources/Text/";
    private const string STATISTICS_JSON_PATH = "statistics.json";
    private const string INFO_TXT_PATH = "info.txt";


    
    private static string LoadTextFile(string folder, string fileName)
    {
        var path = Application.dataPath + folder + fileName;
        
        if (_fileCache.ContainsKey(path))
        {
            return _fileCache[path];
        }
        else
        {
            if (!Directory.Exists(Application.dataPath + folder))
            {
                Debug.Log("Incorrect folder path: " + Application.dataPath + folder);
                return null;
            }
            
            var textFileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read);
            string result = null;
            
            using(var streamReader = new StreamReader(textFileStream))
                result = streamReader.ReadToEnd();
            
            _fileCache.Add(path, result);
            
            return result;
        }
    }

    private static void SaveTextFile(string folder, string fileName, string text)
    {
        var path = Application.dataPath + folder + fileName;

        if (_fileCache.ContainsKey(path))
            _fileCache[path] = text;
        else
            _fileCache.Add(path, text);
        
        var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
        var textBytes = new UTF8Encoding(true).GetBytes(text);

        file.Write(textBytes, 0, textBytes.Length);
        file.Close();
    }
    
    public static string LoadStatistics() => LoadTextFile(JSON_PATH, STATISTICS_JSON_PATH);
    public static void SaveStatistics(string text) => SaveTextFile(JSON_PATH, STATISTICS_JSON_PATH, text);
    public static string LoadTargets() => LoadTextFile(TEXT_PATH, INFO_TXT_PATH);
    public static void SaveTargets(string text) => SaveTextFile(TEXT_PATH, INFO_TXT_PATH, text);
}
