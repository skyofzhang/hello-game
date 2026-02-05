using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public class BuildScript
{
    [MenuItem("Tools/Hello Game/Build Android APK")]
    public static void BuildAndroid()
    {
        string buildPath = "Builds/Android";
        if (!Directory.Exists(buildPath))
            Directory.CreateDirectory(buildPath);

        string apkPath = Path.Combine(buildPath, "HelloGame.apk");
        string[] scenes = new string[] { "Assets/Scenes/MainScene.unity" };

        PlayerSettings.companyName = "AIGameStudio";
        PlayerSettings.productName = "Hello Game";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.aigamestudio.hellogame");
        PlayerSettings.bundleVersion = "1.0.0";
        PlayerSettings.Android.bundleVersionCode = 1;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = apkPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("BUILD SUCCESS: " + apkPath);
            Debug.Log("APK Size: " + (summary.totalSize / 1024 / 1024) + " MB");
        }
        else
        {
            Debug.LogError("BUILD FAILED: " + summary.result);
        }
    }

    public static void BuildFromCommandLine()
    {
        BuildAndroid();
    }
}
