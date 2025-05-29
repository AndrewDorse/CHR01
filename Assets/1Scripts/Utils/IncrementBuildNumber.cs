#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using System;
using System.IO;
using UnityEngine.Networking;

/*     This class will increment the iOS build number on each build - this is useful for uploading builds
    to TestFlight (iOS) as it requires a more recent build number on each iteration */
public class IncrementBuildNumber : IPreprocessBuild
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        string build = PlayerSettings.Android.bundleVersionCode.ToString();

        // Retrieve auto-incrementing build number from server service
        UnityWebRequest www = UnityWebRequest.Get("https://increment.build/adgames.eggscaperoute");
        www.SendWebRequest();
        while (www.isDone == false)
        {
        }
        if ((www.isNetworkError) || (string.IsNullOrEmpty(www.downloadHandler.text)))
        {
            Debug.LogErrorFormat("Error retrieving auto-increment build number:" + www.error);
        }
        else
        {
            Debug.LogFormat("Retrieved auto-increment build number:" + www.downloadHandler.text);
            //Debug.Log( www.downloadHandler.text );
            build = www.downloadHandler.text;
        }
        PlayerSettings.bundleVersion = "1.0";
        PlayerSettings.iOS.buildNumber = build;
        try
        {
            PlayerSettings.Android.bundleVersionCode = Convert.ToInt32(build);
        }
        catch
        {
            Debug.LogErrorFormat("Error converting auto-increment build number to an integer: {0}", build);
        }
    }
}
#endif