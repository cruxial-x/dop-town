using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    private GameObject signTextObject;
    public string textToShow = "Press E to fish";
    private PlayerController playerController;
    private bool playerNearSign = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Guyo").GetComponent<PlayerController>();
        signTextObject = GameObject.Find("Canvas/Text");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNearSign)
        {
            if (Input.GetKeyDown(KeyCode.E) && !signTextObject.activeSelf)
            {
                signTextObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && signTextObject.activeSelf)
            {
                signTextObject.SetActive(false);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            playerNearSign = true;
            signTextObject.SetActive(false);
            signTextObject.GetComponent<Text>().text = textToShow;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            playerNearSign = false;
            signTextObject.SetActive(false);
        }
    }
}
