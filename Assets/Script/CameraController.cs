using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed = 0.1f;
    public GameObject player;
    private Transform playerTransform;
    private Vector3 offset;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    public GameObject compass;

    void Start()
    {
        playerTransform = player.transform;
        offset = new Vector3(playerTransform.position.x + xOffset, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
    }
    private void FixedUpdate()
    {
        CamControl();
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform.position);
    }

    [Header("CameraSettings")]
    [SerializeField] Camera cam;
    [SerializeField] Transform character;
    [SerializeField] float MouseZoomSpeed = 10.0f;
    [SerializeField] float TouchZoomSpeed = 0.1f;
    [SerializeField] float ZoomMinBound = 3f;
    [SerializeField] float ZoomMaxBound = 10f;


    void CamControl()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        break;
                    case TouchPhase.Moved:
                        float speed = touch.deltaPosition.x * turnSpeed;
                        offset = Quaternion.AngleAxis(speed, Vector3.up) * offset;
                        compass.transform.Rotate(Vector3.forward, speed);
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        break;
                }
            }

            // Pinch to zoom
            if (Input.touchCount == 2)
            {
                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);

                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);
        }
    }
    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        cam.fieldOfView += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, ZoomMinBound, ZoomMaxBound);

    }
}
