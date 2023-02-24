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
        // Retrieve the UnityPlayer class. �˯� UnityPlayer ���C
        unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        // Retrieve the UnityPlayerActivity object ( a.k.a. the current context )

       unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");



        // Setup the parameters we want to send to our native plugin.    �]�m�ڭ̭n�o�e��ڭ̪���ʹ��󪺰Ѽ�
        parameters = new object[4];
        parameters[0] = unityActivity;
        parameters[1] = notificationContentTitle;
        parameters[2] = notificationContentText;
        parameters[3] = notificationDescription;

        // Retrieve the "Bridge" from our native plugin. �q�ڭ̪����a�����˯���Bridge���C
        // ! Notice we define the complete package name. �`�N�ڭ̩w�q�F���㪺�]�W�C             
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
    [Tooltip("�W��")]
    public string id;
    [Tooltip("�g��")]
    public double latitude;
    [Tooltip("�n��")]
    public double longitude;
    [Tooltip("�b�|100-300")]
    public float radius = 300;
    [Tooltip("")]
    public int loiteringDelay = 3000;
}
#endif