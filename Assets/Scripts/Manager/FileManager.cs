using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager
{
    private Dictionary<string, TextAsset> _fileCache = new Dictionary<string, TextAsset>();
    
    private const string STATISTICS_JSON_PATH = "JSON/statistics.json";
    private const string TARGETS_TXT_PATH = "Text/info.txt";


    
    private TextAsset Load(string path)
    {
        if (_fileCache.ContainsKey(path))
        {
            return _fileCache[path];
        }
        else
        {
            var textAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));
            _fileCache.Add(path, textAsset);
            
            return textAsset;
        }
    }
    
    public TextAsset LoadStatistics() => Load(STATISTICS_JSON_PATH);
    public TextAsset LoadTargets() => Load(TARGETS_TXT_PATH);
}
