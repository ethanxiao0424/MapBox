#if UNITY_ANDROID
//basic imports.
using UnityEngine;

public class AndroidCaller : MonoBehaviour
{
    AndroidJavaClass unityPlayerClass;
    AndroidJavaObject unityActivity;
    AndroidJavaObject bridge;

    public string notificationContentTitle = "Title";
    public string notificationContentText = "ContentText";
    public string notificationDescription = "Description";

    object[] parameters;

    public Geofencing[] geofencing;

    void Start()
    {
        //just done for testing purposes
        CallNativePlugin();
        Debug.Log(geofencing[0].id);
    }
    //method that calls our native plugin.
    public void CallNativePlugin()
    {
        // Retrieve the UnityPlayer class. 檢索 UnityPlayer 類。
        unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        // Retrieve the UnityPlayerActivity object ( a.k.a. the current context )

       unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");



        // Setup the parameters we want to send to our native plugin.    設置我們要發送到我們的原生插件的參數
        parameters = new object[4];
        parameters[0] = unityActivity;
        parameters[1] = notificationContentTitle;
        parameters[2] = notificationContentText;
        parameters[3] = notificationDescription;

        // Retrieve the "Bridge" from our native plugin. 從我們的本地插件中檢索“Bridge”。
        // ! Notice we define the complete package name. 注意我們定義了完整的包名。             
        //bridge = new AndroidJavaObject("com.BrabantWater.mynativemodulegps.Bridge", parameters);

        bridge = new AndroidJavaObject("com.ethan.mygeofencing.Bridge", parameters);

        // Call addGeoFence in bridge, with our parameters.
        foreach (var item in geofencing)
        {
            //bridge.Call("addGeoFence", item.id, item.latitude, item.longitude, item.radius, item.loiteringDelay);
            bridge.Call("addGeoFence", item.id, item.latitude, item.longitude, item.radius, item.loiteringDelay);
        }
        bridge.Call("GeoFenceCompleted");
    }
}

[System.Serializable]
public class Geofencing
{
    [Tooltip("名稱")]
    public string id;
    [Tooltip("經度")]
    public double latitude;
    [Tooltip("緯度")]
    public double longitude;
    [Tooltip("半徑100-300")]
    public float radius = 300;
    [Tooltip("")]
    public int loiteringDelay = 3000;
}
#endif