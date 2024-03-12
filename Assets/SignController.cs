using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public GameObject signTextObject;
    public string textToShow = "Press E to fish";
    public GameObject player;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isNearSign)
        {
            if (Input.GetKeyDown(KeyCode.E) && !signTextObject.activeSelf)
            {
                signTextObject.SetActive(true);
                signTextObject.GetComponent<Text>().text = textToShow;
            }
            else if (Input.GetKeyDown(KeyCode.E) && signTextObject.activeSelf)
            {
                signTextObject.SetActive(false);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerController.isNearSign = true;
            signTextObject.SetActive(true);
            signTextObject.GetComponent<Text>().text = textToShow;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerController.isNearSign = false;
            signTextObject.SetActive(false);
        }
    }
}
