using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerController playerController;
    private bool playerNearRadio = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GameObject.Find("Guyo").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNearRadio)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                }
                else
                {
                    audioSource.Play();
                }
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            playerNearRadio = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            playerNearRadio = false;
        }
    }
}
