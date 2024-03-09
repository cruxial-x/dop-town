using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the player movement
    private bool movingUp, movingDown, movingRight, movingLeft; // Flags for movement direction
    private bool isMoving; // Flag for movement state
    private Animator animator; // Reference to the Animator component
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Ignore collisions with fish
        int playerLayer = LayerMask.NameToLayer("Player");
        int fishLayer = LayerMask.NameToLayer("Fish");
        Physics2D.IgnoreLayerCollision(playerLayer, fishLayer);
    }

    // Update is called once per frame
    void Update()
    {
        movingUp = Input.GetKey(KeyCode.W);
        movingDown = Input.GetKey(KeyCode.S);
        movingRight = Input.GetKey(KeyCode.D);
        movingLeft = Input.GetKey(KeyCode.A);

        isMoving = movingUp || movingDown || movingRight || movingLeft;

        if (movingLeft)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        animator.SetBool("movingUp", movingUp);
        animator.SetBool("movingDown", movingDown);
        animator.SetBool("isMoving", isMoving);
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;

        if (movingUp)
        {
            movement.y += moveSpeed;
        }
        else if (movingDown)
        {
            movement.y -= moveSpeed;
        }

        if (movingRight)
        {
            movement.x += moveSpeed;
        }
        else if (movingLeft)
        {
            movement.x -= moveSpeed;
        }

        rb.velocity = movement;
    }
}