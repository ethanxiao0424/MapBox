using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

public class AndroidCaller : MonoBehaviour
{
    AndroidJavaClass unityPlayerClass;
    public AndroidJavaObject unityActivity;
    AndroidJavaObject bridge;
    
    
    private void Awake()
    {
        //just done for testing purposes
        CallNativePlugin();
    }
    void Start()
    {

    }

    private void Update()
    {
        GetLocation();
    }

    //method that calls our native plugin.
    public void CallNativePlugin()
    {
        // Retrieve the UnityPlayer class. ÀË¯Á UnityPlayer Ãþ¡C
        unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        // Retrieve the UnityPlayerActivity object ( a.k.a. the current context )
        unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
    }


    [SerializeField] Camera _mainCam;
    [SerializeField] AbstractMap _mapRef;
    void GetLocation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosScreen = Input.mousePosition;
            mousePosScreen.z = _mainCam.transform.localPosition.y;
            Vector3 location = _mainCam.ScreenToWorldPoint(mousePosScreen);
            Vector2d coordinate = _mapRef.WorldToGeoPosition(location);
            //Debug.Log(coordinate.x + ", " + coordinate.y);
        }
    }
}