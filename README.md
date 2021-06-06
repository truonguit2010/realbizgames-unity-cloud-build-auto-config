# realbizgames-unity-cloud-build-auto-config

## 1. Goals
1. Auto config Android project.
2. Auto config iOS project when do build the game.
3. Auto INCREASE build number that makes the build always ready for uploading Google Play and AppStore.

## 2. Example

![Alt text](Samples~/Auto_Config_Capabilities.png?raw=true "Optional Title")

![Alt text](Samples~/Auto_Config_Info_Plist.png?raw=true "Optional Title")

## 3. Configuration via an json file

To use this tool, you must naming your file [realbizgames.json](https://github.com/truonguit2010/realbizgames-unity-cloud-build-auto-config/blob/main/Samples%7E/realbizgames.json) 

The content of the file is below:

```
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
```

More infomation about SKAdNetworkIdentifier, Please see it at [ironsource](https://developers.ironsrc.com/ironsource-mobile/general/ios-14-network-support/)

## 4. Auto increase your build number
Your build number must be:
1. Auto increase in every build. It need to be unique for uploading AppStore or Google Play.
2. You saw the build number, You need to know "When did it build?"

Here is the format of the build number: YYMMDDBBB
- YY: For example, Now, the year is 2021. It is the 2-end-numbers, It means 21.
- MM: The month of the year.
- DD: The day of the month.
- BBB: Build number. One day you can build upto 999 builds. 999 is a crazy max build count for a day. If you can touch this number for a day. Please take a rest and wait for the next day to continue. :)

Example: 210606038 = Build 038 at 06 Jun, 2021

# References:
1. [Unity Custom Package](https://docs.unity3d.com/Manual/CustomPackages.html)
2. [Unity Cloud Build](https://docs.unity3d.com/Manual/UnityCloudBuild.html)

