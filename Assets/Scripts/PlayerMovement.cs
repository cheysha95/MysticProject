using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public enum genericState { Idle, Walk ,Interact, Attack, Stagger}
    public genericState CurrentState;
    
    private Rigidbody2D rigidBody; // all movement through rb, becuase physics
    private Animator animator; // send messages to animator
    private SpriteRenderer renderer; // flip on x asis when moving left
    
    public GameObject swordObject; // set by unity editor

    public Vector3 movement; // new input system automatically normalizes movement
    public float moveSpeed = 6f;
    
    public FloatValue CurrentHealth;
    public Signal PlayerHealthSignal;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        CurrentState = genericState.Idle;
    }

    void Update ()
    {

        // Cant be fixed update because inputs would have to be exactly 1/30th of a second or somthing
        //Reset movement vector
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        //Attack if you press button and are able to
        if(Input.GetButtonDown("Fire1") && CurrentState != genericState.Attack && CurrentState != genericState.Stagger)
        {
            attack();
        }
        
        //esle Update animator, and move RidgidBody
        else if (CurrentState == genericState.Walk || CurrentState == genericState.Idle)
        {
            updateAnimatorPerameters();
            
            movement.Normalize();
            rigidBody.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
   


    void updateAnimatorPerameters()
    {
        // set animator parameters
            if (movement != Vector3.zero) {
               
               float tempDir = new float();
               if (movement.x == -1) { tempDir = 1 ; renderer.flipX = true;}
               if (movement.x == 1) { tempDir = 1 ; renderer.flipX = false;}
               if (movement.y == -1) { tempDir = 2 ; renderer.flipX = false;}
               if (movement.y == 1) { tempDir = 0 ; renderer.flipX = false;}
               
               animator.SetFloat("FacingDirection", tempDir);
           }
        
            if (movement.x != 0 || movement.y != 0) {
                animator.SetBool("isMoving", true);
            }
            else {
                animator.SetBool("isMoving", false);
            }
    }

    //-------------------------------------------------------------------------------
    public void changeState(genericState state)
    {
        if (CurrentState != state) //this makes sure it only happens once
        {
            CurrentState = state;
        }
    }

    
    private void attack() {
        StartCoroutine(attackCo());
    }
    IEnumerator attackCo()
    {
        animator.SetBool("isAttacking", true);
        changeState(genericState.Attack);
        swordObject.GetComponent<Sword>().Strike(); // whole coroutine may just go it=nto sword script, a coroutine that waits on a coroutine?
            
        yield return null;
        animator.SetBool("isAttacking", false);
            
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        changeState(genericState.Idle);
        swordObject.GetComponent<Sword>().endStrike();
    }
    
    
    //every object capable of knockback has a knockback script that calls this routine on the game object, every thing that can be knocked should have this mehtod.
    public void Knock(float Knocktime, float damage)
    {
        //give damage to current health, if still alive knockback
        CurrentHealth.RuntimeValue -= damage;
        PlayerHealthSignal.Raise(); // raises signal fro ui
        
        if (CurrentHealth.RuntimeValue > 0) {
            StartCoroutine(KnockCo(rigidBody, Knocktime));
        }else {gameObject.SetActive(false);}
    }
    private IEnumerator KnockCo (Rigidbody2D rigidBody, float knockTime) {
        if (CurrentState != genericState.Stagger) {
            CurrentState = genericState.Stagger;
            yield return new WaitForSeconds(knockTime);
            rigidBody.velocity = Vector2.zero; // stop moving
            changeState(genericState.Idle);
        }
    }


    
    

}

