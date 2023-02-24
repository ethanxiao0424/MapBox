#if UNITY_ANDROID
//basic imports.
using Mapbox.Unity.Map;
using Mapbox.Utils;
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

    private void Update()
    {
        GetLocation();
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


    [SerializeField]Camera _mainCam;
    [SerializeField] AbstractMap _mapRef;
    void GetLocation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosScreen = Input.mousePosition;
            mousePosScreen.z = _mainCam.transform.localPosition.y;
            Vector3 location = _mainCam.ScreenToWorldPoint(mousePosScreen);
            Vector2d coordinate = _mapRef.WorldToGeoPosition(location);
            Debug.Log(coordinate.x + ", " + coordinate.y);
        }
    }

    public void AddGeoFence(string id,double latitude,double longitude,float radius,int loiteringDelay)
    {
        bridge.Call("addGeoFence", id, latitude, longitude, radius, loiteringDelay);
        bridge.Call("GeoFenceCompleted");
    }

    public void RemoveGeoFence(string[] ids)
    {
        bridge.Call("removeGeoFences",ids);
    }

    public void RemoveAllGeoFence()
    {
        bridge.Call("removeAllGeoFences");
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
    public float radius;
    [Tooltip("ms")]
    public int loiteringDelay;
}
#endif