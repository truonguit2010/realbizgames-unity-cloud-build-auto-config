using UnityEngine;

[System.Serializable]
public class RealBizGameConfig
{
    public string versionName;
    public string IOS_GADApplicationIdentifier;
    public string IOS_SKAdNetworkIdentifier_IronSrc;
    public string IOS_SKAdNetworkIdentifier_Unity;
    public string IOS_SKAdNetworkIdentifier_FAN_N_CN;
    public string IOS_SKAdNetworkIdentifier_FAN_CN;
    public string IOS_SKAdNetworkIdentifier_ADMOB;
    public bool IOS_Framework_AdSupport;
    public bool IOS_Framework_UserNotifications;
    public bool IOS_Framework_StoreKit;
    public bool IOS_Framework_Security;
    public bool IOS_Enable_GameCenter;
    public bool IOS_Enable_PushNotifications;
    public bool IOS_Enable_BackgroundModes;
    public bool IOS_Enable_InAppPurchase;
    public string Android_keystoreName;
    public string Android_keystorePass;
    public string Android_keyaliasName;
    public string Android_keyaliasPass;
}
