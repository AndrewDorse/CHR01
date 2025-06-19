
//#if UNITY_EDITOR
//using UnityEditor.Callbacks;
//using UnityEditor.iOS.Xcode;
//using UnityEditor;
//using UnityEngine;

//public static class PostProcBuild
//{
//    [PostProcessBuild]
//    public static void AddToEntitlements(BuildTarget buildTarget, string buildPath)
//    {
//        if (buildTarget != BuildTarget.iOS) return;

//        // get project info
//        string pbxPath = PBXProject.GetPBXProjectPath(buildPath);
//        var proj = new PBXProject();
//        proj.ReadFromFile(pbxPath);
//        var guid = proj.GetUnityMainTargetGuid();

//        // get entitlements path
//        string[] idArray = Application.identifier.Split('.');
//        var entitlementsPath = $"Unity-iPhone/{idArray[idArray.Length - 1]}.entitlements";

//        // create capabilities manager
//        var capManager = new ProjectCapabilityManager(pbxPath, entitlementsPath, null, guid);

//        // Add necessary capabilities
//        capManager.AddPushNotifications(true);

//        // Write to file
//        capManager.WriteToFile();
//    }




//}

//#endif