using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    //canvas childobject, set in inspector
    private GameObject DialogBox; 
    //textbox childobject
    private Text DialogText;
    // string to change canvas text to, set in inspector
    public string Dialog;
    // set by ontrigger, onexit
    private bool playerInRange;

    private void Start()
    {
        //LOOKS FOR CANVAS BY NAME
        var canvas = GameObject.Find("Canvas");
        DialogBox = canvas.transform.GetChild(1).GameObject();
        
        // gets the text in the dialog box object in the canvas
        DialogText = DialogBox.transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    { 
        //if were in range and we press a button
        if (Input.GetButtonDown("Fire2") && playerInRange) {
            if (DialogBox.activeInHierarchy ) {
                DialogBox.SetActive(false);
            }
            else { 
                DialogBox.SetActive(true);
                DialogText.text = Dialog;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    //disable if we walk away
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            DialogBox.SetActive(false);
        }
    }


}
