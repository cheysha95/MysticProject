using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public bool isBroken = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void potBreak()
    {
        
        StartCoroutine(potCo());
    }

    public IEnumerator potCo()
    {
        isBroken = true;
        animator.SetBool("isBroken", isBroken);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //come back sometime, pot breaks too fast
        gameObject.SetActive(false);
    }
}
