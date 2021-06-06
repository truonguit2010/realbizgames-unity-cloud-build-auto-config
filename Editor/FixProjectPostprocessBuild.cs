using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#if UNITY_EDITOR_OSX
using UnityEditor.iOS.Xcode;
#endif
using UnityEngine;

// https://developers.ironsrc.com/ironsource-mobile/unity/admob-mediation-guide/#step-4
class FixProjectPostprocessBuild : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 2; } }

    // public string ADMOB_IOS_APP_ID = "ca-app-pub-7566607394433529~6764832012";


    public void OnPostprocessBuild(BuildReport report)
    {
        Debug.Log("MyCustomBuildProcessor.OnPostprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);

        TextAsset versionFile = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/realbizgames.json");
        RealBizGameConfig realBizGameConfig = JsonUtility.FromJson<RealBizGameConfig>(versionFile.text);

        Environment.SetEnvironmentVariable("OUTPUT_PATH", report.summary.outputPath);
#if UNITY_EDITOR_OSX
        if (report.summary.platform == BuildTarget.iOS)
        {

            string plistPath = report.summary.outputPath + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;

            string encryptKey = "ITSAppUsesNonExemptEncryption";
            rootDict.SetBoolean(encryptKey, false);

            if (string.IsNullOrEmpty(realBizGameConfig.IOS_GADApplicationIdentifier) == false) {
                plist.root.SetString("GADApplicationIdentifier", realBizGameConfig.IOS_GADApplicationIdentifier);
            } else {
                Debug.LogErrorFormat("FixProjectPostprocessBuild - please add ADMOB_IOS_APP_ID for this project");
            }
        // --------------------------------
        // https://developers.ironsrc.com/ironsource-mobile/unity/unity-plugin/#step-3
        //https://developers.ironsrc.com/ironsource-mobile/general/ios-14-network-support/
            // You can also add SKAdNetworkIdentifier  to your Info.plist, by using this code:
            // <key>SKAdNetworkItems</key>
            //  < array >
            //      < dict >
            //          < key > SKAdNetworkIdentifier </ key >
            //          < string > su67r6k2v3.skadnetwork </ string >
            //      </ dict >
            // </ array >
            //---------------------------------
            PlistElementArray ios14Array = plist.root.CreateArray("SKAdNetworkItems");
            // ironsource
            if (string.IsNullOrEmpty(realBizGameConfig.IOS_SKAdNetworkIdentifier_IronSrc) == false) {
                ios14Array.AddDict().SetString("SKAdNetworkIdentifier", realBizGameConfig.IOS_SKAdNetworkIdentifier_IronSrc);
            }
            // unity
            if (string.IsNullOrEmpty(realBizGameConfig.IOS_SKAdNetworkIdentifier_Unity) == false) {
                ios14Array.AddDict().SetString("SKAdNetworkIdentifier", realBizGameConfig.IOS_SKAdNetworkIdentifier_Unity);
            }
            // FAN
            if (string.IsNullOrEmpty(realBizGameConfig.IOS_SKAdNetworkIdentifier_FAN_CN) == false) {
                ios14Array.AddDict().SetString("SKAdNetworkIdentifier", realBizGameConfig.IOS_SKAdNetworkIdentifier_FAN_CN);
            }
            if (string.IsNullOrEmpty(realBizGameConfig.IOS_SKAdNetworkIdentifier_FAN_N_CN) == false) {
                ios14Array.AddDict().SetString("SKAdNetworkIdentifier", realBizGameConfig.IOS_SKAdNetworkIdentifier_FAN_N_CN);
            }
            // ADMOB
            if (string.IsNullOrEmpty(realBizGameConfig.IOS_SKAdNetworkIdentifier_ADMOB) == false) {
                ios14Array.AddDict().SetString("SKAdNetworkIdentifier", realBizGameConfig.IOS_SKAdNetworkIdentifier_ADMOB);
            }
            // END
            //---------------------------------

            PlistElementDict NSAppTransportSecurity = rootDict.CreateDict("NSAppTransportSecurity");
            NSAppTransportSecurity.SetBoolean("NSAllowsArbitraryLoads", true);

            var key = "NSUserTrackingUsageDescription";
            rootDict.SetString(key, "Your data will be used to deliver personalized ads to you.");

            File.WriteAllText(plistPath, plist.WriteToString());

            addCalabilities(report, realBizGameConfig);
        }
#endif
    }

    public void addCalabilities(BuildReport report, RealBizGameConfig realBizGameConfig) {
#if UNITY_EDITOR_OSX
        if (report.summary.platform == BuildTarget.iOS)
        {
            string pathToBuiltProject = report.summary.outputPath;
            string projectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);

            PBXProject project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));
            string targetGUID = project.GetUnityMainTargetGuid();// PBXProject.GetUnityTargetName(); // note, not "project." ...
            string unityTergetFrameworkGuid = project.GetUnityFrameworkTargetGuid();
            //string targetGUID = project.TargetGuidByName(targetName);

            //AddFrameworks(project, targetName);
            if (realBizGameConfig.IOS_Framework_Security) {
                project.AddFrameworkToProject(targetGUID, "Security.framework", false);
            }
            
            if (realBizGameConfig.IOS_Framework_AdSupport) {
                project.AddFrameworkToProject(targetGUID, "AdSupport.framework", false);
            }
                
            //project.AddFrameworkToProject(targetGUID, "iAd.framework", false);
            if (realBizGameConfig.IOS_Framework_UserNotifications) {
                project.AddFrameworkToProject(targetGUID, "UserNotifications.framework", false);
            }
                
            if (realBizGameConfig.IOS_Framework_StoreKit) {
                project.AddFrameworkToProject(targetGUID, "StoreKit.framework", false);
            }
            
            // 
            // Fix bug: ERROR ITMS-90206: "Invalid Bundle. The bundle at 'matching.app/Frameworks/UnityFramework.framework' contains disallowed file 'Frameworks'."
            //UnityFramework
            // Always Embed Swift Standard Libraries
            project.SetBuildProperty(unityTergetFrameworkGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            project.SetBuildProperty(targetGUID, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

            // Write.
            File.WriteAllText(projectPath, project.WriteToString());

            // Capability
            ProjectCapabilityManager manager = new ProjectCapabilityManager(
                projectPath,
                "Entitlements.entitlements",
                targetGuid: project.GetUnityMainTargetGuid()
            );

            if (realBizGameConfig.IOS_Enable_GameCenter)
                manager.AddGameCenter();

            if (realBizGameConfig.IOS_Enable_PushNotifications) {
                manager.AddPushNotifications(false);
                manager.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);
            }
                
            if (realBizGameConfig.IOS_Enable_InAppPurchase) {
                manager.AddInAppPurchase();
            }
            
            manager.WriteToFile();
        }
#endif
    }
}