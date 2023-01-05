using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour // this script schanges the heart container images in the health bar in response to the signals
{
    //the hearts being shown on screen
    public Image[] hearts;
    //possible sprites that we can show
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyheart;
    public FloatValue numberofHearts;
    public FloatValue playerCurrentHealth;
    
    
    
    // Start is called before the first frame update
    void Start() {
        InitHearts();
    }

    //loop through hearts turning onn setting full, etc, so will need to be disabled by default
    public void InitHearts()
    {
        for (int i = 0; i < numberofHearts.initialValue; i++) {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }
    
    //update hearts based on players current health
    public void UpdateHearts()
    {
        var tempHealth = playerCurrentHealth.RuntimeValue / 2; // 1 heart = 2 hp
        
        //loop over all hearts, comparing players current health to them, minus one because of indexing
        for (int i = 0; i < numberofHearts.initialValue; i++) {
            if (i <= tempHealth - 1) {
                //full
                hearts[i].sprite = fullHeart;
            }else if (i >= tempHealth) {
                //empty
                hearts[i].sprite = emptyheart;
            } else {
                //half
                hearts[i].sprite = halfFullHeart; //pt25, 3:49
            }
            
        }
    }

}
