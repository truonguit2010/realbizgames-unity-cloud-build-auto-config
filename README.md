# realbizgames-unity-cloud-build-auto-config

## 1. Goals
1. Auto config Android project.
2. Auto config iOS project when do build the game.
3. Auto INCREASE build number that makes the build always ready for uploading Google Play and AppStore.

## 2. Example

![Alt text](Samples~/Auto_Config_Capabilities.png?raw=true "Optional Title")

![Alt text](Samples~/Auto_Config_Info_Plist.png?raw=true "Optional Title")

## 3. Configuration via an json file

`
{
    "versionName": "1.1",

    "IOS_GADApplicationIdentifier": "YOUR_ADMOB_IOS_APPLICATION_ID",

    "IOS_SKAdNetworkIdentifier_Info_URL": "https://developers.ironsrc.com/ironsource-mobile/general/ios-14-network-support/",
    "IOS_SKAdNetworkIdentifier_IronSrc": "su67r6k2v3.skadnetwork",
    "IOS_SKAdNetworkIdentifier_Unity": "4dzt52r2t5.skadnetwork",
    "IOS_SKAdNetworkIdentifier_FAN_N_CN": "v9wttpbfk9.skadnetwork",
    "IOS_SKAdNetworkIdentifier_FAN_CN": "n38lu8286q.skadnetwork",
    "IOS_SKAdNetworkIdentifier_ADMOB": "cstr6suwn9.skadnetwork",

    "IOS_Framework_AdSupport": "AdSupport.framework",
    "IOS_Framework_UserNotifications": "UserNotifications.framework",
    "IOS_Framework_StoreKit": "StoreKit.framework",
    "IOS_Framework_Security": "Security.framework",

    "IOS_Enable_GameCenter": true,
    "IOS_Enable_PushNotifications": true,
    "IOS_Enable_BackgroundModes": true,
    "IOS_Enable_InAppPurchase": true,
    
    "Android_keystoreName": "your_keystore_name",
    "Android_keystorePass": "your_keystore_pass",
    "Android_keyaliasName": "your_keystore_alias",
    "Android_keyaliasPass": "your_keystore_alias_pass"
}
`

