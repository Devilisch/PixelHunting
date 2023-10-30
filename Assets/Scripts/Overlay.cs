using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    const byte VISIBLE_MESSAGE_COUNT = 10;
    Texture2D backgroundTexture;
    GUIStyle style;
    
    readonly StringBuilder stringBuilder = new StringBuilder();
    readonly Queue<string> messages = new Queue<string>();
    
    string output;
    float elapsed;

    float deltaTime;
    int frames;
    string fps;

    string buildGUID;
    string time;
    long logNumber = 0;

    string data = "";

    public string Data 
    {
        get 
        { 
            return 
                "-------------------------------------------------------------" +
                "\n[System information]:" +
                "\nDevice model: " + SystemInfo.deviceModel +
                "\nOS: " + SystemInfo.operatingSystem +
                "\nGraphics name: " + SystemInfo.graphicsDeviceName +
                "\nGraphics version: " + SystemInfo.graphicsDeviceVersion +
                "\nGraphics size (Mb): " + SystemInfo.graphicsMemorySize +
                "\nProcessor type: " + SystemInfo.processorType +
                "\nProcessor frequency(MHz): " + SystemInfo.processorFrequency +
                "\nSystem memory size: " + SystemInfo.systemMemorySize +
                "\nScreen resolution: " + Screen.width + "x" + Screen.height +
                "\n\n[Account information]:" +
                "\nUID: " + SystemInfo.deviceUniqueIdentifier +
                "\n-------------------------------------------------------------" +
                "\n\n[Logs]:\n\n\n\n" + data; 
        }
    }
    
    [RuntimeInitializeOnLoadMethod]
    static void InitializeOnLoad()
    {
        // new GameObject(nameof(Overlay)).AddComponent<Overlay>();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        // backgroundTexture = GenerateBackgroundTexture(new Color32(0, 0, 0, 169));
        backgroundTexture = GenerateBackgroundTexture(new Color32(0, 0, 0, 128));

        style = new GUIStyle
        {
            normal =
            {
                background = backgroundTexture,
                textColor = Color.white
            },
            // wordWrap = true,
            fontSize = Mathf.CeilToInt(16 * (Screen.safeArea.width / 720))
        };

        Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
        Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
        Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.ScriptOnly);

        // Application.logMessageReceived += AddOnlyErrorsLog; // short logs, only errors
        Application.logMessageReceived += AddMessageToLog; // full logs

// #if !UNITY_EDITOR
//         buildGUID = Guid.Parse(Application.buildGUID).ToString();
// #else
//         buildGUID = Guid.NewGuid().ToString();
// #endif
    }

    static Texture2D GenerateBackgroundTexture(Color color)
    {
        var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);

        var fillPixels = new Color[texture.width * texture.height];

        for (var i = 0; i < fillPixels.Length; i++) 
            fillPixels[i] = color;

        texture.SetPixels(fillPixels);
        texture.Apply();

        return texture;
    }

    void AddOnlyErrorsLog(string message, string stackTrace, LogType logType)
    {
        logNumber++;
        data += "[" + logNumber + "] " + 
                "[" + DateTime.UtcNow.ToLongTimeString() + " " + logType.ToString() + "]\n" + 
                message + ( logType == LogType.Error || logType == LogType.Exception || logType == LogType.Assert ? "Trace:\n" + stackTrace : "" ) + "\n\n";

        if (messages.Count >= VISIBLE_MESSAGE_COUNT) 
            messages.Dequeue();

        string textColor = null;
        switch (logType)
        {
            case LogType.Error:
            case LogType.Exception:
            case LogType.Assert:
                textColor = "red";
                break;
            default:
                return;
        }

        message = "[" + DateTime.UtcNow.ToLongTimeString() + "]: " + message;
        var entry = $"<color={textColor}>{message}</color>\n";

        // messages.Enqueue(entry);

        // UpdateLog();
    }

    void AddMessageToLog(string message, string stackTrace, LogType logType)
    {
        if (messages.Count >= VISIBLE_MESSAGE_COUNT) 
            messages.Dequeue();

        string textColor = null;
        switch (logType)
        {
            case LogType.Error:
            case LogType.Exception:
            case LogType.Assert:
                textColor = "red";
                break;
            case LogType.Warning:
                textColor = "yellow";
                break;
            case LogType.Log:
                textColor = "white";
                break;
            default:
                return;
        }

        message = "[" + DateTime.UtcNow.ToLongTimeString() + "]: " + message;
        var entry = $"<color={textColor}>{message}</color>\n";

        switch (logType)
        {
            case LogType.Error:
            case LogType.Exception:
            case LogType.Assert:
                string res = "";

                for ( int errorLine = 0; errorLine < 20; errorLine++ )
                {
                    var index = stackTrace.IndexOf( "\n" );
                    res += ( errorLine + 1 ) + " : " + stackTrace.Substring( 0, index + 1 );
                    stackTrace.Remove( 0, index + 1 );
                }

                entry += $"<color={textColor}>{res}</color>\n";
                break;
        }

        logNumber++;
        data += "[" + logNumber + "] " + 
                "[" + DateTime.UtcNow.ToLongTimeString() + " " + logType.ToString() + "]\n" + 
                message + ( logType == LogType.Error || logType == LogType.Exception || logType == LogType.Assert ? "Trace:\n" + stackTrace : "" ) + "\n\n";

        messages.Enqueue(entry);

        UpdateLog();
    }

    void UpdateLog()
    {
        stringBuilder.Clear();
        
        foreach (var m in messages)
            stringBuilder.Append(m);

        output = stringBuilder.ToString();
    }

    void OnGUI()
    {
        Log();
        
        GUILayout.Label(buildGUID, style);

        GUILayout.BeginArea(Screen.safeArea);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("FPS      ", style );
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(fps + "      ", style );
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(time + "      ", style);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        ;
    }

    void Log()
    {
        elapsed += Time.unscaledDeltaTime;

        if (elapsed > 3 && messages.Count > 0)
        {
            elapsed = 0;
            messages.Dequeue();

            UpdateLog();
        }

        GUILayout.Label(output, style); 
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        frames++;

        if (deltaTime < .25f) return;

        fps = (frames / deltaTime).ToString("F1", CultureInfo.InvariantCulture);

        frames = 0;
        deltaTime = 0;

        time = DateTime.UtcNow.ToLongTimeString();
    }
}
