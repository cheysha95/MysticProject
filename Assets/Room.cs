using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

    
public class Room : MonoBehaviour
{

    private GameObject virtualCamera;
    

    void Start()
    {
        virtualCamera = transform.GetChild(0).gameObject; //CAMERA SHOULD ALWAYS BE FIRST OBJECT
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) {
            virtualCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) {
            virtualCamera.SetActive(false);
        }
    }
}
