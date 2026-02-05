using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public class BuildScript
{
    [MenuItem("Tools/Hello Game/Build Android APK")]
    public static void BuildAndroid()
    {
        // Ensure output directory exists
        string buildPath = "Builds/Android";
        if (\!Directory.Exists(buildPath))
            Directory.CreateDirectory(buildPath);

        string apkPath = Path.Combine(buildPath, "HelloGame.apk");

        // Get scenes
        string[] scenes = new string[] { "Assets/Scenes/MainScene.unity" };

        // Player settings
        PlayerSettings.companyName = "AIGameStudio";
        PlayerSettings.productName = "Hello Game";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.aigamestudio.hellogame");
        PlayerSettings.bundleVersion = "1.0.0";
        PlayerSettings.Android.bundleVersionCode = 1;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

        // Build options
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = apkPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // Execute build
        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("BUILD SUCCESS: " + apkPath);
            Debug.Log("APK Size: " + (summary.totalSize / 1024 / 1024) + " MB");
            Debug.Log("Build Time: " + summary.totalTime.TotalSeconds + " seconds");
        }
        else
        {
            Debug.LogError("BUILD FAILED: " + summary.result);
            foreach (var step in report.steps)
            {
                foreach (var msg in step.messages)
                {
                    if (msg.type == LogType.Error || msg.type == LogType.Warning)
                        Debug.LogError(msg.content);
                }
            }
        }
    }

    // Command line entry point
    public static void BuildFromCommandLine()
    {
        BuildAndroid();
    }
}