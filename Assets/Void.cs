using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Void : MonoBehaviour
{
    public string nextScene = "The Rooms";
    public Image blackOverlay;
    public GameObject player; // Assign this in the inspector
    private Vector3 lastPosition;
    private float totalDistanceMoved = 0f;
    private const float distanceToMove = 10f;
    private SpriteRenderer playerSprite;

    void Start()
    {
        lastPosition = player.transform.position;
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceMovedThisFrame = Vector3.Distance(lastPosition, player.transform.position);
        totalDistanceMoved += distanceMovedThisFrame;
        lastPosition = player.transform.position;

        float percentageMoved = Mathf.Clamp01(totalDistanceMoved / distanceToMove);
        blackOverlay.color = new Color(0, 0, 0, percentageMoved);
        playerSprite.color = new Color(1, 1, 1, 1 - percentageMoved); // Fade out player sprite

        if (totalDistanceMoved >= distanceToMove)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}