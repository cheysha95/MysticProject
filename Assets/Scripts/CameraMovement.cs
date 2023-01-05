using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour

{
    public Transform target ;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    public Vector2 minPos;
    public Vector2 maxPos;

    private void Start()
    {
        // now set in editor
        
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        //bounds for main room
        //minPos = new Vector2(8f, -13);
        //maxPos = new Vector2(12f, -7);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        // stops the camera from moving z axis
        desiredPosition.z = -10;
        
        // stops the camera from leaving screen bounds, CLAMP
        if (minPos != Vector2.zero) {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPos.x, maxPos.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPos.y, maxPos.y);
        }
        
        // LERP to catch player
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        
        //included for oriantation
        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;
    }
}