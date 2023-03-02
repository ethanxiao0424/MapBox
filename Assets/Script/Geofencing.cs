using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Geofencing : MonoBehaviour
{
    [SerializeField] AndroidCaller androidCaller;
    [SerializeField] TMP_InputField iD;
    [SerializeField] TMP_InputField latitude;
    [SerializeField] TMP_InputField longitude;
    [SerializeField] TMP_InputField radius;
    [SerializeField] TMP_InputField loiteringDelay;
    [SerializeField] Button set;

    AndroidJavaObject bridge;
    public GeofencingInput[] geofencingInput;

    object[] parameters;
    public string notificationContentTitle = "Title";
    public string notificationContentText = "ContentText";
    public string notificationDescription = "Description";

    // Start is called before the first frame update
    void Start()
    {
        CallNativePlugin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallNativePlugin()
    {
        // Setup the parameters we want to send to our native plugin.    �]�m�ڭ̭n�o�e��ڭ̪���ʹ��󪺰Ѽ�
        parameters = new object[4];
        parameters[0] = androidCaller.unityActivity;
        parameters[1] = notificationContentTitle;
        parameters[2] = notificationContentText;
        parameters[3] = notificationDescription;

        // Retrieve the "Bridge" from our native plugin. �q�ڭ̪����a�����˯���Bridge���C
        // ! Notice we define the complete package name. �`�N�ڭ̩w�q�F���㪺�]�W�C             
        //bridge = new AndroidJavaObject("com.BrabantWater.mynativemodulegps.Bridge", parameters);

        bridge = new AndroidJavaObject("com.ethan.mygeofencing.Bridge", parameters);

        // Call addGeoFence in bridge, with our parameters.
        foreach (var item in geofencingInput)
        {
            //bridge.Call("addGeoFence", item.id, item.latitude, item.longitude, item.radius, item.loiteringDelay);
            bridge.Call("addGeoFence", item.id, item.latitude, item.longitude, item.radius, item.loiteringDelay);
        }
        bridge.Call("GeoFenceCompleted");
    }

    //public void AddGeoFence(string id, double latitude, double longitude, float radius, int loiteringDelay)
    //{
    //    bridge.Call("addGeoFence", id, latitude, longitude, radius, loiteringDelay);
    //    bridge.Call("GeoFenceCompleted");
    //}
    public void AddGeoFence()
    {
        bridge.Call("addGeoFence", iD.text, latitude.text, longitude.text, radius.text, loiteringDelay.text);
        bridge.Call("GeoFenceCompleted");
    }

    public void RemoveGeoFence(string[] ids)
    {
        bridge.Call("removeGeoFences", ids);
    }

    public void RemoveAllGeoFence()
    {
        bridge.Call("removeAllGeoFences");
    }

    [System.Serializable]
    public class GeofencingInput
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
}
