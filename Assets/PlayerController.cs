using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public interface IMovementStrategy
{
    void HandleMovement(ref float moveHorizontal, ref float moveVertical);
}

public class DiagonalMovement : IMovementStrategy
{
    public void HandleMovement(ref float moveHorizontal, ref float moveVertical)
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }
}

public class CardinalMovement : IMovementStrategy
{
    public void HandleMovement(ref float moveHorizontal, ref float moveVertical)
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(inputHorizontal) > Mathf.Abs(inputVertical))
        {
            moveHorizontal = inputHorizontal;
            moveVertical = 0;
        }
        else
        {
            moveVertical = inputVertical;
            moveHorizontal = 0;
        }
    }
}

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [Tooltip("Speed of the player movement")]
    [SerializeField] float moveSpeed = 2f;

    private float moveHorizontal, moveVertical;

    [Tooltip("Flag for movement state")]
    private bool isMoving;

    private Animator animator;
    private Rigidbody2D rb;

    [Tooltip("Pixels per unit of camera's PixelPerfectCamera component")]
    private int pixelsPerUnit;

    [Tooltip("Flag for enabling diagonal movement")]
    [SerializeField] bool enableDiagonalMovement = false;

    private IMovementStrategy movementStrategy;

    [Tooltip("Threshold for changing walking animation")]
    [SerializeField] private float animationThreshold = 0.1f;


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
        movementStrategy = enableDiagonalMovement ? new DiagonalMovement() : new CardinalMovement();
    }

    // Update is called once per frame
    void Update()
    {
        movementStrategy.HandleMovement(ref moveHorizontal, ref moveVertical);

        // If the absolute value of the horizontal or vertical movement is less than the animation threshold, consider it as zero
        float effectiveMoveHorizontal = Mathf.Abs(moveHorizontal) < animationThreshold ? 0 : moveHorizontal;
        float effectiveMoveVertical = Mathf.Abs(moveVertical) < animationThreshold ? 0 : moveVertical;

        isMoving = effectiveMoveHorizontal != 0 || effectiveMoveVertical != 0;

        if (effectiveMoveHorizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (effectiveMoveHorizontal > 0 || (effectiveMoveHorizontal == 0 && effectiveMoveVertical != 0))
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        animator.SetBool("movingUp", effectiveMoveVertical > 0 && effectiveMoveHorizontal == 0);
        animator.SetBool("movingDown", effectiveMoveVertical < 0 && effectiveMoveHorizontal == 0);
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