using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the player movement
    private bool movingUp, movingDown, movingRight, movingLeft; // Flags for movement direction
    private bool isMoving; // Flag for movement state
    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        Vector2 movement = new((movingRight ? 1 : 0) - (movingLeft ? 1 : 0), (movingUp ? 1 : 0) - (movingDown ? 1 : 0));
        transform.Translate(moveSpeed * Time.fixedDeltaTime * movement);
    }
}