using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int originalSortingOrder;
    private Vector2 enterPosition;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Guyo")
        {
            enterPosition = other.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Guyo")
        {
            // If the player exits to the north or south, set the sorting order back to the original value
            if (other.transform.position.y >= enterPosition.y)
            {
                spriteRenderer.sortingOrder = originalSortingOrder;
            }
            else
            {
                spriteRenderer.sortingOrder = originalSortingOrder - 2;
            }
            Debug.Log("Exit position: " + other.transform.position.y + " Enter position: " + enterPosition.y);
        }
    }

}
