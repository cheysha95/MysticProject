using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class treeEnemy : Enemy // Behavior for slime, will chase when player is within radius, will attack when player is in smaller radius
{
    public Transform target;
    
    public float chaseRadius, attackRadius;
    
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    void FixedUpdate() 
    {
        //if not staggering look for player
        if (CurrentState != genericState.Stagger) {
            checkRadius();
        }
    }

    // looks to see if player is in check raidus and moves twords it if it is, supposed to stop once in attack radius, unique to treeEnemy
    public void checkRadius()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position,transform.position) >= attackRadius) {
            if (CurrentState == genericState.Idle || CurrentState == genericState.Walk) // if not staggering or attacking
            {
                Vector3 tempPos = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //FIX
                rigidBody.MovePosition(tempPos);
                changeState(genericState.Walk);
            }
    
        }
        else { changeState(genericState.Idle);}
    }
}
