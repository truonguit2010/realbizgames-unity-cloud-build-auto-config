#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build.Reporting;
#endif
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;


#if UNITY_2018_1_OR_NEWER
public class GameVersionPreProcessor : IPreprocessBuildWithReport
#else
public class GameVersionPreProcessor : IPreprocessBuild 
#endif
{
    public int callbackOrder { get { return 0; } }

#if UNITY_2018_1_OR_NEWER
    public void OnPreprocessBuild(BuildReport report)
#else
    public void OnPreprocessBuild(BuildTarget target, string path)
#endif
    {
        TextAsset versionFile = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/realbizgames.json");
        RealBizGameConfig realBizGameConfig = JsonUtility.FromJson<RealBizGameConfig>(versionFile.text);

        PlayerSettings.bundleVersion = realBizGameConfig.versionName;

        SetupAndroidKeyStore(realBizGameConfig);
    }

    private void SetupAndroidKeyStore(RealBizGameConfig realBizGameConfig) {
        if (string.IsNullOrEmpty(realBizGameConfig.Android_keystoreName) == false) {
            PlayerSettings.Android.keystoreName = realBizGameConfig.Android_keystoreName;
        }
        if (string.IsNullOrEmpty(realBizGameConfig.Android_keystorePass) == false) {
            PlayerSettings.Android.keystorePass = realBizGameConfig.Android_keystorePass;
        }
        if (string.IsNullOrEmpty(realBizGameConfig.Android_keyaliasName) == false) {
            PlayerSettings.Android.keyaliasName = realBizGameConfig.Android_keyaliasName;
        }
        if (string.IsNullOrEmpty(realBizGameConfig.Android_keyaliasPass) == false) {
            PlayerSettings.Android.keyaliasPass = realBizGameConfig.Android_keyaliasPass;
        }
    }
}

