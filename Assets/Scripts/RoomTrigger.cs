using System;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomTrigger : MonoBehaviour
{
    public string direction; //set by tiled map?
    
    public Vector2 cameraShift; 
    public Vector3 playerShift;

    public float roomSize = 20;
    public float playerMove = 2;
    
    public CameraMovement camera;
    
    void Start()
    {
        camera = Camera.main.GetComponent<CameraMovement>();
        // bring over from custom properties
        direction = gameObject.GetComponent<SuperCustomProperties>().m_Properties[0].m_Value;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger == false) {
            

            if (direction == "UP") {
                cameraShift = new Vector2(0, roomSize);
                playerShift = new Vector3(0, playerMove);
            }

            if (direction == "DOWN") {
                cameraShift = new Vector2(0, -roomSize);
                playerShift = new Vector3(0, -playerMove);
            }

            if (direction == "LEFT") {
                cameraShift = new Vector2(-roomSize, 0);
                playerShift = new Vector3(-playerMove - 0.5f, 0);
            }

            if (direction == "RIGHT") {
                cameraShift = new Vector2(roomSize, 0);
                playerShift = new Vector3(playerMove + 0.5f, 0);
            }

            
            // adds amount to min and maxpos +x or -x depending on direction, if dealing with different 
            //sized rooms this will need to be updated
            camera.minPos += cameraShift; 
            camera.maxPos += cameraShift;
            
            //move player, using transform because we don't want physics
            other.transform.position += playerShift;
        }
    }
}
