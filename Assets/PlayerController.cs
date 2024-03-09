using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the player movement
    private float moveHorizontal, moveVertical; // Variables for movement direction
    private bool isMoving; // Flag for movement state
    private Animator animator; // Reference to the Animator component
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private int pixelsPerUnit; // Pixels per unit of camera's PixelPerfectCamera component

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Get the PixelPerfectCamera component from the main camera
        PixelPerfectCamera pixelPerfectCamera = Camera.main.GetComponent<PixelPerfectCamera>();
        pixelsPerUnit = pixelPerfectCamera.assetsPPU;

        // Ignore collisions with fish
        int playerLayer = LayerMask.NameToLayer("Default");
        int fishLayer = LayerMask.NameToLayer("Water");
        Physics2D.IgnoreLayerCollision(playerLayer, fishLayer);
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        isMoving = moveHorizontal != 0 || moveVertical != 0;

        if (moveHorizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveHorizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        animator.SetBool("movingUp", moveVertical > 0);
        animator.SetBool("movingDown", moveVertical < 0);
        animator.SetBool("isMoving", isMoving);
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
        rb.velocity = movement;

        Vector2 pos = rb.position;
        pos.x = Mathf.Round(pos.x * pixelsPerUnit) / pixelsPerUnit;
        pos.y = Mathf.Round(pos.y * pixelsPerUnit) / pixelsPerUnit;
        rb.position = pos;
    }
}