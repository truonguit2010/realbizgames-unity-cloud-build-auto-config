using UnityEngine;

public class AutoIncrementVersionCodeInCloudBuild : MonoBehaviour
{
    
    #if UNITY_CLOUD_BUILD
    public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest)
    {
#if AMAZON_STORE
        UnityPurchasingEditor.TargetAndroidStore(AndroidStore.AmazonAppStore);
#endif
        string buildNumber = manifest.GetValue("buildNumber", "0");
        VersionIncrementEdior.increaseBuildVersion(buildNumber);
    }
#endif

}
