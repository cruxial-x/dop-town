using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed of the player movement
    private Vector2 gridMoveDirection; // Direction of the grid movement
    private Vector2 gridPosition; // Current grid position

    // Start is called before the first frame update
    void Start()
    {
        // Initialize grid position to the player's starting position
        gridPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Move();
    }

    void HandleInput()
    {
        // Reset grid move direction
        gridMoveDirection = Vector2.zero;

        // Check for input and set grid move direction
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gridMoveDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            gridMoveDirection = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gridMoveDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gridMoveDirection = Vector2.right;
        }
    }

    void Move()
    {
        // If we're not already moving and a move direction has been set
        if (gridPosition == (Vector2)transform.position && gridMoveDirection != Vector2.zero)
        {
            // Calculate the new grid position
            Vector2 newGridPosition = gridPosition + gridMoveDirection;

            // Start moving towards the new grid position
            StartCoroutine(MoveTowards(newGridPosition));
        }
    }

    IEnumerator MoveTowards(Vector2 destination)
    {
        // While we're not at the destination yet
        while ((Vector2)transform.position != destination)
        {
            // Move a step towards the destination
            transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }

        // Update the current grid position
        gridPosition = transform.position;
    }
}