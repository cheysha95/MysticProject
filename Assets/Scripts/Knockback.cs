using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Knockback : MonoBehaviour
{
    // Knocks other game object backward for a duration of time

    public float thrust;// multiplyiet
    public float staggerTime; // time that enemy moves backward 
    public float damage;
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Enables Knockback between the Player and Enemies
        if (col.CompareTag("Enemy") || col.CompareTag("Player")) 
        {
            Rigidbody2D hitBody = col.GetComponent<Rigidbody2D>();
            if (hitBody != null) 
            {
                // get difference between the two colliding objects and applies a force based on that
                Vector2 difference = hitBody.transform.position - transform.position;
                difference = difference.normalized * thrust; // needs normalized, make sure it has length of 1
                hitBody.AddForce(difference,ForceMode2D.Impulse); 
                
                // maybe another check to make sure theyre not already staggering?
                
                //Runs individual knock methods based on class
                if (hitBody.CompareTag("Enemy") && col.isTrigger) { // is hitt both colliders, make the hit box on a child object of enemy, tag it as enemy, hitbox not tagged, same for player and room triggers
                    hitBody.GetComponent<Enemy>().Knock(hitBody,staggerTime, damage); 
                }
                if (hitBody.CompareTag("Player") && col.isTrigger) {
                    hitBody.GetComponent<PlayerMovement>().Knock(staggerTime, damage); 
                }
            }
        }
        
        //lets player sword break pots and such
        if (col.CompareTag("Breakable") && this.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<Pot>().potBreak();
        }
    }
}
