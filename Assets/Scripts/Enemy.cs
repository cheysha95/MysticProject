using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum genericState { Idle, Walk ,Interact, Attack, Stagger}
    public genericState CurrentState;

    public Animator animator;
    public Rigidbody2D rigidBody;
    
    //-------------------------------------------------------------
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public float moveSpeed;
    public float baseAttack;
    

    public void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth.initialValue;
        rigidBody = GetComponent<Rigidbody2D>();
    }


    public void changeState(genericState state)
    {
        if (CurrentState != state) //this makes sure it only happens once
        {
            CurrentState = state;
        }
    }

    //--------------------------------------------------------------------------------------------------

    private void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 ) {
            this.gameObject.SetActive(false);
        }
    }

    
    public void Knock(Rigidbody2D rigidBody, float Knocktime, float damage) { //damage
        StartCoroutine(KnockCo(rigidBody, Knocktime));
        takeDamage(damage); // take damage after coroutine to avoid erros
    }
    private IEnumerator KnockCo (Rigidbody2D rigidBody, float knockTime) {
        if (CurrentState != genericState.Stagger) {
            
            changeState(genericState.Stagger);
            yield return new WaitForSeconds(knockTime);
            rigidBody.velocity = Vector2.zero;
            changeState(genericState.Idle);
        }
    }
    
    
    // -was used for slime
    public void Die() { StartCoroutine(die()); }
    public IEnumerator die() {
        animator.Play("Die");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        Debug.Log("dead");
    }
    
}
