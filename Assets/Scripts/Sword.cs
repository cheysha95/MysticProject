using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private BoxCollider2D swordHitbox;
    private Vector3 direction;

    //public void Start() { direction = Vector3.down; }

    private void FixedUpdate()
    {
        var parentDirection = GetComponentInParent<PlayerMovement>().movement;
        if (parentDirection != Vector3.zero) {
            direction = parentDirection;
        } 
    }

    void Awake() // awake is called earlier than start
    {
        swordHitbox = GetComponent<BoxCollider2D>();
    }
    
    
    // really just generates the sword hitbox
    // should probaly just make this into a coroutine
    public void Strike()
    {
        swordHitbox.enabled = true;
        if(direction == Vector3.up) {
            swordHitbox.size = new Vector2(1.222f,.626f);
            swordHitbox.offset = new Vector2(-.671f,.244f);
        }
        
        if(direction == Vector3.down) {
            swordHitbox.size = new Vector2(1.222f,.626f);
            swordHitbox.offset = new Vector2(-.384f,-.533f);
        }
        
        if(direction == Vector3.left) {
            swordHitbox.size = new Vector2(1.45f,.8033f);
            swordHitbox.offset = new Vector2(-1,-.301f);
        }

        if(direction == Vector3.right) {
            swordHitbox.size = new Vector2(1.865f,.8033f);
            swordHitbox.offset = new Vector2(-.393f,-.301f);
        }
    }
    public void endStrike()
    {
        swordHitbox.enabled = false;
    }

}
